using System;
using System.Collections.Generic;
using GildedRose.Exceptions;
using GildedRose.Interfaces;

namespace GildedRose.StrategyFactories.ShelfLife
{
    /// <summary>
    /// Responsible for creating shelf life strategies
    /// </summary>
    internal class ShelfLifeAlgorithmFactory : IShelfLifeAlgorithmFactory
    {
        private readonly GenericFactory<ShelfLifeStrategy, IShelfLifeAlgorithm> _genericFactory;
        
        /// <summary>
        /// Initialises the factory with a lookup list
        /// </summary>
        /// <param name="typeMap">The lookup list</param>
        public ShelfLifeAlgorithmFactory(IDictionary<ShelfLifeStrategy, Type> typeMap)
        {
            _genericFactory = new GenericFactory<ShelfLifeStrategy, IShelfLifeAlgorithm>( typeMap );
        }

        /// <summary>
        /// Creates a new algorithm based on the quality strategy provided
        /// </summary>
        /// <param name="strategyType">The strategy</param>
        /// <exception cref="ArgumentException">Indicates that the strategy type that was provided was invalid</exception>
        /// <exception cref="GildedRoseException">Indicates that the lookup list that was provided in the constructor
        /// was invalid</exception>
        /// <returns>The quality algorithm</returns>
        public IShelfLifeAlgorithm Create(ShelfLifeStrategy strategyType)
        {
            try
            {
                return _genericFactory.Create(strategyType);
            }
            catch (ArgumentException ex )
            {
                // This exception means that the shelf life strategy did not exist in the lookup provided.
                // We don't rethrow here because the parameter name would make no sense to the caller.
                throw new ArgumentException( nameof( strategyType), ex);
            }
            catch (InvalidCastException)
            {
                // This means the type specified in the lookup list is not compatible with our return type
                // TODO: This should really be a custom exception type
                throw new GildedRoseException( $"The lookup list item relating to '{strategyType}' was not compatible" +
                                               $"with the IShelfLifeAlgorithm return type.");
            }
        }
    }
}