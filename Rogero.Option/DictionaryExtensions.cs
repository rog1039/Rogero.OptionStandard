using System.Collections.Generic;

namespace Rogero.Options
{
    public static class DictionaryExtensions
    {
        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value)
                ? (Option<TValue>) value
                : Option<TValue>.None;
        }
    }
}