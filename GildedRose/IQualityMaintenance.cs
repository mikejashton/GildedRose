namespace GildedRose
{
    /// <summary>
    /// Provides write access to an entity's quality score
    /// </summary>
    public interface IQualityMaintenance
    {
        /// <summary>
        /// Allows the quality score to be modified
        /// </summary>
        public int Quality {set; }
    }
}