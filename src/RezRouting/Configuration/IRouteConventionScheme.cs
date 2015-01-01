﻿using System.Collections.Generic;

namespace RezRouting.Configuration
{
    /// <summary>
    /// Creates a set of IRouteConventions for configuring routes based on a specific pattern
    /// </summary>
    public interface IRouteConventionScheme
    {
        IEnumerable<IRouteConvention> GetConventions();
    }
}