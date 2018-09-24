using System;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace CloseAllTabs
{
    public class CloseOpenDocuments
    {
        private readonly IServiceProvider _serviceProvider;
        private DTE2 _dte;
        private Options _options;

        private CloseOpenDocuments(IServiceProvider serviceProvider, DTE2 dte, Options options)
        {
            _serviceProvider = serviceProvider;
            _dte = dte;
            _options = options;
        }

        public static CloseOpenDocuments Instance { get; private set; }

        public static void Initialize(Package serviceProvider, DTE2 dte, Options options)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            Instance = new CloseOpenDocuments(serviceProvider, dte, options);
            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnBeforeCloseSolution += (s, e) => Instance.Execute();
        }

        private void Execute()
        {
            if (!_options.CloseDocuments)
                return;

            foreach (Document document in _dte.Documents)
            {
                string filePath = document.FullName;

                // Don't close pinned files
                if (VsShellUtilities.IsDocumentOpen(_serviceProvider, filePath, VSConstants.LOGVIEWID_Primary, out IVsUIHierarchy hierarchy, out uint itemId, out IVsWindowFrame frame))
                {
                    ErrorHandler.ThrowOnFailure(frame.GetProperty((int)__VSFPROPID5.VSFPROPID_IsPinned, out object propVal));

                    if (bool.TryParse(propVal.ToString(), out bool isPinned) && !isPinned)
                    {
                        document.Close(vsSaveChanges.vsSaveChangesPrompt);
                    }
                }
                else
                {
                    document.Close(vsSaveChanges.vsSaveChangesPrompt);
                }
            }
        }
    }
}
