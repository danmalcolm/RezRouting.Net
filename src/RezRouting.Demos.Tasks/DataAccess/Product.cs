using System;

namespace RezRouting.Demos.Tasks.DataAccess
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}