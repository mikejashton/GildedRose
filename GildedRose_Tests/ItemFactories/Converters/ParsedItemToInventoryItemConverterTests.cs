using System;
using GildedRose;
using GildedRose.Interfaces;
using GildedRose.ItemFactories.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GildedRose_Tests.ItemFactories.Converters
{
    [TestClass]
    public class ParsedItemToInventoryItemConverterTests
    {
        [TestMethod]
        public void Failure_ThrowsOnNullItemFactory()
        {
            // Execution
            Assert.ThrowsException<ArgumentNullException>(() => new ParsedItemToInventoryItemConverter(null));
        }
        
        [TestMethod]
        public void Success_ReturnsNullWhenItemNull()
        {
            // Setup
            var itemFactory = new Mock<IInventoryItemFactory>();
            var converter = new ParsedItemToInventoryItemConverter( itemFactory.Object );

            // Execution
            var result = converter.Convert(null);
            
            // Assert
            Assert.IsNull( result );
        }
        
        [TestMethod]
        public void Success_ReturnsItemWhenFactoryCreatesOne()
        {
            // Setup
            const string expectedName = "This new item";
            const int expectedSellIn = 44;
            const int expectedQuality = 33;
            var expectedItem = new Item(expectedName, expectedSellIn, expectedQuality, QualityStrategy.Stable,
                ShelfLifeStrategy.Stable);
            
            var itemFactory = new Mock<IInventoryItemFactory>( MockBehavior.Strict );
            itemFactory.Setup(f => f.Create(expectedName, expectedSellIn, expectedQuality))
                .Returns( expectedItem );
                
            var converter = new ParsedItemToInventoryItemConverter( itemFactory.Object );

            // Execution
            var result = converter.Convert(new ParsedItem()
            {
                Name = expectedName,
                SellBy = expectedSellIn,
                Quality = expectedQuality
            });
            
            // Assert
            Assert.IsNotNull( result );
            Assert.AreSame(expectedItem, result);
        }
    }
}