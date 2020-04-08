using GildedRose;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.ShelfLife.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories.ShelfLife.Strategies
{
    [TestClass]
    public class LinearDecreaseAlgorithmTests
    {
        /// <summary>
        /// A positive sell by date should be reduced by one
        /// </summary>
        [TestMethod]
        public void Success_StrategyDecreasesQualityByOne()
        {
            // Setup
            const int sellIn = 2;
            var item = new Item( "Name", sellIn, 5, QualityStrategy.LinearDecrease, SellByStrategy.LinearDecrease );
            var algorithm = new LinearDecreaseShelfLifeAlgorithm();
            
            // Execution
            algorithm.Run(item, item );
            
            // Assert
            Assert.AreEqual( sellIn - 1, item.SellIn );
        }
    }
}