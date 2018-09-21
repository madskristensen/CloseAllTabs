using System.Linq;
using EnvDTE;
using EnvDTE80;

namespace CloseAllTabs
{
    public class CollapseFolders
    {
        private DTE2 _dte;
        private Options _options;
        private SolutionEvents _solEvents;

        private CollapseFolders(DTE2 dte, Options options)
        {
            _dte = dte;
            _options = options;

            _solEvents = _dte.Events.SolutionEvents;
            _solEvents.BeforeClosing += Execute;
        }

        public static CollapseFolders Instance { get; private set; }

        public static void Initialize(DTE2 dte, Options options)
        {
            Instance = new CollapseFolders(dte, options);
        }

        private void Execute()
        {
            if (!_options.CollapseOn)
                return;

            UIHierarchyItems hierarchy = _dte.ToolWindows.SolutionExplorer.UIHierarchyItems;

            try
            {
                _dte.SuppressUI = true;
                CollapseHierarchy(hierarchy);
            }
            finally
            {
                _dte.SuppressUI = false;
            }
        }

        private void CollapseHierarchy(UIHierarchyItems hierarchy)
        {
            foreach (UIHierarchyItem item in hierarchy.Cast<UIHierarchyItem>().Where(item => item.UIHierarchyItems.Count > 0))
            {
                CollapseHierarchy(item.UIHierarchyItems);

                if (ShouldCollapse(item))
                    item.UIHierarchyItems.Expanded = false;
            }
        }

        private bool ShouldCollapse(UIHierarchyItem item)
        {
            if (!item.UIHierarchyItems.Expanded)
                return false;

            var project = item.Object as Project;

            // Always collapse files and folders
            if (project == null)
                return true;

            // Collapse solution folders if enabled in settings
            if (project.Kind == ProjectKinds.vsProjectKindSolutionFolder && _options.CollapseSolutionFolders)
                return true;

            // Collapse projects if enabled in settings
            if (project.Kind != ProjectKinds.vsProjectKindSolutionFolder && _options.CollapseProjects)
                return true;

            return false;
        }
    }
}
