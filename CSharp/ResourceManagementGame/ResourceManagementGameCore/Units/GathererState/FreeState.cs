using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Units.GathererState
{
    public class FreeState : IGathererState
    {
        public List<ResourceAmount> DoWork(List<ResourceAmount> amounts)
        {
            return new List<ResourceAmount>();
        }
    }
}
