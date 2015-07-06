using System.Collections.Generic;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Manufacturers.Manufacturer
{
    public class ManufacturerDetailsModel
    {
        public DataAccess.Manufacturer Manufacturer { get; set; }
        public List<Product> Products { get; set; }
    }
}