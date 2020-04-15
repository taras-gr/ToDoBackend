using System;
using System.Collections.Generic;

namespace ToDo.IoC
{
    public static class IoC
    {
        private static readonly Dictionary<Type, Type> Dependencies = new Dictionary<Type, Type>();
        public static void Register(Type abstraction, Type implementation)
        {
            Dependencies.Add(abstraction, implementation);
        }
        public static Dictionary<Type, Type> GetDependencies()
        {
            return Dependencies;
        }
    }
}