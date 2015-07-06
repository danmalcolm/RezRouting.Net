using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Common
{
    public abstract class EntityDetailsController<TEntity,TModel> : Controller
        where TEntity : Entity
    {
        public ActionResult Show(int id)
        {
            var entity = GetEntity(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            var model = CreateModel(entity);
            return View(model);
        }

        protected virtual TEntity GetEntity(int id)
        {
            return DemoData.Get<TEntity>(id);
        }

        protected abstract TModel CreateModel(TEntity entity);
    }
}