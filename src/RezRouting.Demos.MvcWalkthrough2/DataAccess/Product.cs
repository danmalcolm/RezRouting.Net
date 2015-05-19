using System;

namespace RezRouting.Demos.MvcWalkthrough2.DataAccess
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}