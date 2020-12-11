using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceManagementGameCore.Units.GathererState
{
    public class MinerState : IGathererState
    {
        public List<ResourceAmount> DoWork(List<ResourceAmount> amounts)
        {
            List<ResourceAmount> result = new List<ResourceAmount>();
            result.AddRange(amounts.Where(r => r.Type == "rock").Select(r => new ResourceAmount(r.Type, r.Amount)));
            result.AddRange(amounts.Where(r => r.Type == "ore").Select(r => new ResourceAmount(r.Type, r.Amount)));
            return result;
        }
    }
}
