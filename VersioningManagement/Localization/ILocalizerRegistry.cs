namespace VersioningManagement.Localization
{
    /// <summary>
    /// The interface ILocalizerRegistry defines a registry used to get the concrete localizer based on a type
    /// </summary>
    public interface ILocalizerRegistry
    {
        /// <summary>
        /// Creates the localizer.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <returns></returns>
        ILocalizer<TType> CreateLocalizer<TType>() where TType : class;
    }
}