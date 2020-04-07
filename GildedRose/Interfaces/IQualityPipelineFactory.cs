using System;
using System.Collections.Generic;

namespace GildedRose.Interfaces
{
    /// <summary>
    /// Manufactures quality pipelines based on an item configuration
    /// </summary>
    public interface IQualityPipelineFactory
    {
        /// <summary>
        /// Creates the pipeline of quality maintenance elements
        /// </summary>
        /// <param name="item">The item to be inspected</param>
        /// <returns>A pipeline of elements to be run</returns>
        /// <exception cref="ArgumentNullException">The item that was provided was null</exception>
        IList<IQualityAlgorithm> CreatePipeline(IInventoryItem item);
    }
}