using System.Linq;
using RezRouting.AspNetMvc.RouteTypes.Tasks;
using RezRouting.Tests.AspNetMvc.RouteTypes.Tasks.TestControllers.Profile;
using RezRouting.Tests.Infrastructure.Assertions;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.RouteTypes.Tasks
{
    public class SingularRouteTypeTests
    {
        private readonly Resource resource;

        public SingularRouteTypeTests()
        {
            var scheme = new TaskRouteTypeBuilder();
            var mapper = new RouteMapper();
            mapper.RouteTypes(scheme.Build());
            mapper.Singular("Profile", profile =>
            {
                profile.HandledBy<DisplayProfileController>();
                profile.HandledBy<DeleteProfileController>();
                profile.HandledBy<EditProfileController>();
            });
            resource = mapper.Build().Single();
        }

        [Fact]
        public void should_map_singular_display_route()
        {
            resource.ShouldContainRoute("Show", typeof(DisplayProfileController), "Show", "GET", "");
        }

        [Fact]
        public void should_map_singular_task_edit_routes()
        {
            resource.ShouldContainRoute("EditProfile.Edit", typeof(EditProfileController), "Edit", "GET", "edit");
            resource.ShouldContainRoute("DeleteProfile.Edit", typeof(DeleteProfileController), "Edit", "GET", "delete");
        }

        [Fact]
        public void should_map_singular_task_handle_routes()
        {
            resource.ShouldContainRoute("EditProfile.Handle", typeof(EditProfileController), "Handle", "POST", "edit");
            resource.ShouldContainRoute("DeleteProfile.Handle", typeof(DeleteProfileController), "Handle", "POST", "delete");
        }
    }
}