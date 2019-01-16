using EnvDTE80;
using System;

namespace CloseAllTabs
{
    public class DeleteIISExpressTraceLogFilesFolder : DeleteBase
    {
        private DeleteIISExpressTraceLogFilesFolder(DTE2 dte, Options options)
        {
            _dte = dte;
            _options = options;

            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnBeforeCloseSolution += (s, e) => Execute();
        }

        public static DeleteIISExpressTraceLogFilesFolder Instance { get; private set; }

        public static void Initialize(DTE2 dte, Options options)
        {
            Instance = new DeleteIISExpressTraceLogFilesFolder(dte, options);
        }

        private void Execute()
        {
            if (!_options.DeleteIISExpressTraceLogFilesFolder)
                return;

            try
            {
                string root = GetIISExpressTraceLogFilesFolder();
                DeleteFiles(root);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }
    }
}
