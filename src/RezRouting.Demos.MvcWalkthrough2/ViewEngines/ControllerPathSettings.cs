namespace RezRouting.Demos.MvcWalkthrough2.ViewEngines
{
    // Adapted from https://github.com/danmalcolm/ControllerPathViewEngine
    public class ControllerPathSettings
    {
        public bool MergeNameIntoNamespace { get; private set; }

        public ControllerPathSettings(bool mergeNameIntoNamespace = false)
        {
            MergeNameIntoNamespace = mergeNameIntoNamespace;
        }
    }
}