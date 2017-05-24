using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace CloseAllTabs
{
    public class Options : DialogPage
    {
        // General
        [Category("General")]
        [DisplayName("Close documents")]
        [Description("Close all open documents on close unless they are pinned")]
        [DefaultValue(true)]
        public bool CloseDocuments { get; set; } = true;

        [Category("General")]
        [DisplayName("Delete bin and obj folder")]
        [Description("Deletes the bin and obj folders on close unless they are under source control")]
        [DefaultValue(false)]
        public bool DeleteBinFolder { get; set; }

        [Category("General")]
        [DisplayName("Delete TestResults folder")]
        [Description("Deletes the TestResults folders on close unless they are under source control")]
        [DefaultValue(false)]
        public bool DeleteTestResultsFolder { get; set; }


        [Category("General")]
        [DisplayName("Delete .vs folder")]
        [Description("Deletes the .vs folders on close unless they are under source control")]
        [DefaultValue(false)]
        public bool DeleteDotVsFolder { get; set; }

        // Solution Explorer
        [Category("Solution Explorer")]
        [DisplayName("Collapse files and folders")]
        [Description("Collapse nodes in Solution Explorer on close")]
        [DefaultValue(true)]
        public bool CollapseOn { get; set; } = true;

        [Category("Solution Explorer")]
        [DisplayName("Collapse solution folders")]
        [Description("Collapse solution folders when collapsing")]
        [DefaultValue(true)]
        public bool CollapseSolutionFolders { get; set; } = true;

        [Category("Solution Explorer")]
        [DisplayName("Collapse projects")]
        [Description("Collapse all projects in a solution on close")]
        [DefaultValue(false)]
        public bool CollapseProjects { get; set; }

        [Category("Solution Explorer")]
        [DisplayName("Visible on open")]
        [Description("Makes sure Solution Explorer is visible when a solution is opened")]
        [DefaultValue(false)]
        public bool FocusSolutionExplorer { get; set; }
}
}
