using RezRouting.Tests.Shared.Assertions;
using RezRouting.Configuration;
using RezRouting.Tests.RouteMapping.TestControllers.Users;
using Xunit;

namespace RezRouting.Tests.RouteMapping
{
    /// <summary>
    /// Tests global configuration around resource naming
    /// </summary>
    public class ResourcePathConfigurationTests
    {
        private readonly RootResourceBuilder builder;

        public ResourcePathConfigurationTests()
        {
            builder = new RootResourceBuilder();
            builder.Collection(users => users.HandledBy<UsersController>());
        }

        [Fact]
        public void ShouldUseCustomSettingsForResourcePath()
        {
            builder.Configure(config => config.FormatResourcePaths(new ResourcePathSettings(CaseStyle.Upper)));
           
            builder.ShouldMapRoutesWithUrls("USERS", "USERS/{id}", "USERS/new", "USERS", "USERS/{id}/edit", "USERS/{id}", "USERS/{id}");
        }

        [Fact]
        public void ShouldUseCustomFormatterForResourcePath()
        {
            builder.Configure(config => config.FormatResourcePaths(new MyResourcePathFormatter()));

            builder.ShouldMapRoutesWithUrls("USERS", "USERS/{id}", "USERS/new", "USERS", "USERS/{id}/edit", "USERS/{id}", "USERS/{id}");
        }

        public class MyResourcePathFormatter : IResourcePathFormatter
        {
            public string GetResourcePath(string resourceName)
            {
                return resourceName.ToUpper();
            }
        }

        [Fact]
        public void ShouldUseCustomFunctionForResourcePath()
        {
            builder.Configure(config => config.FormatResourcePaths(name => name.ToUpper()));

            builder.ShouldMapRoutesWithUrls("USERS", "USERS/{id}", "USERS/new", "USERS", "USERS/{id}/edit", "USERS/{id}", "USERS/{id}");
        }
    }
}