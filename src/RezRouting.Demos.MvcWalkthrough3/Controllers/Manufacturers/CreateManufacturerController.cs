using System;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Manufacturers
{
    public class CreateManufacturerController : TaskController<CreateManufacturerRequest>
    {
        protected override ActionResult ExecuteTask(CreateManufacturerRequest request)
        {
            var manufacturer = new DataAccess.Manufacturer
            {
                Id = DemoData.Manufacturers.Count + 1,
                Name = request.Name,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
            };
            DemoData.Manufacturers.Add(manufacturer);

            TempData["alert-success"] = "Manufacturer Created";
            return RedirectToIndex<ManufacturerIndexController>();
        }
    }
}