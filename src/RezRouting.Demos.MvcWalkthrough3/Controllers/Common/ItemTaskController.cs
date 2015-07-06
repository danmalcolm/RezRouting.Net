using System.Web.Mvc;
using RezRouting.Demos.MvcWalkthrough3.DataAccess;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Common
{
    /// <summary>
    /// Base class for controllers displaying and handling tasks that operate on an
    /// individual item resource. Contains some shared logic for loading entity object
    /// and making it available during task display and execution.
    /// </summary>
    public abstract class ItemTaskController<TRequest,TEntity> : TaskController<TRequest> 
        where TRequest : new()
        where TEntity : Entity
    {
        protected sealed override void PrepareRequest(TRequest request)
        {
            var entity = GetEntity();
            PrepareRequest(request, entity);
        }

        protected virtual void PrepareRequest(TRequest request, TEntity entity)
        {
            
        }

        protected sealed override ActionResult ExecuteTask(TRequest request)
        {
            var entity = GetEntity();
            if (entity == null)
            {
                return HttpNotFound();
            }
            return ExecuteTask(request, entity);
        }

        protected sealed override TaskModel<TRequest> CreateModel(TRequest request)
        {
            var entity = GetEntity();
            return CreateModel(request, entity);
        }

        protected virtual TaskModel<TRequest> CreateModel(TRequest request, TEntity entity)
        {
            return base.CreateModel(request);
        }

        protected abstract ActionResult ExecuteTask(TRequest request, TEntity entity);

        protected virtual TEntity GetEntity()
        {
            int? id = RouteDataHelper.GetEntityId(typeof(TEntity), RouteData.Values);
            return id != null ? DemoData.Get<TEntity>(id.Value) : null;
        }
    }
}