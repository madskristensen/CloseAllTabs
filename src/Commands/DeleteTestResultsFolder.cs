using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;

namespace CloseAllTabs
{
    public class DeleteTestResultsFolder : DeleteBase
    {
        private DeleteTestResultsFolder(DTE2 dte, Options options)
        {
            _dte = dte;
            _options = options;

            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnBeforeCloseSolution += (s, e) => Execute();
        }

        public static DeleteTestResultsFolder Instance { get; private set; }

        public static void Initialize(DTE2 dte, Options options)
        {
            Instance = new DeleteTestResultsFolder(dte, options);
        }

        private void Execute()
        {
            if (!_options.DeleteTestResultsFolder)
                return;

            try
            {
                string root = GetSolutionRootFolder(_dte.Solution);
                string testResults = Path.Combine(root, "TestResults");
                DeleteFiles(testResults);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }
    }
}
