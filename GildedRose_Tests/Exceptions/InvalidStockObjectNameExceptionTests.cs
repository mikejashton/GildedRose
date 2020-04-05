using GildedRose.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.Exceptions
{
    [TestClass]
    public class InvalidStockObjectNameExceptionTests
    {
        [TestMethod]
        public void Success_CanCreateException()
        {
            // Setup
            const string stockItemName = "My Stock Item";
            
            // Execution
            var exception = new InvalidStockObjectNameException( stockItemName );
            
            // Assert
            Assert.AreEqual( stockItemName, exception.ItemName );
            Assert.AreEqual( "The stock item name that was provided is invalid", exception.Message );
        }
    }
}