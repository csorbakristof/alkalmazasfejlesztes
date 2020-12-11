using ResourceManagementGameCore;
using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Factories;
using ResourceManagementGameCore.Factories.UnitFactories;
using ResourceManagementGameCore.Units;
using System;
using System.Linq;
using Xunit;

namespace ResourceManagementGameTests
{
    [Collection("GameTests")]
    public class UnitFactoryTests
    {
        public UnitFactoryTests()
        {
            UnitFactory.CleanFactories();
            ConfigurationManager.SetJson(/*Utility.RESOURCE_JSON,*/ Utility.CONFIGFILE_JSON);
       }
        [Fact]
        public void CreateGathererTest()
        {
            Assert.Throws<Exception>(() => UnitFactory.CreateUnit(typeof(Gatherer)));
            GathererUnitRegister visitor = new GathererUnitRegister();
            UnitFactory.AddFactory(visitor);
            var unit = UnitFactory.CreateUnit(typeof(Gatherer));
            Assert.IsType<Gatherer>(unit);
            var gatherer = unit as Gatherer;
            Assert.Equal(1, gatherer.FoodNeeded);
            Assert.Equal(Satisfaction.Ok, gatherer.Satisfaction);
            Assert.Equal(2, gatherer.Products.Count);
            Assert.Single(gatherer.Products.Where(p => p.Type == "wood" && p.Amount == 1));
            Assert.Single(gatherer.Products.Where(p => p.Type == "rock" && p.Amount == 1));           
        }

        [Fact]
        public void CreateBuilderTest()
        {
            Assert.Throws<Exception>(() => UnitFactory.CreateUnit(typeof(Builder)));
            BuilderUnitRegister visitor = new BuilderUnitRegister();
            UnitFactory.AddFactory(visitor);
            var unit = UnitFactory.CreateUnit(typeof(Builder));
            Assert.IsType<Builder>(unit);
            var builder = unit as Builder;
            Assert.Equal(1, builder.FoodNeeded);
            Assert.Equal(Satisfaction.Ok, builder.Satisfaction);
            Assert.Equal(1, builder.ProgressAmount);          
        }

        [Fact]
        public void CreateFarmerTest()
        {
            Assert.Throws<Exception>(() => UnitFactory.CreateUnit(typeof(Farmer)));
            FarmerUnitRegister visitor = new FarmerUnitRegister();
            UnitFactory.AddFactory(visitor);
            var unit = UnitFactory.CreateUnit(typeof(Farmer));
            Assert.IsType<Farmer>(unit);
            var farmer = unit as Farmer;
            Assert.Equal(1, farmer.FoodNeeded);
            Assert.Equal(Satisfaction.Ok, farmer.Satisfaction);
            Assert.Equal(5, farmer.MaxInput);
            Assert.Equal(3, farmer.InputOutputMapping.Count);
            Assert.Equal(2, farmer.InputOutputMapping["cow"].Count);
            Assert.Single(farmer.InputOutputMapping["cow"].Where(r => r.Type == "meat" && r.Amount == 2));
            Assert.Single(farmer.InputOutputMapping["cow"].Where(r => r.Type == "milk" && r.Amount == 2));
            Assert.Single(farmer.InputOutputMapping["pig"]);
            Assert.Single(farmer.InputOutputMapping["pig"].Where(r => r.Type == "meat" && r.Amount == 4));
            Assert.Equal(2, farmer.InputOutputMapping["chicken"].Count);
            Assert.Single(farmer.InputOutputMapping["chicken"].Where(r => r.Type == "meat" && r.Amount == 2));
            Assert.Single(farmer.InputOutputMapping["chicken"].Where(r => r.Type == "egg" && r.Amount == 2));
        }

        [Fact]
        public void CreateResearcherTest()
        {
            Assert.Throws<Exception>(() => UnitFactory.CreateUnit(typeof(Researcher)));
            ResearcherUnitRegister visitor = new ResearcherUnitRegister();
            UnitFactory.AddFactory(visitor);
            var unit = UnitFactory.CreateUnit(typeof(Researcher));
            Assert.IsType<Researcher>(unit);
            var researcher = unit as Researcher;
            Assert.Equal(1, researcher.FoodNeeded);
            Assert.Equal(Satisfaction.Ok, researcher.Satisfaction);
            Assert.Equal(int.MaxValue, researcher.MaxInput);
            Assert.Empty(researcher.InputOutputMapping);
            Assert.Single(researcher.Products);
            Assert.Single(researcher.Products.Where(r => r.Type == "researchpoint" && r.Amount == 1));
        }
    }
}
