using System;
using System.Collections.Generic;
using RezRouting.Utility;

namespace RezRouting.AspNetMvc
{
    /// <summary>
    /// Extensions used to access convention data
    /// </summary>
    internal static class ConventionDataExtensions
    {
        /// <summary>
        /// Gets controller types registered with the HandledBy extension methods used
        /// by the public API
        /// </summary>
        /// <param name="conventionData"></param>
        /// <returns></returns>
        public static List<Type> GetControllerTypes(this Dictionary<string,object> conventionData)
        {
            return conventionData.GetOrAdd(ConventionDataKeys.ControllerTypes, () => new List<Type>());
        }

        public static void AddControllerTypes(this Dictionary<string, object> conventionData, IEnumerable<Type> types)
        {
            var data = conventionData.GetControllerTypes();
            data.AddRange(types);
        }
    }
}