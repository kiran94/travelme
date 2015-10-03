namespace com.kiransprojects.travelme.Extensions
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// List Extension Methods
    /// </summary>
    public static class ListExtensionMethods
    {
        /// <summary>
        /// Checks if a list is ready to be displayed
        /// </summary>
        /// <typeparam name="T">Type of List</typeparam>
        /// <param name="value">List to be searched</param>
        /// <returns>Flag indicating if the list is not empty, not null and has content</returns>
        public static bool HasContent<T>(this List<T> value) where T : EntityBase
        {
            if (value == null || value.Count == 0)
            {
                return false; 
            }

            for (int i = 0; i < value.Count; i++)
            {
                if (value[i] != null)
                {
                    return true; 
                }
            }

            return false; 
        }
    }
}