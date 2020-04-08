using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using GildedRose.Interfaces;
using GildedRose.InventoryPipelineProcessing;
using GildedRose.ItemFactories;
using GildedRose.ItemFactories.Converters;
using GildedRose.ItemTextParsers;
using GildedRose.Output;
using GildedRose.StrategyFactories;
using GildedRose.StrategyFactories.Quality;
using GildedRose.StrategyFactories.Quality.Strategies;
using GildedRose.StrategyFactories.ShelfLife;
using GildedRose.StrategyFactories.ShelfLife.Strategies;

namespace GildedRose
{
    class Program
    {
        /// <summary>
        /// The main program entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static async Task<int> Main(string[] args)
        {
            // We require two arguments, one for the input file and the other for the output file. If we don't have these
            // we must stop
            if (args.Length != 2)
            {
                Console.WriteLine( "Usage: GuildedRose InputFileName OutputFileName");
                Console.WriteLine( "The program will parse the input file, modify the contents and output the result to " + 
                                    "the output file");
                return -1;
            }
            
            // Creates a mapping between the quality control strategy and the implementation. This uses
            // type and not a direct instantiation to prevent to prevent more complicated classes interfering with
            // each other should this algorithm ultimately be paralleled.
            var qualityAlgorithmFactory = new QualityAlgorithmFactory(new Dictionary<QualityStrategy, Type>()
            {
                {QualityStrategy.Stable, typeof(StableQualityAlgorithm)},
                {QualityStrategy.LinearDecrease, typeof(LinearDecreaseAlgorithm)},
                {QualityStrategy.LinearIncrease, typeof(LinearIncreaseAlgorithm)},
                {QualityStrategy.RapidDecrease, typeof(RapidDecreaseAlgorithm)},
                {QualityStrategy.IncreasingUntilSellBy, typeof(IncreasingValueUntilSellByAlgorithm)},
            });

            // Creates a mapping between the shelf life strategy and the implementation.
            var shelfLifeAlgorithmFactory = new ShelfLifeAlgorithmFactory(new Dictionary<ShelfLifeStrategy, Type>()
            {
                {ShelfLifeStrategy.Stable, typeof(StableShelfLifeAlgorithm)},
                {ShelfLifeStrategy.LinearDecrease, typeof(LinearDecreaseShelfLifeAlgorithm)}
            });

            // Creates a mapping between an item of stock and it's corresponding stock management strategy.
            var itemFactory = new ItemFactory(new Dictionary<string, StockManagementStrategy>()
            {
                {
                    "Aged Brie",
                    new StockManagementStrategy(QualityStrategy.LinearIncrease, ShelfLifeStrategy.LinearDecrease)
                },
                {
                    "Backstage passes",
                    new StockManagementStrategy(QualityStrategy.IncreasingUntilSellBy, ShelfLifeStrategy.LinearDecrease)
                },
                { "Sulfuras", new StockManagementStrategy(QualityStrategy.Stable, ShelfLifeStrategy.Stable)},
                {
                    "Normal Item",
                    new StockManagementStrategy(QualityStrategy.LinearDecrease, ShelfLifeStrategy.LinearDecrease)
                },
                {
                    "Conjured",
                    new StockManagementStrategy(QualityStrategy.RapidDecrease, ShelfLifeStrategy.LinearDecrease)
                }
            });

            // Define a list of quality control steps that must be run every time
            var qualityControl = new List<IQualityAlgorithm>()
            {
                new QualityNeverNegativeAlgorithm(),
                new QualityNeverAboveThresholdAlgorithm(50)
            };

            // And now the object that creates the factory for the item.
            var qualityFactory = new QualityPipelineFactory(qualityAlgorithmFactory, qualityControl);

            // Create an object that can parse the text. If multiple formats are required this could be created via
            // a factory object. For now, we only have one format
            var stringParse = new SpaceSeparatedParser();

            // Create the processing object that performs the main business logic
            var itemProcessor = new ItemProcessor(qualityFactory, shelfLifeAlgorithmFactory);

            // Create the object that can convert between our parsed item and the real inventory item
            var itemToInventoryParse = new ParsedItemToInventoryItemConverter(itemFactory);
            
            try
            {
                // Open the input file
                await using var inputStream = new FileStream(args[0], FileMode.Open);

                // Create the output file
                await using var outputStream = new FileStream(args[1], FileMode.Create);
                await using var outputStreamWriter = new StreamWriter(outputStream);

                // Create the object that performs the final output
                var outputProcessor = new OutputProcessor(outputStreamWriter);
                
                // Run the pipeline by awaiting on the observable
                await create_observable_stream(inputStream, stringParse, itemToInventoryParse, itemProcessor, outputProcessor);
                
                Console.WriteLine("File processed.");

                // The file was processed successfully. We do not consider any parsing errors as significant (since
                // the output file has been marked accordingly).
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"The process could not be completed because an error occurred. Exception: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Creates the observable that pulls everything together
        /// </summary>
        /// <param name="inputStream">The file to be read</param>
        /// <param name="stringParse">The parser for line items</param>
        /// <param name="itemToInventoryParse">The converter that turns parsed strings into inventory items</param>
        /// <param name="itemProcessor">The main business logic</param>
        /// <param name="outputProcessor">The output parser</param>
        /// <returns>The observable</returns>
        private static IObservable<bool> create_observable_stream(FileStream inputStream, IItemStringParser stringParse,
            IParsedItemToInventoryItemConverter itemToInventoryParse, IItemProcessor itemProcessor,
            OutputProcessor outputProcessor)
        {
            // Create an observable that will pull the entire process together. Using a stream ensures that memory
            // is not committed for the entire file. This will allow very large files to be processed
            var observable = Observable.Using(
                    () => new StreamReader(inputStream),
                    reader => Observable.FromAsync(reader.ReadLineAsync)
                        .Repeat()
                        .TakeWhile(line => line != null))
                .Select(stringParse.Parse) // Parse the string into its individual parts
                .Select(parsedItem =>
                {
                    // Turn the individual parts into a full inventory item
                    try
                    {
                        // This step will create our real inventory item. If this fails (i.e. the type of the item
                        // is not recognised) an exception will be thrown.
                        return itemToInventoryParse.Convert(parsedItem);
                    }
                    catch (Exception)
                    {
                        // Returning null will allow the pipeline to continue. This will be handled correctly
                        return null;
                    }
                })
                .Select(itemProcessor.Process) // Perform business logic on inventory item
                .Select(outputProcessor.Process); // Output to file
            return observable;
        }
    }
}