using System;
using System.IO;
using GildedRose.Interfaces;

namespace GildedRose.Output
{
    /// <summary>
    /// Outputs formatted content to a stream
    /// </summary>
    internal class OutputProcessor
    {
        private readonly StreamWriter _outputStream;

        /// <summary>
        /// Outputs the content to the stream
        /// </summary>
        /// <param name="outputStream">The stream to be written to</param>
        public OutputProcessor(StreamWriter outputStream)
        {
            _outputStream = outputStream ?? throw new ArgumentNullException( nameof( outputStream ));
        }
        
        /// <summary>
        /// Performs the formatting and outputting to the stream
        /// </summary>
        /// <param name="item">The item to be processed</param>
        /// <returns>Indicates whether the item was processed successfully.</returns>
        public bool Process(IInventoryItem item)
        {
            if (item != null)
            {
                _outputStream.WriteLine( $"{item.Name} {item.SellIn} {item.Quality}" );
                return true;
            }

            _outputStream.WriteLine("NO SUCH ITEM");
            return false;
        }
    }
}