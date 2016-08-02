using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace CloseAllTabs
{
    public class Options : DialogPage
    {
        [DisplayName("Collapse nodes")]
        [Description("Collapse nodes in Solution Explorer on close")]
        [DefaultValue(true)]
        public bool CollapseOnClose { get; set; } = true;

        [DisplayName("Close documents")]
        [Description("Close open all documents on close")]
        [DefaultValue(true)]
        public bool CloseDocumentsOnClose { get; set; } = true;


    }
}
