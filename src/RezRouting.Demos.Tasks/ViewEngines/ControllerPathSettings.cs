namespace RezRouting.Demos.Tasks.ViewEngines
{
    public class ControllerPathSettings
    {
        public string RootNamespace { get; private set; }
        public string RootControllerNamespace { get; private set; }
        public bool MergeNameIntoNamespace { get; private set; }

        public ControllerPathSettings(string rootNamespace, string controllerSubNamespace = "Controllers", bool mergeNameIntoNamespace = false)
        {
            RootNamespace = rootNamespace;
            RootControllerNamespace = string.Format("{0}.{1}", rootNamespace, controllerSubNamespace);
            MergeNameIntoNamespace = mergeNameIntoNamespace;
        }

    }
}
