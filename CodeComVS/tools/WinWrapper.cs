using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell.Interop;

namespace slc.codecom.vs.tools
  {
  /*****************************************************************************
  * Used to make VS a parent to the winform.
  *****************************************************************************/
  public class WinWrapper : IWin32Window 
    { 
    IntPtr     handle = IntPtr.Zero;
    IVsUIShell uiShell;

    public IntPtr Handle { get { return handle; }}

    public WinWrapper(IVsUIShell uiShell)
      {
      this.uiShell = uiShell;
      uiShell.GetDialogOwnerHwnd(out handle);
      }

    public DialogResult showDialog(Form dialog)
      {
      try
        {
        uiShell.EnableModeless(0);
        return dialog.ShowDialog();
        }
      finally
        {
        uiShell.EnableModeless(1);
        }
      }
    }
  }
