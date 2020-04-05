using GildedRose;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GuildedRose_Tests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void Success_ItemMaintainedValue()
        {
            // Setup
            var newItem = new Item()
            {
                Name = "Some Stock Item",
                Quality = 4,
                SellBy = -3
            };
            
            //  Assert
            Assert.AreEqual("Some Stock Item", newItem.Name);
            Assert.AreEqual(4, newItem.Quality);
            Assert.AreEqual(-3, newItem.SellBy);
        }
    }
}