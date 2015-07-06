namespace RezRouting.Demos.MvcWalkthrough3.Controllers.Common
{
    /// <summary>
    /// Standard model class used to display a form used to execute a task. The Request
    /// property contains the values contained within the form elements that are submitted
    /// for handling
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public class TaskModel<TRequest>
    {
        public TRequest Request { get; set; }
    }
}