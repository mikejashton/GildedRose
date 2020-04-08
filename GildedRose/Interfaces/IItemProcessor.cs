namespace GildedRose.Interfaces
{
    /// <summary>
    /// Responsible for performing the processing pipeline on an inventory item
    /// </summary>
    internal interface IItemProcessor
    {
        /// <summary>
        /// Processes the inventory item
        /// </summary>
        /// <param name="inventoryItem">The inventory item that is to be processed</param>
        /// <returns>The inventory item to be processed by the remainder of the pipeline</returns>
        IInventoryItem Process(IInventoryItem inventoryItem );
    }
}