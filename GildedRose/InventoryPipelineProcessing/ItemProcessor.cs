using System;
using GildedRose.Interfaces;
using GildedRose.ItemFactories;

namespace GildedRose.InventoryPipelineProcessing
{
    /// <summary>
    /// Processes an inventory item as part of the processing pipeline.
    /// This is the main business logic
    /// </summary>
    internal class ItemProcessor : IItemProcessor
    {
        private readonly IQualityPipelineFactory _qualityFactory;
        private readonly IShelfLifeAlgorithmFactory _shelfLifeFactory;

        /// <summary>
        /// Constructs the processor. This object transforms a text input into a text output
        /// </summary>
        /// <param name="qualityFactory">The quality factory</param>
        /// <param name="shelfLifeFactory">The shelf life factory</param>
        public ItemProcessor(IQualityPipelineFactory qualityFactory, IShelfLifeAlgorithmFactory shelfLifeFactory)
        {
            _qualityFactory = qualityFactory ?? throw new ArgumentNullException( nameof( qualityFactory ));
            _shelfLifeFactory = shelfLifeFactory  ?? throw new ArgumentNullException( nameof( shelfLifeFactory ));;
        }
        
        /// <summary>
        /// Perform the processing
        /// </summary>
        /// <param name="inventoryItem">The inventory item to be processed</param>
        /// <returns>Returns the same inventory item as was provided in the input</returns>
        public IInventoryItem Process(IInventoryItem inventoryItem )
        {
            if (inventoryItem == null)
            {
                return null;
            }
                
            try
            {
                // Run the algorithm against the stock life first
                var shelfLifeAlgorithm = _shelfLifeFactory.Create(inventoryItem.ShelfLifeStrategy);
                if (shelfLifeAlgorithm == null)
                {
                    Console.WriteLine( $"The shelf life algorithm return was null. Item: {inventoryItem.Name}");
                    return null;
                }
                shelfLifeAlgorithm.Run(inventoryItem, inventoryItem as IShelfLifeMaintenance);

                // Now run the algorithm against the quality metric
                var qualityPipeline = _qualityFactory.CreatePipeline(inventoryItem);
                foreach (var qualityAlgorithm in qualityPipeline)
                {
                    qualityAlgorithm.Run(inventoryItem, inventoryItem as IQualityMaintenance);
                }

                return inventoryItem;
            }
            catch (Exception ex)
            {
                Console.WriteLine( $"An error has occurred processing an item. Processing will continue. Error: {ex.Message}");
                return null;
            }
        }
    }
}