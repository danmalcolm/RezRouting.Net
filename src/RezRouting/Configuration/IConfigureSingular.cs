﻿namespace RezRouting.Configuration
{
    /// <summary>
    /// Configures a singulartype resource
    /// </summary>
    public interface IConfigureSingular : IConfigureResource, IConfigureChildren
    {
        /// <summary>
        /// Sets a custom path within the URL that identifies this resource.
        /// If a custom path is not set, then the URL is based on the name of the
        /// resource.
        /// </summary>
        /// <param name="path"></param>
        void UrlPath(string path);
    }
}