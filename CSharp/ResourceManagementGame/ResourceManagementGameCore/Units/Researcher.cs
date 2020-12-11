using ResourceManagementGameCore.Research;
using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceManagementGameCore.Units
{
    public class Researcher : ProducerAndConverterUnit, IConverterInputResourceUpgradeable, IProducerAmountUpgradeable, IConverterOutputForInputResourceUpgradeable
    {
        public Dictionary<string, List<ResourceAmount>> AllInputOutputMapping { get; set; }
        public Researcher(int food, List<ResourceAmount> allProducts, List<ResourceAmount> initialProducts, Dictionary<string, List<ResourceAmount>> allMapping, Dictionary<string, List<ResourceAmount>> initialMapping, int maxInput) : base(food, initialProducts, initialMapping, maxInput)
        {
            if(maxInput == -1)
                MaxInput = int.MaxValue;
            AllInputOutputMapping = allMapping;
        }

        public void UpgradeConverterOutputResource(string resource)
        {
            if (InputOutputMapping.ContainsKey(resource))
                return;
            InputOutputMapping.Add(resource, new List<ResourceAmount>());
            CurrentInput.Add(new ResourceAmount(resource, 0));
        }

        public void UpgradeProducerAmount(ResourceAmount resourceAmount)
        {
            foreach (var item in Products)
            {
                if (item.Type == resourceAmount.Type)
                    item.Amount += resourceAmount.Amount;
            }
        }

        public void UpgradeConverterInputMapping(string input, List<ResourceAmount> outputResourceAmounts)
        {
            if (!(AllInputOutputMapping.ContainsKey(input)&&InputOutputMapping.ContainsKey(input)))
                throw new Exception("Input not found in inputoutput mapping");
            foreach (var item in outputResourceAmounts)
            {
                if (!AllInputOutputMapping[input].Any(m => m.Type == item.Type))
                    throw new Exception("Output resource not found in inputoutput mapping");
            }

            InputOutputMapping[input].AddRange(outputResourceAmounts);
        }
    }
}
