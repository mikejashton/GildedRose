namespace GildedRose.Exceptions
{
    /// <summary>
    /// Indicates that a stock item's name is invalid
    /// </summary>
    public class InvalidStockObjectNameException : GildedRoseException
    {

        /// <summary>
        /// The provided stock item was invalid
        /// </summary>
        /// <param name="itemName">The name of the invalid item</param>
        public InvalidStockObjectNameException(string itemName) : base(
            "The stock item name that was provided is invalid")
        {
            ItemName = itemName;
        }

        /// <summary>
        /// The item of stock that is invalid
        /// </summary>
        public string ItemName { get; private set; }
    }
}