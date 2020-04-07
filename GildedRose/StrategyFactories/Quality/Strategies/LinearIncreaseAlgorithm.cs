using GildedRose.Interfaces;
 
 namespace GildedRose.StrategyFactories.Quality.Strategies
 {
     /// <summary>
     /// Quality increases by one
     /// </summary>
     public class LinearIncreaseAlgorithm : IQualityAlgorithm
     {
         /// <summary>
         /// Runs the algorithm
         /// </summary>
         /// <param name="item">The item to be maintained</param>
         /// <param name="qualityMaintainer">The item to be maintained</param>
         public void Run(IInventoryItem item, IQualityMaintenance qualityMaintainer)
         {
             qualityMaintainer.Quality++;
         }
     }
 }