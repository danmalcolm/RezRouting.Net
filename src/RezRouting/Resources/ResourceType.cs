﻿namespace RezRouting.Resources
{
    /// <summary>
    /// Represents type of Resource
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// The root resource within an application, or section of an application
        /// </summary>
        Base,
        /// <summary>
        /// A single Resource identified by a URL
        /// </summary>
        Singular,
        /// <summary>
        /// A collection of Resources, typically identified by a plural URL
        /// </summary>
        Collection,
        /// <summary>
        /// An item within a Resource collection
        /// </summary>
        CollectionItem
    }
}