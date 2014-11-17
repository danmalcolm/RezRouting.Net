using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using RezRouting.Options;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests
{
    public class RouteMapperOptionsTests
    {
        private static RouteMapper CreateRouteMapper()
        {
            var routeType1 = new TestRouteType("RouteType1",
                (resource, type, route) => route.Configure("Route1", "Action1", "GET", "action1"));

            var mapper = new RouteMapper();
            mapper.RouteTypes(routeType1);
            return mapper;
        }

        [Fact]
        public void should_customise_url_formatting_using_options()
        {
            var mapper = CreateRouteMapper();

            mapper.Collection("FineProducts", products => products.HandledBy<TestController1>());
            mapper.Options(options => options.FormatUrlPaths(new UrlPathSettings(caseStyle:CaseStyle.Upper, wordSeparator: "_")));
            var model = mapper.Build();

            var routeUrl = model.Resources.Single().Routes.Single().Url;
            routeUrl.Should().Be("FINE_PRODUCTS/action1");
        }

        [Fact]
        public void should_customise_id_names_using_options()
        {
            var mapper = CreateRouteMapper();
            mapper.Collection("Products", products => products.Items(product => product.HandledBy<TestController1>()));
            mapper.Options(options => options.CustomiseIdNames(new DefaultIdNameConvention("code", true)));
            var model = mapper.Build();

            var resourceUrl = model.Resources.Single().Children.Single(x => x.Level == ResourceLevel.CollectionItem).Url;
            resourceUrl.Should().Be("products/{productCode}");
        }


        public class TestController1
        {
            
        }
    }

}