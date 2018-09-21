using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;

namespace CloseAllTabs
{
    public class DeleteBinFolder
    {
        private DTE2 _dte;
        private Options _options;
        private SolutionEvents _solEvents;

        private DeleteBinFolder(DTE2 dte, Options options)
        {
            _dte = dte;
            _options = options;

            _solEvents = _dte.Events.SolutionEvents;
            _solEvents.BeforeClosing += Execute;
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
                foreach (Project project in GetAllProjects())
                {
                    string root = GetRootFolder(project);

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

        private void DeleteFiles(params string[] folders)
        {
            IEnumerable<string> existingFolders = folders.Where(f => Directory.Exists(f));

            foreach (string folder in existingFolders)
            {
                IEnumerable<string> files = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories);

                if (!files.Any(f => f.EndsWith(".refresh") || _dte.SourceControl.IsItemUnderSCC(f)))
                    Directory.Delete(folder, true);
            }
        }

        private IEnumerable<Project> GetAllProjects()
        {
            return _dte.Solution.Projects
                  .Cast<Project>()
                  .SelectMany(GetChildProjects)
                  .Union(_dte.Solution.Projects.Cast<Project>())
                  .Where(p => { try { return !string.IsNullOrEmpty(p.FullName); } catch { return false; } });
        }

        private static IEnumerable<Project> GetChildProjects(Project parent)
        {
            try
            {
                if (parent.Kind != ProjectKinds.vsProjectKindSolutionFolder && parent.Collection == null)  // Unloaded
                    return Enumerable.Empty<Project>();

                if (!string.IsNullOrEmpty(parent.FullName))
                    return new[] { parent };
            }
            catch (COMException)
            {
                return Enumerable.Empty<Project>();
            }

            return parent.ProjectItems
                    .Cast<ProjectItem>()
                    .Where(p => p.SubProject != null)
                    .SelectMany(p => GetChildProjects(p.SubProject));
        }

        public static string GetRootFolder(Project project)
        {
            if (string.IsNullOrEmpty(project.FullName))
                return null;

            string fullPath;

            try
            {
                fullPath = project.Properties.Item("FullPath").Value as string;
            }
            catch (ArgumentException)
            {
                try
                {
                    // MFC projects don't have FullPath, and there seems to be no way to query existence
                    fullPath = project.Properties.Item("ProjectDirectory").Value as string;
                }
                catch (ArgumentException)
                {
                    // Installer projects have a ProjectPath.
                    fullPath = project.Properties.Item("ProjectPath").Value as string;
                }
            }

            if (string.IsNullOrEmpty(fullPath))
                return File.Exists(project.FullName) ? Path.GetDirectoryName(project.FullName) : null;

            if (Directory.Exists(fullPath))
                return fullPath;

            if (File.Exists(fullPath))
                return Path.GetDirectoryName(fullPath);

            return null;
        }
    }
}
