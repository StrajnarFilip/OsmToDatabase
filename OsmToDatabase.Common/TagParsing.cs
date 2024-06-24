using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OsmSharp;
using OsmSharp.Tags;

namespace OsmToDatabase.Common
{
    public static class TagParsing
    {
        /// <summary>
        /// Gets the value of a tag with the given key.
        /// </summary>
        /// <param name="node">Node that is being searched for tags.</param>
        /// <param name="tagKey">Key of the tag that is being searched for.</param>
        /// <returns>Value of the tag, or null if it is not found.</returns>
        public static string? TagValueByKey(this Node node, string tagKey)
        {
            return node
                .Tags.Select<Tag, Tag?>(t => t)
                .FirstOrDefault(tag => tag is not null && tag.Value.Key == tagKey)
                ?.Value;
        }

        public static double? TagDoubleValueByKey(this Node node, string tagKey)
        {
            string? tagValue = node.TagValueByKey(tagKey)?.Replace(",", ".");
            bool success = double.TryParse(tagValue, out double result);
            return success ? result : null;
        }
    }
}
