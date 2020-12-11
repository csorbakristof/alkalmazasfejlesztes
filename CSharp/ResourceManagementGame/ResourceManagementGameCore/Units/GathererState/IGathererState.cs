using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Units.GathererState
{
    public interface IGathererState
    {
        List<ResourceAmount> DoWork(List<ResourceAmount> amounts);
    }
}
