using System;
using System.Collections.Generic;
using GildedRose.Exceptions;
using GildedRose.Interfaces;
using GildedRose.ItemFactories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.ItemFactories
{
    [TestClass]
    public class ItemFactoryTests
    {
        [TestMethod]
        public void Failure_NullConstructorParameters()
        {
            // Setup
            Assert.ThrowsException<ArgumentNullException>(() => new ItemFactory(null) );
        }
    
        /// <summary>
        /// An inventory object should be created given a valid input
        /// </summary>
        [TestMethod]
        public void Success_CorrectlyFormedObjectCreated()
        {
            // Setup
            const string objectName = "My new object";
            const int sellIn = 99;
            const int quality = 66;
            const QualityStrategy expectedQualityStrategy = QualityStrategy.LinearDecrease;
            var factory = new ItemFactory(new Dictionary<string, StockManagementStrategy>()
            {
                { "Some Other object", new StockManagementStrategy( QualityStrategy.Stable ) },
                { "Yet another object", new StockManagementStrategy( QualityStrategy.RapidDecrease ) },
                { objectName, new StockManagementStrategy( expectedQualityStrategy ) }, 
            }) ;

            // Execution
            var newItem = factory.Create(objectName, sellIn, quality);

            // Assert
            Assert.IsNotNull(newItem);
            Assert.AreEqual(objectName, newItem.Name);
            Assert.AreEqual(sellIn, newItem.SellIn);
            Assert.AreEqual(quality, newItem.Quality);
            Assert.AreEqual( expectedQualityStrategy, newItem.QualityStrategy );
        }

        /// <summary>
        /// The factory should throw an exception is the name is invalid
        /// </summary>
        [TestMethod]
        public void Failure_ExceptionOnNullName()
        {
            // Setup
            var factory = new ItemFactory(new Dictionary<string, StockManagementStrategy>()
            {
                { "SomeObject", new StockManagementStrategy( QualityStrategy.Stable) }, 
            }) ;
            
            // Execution
            const int sellIn = 99;
            const int quality = 66;
            Assert.ThrowsException<ArgumentNullException>(() => factory.Create(null, sellIn, quality) );
        }

        [TestMethod]
        public void Failure_InvalidNameGeneratesException()
        {
            // Setup
            var factory = new ItemFactory(new Dictionary<string, StockManagementStrategy>()
            {
                { "This is a valid object", new StockManagementStrategy( QualityStrategy.Stable) }, 
            }) ;
            
            // Execution
            Assert.ThrowsException<InvalidStockObjectNameException>(() => factory.Create("Invalid Object", 99, 66) );
        }
    }
}