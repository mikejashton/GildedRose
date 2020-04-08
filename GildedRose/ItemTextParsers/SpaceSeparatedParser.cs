using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GildedRose.Interfaces;

namespace GildedRose.ItemTextParsers
{
    /// <summary>
    /// Parses a string to retrieve the inventory information. This parses uses a special logic to parse an input
    /// with no fixes delimiters
    /// </summary>
    public class SpaceSeparatedParser : IItemStringParser
    {
        /// <summary>
        /// Parses the item
        /// </summary>
        /// <param name="inputText">The string to be parsed</param>
        /// <returns>The parsed item. NULL must be returned for an non-conforming items</returns>
        public ParsedItem Parse(string inputText)
        {
            if (inputText == null)
            {
                return null;
            }

            // Split the string by spaces. 
            var elements= inputText.Split(' ');
            
            // We would expect to see at least 3 columns
            if (elements.Length < 3)
            {
                return null;
            }
            
            // The last two columns should be the sell by and quality respectively. We concatenate the remaining
            // columns to make the "name"
            var sellByStr = elements[^2];
            var qualityStr = elements[^1];
            
            // We should now try to coalesce the types to integer.
            var sellByConverted = int.TryParse(sellByStr, out var sellBy);
            var qualityConverted = int.TryParse(qualityStr, out var quality);
            if (!sellByConverted || !qualityConverted)
            {
                return null;
            }

            // We need to reconstruct the name. It's not nice.
            var builder = new StringBuilder();
            var nameElements = elements[..^2];
            foreach (var element in nameElements)
            {
                builder.Append(element);
                builder.Append(" ");
            }

            var name = builder.ToString().TrimEnd();
            return new ParsedItem() { Name = name, Quality = quality, SellBy = sellBy };
        }
    }
}