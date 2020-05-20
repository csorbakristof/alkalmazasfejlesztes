using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Composite.Model
{
    /// <summary>
    /// "Leaf" a mintában
    /// </summary>
    public class SimpleReward : RewardBase
    {
        
        public override string GetRewardsString()
        {
            return this.Name;
        }
        public override void AddReward(RewardBase newReward)
        {
            throw new InvalidOperationException();
        }
    }
}
