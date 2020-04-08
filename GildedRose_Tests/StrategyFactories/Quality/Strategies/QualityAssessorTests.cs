using GildedRose;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.Quality.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories.Quality.Strategies
{
    [TestClass]
    public class QualityAssessorTests
    {
        /// <summary>
        /// In date product deteriorate by one day
        /// </summary>
        [TestMethod]
        public void Success_InDateProductsDeteriorateAtOneDay()
        {
            // Setup
            const int sellIn = 0;
            const int quality = 9;
            var item = new Item( "Name", sellIn, quality, QualityStrategy.LinearDecrease, SellByStrategy.Stable );
            
            // Execution
            var result = QualityAssessor.CalculateDeteriorationRate(item);
            
            // Assert
            Assert.AreEqual( 1, result );
        }
        
        /// <summary>
        /// Product that is on the sell date should deteriorate by one day
        /// </summary>
        [TestMethod]
        public void Success_OnDateProductsDeteriorateAtOneDay()
        {
            // Setup
            const int sellIn = 0;
            const int quality = 9;
            var item = new Item( "Name", sellIn, quality, QualityStrategy.LinearDecrease, SellByStrategy.Stable );
            
            // Execution
            var result = QualityAssessor.CalculateDeteriorationRate(item);
            
            // Assert
            Assert.AreEqual( 1, result );
        }
        
        /// <summary>
        /// Product that is pass the sell date should deteriorate by two days
        /// </summary>
        [TestMethod]
        public void Success_OutOfDateProductsDeteriorateAtTwoDays()
        {
            // Setup
            const int sellIn = -1;
            const int quality = 9;
            var item = new Item( "Name", sellIn, quality, QualityStrategy.LinearDecrease, SellByStrategy.Stable );
            
            // Execution
            var result = QualityAssessor.CalculateDeteriorationRate(item);
            
            // Assert
            Assert.AreEqual( 2, result );
        }
    }
}