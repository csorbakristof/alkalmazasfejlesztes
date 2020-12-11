using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using ResourceManagementGameCore.Algos;
using ResourceManagementGameCore.Research;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ResourceManagementGameCore.Config
{
    public class ConfigFile
    {
        public List<ResourceConfig> Resources { get; set; } = new List<ResourceConfig>();
        public List<ConstructionUnitConfig> ConstructionUnits { get; set; } = new List<ConstructionUnitConfig>();
        public List<ProducerUnitConfig> ProducerUnits { get; set; } = new List<ProducerUnitConfig>();
        public List<ConverterUnitConfig> ConverterUnits { get; set; } = new List<ConverterUnitConfig>();
        public List<ProducerAndConverterUnitConfig> ProducerAndConverterUnits { get; set; } = new List<ProducerAndConverterUnitConfig>();
        public List<DecorConfig> DecorBuildings { get; set; } = new List<DecorConfig>();
        public List<WorkplaceConfig> WorkplaceBuildings { get; set; } = new List<WorkplaceConfig>();
        //amennyiért tudsz venni valamit a market-ről
        public Dictionary<string, List<ResourceAmountConfig>> SellingPrices { get; set; } = new Dictionary<string, List<ResourceAmountConfig>>();
        //amennyiért el tudsz adni valamit a market-en
        public Dictionary<string, List<ResourceAmountConfig>> BuyingPrices { get; set; } = new Dictionary<string, List<ResourceAmountConfig>>();
        public SatisfactionConfig SatisfactionPrice { get; set; }
        public ResearchAlgoConfig ResearchAlgo { get; set; }
        public ConfigFile()
        {
            Default();
        }
        public string GetAll()
        {
            string json = JsonConvert.SerializeObject(this);
            return JsonConvert.ToString(json);
        }
        public void Default()
        {
            Resources.Add(new ResourceConfig() { Name = "coin", IsFood = false });
            Resources.Add(new ResourceConfig() { Name = "wood", IsFood = false });
            Resources.Add(new ResourceConfig() { Name = "rock", IsFood = false });
            Resources.Add(new ResourceConfig() { Name = "ore", IsFood = false });
            Resources.Add(new ResourceConfig() { Name = "ironore", IsFood = false });
            Resources.Add(new ResourceConfig() { Name = "goldore", IsFood = false });
            Resources.Add(new ResourceConfig() { Name = "researchpoint", IsFood = false });
            Resources.Add(new ResourceConfig() { Name = "milk", IsFood = true });
            Resources.Add(new ResourceConfig() { Name = "meat", IsFood = true });
            Resources.Add(new ResourceConfig() { Name = "egg", IsFood = true });

            Resources.Add(new ResourceConfig() { Name = "cow", IsFood = false});
            Resources.Add(new ResourceConfig() { Name = "pig", IsFood = false});
            Resources.Add(new ResourceConfig() { Name = "chicken", IsFood = false});

            ResearchAlgo = new ResearchAlgoConfig()
            {
                IncrementAlgoType = AlgoType.Fibonacci
            };

            SatisfactionPrice = new SatisfactionConfig()
            {
                InitialCost = 2,
                IncrementAlgoType = AlgoType.Exp,
            };
            SellingPrices = new Dictionary<string, List<ResourceAmountConfig>>()
            {
                {
                     "cow",
                    new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("coin",10)
                    }
                },{
                    "pig",
                    new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("coin",20)
                    }
                },{
                    "chicken",
                    new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("coin",10)
                    }
                },
            };

            BuyingPrices = new Dictionary<string, List<ResourceAmountConfig>>()
            {
                {
                    "wood",
                    new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("coin",1)
                    }
                },{
                    "rock",
                    new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("coin",1)
                    }
                },{
                    "ore",
                    new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("coin",2)
                    }
                },{
                    "ironore",
                    new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("coin",5)
                    }
                },{
                    "goldore",
                    new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("coin",25)
                    }
                },{
                    "meat",
                    new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("coin",10)
                    }
                },{
                    "milk",
                    new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("coin",13)
                    }
                },{
                    "egg",
                    new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("coin",15)
                    }
                },
            };

            ConstructionUnits.Add(new ConstructionUnitConfig()
            {
                UnitType = "ResourceManagementGameCore.Units.Builder",
                FoodConsume = 1,
                Progress = 1,
                InitialCost = new List<ResourceAmountConfig>() { new ResourceAmountConfig("coin", 100) },
                CostIncrementAlgoType = AlgoType.Multiple,
                AlgoSecondaryValue = 1.2
            });

            ProducerUnits.Add(new ProducerUnitConfig()
            {
                UnitType = "ResourceManagementGameCore.Units.Gatherer",
                FoodConsume = 1,
                InitialProductableResources = new List<ResourceAmountConfig>() { new ResourceAmountConfig("wood", 1), new ResourceAmountConfig("rock", 1) },
                AllProductableResources = new List<ResourceAmountConfig>() { new ResourceAmountConfig("wood", 1), new ResourceAmountConfig("rock", 1), new ResourceAmountConfig("ore", 1) },
                InitialCost = new List<ResourceAmountConfig>() { new ResourceAmountConfig("coin", 50) },
                CostIncrementAlgoType = AlgoType.Increment,
                AlgoSecondaryValue = 1
            });

            ConverterUnits.Add(new ConverterUnitConfig()
            {
                UnitType = "ResourceManagementGameCore.Units.Farmer",
                FoodConsume = 1,
                InitialCost = new List<ResourceAmountConfig>() { new ResourceAmountConfig("coin", 50) },
                CostIncrementAlgoType = AlgoType.Increment,
                AlgoSecondaryValue = 1,
                AllConvertableResourceMapping = new Dictionary<string, List<ResourceAmountConfig>>()
                {
                    { "cow", new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("meat", 2),
                        new ResourceAmountConfig("milk", 2)
                    } },
                    { "pig", new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("meat", 4),
                    } },
                    { "chicken", new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("meat", 2),
                        new ResourceAmountConfig("egg", 2)
                    } },
                },
                InitialConvertableResourceMapping = new Dictionary<string, List<ResourceAmountConfig>>()
                {
                    { "cow", new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("meat", 2),
                        new ResourceAmountConfig("milk", 2)
                    } },
                    { "pig", new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("meat", 4),
                    } },
                    { "chicken", new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("meat", 2),
                        new ResourceAmountConfig("egg", 2)
                    } },
                },
                MaxInput = 5
            });

            ProducerAndConverterUnits.Add(new ProducerAndConverterUnitConfig()
            {
                UnitType = "ResourceManagementGameCore.Units.Researcher",
                FoodConsume = 1,
                InitialCost = new List<ResourceAmountConfig>() { new ResourceAmountConfig("coin", 100) },
                CostIncrementAlgoType = AlgoType.Multiple,
                AlgoSecondaryValue = 1.5,
                AllConvertableResourceMapping = new Dictionary<string, List<ResourceAmountConfig>>()
                {
                    { "ore", new List<ResourceAmountConfig>()
                    {
                        new ResourceAmountConfig("ironore", 1),
                        new ResourceAmountConfig("goldore", 1)
                    } }
                },
                InitialConvertableResourceMapping = new Dictionary<string, List<ResourceAmountConfig>>(),
                InitialProductableResources = new List<ResourceAmountConfig>() { new ResourceAmountConfig("researchpoint", 1) },
                AllProductableResources = new List<ResourceAmountConfig>() { new ResourceAmountConfig("researchpoint", 1) },
                MaxInput = -1
            });

            WorkplaceBuildings.Add(new WorkplaceConfig()
            {
                BuildingType= "ResourceManagementGameCore.Buildings.ForestCamp",
                CostIncrementAlgoType = AlgoType.Multiple,
                AlgoSecondaryValue =1.5,
                BuildCost= new List<ResourceAmountConfig>()
                {
                    new ResourceAmountConfig("wood", 10),
                    new ResourceAmountConfig("rock", 5)
                },
                BuildTime=5,
                MaxCapacity=5,
            });

            WorkplaceBuildings.Add(new WorkplaceConfig()
            {
                BuildingType = "ResourceManagementGameCore.Buildings.MiningVillage",
                CostIncrementAlgoType = AlgoType.Multiple,
                AlgoSecondaryValue = 1.3,
                BuildCost = new List<ResourceAmountConfig>()
                {
                    new ResourceAmountConfig("wood", 10),
                    new ResourceAmountConfig("rock", 5)
                },
                BuildTime = 5,
                MaxCapacity = 5,
            });

            WorkplaceBuildings.Add(new WorkplaceConfig()
            {
                BuildingType = "ResourceManagementGameCore.Buildings.Lab",
                CostIncrementAlgoType = AlgoType.Multiple,
                AlgoSecondaryValue = 1.3,
                BuildCost = new List<ResourceAmountConfig>()
                {
                    new ResourceAmountConfig("wood", 30),
                    new ResourceAmountConfig("rock", 20),
                    new ResourceAmountConfig("ironore", 10)
                },
                BuildTime = 10,
                MaxCapacity = 5,
            });

            WorkplaceBuildings.Add(new WorkplaceConfig()
            {
                BuildingType = "ResourceManagementGameCore.Buildings.Farm",
                CostIncrementAlgoType = AlgoType.Multiple,
                AlgoSecondaryValue = 2,
                BuildCost = new List<ResourceAmountConfig>()
                {
                    new ResourceAmountConfig("wood", 50),
                    new ResourceAmountConfig("rock", 10),
                    new ResourceAmountConfig("ironore", 10)
                },
                BuildTime = 7,
                MaxCapacity = 1,
            });
        }
    }
    public class ResourceConfig
    {
        public string Name { get; set; }
        public bool IsFood { get; set; }
    }
    public class ResourceAmountConfig
    {
        public string Type { get; set; }
        public int Amount { get; set; }
        public ResourceAmountConfig(string t, int a)
        {
            Type = t;
            Amount = a;
        }
    }
    public class UnitConfig
    {
        public string UnitType { get; set; }
        public int FoodConsume { get; set; }
        public List<ResourceAmountConfig> InitialCost { get; set; }
        public AlgoType CostIncrementAlgoType { get; set; }
        public double? AlgoSecondaryValue { get; set; }

    }
    public class ConstructionUnitConfig : UnitConfig
    {
        public int Progress { get; set; }
    }
    public class ProducerUnitConfig : UnitConfig
    {
        public List<ResourceAmountConfig> AllProductableResources { get; set; }
        public List<ResourceAmountConfig> InitialProductableResources { get; set; }
    }
    public class ConverterUnitConfig : UnitConfig
    {
        public Dictionary<string, List<ResourceAmountConfig>> AllConvertableResourceMapping { get; set; }
        public Dictionary<string, List<ResourceAmountConfig>> InitialConvertableResourceMapping { get; set; }
        public int MaxInput { get; set; }
    }
    public class ProducerAndConverterUnitConfig : UnitConfig
    {
        public List<ResourceAmountConfig> AllProductableResources { get; set; }
        public List<ResourceAmountConfig> InitialProductableResources { get; set; }
        public Dictionary<string, List<ResourceAmountConfig>> AllConvertableResourceMapping { get; set; }
        public Dictionary<string, List<ResourceAmountConfig>> InitialConvertableResourceMapping { get; set; }
        public int MaxInput { get; set; }
    }
    public class BuildingConfig
    {
        public string BuildingType { get; set; }
        public int BuildTime { get; set; }
        public List<ResourceAmountConfig> BuildCost { get; set; }
    }
    public class DecorConfig : BuildingConfig
    {
        public int SatisfactionBoost { get; set; }
    }
    public class WorkplaceConfig : BuildingConfig
    {
        public int MaxCapacity { get; set; }
        public AlgoType CostIncrementAlgoType { get; set; }
        public double? AlgoSecondaryValue { get; set; }
    }
    public class SatisfactionConfig
    {
        public int? InitialCost { get; set; }
        public AlgoType IncrementAlgoType { get; set; }
        public double? AlgoSecondValue { get; set; }
    }
    public class ResearchAlgoConfig
    {
        public int? InitialCost { get; set; }
        public AlgoType IncrementAlgoType { get; set; }
        public double? AlgoSecondValue { get; set; }
    }
}
