namespace RezRouting.Configuration
{
    /// <summary>
    /// Defines a component that handles one or more of a resource's routes. The RezRouting 
    /// core configuration is framework-neutral and concrete implementations of IResourceHandler 
    /// exist for each framework. Each resource's IResourceHandlers are defined at configuration 
    /// time and inspected by the route conventions being used to determine which actions 
    /// are supported. 
    /// </summary>
    public interface IResourceHandler
    {
         
    }
}