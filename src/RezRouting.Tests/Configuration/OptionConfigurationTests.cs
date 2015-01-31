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
    public class OptionConfigurationTests
    {
        [Fact]
        public void should_customise_url_formatting_using_options()
        {
            var root = RootResourceBuilder.Create("");
            root.Collection("FineProducts", products =>
                products.Route("Route2", MvcAction.For((TestController c) => c.Action1()), "GET", "action1"));
            
            var options = new ResourceOptions
            {
                UrlPathSettings = new UrlPathSettings(caseStyle: CaseStyle.Upper, wordSeparator: "_")
            };
            var rootResource = root.Build(options);

            var routeUrl = rootResource.Children.Single().Routes.Single().Url;
            routeUrl.Should().Be("FINE_PRODUCTS/action1");
        }

        [Fact]
        public void should_customise_id_names_using_options()
        {
            var root = RootResourceBuilder.Create("");
            root.Collection("Products", products => products.Items(product => product.HandledBy<TestController>()));
            var options = new ResourceOptions
            {
                IdNameConvention = new DefaultIdNameConvention("code", true)
            };
            var rootResource = root.Build(options);

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