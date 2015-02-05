using System.Linq;
using RezRouting.AspNetMvc;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.Configuration;
using RezRouting.Resources;
using RezRouting.Tests.AspNetMvc.RouteConventions.Tasks.TestControllers.Profile;
using RezRouting.Tests.Infrastructure.Assertions.AspNetMvc;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Tasks
{
    public class SingularRouteConventionTests
    {
        private readonly Resource resource;

        public SingularRouteConventionTests()
        {
            var builder = RootResourceBuilder.Create("");
            builder.Singular("Profile", profile =>
            {
                profile.HandledBy<DisplayProfileController>();
                profile.HandledBy<DeleteProfileController>();
                profile.HandledBy<EditProfileController>();
            });
            builder.ApplyRouteConventions(new TaskRouteConventions());
            var root = builder.Build();
            resource = root.Children.Single();
        }

        [Fact]
        public void should_map_singular_display_route()
        {
            resource.ShouldContainMvcRoute("Show", typeof(DisplayProfileController), "Show", "GET", "");
        }

        [Fact]
        public void should_map_singular_task_edit_routes()
        {
            resource.ShouldContainMvcRoute("EditProfile.Edit", typeof(EditProfileController), "Edit", "GET", "edit");
            resource.ShouldContainMvcRoute("DeleteProfile.Edit", typeof(DeleteProfileController), "Edit", "GET", "delete");
        }

        [Fact]
        public void should_map_singular_task_handle_routes()
        {
            resource.ShouldContainMvcRoute("EditProfile.Handle", typeof(EditProfileController), "Handle", "POST", "edit");
            resource.ShouldContainMvcRoute("DeleteProfile.Handle", typeof(DeleteProfileController), "Handle", "POST", "delete");
        }
    }
}