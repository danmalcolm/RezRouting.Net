using System.Collections.Generic;
using RezRouting.Demos.Crud.DataAccess;

namespace RezRouting.Demos.Crud.Controllers.Manufacturers.Manufacturer
{
    public class ManufacturerDetailsModel
    {
        public DataAccess.Manufacturer Manufacturer { get; set; }
        public List<Product> Products { get; set; }
    }
}