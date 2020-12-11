using ResourceManagementGameCore.Research;
using ResourceManagementGameCore.Resources;
using ResourceManagementGameCore.Units.GathererState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ResourceManagementGameCore.Units
{
    public class Gatherer : ProducerUnit, IProducerAmountUpgradeable, IProducerProductsUpgradeable
    {
        private List<ResourceAmount> AllProducts;
        private IGathererState CurrentState;
        public Gatherer(int food, List<ResourceAmount> allProducts, List<ResourceAmount> initialProducts) : base(food, initialProducts)
        {
            AllProducts = allProducts.Select(p => new ResourceAmount(p.Type, p.Amount)).ToList();
            CurrentState = new FreeState();
        }
        public void SwitchState(IGathererState state)
        {
            if (CurrentState.GetType() == state.GetType())
                return;
            CurrentState = state;
        }
        public override List<ResourceAmount> Produce()
        {
            switch (this.Satisfaction)
            {
                case (Satisfaction.Unsatisfied):
                    return new List<ResourceAmount>();
                case (Satisfaction.Ok):
                    return CurrentState.DoWork(Products);
                case (Satisfaction.Happy):
                    var products = CurrentState.DoWork(Products);
                    products.ForEach(p => p.Amount *= 2);
                    return products;
            }
            return new List<ResourceAmount>();
        }

        public void UpgradeProducerProduct(string resource)
        {
            var res = AllProducts.Where(p => p.Type == resource).SingleOrDefault();
            if (res == null)
                return;
            Products.Add(new ResourceAmount(res.Type, res.Amount));
        }

        public void UpgradeProducerAmount(ResourceAmount resourceAmount)
        {
            foreach (var item in Products)
            {
                if (item.Type == resourceAmount.Type)
                    item.Amount += resourceAmount.Amount;
            }
            foreach (var item in AllProducts)
            {
                if (item.Type == resourceAmount.Type)
                    item.Amount += resourceAmount.Amount;
            }
        }
    }
}
