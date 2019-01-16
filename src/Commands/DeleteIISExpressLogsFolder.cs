using EnvDTE80;
using System;

namespace CloseAllTabs
{
    public class DeleteIISExpressLogsFolder : DeleteBase
    {
        private DeleteIISExpressLogsFolder(DTE2 dte, Options options)
        {
            _dte = dte;
            _options = options;

            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnBeforeCloseSolution += (s, e) => Execute();
        }

        public static DeleteIISExpressLogsFolder Instance { get; private set; }

        public static void Initialize(DTE2 dte, Options options)
        {
            Instance = new DeleteIISExpressLogsFolder(dte, options);
        }

        private void Execute()
        {
            if (!_options.DeleteIISExpressLogsFolder)
                return;

            try
            {
                string root = GetIISExpressLogsFolder();
                DeleteFiles(root);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }
    }
}
