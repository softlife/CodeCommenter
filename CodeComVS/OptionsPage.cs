using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace slc.codecom.vs
  {
  /*************************************************************************************************
  * Package options page added to `Tools/Options`.
  /************************************************************************************************/
  public class OptionsPage : DialogPage
    {
    [Category("Block Comment")]
    [DisplayName("Max width")]
    [Description("Maximum comment line width for formatted block, 0=auto-calculate. Block is formatted such that all lines are no longer than this value.")]
    public int bcMaxWidth { get; set; }
    }
  }
