using EnvDTE;
using EnvDTE80;

namespace CloseAllTabs
{
    public class CloseOpenDocuments
    {
        private DTE2 _dte;
        private Options _options;
        private SolutionEvents _solEvents;

        private CloseOpenDocuments(DTE2 dte, Options options)
        {
            _dte = dte;
            _options = options;

            _solEvents = _dte.Events.SolutionEvents;
            _solEvents.BeforeClosing += Execute;
        }

        public static CloseOpenDocuments Instance { get; private set; }

        public static void Initialize(DTE2 dte, Options options)
        {
            Instance = new CloseOpenDocuments(dte, options);
        }

        private void Execute()
        {
            if (!_options.CloseDocumentsOnClose)
                return;

            var cmd = _dte.Commands.Item("Window.CloseAllDocuments");

            if (cmd.IsAvailable)
            {
                _dte.Commands.Raise(cmd.Guid, cmd.ID, null, null);
            }
        }
    }
}
