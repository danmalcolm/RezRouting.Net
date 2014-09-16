using System.Web.Mvc;
using FluentAssertions;
using RezRouting.Tests.Infrastructure;
using RezRouting.Tests.Infrastructure.TestControllers.Orders;
using Xunit;

namespace RezRouting.Tests.UrlGeneration
{
    public class ResourceUrlGenerationTests
    {
        private UrlHelper helper;

        public ResourceUrlGenerationTests()
        {
            var context = TestRequestContextBuilder.Create();
            var mapper = new RouteMapper();
            mapper.Collection(orders =>
            {
                orders.HandledBy<OrdersController>();
                orders.Collection(notes => notes.HandledBy<NotesController>());
            });
            var routes = mapper.MapRoutes();
            helper = new UrlHelper(context, routes);    
        }

        [Fact]
        public void should_generate_url_specified_by_controller_and_action()
        {
            string url = helper.ResourceUrl(typeof(OrdersController), "show", new {id = 123 });
            url.Should().Be("/orders/123");

        }

        [Fact]
        public void should_generate_url_specified_by_action_expression_tree()
        {
            string url = helper.ResourceUrl((OrdersController x) => x.Show("123"));
            url.Should().Be("/orders/123");
        }
    }
}