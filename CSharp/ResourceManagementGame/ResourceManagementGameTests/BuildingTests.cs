using ResourceManagementGameCore.Algos;
using ResourceManagementGameCore.Buildings;
using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Factories;
using ResourceManagementGameCore.Factories.BuildingFactories;
using ResourceManagementGameCore.Factories.UnitFactories;
using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ResourceManagementGameTests
{
    [Collection("GameTests")]
    public class BuildingTests
    {
        public BuildingTests()
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
        }
        [Fact]
        public void ForestCampBuildTest()
        {
            ForestCamp forestCamp = new ForestCamp(5, 1);
            Builder builder = (Builder)UnitFactory.CreateUnit(typeof(Builder));

            Assert.False(forestCamp.AbleToFunction);
            Assert.Equal(1, forestCamp.BuildingTime);
            Assert.Equal(0, forestCamp.BuildProgress);
            Assert.Equal(0, forestCamp.CurrentCapacity);
            Assert.Equal(5, forestCamp.MaxCapacity);
            Assert.Equal(0, forestCamp.CurrentConstructionUnitCount);
            Assert.Single(forestCamp.UnitTypes);
            Assert.Single(forestCamp.UnitTypes.Where(u => u == typeof(Gatherer)));

            forestCamp.DoBuildProcess();
            Assert.Equal(0, forestCamp.BuildProgress);

            forestCamp.AddConstructionUnit(builder);
            Assert.Equal(1, forestCamp.CurrentConstructionUnitCount);
            forestCamp.DoBuildProcess();
            Assert.Equal(1, forestCamp.BuildProgress);
            Assert.True(forestCamp.AbleToFunction);
        }
        [Fact]
        public void ForestCampBuildTest2()
        {
            ForestCamp forestCamp = new ForestCamp(5, 3);
            Builder builder = (Builder)UnitFactory.CreateUnit(typeof(Builder));

            Assert.False(forestCamp.AbleToFunction);
            Assert.Equal(3, forestCamp.BuildingTime);
            Assert.Equal(0, forestCamp.BuildProgress);
            Assert.Equal(0, forestCamp.CurrentCapacity);
            Assert.Equal(5, forestCamp.MaxCapacity);
            Assert.Equal(0, forestCamp.CurrentConstructionUnitCount);
            Assert.Single(forestCamp.UnitTypes);
            Assert.Single(forestCamp.UnitTypes.Where(u => u == typeof(Gatherer)));

            forestCamp.DoBuildProcess();
            Assert.Equal(0, forestCamp.BuildProgress);

            forestCamp.AddConstructionUnit(builder);
            Assert.Equal(1, forestCamp.CurrentConstructionUnitCount);
            forestCamp.DoBuildProcess();
            Assert.Equal(1, forestCamp.BuildProgress);
            Assert.False(forestCamp.AbleToFunction);

            forestCamp.DoBuildProcess();
            Assert.Equal(2, forestCamp.BuildProgress);
            Assert.False(forestCamp.AbleToFunction);

            forestCamp.DoBuildProcess();
            Assert.Equal(3, forestCamp.BuildProgress);
            Assert.True(forestCamp.AbleToFunction);
        }
        [Fact]
        public void ForestCampBuildTest3()
        {
            ForestCamp forestCamp = new ForestCamp(5, 3);
            Builder builder = (Builder)UnitFactory.CreateUnit(typeof(Builder));

            Assert.False(forestCamp.AbleToFunction);
            Assert.Equal(3, forestCamp.BuildingTime);
            Assert.Equal(0, forestCamp.BuildProgress);
            Assert.Equal(0, forestCamp.CurrentCapacity);
            Assert.Equal(5, forestCamp.MaxCapacity);
            Assert.Equal(0, forestCamp.CurrentConstructionUnitCount);
            Assert.Single(forestCamp.UnitTypes);
            Assert.Single(forestCamp.UnitTypes.Where(u => u == typeof(Gatherer)));

            forestCamp.DoBuildProcess();
            Assert.Equal(0, forestCamp.BuildProgress);

            forestCamp.AddConstructionUnit(builder);
            Assert.Equal(1, forestCamp.CurrentConstructionUnitCount);
            forestCamp.DoBuildProcess();
            Assert.Equal(1, forestCamp.BuildProgress);
            Assert.False(forestCamp.AbleToFunction);

            forestCamp.AddConstructionUnit(builder);
            Assert.Equal(1, forestCamp.CurrentConstructionUnitCount);
            forestCamp.DoBuildProcess();
            Assert.Equal(2, forestCamp.BuildProgress);
            Assert.False(forestCamp.AbleToFunction);

            forestCamp.AddConstructionUnit(builder);
            Assert.Equal(1, forestCamp.CurrentConstructionUnitCount);
            forestCamp.DoBuildProcess();
            Assert.Equal(3, forestCamp.BuildProgress);
            Assert.True(forestCamp.AbleToFunction);
        }
        [Fact]
        public void ForestCampBuildTest4()
        {
            ForestCamp forestCamp = new ForestCamp(5, 3);
            Builder builder1 = (Builder)UnitFactory.CreateUnit(typeof(Builder));
            Builder builder2 = (Builder)UnitFactory.CreateUnit(typeof(Builder));

            Assert.False(forestCamp.AbleToFunction);
            Assert.Equal(3, forestCamp.BuildingTime);
            Assert.Equal(0, forestCamp.BuildProgress);
            Assert.Equal(0, forestCamp.CurrentCapacity);
            Assert.Equal(5, forestCamp.MaxCapacity);
            Assert.Equal(0, forestCamp.CurrentConstructionUnitCount);
            Assert.Single(forestCamp.UnitTypes);
            Assert.Single(forestCamp.UnitTypes.Where(u => u == typeof(Gatherer)));

            Assert.Empty(forestCamp.DoWork());

            forestCamp.DoBuildProcess();
            Assert.Equal(0, forestCamp.BuildProgress);

            forestCamp.AddConstructionUnit(builder1);
            Assert.Equal(1, forestCamp.CurrentConstructionUnitCount);
            forestCamp.DoBuildProcess();
            Assert.Equal(1, forestCamp.BuildProgress);
            Assert.False(forestCamp.AbleToFunction);

            forestCamp.AddConstructionUnit(builder2);
            Assert.Equal(2, forestCamp.CurrentConstructionUnitCount);
            forestCamp.DoBuildProcess();
            Assert.Equal(3, forestCamp.BuildProgress);
            Assert.True(forestCamp.AbleToFunction);
        }
        [Fact]
        public void MiningVillageBuildTest()
        {
            MiningVillage miningVillage = new MiningVillage(5, 4);
            Builder builder = (Builder)UnitFactory.CreateUnit(typeof(Builder));

            Assert.False(miningVillage.AbleToFunction);
            Assert.Equal(4, miningVillage.BuildingTime);
            Assert.Equal(0, miningVillage.BuildProgress);
            Assert.Equal(0, miningVillage.CurrentCapacity);
            Assert.Equal(5, miningVillage.MaxCapacity);
            Assert.Equal(0, miningVillage.CurrentConstructionUnitCount);
            Assert.Single(miningVillage.UnitTypes);
            Assert.Single(miningVillage.UnitTypes.Where(u => u == typeof(Gatherer)));

            Assert.Empty(miningVillage.DoWork());

            miningVillage.DoBuildProcess();
            Assert.Equal(0, miningVillage.BuildProgress);

            miningVillage.AddConstructionUnit(builder);
            Assert.Equal(1, miningVillage.CurrentConstructionUnitCount);
            miningVillage.DoBuildProcess();
            Assert.Equal(1, miningVillage.BuildProgress);
            Assert.False(miningVillage.AbleToFunction);

            miningVillage.DoBuildProcess();
            Assert.Equal(2, miningVillage.BuildProgress);
            Assert.False(miningVillage.AbleToFunction);

            miningVillage.DoBuildProcess();
            Assert.Equal(3, miningVillage.BuildProgress);
            Assert.False(miningVillage.AbleToFunction);

            miningVillage.DoBuildProcess();
            Assert.Equal(4, miningVillage.BuildProgress);
            Assert.True(miningVillage.AbleToFunction);

            miningVillage.DoBuildProcess();
            Assert.Equal(4, miningVillage.BuildProgress);
            Assert.True(miningVillage.AbleToFunction);
        }
        [Fact]
        public void FarmBuildingTest()
        {
            Farm farm = new Farm(5, 2);
            Builder builder = (Builder)UnitFactory.CreateUnit(typeof(Builder));

            Assert.False(farm.AbleToFunction);
            Assert.Equal(2, farm.BuildingTime);
            Assert.Equal(0, farm.BuildProgress);
            Assert.Equal(0, farm.CurrentCapacity);
            Assert.Equal(5, farm.MaxCapacity);
            Assert.Equal(0, farm.CurrentConstructionUnitCount);
            Assert.Single(farm.UnitTypes);
            Assert.Single(farm.UnitTypes.Where(u => u == typeof(Farmer)));

            Assert.Empty(farm.DoWork());

            farm.DoBuildProcess();
            Assert.Equal(0, farm.BuildProgress);

            farm.AddConstructionUnit(builder);
            Assert.Equal(1, farm.CurrentConstructionUnitCount);
            farm.DoBuildProcess();
            Assert.Equal(1, farm.BuildProgress);
            Assert.False(farm.AbleToFunction);

            farm.DoBuildProcess();
            Assert.Equal(2, farm.BuildProgress);
            Assert.True(farm.AbleToFunction);
        }
        [Fact]
        public void LabBuildingTest()
        {
            Lab lab = new Lab(5, 2);
            Builder builder = (Builder)UnitFactory.CreateUnit(typeof(Builder));

            Assert.False(lab.AbleToFunction);
            Assert.Equal(2, lab.BuildingTime);
            Assert.Equal(0, lab.BuildProgress);
            Assert.Equal(0, lab.CurrentCapacity);
            Assert.Equal(5, lab.MaxCapacity);
            Assert.Equal(0, lab.CurrentConstructionUnitCount);
            Assert.Single(lab.UnitTypes);
            Assert.Single(lab.UnitTypes.Where(u => u == typeof(Researcher)));

            Assert.Empty(lab.DoWork());

            lab.DoBuildProcess();
            Assert.Equal(0, lab.BuildProgress);

            lab.AddConstructionUnit(builder);
            Assert.Equal(1, lab.CurrentConstructionUnitCount);
            lab.DoBuildProcess();
            Assert.Equal(1, lab.BuildProgress);
            Assert.False(lab.AbleToFunction);

            lab.DoBuildProcess();
            Assert.Equal(2, lab.BuildProgress);
            Assert.True(lab.AbleToFunction);
        }

        [Fact]
        public void ForestCampTest()
        {
            ForestCamp forestCamp = new ForestCamp(5, 1);
            Builder builder = (Builder)UnitFactory.CreateUnit(typeof(Builder));
            Gatherer gatherer = (Gatherer)UnitFactory.CreateUnit(typeof(Gatherer));

            Assert.False(forestCamp.AbleToFunction);
            Assert.Equal(1, forestCamp.BuildingTime);
            Assert.Equal(0, forestCamp.BuildProgress);
            Assert.Equal(0, forestCamp.CurrentCapacity);
            Assert.Equal(5, forestCamp.MaxCapacity);
            Assert.Equal(0, forestCamp.CurrentConstructionUnitCount);
            Assert.Single(forestCamp.UnitTypes);
            Assert.Single(forestCamp.UnitTypes.Where(u => u == typeof(Gatherer)));

            forestCamp.AddConstructionUnit(builder);
            Assert.Equal(1, forestCamp.CurrentConstructionUnitCount);
            var removed = forestCamp.RemoveLastConstructionUnit();
            Assert.Equal(builder, removed);
            Assert.Equal(0, forestCamp.CurrentConstructionUnitCount);

            forestCamp.AddConstructionUnit(builder);
            Assert.Equal(1, forestCamp.CurrentConstructionUnitCount);
            forestCamp.DoBuildProcess();
            Assert.Equal(1, forestCamp.BuildProgress);
            Assert.True(forestCamp.AbleToFunction);

            forestCamp.AssignUnit(gatherer);
            Assert.Equal(1, forestCamp.CurrentCapacity);
            var resources = forestCamp.DoWork();
            Assert.NotNull(resources);
            Assert.NotEmpty(resources);
            Assert.Single(resources);
            Assert.Single(resources.Where(r => r.Type == "wood"));
            Assert.Single(resources.Where(r => r.Type == "wood" && r.Amount == 1));
        }
        [Fact]
        public void ForestCampTest2()
        {
            ForestCamp forestCamp = new ForestCamp(5, 1);
            Builder builder = (Builder)UnitFactory.CreateUnit(typeof(Builder));
            Gatherer gatherer = (Gatherer)UnitFactory.CreateUnit(typeof(Gatherer));
            Gatherer gatherer2 = (Gatherer)UnitFactory.CreateUnit(typeof(Gatherer));
            Gatherer gatherer3 = (Gatherer)UnitFactory.CreateUnit(typeof(Gatherer));

            Assert.False(forestCamp.AbleToFunction);
            Assert.Equal(1, forestCamp.BuildingTime);
            Assert.Equal(0, forestCamp.BuildProgress);
            Assert.Equal(0, forestCamp.CurrentCapacity);
            Assert.Equal(5, forestCamp.MaxCapacity);
            Assert.Equal(0, forestCamp.CurrentConstructionUnitCount);
            Assert.Single(forestCamp.UnitTypes);
            Assert.Single(forestCamp.UnitTypes.Where(u => u == typeof(Gatherer)));

            forestCamp.AddConstructionUnit(builder);
            Assert.Equal(1, forestCamp.CurrentConstructionUnitCount);
            var removed = forestCamp.RemoveLastConstructionUnit();
            Assert.Equal(builder, removed);
            Assert.Equal(0, forestCamp.CurrentConstructionUnitCount);

            forestCamp.AddConstructionUnit(builder);
            Assert.Equal(1, forestCamp.CurrentConstructionUnitCount);
            forestCamp.DoBuildProcess();
            Assert.Equal(1, forestCamp.BuildProgress);
            Assert.True(forestCamp.AbleToFunction);

            forestCamp.AssignUnit(gatherer);
            Assert.Equal(1, forestCamp.CurrentCapacity);
            var resources = forestCamp.DoWork();
            Assert.NotNull(resources);
            Assert.NotEmpty(resources);
            Assert.Single(resources);
            Assert.Single(resources.Where(r => r.Type == "wood"));
            Assert.Single(resources.Where(r => r.Type == "wood" && r.Amount == 1));

            forestCamp.AssignUnit(gatherer2);
            Assert.Equal(2, forestCamp.CurrentCapacity);
            resources = forestCamp.DoWork();
            Assert.NotNull(resources);
            Assert.NotEmpty(resources);
            Assert.Single(resources);
            Assert.Single(resources.Where(r => r.Type == "wood"));
            Assert.Single(resources.Where(r => r.Type == "wood" && r.Amount == 2));

            forestCamp.AssignUnit(gatherer3);
            Assert.Equal(3, forestCamp.CurrentCapacity);
            resources = forestCamp.DoWork();
            Assert.NotNull(resources);
            Assert.NotEmpty(resources);
            Assert.Single(resources);
            Assert.Single(resources.Where(r => r.Type == "wood"));
            Assert.Single(resources.Where(r => r.Type == "wood" && r.Amount == 3));

            var unit = forestCamp.RemoveLastUnit();
            Assert.Equal(gatherer3, unit);
            Assert.Equal(2, forestCamp.CurrentCapacity);
            resources = forestCamp.DoWork();
            Assert.NotNull(resources);
            Assert.NotEmpty(resources);
            Assert.Single(resources);
            Assert.Single(resources.Where(r => r.Type == "wood"));
            Assert.Single(resources.Where(r => r.Type == "wood" && r.Amount == 2));
        }

        [Fact]
        public void MiningVillageTest()
        {
            MiningVillage miningVillage = new MiningVillage(5, 2);
            Builder builder = (Builder)UnitFactory.CreateUnit(typeof(Builder));
            Gatherer gatherer = (Gatherer)UnitFactory.CreateUnit(typeof(Gatherer));

            Assert.False(miningVillage.AbleToFunction);
            Assert.Equal(2, miningVillage.BuildingTime);
            Assert.Equal(0, miningVillage.BuildProgress);
            Assert.Equal(0, miningVillage.CurrentCapacity);
            Assert.Equal(5, miningVillage.MaxCapacity);
            Assert.Equal(0, miningVillage.CurrentConstructionUnitCount);
            Assert.Single(miningVillage.UnitTypes);
            Assert.Single(miningVillage.UnitTypes.Where(u => u == typeof(Gatherer)));

            miningVillage.DoBuildProcess();
            Assert.Equal(0, miningVillage.BuildProgress);

            miningVillage.AddConstructionUnit(builder);
            Assert.Equal(1, miningVillage.CurrentConstructionUnitCount);
            miningVillage.DoBuildProcess();
            Assert.Equal(1, miningVillage.BuildProgress);
            Assert.False(miningVillage.AbleToFunction);

            miningVillage.DoBuildProcess();
            Assert.Equal(2, miningVillage.BuildProgress);
            Assert.True(miningVillage.AbleToFunction);

            miningVillage.AssignUnit(gatherer);
            Assert.Equal(1, miningVillage.CurrentCapacity);
            var resources = miningVillage.DoWork();
            Assert.NotNull(resources);
            Assert.NotEmpty(resources);
            Assert.Single(resources);
            Assert.Single(resources.Where(r => r.Type == "rock"));
            Assert.Single(resources.Where(r => r.Type == "rock" && r.Amount == 1));
        }
        [Fact]
        public void FarmTest()
        {
            Farm farm = new Farm(1, 2);
            Builder builder = (Builder)UnitFactory.CreateUnit(typeof(Builder));
            Farmer farmer = (Farmer)UnitFactory.CreateUnit(typeof(Farmer));

            Assert.False(farm.AbleToFunction);
            Assert.Equal(2, farm.BuildingTime);
            Assert.Equal(0, farm.BuildProgress);
            Assert.Equal(0, farm.CurrentCapacity);
            Assert.Equal(1, farm.MaxCapacity);
            Assert.Equal(0, farm.CurrentConstructionUnitCount);
            Assert.Single(farm.UnitTypes);
            Assert.Single(farm.UnitTypes.Where(u => u == typeof(Farmer)));

            farm.DoBuildProcess();
            Assert.Equal(0, farm.BuildProgress);

            farm.AddConstructionUnit(builder);
            Assert.Equal(1, farm.CurrentConstructionUnitCount);
            farm.DoBuildProcess();
            Assert.Equal(1, farm.BuildProgress);
            Assert.False(farm.AbleToFunction);

            farm.DoBuildProcess();
            Assert.Equal(2, farm.BuildProgress);
            Assert.True(farm.AbleToFunction);

            farm.AssignUnit(farmer);
            Assert.Equal(1, farm.CurrentCapacity);
            var resources = farm.DoWork();
            Assert.NotNull(resources);
            Assert.Empty(resources);

            farm.AddInput(new ResourceAmount("cow", 3));
            resources = farm.DoWork();
            Assert.Equal(2,resources.Count);
            Assert.Single(resources.Where(r => r.Type == "meat"));
            Assert.Single(resources.Where(r => r.Type == "meat" && r.Amount == 6)); //3 cow -> 2*3
            Assert.Single(resources.Where(r => r.Type == "milk"));
            Assert.Single(resources.Where(r => r.Type == "milk" && r.Amount == 6));

            resources = farm.DoWork();
            Assert.NotNull(resources);
            Assert.Empty(resources);

            farm.AddInput(new ResourceAmount("cow", 2));
            farm.RemoveInput(new ResourceAmount("cow", 2));
            Assert.NotNull(resources);
            Assert.Empty(resources);

            farm.AddInput(new ResourceAmount("cow", 2));
            farm.RemoveInput(new ResourceAmount("cow", 1));
            resources = farm.DoWork();
            Assert.Equal(2, resources.Count);
            Assert.Single(resources.Where(r => r.Type == "meat"));
            Assert.Single(resources.Where(r => r.Type == "meat" && r.Amount == 2));
            Assert.Single(resources.Where(r => r.Type == "milk"));
            Assert.Single(resources.Where(r => r.Type == "milk" && r.Amount == 2));
        }
        [Fact]
        public void LabTest()
        {
            Lab lab = new Lab(5, 2);
            Builder builder = (Builder)UnitFactory.CreateUnit(typeof(Builder));
            Researcher researcher = (Researcher)UnitFactory.CreateUnit(typeof(Researcher));

            Assert.False(lab.AbleToFunction);
            Assert.Equal(2, lab.BuildingTime);
            Assert.Equal(0, lab.BuildProgress);
            Assert.Equal(0, lab.CurrentCapacity);
            Assert.Equal(5, lab.MaxCapacity);
            Assert.Equal(0, lab.CurrentConstructionUnitCount);
            Assert.Single(lab.UnitTypes);
            Assert.Single(lab.UnitTypes.Where(u => u == typeof(Researcher)));

            lab.DoBuildProcess();
            Assert.Equal(0, lab.BuildProgress);

            lab.AddConstructionUnit(builder);
            Assert.Equal(1, lab.CurrentConstructionUnitCount);
            lab.DoBuildProcess();
            Assert.Equal(1, lab.BuildProgress);
            Assert.False(lab.AbleToFunction);

            lab.DoBuildProcess();
            Assert.Equal(2, lab.BuildProgress);
            Assert.True(lab.AbleToFunction);

            var resources = lab.DoWork();
            Assert.NotNull(resources);
            Assert.Empty(resources);

            lab.AssignUnit(researcher);
            Assert.Equal(1, lab.CurrentCapacity);
            resources = lab.DoWork();
            Assert.NotNull(resources);
            Assert.Single(resources);
            Assert.Single(resources.Where(r => r.Type == "researchpoint"));
            Assert.Single(resources.Where(r => r.Type == "researchpoint" && r.Amount == 1));

            //unlock
            researcher.UpgradeConverterOutputResource("ore");
            researcher.UpgradeConverterInputMapping("ore", new List<ResourceAmount>() { new ResourceAmount("ironore", 1) });
            resources = lab.DoWork();
            Assert.NotNull(resources);
            Assert.Single(resources);
            Assert.Single(resources.Where(r => r.Type == "researchpoint"));
            Assert.Single(resources.Where(r => r.Type == "researchpoint" && r.Amount == 1));

            //add input
            lab.AddInput(new ResourceAmount("ore", 2));
            resources = lab.DoWork();
            Assert.NotNull(resources);
            Assert.Equal(2,resources.Count);
            Assert.Single(resources.Where(r => r.Type == "researchpoint"));
            Assert.Single(resources.Where(r => r.Type == "researchpoint" && r.Amount == 1));
            Assert.Single(resources.Where(r => r.Type == "ironore"));
            Assert.Single(resources.Where(r => r.Type == "ironore" && r.Amount == 2));

            //unlock x2
            researcher.UpgradeConverterInputMapping("ore", new List<ResourceAmount>() { new ResourceAmount("goldore", 1) });
            lab.AddInput(new ResourceAmount("ore", 1));
            resources = lab.DoWork();
            Assert.NotNull(resources);
            Assert.Equal(3, resources.Count);
            Assert.Single(resources.Where(r => r.Type == "researchpoint"));
            Assert.Single(resources.Where(r => r.Type == "researchpoint" && r.Amount == 1));
            Assert.Single(resources.Where(r => r.Type == "ironore"));
            Assert.Single(resources.Where(r => r.Type == "ironore" && r.Amount == 1));
            Assert.Single(resources.Where(r => r.Type == "goldore"));
            Assert.Single(resources.Where(r => r.Type == "goldore" && r.Amount == 1));

            lab.AddInput(new ResourceAmount("ore", 3));
            lab.RemoveInput(new ResourceAmount("ore", 2));
            resources = lab.DoWork();
            Assert.NotNull(resources);
            Assert.Equal(3, resources.Count);
            Assert.Single(resources.Where(r => r.Type == "researchpoint"));
            Assert.Single(resources.Where(r => r.Type == "researchpoint" && r.Amount == 1));
            Assert.Single(resources.Where(r => r.Type == "ironore"));
            Assert.Single(resources.Where(r => r.Type == "ironore" && r.Amount == 1));
            Assert.Single(resources.Where(r => r.Type == "goldore"));
            Assert.Single(resources.Where(r => r.Type == "goldore" && r.Amount == 1));
        }
    }
}
