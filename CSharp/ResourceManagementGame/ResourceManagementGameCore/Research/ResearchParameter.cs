using ResourceManagementGameCore.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Research
{
    public interface IResearchParameter
    {
    }
    public class ConverterOutputForInputResourceParameter : IResearchParameter
    {
        public string InputResourceName { get; }
        public List<ResourceAmount> OutputResources { get; }
        public ConverterOutputForInputResourceParameter(string input, List<ResourceAmount> outputs)
        {
            InputResourceName = input;
            OutputResources = outputs;
        }
    }
    public class ConverterInputResourceParameters : IResearchParameter
    {
        public string ResourceName { get; }
        public ConverterInputResourceParameters(string resource)
        {
            ResourceName = resource;
        }
    }
    public class ProducerAmountParameters : IResearchParameter
    {
        public ResourceAmount Resource { get; }
        public ProducerAmountParameters(ResourceAmount resource)
        {
            Resource = resource;
        }
    }
    public class ProducerProductParameters : IResearchParameter
    {
        public string ResourceName { get; }
        public ProducerProductParameters(string resource)
        {
            ResourceName = resource;
        }
    }
}
