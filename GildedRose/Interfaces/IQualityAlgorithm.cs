﻿namespace GildedRose.Interfaces
{
    /// <summary>
    /// An algorithm that acts upon an item's quality metric  
    /// </summary>
    public interface IQualityAlgorithm
    {
        /// <summary>
        /// Runs the algorithm
        /// </summary>
        /// <param name="qualityMaintainer">The objects that allows the quality to be queried and modified</param>
        public void Run(IQualityMaintenance qualityMaintainer);
    }
}