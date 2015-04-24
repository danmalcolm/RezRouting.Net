using System.Collections.Generic;
using RezRouting.Demos.MvcWalkthrough1.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough1.Controllers.Manufacturers
{
    public class ManufacturerDetailsModel
    {
        public Manufacturer Manufacturer { get; set; }
        public List<Product> Products { get; set; }
    }
}