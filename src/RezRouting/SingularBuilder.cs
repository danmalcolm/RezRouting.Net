namespace RezRouting
{
    /// <summary>
    /// Configures routes for the actions available on a singular resource
    /// </summary>
    public class SingularBuilder : ResourceBuilder
    {
        protected override ResourceType ResourceType
        {
            get { return ResourceType.Singular; }
        }
    }
}