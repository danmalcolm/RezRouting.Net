using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Routing;

namespace RezRouting.Tests.Shared.Expectations
{
    /// <summary>
    /// Builds list of expectations to verify against a collection of routes
    /// </summary>
    public class MappingExpectations
    {
        private readonly RouteCollection routes;
        private readonly List<MappingExpectation> expectations = new List<MappingExpectation>();

        public MappingExpectations(RouteCollection routes)
        {
            this.routes = routes;
        }

        public MappingExpectations ExpectMatch(string request, string routeName, string controllerAction,
            object otherRouteValues = null, NameValueCollection form = null, NameValueCollection headers = null, string desc = null)
        {
            var testRequest = RouteTestingRequest.Create(request, headers, form);
            var expectation = MappingExpectation.Match(routes, testRequest, routeName, controllerAction, otherRouteValues, desc);
            expectations.Add(expectation);
            return this;
        }

        public MappingExpectations ExpectNoMatch(string request, NameValueCollection form = null, NameValueCollection headers = null, string desc = null)
        {
            var testRequest = RouteTestingRequest.Create(request, headers, form);
            var expectation = MappingExpectation.NoMatch(routes, testRequest, desc);
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