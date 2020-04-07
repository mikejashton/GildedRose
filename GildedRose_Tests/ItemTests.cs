using GildedRose;
using GildedRose.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests
{
    [TestClass]
    public class ItemTests
    {
        /// <summary>
        /// This test ensures that the object maintains the values that is it given
        /// </summary>
        [TestMethod]
        public void Success_ItemMaintainedValue()
        {
            // Setup
            var newItem = new Item( "Some Stock Item", -3, 4, 
                QualityStrategy.LinearDecrease) as IInventoryItem;

            //  Assert
            Assert.AreEqual("Some Stock Item", newItem.Name);
            Assert.AreEqual(4, newItem.Quality);
            Assert.AreEqual(-3, newItem.SellIn);
            Assert.AreEqual( QualityStrategy.LinearDecrease, newItem.QualityStrategy );
        }

        /// <summary>
        /// Ensures that the Item's quality property provides access to the same Quality member as the
        /// IInventoryItem
        /// </summary>
        [TestMethod]
        public void Success_QualityMaintainerUpdatesValues()
        {
            // Setup
            var newItem = new Item( "Some Stock Item", -3, 4, 
                QualityStrategy.RapidDecrease ) as IInventoryItem;
            var qualityMaintainer = (IQualityMaintenance)newItem;
            
            // Execution
            // Update the quality, ensure that all other members are untouched
            qualityMaintainer.Quality = 109;

            //  Assert
            Assert.AreEqual("Some Stock Item", newItem.Name);
            Assert.AreEqual(109, newItem.Quality);
            Assert.AreEqual(-3, newItem.SellIn);
            Assert.AreEqual( QualityStrategy.RapidDecrease, newItem.QualityStrategy );
        }
        
        /// <summary>
        /// Ensures that the Item's SellBy property provides access to the same SellBy member as the
        /// IInventoryItem
        /// </summary>
        [TestMethod]
        public void Success_ShelfLifeMaintainerUpdatesValues()
        {
            // Setup
            var newItem = new Item( "Some Stock Item", -3, 4, 
                QualityStrategy.LinearDecrease) as IInventoryItem;
            var qualityMaintainer = (IShelfLifeMaintenance)newItem;
            
            // Execution
            // Update the quality, ensure that all other members are untouched
            qualityMaintainer.SellIn = 33;

            //  Assert
            Assert.AreEqual("Some Stock Item", newItem.Name);
            Assert.AreEqual(4, newItem.Quality);
            Assert.AreEqual(33, newItem.SellIn);
            Assert.AreEqual( QualityStrategy.LinearDecrease, newItem.QualityStrategy );
        }
    }
}