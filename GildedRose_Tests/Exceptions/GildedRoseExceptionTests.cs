using System;
using GildedRose.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.Exceptions
{
    [TestClass]
    public class GildedRoseExceptionTests
    {
        [TestMethod]
        public void Success_CanCreateBasicException()
        {
            var exception = new GildedRoseException();
            
            // We'll check the object, but really what counts is that no exception has been generated
            Assert.IsNotNull( exception );
        }
        
        [TestMethod]
        public void Success_CanSetMessage()
        {
            // Setup
            const string message = "My error message";
            
            // Execution
            var exception = new GildedRoseException( message );
            
            // Assert
            Assert.AreEqual( message, exception.Message );
        }
        
        [TestMethod]
        public void Success_CanSetMessageAndInnerException()
        {
            // Setup
            const string message = "My error message";
            var innerException = new InvalidOperationException( "My message ");
            
            // Execution
            var exception = new GildedRoseException( message, innerException );
            
            // Assert
            Assert.AreEqual( message, exception.Message );
            Assert.AreEqual( innerException, exception.InnerException );
        }
    }
}