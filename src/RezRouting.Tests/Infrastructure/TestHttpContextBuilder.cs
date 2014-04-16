using System;
using System.Collections.Specialized;
using System.Web;
using Moq;

namespace RezRouting.Tests.Infrastructure
{
    public static class TestHttpContextBuilder
    {
        public static HttpContextBase Create(string methodAndPath, NameValueCollection headers = null, NameValueCollection form = null)
        {
            var requestParts = methodAndPath.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (requestParts.Length != 2)
                throw new ArgumentException("Request should be in the form [METHOD] [PATH], e.g. \"GET /users\"", "methodAndPath");
            string httpMethod = requestParts[0];
            string path = requestParts[1];

            var uri = new Uri("http://www.tempuri.org" + path, UriKind.Absolute);
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(c => c.Request.ApplicationPath).Returns("/");
            httpContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns("~" + uri.LocalPath);
            httpContext.Setup(c => c.Request.Url).Returns(uri);
            httpContext.Setup(c => c.Request.PathInfo).Returns("");
            httpContext.Setup(c => c.Request.ServerVariables).Returns(new NameValueCollection());
            var queryString = HttpUtility.ParseQueryString(uri.Query);
            httpContext.Setup(x => x.Request.QueryString).Returns(queryString);
            headers = headers ?? new NameValueCollection();
            httpContext.Setup(x => x.Request.Headers).Returns(headers);
            httpContext.Setup(x => x.Request.HttpMethod).Returns(httpMethod);
            form = form ?? new NameValueCollection();
            httpContext.Setup(x => x.Request.Form).Returns(form);
            httpContext.Setup(x => x.Request.Unvalidated.Form).Returns(form);
            httpContext.Setup(x => x.Response.ApplyAppPathModifier(It.IsAny<string>())).Returns((string x) => x);
            return httpContext.Object;
        } 
    }
}