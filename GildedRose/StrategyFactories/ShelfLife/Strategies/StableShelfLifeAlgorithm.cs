using GildedRose.Interfaces;

namespace GildedRose.StrategyFactories.ShelfLife.Strategies
{
    /// <summary>
    /// Implements the requirement that the quality of some items should never change
    /// </summary>
    public class StableShelfLifeAlgorithm : IShelfLifeAlgorithm
    {
        /// <summary>
        /// Runs the algorithm
        /// </summary>
        /// <param name="item">The item to be maintained</param>
        /// <param name="shelfLifeMaintainer">The object to be maintained</param>
        public void Run(IInventoryItem item, IShelfLifeMaintenance shelfLifeMaintainer)
        {
            // We don't do anything, because the value needs to remain stable.
        }
    }
}