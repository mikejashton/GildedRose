using System;
using System.Collections.Generic;
using GildedRose;
using GildedRose.Exceptions;
using GildedRose.Interfaces;
using GildedRose.StrategyFactories;
using GildedRose.StrategyFactories.Quality.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GildedRose_Tests.StrategyFactories.ShelfLife
{
    [TestClass]
    public class QualityPipelineFactoryTests
    {
        [TestMethod]
        public void Failure_StrategyFactoryNullRaisesException()
        {
            // Setup
            var list = new List<IQualityAlgorithm>();
                
            // Execution
            Assert.ThrowsException<ArgumentNullException>(() => new QualityPipelineFactory(null, list));
        }
        
        [TestMethod]
        public void Success_NullListOfQualityControlChecksDoesNotRaiseException()
        {
            // Setup
            var qualityFactoryMock = new Mock<IQualityAlgorithmFactory>();

            // Execution
            var factory = new QualityPipelineFactory(qualityFactoryMock.Object, null);
            
            // Assert
            Assert.IsNotNull( factory ); // Not really needed
        }
        
        [TestMethod]
        public void Failure_ItemIsNull()
        {
            // Setup
            var qualityFactoryMock = new Mock<IQualityAlgorithmFactory>();
            var factory = new QualityPipelineFactory(qualityFactoryMock.Object, null);

            // Execution and assert
            Assert.ThrowsException<ArgumentNullException>(() => factory.CreatePipeline(null));
        }
        
        [TestMethod]
        public void Failure_FactoryThrowsArgumentException()
        {
            // Setup
            var item = new Item( "NewItem", 1, 3, QualityStrategy.Stable, SellByStrategy.Stable);
            var qualityFactoryMock = new Mock<IQualityAlgorithmFactory>();
            qualityFactoryMock.Setup(f => f.Create(It.IsAny<QualityStrategy>()))
                .Throws(new ArgumentException());
            var factory = new QualityPipelineFactory(qualityFactoryMock.Object, null);

            // Execution and assert
            // We'd expect this to result in a GildedRoseException exception
            Assert.ThrowsException<GildedRoseException>(() => factory.CreatePipeline(item));
        }
        
        [TestMethod]
        public void Failure_FactoryThrowsGildedRoseException()
        {
            // Setup
            var item = new Item( "NewItem", 1, 3, QualityStrategy.Stable, SellByStrategy.Stable);
            var qualityFactoryMock = new Mock<IQualityAlgorithmFactory>();
            var expectedException = new GildedRoseException();
            qualityFactoryMock.Setup(f => f.Create(It.IsAny<QualityStrategy>()))
                .Throws(expectedException);
            var factory = new QualityPipelineFactory(qualityFactoryMock.Object, null);

            // Execution and assert
            // We'd expect this to result in the original exception being rethrown
            var theException = Assert.ThrowsException<GildedRoseException>(() => factory.CreatePipeline(item));
            Assert.AreEqual( expectedException, theException );
        }
        
        [TestMethod]
        public void Success_CorrectPipelineReturned()
        {
            // Setup
            var qualityStrategyExpected = QualityStrategy.Stable;
            var item = new Item( "NewItem", 1, 3, qualityStrategyExpected, SellByStrategy.Stable);
            var qualityFactoryMock = new Mock<IQualityAlgorithmFactory>(MockBehavior.Strict);
            
            // Create a mock that expects the sort of strategy that is represented in the item above
            var expectedReturn = new StableQualityAlgorithm();
            qualityFactoryMock.Setup(f => f.Create( qualityStrategyExpected ) )
                .Returns( expectedReturn ) ;

            var factory = new QualityPipelineFactory(qualityFactoryMock.Object, null );
            
            // Execution
            var result = factory.CreatePipeline(item);
            
            // Assert
            // Since we haven't provided any QC steps, we should have a single element
            Assert.AreEqual( 1, result.Count );
            Assert.AreEqual( expectedReturn, result[0] );
        }
        
        [TestMethod]
        public void Success_QCPipelineAttached()
        {
            // Setup
            var qualityStrategyExpected = QualityStrategy.LinearIncrease;
            var item = new Item( "NewItem", 1, 3, qualityStrategyExpected, SellByStrategy.Stable);
            var qualityFactoryMock = new Mock<IQualityAlgorithmFactory>(MockBehavior.Strict);
            var qcList = new List<IQualityAlgorithm>()
            {
                new QualityNeverNegativeAlgorithm()
            };
            
            // Create a mock that expects the sort of strategy that is represented in the item above
            var expectedReturn = new LinearIncreaseAlgorithm();
            qualityFactoryMock.Setup(f => f.Create( qualityStrategyExpected ) )
                .Returns( expectedReturn ) ;

            var factory = new QualityPipelineFactory(qualityFactoryMock.Object, qcList );
            
            // Execution
            var result = factory.CreatePipeline(item);
            
            // Assert
            // Since we haven't provided any QC steps, we should have a single element
            Assert.AreEqual( 1 + qcList.Count, result.Count );
            Assert.AreEqual( expectedReturn, result[0] );

            for (var i = 0; i < qcList.Count; i++)
            {
                Assert.AreEqual( qcList[i], result[ 1 + i ]);
            }
        }
        
    }
}