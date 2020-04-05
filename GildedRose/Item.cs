using GildedRose.Interfaces;

namespace GildedRose
{
    /// <summary>
    /// Represents an item of stock
    /// </summary>
    public class Item : IInventoryItem, IQualityMaintenance, IShelfLifeMaintenance
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
        /// The sell by metric for this item
        /// </summary>
        public int SellBy { get; set; }
    }
}