using GildedRose.Interfaces;

namespace GildedRose.StrategyFactories.Quality.Strategies
{
    /// <summary>
    /// Prevents the quality from ever becoming negative
    /// </summary>
    public class QualityNeverNegativeAlgorithm : IQualityAlgorithm
    {
        /// <summary>
        /// Runs the algorithm
        /// </summary>
        /// <param name="item">The item to be maintained</param>
        /// <param name="qualityMaintainer">The item to be maintained</param>
        public void Run(IInventoryItem item, IQualityMaintenance qualityMaintainer)
        {
            if (qualityMaintainer.Quality < 0)
            {
                qualityMaintainer.Quality = 0;
            }
        }
    }
}