using GildedRose;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.Quality.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories.Quality.Strategies
{
    [TestClass]
    public class StableQualityAlgorithmTests
    {
        /// <summary>
        /// The quality algorithm should not modify the quality member
        /// </summary>
        [TestMethod]
        public void Success_StrategyDoesNothing()
        {
            // Setup
            const int qualityScore = 2;
            var item = new Item( "Name", 1, qualityScore, QualityStrategy.Stable, SellByStrategy.LinearDecrease );
            var algorithm = new StableQualityAlgorithm();
            
            // Execution
            algorithm.Run(item, item );
            
            // Assert
            Assert.AreEqual( qualityScore, item.Quality );
        }
    }
}