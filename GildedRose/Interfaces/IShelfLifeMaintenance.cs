namespace GildedRose.Interfaces
{
    /// <summary>
    /// Allows modification of an entity's shelflife
    /// </summary>
    public interface IShelfLifeMaintenance
    {
        /// <summary>
        /// Allows the sell-by metric to be modified
        /// </summary>
        public int SellBy { set; }
    }
}