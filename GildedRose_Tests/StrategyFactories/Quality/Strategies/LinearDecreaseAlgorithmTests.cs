using GildedRose;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.Quality.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories.Quality.Strategies
{
    [TestClass]
    public class LinearDecreaseAlgorithmTests
    {
        /// <summary>
        /// The quality algorithm should decrease a non-zero quality to zero
        /// </summary>
        [TestMethod]
        public void Success_StrategyDecreasesQuality()
        {
            // Setup
            const int qualityScore = 2;
            var item = new Item( "Name", 1, qualityScore, QualityStrategy.LinearDecrease, SellByStrategy.LinearDecrease );
            var algorithm = new LinearDecreaseAlgorithm();
            
            // Execution
            algorithm.Run( item );
            
            // Assert
            Assert.AreEqual( qualityScore - 1, item.Quality );
        }
        
        /// <summary>
        /// The quality algorithm should not modify the quality member because it would result in a quality less than 0
        /// </summary>
        [TestMethod]
        public void Success_StrategyDoesNotDecreasesQualityPastZero()
        {
            // Setup
            const int qualityScore = 0;
            var item = new Item( "Name", 1, qualityScore, QualityStrategy.LinearDecrease, SellByStrategy.LinearDecrease );
            var algorithm = new LinearDecreaseAlgorithm();
            
            // Execution
            algorithm.Run( item );
            
            // Assert
            Assert.AreEqual( qualityScore, item.Quality );
        }
    }
}