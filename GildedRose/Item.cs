using GildedRose.Interfaces;

namespace GildedRose
{
    /// <summary>
    /// Represents an item of stock
    /// </summary>
    public class Item : IInventoryItem, IQualityMaintenance, IShelfLifeMaintenance
    {
        /// <summary>
        /// Represents an item of stock.
        /// </summary>
        /// <param name="name">The name of the item</param>
        /// <param name="sellIn">The number of days remaining before the item must be sold</param>
        /// <param name="quality">The quality metric associated with the item</param>
        internal Item(string name, int sellIn, int quality)
        {
            Name = name;
            SellIn = sellIn;
            Quality = quality;
        }
        
        /// <summary>
        /// The name of this item
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The quality score associated with it. 
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// The sell by metric for this item
        /// </summary>
        public int SellIn { get; set; }
    }
}