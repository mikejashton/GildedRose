using GildedRose.Interfaces;

namespace GildedRose.StrategyFactories.Quality.Strategies
{
    /// <summary>
    /// Prevents the quality from ever going above a specified value
    /// </summary>
    public class QualityNeverAboveThresholdAlgorithm : IQualityAlgorithm
    {
        private readonly int _threshold;

        /// <summary>
        /// Prevents the quality from ever going above the specified value
        /// </summary>
        /// <param name="threshold">The maximum value</param>
        public QualityNeverAboveThresholdAlgorithm(int threshold)
        {
            _threshold = threshold;
        }
        
        /// <summary>
        /// Runs the algorithm
        /// </summary>
        /// <param name="item">The item to be maintained</param>
        /// <param name="qualityMaintainer">The item to be maintained</param>
        public void Run(IInventoryItem item, IQualityMaintenance qualityMaintainer)
        {
            if (qualityMaintainer.Quality > _threshold)
            {
                qualityMaintainer.Quality = _threshold;
            }
        }
    }
}