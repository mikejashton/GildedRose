using System;
using GildedRose.Interfaces;

namespace GildedRose.ItemFactories
{
    /// <summary>
    /// Manufactures an item based on basic properties
    /// </summary>
    public class ItemFactory : IInventoryItemFactory
    {
        /// <summary>
        /// Creates a new inventory item
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="sellIn">The number of days before the item must be sold</param>
        /// <param name="quality">The item's quality metric</param>
        /// <returns>A newly constructed inventory item</returns>
        public IInventoryItem Create(string name, int sellIn, int quality)
        {
            if ( name == null )
            {
                throw new ArgumentNullException( nameof( name ));
            }
            
            return new Item( name, sellIn, quality );
        }
    }
}
