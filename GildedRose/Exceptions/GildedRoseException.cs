using System;

namespace GildedRose.Exceptions
{
    /// <summary>
    /// Base class for all application exceptions
    /// </summary>
    public class GildedRoseException : Exception
    {
        /// <summary>
        /// Base class for all application exceptions
        /// </summary>
        public GildedRoseException()
        {
        }

        /// <summary>
        /// Base class for all application exceptions
        /// </summary>
        /// <param name="message">The error message</param>
        public GildedRoseException(string message) : base( message )
        {
        }

        /// <summary>
        /// Base class for all application exceptions
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception</param>
        public GildedRoseException(string message, Exception innerException)
            : base( message, innerException )
        {
        }
    }
}