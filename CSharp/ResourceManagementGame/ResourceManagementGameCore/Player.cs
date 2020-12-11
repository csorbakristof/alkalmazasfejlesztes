using ResourceManagementGameCore.Algos;
using ResourceManagementGameCore.Buildings;
using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Factories;
using ResourceManagementGameCore.Research;
using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ResourceManagementGameCore
{
    public class Player
    {
        public List<Unit> AllUnits { get; set; }
        public List<Unit> UnassignedUnits { get; set; }
        public List<Building> NonfunctionalBuildings { get; set; }
        public List<Building> FunctionalBuildings { get; set; }
        public Dictionary<string, int> Resources { get; set; }
        public int RoundCounter { get;private set; }
        private int satisfactionPercentage;
        public int SatisfactionPercentage
        {
            get => satisfactionPercentage;
            private set
            {
                satisfactionPercentage = value;
                if (value >= 80)
                    Satisfaction = Satisfaction.Happy;
                else if (value >= 1)
                    Satisfaction = Satisfaction.Ok;
                else
                    Satisfaction = Satisfaction.Unsatisfied;
            }
        }
        private Satisfaction satisfaction;
        public Satisfaction Satisfaction
        {
            get => satisfaction;
            private set
            {
                    satisfaction = value;
                    foreach (var item in AllUnits)
                    {
                        item.NotifySatisfactionChange(value);
                    }
            }
        }
        private IAlgo ResearchCostIncrementAlgo;
        public int ResearchCurrentCost { get; private set; }
        private static Player _instance;
        public static Player GetInstance()
        {
            if (_instance == null) _instance = new Player();
            return _instance;
        }
        public void ResetPlayer()
        {
            //2 gatherer, 1 researcher, 1 builder
            AllUnits = new List<Unit>();
            UnassignedUnits = new List<Unit>();
            var gatherer1 = UnitFactory.CreateUnit(typeof(Gatherer));
            var gatherer2 = UnitFactory.CreateUnit(typeof(Gatherer));
            var researcher = UnitFactory.CreateUnit(typeof(Researcher));
            var builder = UnitFactory.CreateUnit(typeof(Builder));
            AllUnits.Add(gatherer1);
            AllUnits.Add(gatherer2);
            AllUnits.Add(researcher);
            AllUnits.Add(builder);
            UnassignedUnits.Add(gatherer1);
            UnassignedUnits.Add(gatherer2);
            UnassignedUnits.Add(researcher);
            UnassignedUnits.Add(builder);

            //1 forestcamp, 1 miningvillage, 1 lab
            //AllBuildings = new List<Building>();
            FunctionalBuildings = new List<Building>();
            NonfunctionalBuildings = new List<Building>();
            var forestCamp = BuildingFactory.CreateBuilding(typeof(ForestCamp));
            var miningVillage = BuildingFactory.CreateBuilding(typeof(MiningVillage));
            var lab = BuildingFactory.CreateBuilding(typeof(Lab));
            NonfunctionalBuildings.Add(forestCamp);
            NonfunctionalBuildings.Add(miningVillage);
            NonfunctionalBuildings.Add(lab);
            RoundCounter = 0;

            Resources = new Dictionary<string, int>();

            SatisfactionPercentage = 100;
            //init: satisfaction happy

            //research algo
            var researchAlgo = ConfigurationManager.GetInstance().GetResearchCostIncrementAlgo();
            ResearchCostIncrementAlgo = AlgoFactory.CreateAlgo(researchAlgo.algoType, researchAlgo.initialValue, researchAlgo.algoSecondValue);
            ResearchCurrentCost = ResearchCostIncrementAlgo.GetNext();

            //research tree
            var researchBuilder = new ResearchBuilder();
            researchBuilder.AddLeaf(ResearchType.ProducerResourceUnlock,
                typeof(Gatherer),
                new ProducerProductParameters("ore"));
            researchBuilder.AddNode(ResearchType.ConverterInputResourceUnlock,
                typeof(ResearchType),
                new ConverterInputResourceParameters("ore"));
            researchBuilder.AddNode(ResearchType.ProducerAmountBoost,
                typeof(Gatherer),
                new ProducerAmountParameters(new ResourceAmount("wood", 1)));
            researchBuilder.AddNode(ResearchType.ProducerAmountBoost,
                typeof(Gatherer),
                new ProducerAmountParameters(new ResourceAmount("rock", 1)));
            researchBuilder.AddNode(ResearchType.ProducerAmountBoost,
                typeof(Gatherer),
                new ProducerAmountParameters(new ResourceAmount("ore", 1)));
            researchBuilder.AddNode(ResearchType.ProducerAmountBoost,
                typeof(Researcher),
                new ProducerAmountParameters(new ResourceAmount("researchpoint", 1)));
            researchBuilder.ChangeToLower(1);
            researchBuilder.AddNode(ResearchType.ConverterOutputUnlock,
                typeof(Researcher),
                new ConverterOutputForInputResourceParameter("ore", new List<ResourceAmount>() { new ResourceAmount("ironore", 1) }));
            researchBuilder.ChangeToLower(0);
            researchBuilder.AddLeaf(ResearchType.ConverterOutputUnlock,
                typeof(Researcher),
                new ConverterOutputForInputResourceParameter("ore", new List<ResourceAmount>() { new ResourceAmount("goldore", 1) }));

            RootResearchItem = researchBuilder.Root;
        }
        private Player()
        {
            this.ResetPlayer();
            
        }
        #region AssignWorkers
        public void AssignConstructionUnitToBuilding(Building building)
        {
            var unit = UnassignedUnits.Where(u => u is ConstructionUnit).FirstOrDefault();
            if (unit is null) return;
            var constructionUnit = (ConstructionUnit)unit;
            UnassignedUnits.Remove(unit);

            building.AddConstructionUnit(constructionUnit);
        }
        public void RemoveConstructionUnitFromBuilding(Building building)
        {
            var unit = building.RemoveLastConstructionUnit();
            if (unit is null) return;
            UnassignedUnits.Add(unit);
        }
        public void AssignUnitToWorkplaceBuilding(WorkplaceBuilding building)
        {
            try
            {
                var types = building.UnitTypes;
                var unit = UnassignedUnits.Where(u => types.Any(t => u.GetType() == t)).FirstOrDefault();
                if (unit is null) return;
                building.AssignUnit(unit);
                UnassignedUnits.Remove(unit);
            }
            catch
            {
                //típus vagy max cap hiba történt
            }
        }
        public void RemoveUnitFromWorkplaceBuilding(WorkplaceBuilding building)
        {
            var unit = building.RemoveLastUnit();
            if (unit is null) return;
            UnassignedUnits.Add(unit);
        }
        #endregion
        #region ConverterInputs
        public void AddInputForConverterBuilding(ResourceAmount input, ConverterBuilding converterBuilding)
        {
            try
            {
                if (Resources.ContainsKey(input.Type))
                {
                    if (Resources[input.Type] >= input.Amount)
                        converterBuilding.AddInput(input); //hibakezelés??
                    else
                        throw new Exception("Not enough resource to add to converter");
                }
                else
                {
                    throw new Exception("Not enough resource to add to converter");
                }
            }
            catch
            {
                //hibakezelés?
            }
        }
        public void AddInputForConverterBuilding(ResourceAmount input, ProducerAndConverterBuilding producerAndConverterBuilding)
        {
            try
            {
                if (Resources.ContainsKey(input.Type))
                {
                    if (Resources[input.Type] >= input.Amount)
                        producerAndConverterBuilding.AddInput(input); //hibakezelés??
                    else
                        throw new Exception("Not enough resource to add to converter");
                }
                else
                {
                    throw new Exception("Not enough resource to add to converter");
                }
            }
            catch
            {
                //hibakezelés?
            }
        }
        public void RemoveInputFromConverterBuilding(ResourceAmount input, ConverterBuilding converterBuilding)
        {
            try
            {
                converterBuilding.RemoveInput(input);
                if (Resources.ContainsKey(input.Type))
                {
                    Resources[input.Type] += input.Amount;
                }
                else
                    Resources.Add(input.Type, input.Amount);
            }
            catch
            {
                //hibakezelés?
            }
        }
        public void RemoveInputFromConverterBuilding(ResourceAmount input, ProducerAndConverterBuilding producerAndConverterBuilding)
        {
            try
            {
                producerAndConverterBuilding.RemoveInput(input);
                if (Resources.ContainsKey(input.Type))
                {
                    Resources[input.Type] += input.Amount;
                }
                else
                    Resources.Add(input.Type, input.Amount);
            }
            catch
            {
                //hibakezelés?
            }            
        }
        #endregion
        #region Trade
        public void BuyBuilding(Type building)
        {
            List<ResourceAmount> cost = new List<ResourceAmount>();
            if(Market.BuildingSales.TryGetValue(building, out cost))
            {
                try
                {
                    if(cost.Any(c=> Resources[c.Type] < c.Amount))
                    {
                        //kevesebb áll rendelkezésre mint kellene
                        throw new Exception("Not enought resource to buy this");
                    }
                    foreach (var item in cost)
                    {
                        Resources[item.Type] -= item.Amount;
                        var b = Market.SellBuilding(building);
                        NonfunctionalBuildings.Add(b);
                    }
                }
                catch
                {
                    //magic, lekezelni hogy kevés a money
                }
            }
            else
            {
                //nincs ilyen building...
            }
        }
        public void BuyUnit(Type unit)
        {
            List<ResourceAmount> cost = new List<ResourceAmount>();
            if (Market.UnitSales.TryGetValue(unit, out cost))
            {
                try
                {
                    if (cost.Any(c => Resources[c.Type] < c.Amount))
                    {
                        //kevesebb áll rendelkezésre mint kellene
                        throw new Exception("Not enought resource to buy this");
                    }
                    foreach (var item in cost)
                    {
                        Resources[item.Type] -= item.Amount;
                        var u = Market.SellUnit(unit);
                        AllUnits.Add(u);
                        UnassignedUnits.Add(u);
                    }
                }
                catch
                {
                    //magic, lekezelni hogy kevés a money
                }
            }
            else
            {
                //nincs ilyen unit...
            }
        }
        public void BuyResource(string resource)
        {
            List<ResourceAmount> cost = new List<ResourceAmount>();
            if (Market.ResourceSales.TryGetValue(resource, out cost))
            {
                try
                {
                    if (cost.Any(c => Resources[c.Type] < c.Amount))
                    {
                        //kevesebb áll rendelkezésre mint kellene
                        throw new Exception("Not enought resource to buy this");
                    }
                    foreach (var item in cost)
                    {
                        Resources[item.Type] -= item.Amount;
                        var r = Market.SellResource(resource);
                        Resources.Add(r.Type, r.Amount);
                    }
                }
                catch
                {
                    //magic, lekezelni hogy kevés a money
                }
            }
            else
            {
                //nincs ilyen resource...
            }
        }
        public void BuySatisfactionBoost()
        {
            try
            {
                if (Resources["coin"] < Market.SatisfactionSale)
                    throw new Exception("Not enought resource to buy this");
                Resources["coin"] -= Market.SatisfactionSale;
                SatisfactionPercentage += Market.SellSatisfactionBoost();
            }
            catch
            {
                //magic, lekezelni hogy kevés a money
            }
        }
        public void SellResource(string resource)
        {
            List<ResourceAmount> income = new List<ResourceAmount>();
            if (Market.ResourceOffers.TryGetValue(resource, out income))
            {
                try
                {
                    if (!Resources.ContainsKey(resource))
                        throw new Exception("Not enought resource to sell");
                    if (Resources[resource] <= 0)
                    {
                        //kevesebb áll rendelkezésre mint kellene
                        throw new Exception("Not enought resource to sell");
                    }
                    Resources[resource]--;
                    income = Market.BuyResource(resource);
                    foreach (var item in income)
                    {
                        if (Resources.ContainsKey(item.Type))
                            Resources[item.Type] += item.Amount;
                        else
                            Resources.Add(item.Type, item.Amount);
                    }
                }
                catch
                {
                    //magic lekezelni hogy nincs elég shit
                }
                
            }
            else
            {
                //nincs ilyen resource...
            }
        }
        #endregion
        #region Research
        public RootResearchItem RootResearchItem { get; set; }
        public void UnlockResearch(ResearchItem research)
        {
            if (!RootResearchItem.Contains(research))
                //nincs ilyen research item...
                return;
            if (research.IsApplied)
                //van ilyen, de már érvényes és van
                return;
            if (!research.Parent.IsApplied)
                //a szülő elem még nincs unlock-olva
                return;
            if (Resources["researchpoint"] < ResearchCurrentCost)
                //nincs elég researchpoint megvenni...
                return;

            foreach (var item in AllUnits)
            {
                if (item.GetType() == research.UnlockOn)
                    research.Unlock(item);
            }
            Resources["researchpoint"] -= ResearchCurrentCost;
            ResearchCurrentCost = ResearchCostIncrementAlgo.GetNext();
        }

        #endregion
        public void StartRound()
        {
            //trade+assign done b4
            //dowork+dobuild
            foreach (WorkplaceBuilding item in FunctionalBuildings.Where(b=>b is WorkplaceBuilding))
            {
                item.DoWork().ForEach(p =>
                {
                    if (Resources.ContainsKey(p.Type))
                        Resources[p.Type] += p.Amount;
                    else Resources.Add(p.Type, p.Amount);
                });
            }

            foreach (Building item in NonfunctionalBuildings)
            {
                item.DoBuildProcess();
                if (item.AbleToFunction)
                    FunctionalBuildings.Add(item);
            }
            NonfunctionalBuildings.RemoveAll(b => b.AbleToFunction);
            //consumefood //első 5 körben nem
            if (RoundCounter >= 5)
            {
                var food = Resources.Where(r => ResourceType.IsFoodResourceType(r.Key)).Select(r => new ResourceAmount(r.Key, r.Value));
                foreach (var item in AllUnits)
                {
                    var choosen = food.Where(r => r.Amount >= item.FoodNeeded).FirstOrDefault();
                    if (!item.ConsumedFood(choosen))
                        SatisfactionPercentage--;
                    else
                        Resources[choosen.Type] -= item.FoodNeeded;
                }
            }
            RoundCounter++;
        }
    }
}
