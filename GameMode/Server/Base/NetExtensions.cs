#if GTMP
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
#endif
#if RAGEMP
using GTANetworkAPI;
#endif

using GTMPGameMode.Server.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTMPGameMode
{
    public static class Extensions
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static float Radians(float degrees)
        {
            return (float)(degrees * Math.PI / 180.0);
        }

        public static Vector3 Rotate(this Vector3 point, float degrees, float xOffset, float yOffset)
        {
            return new Vector3(
               (float)(point.X + (xOffset * Math.Cos(Radians(degrees))) - (yOffset * Math.Sin(Radians(degrees)))),
               (float)(point.Y + (xOffset * Math.Sin(Radians(degrees))) + (yOffset * Math.Cos(Radians(degrees)))),
               point.Z);
        }

        public static TSVector ToTSVector(this Vector3 pos)
        {
            return new TSVector(pos.X, pos.Y, pos.Z);
        }

        public static Vector3 ExtendSigned(this Vector3 point, Vector3 extensionSize)
        {
            return new Vector3(point.X + (extensionSize.X * Math.Sign(point.X)), point.Y + (extensionSize.Y * Math.Sign(point.Y)), point.Z + (extensionSize.Z * Math.Sign(point.Z)));
        }

        public static Vector3 Extend(this Vector3 point, Vector3 extensionSize)
        {
            return new Vector3(point.X + extensionSize.X, point.Y + (extensionSize.Y), point.Z + (extensionSize.Z));
        }

        private static float GetArea(Vector3 pos0, Vector3 pos1, Vector3 pos2)
        {
            return (float)(0.5) * (pos1.X * pos2.Y - pos1.Y * pos2.X - pos0.X * pos2.Y + pos0.Y * pos2.X + pos0.X * pos1.Y - pos0.Y * pos1.X);
        }

        public static bool IsInShape(this Vector3 point, Vector3 PosA, Vector3 PosB, Vector3 PosC, Vector3 PosD)
        {
            var ABP = GetArea(PosA, PosB, point);
            var BCP = GetArea(PosB, PosC, point);
            var CDP = GetArea(PosC, PosD, point);
            var DAP = GetArea(PosD, PosA, point);
            return (Math.Sign(ABP) == Math.Sign(BCP)) && (Math.Sign(CDP) == Math.Sign(DAP)) && (Math.Sign(ABP) == Math.Sign(CDP));
        }

        public static bool CheckDistanceWithZ(this Vector3 v, Vector3 other, float maxDistance2D, float maxDistanceZ)
        {
            var res = v.DistanceTo2D(other) < maxDistance2D;
            if (res)
            {
                if (v.Z == 0)
                    return res;
                var nZ = v.Z - other.Z;
                return Math.Sqrt(nZ * nZ) < maxDistanceZ;
            }
            return res;
        }


        public static bool HasInterface(this Type t, Type interfaceType)
        {
            return t.GetInterfaces().Contains(interfaceType);
        }



        public static string Elipsis(this string s, int maxChars)
        {
            if (String.IsNullOrEmpty(s) || (s.Length < maxChars))
                return s;
            return s.Substring(0, maxChars) + "...";
        }

        public static int ToLocationID(this Vector3 pos)
        {
            return ((int)(pos.X + 8192) << 16) | ((int)(pos.Y + 8192));
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None);
        }

        public static T FromJSon<T>(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return default;
            return JsonConvert.DeserializeObject<T>(s);
        }

        public static Vector3 FromJSon(this string s)
        {
            if (String.IsNullOrEmpty(s) || (s == "null"))
                return Constants.EmptyVector;
            return JsonConvert.DeserializeObject<Vector3>(s);
        }


        public static T DeserializeAs<T>(this string s, T defaultValue = default)
        {
            if (String.IsNullOrEmpty(s) || (s == "null"))
                return defaultValue;
            return JsonConvert.DeserializeObject<T>(s);
        }

        public static string ToPhoneFormat(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            if (s.Length != 9)
                return s;
            return $"({s.Substring(0, 3)}) {s.Substring(3, 3)}-{s.Substring(6, 3)}";
        }

        public static bool OnlyNumbers(this string s, int minLength, int maxLength)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            if ((s.Length > maxLength) || (s.Length < minLength))
                return false;
            foreach (var item in s)
            {
                if (!Char.IsDigit(item))
                    return false;
            }
            return true;
        }

        public static string UTF8Decode(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;
            return s;
        }

        public static string UrlEncode(this string s)
        {
            return Uri.EscapeDataString(s);
        }

        public static bool IsNullVector(this Vector3 v)
        {
            if (v == null)
                return true;
            return v.Length() == 0;
        }



        public static string ToTeleport(this Vector3 v)
        {
            return $"{v.X} {v.Y} {v.Z}";
        }
        /*
        public static void PlaySound(this Entity target, string soundName, float distanceToHear = 15)
        {
            SoundController.PlaySoundFor(target, soundName, distanceToHear);
        }
        */
        public static string ToPrettyFormat(this TimeSpan span, bool inShort = false)
        {

            if (span == TimeSpan.Zero)
                return "gerade eben";

            var sb = new StringBuilder();
            if (inShort)
            {
                if (span.Days > 0)
                    sb.AppendFormat("{0} Tag ", span.Days, span.Days > 1 ? "e" : String.Empty);
                if (span.Hours > 0)
                    sb.AppendFormat("{0} Std ", span.Hours, span.Hours > 1 ? "n" : String.Empty);
                if (span.Minutes > 0)
                    sb.AppendFormat("{0} Min ", span.Minutes, span.Minutes > 1 ? "n" : String.Empty);
            }
            else
            {
                if (span.Days > 0)
                    sb.AppendFormat("{0} Tag{1} ", span.Days, span.Days > 1 ? "e" : String.Empty);
                if (span.Hours > 0)
                    sb.AppendFormat("{0} Stunde{1} ", span.Hours, span.Hours > 1 ? "n" : String.Empty);
                if (span.Minutes > 0)
                    sb.AppendFormat("{0} Minute{1} ", span.Minutes, span.Minutes > 1 ? "n" : String.Empty);
                if (span.Seconds > 0)
                    sb.AppendFormat("{0} Sekunde{1}", span.Seconds, span.Seconds > 1 ? "n" : "");
            }
            return sb.ToString();

        }

        public static string ToTimespanFormat(this int span, bool inShort = false)
        {
            return TimeSpan.FromMinutes(span).ToPrettyFormat(inShort);
        }

        public static bool IsNumber(this string inStr)
        {
            if (String.IsNullOrEmpty(inStr))
                return false;
            foreach (var item in inStr)
            {
                if (!Char.IsDigit(item))
                    return false;
            }
            return true;
        }

        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {

            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {

                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));
                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    return await task;  // Very important in order to propagate exceptions
                }
                else
                {
                    throw new TimeoutException("The operation has timed out.");
                }
            }
        }

        public static Task DelayedCall<T>(this T obj, int delayMilliseconds, Action<T> act)
        {
            return (new Func<T, Task>((p) => Task.Delay(delayMilliseconds).ContinueWith((t) => { try { act(p); } catch (Exception ex) { logger.Warn(ex.ToString()); } }))).Invoke(obj);
        }



        public static Task DelayedCall<T1, T2>(this T1 obj, T2 arg, int delayMilliseconds, Action<T1, T2> act)
        {
            return (new Func<T1, T2, Task>((p, p1) => Task.Delay(delayMilliseconds).ContinueWith((t) =>
            {
                try
                {
                    act(p, p1);
                }
                catch (Exception ex) { logger.Warn(ex.ToString()); }
            }))).Invoke(obj, arg);
        }
        public static Task DelayedCall<T1, T2, T3>(this T1 obj, T2 arg, T3 arg1, int delayMilliseconds, Action<T1, T2, T3> act)
        {
            return (new Func<T1, T2, T3, Task>((p, p1, p2) => Task.Delay(delayMilliseconds).ContinueWith((t) =>
            {
                try
                {
                    act(p, p1, p2);
                }
                catch (Exception ex) { logger.Warn(ex.ToString()); }
            }))).Invoke(obj, arg, arg1);
        }
        public static Task DelayedCall<T1, T2, T3, T4>(this T1 obj, T2 arg, T3 arg1, T4 arg2, int delayMilliseconds, Action<T1, T2, T3, T4> act)
        {
            return (new Func<T1, T2, T3, T4, Task>((p, p1, p2, p3) => Task.Delay(delayMilliseconds).ContinueWith((t) =>
            {
                try
                {
                    act(p, p1, p2, p3);
                }
                catch (Exception ex) { logger.Warn(ex.ToString()); }
            }))).Invoke(obj, arg, arg1, arg2);
        }
        public static object CastToUnsigned<T>(this T number) where T : struct
        {
            unchecked
            {
                switch (number)
                {
                    case long xlong:
                        return (ulong)xlong;
                    case int xint:
                        return (uint)xint;
                    case short xshort:
                        return (ushort)xshort;
                    case sbyte xsbyte:
                        return (byte)xsbyte;
                }
            }
            return number;
        }
        public static uint CastToUnsignedInt(this int number)
        {
            unchecked
            {
                return (uint)number;
            }
        }
    }

    public static class ListExtensions
    {
        public static List<string> ToStringList<T>(this List<T> inList)
        {
            var result = new List<string>();
            inList?.ForEach(v => result.Add(Convert.ToString(v)));
            return result;
        }

        public static bool IsValidIndex<T>(this List<T> inList, int index)
        {
            if (inList.Count == 0)
                return false;
            return (index < inList.Count) && (index >= 0);
        }
    }

    public static class DictionaryExtensions
    {
        public static T GetData<T>(this Dictionary<string, string> dictionary, string dataName, T defaultValue = default)
        {
            try
            {
                if (dictionary == null)
                    return defaultValue;
                if (!dictionary.ContainsKey(dataName))
                    return defaultValue;

                var tmp = dictionary[dataName];
                if (tmp == null)
                    return defaultValue;
                if (typeof(T).IsPrimitive || (typeof(T) == typeof(String)))
                {
                    return (T)Convert.ChangeType(tmp, typeof(T), CultureInfo.InvariantCulture);
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(tmp);
                }
            }
            catch { return defaultValue; }
        }

        public static void SetData(this Dictionary<string, string> dictionary, string key, object value)
        {
            if (dictionary == null)
                return;

            if (value.GetType().IsPrimitive || (value is String))
                dictionary[key] = Convert.ToString(value, CultureInfo.InvariantCulture);
            else
                dictionary[key] = JsonConvert.SerializeObject(value, Formatting.None);
        }

        public delegate bool Predicate<TKey, TValue>(KeyValuePair<TKey, TValue> d);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void RemoveWhere<TKey, TValue>(
            this Dictionary<TKey, TValue> hashtable, Predicate<TKey, TValue> p)
        {
            foreach (KeyValuePair<TKey, TValue> value in hashtable.ToList().Where(value => p(value)))
                hashtable.Remove(value.Key);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void RemoveWhere<TKey, TValue>(
            this ConcurrentDictionary<TKey, TValue> hashtable, Predicate<TKey, TValue> p)
        {
            foreach (KeyValuePair<TKey, TValue> value in hashtable.ToList().Where(value => p(value)))
                hashtable.Remove(value.Key);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> hashtable, TKey key) where TValue : class
        {
            if (hashtable.TryGetValue(key, out TValue valOut))
                return valOut;
            return null;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> hashtable, TKey key, TValue value) where TValue : class
        {
            if (hashtable.TryGetValue(key, out TValue valOut))
                return valOut;
            hashtable.Add(key, value);
            return value;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, TKey key)
        {
            if ((key == null) || !dict.ContainsKey(key))
                return false;
            dict.TryRemove(key, out TValue value);
            return true;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool Add<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (key == null)
                return false;
            if (dict.ContainsKey(key))
                dict.Remove(key);
            int tries = 0;
            while (!dict.TryAdd(key, value))
            {
                tries++;
                if (tries > 10)
                {
                    //Global.Logger.Warn("ConCurrent add for {0} failed after 10 tries", key);
                    return false;
                }
                Thread.Sleep(100);
            }
            return true;
        }
    }

    public static class HashSetExtensions
    {
        public static HashSet<T> Add<T>(this HashSet<T> hashSet, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                hashSet.Add(item);
            }
            return hashSet;
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, TSource, bool> comparer)
        {
            return enumerable.Distinct(new LambdaComparer<TSource>(comparer));
        }

        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> enumerable, IEnumerable<TSource> other, Func<TSource, TSource, bool> comparer)
        {
            return enumerable.Intersect<TSource>(other, new LambdaComparer<TSource>(comparer));
        }

        public static T GetArg<T>(this IEnumerable<object> args, int index, T defaultValue = default)
        {
            var tmpList = args?.ToList();
            if ((args == null) || (index >= tmpList.Count))
                return defaultValue;
            try
            {
                if (typeof(T).IsEnum)
                    return (T)Enum.Parse(typeof(T), tmpList[index].ToString());
                if (typeof(T) is IConvertible)
                    return (T)Convert.ChangeType(tmpList[index], typeof(T), CultureInfo.InvariantCulture);
                return (T)tmpList[index];
            }
            catch { return defaultValue; }
        }

        public static T GetArg<T>(this object[] args, int index, T defaultValue = default)
        {
            var tmpList = args?.ToList();
            if ((args == null) || (index >= tmpList.Count))
                return defaultValue;
            try
            {
                if (typeof(T).IsEnum)
                    return (T)Enum.Parse(typeof(T), tmpList[index].ToString());
                if (typeof(T) is IConvertible)
                    return (T)Convert.ChangeType(tmpList[index], typeof(T), CultureInfo.InvariantCulture);
                return (T)tmpList[index];
            }
            catch { return defaultValue; }
        }


        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            Contract.Requires(enumerable != null);
            Contract.Requires(action != null);

            if (enumerable is T[])
            {
                ForEach((T[])enumerable, action);
                return;
            }

            if (enumerable is IReadOnlyList<T>)
            {
                ForEach((IReadOnlyList<T>)enumerable, action);
                return;
            }

            if (enumerable is IList<T>)
            {
                ForEach((IList<T>)enumerable, action);
                return;
            }

            foreach (var item in enumerable)
                action(item);
        }

        public static void ForEach<T>(this IReadOnlyList<T> list, Action<T> action)
        {
            Contract.Requires(list != null);
            Contract.Requires(action != null);

            for (int i = 0; i < list.Count; i++)
                action(list[i]);
        }

        private static void ForEach<T>(this IList<T> list, Action<T> action)
        {
            Contract.Requires(list != null);
            Contract.Requires(action != null);

            for (int i = 0; i < list.Count; i++)
                action(list[i]);
        }

        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            Contract.Requires(array != null);
            Contract.Requires(action != null);

            for (int i = 0; i < array.Length; i++)
                action(array[i]);
        }
    }

    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _lambdaComparer;
        private readonly Func<T, int> _lambdaHash;

        public LambdaComparer(Func<T, T, bool> lambdaComparer) :
            this(lambdaComparer, o => 0)
        {
        }

        public LambdaComparer(Func<T, T, bool> lambdaComparer, Func<T, int> lambdaHash)
        {
            _lambdaComparer = lambdaComparer ?? throw new ArgumentNullException("lambdaComparer");
            _lambdaHash = lambdaHash ?? throw new ArgumentNullException("lambdaHash");
        }

        public bool Equals(T x, T y)
        {
            return _lambdaComparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _lambdaHash(obj);
        }
    }

    /// <summary>
    /// Special JsonConvert resolver that allows you to ignore properties.  See https://stackoverflow.com/a/13588192/1037948
    /// </summary>
    public class IgnorableSerializerContractResolver : DefaultContractResolver
    {
        protected readonly Dictionary<Type, HashSet<string>> Ignores;

        public IgnorableSerializerContractResolver()
        {
            this.Ignores = new Dictionary<Type, HashSet<string>>();
        }

        /// <summary>
        /// Explicitly ignore the given property(s) for the given type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName">one or more properties to ignore.  Leave empty to ignore the type entirely.</param>
        public void Ignore(Type type, params string[] propertyName)
        {
            // start bucket if DNE
            if (!this.Ignores.ContainsKey(type))
                this.Ignores[type] = new HashSet<string>();

            foreach (var prop in propertyName)
            {
                this.Ignores[type].Add(prop);
            }
        }

        /// <summary>
        /// Is the given property for the given type ignored?
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public bool IsIgnored(Type type, string propertyName)
        {
            if (!this.Ignores.ContainsKey(type))
                return false;

            // if no properties provided, ignore the type entirely
            if (this.Ignores[type].Count == 0)
                return true;

            return this.Ignores[type].Contains(propertyName);
        }

        /// <summary>
        /// The decision logic goes here
        /// </summary>
        /// <param name="member"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (this.IsIgnored(property.DeclaringType, property.PropertyName))
            {
                property.ShouldSerialize = instance => { return false; };
            }

            return property;
        }
    }
}




