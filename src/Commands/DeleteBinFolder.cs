using System;
using System.IO;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Events;

namespace CloseAllTabs
{
    public class DeleteBinFolder : DeleteBase
    {
        private DeleteBinFolder(DTE2 dte, Options options)
        {
            _dte = dte;
            _options = options;

            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnBeforeCloseSolution += (s, e) => Execute();
        }

        public static DeleteBinFolder Instance { get; private set; }

        public static void Initialize(DTE2 dte, Options options)
        {
            Instance = new DeleteBinFolder(dte, options);
        }

        private void Execute()
        {
            if (!_options.DeleteBinFolder)
                return;

            try
            {
                foreach (EnvDTE.Project project in GetAllProjects())
                {
                    string root = GetProjectRootFolder(project);

                    if (root == null)
                        return;

                    string bin = Path.Combine(root, "bin");
                    string obj = Path.Combine(root, "obj");

                    DeleteFiles(bin, obj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }
    }
}
