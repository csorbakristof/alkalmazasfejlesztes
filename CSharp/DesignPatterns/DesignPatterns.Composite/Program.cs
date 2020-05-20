using DesignPatterns.Composite.Model;
using System;

namespace DesignPatterns.Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleReward simpleReward = new SimpleReward() { Name = "Simple reward, yay!" };
            Console.WriteLine(simpleReward.GetRewardsString());

            CompositeReward compositeReward = new CompositeReward() { Name = "Composite reward" };
            CompositeReward smallPouch = new CompositeReward() { Name = "Small pouch" };
            SimpleReward silverCoins = new SimpleReward() { Name = "10 silver coins" };
            SimpleReward bread = new SimpleReward() { Name = "Fresh bread" };
            smallPouch.AddReward(silverCoins);
            smallPouch.AddReward(bread);
            CompositeReward mediumPouch = new CompositeReward() { Name = "Medium pouch" };
            SimpleReward goldCoins = new SimpleReward() { Name = "5 gold coins" };
            CompositeReward potionPouch = new CompositeReward() { Name = "Potion pouch" };
            SimpleReward hpPotion = new SimpleReward() { Name = "HP potion" };
            potionPouch.AddReward(hpPotion);
            mediumPouch.AddReward(goldCoins);
            mediumPouch.AddReward(potionPouch);
            compositeReward.AddReward(smallPouch);
            compositeReward.AddReward(mediumPouch);

            Console.WriteLine(compositeReward.GetRewardsString());

            
        }
    }
}
