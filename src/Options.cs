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
        public bool CollapseOnClose { get; set; } = true;

        [Category("Solution Explorer")]
        [DisplayName("Collapse solution folders")]
        [Description("Collapse solution folders when collapsing")]
        [DefaultValue(true)]
        public bool CollapseSolutionFoldersOnClose { get; set; } = true;

        [Category("Solution Explorer")]
        [DisplayName("Make visible")]
        [Description("Makes sure Solution Explorer is visible")]
        [DefaultValue(true)]
        public bool FocusSolutionExplorerOnClose { get; set; } = true;

        [Category("Documents")]
        [DisplayName("Close documents")]
        [Description("Close open all documents on close")]
        [DefaultValue(true)]
        public bool CloseDocumentsOnClose { get; set; } = true;
    }
}
