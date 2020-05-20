using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Composite.Model
{
    /// <summary>
    /// "Component" a mintában
    /// </summary>
    public abstract class RewardBase
    {
        public string Name { get; set; }

        public abstract string GetRewardsString();
        public abstract void AddReward(RewardBase newReward);
    }
}
