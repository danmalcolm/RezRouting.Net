﻿namespace RezRouting
{
    /// <summary>
    /// An element within a resource URL that contains the id of a resource
    /// </summary>
    public class IdUrlSegment : IUrlSegment
    {
        public IdUrlSegment(string idName, string idNameAsAncestor)
        {
            IdName = idName;
            IdNameAsAncestor = idNameAsAncestor;
        }

        public string IdName { get; private set; }

        public string IdNameAsAncestor { get; private set; }

        public string Path
        {
            get
            {
                return string.Concat("{", IdName + "}");
            }
        }

        public string PathAsAncestor
        {
            get
            {
                return string.Concat("{", IdNameAsAncestor + "}");
            }
        }
    }
}