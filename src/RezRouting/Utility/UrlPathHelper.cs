namespace RezRouting.Utility
{
    internal static class UrlPathHelper
    {
        public static string JoinPaths(string path1, string path2)
        {
            if (string.IsNullOrEmpty(path1))
                return path2;
            if (string.IsNullOrEmpty(path2))
                return path1;
            else
                return string.Concat(path1, "/", path2);
        }
    }
}