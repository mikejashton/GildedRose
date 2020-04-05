namespace GildedRose.Interfaces
{
    /// <summary>
    /// Creates quality algorithms from the strategy
    /// </summary>
    public interface IQualityAlgorithmFactory
    {
        /// <summary>
        /// Creates a new quality algorithm
        /// </summary>
        /// <param name="strategyType">The strategy type</param>
        /// <returns>The algorithm</returns>
        IQualityAlgorithm Create(QualityStrategy strategyType);
    }
}