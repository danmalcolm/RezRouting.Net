using System;
using System.Linq;
using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Common
{
    public abstract class EntityIndexController<TEntity,TCriteria> : Controller
        where TEntity : Entity
        where TCriteria : EntityCriteria
    {
        public ActionResult Index(TCriteria criteria)
        {
            var query = DemoData.Query<TEntity>();
            query = ApplyFilter(query, criteria);
            query = ApplyPaging(query, criteria);
            var entities = query.ToList();

            // Probably need to project / map these entities to list item classes
            // rather than loading full entities - you get the idea though...

            var model = new EntityIndexModel<TEntity, TCriteria>
            {
                Items = entities,
                Criteria = criteria
            };
            // Could also support custom models via a virtual CreateModel method
            return View(model);
        }

        protected abstract IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TCriteria criteria);

        private IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TCriteria criteria)
        {
            int page = Math.Max(0, criteria.Page - 1);
            int pageSize = Math.Min(100, criteria.PageSize);
            return query.Skip(page).Take(pageSize);
        }
    }
}