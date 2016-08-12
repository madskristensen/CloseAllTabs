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
        [DefaultValue(false)]
        public bool FocusSolutionExplorer { get; set; }

        [Category("General")]
        [DisplayName("Close documents")]
        [Description("Close open all documents on close")]
        [DefaultValue(true)]
        public bool CloseDocuments { get; set; } = true;

        [Category("General")]
        [DisplayName("Delete bin and obj folder")]
        [Description("Deletes the bin and obj folders on close unless they are under source control")]
        [DefaultValue(false)]
        public bool DeleteBinFolder { get; set; }
    }
}
