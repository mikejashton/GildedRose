using System;
using System.Collections.Generic;
using GildedRose;
using GildedRose.Interfaces;
using GildedRose.InventoryPipelineProcessing;
using GildedRose.StrategyFactories.ShelfLife.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GildedRose_Tests.InventoryPipelineProcessing
{
    internal class MultipleQuality : IQualityAlgorithm
    {
        public void Run(IInventoryItem item, IQualityMaintenance qualityMaintainer)
        {
            qualityMaintainer.Quality *= 2;
        }
    }
    
    internal class SubtractOneQuality : IQualityAlgorithm
    {
        public void Run(IInventoryItem item, IQualityMaintenance qualityMaintainer)
        {
            qualityMaintainer.Quality--;
        }
    }
    
    [TestClass]
    public class ItemProcessorTests
    {
        [TestMethod]
        public void Failure_ThrowsErrorOnNullQualityFactory()
        {
            // Setup
            var shelfLifeFactory = new Mock<IShelfLifeAlgorithmFactory>();
            
            // Execution
            Assert.ThrowsException<ArgumentNullException>( () => new ItemProcessor(null, shelfLifeFactory.Object));
        }
        
        [TestMethod]
        public void Failure_ThrowsErrorOnNullShelfLifeFactory()
        {
            // Setup
            var qualityFactory = new Mock<IQualityPipelineFactory>();
            
            // Execution
            Assert.ThrowsException<ArgumentNullException>( () => new ItemProcessor(qualityFactory.Object, null ));
        }
        
        [TestMethod]
        public void Failure_ThrowsErrorOnAllElementsNullInConstructor()
        {
            // Setup
            var qualityFactory = new Mock<IQualityPipelineFactory>();
            
            // Execution
            Assert.ThrowsException<ArgumentNullException>( () => new ItemProcessor(null, null ));
        }

        [TestMethod]
        public void Success_HandlesNullItem()
        {
            // Setup
            var qualityFactory = new Mock<IQualityPipelineFactory>();
            var shelfLifeFactory = new Mock<IShelfLifeAlgorithmFactory>();
            var factory = new ItemProcessor( qualityFactory.Object, shelfLifeFactory.Object );
            
            // Execution
            var result = factory.Process(null );
            
            // Assert
            Assert.IsNull( result );
        }

        [TestMethod]
        public void Failure_HandlesNullShelfLifeStrategy()
        {
            // Setup
            var newItem = new Item( "Some Stock Item", -3, 3, 
                QualityStrategy.LinearDecrease, ShelfLifeStrategy.LinearDecrease) as IInventoryItem;
            
            var qualityFactory = new Mock<IQualityPipelineFactory>();
            var shelfLifeFactory = new Mock<IShelfLifeAlgorithmFactory>();
            shelfLifeFactory.Setup(f => f.Create(ShelfLifeStrategy.LinearDecrease))
                .Returns<IShelfLifeAlgorithm>(null);
            var factory = new ItemProcessor( qualityFactory.Object, shelfLifeFactory.Object );
            
            // Execution
            var result = factory.Process( newItem );
            
            // Assert
            Assert.IsNull( result );
        }
        
        [TestMethod]
        public void Success_RunsEntireBusiessLogic()
        {
            // Setup
            const int initialQuality = 4;
            var newItem = new Item( "Some Stock Item", -3, initialQuality, 
                QualityStrategy.LinearDecrease, ShelfLifeStrategy.LinearDecrease) as IInventoryItem;
            
            // Set up the shelf life factory to return a mocked algorithm so we can check that the algorithm is called
            var shelfLifeFactory = new Mock<IShelfLifeAlgorithmFactory>();
            shelfLifeFactory.Setup(f => f.Create( ShelfLifeStrategy.LinearDecrease ))
                .Returns( new LinearDecreaseShelfLifeAlgorithm() );

            // Setup a basic quality pipeline factory to return two elements
            var qualityPipelineFactory = new Mock<IQualityPipelineFactory>( MockBehavior.Strict);
            qualityPipelineFactory.Setup(p => p.CreatePipeline(newItem))
                .Returns( new List<IQualityAlgorithm>()
                {
                    new MultipleQuality(),
                    new SubtractOneQuality()
                });
            
            var factory = new ItemProcessor( qualityPipelineFactory.Object, shelfLifeFactory.Object );
            
            // Execution
            var result = factory.Process( newItem );
            
            // Assert
            Assert.IsNotNull( result );
            
            // If the pipeline has been executed correctly the quality should be = ( initial * 2 ) - 1 = 7
            const int expectedValue = (initialQuality * 2) - 1;
            Assert.AreEqual( expectedValue, result.Quality );
        }
        
        [TestMethod]
        public void Success_ExceptionFromFactoryIsCaught()
        {
            // Setup
            const int initialQuality = 4;
            var newItem = new Item( "Some Stock Item", -3, initialQuality, 
                QualityStrategy.LinearDecrease, ShelfLifeStrategy.LinearDecrease) as IInventoryItem;
            
            // Set up the shelf life factory to return a mocked algorithm so we can check that the algorithm is called
            var shelfLifeFactory = new Mock<IShelfLifeAlgorithmFactory>();
            shelfLifeFactory.Setup(f => f.Create(ShelfLifeStrategy.LinearDecrease))
                .Throws( new Exception() );

            // Setup a basic quality pipeline factory to return two elements
            var qualityPipelineFactory = new Mock<IQualityPipelineFactory>( MockBehavior.Strict);
            
            var factory = new ItemProcessor( qualityPipelineFactory.Object, shelfLifeFactory.Object );
            
            // Execution
            var result = factory.Process( newItem );
            
            // Assert
            Assert.IsNull( result );
        }
    }
}