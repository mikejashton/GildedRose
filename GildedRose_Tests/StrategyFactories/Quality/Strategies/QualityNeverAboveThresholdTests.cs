using GildedRose;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.Quality.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories.Quality.Strategies
{
    [TestClass]
    public class QualityNeverAboveThresholdAlgorithmTests
    {
        /// <summary>
        /// A quality below the threshold should be left unchanged
        /// </summary>
        [TestMethod]
        public void Success_QualityLowerThanThresholdNeverChanged()
        {
            // Setup
            const int maxThreshold = 50;

            var algorithm = new QualityNeverAboveThresholdAlgorithm(maxThreshold);
            for (var qualityScore = 0; qualityScore <= maxThreshold; qualityScore++)
            {
                var item = new Item("Name", 1, qualityScore, QualityStrategy.LinearDecrease, ShelfLifeStrategy.Stable);

                // Execution
                algorithm.Run(item, item);

                // Assert
                Assert.AreEqual(qualityScore, item.Quality);
            }
        }

        /// <summary>
        /// A positive quality (but zero) should be left unchanged
        /// </summary>
        [TestMethod]
        public void Success_ValueAboveThresholdClippedChanged()
        {
            // Setup
            const int qualityScore = 51;
            const int maxThreshold = 50;
            var item = new Item("Name", 1, qualityScore, QualityStrategy.LinearDecrease, ShelfLifeStrategy.Stable);
            var algorithm = new QualityNeverAboveThresholdAlgorithm(maxThreshold);

            // Execution
            algorithm.Run(item, item);

            // Assert
            Assert.AreEqual(50, item.Quality);
        }
    }
}