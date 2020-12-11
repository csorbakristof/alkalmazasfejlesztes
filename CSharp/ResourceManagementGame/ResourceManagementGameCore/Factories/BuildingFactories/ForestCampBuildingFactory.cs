using ResourceManagementGameCore.Buildings;
using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Factories.BuildingFactories
{
    public class ForestCampBuildingFactory :IBuildingFactory
    {
        public Building CreateBuilding()
        {
            return new ForestCamp(ConfigurationManager.GetInstance().GetWorkplaceBuildingMaxCapacity(typeof(ForestCamp)),
                ConfigurationManager.GetInstance().GetBuildingBuildTime(typeof(ForestCamp)));            
        }
    }
    public class ForestCampBuildingRegister : IBuildingRegister
    {
        public IBuildingFactory CreateFactory()
        {
            return new ForestCampBuildingFactory();
        }

        public Type GetBuildingType()
        {
            return typeof(ForestCamp);
        }
    }
}
