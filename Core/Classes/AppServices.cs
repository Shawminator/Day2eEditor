using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public static class AppServices
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static void Register<T>(T service)
        {
            _services[typeof(T)] = service!;
        }

        public static T? Get<T>()
        {
            return _services.TryGetValue(typeof(T), out var service) ? (T)service : default;
        }
    }
}
