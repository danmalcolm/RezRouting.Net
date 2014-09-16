﻿using System.Linq;
using FluentAssertions;
using RezRouting.Routing;

namespace RezRouting.Tests.Infrastructure.Assertions
{
    public static class ResourceBuilderAssertionExtensions
    {
        public static void ShouldMapRoutesWithControllerActions(this RouteMapper builder, params string[] expectedControllerActions)
        {
            var routes = builder.MapRoutes();
            var expected = expectedControllerActions.Select(ControllerActionInfo.Parse).ToArray();
            routes.OfType<ResourceActionRoute>().Select(r => new ControllerActionInfo(r.Defaults))
                .Should().BeEquivalentTo(expected);
        }

        public static void ShouldMapRoutesWithNames(this RouteMapper builder, params string[] expectedNames)
        {
            var routes = builder.MapRoutes();
            routes.OfType<ResourceActionRoute>().Select(r => r.Name).Should().BeEquivalentTo(expectedNames);
        }

        public static void ShouldMapRoutesWithUrls(this RouteMapper builder, params string[] expectedUrls)
        {
            var routes = builder.MapRoutes();
            routes.OfType<ResourceActionRoute>().Select(r => r.Url).Should().BeEquivalentTo(expectedUrls);
        }
    }
}