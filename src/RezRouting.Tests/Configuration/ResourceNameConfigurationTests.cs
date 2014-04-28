using System;
using System.Collections.Generic;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Assertions;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    /// <summary>
    /// Tests global configuration around resource naming
    /// </summary>
    public class ResourceNameConfigurationTests
    {
        private readonly RouteMapper builder;

        public ResourceNameConfigurationTests()
        {
            builder = new RouteMapper();
            builder.Collection(users => users.HandledBy<UsersController>());
        }

        [Fact]
        public void ShouldUseDefaultConventionForResourceNameWhenNotConfigured()
        {
            builder.ShouldMapRoutesWithNames("Users.Index", "Users.Show", "Users.New", "Users.Create", "Users.Edit", "Users.Update", "Users.Delete");
        }

        [Fact]
        public void ShouldUseCustomConventionForResourceNameWhenSpecified()
        {
            builder.Configure(config => config.CustomiseResourceNames(new MyResourceNameConvention()));

            builder.ShouldMapRoutesWithNames("NiceUsers.Index", "NiceUsers.Show", "NiceUsers.New", "NiceUsers.Create",
                "NiceUsers.Edit", "NiceUsers.Update", "NiceUsers.Delete");
        }

        public class MyResourceNameConvention : DefaultResourceNameConvention 
        {
            public override string GetResourceName(IEnumerable<Type> controllerTypes, ResourceType resourceType)
            {
                return "Nice" + base.GetResourceName(controllerTypes, resourceType);
            }
        }

        [Fact]
        public void ShouldUseCustomFunctionForResourceNameIfSpecified()
        {
            builder.Configure(config => config.CustomiseResourceNames
                ((types, resourceType) => "Whatever"));

            builder.ShouldMapRoutesWithNames("Whatever.Index", "Whatever.Show", "Whatever.New", "Whatever.Create",
                "Whatever.Edit", "Whatever.Update", "Whatever.Delete");
        }
    }
}