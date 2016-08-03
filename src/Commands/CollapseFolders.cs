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

            var hierarchy = _dte.ToolWindows.SolutionExplorer.UIHierarchyItems;

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
            foreach (UIHierarchyItem item in hierarchy)
            {
                var project = item.Object as Project;

                // Only collapse non-project nodes
                if (project == null || (project.Kind == ProjectKinds.vsProjectKindSolutionFolder && _options.CollapseSolutionFolders))
                {
                    item.UIHierarchyItems.Expanded = false;
                }

                CollapseHierarchy(item.UIHierarchyItems);
            }
        }
    }
}
