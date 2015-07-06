using System.Linq;
using RezRouting.Demos.MvcWalkthrough3.Controllers.Common;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Products
{
    public class ProductIndexController : EntityIndexController<DataAccess.Product,EntityCriteria>
    {
        protected override IQueryable<DataAccess.Product> ApplyFilter(IQueryable<DataAccess.Product> query, EntityCriteria criteria)
        {
            if (!string.IsNullOrWhiteSpace(criteria.Keyword))
            {
                string keyword = criteria.Keyword.Trim();
                query = query.Where(product => product.Name.Contains(keyword));
            }
            return query;
        }
    }
}