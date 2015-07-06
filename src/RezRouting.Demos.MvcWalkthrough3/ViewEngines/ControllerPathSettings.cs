namespace RezRouting.Demos.MvcWalkthrough3.ViewEngines
{
    // Adapted from https://github.com/danmalcolm/ControllerPathViewEngine
    public class ControllerPathSettings
    {
        public bool MergeNameIntoNamespace { get; private set; }

        /// <summary>
        /// Creates a new instance of ControllerPathSettings
        /// </summary>
        /// <param name="mergeNameIntoNamespace">Determines whether an extra folder is included if the
        /// name of the controller matches the containing namespace</param>
        public ControllerPathSettings(bool mergeNameIntoNamespace = false)
        {
            MergeNameIntoNamespace = mergeNameIntoNamespace;
        }
    }
}