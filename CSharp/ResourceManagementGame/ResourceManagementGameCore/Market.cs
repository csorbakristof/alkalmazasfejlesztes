using ResourceManagementGameCore.Algos;
using ResourceManagementGameCore.Buildings;
using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Factories;
using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace ResourceManagementGameCore
{
    public static class Market
    {
        public static Dictionary<Type, List<ResourceAmount>> BuildingSales { get; set; }
        private static Dictionary<(Type, string), IAlgo> BuildingCostIncrement { get; set; }
        public static Dictionary<Type, List<ResourceAmount>> UnitSales { get; set; }
        private static Dictionary<(Type, string), IAlgo> UnitCostIncrement { get; set; }
        //market-ről tudsz venni
        public static Dictionary<string, List<ResourceAmount>> ResourceSales { get; set; }
        //amit a market felvásárol tőled
        public static Dictionary<string, List<ResourceAmount>> ResourceOffers { get; set; }
        public static int SatisfactionSale { get;private set; }
        private static IAlgo SatisfactionCostIncrement;
        public static void ResetMarket()
        {
            UnitSales =  ConfigurationManager.GetInstance().GetUnitInitialPrices();
            UnitCostIncrement = new Dictionary<(Type, string), IAlgo>();
            var algos = ConfigurationManager.GetInstance().GetUnitIncrementAlgos();
            var algoValues = ConfigurationManager.GetInstance().GetUnitIncrementAlgoValues();
            foreach (var sale in UnitSales)
            {
                foreach (var res in sale.Value)
                {
                    UnitCostIncrement.Add((sale.Key, res.Type), AlgoFactory.CreateAlgo(algos[sale.Key], res.Amount, algoValues[sale.Key]));
                }
                
            }
            BuildingSales = ConfigurationManager.GetInstance().GetBuildingInitialPrices();
            BuildingCostIncrement = new Dictionary<(Type, string), IAlgo>();
            algos = ConfigurationManager.GetInstance().GetBuildingIncrementAlgos();
            algoValues = ConfigurationManager.GetInstance().GetBuildingIncrementAlgoValues();
            foreach (var sale in BuildingSales)
            {
                foreach (var res in sale.Value)
                {
                    BuildingCostIncrement.Add((sale.Key, res.Type), AlgoFactory.CreateAlgo(algos[sale.Key], res.Amount, algoValues[sale.Key]));
                }
            }
            var satisfactionBoost = ConfigurationManager.GetInstance().GetSatisfactionBoostAlgo();
            if (satisfactionBoost.initial.HasValue)
                SatisfactionSale = satisfactionBoost.initial.Value;
            SatisfactionCostIncrement = AlgoFactory.CreateAlgo(satisfactionBoost.algoType, satisfactionBoost.initial, satisfactionBoost.algoValue);

            ResourceSales = ConfigurationManager.GetInstance().GetSellingPrices();
            ResourceOffers = ConfigurationManager.GetInstance().GetBuyingPrices();

            //fix initial "getnext"
            foreach (var item in UnitCostIncrement)
            {
                item.Value.GetNext();
            }
            foreach(var item in BuildingCostIncrement)
            {
                item.Value.GetNext();
            }
            SatisfactionSale = SatisfactionCostIncrement.GetNext();
        }
        static Market()
        {
            ResetMarket();
        }

        public static Building SellBuilding(Type building)
        {
            if (!BuildingSales.ContainsKey(building)) return null;
            foreach (var item in BuildingSales[building])
            {
                item.Amount = BuildingCostIncrement[(building, item.Type)].GetNext();
            }
            return BuildingFactory.CreateBuilding(building);
        }
        public static Unit SellUnit(Type unitType)
        {
            if (!UnitSales.ContainsKey(unitType)) return null;
            foreach (var item in UnitSales[unitType])
            {
                item.Amount = UnitCostIncrement[(unitType, item.Type)].GetNext();
            }
            var res = UnitFactory.CreateUnit(unitType);
            return res;
        }
        public static int SellSatisfactionBoost()
        {
            SatisfactionSale = SatisfactionCostIncrement.GetNext();
            return 10;
        }
        public static ResourceAmount SellResource(string resource)
        {
            if (ResourceSales.ContainsKey(resource))
                return new ResourceAmount(resource, 1);
            throw new Exception("Invalid resource type");
        }
        public static List<ResourceAmount> BuyResource(string resource)
        {
            if (ResourceOffers.ContainsKey(resource))
                return ResourceOffers[resource].Select(r => new ResourceAmount(r.Type, r.Amount)).ToList();
            throw new Exception("Invalid resource type");
        }
    }
}
