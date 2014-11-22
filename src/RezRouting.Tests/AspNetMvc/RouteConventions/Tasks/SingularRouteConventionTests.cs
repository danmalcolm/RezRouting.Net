using System.Linq;
using RezRouting.AspNetMvc.RouteConventions.Tasks;
using RezRouting.Configuration;
using RezRouting.Resources;
using RezRouting.Tests.AspNetMvc.RouteConventions.Tasks.TestControllers.Profile;
using RezRouting.Tests.Infrastructure.Assertions;
using Xunit;

namespace RezRouting.Tests.AspNetMvc.RouteConventions.Tasks
{
    public class SingularRouteConventionTests
    {
        private readonly Resource resource;

        public SingularRouteConventionTests()
        {
            var scheme = new TaskRouteConventionBuilder();
            var mapper = new RouteMapper();
            mapper.RouteConventions(scheme.Build());
            mapper.Singular("Profile", profile =>
            {
                profile.HandledBy<DisplayProfileController>();
                profile.HandledBy<DeleteProfileController>();
                profile.HandledBy<EditProfileController>();
            });
            var model = mapper.Build();
            resource = model.Resources.Single();
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