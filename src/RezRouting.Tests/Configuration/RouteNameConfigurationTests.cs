using System;
using System.Collections.Generic;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Assertions;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    /// <summary>
    /// Tests global configuration around route naming
    /// </summary>
    public class RouteNameConfigurationTests
    {
        private readonly RouteMapper mapper;

        public RouteNameConfigurationTests()
        {
            mapper = new RouteMapper();
            mapper.Collection(users => users.HandledBy<UsersController>());
        }

        [Fact]
        public void ShouldUseDefaultConventionForRouteNameWhenNotConfigured()
        {
            mapper.ShouldMapRoutesWithNames("Users.Index", "Users.Show", "Users.New", "Users.Create", "Users.Edit", "Users.Update", "Users.Delete");
        }

        [Fact]
        public void ShouldUseCustomConventionForRouteNameWhenSpecified()
        {
            mapper.Configure(config => config.CustomiseRouteNames(new MyRouteNameConvention()));

            mapper.ShouldMapRoutesWithNames("NiceUsers.Index", "NiceUsers.Show", "NiceUsers.New", "NiceUsers.Create",
                "NiceUsers.Edit", "NiceUsers.Update", "NiceUsers.Delete");
        }

        public class MyRouteNameConvention : DefaultRouteNameConvention 
        {
            public override string GetRouteName(IEnumerable<string> resourceNames, string routeTypeName, Type controllerType, bool includeController)
            {
                return "Nice" + base.GetRouteName(resourceNames, routeTypeName, controllerType, includeController);
            }
        }

        [Fact]
        public void ShouldUseCustomFunctionForRouteNameIfSpecified()
        {
            mapper.Configure(config => config.CustomiseRouteNames
                ((resourceNames, routeTypeName, controllerType, multiple) => "Whatever." + routeTypeName));

            mapper.ShouldMapRoutesWithNames("Whatever.Index", "Whatever.Show", "Whatever.New", "Whatever.Create",
                "Whatever.Edit", "Whatever.Update", "Whatever.Delete");
        }
    }
}