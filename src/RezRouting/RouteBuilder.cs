﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RezRouting
{
    public class RouteBuilder : IConfigureRoute
    {
        private Type controllerType;

        private bool configured = false;

        private string name = null;
        private string httpMethod = null;
        private string path = null;
        private string action = null;

        public RouteBuilder(Type controllerType)
        {
            this.controllerType = controllerType;
        }

        public void Configure(string name, string action, string httpMethod, string path)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (action == null) throw new ArgumentNullException("action");
            if (httpMethod == null) throw new ArgumentNullException("httpMethod");
            if (path == null) throw new ArgumentNullException("path");

            this.name = name;
            this.action = action;
            this.httpMethod = httpMethod;
            this.path = path;

            this.configured = true;
        }

        public Route Build()
        {
            if (!configured) return null;

            return new Route(name, controllerType, action, httpMethod, path);
        }
    }
}