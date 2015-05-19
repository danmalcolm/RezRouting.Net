using System.Linq;
using RezRouting.Demos.MvcWalkthrough2.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough2.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Manufacturers.Manufacturer
{
    public class ManufacturerDetailsController : EntityDetailsController<DataAccess.Manufacturer, ManufacturerDetailsModel>
    {
        protected override ManufacturerDetailsModel CreateModel(DataAccess.Manufacturer entity)
        {
            var products = DemoData.Products.Where(x => x.Manufacturer == entity)
                .OrderByDescending(x => x.ModifiedOn)
                .Take(10)
                .ToList();
            return new ManufacturerDetailsModel
            {
                Manufacturer = entity,
                Products = products
            };
        }
    }
}