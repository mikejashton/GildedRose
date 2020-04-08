namespace GildedRose.Interfaces
{
    /// <summary>
    /// Creates shelf life algorithms from the strategy
    /// </summary>
    public interface IShelfLifeAlgorithmFactory
    {
        /// <summary>
        /// Creates a new shelf life algorithm
        /// </summary>
        /// <param name="strategyType">The strategy type</param>
        /// <returns>The algorithm</returns>
        IShelfLifeAlgorithm Create(SellByStrategy strategyType);
    }
}