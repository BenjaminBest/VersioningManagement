using System;
using System.Collections.Generic;
using System.Linq;
using VersioningManagement.Helpers;

namespace VersioningManagement.Localization
{
    /// <summary>
    /// The LocalizerRegistry is used to create a concrete localizer based on a given type
    /// </summary>
    public class LocalizerRegistry : ILocalizerRegistry
    {
        /// <summary>
        /// The localizers
        /// </summary>
        private readonly Dictionary<Type, Type> _localizers;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizerRegistry"/> class.
        /// </summary>
        public LocalizerRegistry()
        {
            _localizers = TypeHelper.FindNonAbstractTypes(typeof(ILocalizer<>)).ToDictionary(d => d.GetInterfaces().FirstOrDefault().GetGenericArguments()[0], d => d);
        }

        /// <summary>
        /// Creates the localizer.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <returns></returns>
        public ILocalizer<TType> CreateLocalizer<TType>() where TType : class
        {
            if (!_localizers.ContainsKey(typeof(TType)))
                return null;

            var type = _localizers[typeof(TType)];

            return (ILocalizer<TType>)Activator.CreateInstance(type);
        }
    }
}
