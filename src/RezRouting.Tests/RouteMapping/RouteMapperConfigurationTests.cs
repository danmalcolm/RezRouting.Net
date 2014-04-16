using System.Linq;
using FluentAssertions;
using RezRouting.Routing;
using RezRouting.Tests.Infrastructure.TestControllers.Orders;
using Xunit;

namespace RezRouting.Tests.RouteMapping
{
    public class RouteMapperConfigurationTests
    {
        private RouteMapper mapper;

        public RouteMapperConfigurationTests()
        {
            mapper = new RouteMapper();
            mapper.Collection(orders =>
            {
                orders.HandledBy<OrdersController>();
                orders.Singular(customer => customer.HandledBy<CustomerController>());
            });
        }

        [Fact]
        public void ShouldAddRouteNamePrefix()
        {
            mapper.Configure(config => config.PrefixRouteNames("OrderProcessing"));
            var routes = mapper.MapRoutes();
            routes.Cast<ResourceActionRoute>().Should().OnlyContain(x => x.Name.StartsWith("OrderProcessing.Orders"));
        }
    }
}