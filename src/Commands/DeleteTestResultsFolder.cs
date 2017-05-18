using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;

namespace CloseAllTabs
{
    public class DeleteTestResultsFolder
    {
        private DTE2 _dte;
        private Options _options;
        private SolutionEvents _solEvents;

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
                var root = GetRootFolder(_dte.Solution);
                string testResults = Path.Combine(root, "TestResults");
                DeleteFiles(testResults);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }

        private void DeleteFiles(params string[] folders)
        {
            var existingFolders = folders.Where(f => Directory.Exists(f));

            foreach (var folder in existingFolders)
            {
                var files = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories);

                if (!files.Any(f => f.EndsWith(".refresh") || _dte.SourceControl.IsItemUnderSCC(f)))
                    Directory.Delete(folder, true);
            }
        }

        public static string GetRootFolder(Solution solution)
        {
            if (string.IsNullOrEmpty(solution.FullName))
                return File.Exists(solution.FullName) ? Path.GetDirectoryName(solution.FullName) : null;

            return null;
        }
    }
}
