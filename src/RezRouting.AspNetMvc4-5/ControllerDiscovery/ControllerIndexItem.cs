using System;

namespace RezRouting.AspNetMvc.ControllerDiscovery
{
    public class ControllerIndexItem
    {
        public ControllerIndexItem(string key, Type type)
        {
            Key = key;
            Type = type;
        }

        public string Key { get; set; }

        public Type Type { get; set; }

        public override string ToString()
        {
            return string.Format("Key: {0}, Type: {1}", Key, Type);
        }
    }
}