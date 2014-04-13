using System.Collections.Generic;
using RezRouting.Tests.RouteMapping.TestControllers.Orders;
using RezRouting.Tests.Shared.Expectations;
using Xunit.Extensions;

namespace RezRouting.Tests.RouteMapping
{
    public class NestedResourceRouteTests
    {
        [Theory, PropertyData("Collections2LevelsDeepData")]
        public void Collections2LevelsDeep(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> Collections2LevelsDeepData
        {
            get
            {
                var builder = new RootResourceBuilder();
                builder.Collection(orders =>
                {
                    orders.HandledBy<OrdersController>();
                    orders.Collection(notes => notes.HandledBy<NotesController>());
                });
                return new MappingExpectations(builder.MapRoutes())
                    .ExpectMatch("GET orders", "Orders.Index", "Orders#Index")
                    .ExpectMatch("GET orders/123", "Orders.Show", "Orders#Show", new { id = "123" })
                    .ExpectMatch("GET orders/new", "Orders.New", "Orders#New")
                    .ExpectMatch("POST orders", "Orders.Create", "Orders#Create")
                    .ExpectMatch("GET orders/123/edit", "Orders.Edit", "Orders#Edit", new { id = "123" })
                    .ExpectMatch("PUT orders/123", "Orders.Update", "Orders#Update", new { id = "123" })
                    .ExpectMatch("DELETE orders/123", "Orders.Delete", "Orders#Destroy", new { id = "123" })
                    // child collection
                    .ExpectMatch("GET orders/123/notes", "Orders.Notes.Index", "Notes#Index", new { orderId = "123" })
                    .ExpectMatch("GET orders/123/notes/456", "Orders.Notes.Show", "Notes#Show", new { orderId = "123", id = "456" })
                    .ExpectMatch("GET orders/123/notes/new", "Orders.Notes.New", "Notes#New", new { orderId = "123" })
                    .ExpectMatch("POST orders/123/notes", "Orders.Notes.Create", "Notes#Create", new { orderId = "123" })
                    .ExpectMatch("GET orders/123/notes/456/edit", "Orders.Notes.Edit", "Notes#Edit", new { orderId = "123", id = "456" })
                    .ExpectMatch("PUT orders/123/notes/456", "Orders.Notes.Update", "Notes#Update", new { orderId = "123", id = "456" })
                    .ExpectMatch("DELETE orders/123/notes/456", "Orders.Notes.Delete", "Notes#Destroy", new { orderId = "123", id = "456" })
                    .AsPropertyData();
            }
        }

        [Theory, PropertyData("Collections3LevelsDeepData")]
        public void Collections3LevelsDeep(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> Collections3LevelsDeepData
        {
            get
            {
                var builder = new RootResourceBuilder();
                builder.Collection(orders =>
                {
                    orders.HandledBy<OrdersController>();
                    orders.Collection(notes =>
                    {
                        notes.HandledBy<NotesController>();
                        notes.Collection(comments => comments.HandledBy<CommentsController>());
                    });
                });
                return new MappingExpectations(builder.MapRoutes())
                    .ExpectMatch("GET orders", "Orders.Index", "Orders#Index")
                    .ExpectMatch("GET orders/123", "Orders.Show", "Orders#Show", new { id = "123" })
                    .ExpectMatch("GET orders/new", "Orders.New", "Orders#New")
                    .ExpectMatch("POST orders", "Orders.Create", "Orders#Create")
                    .ExpectMatch("GET orders/123/edit", "Orders.Edit", "Orders#Edit", new { id = "123" })
                    .ExpectMatch("PUT orders/123", "Orders.Update", "Orders#Update", new { id = "123" })
                    .ExpectMatch("DELETE orders/123", "Orders.Delete", "Orders#Destroy", new { id = "123" })
                    // level 2
                    .ExpectMatch("GET orders/123/notes", "Orders.Notes.Index", "Notes#Index", new { orderId = "123" })
                    .ExpectMatch("GET orders/123/notes/456", "Orders.Notes.Show", "Notes#Show", new { orderId = "123", id = "456" })
                    .ExpectMatch("GET orders/123/notes/new", "Orders.Notes.New", "Notes#New", new { orderId = "123" })
                    .ExpectMatch("POST orders/123/notes", "Orders.Notes.Create", "Notes#Create", new { orderId = "123" })
                    .ExpectMatch("GET orders/123/notes/456/edit", "Orders.Notes.Edit", "Notes#Edit", new { orderId = "123", id = "456" })
                    .ExpectMatch("PUT orders/123/notes/456", "Orders.Notes.Update", "Notes#Update", new { orderId = "123", id = "456" })
                    .ExpectMatch("DELETE orders/123/notes/456", "Orders.Notes.Delete", "Notes#Destroy", new { orderId = "123", id = "456" })
                    // level 3
                    .ExpectMatch("GET orders/123/notes/456/comments", "Orders.Notes.Comments.Index", "Comments#Index", 
                        new { orderId = "123", noteId = "456" })
                    .ExpectMatch("GET orders/123/notes/456/comments/789", "Orders.Notes.Comments.Show", "Comments#Show", 
                        new { orderId = "123", noteId = "456", id ="789" })
                    .ExpectMatch("GET orders/123/notes/456/comments/new", "Orders.Notes.Comments.New", "Comments#New", 
                        new { orderId = "123", noteId = "456" })
                    .ExpectMatch("POST orders/123/notes/456/comments", "Orders.Notes.Comments.Create", "Comments#Create", 
                        new { orderId = "123", noteId = "456" })
                    .ExpectMatch("GET orders/123/notes/456/comments/789/edit", "Orders.Notes.Comments.Edit", "Comments#Edit", 
                        new { orderId = "123", noteId = "456", id = "789" })
                    .ExpectMatch("PUT orders/123/notes/456/comments/789", "Orders.Notes.Comments.Update", "Comments#Update",
                        new { orderId = "123", noteId = "456", id = "789" })
                    .ExpectMatch("DELETE orders/123/notes/456/comments/789", "Orders.Notes.Comments.Delete", "Comments#Destroy",
                        new { orderId = "123", noteId = "456", id = "789" })
                    .AsPropertyData();
            }
        }

        [Theory, PropertyData("SingularWithinChildData")]
        public void SingularWithinChild(MappingExpectation expectation)
        {
            expectation.Verify();
        }

        public static IEnumerable<object[]> SingularWithinChildData
        {
            get
            {
                var root = new RootResourceBuilder();
                root.Collection(orders =>
                {
                    orders.HandledBy<OrdersController>();
                    orders.Singular(customer => customer.HandledBy<CustomerController>());
                });

                return new MappingExpectations(root.MapRoutes())
                    .ExpectMatch("GET orders", "Orders.Index", "Orders#Index")
                    .ExpectMatch("GET orders/123", "Orders.Show", "Orders#Show", new { id = "123" })
                    .ExpectMatch("GET orders/new", "Orders.New", "Orders#New")
                    .ExpectMatch("POST orders", "Orders.Create", "Orders#Create")
                    .ExpectMatch("GET orders/123/edit", "Orders.Edit", "Orders#Edit", new { id = "123" })
                    .ExpectMatch("PUT orders/123", "Orders.Update", "Orders#Update", new { id = "123" })
                    .ExpectMatch("DELETE orders/123", "Orders.Delete", "Orders#Destroy", new { id = "123" })
                    // child singular resource
                    .ExpectMatch("GET orders/123/customer", "Orders.Customer.Show", "Customer#Show", new { orderId = "123" })
                    .ExpectMatch("GET orders/123/customer/new", "Orders.Customer.New", "Customer#New", new { orderId = "123" })
                    .ExpectMatch("POST orders/123/customer", "Orders.Customer.Create", "Customer#Create", new { orderId = "123" })
                    .ExpectMatch("GET orders/123/customer/edit", "Orders.Customer.Edit", "Customer#Edit", new { orderId = "123" })
                    .ExpectMatch("PUT orders/123/customer", "Orders.Customer.Update", "Customer#Update", new { orderId = "123" })
                    .ExpectMatch("DELETE orders/123/customer", "Orders.Customer.Delete", "Customer#Destroy", new { orderId = "123"})
                    .AsPropertyData();
            }
        }
    }
}