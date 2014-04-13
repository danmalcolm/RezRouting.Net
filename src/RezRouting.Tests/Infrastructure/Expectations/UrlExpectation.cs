using System.Text;
using System.Web.Mvc;
using FluentAssertions;

namespace RezRouting.Tests.Infrastructure.Expectations
{
    /// <summary>
    /// Defines expected outcome for URL generated from routes
    /// </summary>
    public class UrlExpectation
    {
        public static UrlExpectation ForRoute(UrlHelper urlHelper, string routeName, object additionalValues, string expectedUrl)
        {
            return new UrlExpectation
            {
                UrlHelper = urlHelper,
                Route = routeName,
                RouteValues = additionalValues,
                ExpectedUrl = expectedUrl
            };
        }

        public static UrlExpectation ForAction(UrlHelper urlHelper, string controller, string action, object additionalValues, string expectedUrl)
        {
            return new UrlExpectation
            {
                UrlHelper = urlHelper,
                Controller = controller,
                Action = action,
                RouteValues = additionalValues,
                ExpectedUrl = expectedUrl
            };
        }

        public UrlHelper UrlHelper { get; set; }

        public object RouteValues { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Route { get; set; }

        public string ExpectedUrl { get; set; }

        public void Verify()
        {
            string url = Route != null
                ? UrlHelper.RouteUrl(Route, RouteValues)
                : UrlHelper.Action(Action, Controller, RouteValues);

            if (ExpectedUrl != null)
            {
                url.Should().NotBeNull("a URL should be generated");
                url.ShouldBeEquivalentTo(ExpectedUrl, "should generate URL {0}", ExpectedUrl);
            }
            else
            {
                url.Should().BeNull("a URL should not be generated");
            }
        }

        public override string ToString()
        {
            var description = new StringBuilder();
            if (Route != null)
            {
                description.AppendFormat("Url for route {0}", Route);
            }
            else
            {
                description.AppendFormat("Url for {0}#{1}", Controller, Action);
            }
            
            if (RouteValues != null)
            {
                description.AppendFormat(" with values {0}", RouteValues);
            }

            if (ExpectedUrl != null)
            {
                description.AppendFormat(" should generate URL {0}", ExpectedUrl);
            }
            else
            {
                description.AppendFormat(" should not generate URL");
            }
            
            return description.ToString();
        }
    }
}