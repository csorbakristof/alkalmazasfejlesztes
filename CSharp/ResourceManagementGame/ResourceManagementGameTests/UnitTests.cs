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
    public class UnitTests
    {
        public UnitTests()
        {
            ResourceType.AddResourceType("coin", false);
            ResourceType.AddResourceType("wood", false);
            ResourceType.AddResourceType("rock", false);
            ResourceType.AddResourceType("ore", false);
            ResourceType.AddResourceType("ironore", false);
            ResourceType.AddResourceType("goldore", false);
            ResourceType.AddResourceType("researchpoint", false);
            ResourceType.AddResourceType("milk", true);
            ResourceType.AddResourceType("meat", true);
            ResourceType.AddResourceType("egg", true);

            ResourceType.AddResourceType("cow", false);
            ResourceType.AddResourceType("pig", false);
            ResourceType.AddResourceType("chicken", false);
        }
        [Fact]
        public void GathererTest()
        {
            Gatherer gatherer = new Gatherer(1, new List<ResourceAmount>()
            {
                new ResourceAmount("wood", 1),
                new ResourceAmount("rock", 1),
                new ResourceAmount("ore",1)
            },
            new List<ResourceAmount>()
            {
                new ResourceAmount("wood", 1),
                new ResourceAmount("rock", 1),
            });

            Assert.Equal(1, gatherer.FoodNeeded);
            Assert.Equal(2, gatherer.Products.Count);
            Assert.Single(gatherer.Products.Where(p => p.Type == "wood" && p.Amount == 1));
            Assert.Single(gatherer.Products.Where(p => p.Type == "rock" && p.Amount == 1));

            
        }

        [Fact]
        public void GathererDefaultProduceTest()
        {
            Gatherer gatherer = new Gatherer(1, new List<ResourceAmount>()
            {
                new ResourceAmount("wood", 1),
                new ResourceAmount("rock", 1)
            },
            new List<ResourceAmount>()
            {
                new ResourceAmount("wood", 1),
                new ResourceAmount("rock", 1),
            });

            Assert.Empty(gatherer.Produce());

            gatherer.SwitchState(new WoodcutterState());

            var woods = gatherer.Produce();
            Assert.Single(woods);
            Assert.Single(woods.Where(p => p.Type == "wood"));
            Assert.Equal(1, woods.Where(p => p.Type == "wood").Single().Amount);

            gatherer.SwitchState(new MinerState());

            var rocks = gatherer.Produce();
            Assert.Single(rocks);
            Assert.Single(rocks.Where(p => p.Type == "rock"));
            Assert.Equal(1, rocks.Where(p => p.Type == "rock").Single().Amount);
        }
        [Fact]
        public void GathererDefaultUpgradesTest()
        {
            Gatherer gatherer = new Gatherer(1, new List<ResourceAmount>()
            {
                new ResourceAmount("wood", 1),
                new ResourceAmount("rock", 1),
                new ResourceAmount("ore",1)
            },
            new List<ResourceAmount>()
            {
                new ResourceAmount("wood", 1),
                new ResourceAmount("rock", 1),
            });

            Assert.Empty(gatherer.Produce());

            gatherer.SwitchState(new WoodcutterState());

            var woods = gatherer.Produce();
            Assert.Single(woods);
            Assert.Single(woods.Where(p => p.Type == "wood"));
            Assert.Equal(1, woods.Where(p => p.Type == "wood").Single().Amount);

            gatherer.SwitchState(new MinerState());

            var rocks = gatherer.Produce();
            Assert.Single(rocks);
            Assert.Single(rocks.Where(p => p.Type == "rock"));
            Assert.Equal(1, rocks.Where(p => p.Type == "rock").Single().Amount);

            gatherer.SwitchState(new FreeState());
            Assert.Empty(gatherer.Produce());

            gatherer.UpgradeProducerAmount(new ResourceAmount("wood", 2));

            gatherer.SwitchState(new WoodcutterState());

            woods = gatherer.Produce();
            Assert.Single(woods);
            Assert.Single(woods.Where(p => p.Type == "wood"));
            Assert.Equal(3, woods.Where(p => p.Type == "wood").Single().Amount);

            gatherer.UpgradeProducerProduct("ore");
            gatherer.SwitchState(new MinerState());

            var minerResults = gatherer.Produce();
            Assert.Equal(2, minerResults.Count);
            Assert.Single(minerResults.Where(p => p.Type == "rock"));
            Assert.Equal(1, minerResults.Where(p => p.Type == "rock").Single().Amount);
            Assert.Single(minerResults.Where(p => p.Type == "ore"));
            Assert.Equal(1, minerResults.Where(p => p.Type == "ore").Single().Amount);

            gatherer.UpgradeProducerAmount(new ResourceAmount("ore", 4));

            minerResults = gatherer.Produce();
            Assert.Equal(2, minerResults.Count);
            Assert.Single(minerResults.Where(p => p.Type == "rock"));
            Assert.Equal(1, minerResults.Where(p => p.Type == "rock").Single().Amount);
            Assert.Single(minerResults.Where(p => p.Type == "ore"));
            Assert.Equal(5, minerResults.Where(p => p.Type == "ore").Single().Amount);
        }

        [Fact]
        public void BuilderTest()
        {
            Builder builder = new Builder(1, 1);

            Assert.Equal(1, builder.FoodNeeded);
            Assert.Equal(1, builder.ProgressAmount);
        }

        [Fact]
        public void FarmerTest()
        {
            Farmer farmer = new Farmer(1,
                new Dictionary<string, List<ResourceAmount>>()
                {
                    { "cow", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("milk", 2)
                    } },
                    { "pig", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 4),
                    } },
                    { "chicken", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("egg", 2)
                    } },
                }, 5);

            Assert.Equal(1, farmer.FoodNeeded);
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
        public void FarmerDefaultConvertTest()
        {
            Farmer farmer = new Farmer(1,
                new Dictionary<string, List<ResourceAmount>>()
                {
                    { "cow", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("milk", 2)
                    } },
                    { "pig", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 4),
                    } },
                    { "chicken", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("egg", 2)
                    } },
                }, 5);

            Assert.Empty(farmer.Convert());

            farmer.AddInput(new ResourceAmount("cow", 1));
            var convertedCow = farmer.Convert();
            Assert.Equal(2, convertedCow.Count);
            Assert.Single(convertedCow.Where(c => c.Type == "meat"));
            Assert.Equal(2, convertedCow.Where(c => c.Type == "meat").Single().Amount);
            Assert.Single(convertedCow.Where(c => c.Type == "milk"));
            Assert.Equal(2, convertedCow.Where(c => c.Type == "milk").Single().Amount);

            Assert.Empty(farmer.Convert());

            farmer.AddInput(new ResourceAmount("pig", 1));
            var convertedPig = farmer.Convert();
            Assert.Single(convertedPig);
            Assert.Single(convertedPig.Where(c => c.Type == "meat"));
            Assert.Equal(4, convertedPig.Where(c => c.Type == "meat").Single().Amount);

            Assert.Empty(farmer.Convert());

            farmer.AddInput(new ResourceAmount("chicken", 1));
            var convertedChicken = farmer.Convert();
            Assert.Equal(2, convertedChicken.Count);
            Assert.Single(convertedChicken.Where(c => c.Type == "meat"));
            Assert.Equal(2, convertedChicken.Where(c => c.Type == "meat").Single().Amount);
            Assert.Single(convertedChicken.Where(c => c.Type == "egg"));
            Assert.Equal(2, convertedChicken.Where(c => c.Type == "egg").Single().Amount);

            Assert.Empty(farmer.Convert());
        }

        [Fact]
        public void FarmerDefaultConvertTest2()
        {
            Farmer farmer = new Farmer(1,
                new Dictionary<string, List<ResourceAmount>>()
                {
                    { "cow", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("milk", 2)
                    } },
                    { "pig", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 4),
                    } },
                    { "chicken", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("egg", 2)
                    } },
                }, 5);

            farmer.AddInput(new ResourceAmount("cow", 2));
            var convertedCow = farmer.Convert();
            Assert.Equal(2, convertedCow.Count);
            Assert.Single(convertedCow.Where(c => c.Type == "meat"));
            Assert.Equal(4, convertedCow.Where(c => c.Type == "meat").Single().Amount);
            Assert.Single(convertedCow.Where(c => c.Type == "milk"));
            Assert.Equal(4, convertedCow.Where(c => c.Type == "milk").Single().Amount);

            Assert.Empty(farmer.Convert());

            farmer.AddInput(new ResourceAmount("pig", 2));
            var convertedPig = farmer.Convert();
            Assert.Single(convertedPig);
            Assert.Single(convertedPig.Where(c => c.Type == "meat"));
            Assert.Equal(8, convertedPig.Where(c => c.Type == "meat").Single().Amount);

            Assert.Empty(farmer.Convert());

            farmer.AddInput(new ResourceAmount("chicken", 2));
            var convertedChicken = farmer.Convert();
            Assert.Equal(2, convertedChicken.Count);
            Assert.Single(convertedChicken.Where(c => c.Type == "meat"));
            Assert.Equal(4, convertedChicken.Where(c => c.Type == "meat").Single().Amount);
            Assert.Single(convertedChicken.Where(c => c.Type == "egg"));
            Assert.Equal(4, convertedChicken.Where(c => c.Type == "egg").Single().Amount);

            Assert.Empty(farmer.Convert());
        }

        [Fact]
        public void FarmerDefaultConvertTest3()
        {
            Farmer farmer = new Farmer(1,
                new Dictionary<string, List<ResourceAmount>>()
                {
                    { "cow", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("milk", 2)
                    } },
                    { "pig", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 4),
                    } },
                    { "chicken", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("egg", 2)
                    } },
                }, 5);

            Assert.Empty(farmer.Convert());

            farmer.AddInput(new ResourceAmount("cow", 1));
            farmer.AddInput(new ResourceAmount("pig", 1));
            farmer.AddInput(new ResourceAmount("chicken", 1));

            var converted = farmer.Convert();
            Assert.Equal(3, converted.Count);
            Assert.Single(converted.Where(c => c.Type == "meat"));
            Assert.Equal(8, converted.Where(c => c.Type == "meat").Single().Amount);//pig->4; cow->2; chick->2
            Assert.Single(converted.Where(c => c.Type == "milk"));
            Assert.Equal(2, converted.Where(c => c.Type == "milk").Single().Amount);
            Assert.Single(converted.Where(c => c.Type == "egg"));
            Assert.Equal(2, converted.Where(c => c.Type == "egg").Single().Amount);

            Assert.Empty(farmer.Convert());

            farmer.AddInput(new ResourceAmount("cow", 2));
            farmer.AddInput(new ResourceAmount("pig", 2));
            farmer.AddInput(new ResourceAmount("chicken", 2));

            var converted2 = farmer.Convert();
            Assert.Equal(3, converted2.Count);
            Assert.Single(converted2.Where(c => c.Type == "meat"));
            Assert.Equal(16, converted2.Where(c => c.Type == "meat").Single().Amount);//pig->4; cow->2; chick->2.. *2
            Assert.Single(converted2.Where(c => c.Type == "milk"));
            Assert.Equal(4, converted2.Where(c => c.Type == "milk").Single().Amount);
            Assert.Single(converted2.Where(c => c.Type == "egg"));
            Assert.Equal(4, converted2.Where(c => c.Type == "egg").Single().Amount);

            Assert.Empty(farmer.Convert());
        }

        [Fact]
        public void FarmerDefaultConvertTest4()
        {
            Farmer farmer = new Farmer(1,
                new Dictionary<string, List<ResourceAmount>>()
                {
                    { "cow", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("milk", 2)
                    } },
                    { "pig", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 4),
                    } },
                    { "chicken", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("egg", 2)
                    } },
                }, 5);

            Assert.Empty(farmer.Convert());
            Assert.Throws<Exception>(() => farmer.RemoveInput(new ResourceAmount("cow", 1)));

            farmer.AddInput(new ResourceAmount("cow", 1));
            Assert.Throws<Exception>(() => farmer.RemoveInput(new ResourceAmount("cow", 2)));

            
        }

        [Fact]
        public void FarmerDefaultConvertTest5()
        {
            var farmer = new Farmer(1,
                new Dictionary<string, List<ResourceAmount>>()
                {
                    { "cow", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("milk", 2)
                    } },
                    { "pig", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 4),
                    } },
                    { "chicken", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("egg", 2)
                    } },
                }, 5);

            farmer.AddInput(new ResourceAmount("cow", 2));
            farmer.RemoveInput(new ResourceAmount("cow", 1));
            var convertedCow = farmer.Convert();
            Assert.Equal(2, convertedCow.Count);
            Assert.Single(convertedCow.Where(c => c.Type == "meat"));
            Assert.Equal(2, convertedCow.Where(c => c.Type == "meat").Single().Amount);
            Assert.Single(convertedCow.Where(c => c.Type == "milk"));
            Assert.Equal(2, convertedCow.Where(c => c.Type == "milk").Single().Amount);

            Assert.Throws<Exception>(() => farmer.RemoveInput(new ResourceAmount("cow", 1)));
        }

        [Fact]
        public void FarmerDefaultConvertTest6()
        {
            var farmer = new Farmer(1,
                new Dictionary<string, List<ResourceAmount>>()
                {
                    { "cow", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("milk", 2)
                    } },
                    { "pig", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 4),
                    } },
                    { "chicken", new List<ResourceAmount>()
                    {
                        new ResourceAmount("meat", 2),
                        new ResourceAmount("egg", 2)
                    } },
                }, 5);
            Assert.Throws<Exception>(() => farmer.AddInput(new ResourceAmount("cow", 6)));

            farmer.AddInput(new ResourceAmount("cow", 3));
            farmer.AddInput(new ResourceAmount("pig", 1));
            farmer.RemoveInput(new ResourceAmount("cow", 2));
            var converted = farmer.Convert();
            Assert.Equal(2, converted.Count);
            Assert.Single(converted.Where(c => c.Type == "meat"));
            Assert.Equal(6, converted.Where(c => c.Type == "meat").Single().Amount); //1pig+1cow
            Assert.Single(converted.Where(c => c.Type == "milk"));
            Assert.Equal(2, converted.Where(c => c.Type == "milk").Single().Amount);

            Assert.Throws<Exception>(() => farmer.AddInput(new ResourceAmount("wood", 1)));
            Assert.Throws<Exception>(() => farmer.RemoveInput(new ResourceAmount("rock", 1)));
        }

        [Fact]
        public void FarmerCustomConvertTest()
        {
            Farmer farmer = new Farmer(1,
                new Dictionary<string, List<ResourceAmount>>()
                {
                    { "ore", new List<ResourceAmount>()
                    {
                        new ResourceAmount("ironore", 2),
                        new ResourceAmount("goldore", 3)
                    } },
                }, 5);
            Assert.Empty(farmer.Convert());

            farmer.AddInput(new ResourceAmount("ore", 3));
            var converted = farmer.Convert();
            Assert.Equal(2, converted.Count);
            Assert.Single(converted.Where(c => c.Type == "ironore"));
            Assert.Equal(6, converted.Where(c => c.Type == "ironore").Single().Amount);
            Assert.Single(converted.Where(c => c.Type == "goldore"));
            Assert.Equal(9, converted.Where(c => c.Type == "goldore").Single().Amount);

            Assert.Throws<Exception>(() => farmer.AddInput(new ResourceAmount("cow", 1)));
            Assert.Throws<Exception>(() => farmer.RemoveInput(new ResourceAmount("pig", 8)));

            Assert.Throws<Exception>(() => farmer.AddInput(new ResourceAmount("ore", 6)));
        }
        [Fact]
        public void ResearcherTest()
        {
            Researcher researcher = new Researcher(1,
               new List<ResourceAmount>() { new ResourceAmount("researchpoint", 1) },
               new List<ResourceAmount>() { new ResourceAmount("researchpoint", 1) },
               new Dictionary<string, List<ResourceAmount>>()
               {
                    { "ore", new List<ResourceAmount>()
                    {
                        new ResourceAmount("ironore", 1),
                        new ResourceAmount("goldore", 1)
                    } },
               },
               new Dictionary<string, List<ResourceAmount>>(), -1);
            Assert.Equal(1, researcher.FoodNeeded);
            Assert.Equal(int.MaxValue, researcher.MaxInput);
            Assert.Empty(researcher.InputOutputMapping);
            Assert.Single(researcher.Products);
            Assert.Single(researcher.Products.Where(r => r.Type == "researchpoint" && r.Amount == 1));
        }
        [Fact]
        public void ResearcherDefaultTest()
        {
            Researcher researcher = new Researcher(1,
                new List<ResourceAmount>() { new ResourceAmount("researchpoint", 1) },
                new List<ResourceAmount>() { new ResourceAmount("researchpoint", 1) },
                new Dictionary<string, List<ResourceAmount>>()
                {
                    { "ore", new List<ResourceAmount>()
                    {
                        new ResourceAmount("ironore", 1),
                        new ResourceAmount("goldore", 1)
                    } },
                },
                new Dictionary<string, List<ResourceAmount>>(), -1);

            Assert.Throws<Exception>(() => researcher.AddInput(new ResourceAmount("ore", 1)));
            Assert.Throws<Exception>(() => researcher.RemoveInput(new ResourceAmount("ore", 1)));

            Assert.Empty(researcher.Convert());

            var product = researcher.Produce();
            Assert.Single(product);
            Assert.Single(product.Where(p => p.Type == "researchpoint"));
            Assert.Equal(1, product.Where(p => p.Type == "researchpoint").Single().Amount);
        }
        [Fact]
        public void ResearcherDefaultUpgradesTest()
        {
            Researcher researcher = new Researcher(1,
                new List<ResourceAmount>() { new ResourceAmount("researchpoint", 1) },
                new List<ResourceAmount>() { new ResourceAmount("researchpoint", 1) },
                new Dictionary<string, List<ResourceAmount>>()
                {
                    { "ore", new List<ResourceAmount>()
                    {
                        new ResourceAmount("ironore", 1),
                        new ResourceAmount("goldore", 1)
                    } },
                },
                new Dictionary<string, List<ResourceAmount>>(), -1);

            var product = researcher.Produce();
            Assert.Single(product);
            Assert.Single(product.Where(p => p.Type == "researchpoint"));
            Assert.Equal(1, product.Where(p => p.Type == "researchpoint").Single().Amount);

            researcher.UpgradeProducerAmount(new ResourceAmount("researchpoint", 2));
            product = researcher.Produce();
            Assert.Single(product);
            Assert.Single(product.Where(p => p.Type == "researchpoint"));
            Assert.Equal(3, product.Where(p => p.Type == "researchpoint").Single().Amount);

            Assert.Empty(researcher.Convert());

            Assert.Throws<Exception>(() => researcher.UpgradeConverterInputMapping("ore", new List<ResourceAmount>() { new ResourceAmount("ironore", 1) }));

            researcher.UpgradeConverterOutputResource("ore");
            researcher.AddInput(new ResourceAmount("ore", 1));
            Assert.Empty(researcher.Convert());

            researcher.UpgradeConverterInputMapping("ore", new List<ResourceAmount>() { new ResourceAmount("ironore", 1) });
            Assert.Empty(researcher.Convert());

            researcher.AddInput(new ResourceAmount("ore", 1));
            var convert = researcher.Convert();
            Assert.Single(convert);
            Assert.Single(convert.Where(p => p.Type == "ironore"));
            Assert.Equal(1, convert.Where(p => p.Type == "ironore").Single().Amount);

            researcher.UpgradeConverterInputMapping("ore", new List<ResourceAmount>() { new ResourceAmount("goldore", 1) });
            researcher.AddInput(new ResourceAmount("ore", 1));
            convert = researcher.Convert();
            Assert.Equal(2, convert.Count);
            Assert.Single(convert.Where(p => p.Type == "ironore"));
            Assert.Equal(1, convert.Where(p => p.Type == "ironore").Single().Amount);
            Assert.Single(convert.Where(p => p.Type == "goldore"));
            Assert.Equal(1, convert.Where(p => p.Type == "goldore").Single().Amount);
        }
        [Fact]
        public void ResearcherDefaultTest2()
        {
            Researcher researcher = new Researcher(1,
                new List<ResourceAmount>() { new ResourceAmount("researchpoint", 1) },
                new List<ResourceAmount>() { new ResourceAmount("researchpoint", 1) },
                new Dictionary<string, List<ResourceAmount>>()
                {
                    { "ore", new List<ResourceAmount>()
                    {
                        new ResourceAmount("ironore", 1),
                        new ResourceAmount("goldore", 1)
                    } },
                },
                 new Dictionary<string, List<ResourceAmount>>(),
                 -1);
            Assert.Throws<Exception>(() => researcher.AddInput(new ResourceAmount("ore", 1)));
            Assert.Throws<Exception>(() => researcher.RemoveInput(new ResourceAmount("ore", 1)));

            Assert.Empty(researcher.Convert());

            var product = researcher.Produce();
            Assert.Single(product);
            Assert.Single(product.Where(p => p.Type == "researchpoint"));
            Assert.Equal(1, product.Where(p => p.Type == "researchpoint").Single().Amount);

            researcher.UpgradeConverterOutputResource("ore");
            researcher.UpgradeConverterInputMapping("ore", new List<ResourceAmount>() { new ResourceAmount("ironore", 1) });
            researcher.UpgradeConverterInputMapping("ore", new List<ResourceAmount>() { new ResourceAmount("goldore", 1) });
            researcher.AddInput(new ResourceAmount("ore", 1));

            var converted = researcher.Convert();
            Assert.Equal(2, converted.Count);
            Assert.Single(converted.Where(c => c.Type == "ironore"));
            Assert.Equal(1, converted.Where(c => c.Type == "ironore").Single().Amount);
            Assert.Single(converted.Where(c => c.Type == "goldore"));
            Assert.Equal(1, converted.Where(c => c.Type == "goldore").Single().Amount);

        }
    }
}
