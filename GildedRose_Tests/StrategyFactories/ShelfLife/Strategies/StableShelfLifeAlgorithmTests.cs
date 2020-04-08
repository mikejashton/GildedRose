using GildedRose;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.ShelfLife.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories.ShelfLife.Strategies
{
    [TestClass]
    public class StableShelfLifeAlgorithmTests
    {
        /// <summary>
        /// The quality algorithm should not modify the sell by member
        /// </summary>
        [TestMethod]
        public void Success_StrategyDoesNothing()
        {
            // Setup
            const int sellIn = 2;
            var item = new Item( "Name", sellIn, 5, QualityStrategy.LinearDecrease, SellByStrategy.Stable );
            var algorithm = new StableShelfLifeAlgorithm();
            
            // Execution
            algorithm.Run(item, item );
            
            // Assert
            Assert.AreEqual( sellIn, item.SellIn );
        }
    }
}