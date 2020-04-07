using GildedRose.Interfaces;

namespace GildedRose.StrategyFactories.Quality.Strategies
{
    /// <summary>
    /// The quality steadily increases as the sell by approaches, and become zero after the sell by has passed
    /// </summary>
    public class IncreasingValueUntilSellByAlgorithm : IQualityAlgorithm
    {
        private readonly int FirstThreshold = 10;
        private readonly int SecondThreshold = 4;
        
        private readonly int FirstQualityIncrease = 1;
        private readonly int SecondQualityIncrease = 2;
        private readonly int ThirdQualityIncrease = 5;

        /// <summary>
        /// Runs the algorithm
        /// </summary>
        /// <param name="item">The item to be maintained</param>
        /// <param name="qualityMaintainer">The item to be maintained</param>
        public void Run(IInventoryItem item, IQualityMaintenance qualityMaintainer)
        {
            var daysRemaining = item.SellIn;
            if (daysRemaining > FirstThreshold)
            {
                qualityMaintainer.Quality += FirstQualityIncrease;
            }
            else if (daysRemaining >= SecondThreshold && daysRemaining <= FirstThreshold)
            {
                qualityMaintainer.Quality += SecondQualityIncrease;
            }
            else if (daysRemaining >= 0 && daysRemaining < SecondThreshold)
            {
                qualityMaintainer.Quality += ThirdQualityIncrease;
            }
            else
            {
                qualityMaintainer.Quality = 0;
            }
        }
    }
}