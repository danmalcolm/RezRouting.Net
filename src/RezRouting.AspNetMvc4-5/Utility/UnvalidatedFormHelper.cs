using System;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Helpers;

namespace RezRouting.AspNetMvc.Utility
{
    public class UnvalidatedFormHelper
    {
        private static readonly Func<HttpRequest, NameValueCollection> FormGetter;

        static UnvalidatedFormHelper()
        {
            FormGetter = CreateFormGetter();
        }

        public static NameValueCollection GetUnvalidatedForm(HttpRequestBase request)
        {
            var wrapper = request as HttpRequestWrapper;
            if (wrapper != null && HttpContext.Current != null)
            {
                // The Unvalidated method in System.Web.Helpers actually
                // uses HttpContext.Current and disregards the HttpRequestBase
                // parameter value
                return request.Unvalidated().Form;
            }
            else
            {
                return request.Form;
            }
        }

        private static Func<HttpRequest, NameValueCollection> CreateFormGetter()
        {
            var field = typeof(HttpRequest).GetField("_form", BindingFlags.Instance | BindingFlags.NonPublic); 
            var target = Expression.Parameter(typeof(NameValueCollection));
            var lookup = Expression.Field(target, field);
            var lambda = Expression.Lambda<Func<HttpRequest, NameValueCollection>>(lookup, target);
            return lambda.Compile();
        } 
    }
}