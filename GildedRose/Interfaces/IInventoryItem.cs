namespace GildedRose.Interfaces
{
    /// <summary>
    /// Represents the quality strategy to be applied
    /// </summary>
    public enum QualityStrategy
    {
        /// <summary>
        /// Maintains a constant value 
        /// </summary>
        Stable = 0,
        
        /// <summary>
        /// Decreases the quality score by one each day
        /// </summary>
        LinearDecrease = 1,
        
        /// <summary>
        /// Increases the quality score by one each day
        /// </summary>
        LinearIncrease = 2,
        
        /// <summary>
        /// Increases the quality score by two each day
        /// </summary>
        RapidDecrease = 3,
        
        /// <summary>
        /// The value increases as the sell by approaches, dropping to zero afterwards
        /// </summary>
        IncreasingUntilSellBy = 4
    }

    /// <summary>
    /// Represents the sell-by strategy to be applied 
    /// </summary>
    public enum ShelfLifeStrategy
    {
        /// <summary>
        /// Maintains a constant value 
        /// </summary>
        Stable = 0,

        /// <summary>
        /// Decreases the sell by score by one each day
        /// </summary>
        LinearDecrease = 1,
    }

    /// <summary>
    /// Represents an item of inventory
    /// </summary>
    public interface IInventoryItem
    {
        /// <summary>
        /// The name of the item
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// The quality strategy applied to the item
        /// </summary>
        QualityStrategy QualityStrategy { get; }

        /// <summary>
        /// The sell by strategy applied to the item 
        /// </summary>
        ShelfLifeStrategy ShelfLifeStrategy { get;  }
        
        /// <summary>
        /// A measure of the quality of the item. This value may change as the item ages
        /// </summary>
        int Quality { get; }

        /// <summary>
        /// The number of days until the item should be sold
        /// </summary>
        int SellIn { get; }
    }
}