using GildedRose;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.Quality.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories.Quality.Strategies
{
    [TestClass]
    public class QualityNeverNegativeAlgorithmTests
    {
        /// <summary>
        /// A positive quality should be left unchanged
        /// </summary>
        [TestMethod]
        public void Success_PositiveQualityNeverChanged()
        {
            // Setup
            const int qualityScore = 2;
            var item = new Item( "Name", 1, qualityScore, QualityStrategy.LinearDecrease );
            var algorithm = new QualityNeverNegativeAlgorithm();
            
            // Execution
            algorithm.Run( item, item );
            
            // Assert
            Assert.AreEqual( qualityScore , item.Quality );
        }
        
        /// <summary>
        /// A positive quality (but zero) should be left unchanged
        /// </summary>
        [TestMethod]
        public void Success_PositiveQualityButZeroNeverChanged()
        {
            // Setup
            const int qualityScore = 0;
            var item = new Item( "Name", 1, qualityScore, QualityStrategy.LinearDecrease );
            var algorithm = new QualityNeverNegativeAlgorithm();
            
            // Execution
            algorithm.Run( item, item );
            
            // Assert
            Assert.AreEqual( qualityScore , item.Quality );
        }
        
        /// <summary>
        /// A positive quality (but zero) should be left unchanged
        /// </summary>
        [TestMethod]
        public void Success_NegativeQualityClippedAtZero()
        {
            // Setup
            const int qualityScore = -10;
            var item = new Item( "Name", 1, qualityScore, QualityStrategy.LinearDecrease );
            var algorithm = new QualityNeverNegativeAlgorithm();
            
            // Execution
            algorithm.Run( item, item );
            
            // Assert
            Assert.AreEqual( 0 , item.Quality );
        }
    }
}