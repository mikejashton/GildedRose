using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using GildedRose.Exceptions;
using GildedRose.Interfaces;

namespace GildedRose.StrategyFactories
{
    /// <summary>
    /// Returns a list of quality pipeline operations
    /// </summary>
    public class QualityPipelineFactory : IQualityPipelineFactory
    {
        private readonly IQualityAlgorithmFactory _strategyFactory;
        private readonly IList<IQualityAlgorithm> _qualityControlAlgorithms;

        /// <summary>
        /// Initialises the factory
        /// </summary>
        /// <param name="strategyFactory">The factory that will create the necessary element</param>
        /// <param name="qualityControlAlgorithms">A list of strategies that must always be present to ensure correct behaviour</param>
        public QualityPipelineFactory( IQualityAlgorithmFactory strategyFactory,
            IList<IQualityAlgorithm> qualityControlAlgorithms = null )
        {
            _strategyFactory = strategyFactory ?? throw new ArgumentNullException( nameof( strategyFactory ));
            _qualityControlAlgorithms = qualityControlAlgorithms ?? new List<IQualityAlgorithm>();
        }
        
        /// <summary>
        /// Creates the pipeline of quality maintenance elements
        /// </summary>
        /// <param name="item">The item to be inspected</param>
        /// <returns>A pipeline of elements to be run</returns>
        /// <exception cref="ArgumentNullException">The item that was provided was null</exception>
        public IList<IQualityAlgorithm> CreatePipeline(IInventoryItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException( nameof( item ));
            }
            
            try
            {
                // Extract the correct algorithm to deal with this class of product
                var algorithm = _strategyFactory.Create(item.QualityStrategy);

                // Add the quality control elements at the end
                var returnValue = new List<IQualityAlgorithm>() {algorithm};
                returnValue.AddRange(_qualityControlAlgorithms);

                return returnValue;
            }
            catch (ArgumentException)
            {
                // This indicates that the factory has not been configured properly.
               throw new GildedRoseException( "The IQualityAlgorithmFactory is improperly configured");
            }
            catch (GildedRoseException)
            {
                // This should be a custom exception really that derived from GildedRoseException
                // The type specified in the item is not supported by this factory
                // TODO: Log
                throw;
            }
        }
    }
}