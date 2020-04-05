using System;
using GildedRose.ItemFactories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.ItemFactories
{
    [TestClass]
    public class ItemFactoryTests
    {
        /// <summary>
        /// An inventory object should be created given a valid input
        /// </summary>
        [TestMethod]
        public void Success_CorrectlyFormedObjectCreated()
        {
            // Setup
            var factory = new ItemFactory();
            
            // Execution
            const string objectName = "My new object";
            const int sellIn = 99;
            const int quality = 66;
            var newItem = factory.Create(objectName, sellIn, quality);
            
            // Assert
            Assert.IsNotNull( newItem );
            Assert.AreEqual( objectName, newItem.Name );
            Assert.AreEqual( sellIn, newItem.SellIn );
            Assert.AreEqual( quality, newItem.Quality );
        }
        
        /// <summary>
        /// The factory should throw an exception is the name is invalid
        /// </summary>
        [TestMethod]
        public void Failure_ExceptionOnNullName()
        {
            // Setup
            var factory = new ItemFactory();
            
            // Execution
            const int sellIn = 99;
            const int quality = 66;
            Assert.ThrowsException<ArgumentNullException>(() => factory.Create(null, sellIn, quality) );
        }
    }
}