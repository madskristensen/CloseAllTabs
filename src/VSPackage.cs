using System;
using System.Runtime.InteropServices;
using System.Threading;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using task = System.Threading.Tasks.Task;

namespace CloseAllTabs
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.Version, IconResourceID = 400)]
    [ProvideAutoLoad(UIContextGuids.SolutionHasSingleProject, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UIContextGuids.SolutionHasMultipleProjects, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideOptionPage(typeof(Options), "Environment", Vsix.Name, 101, 102, true, new string[] { }, ProvidesLocalizedCategoryName = false)]
    [Guid(Vsix.Id)]
    public sealed class CleanOnClosePackage : Package
    {
        protected override void Initialize()
        {
            var dte = GetService(typeof(DTE)) as DTE2;

            var options = (Options)GetDialogPage(typeof(Options));

            CloseOpenDocuments.Initialize(this, dte, options);
            CollapseFolders.Initialize(dte, options);
            SolutionExplorerFocus.Initialize(dte, options);
            DeleteBinFolder.Initialize(dte, options);
        }
    }
}
