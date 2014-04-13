namespace RezRouting
{
    /// <summary>
    /// Specifies the level to which a route applies on a collection resource
    /// </summary>
    public enum CollectionLevel
    {
        None,
        /// <summary>
        /// The route applies to a collection
        /// </summary>
        Collection,
        /// <summary>
        /// THe route applies to an individual resource within a collection
        /// </summary>
        Item
    }
}