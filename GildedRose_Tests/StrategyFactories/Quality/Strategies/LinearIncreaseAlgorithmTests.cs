using GildedRose;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.Quality.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories.Quality.Strategies
{
    [TestClass]
    public class LinearIncreaseAlgorithmTests
    {
        /// <summary>
        /// The algorithm should always increase the quality (positive value case)
        /// </summary>
        [TestMethod]
        public void Success_AlwaysIncreasesQuality()
        {
            // Setup
            const int qualityScore = 99;
            var item = new Item( "Name", 1, qualityScore, QualityStrategy.Stable );
            var algorithm = new LinearIncreaseAlgorithm();
            
            // Execution
            algorithm.Run(item, item );
            
            // Assert
            Assert.AreEqual( qualityScore + 1, item.Quality );
        }
        
        /// <summary>
        /// The algorithm should always increase the quality (zero value case)
        /// </summary>
        [TestMethod]
        public void Success_AlwaysIncreasesQualityZeroValue()
        {
            // Setup
            const int qualityScore = 0;
            var item = new Item( "Name", 1, qualityScore, QualityStrategy.Stable );
            var algorithm = new LinearIncreaseAlgorithm();
            
            // Execution
            algorithm.Run(item, item );
            
            // Assert
            Assert.AreEqual( qualityScore + 1, item.Quality );
        }
    }
}