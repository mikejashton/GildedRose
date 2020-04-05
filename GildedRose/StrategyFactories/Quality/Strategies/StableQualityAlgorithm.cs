using GildedRose.Interfaces;

namespace GildedRose.StrategyFactories.Quality.Strategies
{
    public class StableQualityAlgorithm : IQualityAlgorithm
    {
        public void Run(IQualityMaintenance qualityMaintainer)
        {
            // We don't do anything, because the value needs to remain stable.
        }
    }
}