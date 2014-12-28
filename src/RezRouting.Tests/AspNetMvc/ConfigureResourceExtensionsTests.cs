using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using Moq;
using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.AspNetMvc
{
    public class ConfigureResourceExtensionsTests
    {
        [Fact]
        public void when_specifying_controller_should_add_handler_to_resource()
        {
            var resource = new TestResource();

            resource.HandledBy<Controller1>();

            resource.Handlers.Should().HaveCount(1);
            var handler = resource.Handlers.Single();
            handler.Should().BeOfType<MvcController>();
            var mvcHandler = (MvcController) handler;
            mvcHandler.ControllerType.Should().Be(typeof (Controller1));
        }

        public class Controller1 : Controller
        {
            
        }

        public class Controller2 : Controller
        {

        }

        // Test implementation used to test extensions
        public class TestResource : IConfigureResource
        {
            public List<IResourceHandler> Handlers = new List<IResourceHandler>();

            public void HandledBy(IResourceHandler handler)
            {
                Handlers.Add(handler);
            }
            
            public void HandledBy(Type type)
            {
                throw new NotImplementedException();
            }
            
            public void Route(string name, IRouteHandler handler, string httpMethod, string path,
                IDictionary<string, object> customProperties = null)
            {
                throw new NotImplementedException();
            }

            public void CustomProperties(IDictionary<string, object> properties)
            {
                throw new NotImplementedException();
            }
        }
    }
}