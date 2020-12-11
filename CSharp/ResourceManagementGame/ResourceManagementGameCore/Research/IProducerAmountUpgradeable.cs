using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Research
{
    public interface IProducerAmountUpgradeable
    {
        void UpgradeProducerAmount(ResourceAmount resourceAmount);
    }
}
