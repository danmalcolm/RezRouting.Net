using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc.RouteConventions;
using RezRouting.Configuration;
using Xunit;

namespace RezRouting.AspNetMvc.Tests
{
    public class ResourceConfiguratorExtensionsTests
    {
        [Fact]
        public void when_specifying_controller_handling_resource_should_add_to_convention_data()
        {
            var builder = RootResourceBuilder.Create();
            builder.Controller<Controller1>();
            builder.Controller<Controller2>();
            builder.ExtensionData(data =>
            {
                data.Should().ContainKey(ExtensionDataKeys.ControllerTypes);
                var value = data[ExtensionDataKeys.ControllerTypes];
                value.Should().BeOfType<List<Type>>();
                ((List<Type>) value).Should().Equal(typeof (Controller1), typeof (Controller2));
            });
        }

        public class Controller1 : Controller
        {
        }

        public class Controller2 : Controller
        {
        }
    }
}