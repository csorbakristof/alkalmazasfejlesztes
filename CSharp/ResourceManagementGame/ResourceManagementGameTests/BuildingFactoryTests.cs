using ResourceManagementGameCore.Buildings;
using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Factories;
using ResourceManagementGameCore.Factories.BuildingFactories;
using ResourceManagementGameCore.Units;
using System;
using System.Linq;
using Xunit;

namespace ResourceManagementGameTests
{
    [Collection("GameTests")]
    public class BuildingFactoryTests
    {
        public BuildingFactoryTests()
        {
            BuildingFactory.CleanFactories();
            ConfigurationManager.SetJson(Utility.CONFIGFILE_JSON);
        }

        [Fact]
        public void CreateForestCampTest()
        {
            Assert.Throws<Exception>(() => BuildingFactory.CreateBuilding(typeof(ForestCamp)));
            ForestCampBuildingRegister visitor = new ForestCampBuildingRegister();
            BuildingFactory.AddFactory(visitor);
            var building = BuildingFactory.CreateBuilding(typeof(ForestCamp));
            Assert.IsType<ForestCamp>(building);
            var forestCamp = building as ForestCamp;
            Assert.False(forestCamp.AbleToFunction);
            Assert.Equal(5, forestCamp.BuildingTime);
            Assert.Equal(0, forestCamp.BuildProgress);
            Assert.Equal(0, forestCamp.CurrentCapacity);
            Assert.Single(forestCamp.UnitTypes);
            Assert.Equal(typeof(Gatherer), forestCamp.UnitTypes.Single());
            Assert.Equal(5, forestCamp.MaxCapacity);
        }

        [Fact]
        public void CreateMiningVillageTest()
        {
            Assert.Throws<Exception>(() => BuildingFactory.CreateBuilding(typeof(MiningVillage)));
            MiningVillageBuildingRegister visitor = new MiningVillageBuildingRegister();
            BuildingFactory.AddFactory(visitor);
            var building = BuildingFactory.CreateBuilding(typeof(MiningVillage));
            Assert.IsType<MiningVillage>(building);
            var miningVillage = building as MiningVillage;
            Assert.False(miningVillage.AbleToFunction);
            Assert.Equal(5, miningVillage.BuildingTime);
            Assert.Equal(0, miningVillage.BuildProgress);
            Assert.Equal(0, miningVillage.CurrentCapacity);
            Assert.Single(miningVillage.UnitTypes);
            Assert.Equal(typeof(Gatherer), miningVillage.UnitTypes.Single());
            Assert.Equal(5, miningVillage.MaxCapacity);
        }
        [Fact]
        public void CreateFarmTest()
        {
            Assert.Throws<Exception>(() => BuildingFactory.CreateBuilding(typeof(Farm)));
            FarmBuildingRegister visitor = new FarmBuildingRegister();
            BuildingFactory.AddFactory(visitor);
            var building = BuildingFactory.CreateBuilding(typeof(Farm));
            Assert.IsType<Farm>(building);
            var farm = building as Farm;
            Assert.False(farm.AbleToFunction);
            Assert.Equal(7, farm.BuildingTime);
            Assert.Equal(0, farm.BuildProgress);
            Assert.Equal(0, farm.CurrentCapacity);
            Assert.Single(farm.UnitTypes);
            Assert.Equal(typeof(Farmer), farm.UnitTypes.Single());
            Assert.Equal(1, farm.MaxCapacity);
        }
        [Fact]
        public void CreateLabTest()
        {
            Assert.Throws<Exception>(() => BuildingFactory.CreateBuilding(typeof(Lab)));
            LabBuildingRegister visitor = new LabBuildingRegister();
            BuildingFactory.AddFactory(visitor);
            var building = BuildingFactory.CreateBuilding(typeof(Lab));
            Assert.IsType<Lab>(building);
            var lab = building as Lab;
            Assert.False(lab.AbleToFunction);
            Assert.Equal(10, lab.BuildingTime);
            Assert.Equal(0, lab.BuildProgress);
            Assert.Equal(0, lab.CurrentCapacity);
            Assert.Single(lab.UnitTypes);
            Assert.Equal(typeof(Researcher), lab.UnitTypes.Single());
            Assert.Equal(5, lab.MaxCapacity);
        }
    }
}
