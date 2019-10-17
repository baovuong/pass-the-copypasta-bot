using System.Collections.Generic;

namespace PassTheCopyPastaBot
{
    public enum PostFilter
    {
        TOP,
        HOT,
        NEW,
        INVALID
    }

    static partial class PostFilterHelper
    {
        private static readonly IDictionary<PostFilter, string> mapping = new Dictionary<PostFilter, string>
        { 
            {PostFilter.TOP, "top" },
            {PostFilter.HOT, "hot" },
            {PostFilter.NEW, "new" }
        };

        public static string GetString(this PostFilter postFilter)
        {
            string value;

            if (mapping.TryGetValue(postFilter, out value))
            {
                return value;
            }

            return null;
        }
    }

}