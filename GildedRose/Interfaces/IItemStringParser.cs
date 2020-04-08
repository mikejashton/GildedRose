namespace GildedRose.Interfaces
{
    /// <summary>
    /// An item, as parsed from an input source
    /// </summary>
    public class ParsedItem
    {
        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The quality of the item
        /// </summary>
        public int Quality { get; set; }
        
        /// <summary>
        /// The sell by value for the item
        /// </summary>
        public int SellBy { get; set; }
    }
    
    /// <summary>
    /// An object that can parse a string containing the details for an inventory item
    /// </summary>
    public interface IItemStringParser
    {
        /// <summary>
        /// Parses the item
        /// </summary>
        /// <param name="inputText">The string to be parsed</param>
        /// <returns>The parsed item. NULL must be returned for an non-conforming items</returns>
        ParsedItem Parse(string inputText);
    }
}