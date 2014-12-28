using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class ResourceBuilderOptionConfigurationTests
    {
        private static ResourcesBuilder CreateBuilder()
        {
            var convention = new TestRouteConvention("Route1", "Action1", "GET", "action1");

            var builder = new ResourcesBuilder();
            builder.RouteConventions(convention);
            return builder;
        }

        [Fact]
        public void should_customise_url_formatting_using_options()
        {
            var builder = CreateBuilder();

            builder.Collection("FineProducts", products => products.HandledBy<TestController1>());
            builder.Options(options => options.FormatUrlPaths(new UrlPathSettings(caseStyle:CaseStyle.Upper, wordSeparator: "_")));
            var model = builder.Build();

            var routeUrl = model.Resources.Single().Routes.Single().Url;
            routeUrl.Should().Be("FINE_PRODUCTS/action1");
        }

        [Fact]
        public void should_customise_id_names_using_options()
        {
            var builder = CreateBuilder();
            builder.Collection("Products", products => products.Items(product => product.HandledBy<TestController1>()));
            builder.Options(options => options.CustomiseIdNames(new DefaultIdNameConvention("code", true)));
            var model = builder.Build();

            var resourceUrl = model.Resources.Single().Children.Single(x => x.Level == ResourceLevel.CollectionItem).Url;
            resourceUrl.Should().Be("products/{productCode}");
        }


        public class TestController1 : Controller
        {
            
        }
    }

}