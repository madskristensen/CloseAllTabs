using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace CloseAllTabs
{
    public class Options : DialogPage
    {
        [Category("Solution Explorer")]
        [DisplayName("Collapse nodes")]
        [Description("Collapse nodes in Solution Explorer on close")]
        [DefaultValue(true)]
        public bool CollapseOn { get; set; } = true;

        [Category("Solution Explorer")]
        [DisplayName("Collapse solution folders")]
        [Description("Collapse solution folders when collapsing")]
        [DefaultValue(true)]
        public bool CollapseSolutionFolders { get; set; } = true;

        [Category("Solution Explorer")]
        [DisplayName("Visible on open")]
        [Description("Makes sure Solution Explorer is visible when a solution is opened")]
        [DefaultValue(true)]
        public bool FocusSolutionExplorer { get; set; } = true;

        [Category("Documents")]
        [DisplayName("Close documents")]
        [Description("Close open all documents on close")]
        [DefaultValue(true)]
        public bool CloseDocuments { get; set; } = true;
    }
}
