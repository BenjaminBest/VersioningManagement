using System;

namespace VersioningManagement.Helpers
{
    /// <summary>
    /// The class ObjectExtensions contains extensions for object
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Determines whether the object is not null, then executes the function and returns the result
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="whenNotNull">The when not null.</param>
        /// <returns></returns>
        public static TResult IsNotNull<TResult, TType>(this TType obj, Func<TType, TResult> whenNotNull)
        {
            return obj == null ? default(TResult) : whenNotNull(obj);
        }
    }
}
