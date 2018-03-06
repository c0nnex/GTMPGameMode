using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GTMPGameMode.Base
{
    public static class NetExtensions
    {
        public static bool HasInterface(this Type t, Type interfaceType)
        {
            return t.GetInterfaces().Contains(interfaceType);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> hashtable, TKey key, TValue value) where TValue : class
        {
            TValue valOut;
            if (hashtable.TryGetValue(key, out valOut))
                return valOut;
            hashtable.Add(key, value);
            return value;
        }
    }
}
