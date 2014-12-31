using System;
using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc;
using Xunit;

namespace RezRouting.Tests.AspNetMvc
{
    public class MvcActionTests
    {
        [Fact]
        public void should_create_instance_based_on_expression()
        {
            var action = MvcAction.For((TestController c) => c.Action1());

            action.ControllerType.Should().Be(typeof (TestController));
            action.ActionName.Should().Be("Action1");
        }

        [Fact]
        public void should_throw_if_attempting_to_create_using_invalid_expressions()
        {
            Action action = () => MvcAction.For((TestController c) => c.HttpContext.Request.Url.Equals(0));
            action.ShouldThrow<ArgumentException>();

            action = () => MvcAction.For((TestController c) => Equals(c.HttpContext.Request.Url, 0));
            action.ShouldThrow<ArgumentException>();
        }

        public class TestController : Controller
        {
            public ActionResult Action1()
            {
                return null;
            }
        }
    }
}