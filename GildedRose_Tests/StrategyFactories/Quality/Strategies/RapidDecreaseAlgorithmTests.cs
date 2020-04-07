using GildedRose;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.Quality.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories.Quality.Strategies
{
    [TestClass]
    public class RapidDecreaseAlgorithmTests
    {
        /// <summary>
        /// A positive quality should be reduced by two when the product is in date
        /// </summary>
        [TestMethod]
        public void Success_StrategyDecreasesQualityByOneDayWhileInDate()
        {
            // Setup
            const int qualityScore = 6;
            var item = new Item( "Name", 1, qualityScore, QualityStrategy.LinearDecrease, SellByStrategy.LinearDecrease );
            var algorithm = new RapidDecreaseAlgorithm();
            
            // Execution
            algorithm.Run(item, item );
            
            // Assert
            Assert.AreEqual( qualityScore - 2, item.Quality );
        }
        
        /// <summary>
        /// A positive quality should be reduced by two when the product is at the sell in date
        /// </summary>
        [TestMethod]
        public void Success_StrategyDecreasesQualityOnDate()
        {
            // Setup
            const int qualityScore = 6;
            const int sellIn = 1;
            var item = new Item( "Name", sellIn, qualityScore, QualityStrategy.LinearDecrease, SellByStrategy.LinearDecrease );
            var algorithm = new RapidDecreaseAlgorithm();
            
            // Execution
            algorithm.Run(item, item );
            
            // Assert
            Assert.AreEqual( qualityScore - 2, item.Quality );
        }
        
        /// <summary>
        /// A positive quality should be reduced by four when the product is past the sell in date
        /// </summary>
        [TestMethod]
        public void Success_StrategyDecreasesByTwoQualityPastDate()
        {
            // Setup
            const int qualityScore = 6;
            const int sellIn = -1;
            var item = new Item( "Name", sellIn, qualityScore, QualityStrategy.LinearDecrease, SellByStrategy.LinearDecrease );
            var algorithm = new RapidDecreaseAlgorithm();
            
            // Execution
            algorithm.Run(item, item );
            
            // Assert
            Assert.AreEqual( qualityScore - 4, item.Quality );
        }

    }
}