using GildedRose.Interfaces;

namespace GildedRose.StrategyFactories.Quality.Strategies
{
    /// <summary>
    /// Not happy about this. This should really be abstracted to an interface and injected into the strategies
    /// </summary>
    public static class QualityAssessor
    {
        private const int StandardDeteriorationRate = 1;
        
        /// <summary>
        /// Returns the deterioration rate for an item
        /// </summary>
        /// <param name="item">The item to be modified</param>
        /// <returns>The deterioration rate</returns>
        public static int CalculateDeteriorationRate(IInventoryItem item)
        {
            if (item.SellIn >= 0)
            {
                return StandardDeteriorationRate;
            }
            else
            {
                // The quality must deteriorate at twice the rate once the sell in date has passed
                return StandardDeteriorationRate * 2;
            }
        }
    }
}