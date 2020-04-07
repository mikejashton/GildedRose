using GildedRose.Interfaces;

namespace GildedRose.StrategyFactories.Quality.Strategies
{
    /// <summary>
    /// Steadily decreases the quality of a product until it reaches zero
    /// </summary>
    public class LinearDecreaseAlgorithm : IQualityAlgorithm
    {
        /// <summary>
        /// Runs the algorithm
        /// </summary>
        /// <param name="qualityMaintainer">The item to be maintained</param>
        public void Run(IQualityMaintenance qualityMaintainer)
        {
            if (qualityMaintainer.Quality > 0)
            {
                qualityMaintainer.Quality--;
            }
        }
    }
}