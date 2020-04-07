using System;
using System.Collections.Generic;
using GildedRose;
using GildedRose.Exceptions;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.Quality;
using GildedRose.StrategyFactories.Quality.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories.Quality
{
    public class TestAlgorithm1 : IQualityAlgorithm
    {
        public void Run(IInventoryItem item, IQualityMaintenance qualityMaintainer)
        {
        }
    }
    
    public class TestAlgorithm2 : IQualityAlgorithm
    {
        public void Run(IInventoryItem item, IQualityMaintenance qualityMaintainer)
        {
        }
    }
    
    [TestClass]
    public class QualityAlgorithmFactoryTests
    {
        /// <summary>
        /// The type can be created successfully
        /// </summary>
        [TestMethod]
        public void Success_QualityAlgorithmCreated()
        {
            // Setup
            var factory = new QualityAlgorithmFactory( new Dictionary<QualityStrategy, Type>()
            {
                { QualityStrategy.Stable, typeof( TestAlgorithm1 ) },
                { QualityStrategy.LinearDecrease, typeof( TestAlgorithm2 ) }
            });
            
            // Execution
            var result = factory.Create(QualityStrategy.LinearDecrease);
            
            // Assert
            Assert.IsNotNull( result );
            Assert.AreEqual( typeof( TestAlgorithm2), result.GetType() );
        }
        
        /// <summary>
        /// The type cannot be created because it does not exist in the lookup
        /// </summary>
        [TestMethod]
        public void Failure_LookupDoesNotContainKey()
        {
            // Setup
            var factory = new QualityAlgorithmFactory( new Dictionary<QualityStrategy, Type>()
            {
                { QualityStrategy.Stable, typeof( TestAlgorithm1 ) },
                { QualityStrategy.LinearDecrease, typeof( TestAlgorithm2 ) }
            });
            
            // Execution and assert
            Assert.ThrowsException<ArgumentException>(() => factory.Create(QualityStrategy.RapidDecrease));
        }
        
        /// <summary>
        /// The type cannot be created because the lookup contains the wrong type
        /// </summary>
        [TestMethod]
        public void Failure_LookupContainsWrongType()
        {
            // Setup
            var factory = new QualityAlgorithmFactory( new Dictionary<QualityStrategy, Type>()
            {
                { QualityStrategy.Stable, typeof( TestAlgorithm1 ) },
                { QualityStrategy.LinearDecrease, typeof( List<int> ) }
            });
            
            // Execution and assert
            var exception = Assert.ThrowsException<GildedRoseException>(() => factory.Create(QualityStrategy.LinearDecrease));
            Assert.AreEqual( $"The lookup list item relating to '{QualityStrategy.LinearDecrease}' was not compatible" +
                             $"with the IQualityAlgorithm return type.", exception.Message );
        }
    }
}