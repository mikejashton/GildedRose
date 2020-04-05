using GildedRose.Interfaces;

namespace GildedRose
{
    /// <summary>
    /// Represents an item of stock
    /// </summary>
    public class Item : IInventoryItem, IQualityMaintenance
    {
        /// <summary>
        /// The name of this item
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The quality score associated with it. 
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// The sell-by date for this item. This represents the number of days until the item must be sold.
        /// </summary>
        public int SellBy { get; set; }
    }
}