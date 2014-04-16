using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure.Assertions;
using RezRouting.Tests.Infrastructure.TestControllers.Users;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    /// <summary>
    /// Tests global configuration around resource URL path formatting
    /// </summary>
    public class UrlPathConfigurationTests
    {
        private readonly RouteMapper builder;

        public UrlPathConfigurationTests()
        {
            builder = new RouteMapper();
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