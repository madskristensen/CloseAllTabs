using EnvDTE;
using EnvDTE80;

namespace CloseAllTabs
{
    public class CloseOpenDocuments
    {
        private DTE2 _dte;
        private DTEEvents _dteEvents;
        private SolutionEvents _solEvents;

        private CloseOpenDocuments(DTE2 dte)
        {
            _dte = dte;

            _dteEvents = _dte.Events.DTEEvents;
            _solEvents = _dte.Events.SolutionEvents;

            //_dteEvents.OnBeginShutdown += Execute;
            _solEvents.BeforeClosing += Execute;
        }

        public static CloseOpenDocuments Instance { get; private set; }

        public static void Initialize(DTE2 dte)
        {
            Instance = new CloseOpenDocuments(dte);
        }

        private void Execute()
        {
            var cmd = _dte.Commands.Item("Window.CloseAllDocuments");

            if (cmd.IsAvailable)
            {
                _dte.Commands.Raise(cmd.Guid, cmd.ID, null, null);
            }
        }
    }
}
