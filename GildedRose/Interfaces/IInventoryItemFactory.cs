namespace GildedRose.Interfaces
{
    /// <summary>
    /// Creates and fully configures an inventory item based on basic pieces of data about the item
    /// </summary>
    public interface IInventoryItemFactory
    {
        /// <summary>
        /// Creates an inventory item
        /// </summary>
        /// <param name="name">The name of the item</param>
        /// <param name="sellIn">The sell in period</param>
        /// <param name="quality">The quality score</param>
        /// <returns>The inventory item</returns>
        public IInventoryItem Create(string name, int sellIn, int quality);
    }
}
