using EnvDTE;
using EnvDTE80;

namespace CloseAllTabs
{
    public class SolutionExplorerFocus
    {
        private DTE2 _dte;
        private Options _options;
        private SolutionEvents _solEvents;

        private SolutionExplorerFocus(DTE2 dte, Options options)
        {
            _dte = dte;
            _options = options;

            _solEvents = _dte.Events.SolutionEvents;
            _solEvents.Opened += Execute;
        }

        public static SolutionExplorerFocus Instance { get; private set; }

        public static void Initialize(DTE2 dte, Options options)
        {
            Instance = new SolutionExplorerFocus(dte, options);
        }

        private void Execute()
        {
            if (!_options.FocusSolutionExplorer)
                return;

            var cmd = _dte.Commands.Item("View.SolutionExplorer");

            if (cmd.IsAvailable)
            {
                _dte.Commands.Raise(cmd.Guid, cmd.ID, null, null);
            }
        }
    }
}
