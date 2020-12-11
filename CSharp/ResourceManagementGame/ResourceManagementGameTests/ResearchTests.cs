using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Factories;
using ResourceManagementGameCore.Factories.UnitFactories;
using ResourceManagementGameCore.Research;
using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using ResourceManagementGameCore.Units.GathererState;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ResourceManagementGameTests
{
    [Collection("GameTests")]
    public class ResearchTests
    {
        public ResearchTests()
        {

            ConfigurationManager.SetJson(/*Utility.RESOURCE_JSON,*/ Utility.CONFIGFILE_JSON);
            ConfigurationManager.GetInstance();
            UnitFactory.CleanFactories();
            GathererUnitRegister gathererVisitor = new GathererUnitRegister();
            UnitFactory.AddFactory(gathererVisitor);
            BuilderUnitRegister builderVisitor = new BuilderUnitRegister();
            UnitFactory.AddFactory(builderVisitor);
            FarmerUnitRegister farmerVisitor = new FarmerUnitRegister();
            UnitFactory.AddFactory(farmerVisitor);
            ResearcherUnitRegister researcherVisitor = new ResearcherUnitRegister();
            UnitFactory.AddFactory(researcherVisitor);
        }
        [Fact]
        public void CreateResearchTree()
        {
            var root = new CompositeResearchItem(ResearchType.ProducerResourceUnlock,
                typeof(Gatherer),
                new ProducerProductParameters("ore"));
            Assert.False(root.IsApplied);
            Assert.Equal(typeof(Gatherer), root.UnlockOn);
            Assert.NotNull(root.GetChildUnlocks());
            Assert.Empty(root.GetChildUnlocks());

            var child1_1 = new LeafResearchItem(ResearchType.ProducerAmountBoost,
                typeof(Gatherer),
                new ProducerAmountParameters(new ResourceAmount("ore", 2)));
            Assert.False(child1_1.IsApplied);
            Assert.Equal(typeof(Gatherer), child1_1.UnlockOn);

            root.AddChildUnlock(child1_1);
            Assert.NotNull(root.GetChildUnlocks());
            Assert.Single(root.GetChildUnlocks());
            Assert.Equal(child1_1, root.GetChildUnlocks().Single());

            Assert.True(root.Contains(child1_1));

            var child1_2 = new CompositeResearchItem(ResearchType.ConverterInputResourceUnlock,
                typeof(Researcher),
                new ConverterInputResourceParameters("ore"));
            Assert.False(child1_2.IsApplied);
            Assert.Equal(typeof(Researcher), child1_2.UnlockOn);
            Assert.NotNull(child1_2.GetChildUnlocks());
            Assert.Empty(child1_2.GetChildUnlocks());

            root.AddChildUnlock(child1_2);
            Assert.NotNull(root.GetChildUnlocks());
            Assert.Equal(2, root.GetChildUnlocks().Count);
            Assert.Equal(child1_1, root.GetChildUnlocks()[0]);
            Assert.Equal(child1_2, root.GetChildUnlocks()[1]);

            Assert.True(root.Contains(child1_1));
            Assert.True(root.Contains(child1_2));

            var child2_1 = new LeafResearchItem(ResearchType.ConverterOutputUnlock,
                typeof(Researcher),
                new ConverterOutputForInputResourceParameter("ore", new List<ResourceAmount>() { new ResourceAmount("ironore", 1) }));
            Assert.False(child2_1.IsApplied);
            Assert.Equal(typeof(Researcher), child2_1.UnlockOn);

            Assert.False(root.Contains(child2_1));
            child1_2.AddChildUnlock(child2_1);
            Assert.NotNull(child1_2.GetChildUnlocks());
            Assert.Single(child1_2.GetChildUnlocks());
            Assert.Equal(child2_1, child1_2.GetChildUnlocks().Single());

            Assert.True(child1_2.Contains(child2_1));
            Assert.True(root.Contains(child2_1));

        }
        [Fact]
        public void UnlockResearcherTest()
        {
            var root = new CompositeResearchItem(ResearchType.ProducerResourceUnlock,
               typeof(Gatherer),
               new ProducerProductParameters("ore"));
            
            var child1_1 = new LeafResearchItem(ResearchType.ProducerAmountBoost,
                typeof(Gatherer),
                new ProducerAmountParameters(new ResourceAmount("ore", 2)));


            root.AddChildUnlock(child1_1);

            var child1_2 = new CompositeResearchItem(ResearchType.ConverterInputResourceUnlock,
                typeof(Researcher),
                new ConverterInputResourceParameters("ore"));

            root.AddChildUnlock(child1_2);

            Assert.True(root.Contains(child1_1));
            Assert.True(root.Contains(child1_2));

            var child2_1 = new LeafResearchItem(ResearchType.ConverterOutputUnlock,
                typeof(Researcher),
                new ConverterOutputForInputResourceParameter("ore", new List<ResourceAmount>() { new ResourceAmount("ironore", 1) }));
            
            child1_2.AddChildUnlock(child2_1);
            
            Assert.True(child1_2.Contains(child2_1));
            Assert.True(root.Contains(child2_1));

            var gatherer =(Gatherer)UnitFactory.CreateUnit(typeof(Gatherer));
            var researcher = (Researcher)UnitFactory.CreateUnit(typeof(Researcher));

            gatherer.SwitchState(new MinerState());

            var resources = gatherer.Produce();
            Assert.Single(resources);
            Assert.Single(resources.Where(r => r.Type == "rock"));
            Assert.Single(resources.Where(r => r.Type == "rock" && r.Amount == 1));

            Assert.Throws<Exception>(() => root.Unlock(researcher));
            root.Unlock(gatherer);
            Assert.True(root.IsApplied);
            resources = gatherer.Produce();
            Assert.Equal(2,resources.Count);
            Assert.Single(resources.Where(r => r.Type == "rock"));
            Assert.Single(resources.Where(r => r.Type == "rock" && r.Amount == 1));
            Assert.Single(resources.Where(r => r.Type == "ore"));
            Assert.Single(resources.Where(r => r.Type == "ore" && r.Amount == 1));

            Assert.Throws<Exception>(() => child1_1.Unlock(researcher));
            child1_1.Unlock(gatherer);
            Assert.True(child1_1.IsApplied);
            resources = gatherer.Produce();
            Assert.Equal(2, resources.Count);
            Assert.Single(resources.Where(r => r.Type == "rock"));
            Assert.Single(resources.Where(r => r.Type == "rock" && r.Amount == 1));
            Assert.Single(resources.Where(r => r.Type == "ore"));
            Assert.Single(resources.Where(r => r.Type == "ore" && r.Amount == 3));

            child1_1.Unlock(gatherer); //ugyanaz még 1x -> nem lesz hatása
            Assert.True(child1_1.IsApplied);
            resources = gatherer.Produce();
            Assert.Equal(2, resources.Count);
            Assert.Single(resources.Where(r => r.Type == "rock"));
            Assert.Single(resources.Where(r => r.Type == "rock" && r.Amount == 1));
            Assert.Single(resources.Where(r => r.Type == "ore"));
            Assert.Single(resources.Where(r => r.Type == "ore" && r.Amount == 3));

            resources = researcher.Convert();
            Assert.NotNull(resources);
            Assert.Empty(resources);
            Assert.Throws<Exception>(() => child1_2.Unlock(gatherer));
            child1_2.Unlock(researcher);
            Assert.True(child1_2.IsApplied);
            resources = researcher.Convert();
            Assert.NotNull(resources);
            Assert.Empty(resources);
            researcher.AddInput(new ResourceAmount("ore", 1));
            resources = researcher.Convert();
            Assert.NotNull(resources);
            Assert.Empty(resources);

            Assert.Throws<Exception>(() => child2_1.Unlock(gatherer));
            child2_1.Unlock(researcher);
            Assert.True(child2_1.IsApplied);
            researcher.AddInput(new ResourceAmount("ore", 1));
            resources = researcher.Convert();
            Assert.NotNull(resources);
            Assert.Single(resources);
            Assert.Single(resources.Where(r => r.Type == "ironore"));
            Assert.Single(resources.Where(r => r.Type == "ironore" && r.Amount == 1));

        }
        [Fact]
        public void CreateDefaultResearchTree()
        {
            var root = new RootResearchItem();
            Assert.True(root.IsApplied);

            //haldó bányász
            var child1_1 = new LeafResearchItem(ResearchType.ProducerResourceUnlock,
                typeof(Gatherer),
                new ProducerProductParameters("ore"));
            root.AddChildUnlock(child1_1);
            //researcher ore
            var child1_2 = new CompositeResearchItem(ResearchType.ConverterInputResourceUnlock,
                typeof(ResearchType),
                new ConverterInputResourceParameters("ore"));
            root.AddChildUnlock(child1_2);

            //researcher ore->ironore
            var child2_1 = new CompositeResearchItem(ResearchType.ConverterOutputUnlock,
                typeof(Researcher),
                new ConverterOutputForInputResourceParameter("ore", new List<ResourceAmount>() { new ResourceAmount("ironore", 1) }));

            child1_2.AddChildUnlock(child2_1);

            //researcher ore->goldore
            var child3_1 = new LeafResearchItem(ResearchType.ConverterOutputUnlock,
                typeof(Researcher),
                new ConverterOutputForInputResourceParameter("ore", new List<ResourceAmount>() { new ResourceAmount("goldore", 1) }));

            child2_1.AddChildUnlock(child3_1);

            //favágó
            var child1_3 = new CompositeResearchItem(ResearchType.ProducerAmountBoost,
                typeof(Gatherer),
                new ProducerAmountParameters(new ResourceAmount("wood", 1)));
            root.AddChildUnlock(child1_3);

            //bánáysz
            var child1_4 = new CompositeResearchItem(ResearchType.ProducerAmountBoost,
                typeof(Gatherer),
                new ProducerAmountParameters(new ResourceAmount("rock", 1)));
            root.AddChildUnlock(child1_4);

            var child1_5 = new CompositeResearchItem(ResearchType.ProducerAmountBoost,
                typeof(Gatherer),
                new ProducerAmountParameters(new ResourceAmount("ore", 1)));
            root.AddChildUnlock(child1_5);

            //kutató
            var child1_6 = new CompositeResearchItem(ResearchType.ProducerAmountBoost,
                typeof(Researcher),
                new ProducerAmountParameters(new ResourceAmount("researchpoint", 1)));
            root.AddChildUnlock(child1_6);


            Assert.True(root.IsApplied);
            Assert.NotNull(root.GetChildUnlocks());
            Assert.NotEmpty(root.GetChildUnlocks());

            Assert.Equal(6, root.GetChildUnlocks().Count);

            var c1 = root.GetChildUnlocks()[0];
            Assert.Equal(c1, child1_1);
            Assert.False(c1.IsApplied);
            var c2 = root.GetChildUnlocks()[1];
            Assert.Equal(c2, child1_2);
            Assert.False(c2.IsApplied);
            var c2_1 = ((CompositeResearchItem)c2).GetChildUnlocks().Single();
            Assert.Equal(c2_1, child2_1);
            Assert.False(c2_1.IsApplied);
            var c3_1 = ((CompositeResearchItem)c2_1).GetChildUnlocks().Single();
            Assert.Equal(c3_1, child3_1);
            Assert.False(c3_1.IsApplied);
            var c3 = root.GetChildUnlocks()[2];
            Assert.Equal(c3, child1_3);
            Assert.False(c3.IsApplied);
            var c4 = root.GetChildUnlocks()[3];
            Assert.Equal(c4, child1_4);
            Assert.False(c4.IsApplied);
            var c5 = root.GetChildUnlocks()[4];
            Assert.Equal(c5, child1_5);
            Assert.False(c5.IsApplied);
            var c6 = root.GetChildUnlocks()[5];
            Assert.Equal(c6, child1_6);
            Assert.False(c6.IsApplied);
        }

        [Fact]
        public void CreateDefaultResearchTreeWithBuilder()
        {
            ResearchBuilder builder = new ResearchBuilder();
            builder.AddLeaf(ResearchType.ProducerResourceUnlock,
                typeof(Gatherer),
                new ProducerProductParameters("ore"));
            builder.AddNode(ResearchType.ConverterInputResourceUnlock,
                typeof(ResearchType),
                new ConverterInputResourceParameters("ore"));
            builder.AddNode(ResearchType.ProducerAmountBoost,
                typeof(Gatherer),
                new ProducerAmountParameters(new ResourceAmount("wood", 1)));
            builder.AddNode(ResearchType.ProducerAmountBoost,
                typeof(Gatherer),
                new ProducerAmountParameters(new ResourceAmount("rock", 1)));
            builder.AddNode(ResearchType.ProducerAmountBoost,
                typeof(Gatherer),
                new ProducerAmountParameters(new ResourceAmount("ore", 1)));
            builder.AddNode(ResearchType.ProducerAmountBoost,
                typeof(Researcher),
                new ProducerAmountParameters(new ResourceAmount("researchpoint", 1)));
            builder.ChangeToLower(1);
            builder.AddNode(ResearchType.ConverterOutputUnlock,
                typeof(Researcher),
                new ConverterOutputForInputResourceParameter("ore", new List<ResourceAmount>() { new ResourceAmount("ironore", 1) }));
            builder.ChangeToLower(0);
            builder.AddLeaf(ResearchType.ConverterOutputUnlock,
                typeof(Researcher),
                new ConverterOutputForInputResourceParameter("ore", new List<ResourceAmount>() { new ResourceAmount("goldore", 1) }));
            var root = builder.Root;
            Assert.True(root.IsApplied);
            Assert.NotNull(root.GetChildUnlocks());
            Assert.NotEmpty(root.GetChildUnlocks());

            Assert.Equal(6, root.GetChildUnlocks().Count);

            var c1 = root.GetChildUnlocks()[0];
            Assert.False(c1.IsApplied);
            Assert.Equal(root, c1.Parent);
            Assert.IsType<LeafResearchItem>(c1);
            Assert.Equal(typeof(Gatherer),c1.UnlockOn);
        }
    }
}
