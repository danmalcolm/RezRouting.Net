using System.Linq;
using FluentAssertions;
using RezRouting.Routing;
using RezRouting.Tests.Infrastructure.Assertions;
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

        [Fact]
        public void ShouldNotMapRemovedRouteType()
        {
            mapper.Configure(config => config.RemoveRouteType("Edit"));
            mapper.ShouldMapRoutesWithNames("Orders.Index", "Orders.Show", "Orders.New", "Orders.Create",
                "Orders.Update", "Orders.Delete", "Orders.Customer.Show", "Orders.Customer.New", 
                "Orders.Customer.Create", "Orders.Customer.Update", "Orders.Customer.Delete");
        }

        [Fact]
        public void ShouldNotMapRemovedRouteTypes()
        {
            mapper.Configure(config => config.RemoveRouteTypes("Edit", "Show"));
            mapper.ShouldMapRoutesWithNames("Orders.Index", "Orders.New", "Orders.Create",
                "Orders.Update", "Orders.Delete", "Orders.Customer.New",
                "Orders.Customer.Create", "Orders.Customer.Update", "Orders.Customer.Delete");
        }
    }
}