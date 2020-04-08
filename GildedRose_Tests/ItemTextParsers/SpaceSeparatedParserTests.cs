using GildedRose.ItemTextParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.ItemTextParsers
{
    [TestClass]
    public class SpaceSeparatedParserTests
    {
        [TestMethod]
        public void Success_NullIsAccepted()
        {
            // Setup
            var parser = new SpaceSeparatedParser();
            
            // Execution
            var result = parser.Parse(null);
            
            // Assert
            Assert.IsNull( result );
        }
        
        [TestMethod]
        public void Success_TooFewColumnsAccepted()
        {
            // Setup
            var parser = new SpaceSeparatedParser();
            var columns = "Col1 Col2";
            
            // Execution
            var result = parser.Parse( columns );
            
            // Assert
            Assert.IsNull( result );
        }
        
        [TestMethod]
        public void Success_CorrectStringWithThreeColumnsSupported()
        {
            // Setup
            var parser = new SpaceSeparatedParser();
            var columns = "ItemName 99 66";
            
            // Execution
            var result = parser.Parse( columns );
            
            // Assert
            Assert.IsNotNull( result );
            Assert.AreEqual( "ItemName", result.Name );
            Assert.AreEqual( 99, result.SellBy );
            Assert.AreEqual( 66, result.Quality );
        }
        
        [TestMethod]
        public void Success_CorrectStringWithMoreThanThreeColumnsSupported()
        {
            // Setup
            var parser = new SpaceSeparatedParser();
            var columns = "This Is My Item Name 23 67";
            
            // Execution
            var result = parser.Parse( columns );
            
            // Assert
            Assert.IsNotNull( result );
            Assert.AreEqual( "This Is My Item Name", result.Name );
            Assert.AreEqual( 23, result.SellBy );
            Assert.AreEqual( 67, result.Quality );
        }
        
        [TestMethod]
        public void Success_SupportsNotNumericSellIn()
        {
            // Setup
            var parser = new SpaceSeparatedParser();
            var columns = "This Is My Item Name NotANumber 67";
            
            // Execution
            var result = parser.Parse( columns );
            
            // Assert
            Assert.IsNull( result );
        }
        
        [TestMethod]
        public void Success_SupportsNotNumericQuality()
        {
            // Setup
            var parser = new SpaceSeparatedParser();
            var columns = "This Is My Item Name 23 NotANumber";
            
            // Execution
            var result = parser.Parse( columns );
            
            // Assert
            Assert.IsNull( result );
        }
    }
}