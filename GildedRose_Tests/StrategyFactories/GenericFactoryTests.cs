using System;
using System.Collections.Generic;
using GildedRose.StrategyFactories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GildedRose_Tests.StrategyFactories
{
    [TestClass]
    public class GenericFactoryTests
    {
        /// <summary>
        /// The method should successfully find the type and instantiate it
        /// </summary>
        [TestMethod]
        public void Success_CorrectTypeCreated()
        {
            // Setup
            var factory = new GenericFactory<int, Exception>(new Dictionary<int, Type>()
            {
                {0, typeof(Exception)},
                {1, typeof(InvalidProgramException)},
            });
            
            // Execution
            var result = factory.Create(1);
            
            // Assert
            Assert.IsNotNull( result );
            Assert.AreEqual( typeof( InvalidProgramException ), result.GetType());
        }
        
        /// <summary>
        /// The method should throw an exception because the type does not exist in the dictionary provided in the
        /// constructor
        /// </summary>
        [TestMethod]
        public void Failure_KeyDoesNotExist()
        {
            // Setup
            var factory = new GenericFactory<int, Exception>(new Dictionary<int, Type>()
            {
                {0, typeof(Exception)},
                {1, typeof(InvalidProgramException)},
            });
            
            // Execution and Assert
            Assert.ThrowsException<ArgumentException>( () => factory.Create(2 ));
        }
        
        /// <summary>
        /// The method should throw an exception because the types specified in the dictionary are not
        /// compatible with the return type from the Create method
        /// </summary>
        [TestMethod]
        public void Failure_IncorrectType()
        {
            // Setup
            var factory = new GenericFactory<int, Exception>(new Dictionary<int, Type>()
            {
                {0, typeof(Exception)},
                {1, typeof(InvalidProgramException)},
                {2, typeof(List<Exception>)}
            });
            
            // Execution and Assert
            Assert.ThrowsException<InvalidCastException>( () => factory.Create(2 ));
        }
    }
}