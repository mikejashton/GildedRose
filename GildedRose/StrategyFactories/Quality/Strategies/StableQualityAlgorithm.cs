using GildedRose.Interfaces;

namespace GildedRose.StrategyFactories.Quality.Strategies
{
    /// <summary>
    /// Implements the requirement that the quality of some items should never change
    /// </summary>
    public class StableQualityAlgorithm : IQualityAlgorithm
    {
        /// <summary>
        /// Runs the algorithm
        /// </summary>
        /// <param name="qualityMaintainer">The object to be maintained</param>
        public void Run(IQualityMaintenance qualityMaintainer)
        {
            // We don't do anything, because the value needs to remain stable.
        }
    }
}
