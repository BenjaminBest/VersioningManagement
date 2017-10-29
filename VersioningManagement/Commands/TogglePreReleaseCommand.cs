using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using VersioningManagement.Configuration;
using VersioningManagement.DependencyInjection;
using VersioningManagement.Helpers;
using VersioningManagement.ViewModel;

namespace VersioningManagement.Commands
{
    /// <summary>
    /// The command is responsible for removing or adding the pre to the nuspec versions.
    /// </summary>
    /// <seealso cref="ICommand" />
    public class TogglePreReleaseCommand : ICommand
    {
        /// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        /// <returns>
        /// <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.</returns>
        public bool CanExecute(object parameter)
        {
            var projects = parameter.As<ObservableCollection<ProjectViewModel>>();

            return projects != null && projects.Any();
        }

        /// <summary>Defines the method to be called when the command is invoked.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
            var projects = parameter.As<ObservableCollection<ProjectViewModel>>();
            var preReleaseIdentifier = ServiceLocator.Get<IConfiguration>().PreReleaseIdentifier;

            foreach (var projectViewModel in projects)
            {
                if (projectViewModel.NuspecVersion.Version.EndsWith(preReleaseIdentifier))
                {
                    projectViewModel.NuspecVersion.Version =
                        projectViewModel.NuspecVersion.Version.Replace(preReleaseIdentifier, string.Empty);
                }
                else
                {
                    projectViewModel.NuspecVersion.Version =
                        projectViewModel.NuspecVersion.Version + preReleaseIdentifier;
                }
            }
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
