using Newtonsoft.Json;
using ResourceManagementGameCore.Algos;
using ResourceManagementGameCore.Research;
using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ResourceManagementGameCore.Config
{
    public class ConfigurationManager
    {
        private Dictionary<Type, ProducerUnitConfig> ProducerUnits  = new Dictionary<Type, ProducerUnitConfig>();
        private Dictionary<Type, ConverterUnitConfig> ConverterUnits  = new Dictionary<Type, ConverterUnitConfig>();
        private Dictionary<Type, ProducerAndConverterUnitConfig> ProducerAndConverterUnits  = new Dictionary<Type, ProducerAndConverterUnitConfig>();
        private Dictionary<Type, ConstructionUnitConfig> ConstructionUnits  = new Dictionary<Type, ConstructionUnitConfig>();
        private Dictionary<Type, DecorConfig> DecorBuildings = new Dictionary<Type, DecorConfig>();
        private Dictionary<Type, WorkplaceConfig> WorkplaceBuildings  = new Dictionary<Type, WorkplaceConfig>();
        private int? SatisfactionBoostInitialCost;
        private AlgoType SatisfactionBoostAlgo;
        private double? SatisfactionBoostAlgoValue;
        private Dictionary<string, List<ResourceAmount>> SellingPrices = new Dictionary<string, List<ResourceAmount>>();
        private Dictionary<string, List<ResourceAmount>> BuyingPrices = new Dictionary<string, List<ResourceAmount>>();
        private ResearchAlgoConfig ResearchIncrementAlgo;
        private ResearchBuilder builder;
        public static void SetJson(string all)
        {
            allJson = all;
        }
        private static string allJson;
        private ConfigurationManager()
        {
            try
            {
                var config = JsonConvert.DeserializeObject<ConfigFile>(allJson);
                foreach (var item in config.Resources)
                {
                    ResourceType.AddResourceType(item.Name, item.IsFood);
                }
                foreach (var item in config.ProducerUnits)
                {
                    var type = Type.GetType(item.UnitType);
                    if (!ProducerUnits.ContainsKey(type))
                        ProducerUnits.Add(type, item);                        
                }
                foreach (var item in config.ConverterUnits)
                {
                    var type = Type.GetType(item.UnitType);
                    if (!ConverterUnits.ContainsKey(type))
                        ConverterUnits.Add(type, item);
                }
                foreach (var item in config.ProducerAndConverterUnits)
                {
                    var type = Type.GetType(item.UnitType);
                    if (!ProducerAndConverterUnits.ContainsKey(type))
                        ProducerAndConverterUnits.Add(type, item);
                }
                foreach (var item in config.ConstructionUnits)
                {
                    var type = Type.GetType(item.UnitType);
                    if (!ConstructionUnits.ContainsKey(type))
                        ConstructionUnits.Add(type, item);
                }
                foreach (var item in config.DecorBuildings)
                {
                    DecorBuildings.Add(Type.GetType(item.BuildingType), item);
                }
                foreach (var item in config.WorkplaceBuildings)
                {
                    var type = Type.GetType(item.BuildingType);
                    if(!WorkplaceBuildings.ContainsKey(type))
                        WorkplaceBuildings.Add(Type.GetType(item.BuildingType), item);
                }
                SatisfactionBoostAlgo = config.SatisfactionPrice.IncrementAlgoType;
                SatisfactionBoostInitialCost = config.SatisfactionPrice.InitialCost;
                SatisfactionBoostAlgoValue = config.SatisfactionPrice.AlgoSecondValue;

                foreach(var item in config.SellingPrices)
                {
                    if(!SellingPrices.ContainsKey(item.Key))
                        SellingPrices.Add(item.Key, item.Value.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
                }
                foreach (var item in config.BuyingPrices)
                {
                    if (!BuyingPrices.ContainsKey(item.Key))
                        BuyingPrices.Add(item.Key, item.Value.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
                }
                ResearchIncrementAlgo = config.ResearchAlgo;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Serialization error");
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Debugger.Break();
                return;
            }
        }
        private static ConfigurationManager _instance;
        public static ConfigurationManager GetInstance()
        {
            if (_instance is null) _instance = new ConfigurationManager();
            return _instance;
        }
        public Dictionary<string, List<ResourceAmount>> GetSellingPrices()
        {
            return SellingPrices;
        }
        public Dictionary<string, List<ResourceAmount>> GetBuyingPrices()
        {
            return BuyingPrices;
        }
        public Dictionary<Type, List<ResourceAmount>> GetBuildingInitialPrices()
        {
            Dictionary<Type, List<ResourceAmount>> result = new Dictionary<Type, List<ResourceAmount>>();
            var keys = WorkplaceBuildings.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, WorkplaceBuildings[item].BuildCost.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
            }
            keys = DecorBuildings.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, DecorBuildings[item].BuildCost.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
            }
            return result;
        }
        public (AlgoType algoType, int? initialValue, double? algoSecondValue) GetResearchCostIncrementAlgo()
        {
            return (ResearchIncrementAlgo.IncrementAlgoType, ResearchIncrementAlgo.InitialCost, ResearchIncrementAlgo.AlgoSecondValue);
        }
        public Dictionary<Type, AlgoType> GetBuildingIncrementAlgos()
        {
            Dictionary<Type, AlgoType> result = new Dictionary<Type, AlgoType>();
            var keys = WorkplaceBuildings.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, WorkplaceBuildings[item].CostIncrementAlgoType);
            }
            return result;
        }
        public Dictionary<Type, double?> GetBuildingIncrementAlgoValues()
        {
            Dictionary<Type, double?> result = new Dictionary<Type, double?>();
            var keys = WorkplaceBuildings.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, WorkplaceBuildings[item].AlgoSecondaryValue);
            }
            return result;
        }
        public List<ResourceAmount> GetAllProductsForProducerUnit(Type productionUnitType)
        {
            List<ResourceAmount> result = new List<ResourceAmount>();
            if (ProducerUnits.ContainsKey(productionUnitType))
            {
                var unitConfig = ProducerUnits[productionUnitType];
                result = unitConfig.AllProductableResources.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList();
                return result;
            }
            throw new Exception("Invalid production unit type");
        }
        public List<ResourceAmount> GetInitialProductsForProducerUnit(Type productionUnitType)
        {
            List<ResourceAmount> result = new List<ResourceAmount>();
            if (ProducerUnits.ContainsKey(productionUnitType))
            {
                var unitConfig = ProducerUnits[productionUnitType];
                result = unitConfig.InitialProductableResources.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList();
                return result;
            }
            throw new Exception("Invalid production unit type");
        }
        public Dictionary<string, List<ResourceAmount>> GetInitialMappingForConverterUnit(Type converterUnitType)
        {
            Dictionary<string, List<ResourceAmount>> result = new Dictionary<string, List<ResourceAmount>>();
            if (ConverterUnits.ContainsKey(converterUnitType))
            {
                var unitConfig = ConverterUnits[converterUnitType];
                foreach (var item in unitConfig.InitialConvertableResourceMapping)
                {
                    result.Add(item.Key, item.Value.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
                }
                return result;
            }
            throw new Exception("Invalid production unit type");
        }
        public Dictionary<string, List<ResourceAmount>> GetAllMappingForConverterUnit(Type converterUnitType)
        {
            Dictionary<string, List<ResourceAmount>> result = new Dictionary<string, List<ResourceAmount>>();
            if (ConverterUnits.ContainsKey(converterUnitType))
            {
                var unitConfig = ConverterUnits[converterUnitType];
                foreach (var item in unitConfig.AllConvertableResourceMapping)
                {
                    result.Add(item.Key, item.Value.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
                }
                return result;
            }
            throw new Exception("Invalid production unit type");
        }
        public List<ResourceAmount> GetAllProductsForProducerAndConverterUnit(Type productionAndConverterUnitType)
        {
            List<ResourceAmount> result = new List<ResourceAmount>();
            if (ProducerAndConverterUnits.ContainsKey(productionAndConverterUnitType))
            {
                var unitConfig = ProducerAndConverterUnits[productionAndConverterUnitType];
                result = unitConfig.AllProductableResources.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList();
                return result;
            }
            throw new Exception("Invalid production unit type");
        }
        public List<ResourceAmount> GetInitialProductsForProducerAndConverterUnit(Type productionAndConverterUnitType)
        {
            List<ResourceAmount> result = new List<ResourceAmount>();
            if (ProducerAndConverterUnits.ContainsKey(productionAndConverterUnitType))
            {
                var unitConfig = ProducerAndConverterUnits[productionAndConverterUnitType];
                result = unitConfig.InitialProductableResources.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList();
                return result;
            }
            throw new Exception("Invalid production unit type");
        }
        public Dictionary<string, List<ResourceAmount>> GetInitialMappingForProducerAndConverterUnit(Type productionAndConverterUnitType)
        {
            Dictionary<string, List<ResourceAmount>> result = new Dictionary<string, List<ResourceAmount>>();
            if (ProducerAndConverterUnits.ContainsKey(productionAndConverterUnitType))
            {
                var unitConfig = ProducerAndConverterUnits[productionAndConverterUnitType];
                foreach (var item in unitConfig.InitialConvertableResourceMapping)
                {
                    result.Add(item.Key, item.Value.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
                }
                return result;
            }
            throw new Exception("Invalid production unit type");
        }
        public Dictionary<string, List<ResourceAmount>> GetAllMappingForProducerAndConverterUnit(Type productionAndConverterUnitType)
        {
            Dictionary<string, List<ResourceAmount>> result = new Dictionary<string, List<ResourceAmount>>();
            if (ProducerAndConverterUnits.ContainsKey(productionAndConverterUnitType))
            {
                var unitConfig = ProducerAndConverterUnits[productionAndConverterUnitType];
                foreach (var item in unitConfig.AllConvertableResourceMapping)
                {
                    result.Add(item.Key, item.Value.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
                }
                return result;
            }
            throw new Exception("Invalid production unit type");
        }
        public Dictionary<Type, List<ResourceAmount>> GetUnitInitialPrices()
        {
            Dictionary<Type, List<ResourceAmount>> result = new Dictionary<Type, List<ResourceAmount>>();
            
            var keys = ProducerUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ProducerUnits[item].InitialCost.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
            }

            keys = ConverterUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ConverterUnits[item].InitialCost.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
            }
            keys = ProducerAndConverterUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ProducerAndConverterUnits[item].InitialCost.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
            }

            keys = ConstructionUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ConstructionUnits[item].InitialCost.Select(r => new ResourceAmount(r.Type, r.Amount)).ToList());
            }
            return result;
        }
        public Dictionary<Type, AlgoType> GetUnitIncrementAlgos()
        {
            Dictionary<Type, AlgoType> result = new Dictionary<Type, AlgoType>();

            var keys = ProducerUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ProducerUnits[item].CostIncrementAlgoType);
            }

            keys = ConverterUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ConverterUnits[item].CostIncrementAlgoType);
            }
            keys = ProducerAndConverterUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ProducerAndConverterUnits[item].CostIncrementAlgoType);
            }
            keys = ConstructionUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ConstructionUnits[item].CostIncrementAlgoType);
            }

            return result;
        }
        public Dictionary<Type, double?> GetUnitIncrementAlgoValues()
        {
            Dictionary<Type, double?> result = new Dictionary<Type, double?>();

            var keys = ProducerUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ProducerUnits[item].AlgoSecondaryValue);
            }

            keys = ConverterUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ConverterUnits[item].AlgoSecondaryValue);
            }
            keys = ProducerAndConverterUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ProducerAndConverterUnits[item].AlgoSecondaryValue);
            }
            keys = ConstructionUnits.Keys.ToList();
            foreach (var item in keys)
            {
                result.Add(item, ConstructionUnits[item].AlgoSecondaryValue);
            }

            return result;
        }
        public int GetBuildingBuildTime(Type building)
        {
            if (WorkplaceBuildings.ContainsKey(building))
                return WorkplaceBuildings[building].BuildTime;
            if (DecorBuildings.ContainsKey(building))
                return DecorBuildings[building].BuildTime;
            throw new Exception("Invalid building type");
        }
        public int GetWorkplaceBuildingMaxCapacity(Type building)
        {
            if (WorkplaceBuildings.ContainsKey(building))
                return WorkplaceBuildings[building].MaxCapacity;
            throw new Exception("Invalid building type");
        }
        public int GetUnitFoodConsume(Type unit)
        {
            if (ProducerUnits.ContainsKey(unit))
                return ProducerUnits[unit].FoodConsume;
            if (ConverterUnits.ContainsKey(unit))
                return ConverterUnits[unit].FoodConsume;
            if (ProducerAndConverterUnits.ContainsKey(unit))
                return ProducerAndConverterUnits[unit].FoodConsume;
            if (ConstructionUnits.ContainsKey(unit))
                return ConstructionUnits[unit].FoodConsume;
            throw new Exception("Invalid unit type");
        }
        public int GetConstructionUnitProgressAmount(Type unit)
        {
            if (ConstructionUnits.ContainsKey(unit))
                return ConstructionUnits[unit].Progress;
            throw new Exception("Invalid unit type");
        }
        public int GetConverterUnitMaxInput(Type unit)
        {
            if (ConverterUnits.ContainsKey(unit))
                return ConverterUnits[unit].MaxInput;
            if (ProducerAndConverterUnits.ContainsKey(unit))
                return ProducerAndConverterUnits[unit].MaxInput;
            throw new Exception("Invalid unit type");
        }
        public (AlgoType algoType, int? initial, double? algoValue) GetSatisfactionBoostAlgo()
        {
            return (SatisfactionBoostAlgo, SatisfactionBoostInitialCost, SatisfactionBoostAlgoValue);
        }
    }
}
