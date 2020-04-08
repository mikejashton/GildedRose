using GildedRose;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.Quality.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GildedRose_Tests.StrategyFactories.Quality.Strategies
{
    [TestClass]
    public class IncreasingValueUntilSellByAlgorithmTests
    {
        /// <summary>
        /// With more than 10 days to go, the quality should increase by one
        /// </summary>
        [TestMethod]
        public void Success_ValueIncreasesOneWithMoreThanTenDaysToGo()
        {
            // Setup
            const int qualityScore = 2;
            var item = new Item( "Name", 11, qualityScore, QualityStrategy.IncreasingUntilSellBy, SellByStrategy.Stable );
            var algorithm = new IncreasingValueUntilSellByAlgorithm();
            
            // Execution
            algorithm.Run(item, item );
            
            // Assert
            Assert.AreEqual( qualityScore + 1, item.Quality );
        }
        
        /// <summary>
        /// With between 4 and 10 days to go, the quality should increase by two
        /// </summary>
        [TestMethod]
        public void Success_ValueIncreasesTwoBetweenFourAndTenDaysToGo()
        {
            // Setup
            const int qualityScore = 2;
            const int expectedQualityIncrease = 2;
            var sellIn = 0;
            
            var inventoryItem = new Mock<IInventoryItem>();
            inventoryItem.SetupGet(i => i.SellIn).Returns( () => sellIn );

            // Set up a mock that always expects an increase of 2
            var qualityMaintainer = new Mock<IQualityMaintenance>(MockBehavior.Strict);
            qualityMaintainer.SetupGet(i => i.Quality).Returns(qualityScore);
            qualityMaintainer.SetupSet(qm => qm.Quality = qualityScore + expectedQualityIncrease);
            
            var algorithm = new IncreasingValueUntilSellByAlgorithm();
            
            // Execution and assert
            for (sellIn = 4; sellIn <= 10; sellIn++)
            {
                // The maintainer mock is set to strict mode, so any incorrect value will fail
                algorithm.Run(inventoryItem.Object, qualityMaintainer.Object );                
            }
        }
        
        /// <summary>
        /// With between 0 and 3 days to go, the quality should increase by 5
        /// </summary>
        [TestMethod]
        public void Success_ValueIncreasesFiveBetweenZeroAndThreeDaysToGo()
        {
            // Setup
            const int qualityScore = 2;
            const int expectedQualityIncrease = 5;
            var sellIn = 0;
            
            var inventoryItem = new Mock<IInventoryItem>();
            inventoryItem.SetupGet(i => i.SellIn).Returns( () => sellIn );

            // Set up a mock that always expects an increase of 2
            var qualityMaintainer = new Mock<IQualityMaintenance>(MockBehavior.Strict);
            qualityMaintainer.SetupGet(i => i.Quality).Returns(qualityScore);
            qualityMaintainer.SetupSet(qm => qm.Quality = qualityScore + expectedQualityIncrease);
            
            var algorithm = new IncreasingValueUntilSellByAlgorithm();
            
            // Execution and assert
            for (sellIn = 0; sellIn <= 3; sellIn++)
            {
                // The maintainer mock is set to strict mode, so any incorrect value will fail
                algorithm.Run(inventoryItem.Object, qualityMaintainer.Object );                
            }
        }
        /// <summary>
        /// After the sell by, the quality is always 0
        /// </summary>
        [TestMethod]
        public void Success_ValueIsZeroAfterSellBy()
        {
            // Setup
            const int qualityScore = 2;
            var item = new Item( "Name", -1, qualityScore, QualityStrategy.IncreasingUntilSellBy, SellByStrategy.Stable );
            var algorithm = new IncreasingValueUntilSellByAlgorithm();
            
            // Execution
            algorithm.Run(item, item );
            
            // Assert
            Assert.AreEqual( 0, item.Quality );
        }
    }
}