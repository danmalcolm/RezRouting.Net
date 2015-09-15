using System;
using RezRouting.Configuration.Options;
using RezRouting.Resources;
using RezRouting.Utility;

namespace RezRouting.Configuration.Builders
{
    /// <summary>
    /// Contains editable properties of a collection resource, a mutable representation used 
    /// during resource configuration by builders and extensions
    /// </summary>
    public class CollectionData : ResourceData
    {
        private string urlPath = null;
        
        public override ResourceType Type
        {
            get { return ResourceType.Collection; }
        }

        public string UrlPath
        {
            get
            {
                return urlPath;
            }
            set
            {
                if (value != null && !PathSegmentCleaner.IsValid(value))
                {
                    throw new ArgumentException("Path contains invalid characters. Only numbers, letters, hyphen and underscore characters can be used for a resource's path.", "path");
                }
                urlPath = value;
            }
        }

        protected override IUrlSegment GetUrlSegment(ConfigurationOptions options)
        {
            string path = urlPath ?? options.UrlPathSettings.FormatDirectoryName(Name);
            return new DirectoryUrlSegment(path);
        }

        protected override ResourceData CreateCopy()
        {
            return new CollectionData
            {
                UrlPath = UrlPath
            };
        }
    }
}