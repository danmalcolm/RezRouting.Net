using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Routing;

namespace RezRouting.Tests.Infrastructure.Expectations
{
    /// <summary>
    /// Builds list of expectations to verify against a collection of routes
    /// </summary>
    public class MappingExpectations
    {
        private readonly RouteCollection routes;
        private readonly List<MappingExpectation> expectations = new List<MappingExpectation>();

        public static MappingExpectations For(RouteMapper mapper)
        {
            return new MappingExpectations(mapper.MapRoutes());
        }

        public MappingExpectations(RouteCollection routes)
        {
            this.routes = routes;
        }

        public MappingExpectations ExpectMatch(string methodAndPath, string routeName, string controllerAction,
            object otherRouteValues = null, NameValueCollection form = null, NameValueCollection headers = null, string desc = null)
        {
            var httpContext = TestHttpContextBuilder.Create(methodAndPath, headers, form);
            var expectation = MappingExpectation.Match(routes, httpContext, routeName, controllerAction, otherRouteValues, desc);
            expectations.Add(expectation);
            return this;
        }

        public MappingExpectations ExpectNoMatch(string methodAndPath, NameValueCollection form = null, NameValueCollection headers = null, string desc = null)
        {
            var httpContext = TestHttpContextBuilder.Create(methodAndPath, headers, form);
            var expectation = MappingExpectation.NoMatch(routes, httpContext, desc);
            expectations.Add(expectation);
            return this;
        }

        /// <summary>
        /// Gets a set of arguments for each expectation as input to an XUnit theory, which needs a sequence of object
        /// arrays.
        /// </summary>
        public IEnumerable<object[]> AsPropertyData()
        {
            return expectations.Select(expectation => new object[] { expectation });
        }
    }
}