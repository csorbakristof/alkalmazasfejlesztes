using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Composite.Model
{
    /// <summary>
    /// "Composite" a mintában
    /// </summary>
    public class CompositeReward : RewardBase
    {
        private List<RewardBase> rewardList;
        public CompositeReward()
        {
            rewardList = new List<RewardBase>();
        }
        public override void AddReward(RewardBase newReward)
        {
            rewardList.Add(newReward);
        }

        public override string GetRewardsString()
        {
            string result = this.Name;
            foreach (var reward in rewardList)
            {
                result += "("+reward.GetRewardsString()+")";
            }
            return result;
        }
    }
}
