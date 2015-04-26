using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.Tests.Infrastructure.Assertions;
using RezRouting.Configuration;
using RezRouting.Tests.Infrastructure;
using Xunit;

namespace RezRouting.AspNetMvc.Tests
{
    public class MvcRouteInboundMatchingTests
    {
        private static readonly RouteCollection Routes;

        static MvcRouteInboundMatchingTests()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Singular("Profile", profile =>
            {
                profile.Route("Show", "GET", "", MvcAction.For((ProfileController x) => x.Show()));
                profile.Route("Edit", "GET", "edit", MvcAction.For((ProfileController x) => x.Edit()));
                profile.Route("Update", "PUT", "", MvcAction.For((ProfileController x) => x.Update()));
                profile.Route("Delete", "DELETE", "", MvcAction.For((ProfileController x) => x.Delete()));
            });
            Routes = new RouteCollection();
            builder.MapMvcRoutes(Routes);
        }

        private RouteData GetRouteData(string httpMethod, string path, NameValueCollection headers = null, NameValueCollection form = null)
        {
            var httpContext = TestHttpContextBuilder.Create(httpMethod, path, headers, form);
            var routeData = Routes.GetRouteData(httpContext);
            return routeData;
        }

        [Fact]
        public void should_match_get_request_with_correct_path()
        {
            var routeData = GetRouteData("GET", "/profile");
            routeData.ShouldBeBasedOnRoute("Profile.Show");
        }

        [Fact]
        public void should_match_delete_request_with_correct_path()
        {
            var routeData = GetRouteData("DELETE", "/profile");
            routeData.ShouldBeBasedOnRoute("Profile.Delete");
        }

        [Fact]
        public void should_match_put_request_with_correct_path()
        {
            var routeData = GetRouteData("PUT", "/profile");
            routeData.ShouldBeBasedOnRoute("Profile.Update");
        }

        [Fact]
        public void should_match_route_requiring_put_when_overriden_via_header_within_post_request()
        {
            var headers = new NameValueCollection { { "X-HTTP-Method-Override", "PUT" } };
            var routeData = GetRouteData("POST", "/profile", headers: headers);
            routeData.ShouldBeBasedOnRoute("Profile.Update");
        }

        [Fact]
        public void should_match_route_requiring_put_when_overriden_via_override_in_form_data_within_post_request()
        {
            var form = new NameValueCollection { { "X-HTTP-Method-Override", "PUT" } };
            var routeData = GetRouteData("POST", "/profile", form: form);
            routeData.ShouldBeBasedOnRoute("Profile.Update");
        }

        [Fact]
        public void should_match_route_requiring_put_when_overriden_via_method_in_form_data_within_post_request()
        {
            var form = new NameValueCollection { { "_method", "PUT" } };
            var routeData = GetRouteData("POST", "/profile", form: form);
            routeData.ShouldBeBasedOnRoute("Profile.Update");
        }

        [Fact]
        public void should_not_match_requests_with_invalid_path()
        {
            var routeData = GetRouteData("GET", "/profile2");
            routeData.Should().BeNull();
        }

        [Fact]
        public void should_not_match_requests_with_invalid_http_method()
        {
            var routeData = GetRouteData("PATCH", "/profile");
            routeData.Should().BeNull();
        }
        
        private class ProfileController : Controller
        {
            public ActionResult Show()
            {
                return null;
            }

            public ActionResult Edit()
            {
                return null;
            }

            public ActionResult Update()
            {
                return null;
            }

            public ActionResult Delete()
            {
                return null;
            }

        }
    }
}