using System;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;
using RezRouting.Demos.MvcWalkthrough3.Utility;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Manufacturers.Manufacturer
{
    public class EditManufacturerController : ItemTaskController<EditManufacturerRequest,DataAccess.Manufacturer>
    {
        protected override void PrepareRequest(EditManufacturerRequest request, DataAccess.Manufacturer entity)
        {
            request.Id = entity.Id;
            request.Name = entity.Name;
        }

        protected override ActionResult ExecuteTask(EditManufacturerRequest request, DataAccess.Manufacturer manufacturer)
        {
            manufacturer.Name = request.Name;
            manufacturer.ModifiedOn = DateTime.Now;

            TempData["alert-success"] = "Manufacturer Updated";
            return Redirect(Url.ResourceUrl((ManufacturerDetailsController c) => c.Show(manufacturer.Id)));
        }
    }
}