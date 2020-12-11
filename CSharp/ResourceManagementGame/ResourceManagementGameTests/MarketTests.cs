using ResourceManagementGameCore;
using ResourceManagementGameCore.Algos;
using ResourceManagementGameCore.Buildings;
using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Factories;
using ResourceManagementGameCore.Factories.BuildingFactories;
using ResourceManagementGameCore.Factories.UnitFactories;
using ResourceManagementGameCore.Units;
using System.Linq;
using Xunit;

namespace ResourceManagementGameTests
{
    [Collection("GameTests")]
    public class MarketTests
    {
        public MarketTests()
        {
            ConfigurationManager.SetJson(Utility.CONFIGFILE_JSON);

            AlgoFactory.CleanFactories();
            AlgoFactory.AddFactory(AlgoType.Constant);
            AlgoFactory.AddFactory(AlgoType.Exp);
            AlgoFactory.AddFactory(AlgoType.Fibonacci);
            AlgoFactory.AddFactory(AlgoType.Increment);
            AlgoFactory.AddFactory(AlgoType.Multiple);

            UnitFactory.CleanFactories();
            GathererUnitRegister gathererVisitor = new GathererUnitRegister();
            UnitFactory.AddFactory(gathererVisitor);
            BuilderUnitRegister builderVisitor = new BuilderUnitRegister();
            UnitFactory.AddFactory(builderVisitor);
            FarmerUnitRegister farmerVisitor = new FarmerUnitRegister();
            UnitFactory.AddFactory(farmerVisitor);
            ResearcherUnitRegister researcherVisitor = new ResearcherUnitRegister();
            UnitFactory.AddFactory(researcherVisitor);

            BuildingFactory.CleanFactories();

            ForestCampBuildingRegister forestCampVisitor = new ForestCampBuildingRegister();
            BuildingFactory.AddFactory(forestCampVisitor);
            MiningVillageBuildingRegister miningVillageVisitor = new MiningVillageBuildingRegister();
            BuildingFactory.AddFactory(miningVillageVisitor);
            FarmBuildingRegister farmVisitor = new FarmBuildingRegister();
            BuildingFactory.AddFactory(farmVisitor);
            LabBuildingRegister labVisitor = new LabBuildingRegister();
            BuildingFactory.AddFactory(labVisitor);

            Market.ResetMarket();
        }
        [Fact]
        public void MarketInitialisedTest()
        {
            var unitSales = Market.UnitSales;
            Assert.NotNull(unitSales[typeof(Gatherer)]);
            var gathererSale = unitSales[typeof(Gatherer)];
            Assert.Single(gathererSale);
            Assert.Single(gathererSale.Where(s => s.Type == "coin" && s.Amount == 50));
            Assert.NotNull(unitSales[typeof(Builder)]);
            var builderSale = unitSales[typeof(Builder)];
            Assert.Single(builderSale);
            Assert.Single(builderSale.Where(s => s.Type == "coin" && s.Amount == 100));
            Assert.NotNull(unitSales[typeof(Farmer)]);
            var farmerSale = unitSales[typeof(Farmer)];
            Assert.Single(farmerSale);
            Assert.Single(farmerSale.Where(s => s.Type == "coin" && s.Amount == 50));
            var researcherSale = unitSales[typeof(Researcher)];
            Assert.Single(researcherSale);
            Assert.Single(researcherSale.Where(s => s.Type == "coin" && s.Amount == 100));

            var buidlingSales = Market.BuildingSales;
            Assert.NotNull(buidlingSales[typeof(ForestCamp)]);
            var forestCampSale = buidlingSales[typeof(ForestCamp)];
            Assert.Equal(2, forestCampSale.Count);
            Assert.Single(forestCampSale.Where(s => s.Type == "wood" && s.Amount == 10));
            Assert.Single(forestCampSale.Where(s => s.Type == "rock" && s.Amount == 5));
            Assert.NotNull(buidlingSales[typeof(MiningVillage)]);
            var miningVillageSale = buidlingSales[typeof(MiningVillage)];
            Assert.Equal(2, miningVillageSale.Count);
            Assert.Single(miningVillageSale.Where(s => s.Type == "wood" && s.Amount == 10));
            Assert.Single(miningVillageSale.Where(s => s.Type == "rock" && s.Amount == 5));
            Assert.NotNull(buidlingSales[typeof(Lab)]);
            var labSale = buidlingSales[typeof(Lab)];
            Assert.Equal(3, labSale.Count);
            Assert.Single(labSale.Where(s => s.Type == "wood" && s.Amount == 30));
            Assert.Single(labSale.Where(s => s.Type == "rock" && s.Amount == 20));
            Assert.Single(labSale.Where(s => s.Type == "ironore" && s.Amount == 10));
            Assert.NotNull(buidlingSales[typeof(Farm)]);
            var farmSale = buidlingSales[typeof(Farm)];
            Assert.Equal(3, farmSale.Count);
            Assert.Single(farmSale.Where(s => s.Type == "wood" && s.Amount == 50));
            Assert.Single(farmSale.Where(s => s.Type == "rock" && s.Amount == 10));
            Assert.Single(farmSale.Where(s => s.Type == "ironore" && s.Amount == 10));

            var resourceSales = Market.ResourceSales;
            Assert.Equal(3, resourceSales.Count);
            Assert.NotNull(resourceSales["cow"]);
            var cowSale = resourceSales["cow"];
            Assert.Single(cowSale);
            Assert.Single(cowSale.Where(s => s.Type == "coin" && s.Amount == 10));
            Assert.NotNull(resourceSales["pig"]);
            var pigSale = resourceSales["pig"];
            Assert.Single(pigSale);
            Assert.Single(pigSale.Where(s => s.Type == "coin" && s.Amount == 20));
            Assert.NotNull(resourceSales["chicken"]);
            var chickenSale = resourceSales["chicken"];
            Assert.Single(chickenSale);
            Assert.Single(chickenSale.Where(s => s.Type == "coin" && s.Amount == 10));

            var resourceOffers = Market.ResourceOffers;
            Assert.Equal(8, resourceOffers.Count);
            Assert.NotNull(resourceOffers["wood"]);
            var woodOffer = resourceOffers["wood"];
            Assert.Single(woodOffer);
            Assert.Single(woodOffer.Where(o => o.Type == "coin" && o.Amount == 1));
            Assert.NotNull(resourceOffers["rock"]);
            var rockOffer = resourceOffers["rock"];
            Assert.Single(rockOffer);
            Assert.Single(rockOffer.Where(o => o.Type == "coin" && o.Amount == 1));
            Assert.NotNull(resourceOffers["ore"]);
            var oreOffer = resourceOffers["ore"];
            Assert.Single(oreOffer);
            Assert.Single(oreOffer.Where(o => o.Type == "coin" && o.Amount == 2));
            Assert.NotNull(resourceOffers["ironore"]);
            var ironOreOffer = resourceOffers["ironore"];
            Assert.Single(ironOreOffer);
            Assert.Single(ironOreOffer.Where(o => o.Type == "coin" && o.Amount == 5));
            Assert.NotNull(resourceOffers["goldore"]);
            var goldOreOffer = resourceOffers["goldore"];
            Assert.Single(goldOreOffer);
            Assert.Single(goldOreOffer.Where(o => o.Type == "coin" && o.Amount == 25));
            Assert.NotNull(resourceOffers["meat"]);
            var meatOffer = resourceOffers["meat"];
            Assert.Single(meatOffer);
            Assert.Single(meatOffer.Where(o => o.Type == "coin" && o.Amount == 10));
            Assert.NotNull(resourceOffers["milk"]);
            var milkOffer = resourceOffers["milk"];
            Assert.Single(milkOffer);
            Assert.Single(milkOffer.Where(o => o.Type == "coin" && o.Amount == 13));
            Assert.NotNull(resourceOffers["egg"]);
            var eggOffer = resourceOffers["egg"];
            Assert.Single(eggOffer);
            Assert.Single(eggOffer.Where(o => o.Type == "coin" && o.Amount == 15));
        }
        [Fact]
        public void MarketSellsGatherer()
        {
            var unitSales = Market.UnitSales;
            Assert.NotNull(unitSales[typeof(Gatherer)]);
            var gathererSale = unitSales[typeof(Gatherer)];
            Assert.Single(gathererSale);
            Assert.Single(gathererSale.Where(s => s.Type == "coin" && s.Amount == 50));
            var unit = Market.SellUnit(typeof(Gatherer));
            Assert.True(unit is Gatherer);
            var gatherer = unit as Gatherer;

            var unitSales2 = Market.UnitSales;
            Assert.NotNull(unitSales2[typeof(Gatherer)]);
            var gathererSale2 = unitSales2[typeof(Gatherer)];
            Assert.Single(gathererSale2);
            Assert.Single(gathererSale2.Where(s => s.Type == "coin" && s.Amount == 51));
        }
        [Fact]
        public void MarketSellsBuilder()
        {
            var unitSales = Market.UnitSales;
            Assert.NotNull(unitSales[typeof(Builder)]);
            var builderSale = unitSales[typeof(Builder)];
            Assert.Single(builderSale);
            Assert.Single(builderSale.Where(s => s.Type == "coin" && s.Amount == 100));
            var unit = Market.SellUnit(typeof(Builder));
            Assert.True(unit is Builder);
            var builder = unit as Builder;

            var unitSales2 = Market.UnitSales;
            Assert.NotNull(unitSales2[typeof(Builder)]);
            var builderSale2 = unitSales2[typeof(Builder)];
            Assert.Single(builderSale2);
            Assert.Single(builderSale2.Where(s => s.Type == "coin" && s.Amount == 120)); //100*1,2
        }
        [Fact]
        public void MarketSellsFarmer()
        {
            var unitSales = Market.UnitSales;
            Assert.NotNull(unitSales[typeof(Farmer)]);
            var farmerSale = unitSales[typeof(Farmer)];
            Assert.Single(farmerSale);
            Assert.Single(farmerSale.Where(s => s.Type == "coin" && s.Amount == 50));
            var unit = Market.SellUnit(typeof(Farmer));
            Assert.True(unit is Farmer);
            var farmer = unit as Farmer;

            var unitSales2 = Market.UnitSales;
            Assert.NotNull(unitSales2[typeof(Farmer)]);
            var farmerSale2 = unitSales2[typeof(Farmer)];
            Assert.Single(farmerSale2);
            Assert.Single(farmerSale2.Where(s => s.Type == "coin" && s.Amount == 51));
        }
        [Fact]
        public void MarketSellsResearcher()
        {
            var unitSales = Market.UnitSales;
            Assert.NotNull(unitSales[typeof(Researcher)]);
            var researcherSale = unitSales[typeof(Researcher)];
            Assert.Single(researcherSale);
            Assert.Single(researcherSale.Where(s => s.Type == "coin" && s.Amount == 100));
            var unit = Market.SellUnit(typeof(Researcher));
            Assert.True(unit is Researcher);
            var researcher = unit as Researcher;

            var unitSales2 = Market.UnitSales;
            Assert.NotNull(unitSales2[typeof(Researcher)]);
            var researcherSale2 = unitSales2[typeof(Researcher)];
            Assert.Single(researcherSale2);
            Assert.Single(researcherSale2.Where(s => s.Type == "coin" && s.Amount == 150));
        }
        [Fact]
        public void MarketSellsForestCamp()
        {
            var buildingSales = Market.BuildingSales;
            Assert.NotNull(buildingSales[typeof(ForestCamp)]);
            var forestCampSale = buildingSales[typeof(ForestCamp)];
            Assert.Equal(2, forestCampSale.Count);
            Assert.Single(forestCampSale.Where(s => s.Type == "wood" && s.Amount == 10));
            Assert.Single(forestCampSale.Where(s => s.Type == "rock" && s.Amount == 5));
            var building = Market.SellBuilding(typeof(ForestCamp));
            Assert.True(building is ForestCamp);
            var forestCamp = building as ForestCamp;

            var buildingSales2 = Market.BuildingSales;
            Assert.NotNull(buildingSales2[typeof(ForestCamp)]);
            var forestCampSale2 = buildingSales2[typeof(ForestCamp)];
            Assert.Equal(2, forestCampSale2.Count);
            Assert.Single(forestCampSale2.Where(s => s.Type == "wood" && s.Amount == 15)); //10*1,5
            Assert.Single(forestCampSale2.Where(s => s.Type == "rock" && s.Amount == 8)); //5*1,5=7,5 -> 8
        }

