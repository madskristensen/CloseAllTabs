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

            _solEvents = _dte.Events.SolutionEvents;
            _solEvents.BeforeClosing += Execute;
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
                var root = GetSolutionRootFolder(_dte.Solution);
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
