using System;
using System.Linq;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Resources;

namespace RezRouting.AspNetMvc.Tests.Infrastructure.Assertions
{
    public static class ResourceAssertionExtensions
    {
        public static void ShouldContainMvcRoute(this Resource resource, string name, Type controllerType, string action, string httpMethod, string path)
        {
            resource.Routes.Should().ContainSingle(x => x.Name == name, "resource should contain route {0}", name);
            var route = resource.Routes.Single(x => x.Name == name);

            route.ShouldBeConfiguredAs(name, new MvcAction(controllerType, action), httpMethod, path);
        }
    }
}