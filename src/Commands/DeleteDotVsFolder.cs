using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;

namespace CloseAllTabs
{
    public class DeleteDotVsFolder : DeleteBase
    {
        private DeleteDotVsFolder(DTE2 dte, Options options)
        {
            _dte = dte;
            _options = options;

            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnBeforeCloseSolution += (s, e) => Execute();
        }

        public static DeleteDotVsFolder Instance { get; private set; }

        public static void Initialize(DTE2 dte, Options options)
        {
            Instance = new DeleteDotVsFolder(dte, options);
        }

        private void Execute()
        {
            if (!_options.DeleteDotVsFolder)
                return;

            try
            {
                string root = GetSolutionRootFolder(_dte.Solution);
                string dotVs = Path.Combine(root, ".vs");
                DeleteFiles(dotVs);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }
    }
}
