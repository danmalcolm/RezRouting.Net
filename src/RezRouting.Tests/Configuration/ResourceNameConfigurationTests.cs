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
            public override ResourceName GetResourceName(IEnumerable<Type> controllerTypes, ResourceType resourceType)
            {
                var def = base.GetResourceName(controllerTypes, resourceType);
                return new ResourceName("Nice" + def.Singular, "Nice" + def.Plural);
            }
        }

        [Fact]
        public void ShouldUseCustomFunctionForResourceNameIfSpecified()
        {
            builder.Configure(config => config.CustomiseResourceNames
                ((types, resourceType) => new ResourceName("Whatever")));

            builder.ShouldMapRoutesWithNames("Whatevers.Index", "Whatevers.Show", "Whatevers.New", "Whatevers.Create",
                "Whatevers.Edit", "Whatevers.Update", "Whatevers.Delete");
        }
    }
}