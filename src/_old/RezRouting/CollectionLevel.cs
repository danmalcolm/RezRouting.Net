namespace RezRouting
{
    /// <summary>
    /// Specifies the level to which a route applies to a collection resource
    /// </summary>
    public enum CollectionLevel
    {
        None,
        /// <summary>
        /// The route applies to the collection as a resource in itself
        /// </summary>
        Collection,
        /// <summary>
        /// THe route applies to an individual resource contained within the collection
        /// </summary>
        Item
    }
}