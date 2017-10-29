using System;
using System.Windows.Input;
using VersioningManagement.Helpers;
using VersioningManagement.Versions;
using VersioningManagement.ViewModel;

namespace VersioningManagement.Commands
{
    /// <summary>
    /// The command is responsible for changing the major version
    /// </summary>
    /// <seealso cref="ICommand" />
    public class IncreaseMajorVersionCommand : ICommand
    {
        /// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        /// <returns>
        /// <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.</returns>
        public bool CanExecute(object parameter)
        {
            var version = parameter.As<ProjectViewModel>().IsNotNull(p => p.AssemblyInfoVersion)
                .IsNotNull(p => p.Version);

            if (string.IsNullOrEmpty(version))
                return false;

            return !string.IsNullOrEmpty(version) && VersionChanger.TryParse(version, out VersionChanger oldVersion);
        }

        /// <summary>Defines the method to be called when the command is invoked.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
            var assemblyInfo = parameter.As<ProjectViewModel>().IsNotNull(p => p.AssemblyInfoVersion);

            if (!VersionChanger.TryParse(assemblyInfo.Version, out VersionChanger version))
                return;

            version.IncreaseVersion(VersionPart.Major);

            assemblyInfo.Version = version.Version;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
