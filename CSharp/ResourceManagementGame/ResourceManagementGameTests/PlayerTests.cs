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
    public class PlayerTests
    {
        public PlayerTests()
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
            Player.GetInstance().ResetPlayer();
        }

        [Fact]
        public void InitializePlayerTest()
        {
            Player p1 = Player.GetInstance();
            Assert.Equal(4, p1.AllUnits.Count);
            Assert.Equal(4, p1.UnassignedUnits.Count);
            Assert.Equal(3, p1.NonfunctionalBuildings.Count);
            Assert.Empty(p1.FunctionalBuildings);
            Assert.Equal(0, p1.RoundCounter);
            Assert.Equal(Satisfaction.Happy, p1.Satisfaction);
            Assert.Equal(100, p1.SatisfactionPercentage);
            Assert.Empty(p1.Resources);

            Assert.Equal(2, p1.UnassignedUnits.Where(u => u is Gatherer).Count());
            Assert.Single(p1.UnassignedUnits.Where(u => u is Researcher));
            Assert.Single(p1.UnassignedUnits.Where(u => u is Builder));

            Assert.Single(p1.NonfunctionalBuildings.Where(b => b is ForestCamp));
            Assert.Single(p1.NonfunctionalBuildings.Where(b => b is MiningVillage));
            Assert.Single(p1.NonfunctionalBuildings.Where(b => b is Lab));

            var gatherer1 = p1.UnassignedUnits.Where(u => u is Gatherer).First();
            var gatherer2 = p1.UnassignedUnits.Where(u => u is Gatherer).ToList()[1];
            Assert.NotEqual(gatherer1, gatherer2);

            Assert.True(p1.AllUnits.All(u => u.Satisfaction == Satisfaction.Happy));
        }
        [Fact]
        public void PlayerDoRound()
        {
            Player p1 = Player.GetInstance();
            Assert.Equal(4, p1.AllUnits.Count);
            Assert.Equal(4, p1.UnassignedUnits.Count);
            Assert.Equal(3, p1.NonfunctionalBuildings.Count);
            Assert.Empty(p1.FunctionalBuildings);
            Assert.Equal(0, p1.RoundCounter);
            Assert.Equal(Satisfaction.Happy, p1.Satisfaction);
            Assert.Equal(100, p1.SatisfactionPercentage);
            Assert.Empty(p1.Resources);

            Assert.Equal(2, p1.UnassignedUnits.Where(u => u is Gatherer).Count());
            Assert.Single(p1.UnassignedUnits.Where(u => u is Researcher));
            Assert.Single(p1.UnassignedUnits.Where(u => u is Builder));

            var forestCamp = p1.NonfunctionalBuildings.Where(b => b is ForestCamp).First();
            Assert.NotNull(forestCamp);
            Assert.Equal(0, forestCamp.BuildProgress);
            Assert.False(forestCamp.AbleToFunction);
            p1.AssignConstructionUnitToBuilding(forestCamp);
            Assert.DoesNotContain(p1.UnassignedUnits, u => u is Builder);
            p1.StartRound();
            Assert.Equal(2, forestCamp.BuildProgress); //init happy state miatt 1*2
            Assert.False(forestCamp.AbleToFunction);

            Assert.Equal(100, p1.SatisfactionPercentage);
        }
        [Fact]
        public void SimulatePlayerGameTest()
        {
            Player p1 = Player.GetInstance();
            Assert.Equal(4, p1.AllUnits.Count);
            Assert.Equal(4, p1.UnassignedUnits.Count);
            Assert.Equal(3, p1.NonfunctionalBuildings.Count);
            Assert.Empty(p1.FunctionalBuildings);
            Assert.Equal(0, p1.RoundCounter);
            Assert.Equal(Satisfaction.Happy, p1.Satisfaction);
            Assert.Equal(100, p1.SatisfactionPercentage);
            Assert.Empty(p1.Resources);

            Assert.Equal(2, p1.UnassignedUnits.Where(u => u is Gatherer).Count());
            Assert.Single(p1.UnassignedUnits.Where(u => u is Researcher));
            Assert.Single(p1.UnassignedUnits.Where(u => u is Builder));

            var forestCamp = p1.NonfunctionalBuildings.Where(b => b is ForestCamp).First();
            Assert.NotNull(forestCamp);
            Assert.Equal(0, forestCamp.BuildProgress);
            Assert.False(forestCamp.AbleToFunction);
            p1.AssignConstructionUnitToBuilding(forestCamp);
            Assert.DoesNotContain(p1.UnassignedUnits, u => u is Builder);
            p1.StartRound();
            Assert.Equal(2, forestCamp.BuildProgress); //init happy state miatt 1*2
            Assert.False(forestCamp.AbleToFunction);

            Assert.Equal(100, p1.SatisfactionPercentage);
        }
    }
}
