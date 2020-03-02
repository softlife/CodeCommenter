using slc.codecom.vs.tools;

namespace slc.codecom.vs.handlers
  {
  /*****************************************************************************
  * Base class for text editor window command handlers. Enabled if there is an
  * active document.
  *****************************************************************************/
  public abstract class TextEditorHandler : Handler
    {
    /***************************************************************************
    * show */
    /**
    * Returns `true` if there is an active document.
    ***************************************************************************/
    protected override bool show()
      {
      return (app.ActiveDocument != null);
      }
    }
  }
