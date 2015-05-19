using System.Linq;
using RezRouting.Demos.MvcWalkthrough2.Controllers.Common;

namespace RezRouting.Demos.MvcWalkthrough2.Controllers.Manufacturers
{
    public class ManufacturerIndexController : EntityIndexController<DataAccess.Manufacturer,EntityCriteria>
    {
        protected override IQueryable<DataAccess.Manufacturer> ApplyFilter(IQueryable<DataAccess.Manufacturer> query, EntityCriteria criteria)
        {
            if (!string.IsNullOrWhiteSpace(criteria.Keyword))
            {
                string keyword = criteria.Keyword.Trim();
                query = query.Where(manufacturer => manufacturer.Name.Contains(keyword));
            }
            return query;
        }
    }
}