using System.Windows.Forms;
using slc.codecom.vs.tools;

namespace slc.codecom.vs.handlers
  {
  /*****************************************************************************
  * Block Comment Handler.
  *****************************************************************************/
  public class BlockCommentHandler : BlockConvertHandler
    {
    protected override int commandID { get { return Symbols.idBlockCommentCmd; }}

    /***************************************************************************
    * convertBlock */
    /**
    * Surrounds the selected comment text with a box.
    ***************************************************************************/
    protected override string convertBlock(string comment, int indent)
      {
      /*-----------------------------------------------*/
      /* Load the form and populate text with comment. */
      /*-----------------------------------------------*/
      BlockCommentForm form = new BlockCommentForm();
      try
        {
        tools.BlockComment bc   = new tools.BlockComment(comment);
        form.txtComment.Text    = bc.text;

        /*--------------------------------------------------------------*/
        /* Get the comment text from the form, set the delimiters       */
        /* based on the file extension, format the comment, and return. */
        /*--------------------------------------------------------------*/
        if (new WinWrapper(package.uiShell).showDialog(form) == DialogResult.OK)
          {
          bc.setDelimitersFromExtension(extensions.get(app.ActiveDocument));
          bc.text   = form.txtComment.Text;
          bc.indent = indent;
        
          if (form.chkFormat.Checked)
            bc.format(options.bcMaxWidth, (int)form.numLines.Value);
            
          return bc.toString();
          }
          
        return null;
        }
      finally
        {
        form.Dispose();
        }
      }
    }
  }
