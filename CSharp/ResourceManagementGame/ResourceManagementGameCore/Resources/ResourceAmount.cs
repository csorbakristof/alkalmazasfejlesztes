using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Resources
{
    public class ResourceAmount
    {
        public string Type { get; protected set; }
        public int Amount { get; set; }
        public ResourceAmount(string resourceType, int amount)
        {
                if (!ResourceType.IsValidResourceType(resourceType))
                    throw new Exception($"Invalid resource type: {resourceType}");

            Type = resourceType;
            Amount = amount;
        }
    }
}
