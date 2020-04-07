using System;
using System.Collections.Generic;
using GildedRose.Interfaces;
using GildedRose.ItemFactories;
using GildedRose.StrategyFactories;
using GildedRose.StrategyFactories.Quality;
using GildedRose.StrategyFactories.Quality.Strategies;

namespace GildedRose
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates a mapping between the quality control strategy and the implementation. This uses
            // type and not a direct instantiation to prevent to prevent more complicated classes interfering with
            // each other should this algorithm ultimately be paralleled.
            var qualityAlgorithmFactory = new QualityAlgorithmFactory( new Dictionary<QualityStrategy, Type>()
            {
                { QualityStrategy.Stable, typeof( StableQualityAlgorithm ) },
                { QualityStrategy.LinearDecrease, typeof( LinearDecreaseAlgorithm ) },
                { QualityStrategy.LinearIncrease, typeof( LinearIncreaseAlgorithm ) },
                { QualityStrategy.RapidDecrease, typeof( RapidDecreaseAlgorithm )},
                { QualityStrategy.IncreasingUntilSellBy, typeof( IncreasingValueUntilSellByAlgorithm ) },
            });
            
            // Creates a mapping between an item of stock and it's corresponding stock management strategy. At the
            // moment this stock management strategy simply defines how the quality is adjusted, but it could
            // contain other definitions in future.
            var itemFactory = new ItemFactory( new Dictionary<string, StockManagementStrategy>()
            {
                { "Aged Brie", new StockManagementStrategy( QualityStrategy.LinearIncrease )},
                { "Backstage passes", new StockManagementStrategy( QualityStrategy.IncreasingUntilSellBy )}, 
                { "Sulfuras", new StockManagementStrategy( QualityStrategy.Stable )},
                { "Normal Item", new StockManagementStrategy( QualityStrategy.LinearDecrease )},
                { "Conjured", new StockManagementStrategy( QualityStrategy.RapidDecrease )}
            });
            
            // Create the list of items (this will ultimately be done in a stream)
            var items = new List<IInventoryItem>()
            {
                itemFactory.Create( "Aged Brie", 1, 1 ),
                itemFactory.Create( "Backstage passes", -1, 2 ),
                itemFactory.Create( "Backstage passes", 9, 2 ),
                itemFactory.Create( "Sulfuras", 2, 2 ),
                itemFactory.Create( "Normal Item", -1, 55 ),
                itemFactory.Create( "Normal Item", 2, 2 ),
                //itemFactory.Create( "INVALID ITEM", 2, 2 ),
                itemFactory.Create( "Conjured", 2, 2 ),
                itemFactory.Create( "Conjured", -1, 5 ),
            };
            
            // Define a list of quality control steps that must be run every time
            var qualityControl = new List<IQualityAlgorithm>() { new QualityNeverNegativeAlgorithm() };
            
            // And now the object that creates the factory for the item.
            var pipelineFactory = new QualityPipelineFactory( qualityAlgorithmFactory, qualityControl);
            
            items.ForEach(item =>
            {
                if (item == null)
                {
                    return;
                }
                
                // Decrease the number days by one
                var shelfLifeMaintainer = item as IShelfLifeMaintenance;
                shelfLifeMaintainer.SellIn--;

                try
                {
                    var pipeline = pipelineFactory.CreatePipeline(item);
                    foreach (var qualityAlgorithm in pipeline)
                    {
                        qualityAlgorithm.Run(item, item as IQualityMaintenance);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine( $"An error has occurred processing an item. Processing will continue. Error: {ex.Message}");
                }
            });
        }
    }
}