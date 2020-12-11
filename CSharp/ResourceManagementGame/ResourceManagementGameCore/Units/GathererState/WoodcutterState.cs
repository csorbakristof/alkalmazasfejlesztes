using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceManagementGameCore.Units.GathererState
{
    public class WoodcutterState : IGathererState
    {
        public List<ResourceAmount> DoWork(List<ResourceAmount> amounts)
        {
            return amounts.Where(r => r.Type == "wood").Select(r => new ResourceAmount(r.Type, r.Amount)).ToList();
        }
    }
}
