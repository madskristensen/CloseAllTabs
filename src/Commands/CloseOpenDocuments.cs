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
        private IServiceProvider _serviceProvider;
        private DTE2 _dte;
        private Options _options;
        private SolutionEvents _solEvents;

        private CloseOpenDocuments(IServiceProvider serviceProvider, DTE2 dte, Options options)
        {
            _serviceProvider = serviceProvider;
            _dte = dte;
            _options = options;

            _solEvents = _dte.Events.SolutionEvents;
            _solEvents.BeforeClosing += Execute;
        }

        public static CloseOpenDocuments Instance { get; private set; }

        public static void Initialize(IServiceProvider serviceProvider, DTE2 dte, Options options)
        {
            Instance = new CloseOpenDocuments(serviceProvider, dte, options);
        }

        private void Execute()
        {
            if (!_options.CloseDocuments)
                return;

            foreach (Document document in _dte.Documents)
            {
                var filePath = document.FullName;

                IVsUIHierarchy hierarchy;
                uint itemId;
                IVsWindowFrame frame = null;

                // Don't close pinned files
                if (VsShellUtilities.IsDocumentOpen(_serviceProvider, filePath, VSConstants.LOGVIEWID_Primary, out hierarchy, out itemId, out frame))
                {
                    object propVal;
                    ErrorHandler.ThrowOnFailure(frame.GetProperty((int)__VSFPROPID5.VSFPROPID_IsPinned, out propVal));

                    bool isPinned;
                    if (bool.TryParse(propVal.ToString(), out isPinned) && !isPinned)
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
