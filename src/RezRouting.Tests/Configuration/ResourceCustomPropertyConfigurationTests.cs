using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RezRouting.Configuration;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class ResourceCustomPropertyConfigurationTests
    {
        [Fact]
        public void custom_properties_on_resource_should_be_empty_if_none_configured()
        {
            var resource = BuildResource(root => root.Singular("Profile", profile => { }));
           
            resource.CustomProperties.Should().BeEmpty();
        }

        [Fact]
        public void should_add_custom_properties_configured_to_resource()
        {
            var resource = BuildResource(root => root.Singular("Profile", profile =>
            {
                profile.CustomProperties(new Dictionary<string, object>
                {
                    { "key1", "value1" }, 
                    { "key2", "value2" }
                });
            }));

            var expectedData = new Dictionary<string, object>
            {
                { "key1", "value1" }, 
                { "key2", "value2" }
            };
            resource.CustomProperties.Should().Equal(expectedData);
        }

        [Fact]
        public void should_combine_all_custom_properties_configured_on_resource()
        {
            var resource = BuildResource(root => root.Singular("Profile", profile =>
            {
                profile.CustomProperties(new Dictionary<string, object> { { "key1", "value1" } });
                profile.CustomProperties(new Dictionary<string, object> { { "key2", "value2" } });
            }));
           
            var expectedData = new Dictionary<string, object>
            {
                { "key1", "value1" }, 
                { "key2", "value2" }
            };
            resource.CustomProperties.Should().Equal(expectedData);
        }

        /// <summary>
        /// Configures resources using supplied action and returns the first child resource
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        private Resource BuildResource(Action<IRootResourceBuilder> configure)
        {
            var builder = RootResourceBuilder.Create();
            configure(builder);
            var root = builder.Build();
            return root.Children.Single();
        }
    }
}