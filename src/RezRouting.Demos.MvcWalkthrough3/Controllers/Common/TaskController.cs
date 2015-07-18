using System;
using System.Web.Mvc;
using RezRouting.AspNetMvc.UrlGeneration;

namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Common
{
    public abstract class TaskController<TRequest> : Controller, ITaskController
        where TRequest : new()
    {
        public virtual ActionResult Edit(TRequest request)
        {
            ModelState.Clear();
            PrepareRequest(request);
            return DisplayForm(request);
        }

        protected virtual void PrepareRequest(TRequest request)
        {
            
        }

        protected ActionResult DisplayForm(TRequest request)
        {
            var model = CreateModel(request);
            var viewName = GetViewName();
            return View(viewName, model);
        }

        private string GetViewName()
        {
            string name = GetType().Name;
            if (name.EndsWith("controller", StringComparison.InvariantCultureIgnoreCase))
            {
                name = name.Substring(0, name.Length - 10);
            }
            return name;
        }

        protected virtual TaskModel<TRequest> CreateModel(TRequest request)
        {
            return new TaskModel<TRequest>
            {
                Request = request
            };
        }
        
        public ActionResult Handle(TRequest request)
        {
            // We're relying on built-in validators, but this could easily be extended to use
            // a library like FluentValidation
            if (!ModelState.IsValid)
            {
                return DisplayForm(request);
            }
            return ExecuteTask(request);
        }
        
        protected abstract ActionResult ExecuteTask(TRequest request);

        protected ActionResult RedirectToIndex<TController>() where TController : Controller
        {
            string url = Url.ResourceUrl<TController>("Index");
            return Redirect(url);
        }
    }
}