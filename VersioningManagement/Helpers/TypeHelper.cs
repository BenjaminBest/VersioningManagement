using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VersioningManagement.Helpers
{
    /// <summary>
    /// This class provides helper methods for handling with types
    /// </summary>
    public static class TypeHelper
    {
        public static List<Type> FindNonAbstractTypes(Type type)
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(p => p.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type)).ToList();
        }
    }
}
