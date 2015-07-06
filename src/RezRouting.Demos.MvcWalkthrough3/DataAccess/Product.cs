using System;

namespace RezRouting.Demos.MvcWalkthrough3.DataAccess
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public bool Published { get; set; }

        public string PublishComments { get; set; }

        public string ArchiveComments { get; set; }
    }
}