        [Fact]
        public void MarketSellsMiningVillage()
        {
            var buildingSales = Market.BuildingSales;
            Assert.NotNull(buildingSales[typeof(MiningVillage)]);
            var miningVillageSale = buildingSales[typeof(MiningVillage)];
            Assert.Equal(2, miningVillageSale.Count);
            Assert.Single(miningVillageSale.Where(s => s.Type == "wood" && s.Amount == 10));
            Assert.Single(miningVillageSale.Where(s => s.Type == "rock" && s.Amount == 5));
            var building = Market.SellBuilding(typeof(MiningVillage));
            Assert.True(building is MiningVillage);
            var miningVillage = building as MiningVillage;

            var buildingSales2 = Market.BuildingSales;
            Assert.NotNull(buildingSales2[typeof(MiningVillage)]);
            var miningVillageSale2 = buildingSales2[typeof(MiningVillage)];
            Assert.Equal(2, miningVillageSale2.Count);
            Assert.Single(miningVillageSale2.Where(s => s.Type == "wood" && s.Amount == 13)); //10*1,3
            Assert.Single(miningVillageSale2.Where(s => s.Type == "rock" && s.Amount == 7)); //5*1,3=6,5 -> 7
        }
        [Fact]
        public void MarketSellsFarm()
        {
            var buildingSales = Market.BuildingSales;
            Assert.NotNull(buildingSales[typeof(Farm)]);
            var farmSale = buildingSales[typeof(Farm)];
            Assert.Equal(3, farmSale.Count);
            Assert.Single(farmSale.Where(s => s.Type == "wood" && s.Amount == 50));
            Assert.Single(farmSale.Where(s => s.Type == "rock" && s.Amount == 10));
            Assert.Single(farmSale.Where(s => s.Type == "ironore" && s.Amount == 10));
            var building = Market.SellBuilding(typeof(Farm));
            Assert.True(building is Farm);
            var farm = building as Farm;

            var buildingSales2 = Market.BuildingSales;
            Assert.NotNull(buildingSales2[typeof(Farm)]);
            var farmSale2 = buildingSales2[typeof(Farm)];
            Assert.Equal(3, farmSale2.Count);
            Assert.Single(farmSale2.Where(s => s.Type == "wood" && s.Amount == 100)); //50*2
            Assert.Single(farmSale2.Where(s => s.Type == "rock" && s.Amount == 20)); //10*2
            Assert.Single(farmSale2.Where(s => s.Type == "ironore" && s.Amount == 20)); //10*2
        }
        [Fact]
        public void MarketSellsLab()
        {
            var buildingSales = Market.BuildingSales;
            Assert.NotNull(buildingSales[typeof(Lab)]);
            var labSale = buildingSales[typeof(Lab)];
            Assert.Equal(3, labSale.Count);
            Assert.Single(labSale.Where(s => s.Type == "wood" && s.Amount == 30));
            Assert.Single(labSale.Where(s => s.Type == "rock" && s.Amount == 20));
            Assert.Single(labSale.Where(s => s.Type == "ironore" && s.Amount == 10));
            var building = Market.SellBuilding(typeof(Lab));
            Assert.True(building is Lab);
            var farm = building as Lab;

            var buildingSales2 = Market.BuildingSales;
            Assert.NotNull(buildingSales2[typeof(Lab)]);
            var labSale2 = buildingSales2[typeof(Lab)];
            Assert.Equal(3, labSale2.Count);
            Assert.Single(labSale2.Where(s => s.Type == "wood" && s.Amount == 39)); //30*1,3
            Assert.Single(labSale2.Where(s => s.Type == "rock" && s.Amount == 26)); //20*1,3
            Assert.Single(labSale2.Where(s => s.Type == "ironore" && s.Amount == 13)); //10*1,3
        }
        [Fact]
        public void MarketSellsSatisfactionBoost()
        {
            var satisfactionSale = Market.SatisfactionSale;
            Assert.Equal(2, satisfactionSale);
            Market.SellSatisfactionBoost();
            satisfactionSale = Market.SatisfactionSale;
            Assert.Equal(4, satisfactionSale);
            Market.SellSatisfactionBoost();
            satisfactionSale = Market.SatisfactionSale;
            Assert.Equal(8, satisfactionSale);
            Market.SellSatisfactionBoost();
            satisfactionSale = Market.SatisfactionSale;
            Assert.Equal(16, satisfactionSale);
            Market.SellSatisfactionBoost();
            satisfactionSale = Market.SatisfactionSale;
            Assert.Equal(32, satisfactionSale);
        }
        [Fact]
        public void MarketSellsResources()
        {
            var resourceSales = Market.ResourceSales;
            Assert.Equal(3, resourceSales.Count);
            Assert.NotNull(resourceSales["cow"]);
            var cowSale = resourceSales["cow"];
            Assert.Single(cowSale);
            Assert.Single(cowSale.Where(s => s.Type == "coin" && s.Amount == 10));
            var res = Market.SellResource("cow");
            Assert.Equal("cow", res.Type);
            Assert.Equal(1, res.Amount);

            var cowSale2 = resourceSales["cow"];
            Assert.Single(cowSale2);
            Assert.Single(cowSale2.Where(s => s.Type == "coin" && s.Amount == 10));

            Assert.NotNull(resourceSales["pig"]);
            var pigSale = resourceSales["pig"];
            Assert.Single(pigSale);
            Assert.Single(pigSale.Where(s => s.Type == "coin" && s.Amount == 20));
            res = Market.SellResource("pig");
            Assert.Equal("pig", res.Type);
            Assert.Equal(1, res.Amount);
            var pigSale2 = resourceSales["pig"];
            Assert.Single(pigSale2);
            Assert.Single(pigSale2.Where(s => s.Type == "coin" && s.Amount == 20));

            Assert.NotNull(resourceSales["chicken"]);
            var chickenSake = resourceSales["chicken"];
            Assert.Single(chickenSake);
            Assert.Single(chickenSake.Where(s => s.Type == "coin" && s.Amount == 10));
            res = Market.SellResource("chicken");
            Assert.Equal("chicken", res.Type);
            Assert.Equal(1, res.Amount);
            var chickenSale2 = resourceSales["chicken"];
            Assert.Single(chickenSale2);
            Assert.Single(chickenSale2.Where(s => s.Type == "coin" && s.Amount == 10));
        }
        [Fact]
        public void MarketBuysResources()
        {
            var resourceOffers = Market.ResourceOffers;
            Assert.NotNull(resourceOffers);
            Assert.Contains(resourceOffers, r => r.Key == "wood");
            Assert.Single(resourceOffers["wood"]);
            Assert.Single(resourceOffers["wood"].Where(r => r.Type == "coin"));
            Assert.Single(resourceOffers["wood"].Where(r => r.Type == "coin" && r.Amount == 1));

            var payment = Market.BuyResource("wood");
            Assert.NotNull(payment);
            Assert.NotEmpty(payment);
            Assert.Single(payment);
            Assert.Single(payment.Where(p => p.Type == "coin"));
            Assert.Single(payment.Where(p => p.Type == "coin" && p.Amount == 1));
        }
    }
}
