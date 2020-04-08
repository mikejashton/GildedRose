using GildedRose.Interfaces;
using GildedRose.StrategyFactories.Quality.Strategies;

namespace GildedRose.StrategyFactories.ShelfLife.Strategies
{
    /// <summary>
    /// Steadily decreases the shelf life of a product until it reaches zero
    /// </summary>
    public class LinearDecreaseShelfLifeAlgorithm : IShelfLifeAlgorithm
    {
        /// <summary>
        /// Runs the algorithm
        /// </summary>
        /// <param name="item">The item to be maintained</param>
        /// <param name="shelfLifeMaintainer">The item to be maintained</param>
        public void Run(IInventoryItem item, IShelfLifeMaintenance shelfLifeMaintainer)
        {
            shelfLifeMaintainer.SellIn--;
        }
    }
}