using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Configuration.Conventions;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.AspNetMvc
{
    public class ResourceConfiguratorExtensionsTests
    {
        [Fact]
        public void when_specifying_controller_handling_resource_should_add_to_convention_data()
        {
            var builder = new ResourceGraphBuilder();
            builder.HandledBy<Controller1>();
            builder.HandledBy<Controller2>();
            builder.ConventionData(data =>
            {
                data.Should().ContainKey(ConventionDataKeys.ControllerTypes);
                var value = data[ConventionDataKeys.ControllerTypes];
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