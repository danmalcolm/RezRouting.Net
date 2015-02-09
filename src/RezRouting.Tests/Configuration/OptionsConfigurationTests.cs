using System.Linq;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.Configuration;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using Xunit;

namespace RezRouting.Tests.Configuration
{
    public class OptionsConfigurationTests
    {
        [Fact]
        public void should_customise_url_formatting_using_options()
        {
            var root = RootResourceBuilder.Create("");
            root.Collection("FineProducts", products =>
                products.Route("Route2", MvcAction.For((TestController c) => c.Action1()), "GET", "action1"));

            var settings = new UrlPathSettings(caseStyle: CaseStyle.Upper, wordSeparator: "_");
            root.Options(options => options.UrlPaths(settings));
            var rootResource = root.Build();

            var routeUrl = rootResource.Children.Single().Routes.Single().Url;
            routeUrl.Should().Be("FINE_PRODUCTS/action1");
        }

        [Fact]
        public void should_customise_id_names_using_options()
        {
            var root = RootResourceBuilder.Create("");
            root.Collection("Products", products => products.Items(product => product.HandledBy<TestController>()));

            var formatter = new DefaultIdNameFormatter("code", true);
            root.Options(options => options.IdFormat(formatter));
            var rootResource = root.Build();

            var resourceUrl = rootResource.Children.Single().Children.Single(x => x.Type == ResourceType.CollectionItem).Url;
            resourceUrl.Should().Be("products/{productCode}");
        }


        public class TestController : Controller
        {
            public ActionResult Action1()
            {
                return null;
            }
        }
    }

}