using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Routing;
using FluentAssertions;
using RezRouting2.AspNetMvc;
using RezRouting2.Tests.Infrastructure;
using Xunit;

namespace RezRouting2.Tests.AspNetMvc
{
    public class MvcRouteTests
    {
        private readonly RouteCollection routes;

        public MvcRouteTests()
        {
            var show = new RouteType("Show",
                (resource, type, route) => route.Configure(resource.Name + ".Show", "Show", "GET", ""));
            var edit = new RouteType("Edit",
                (resource, type, route) => route.Configure(resource.Name + ".Edit", "Edit", "GET", "edit"));
            var update = new RouteType("Update",
                (resource, type, route) => route.Configure(resource.Name + ".Update", "Update", "PUT", ""));
            var delete = new RouteType("Update",
                (resource, type, route) => route.Configure(resource.Name + ".Delete", "Delete", "DELETE", ""));
            var mapper = new RouteMapper();
            mapper.RouteTypes(show, edit, update, delete);
            mapper.Singular("Profile", profile => profile.HandledBy<ProfileController>());
            routes = new RouteCollection();
            new MvcRouteMapper().CreateRoutes(mapper.Build(), routes);
        }

        [Fact]
        public void should_match_get_request_with_correct_path()
        {
            var r = GetRoute("GET", "/profile");
            r.Should().NotBeNull();
            ((ResourceRoute)r.Route).Name.Should().Be("Profile.Show");
        }

        [Fact]
        public void should_match_delete_request_with_correct_path()
        {
            var r = GetRoute("DELETE", "/profile");
            ((ResourceRoute)r.Route).Name.Should().Be("Profile.Delete");
        }

        [Fact]
        public void should_match_put_request_with_correct_path()
        {
            var r = GetRoute("PUT", "/profile");
            ((ResourceRoute)r.Route).Name.Should().Be("Profile.Update");
        }

        [Fact]
        public void should_match_route_requiring_put_when_overriden_via_header_within_post_request()
        {
            var headers = new NameValueCollection { { "X-HTTP-Method-Override", "PUT" } };
            var r = GetRoute("POST", "/profile", headers: headers);
            ((ResourceRoute)r.Route).Name.Should().Be("Profile.Update");
        }

        [Fact]
        public void should_match_route_requiring_put_when_overriden_via_override_in_form_data_within_post_request()
        {
            var form = new NameValueCollection { { "X-HTTP-Method-Override", "PUT" } };
            var r = GetRoute("POST", "/profile", form: form);
            ((ResourceRoute)r.Route).Name.Should().Be("Profile.Update");
        }

        [Fact]
        public void should_match_route_requiring_put_when_overriden_via_method_in_form_data_within_post_request()
        {
            var form = new NameValueCollection { { "_method", "PUT" } };
            var r = GetRoute("POST", "/profile", form: form);
            ((ResourceRoute)r.Route).Name.Should().Be("Profile.Update");
        }

        [Fact]
        public void should_not_match_requests_with_invalid_path()
        {
            var r = GetRoute("GET", "/profile2");
            r.Should().BeNull();
        }

        [Fact]
        public void should_not_match_requests_with_invalid_http_method()
        {
            var r = GetRoute("PATCH", "/profile");
            r.Should().BeNull();
        }

        [Fact]
        public void should_not_match_request_for_path_without_http_method()
        {



        }

        private RouteData GetRoute(string httpMethod, string path, NameValueCollection headers = null, NameValueCollection form = null)
        {
            var httpContext = TestHttpContextBuilder.Create(httpMethod, path, headers, form);
            var routeData = routes.GetRouteData(httpContext);
            return routeData;
        }

        private class RouteResult
        {
            public RouteResult(RouteData routeData)
            {
                RouteData = routeData;
            }

            public RouteData RouteData { get; private set; }

            public bool Found
            {
                get
                {
                    return RouteData != null;
                }
            }

            public ResourceRoute Route
            {
                get
                {
                    return RouteData.Route as ResourceRoute;
                }
            }

            public string RouteName
            {
                get { return Route.Name; }
            }
        }

        private class ProfileController
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