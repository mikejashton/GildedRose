using System;
using GildedRose.Interfaces;

namespace GildedRose.ItemFactories.Converters
{
    /// <summary>
    /// Converts between an ParseItem and an InventoryItem
    /// </summary>
    internal class ParsedItemToInventoryItemConverter : IParsedItemToInventoryItemConverter
    {
        private readonly IInventoryItemFactory _itemFactory;

        /// <summary>
        /// Converts an item that has been parsed as part of our input pipeline into an inventory item
        /// </summary>
        /// <param name="itemFactory">The factory that can create inventory items</param>
        public ParsedItemToInventoryItemConverter(IInventoryItemFactory itemFactory)
        {
            _itemFactory = itemFactory ?? throw new ArgumentNullException( nameof( itemFactory ));
        }
        
        /// <summary>
        /// Performs the conversion
        /// </summary>
        /// <param name="parsedItem">The input object</param>
        /// <returns>The output object, or null if the object could not be converted</returns>
        public IInventoryItem Convert(ParsedItem parsedItem)
        {
            if (parsedItem == null)
            {
                return null;
            }

            return _itemFactory.Create(parsedItem.Name, parsedItem.SellBy, parsedItem.Quality);
        }
    }
}