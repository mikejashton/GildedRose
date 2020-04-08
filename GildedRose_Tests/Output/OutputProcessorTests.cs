using System;
using System.IO;
using GildedRose;
using GildedRose.Interfaces;
using GildedRose.Output;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.Output
{
    [TestClass]
    public class OutputProcessorTests
    {
        [TestMethod]
        public void Failure_ThrowsOnNullStream()
        {
            // Execution and assert
            Assert.ThrowsException<ArgumentNullException>(() => new OutputProcessor(null));
        }
        
        [TestMethod]
        public void Success_CorrectlyFormatsOutputOfValidItem()
        {
            // Setup
            const string expectedName = "MyItem";
            const int expectedSellIn = 49;
            const int expectedQuality = 78;
            
            
            bool result;
            byte[] buffer;
            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    var processor = new OutputProcessor(streamWriter);


                    var item = new Item(expectedName, expectedSellIn, expectedQuality, QualityStrategy.Stable,
                        ShelfLifeStrategy.Stable);

                    // Execution
                    result = processor.Process(item);
                }

                // Get the contents of the stream
                buffer = stream.GetBuffer();
            }

            // Assert
            Assert.IsTrue( result );

            using (var memoryStream = new MemoryStream(buffer))
            {
                using (var reader = new StreamReader(memoryStream))
                {
                    var line = reader.ReadLine();
                
                    // Assert
                    var expectedString = $"{expectedName} {expectedSellIn} {expectedQuality}";
                    Assert.IsNotNull( line );
                    Assert.AreEqual( expectedString, line);
                }
            }
        }
        
        [TestMethod]
        public void Success_CorrectlyFormatsOutputOfNullItem()
        {
            // Setup
            var result = false;
            byte[] buffer;
            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    var processor = new OutputProcessor(streamWriter);

                    // Execution
                    result = processor.Process( null );
                }

                // Get the contents of the stream
                buffer = stream.GetBuffer();
            }

            // Assert
            Assert.IsFalse( result );

            using (var memoryStream = new MemoryStream(buffer))
            {
                using (var reader = new StreamReader(memoryStream))
                {
                    var line = reader.ReadLine();
                
                    // Assert
                    var expectedString = "NO SUCH ITEM";
                    Assert.IsNotNull( line );
                    Assert.AreEqual( expectedString, line);
                }
            }
        }
    }
}