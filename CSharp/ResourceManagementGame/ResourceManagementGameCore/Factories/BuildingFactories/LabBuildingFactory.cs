using ResourceManagementGameCore.Buildings;
using ResourceManagementGameCore.Config;
using ResourceManagementGameCore.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Factories.BuildingFactories
{
    public class LabBuildingFactory : IBuildingFactory
    {
        public Building CreateBuilding()
        {
            return new Lab(ConfigurationManager.GetInstance().GetWorkplaceBuildingMaxCapacity(typeof(Lab)),
                ConfigurationManager.GetInstance().GetBuildingBuildTime(typeof(Lab)));
        }
    }
    public class LabBuildingRegister : IBuildingRegister
    {
        public IBuildingFactory CreateFactory()
        {
            return new LabBuildingFactory();
        }

        public Type GetBuildingType()
        {
            return typeof(Lab);
        }
    }
}
