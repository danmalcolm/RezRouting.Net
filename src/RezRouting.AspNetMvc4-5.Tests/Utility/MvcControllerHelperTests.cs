using System.Web.Mvc;
using FluentAssertions;
using RezRouting.AspNetMvc.Utility;
using Xunit;

namespace RezRouting.AspNetMvc.Tests.Utility
{
    public class MvcControllerHelperTests
    {
        [Fact]
        public void should_identify_mvc_controller()
        {
            MvcControllerHelper.IsController(typeof (TestController1)).Should().BeTrue();
        }

        [Fact]
        public void should_only_identify_concrete_controller_types()
        {
            MvcControllerHelper.IsController(typeof(TestController2)).Should().BeFalse();
        } 
    }

    public class TestController1 : Controller
    {
        
    }

    public abstract class TestController2 : Controller
    {

    }
}