using System;

namespace RezRouting.Demos.MvcWalkthrough2.DataAccess
{
    public class Manufacturer : Entity
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}