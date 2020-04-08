namespace GildedRose.Interfaces
{
    /// <summary>
    /// An algorithm that acts upon an item's sell by metric  
    /// </summary>
    public interface IShelfLifeAlgorithm
    {
        /// <summary>
        /// Runs the algorithm
        /// </summary>
        /// <param name="item">The item to be maintained</param>
        /// <param name="shelfLifeMaintainer">The objects that allows the sell by  to be queried and modified</param>
        public void Run(IInventoryItem item, IShelfLifeMaintenance shelfLifeMaintainer);
    }
}