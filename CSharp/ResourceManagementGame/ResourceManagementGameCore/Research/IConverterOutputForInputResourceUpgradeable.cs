using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Research
{
    public interface IConverterOutputForInputResourceUpgradeable
    {
        void UpgradeConverterInputMapping(string inputResourceName, List<ResourceAmount> outputResourceAmounts);
    }
}
