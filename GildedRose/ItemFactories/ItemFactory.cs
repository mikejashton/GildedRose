using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using GildedRose.Interfaces;

namespace GildedRose.ItemFactories
{
    /// <summary>
    /// Represents the strategy to be used for a given type of stock
    /// </summary>
    public class StockManagementStrategy
    {
        /// <summary>
        /// Initialises the stock management strategy
        /// </summary>
        /// <param name="qualityStrategy">The required quality strategy for this item type</param>
        /// <param name="sellByStrategy">The required sell by  strategy for this item type</param>
        public StockManagementStrategy(QualityStrategy qualityStrategy, SellByStrategy sellByStrategy)
        {
            QualityStrategy = qualityStrategy;
            SellByStrategy = sellByStrategy;
        }
        
        /// <summary>
        /// The quality strategy applied to the item
        /// </summary>
        public QualityStrategy QualityStrategy { get; set; }
        
        /// <summary>
        /// The sell by strategy applied to the item 
        /// </summary>
        public SellByStrategy SellByStrategy { get; set; }
    }
    
    /// <summary>
    /// Manufactures an item based on basic properties
    /// </summary>
    public class ItemFactory : IInventoryItemFactory
    {
        private readonly IDictionary<string, StockManagementStrategy> _stockManagementConfig;

        /// <summary>
        /// Initialises the factory with the stock management strategies
        /// </summary>
        /// <param name="stockManagementConfig">A list of stock management strategies that are keyed by the product name
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when the stock management configuration has not been provided</exception>
        public ItemFactory( IDictionary<string, StockManagementStrategy> stockManagementConfig )
        {
            _stockManagementConfig = stockManagementConfig ?? throw new ArgumentNullException( nameof( stockManagementConfig) );
        }

        /// <summary>
        /// Creates a new inventory item
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="sellIn">The number of days before the item must be sold</param>
        /// <param name="quality">The item's quality metric</param>
        /// <exception cref="ArgumentNullException">Thrown when a parameter is invalid</exception>
        /// <returns>A newly constructed inventory item</returns>
        public IInventoryItem Create(string name, int sellIn, int quality)
        {
            if ( name == null )
            {
                throw new ArgumentNullException( nameof( name ));
            }
            
            return new Item( name, sellIn, quality );
        }
    }
}
