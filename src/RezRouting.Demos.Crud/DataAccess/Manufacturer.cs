using System;

namespace RezRouting.Demos.Crud.DataAccess
{
    public class Manufacturer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}