using GildedRose.Interfaces;

namespace GildedRose.StrategyFactories.Quality.Strategies
{
    /// <summary>
    /// Quality decreases by twice the value of the LinearDecreaseAlgorithm
    /// </summary>
    public class RapidDecreaseAlgorithm : IQualityAlgorithm
    {
        /// <summary>
        /// Runs the algorithm
        /// </summary>
        /// <param name="item">The item to be maintained</param>
        /// <param name="qualityMaintainer">The item to be maintained</param>
        public void Run(IInventoryItem item, IQualityMaintenance qualityMaintainer)
        {
            if (qualityMaintainer.Quality > 0)
            {
                // I am not happy with the use of a static class here because it can hamper unit testing. This should
                // ideally be injected
                qualityMaintainer.Quality-= QualityAssessor.CalculateDeteriorationRate( item ) * 2;
            }
        }
    }
}