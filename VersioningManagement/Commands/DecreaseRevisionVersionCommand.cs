using System;
using System.Windows.Input;
using VersioningManagement.Helpers;
using VersioningManagement.Versions;
using VersioningManagement.ViewModel;

namespace VersioningManagement.Commands
{
    /// <summary>
    /// The command is responsible for changing the revision version
    /// </summary>
    /// <seealso cref="ICommand" />
    public class DecreaseRevisionVersionCommand : ICommand
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

            if (!Versioning.TryParse(version, out Versioning oldVersion))
                return false;

            return oldVersion.Revision != default(int);
        }

        /// <summary>Defines the method to be called when the command is invoked.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
            parameter.As<ProjectViewModel>().IsNotNull(p => Versioning.DecreaseRevisionVersion(p.AssemblyInfoVersion.Version));
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