namespace System
{
    public delegate void EventHandler<TSender, TEventArgs>(TSender sender, TEventArgs e);
}

namespace ConcurrentCollections
{
    /// <summary>
    /// Represents a thread-safe hash-based unique collection.
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection.</typeparam>
    /// <remarks>
    /// All public members of <see cref="ConcurrentHashSet{T}"/> are thread-safe and may be used
    /// concurrently from multiple threads.
    /// </remarks>
    [DebuggerDisplay("Count = {Count}")]
    public class ConcurrentHashSet<T> : IReadOnlyCollection<T>, ICollection<T>
    {
        private const int DefaultCapacity = 31;
        private const int MaxLockNumber = 1024;

        private readonly IEqualityComparer<T> _comparer;
        private readonly bool _growLockArray;

        private int _budget;
        private volatile Tables _tables;

        private static int DefaultConcurrencyLevel => 4;

        /// <summary>
        /// Gets the number of items contained in the <see
        /// cref="ConcurrentHashSet{T}"/>.
        /// </summary>
        /// <value>The number of items contained in the <see
        /// cref="ConcurrentHashSet{T}"/>.</value>
        /// <remarks>Count has snapshot semantics and represents the number of items in the <see
        /// cref="ConcurrentHashSet{T}"/>
        /// at the moment when Count was accessed.</remarks>
        public int Count
        {
            get
            {
                var count = 0;
                var acquiredLocks = 0;
                try
                {
                    AcquireAllLocks(ref acquiredLocks);

                    for (var i = 0; i < _tables.CountPerLock.Length; i++)
                    {
                        count += _tables.CountPerLock[i];
                    }
                }
                finally
                {
                    ReleaseLocks(0, acquiredLocks);
                }

                return count;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the <see cref="ConcurrentHashSet{T}"/> is empty.
        /// </summary>
        /// <value>true if the <see cref="ConcurrentHashSet{T}"/> is empty; otherwise,
        /// false.</value>
        public bool IsEmpty
        {
            get
            {
                var acquiredLocks = 0;
                try
                {
                    AcquireAllLocks(ref acquiredLocks);

                    for (var i = 0; i < _tables.CountPerLock.Length; i++)
                    {
                        if (_tables.CountPerLock[i] != 0)
                        {
                            return false;
                        }
                    }
                }
                finally
                {
                    ReleaseLocks(0, acquiredLocks);
                }

                return true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="ConcurrentHashSet{T}"/>
        /// class that is empty, has the default concurrency level, has the default initial capacity, and
        /// uses the default comparer for the item type.
        /// </summary>
        public ConcurrentHashSet()
            : this(DefaultConcurrencyLevel, DefaultCapacity, true, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="ConcurrentHashSet{T}"/>
        /// class that is empty, has the specified concurrency level and capacity, and uses the default
        /// comparer for the item type.
        /// </summary>
        /// <param name="concurrencyLevel">The estimated number of threads that will update the
        /// <see cref="ConcurrentHashSet{T}"/> concurrently.</param>
        /// <param name="capacity">The initial number of elements that the <see
        /// cref="ConcurrentHashSet{T}"/>
        /// can contain.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="concurrencyLevel"/> is
        /// less than 1.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"> <paramref name="capacity"/> is less than
        /// 0.</exception>
        public ConcurrentHashSet(int concurrencyLevel, int capacity)
            : this(concurrencyLevel, capacity, false, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentHashSet{T}"/>
        /// class that contains elements copied from the specified <see
        /// cref="T:System.Collections.IEnumerable{T}"/>, has the default concurrency
        /// level, has the default initial capacity, and uses the default comparer for the item type.
        /// </summary>
        /// <param name="collection">The <see
        /// cref="T:System.Collections.IEnumerable{T}"/> whose elements are copied to
        /// the new
        /// <see cref="ConcurrentHashSet{T}"/>.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="collection"/> is a null reference.</exception>
        public ConcurrentHashSet(IEnumerable<T> collection)
            : this(collection, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentHashSet{T}"/>
        /// class that is empty, has the specified concurrency level and capacity, and uses the specified
        /// <see cref="T:System.Collections.Generic.IEqualityComparer{T}"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer{T}"/>
        /// implementation to use when comparing items.</param>
        public ConcurrentHashSet(IEqualityComparer<T> comparer)
            : this(DefaultConcurrencyLevel, DefaultCapacity, true, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentHashSet{T}"/>
        /// class that contains elements copied from the specified <see
        /// cref="T:System.Collections.IEnumerable"/>, has the default concurrency level, has the default
        /// initial capacity, and uses the specified
        /// <see cref="T:System.Collections.Generic.IEqualityComparer{T}"/>.
        /// </summary>
        /// <param name="collection">The <see
        /// cref="T:System.Collections.IEnumerable{T}"/> whose elements are copied to
        /// the new
        /// <see cref="ConcurrentHashSet{T}"/>.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer{T}"/>
        /// implementation to use when comparing items.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="collection"/> is a null reference
        /// (Nothing in Visual Basic).
        /// </exception>
        public ConcurrentHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
            : this(comparer)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            InitializeFromCollection(collection);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentHashSet{T}"/> 
        /// class that contains elements copied from the specified <see cref="T:System.Collections.IEnumerable"/>, 
        /// has the specified concurrency level, has the specified initial capacity, and uses the specified 
        /// <see cref="T:System.Collections.Generic.IEqualityComparer{T}"/>.
        /// </summary>
        /// <param name="concurrencyLevel">The estimated number of threads that will update the 
        /// <see cref="ConcurrentHashSet{T}"/> concurrently.</param>
        /// <param name="collection">The <see cref="T:System.Collections.IEnumerable{T}"/> whose elements are copied to the new 
        /// <see cref="ConcurrentHashSet{T}"/>.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer{T}"/> implementation to use 
        /// when comparing items.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="collection"/> is a null reference.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="concurrencyLevel"/> is less than 1.
        /// </exception>
        public ConcurrentHashSet(int concurrencyLevel, IEnumerable<T> collection, IEqualityComparer<T> comparer)
            : this(concurrencyLevel, DefaultCapacity, false, comparer)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            InitializeFromCollection(collection);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentHashSet{T}"/>
        /// class that is empty, has the specified concurrency level, has the specified initial capacity, and
        /// uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer{T}"/>.
        /// </summary>
        /// <param name="concurrencyLevel">The estimated number of threads that will update the
        /// <see cref="ConcurrentHashSet{T}"/> concurrently.</param>
        /// <param name="capacity">The initial number of elements that the <see
        /// cref="ConcurrentHashSet{T}"/>
        /// can contain.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer{T}"/>
        /// implementation to use when comparing items.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="concurrencyLevel"/> is less than 1. -or-
        /// <paramref name="capacity"/> is less than 0.
        /// </exception>
        public ConcurrentHashSet(int concurrencyLevel, int capacity, IEqualityComparer<T> comparer)
            : this(concurrencyLevel, capacity, false, comparer)
        {
        }

        private ConcurrentHashSet(int concurrencyLevel, int capacity, bool growLockArray, IEqualityComparer<T> comparer)
        {
            if (concurrencyLevel < 1)
                throw new ArgumentOutOfRangeException(nameof(concurrencyLevel));
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            // The capacity should be at least as large as the concurrency level. Otherwise, we would have locks that don't guard
            // any buckets.
            if (capacity < concurrencyLevel)
            {
                capacity = concurrencyLevel;
            }

            var locks = new object[concurrencyLevel];
            for (var i = 0; i < locks.Length; i++)
            {
                locks[i] = new object();
            }

            var countPerLock = new int[locks.Length];
            var buckets = new Node[capacity];
            _tables = new Tables(buckets, locks, countPerLock);

            _growLockArray = growLockArray;
            _budget = buckets.Length / locks.Length;
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        /// <summary>
        /// Adds the specified item to the <see cref="ConcurrentHashSet{T}"/>.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>true if the items was added to the <see cref="ConcurrentHashSet{T}"/>
        /// successfully; false if it already exists.</returns>
        /// <exception cref="T:System.OverflowException">The <see cref="ConcurrentHashSet{T}"/>
        /// contains too many items.</exception>
        public bool Add(T item) =>
            AddInternal(item, _comparer.GetHashCode(item), true);

        /// <summary>
        /// Removes all items from the <see cref="ConcurrentHashSet{T}"/>.
        /// </summary>
        public void Clear()
        {
            var locksAcquired = 0;
            try
            {
                AcquireAllLocks(ref locksAcquired);

                var newTables = new Tables(new Node[DefaultCapacity], _tables.Locks, new int[_tables.CountPerLock.Length]);
                _tables = newTables;
                _budget = Math.Max(1, newTables.Buckets.Length / newTables.Locks.Length);
            }
            finally
            {
                ReleaseLocks(0, locksAcquired);
            }
        }

        /// <summary>
        /// Determines whether the <see cref="ConcurrentHashSet{T}"/> contains the specified
        /// item.
        /// </summary>
        /// <param name="item">The item to locate in the <see cref="ConcurrentHashSet{T}"/>.</param>
        /// <returns>true if the <see cref="ConcurrentHashSet{T}"/> contains the item; otherwise, false.</returns>
        public bool Contains(T item)
        {
            var hashcode = _comparer.GetHashCode(item);

            // We must capture the _buckets field in a local variable. It is set to a new table on each table resize.
            var tables = _tables;

            var bucketNo = GetBucket(hashcode, tables.Buckets.Length);

            // We can get away w/out a lock here.
            // The Volatile.Read ensures that the load of the fields of 'n' doesn't move before the load from buckets[i].
            var current = Volatile.Read(ref tables.Buckets[bucketNo]);

            while (current != null)
            {
                if (hashcode == current.Hashcode && _comparer.Equals(current.Item, item))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        /// <summary>
        /// Attempts to remove the item from the <see cref="ConcurrentHashSet{T}"/>.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>true if an item was removed successfully; otherwise, false.</returns>
        public bool TryRemove(T item)
        {
            var hashcode = _comparer.GetHashCode(item);
            while (true)
            {
                var tables = _tables;

                GetBucketAndLockNo(hashcode, out int bucketNo, out int lockNo, tables.Buckets.Length, tables.Locks.Length);

                lock (tables.Locks[lockNo])
                {
                    // If the table just got resized, we may not be holding the right lock, and must retry.
                    // This should be a rare occurrence.
                    if (tables != _tables)
                    {
                        continue;
                    }

                    Node previous = null;
                    for (var current = tables.Buckets[bucketNo]; current != null; current = current.Next)
                    {
                        Debug.Assert((previous == null && current == tables.Buckets[bucketNo]) || previous.Next == current);

                        if (hashcode == current.Hashcode && _comparer.Equals(current.Item, item))
                        {
                            if (previous == null)
                            {
                                Volatile.Write(ref tables.Buckets[bucketNo], current.Next);
                            }
                            else
                            {
                                previous.Next = current.Next;
                            }

                            tables.CountPerLock[lockNo]--;
                            return true;
                        }
                        previous = current;
                    }
                }

                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Returns an enumerator that iterates through the <see
        /// cref="ConcurrentHashSet{T}"/>.</summary>
        /// <returns>An enumerator for the <see cref="ConcurrentHashSet{T}"/>.</returns>
        /// <remarks>
        /// The enumerator returned from the collection is safe to use concurrently with
        /// reads and writes to the collection, however it does not represent a moment-in-time snapshot
        /// of the collection.  The contents exposed through the enumerator may contain modifications
        /// made to the collection after <see cref="GetEnumerator"/> was called.
        /// </remarks>
        public IEnumerator<T> GetEnumerator()
        {
            var buckets = _tables.Buckets;

            for (var i = 0; i < buckets.Length; i++)
            {
                // The Volatile.Read ensures that the load of the fields of 'current' doesn't move before the load from buckets[i].
                var current = Volatile.Read(ref buckets[i]);

                while (current != null)
                {
                    yield return current.Item;
                    current = current.Next;
                }
            }
        }

        void ICollection<T>.Add(T item) => Add(item);

        bool ICollection<T>.IsReadOnly => false;

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            var locksAcquired = 0;
            try
            {
                AcquireAllLocks(ref locksAcquired);

                var count = 0;

                for (var i = 0; i < _tables.Locks.Length && count >= 0; i++)
                {
                    count += _tables.CountPerLock[i];
                }

                if (array.Length - count < arrayIndex || count < 0) //"count" itself or "count + arrayIndex" can overflow
                {
                    throw new ArgumentException("The index is equal to or greater than the length of the array, or the number of elements in the set is greater than the available space from index to the end of the destination array.");
                }

                CopyToItems(array, arrayIndex);
            }
            finally
            {
                ReleaseLocks(0, locksAcquired);
            }
        }

        bool ICollection<T>.Remove(T item) => TryRemove(item);

        private void InitializeFromCollection(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                AddInternal(item, _comparer.GetHashCode(item), false);
            }

            if (_budget == 0)
            {
                _budget = _tables.Buckets.Length / _tables.Locks.Length;
            }
        }

        private bool AddInternal(T item, int hashcode, bool acquireLock)
        {
            while (true)
            {
                var tables = _tables;

                GetBucketAndLockNo(hashcode, out int bucketNo, out int lockNo, tables.Buckets.Length, tables.Locks.Length);

                var resizeDesired = false;
                var lockTaken = false;
                try
                {
                    if (acquireLock)
                        Monitor.Enter(tables.Locks[lockNo], ref lockTaken);

                    // If the table just got resized, we may not be holding the right lock, and must retry.
                    // This should be a rare occurrence.
                    if (tables != _tables)
                    {
                        continue;
                    }

                    // Try to find this item in the bucket
                    Node previous = null;
                    for (var current = tables.Buckets[bucketNo]; current != null; current = current.Next)
                    {
                        Debug.Assert(previous == null && current == tables.Buckets[bucketNo] || previous.Next == current);
                        if (hashcode == current.Hashcode && _comparer.Equals(current.Item, item))
                        {
                            return false;
                        }
                        previous = current;
                    }

                    // The item was not found in the bucket. Insert the new item.
                    Volatile.Write(ref tables.Buckets[bucketNo], new Node(item, hashcode, tables.Buckets[bucketNo]));
                    checked
                    {
                        tables.CountPerLock[lockNo]++;
                    }

                    //
                    // If the number of elements guarded by this lock has exceeded the budget, resize the bucket table.
                    // It is also possible that GrowTable will increase the budget but won't resize the bucket table.
                    // That happens if the bucket table is found to be poorly utilized due to a bad hash function.
                    //
                    if (tables.CountPerLock[lockNo] > _budget)
                    {
                        resizeDesired = true;
                    }
                }
                finally
                {
                    if (lockTaken)
                        Monitor.Exit(tables.Locks[lockNo]);
                }

                //
                // The fact that we got here means that we just performed an insertion. If necessary, we will grow the table.
                //
                // Concurrency notes:
                // - Notice that we are not holding any locks at when calling GrowTable. This is necessary to prevent deadlocks.
                // - As a result, it is possible that GrowTable will be called unnecessarily. But, GrowTable will obtain lock 0
                //   and then verify that the table we passed to it as the argument is still the current table.
                //
                if (resizeDesired)
                {
                    GrowTable(tables);
                }

                return true;
            }
        }

        private static int GetBucket(int hashcode, int bucketCount)
        {
            var bucketNo = (hashcode & 0x7fffffff) % bucketCount;
            Debug.Assert(bucketNo >= 0 && bucketNo < bucketCount);
            return bucketNo;
        }

        private static void GetBucketAndLockNo(int hashcode, out int bucketNo, out int lockNo, int bucketCount, int lockCount)
        {
            bucketNo = (hashcode & 0x7fffffff) % bucketCount;
            lockNo = bucketNo % lockCount;

            Debug.Assert(bucketNo >= 0 && bucketNo < bucketCount);
            Debug.Assert(lockNo >= 0 && lockNo < lockCount);
        }

        private void GrowTable(Tables tables)
        {
            const int maxArrayLength = 0X7FEFFFFF;
            var locksAcquired = 0;
            try
            {
                // The thread that first obtains _locks[0] will be the one doing the resize operation
                AcquireLocks(0, 1, ref locksAcquired);

                // Make sure nobody resized the table while we were waiting for lock 0:
                if (tables != _tables)
                {
                    // We assume that since the table reference is different, it was already resized (or the budget
                    // was adjusted). If we ever decide to do table shrinking, or replace the table for other reasons,
                    // we will have to revisit this logic.
                    return;
                }

                // Compute the (approx.) total size. Use an Int64 accumulation variable to avoid an overflow.
                long approxCount = 0;
                for (var i = 0; i < tables.CountPerLock.Length; i++)
                {
                    approxCount += tables.CountPerLock[i];
                }

                //
                // If the bucket array is too empty, double the budget instead of resizing the table
                //
                if (approxCount < tables.Buckets.Length / 4)
                {
                    _budget = 2 * _budget;
                    if (_budget < 0)
                    {
                        _budget = int.MaxValue;
                    }
                    return;
                }

                // Compute the new table size. We find the smallest integer larger than twice the previous table size, and not divisible by
                // 2,3,5 or 7. We can consider a different table-sizing policy in the future.
                var newLength = 0;
                var maximizeTableSize = false;
                try
                {
                    checked
                    {
                        // Double the size of the buckets table and add one, so that we have an odd integer.
                        newLength = tables.Buckets.Length * 2 + 1;

                        // Now, we only need to check odd integers, and find the first that is not divisible
                        // by 3, 5 or 7.
                        while (newLength % 3 == 0 || newLength % 5 == 0 || newLength % 7 == 0)
                        {
                            newLength += 2;
                        }

                        Debug.Assert(newLength % 2 != 0);

                        if (newLength > maxArrayLength)
                        {
                            maximizeTableSize = true;
                        }
                    }
                }
                catch (OverflowException)
                {
                    maximizeTableSize = true;
                }

                if (maximizeTableSize)
                {
                    newLength = maxArrayLength;

                    // We want to make sure that GrowTable will not be called again, since table is at the maximum size.
                    // To achieve that, we set the budget to int.MaxValue.
                    //
                    // (There is one special case that would allow GrowTable() to be called in the future: 
                    // calling Clear() on the ConcurrentHashSet will shrink the table and lower the budget.)
                    _budget = int.MaxValue;
                }

                // Now acquire all other locks for the table
                AcquireLocks(1, tables.Locks.Length, ref locksAcquired);

                var newLocks = tables.Locks;

                // Add more locks
                if (_growLockArray && tables.Locks.Length < MaxLockNumber)
                {
                    newLocks = new object[tables.Locks.Length * 2];
                    Array.Copy(tables.Locks, 0, newLocks, 0, tables.Locks.Length);
                    for (var i = tables.Locks.Length; i < newLocks.Length; i++)
                    {
                        newLocks[i] = new object();
                    }
                }

                var newBuckets = new Node[newLength];
                var newCountPerLock = new int[newLocks.Length];

                // Copy all data into a new table, creating new nodes for all elements
                for (var i = 0; i < tables.Buckets.Length; i++)
                {
                    var current = tables.Buckets[i];
                    while (current != null)
                    {
                        var next = current.Next;
                        GetBucketAndLockNo(current.Hashcode, out int newBucketNo, out int newLockNo, newBuckets.Length, newLocks.Length);

                        newBuckets[newBucketNo] = new Node(current.Item, current.Hashcode, newBuckets[newBucketNo]);

                        checked
                        {
                            newCountPerLock[newLockNo]++;
                        }

                        current = next;
                    }
                }

                // Adjust the budget
                _budget = Math.Max(1, newBuckets.Length / newLocks.Length);

                // Replace tables with the new versions
                _tables = new Tables(newBuckets, newLocks, newCountPerLock);
            }
            finally
            {
                // Release all locks that we took earlier
                ReleaseLocks(0, locksAcquired);
            }
        }

        private void AcquireAllLocks(ref int locksAcquired)
        {
            // First, acquire lock 0
            AcquireLocks(0, 1, ref locksAcquired);

            // Now that we have lock 0, the _locks array will not change (i.e., grow),
            // and so we can safely read _locks.Length.
            AcquireLocks(1, _tables.Locks.Length, ref locksAcquired);
            Debug.Assert(locksAcquired == _tables.Locks.Length);
        }

        private void AcquireLocks(int fromInclusive, int toExclusive, ref int locksAcquired)
        {
            Debug.Assert(fromInclusive <= toExclusive);
            var locks = _tables.Locks;

            for (var i = fromInclusive; i < toExclusive; i++)
            {
                var lockTaken = false;
                try
                {
                    Monitor.Enter(locks[i], ref lockTaken);
                }
                finally
                {
                    if (lockTaken)
                    {
                        locksAcquired++;
                    }
                }
            }
        }

        private void ReleaseLocks(int fromInclusive, int toExclusive)
        {
            Debug.Assert(fromInclusive <= toExclusive);

            for (var i = fromInclusive; i < toExclusive; i++)
            {
                Monitor.Exit(_tables.Locks[i]);
            }
        }

        private void CopyToItems(T[] array, int index)
        {
            var buckets = _tables.Buckets;
            for (var i = 0; i < buckets.Length; i++)
            {
                for (var current = buckets[i]; current != null; current = current.Next)
                {
                    array[index] = current.Item;
                    index++; //this should never flow, CopyToItems is only called when there's no overflow risk
                }
            }
        }

       
        private class Tables
        {
            public readonly Node[] Buckets;
            public readonly object[] Locks;

            public volatile int[] CountPerLock;

            public Tables(Node[] buckets, object[] locks, int[] countPerLock)
            {
                Buckets = buckets;
                Locks = locks;
                CountPerLock = countPerLock;
            }
        }

        private class Node
        {
            public readonly T Item;
            public readonly int Hashcode;

            public volatile Node Next;

            public Node(T item, int hashcode, Node next)
            {
                Item = item;
                Hashcode = hashcode;
                Next = next;
            }
        }
    }
}
