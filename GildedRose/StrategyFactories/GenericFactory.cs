using System;
using System.Collections.Generic;

namespace GildedRose.StrategyFactories
{
    /// <summary>
    /// Implementation of a factory class that dynamically creates the correct type based on a key provided by
    /// the caller.
    /// </summary>
    /// <typeparam name="_TKey">The key type to be used</typeparam>
    /// <typeparam name="TValue">The type of object that will be created</typeparam>
    internal class GenericFactory<_TKey, TValue> where TValue : class
    {
        private readonly IDictionary<_TKey, Type> _typeMap;

        /// <summary>
        /// Initialises the generic factory by providing it with a lookup from key to Type
        /// </summary>
        /// <param name="typeMap">The type map to be used</param>
        public GenericFactory(IDictionary<_TKey, Type> typeMap)
        {
            _typeMap = typeMap;
        }

        /// <summary>
        /// Creates a new object
        /// </summary>
        /// <param name="key">The key associated with the type</param>
        /// <returns>The newly created type</returns>
        /// <exception cref="ArgumentException">The key was not found in the dictionary</exception>
        /// <exception cref="InvalidCastException">The object could not be cast to <c>_Value</c></exception>
        public TValue Create(_TKey key)
        {
            if ( _typeMap.TryGetValue(key, out var type) )
            {
                return (TValue)Activator.CreateInstance(type);
            }
            
            throw new ArgumentException("The key was not found in the map");
        }
    }
}