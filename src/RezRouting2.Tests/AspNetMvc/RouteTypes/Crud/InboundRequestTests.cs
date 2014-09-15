using System.Web.Routing;
using FluentAssertions;
using RezRouting2.AspNetMvc;
using RezRouting2.Tests.AspNetMvc.RouteTypes.Crud.TestModel;
using RezRouting2.Tests.Infrastructure;
using Xunit.Extensions;

namespace RezRouting2.Tests.AspNetMvc.RouteTypes.Crud
{
    public class InboundRequestTests
    {
        private static readonly RouteCollection Routes = new RouteCollection();

        static InboundRequestTests()
        {
            var mapper = CrudResourceModel.Configure();
            new MvcRouteMapper().CreateRoutes(mapper.Build(), Routes);
        }
        
        [Theory]
        [InlineData("GET", "/products", "Products#Index", null)]
        [InlineData("GET", "/products/new", "Products#New", null)]
        [InlineData("POST", "/products", "Products#Create", null)]
        [InlineData("GET", "/products/123", "Product#Show", "123")]
        [InlineData("GET", "/products/123/edit", "Product#Edit", "123")]
        [InlineData("PUT", "/products/123", "Product#Update", "123")]
        [InlineData("DELETE", "/products/123", "Product#Delete", "123")]
        [InlineData("GET", "/profile/new", "Profile#New", null)]
        [InlineData("POST", "/profile", "Profile#Create", null)]
        [InlineData("GET", "/profile", "Profile#Show", null)]
        [InlineData("GET", "/profile/edit", "Profile#Edit", null)]
        [InlineData("PUT", "/profile", "Profile#Update", null)]
        [InlineData("DELETE", "/profile", "Profile#Delete", null)]
        public void should_map_requests_to_controller_actions(string httpMethod, string path, string controllerAction, string id)
        {
            var httpContext = TestHttpContextBuilder.Create(httpMethod, path);
            var routeData = Routes.GetRouteData(httpContext);
            routeData.Should().NotBeNull("request should match a route");
            var expectedAction = ControllerActionInfo.Parse(controllerAction);
            var actualAction = new ControllerActionInfo(routeData.Values);
            actualAction.Controller.Should().BeEquivalentTo(expectedAction.Controller, "it should map to expected controller");
            actualAction.Action.Should().BeEquivalentTo(expectedAction.Action, "it should map to expected action");
            if (id != null)
            {
                var actualValues = new RouteValueDictionary(routeData.Values);
                actualValues.Remove("controller");
                actualValues.Remove("action");
                var expectedValues = new RouteValueDictionary { {"id", id} };
                actualValues.ShouldBeEquivalentTo(expectedValues, "route data should contain expected additional route values");
            }
        }
    }
}