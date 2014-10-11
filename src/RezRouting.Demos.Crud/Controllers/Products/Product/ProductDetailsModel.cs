using System.Collections.Generic;
using RezRouting.Demos.Crud.DataAccess;

namespace RezRouting.Demos.Crud.Controllers.Products.Product
{
    public class ProductDetailsModel
    {
        public DataAccess.Product Product { get; set; }

        public List<Review> Reviews { get; set; }
    }
}