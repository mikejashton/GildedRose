namespace GildedRose.Interfaces
{
    /// <summary>
    /// Represents an item of inventory
    /// </summary>
    public interface IInventoryItem
    {
        /// <summary>
        /// The name of the item
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// A measure of the quality of the item. This value may change as the item ages
        /// </summary>
        int Quality { get; }

        /// <summary>
        /// The number of days until the item should be sold
        /// </summary>
        int SellIn { get; }
    }
}