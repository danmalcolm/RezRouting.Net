namespace RezRouting.Demos.Crud.DataAccess
{
    public class User
    {
        public string UserName { get; set; }

        // HACK: This isn't an exercise in security - never do this
        public string Password { get; set; } 
    }
}