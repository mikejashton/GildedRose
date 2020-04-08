using System;
using System.Collections.Generic;
using GildedRose.Exceptions;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories.Quality;
using GildedRose.StrategyFactories.ShelfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories.ShelfLife
{
    public class TestAlgorithm1 : IShelfLifeAlgorithm
    {
        public void Run(IInventoryItem item, IShelfLifeMaintenance qualityMaintainer)
        {
        }
    }
    
    public class TestAlgorithm2 : IShelfLifeAlgorithm
    {
        public void Run(IInventoryItem item, IShelfLifeMaintenance qualityMaintainer)
        {
        }
    }
    
    [TestClass]
    public class ShelfLifeAlgorithmFactoryTests
    {
        /// <summary>
        /// The type can be created successfully
        /// </summary>
        [TestMethod]
        public void Success_ShelfLifeAlgorithmCreated()
        {
            // Setup
            var factory = new ShelfLifeAlgorithmFactory( new Dictionary<SellByStrategy, Type>()
            {
                { SellByStrategy.Stable, typeof( TestAlgorithm1 ) },
                { SellByStrategy.LinearDecrease, typeof( TestAlgorithm2 ) }
            });
            
            // Execution
            var result = factory.Create(SellByStrategy.LinearDecrease);
            
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
            var factory = new ShelfLifeAlgorithmFactory( new Dictionary<SellByStrategy, Type>()
            {
                { SellByStrategy.LinearDecrease, typeof( TestAlgorithm2 ) }
            });
            
            // Execution and assert
            Assert.ThrowsException<ArgumentException>(() => factory.Create(SellByStrategy.Stable));
        }
        
        /// <summary>
        /// The type cannot be created because the lookup contains the wrong type
        /// </summary>
        [TestMethod]
        public void Failure_LookupContainsWrongType()
        {
            // Setup
            var factory = new ShelfLifeAlgorithmFactory( new Dictionary<SellByStrategy, Type>()
            {
                { SellByStrategy.LinearDecrease, typeof( TestAlgorithm2 ) },
                { SellByStrategy.Stable, typeof( List<int> ) }
            });
            
            // Execution and assert
            var exception = Assert.ThrowsException<GildedRoseException>(() => factory.Create(SellByStrategy.Stable));
            Assert.AreEqual( $"The lookup list item relating to '{SellByStrategy.Stable}' was not compatible" +
                             $"with the IShelfLifeAlgorithm return type.", exception.Message );
        }
    }
}