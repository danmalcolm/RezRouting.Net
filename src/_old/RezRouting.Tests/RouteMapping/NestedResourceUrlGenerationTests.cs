using System.Collections.Generic;
using RezRouting.Tests.Infrastructure.Expectations;
using RezRouting.Tests.Infrastructure.TestControllers.Orders;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    public class NestedResourceUrlGenerationTests
    {
        [Theory, PropertyData("NestedCollectionsExpectations")]
        public void ShouldCreateUrlsForNestedCollections(UrlExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> NestedCollectionsExpectations
        {
            get
            {
                var mapper = new RouteMapper();
                mapper.Collection(orders =>
                {
                    orders.HandledBy<OrdersController>();
                    orders.Collection(notes
                        => notes.HandledBy<NotesController>());
                });


                return new UrlExpectations(mapper.MapRoutes())
                    .ForRoute("Orders.Index", new { httpMethod = "GET" }, "/orders")
                    .ForAction("orders#index", new { httpMethod = "GET" }, "/orders")
                    .ForRoute("Orders.Show", new { httpMethod = "GET", id = "123" }, "/orders/123")
                    .ForAction("orders#show", new { httpMethod = "GET", id = "123" }, "/orders/123")
                    .ForRoute("Orders.New", new { httpMethod = "GET" }, "/orders/new")
                    .ForAction("orders#new", new { httpMethod = "GET" }, "/orders/new")
                    .ForRoute("Orders.Create", new { httpMethod = "POST" }, "/orders")
                    .ForAction("orders#create", new { httpMethod = "POST" }, "/orders")
                    .ForRoute("Orders.Edit", new { httpMethod = "GET", id = "123" }, "/orders/123/edit")
                    .ForAction("orders#edit", new { httpMethod = "GET", id = "123" }, "/orders/123/edit")
                    .ForRoute("Orders.Update", new { httpMethod = "PUT", id = "123" }, "/orders/123")
                    .ForAction("orders#update", new { httpMethod = "PUT", id = "123" }, "/orders/123")
                    .ForRoute("Orders.Delete", new { httpMethod = "DELETE", id = "123" }, "/orders/123")
                    .ForAction("orders#destroy", new { httpMethod = "DELETE", id = "123" }, "/orders/123")
                    // Level 2
                    .ForRoute("Orders.Notes.Index", new { httpMethod = "GET", orderId = "123" }, "/orders/123/notes")
                    .ForAction("notes#index", new { httpMethod = "GET", orderId = "123" }, "/orders/123/notes")
                    .ForRoute("Orders.Notes.Show", new { httpMethod = "GET", orderId = "123", id = "456" }, "/orders/123/notes/456")
                    .ForAction("notes#show", new { httpMethod = "GET", orderId = "123", id = "456" }, "/orders/123/notes/456")
                    .ForRoute("Orders.Notes.New", new { httpMethod = "GET", orderId = "123" }, "/orders/123/notes/new")
                    .ForAction("notes#new", new { httpMethod = "GET", orderId = "123" }, "/orders/123/notes/new")
                    .ForRoute("Orders.Notes.Create", new { httpMethod = "POST", orderId = "123" }, "/orders/123/notes")
                    .ForAction("notes#create", new { httpMethod = "POST", orderId = "123" }, "/orders/123/notes")
                    .ForRoute("Orders.Notes.Edit", new { httpMethod = "GET", orderId = "123", id = "456" }, "/orders/123/notes/456/edit")
                    .ForAction("notes#edit", new { httpMethod = "GET", orderId = "123", id = "456" }, "/orders/123/notes/456/edit")
                    .ForRoute("Orders.Notes.Update", new { httpMethod = "PUT", orderId = "123", id = "456" }, "/orders/123/notes/456")
                    .ForAction("notes#update", new { httpMethod = "PUT", orderId = "123", id = "456" }, "/orders/123/notes/456")
                    .ForRoute("Orders.Notes.Delete", new { httpMethod = "DELETE", orderId = "123", id = "456" }, "/orders/123/notes/456")
                    .ForAction("notes#destroy", new { httpMethod = "DELETE", orderId = "123", id = "456" }, "/orders/123/notes/456")
                    .AsPropertyData();
            }
        }

        [Theory, PropertyData("SingularInCollectionExpectations")]
        public void ShouldCreateUrlsForSingularInCollection(UrlExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> SingularInCollectionExpectations
        {
            get
            {
                var mapper = new RouteMapper();
                mapper.Collection(orders =>
                {
                    orders.HandledBy<OrdersController>();
                    orders.Singular(customer => customer.HandledBy<CustomerController>());
                });

                return new UrlExpectations(mapper.MapRoutes())
                    .ForRoute("Orders.Index", new { httpMethod = "GET" }, "/orders")
                    .ForAction("orders#index", new { httpMethod = "GET" }, "/orders")
                    .ForRoute("Orders.Show", new { httpMethod = "GET", id = "123" }, "/orders/123")
                    .ForAction("orders#show", new { httpMethod = "GET", id = "123" }, "/orders/123")
                    .ForRoute("Orders.New", new { httpMethod = "GET" }, "/orders/new")
                    .ForAction("orders#new", new { httpMethod = "GET" }, "/orders/new")
                    .ForRoute("Orders.Create", new { httpMethod = "POST" }, "/orders")
                    .ForAction("orders#create", new { httpMethod = "POST" }, "/orders")
                    .ForRoute("Orders.Edit", new { httpMethod = "GET", id = "123" }, "/orders/123/edit")
                    .ForAction("orders#edit", new { httpMethod = "GET", id = "123" }, "/orders/123/edit")
                    .ForRoute("Orders.Update", new { httpMethod = "PUT", id = "123" }, "/orders/123")
                    .ForAction("orders#update", new { httpMethod = "PUT", id = "123" }, "/orders/123")
                    .ForRoute("Orders.Delete", new { httpMethod = "DELETE", id = "123" }, "/orders/123")
                    .ForAction("orders#destroy", new { httpMethod = "DELETE", id = "123" }, "/orders/123")
                    // Level 2
                    .ForRoute("Orders.Customer.Show", new { httpMethod = "GET", orderId = "123" }, "/orders/123/customer")
                    .ForAction("customer#show", new { httpMethod = "GET", orderId = "123" }, "/orders/123/customer")
                    .ForRoute("Orders.Customer.New", new { httpMethod = "GET", orderId = "123" }, "/orders/123/customer/new")
                    .ForAction("customer#new", new { httpMethod = "GET", orderId = "123" }, "/orders/123/customer/new")
                    .ForRoute("Orders.Customer.Create", new { httpMethod = "POST", orderId = "123" }, "/orders/123/customer")
                    .ForAction("customer#create", new { httpMethod = "POST", orderId = "123" }, "/orders/123/customer")
                    .ForRoute("Orders.Customer.Edit", new { httpMethod = "GET", orderId = "123" }, "/orders/123/customer/edit")
                    .ForAction("customer#edit", new { httpMethod = "GET", orderId = "123" }, "/orders/123/customer/edit")
                    .ForRoute("Orders.Customer.Update", new { httpMethod = "PUT", orderId = "123" }, "/orders/123/customer")
                    .ForAction("customer#update", new { httpMethod = "PUT", orderId = "123" }, "/orders/123/customer")
                    .ForRoute("Orders.Customer.Delete", new { httpMethod = "DELETE", orderId = "123" }, "/orders/123/customer")
                    .ForAction("customer#destroy", new { httpMethod = "DELETE", orderId = "123" }, "/orders/123/customer")
                    .AsPropertyData();
            }
        }
    }
}
