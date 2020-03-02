using System;
using System.Windows.Forms;
using slc.codecom.vs.tools;

namespace slc.codecom.vs.handlers
  {
  /*****************************************************************************
  * Comment Style Handler.  Allows the user to change the comment style
  * (extension) for the file in the editor window. Defaults to the current file
  * extension if it matches a known extension. Enter sets the extension for the
  * active document to the selected value and ESC resets to the default.
  *****************************************************************************/
  public class CommentStyleHandler : TextEditorHandler
    {
    protected override int commandID { get { return Symbols.idCommentStyleCmd; }}

    /***************************************************************************
    * exec */
    /**
    * Called from Exec to execute the command.
    *
    * @param  sender  Command object.
    ***************************************************************************/
    public override void exec(object sender, EventArgs e)
      {
      /*---------------------------------------------*/
      /* Load the form and set the current extension */
      /* to the extension for the document.          */
      /*---------------------------------------------*/
      CommentStyleForm form = new CommentStyleForm();
      try
        {
        form.cbxStyle.SelectedValue = extensions.get(app.ActiveDocument);

        DialogResult rv = new WinWrapper(package.uiShell).showDialog(form);

        /*---------------------------------------------------*/
        /* Reset the file extension to the actual extension. */
        /*---------------------------------------------------*/
        if (rv == DialogResult.Ignore)
          extensions.set(app.ActiveDocument, null);

        /*-----------------------------------------------*/
        /* Set the file extension to the selected value. */
        /*-----------------------------------------------*/
        if (rv == DialogResult.OK)
          extensions.set(app.ActiveDocument, (string)form.cbxStyle.SelectedValue);
        }
      finally
        {
        form.Dispose();
        }
      }
    }
  }
