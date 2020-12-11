using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceManagementGameCore.Resources
{
    public static class ResourceType
    {
        private static HashSet<string> ResourceTypes { get; set; } = new HashSet<string>();
        private static HashSet<string> FoodTypes { get; set; } = new HashSet<string>();
        public static void AddResourceType(string resourceType, bool isFood = false)
        {
            ResourceTypes.Add(resourceType);
            if (isFood)
                FoodTypes.Add(resourceType);
        }
        public static bool IsValidResourceType(string resourceType)
        {
            return ResourceTypes.Contains(resourceType);
        }
        public static bool IsFoodResourceType(string resourceType)
        {
            return FoodTypes.Contains(resourceType);
        }

    }
}
