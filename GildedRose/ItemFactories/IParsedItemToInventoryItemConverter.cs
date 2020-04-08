using GildedRose.Interfaces;

namespace GildedRose.ItemFactories
{
    /// <summary>
    /// Converts between an ParseItem and an InventoryItem
    /// </summary>
    internal interface IParsedItemToInventoryItemConverter
    {
        /// <summary>
        /// Creates an inventory item
        /// </summary>
        /// <param name="parsedItem"></param>
        /// <returns></returns>
        IInventoryItem Convert(ParsedItem parsedItem);
    }
}