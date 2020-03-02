using EnvDTE;
using System;

namespace slc.codecom.vs.handlers
  {
  /*****************************************************************************
  * Plugin command handler for replacing the selected block of text with the
  * result of a conversion. New text is inserted at the position of the caret.
  *****************************************************************************/
  public abstract class BlockConvertHandler : TextEditorHandler
    {
    /***************************************************************************
    * exec */
    /**
    * Retrieves the selected block of text and replaces it with the result of a
    * call to <c>convertBlock</c>.
    *
    * @param  sender  Command object.
    ***************************************************************************/
    public override void exec(object sender, EventArgs e)
      {
      /*----------------------------------------*/
      /* Get the document and selection. Indent */
      /* offset is current caret position.      */
      /*----------------------------------------*/
      Document      doc    = app.ActiveDocument;
      TextSelection select = (TextSelection)doc.Selection;
      VirtualPoint  top    = select.TopPoint;
      VirtualPoint  bottom = select.BottomPoint;
      int           indent = select.CurrentColumn-1;

      /*--------------------------------------------*/
      /* Set edit points at the start and end of    */
      /* the lines where selection starts and ends. */
      /*--------------------------------------------*/
      EditPoint ep1 = top   .CreateEditPoint();
      EditPoint ep2 = bottom.CreateEditPoint();

      ep1.MoveToLineAndOffset(top   .Line, 1);
      ep2.MoveToLineAndOffset(bottom.Line, bottom.LineLength+1);
        
      /*---------------------------------------*/
      /* Convert the block from the content of */
      /* the start and end line of selection.  */
      /*---------------------------------------*/
      string block = convertBlock(ep1.GetText(ep2), indent);
        
      if (block != null)
        {
        /*------------------------------------*/
        /* Open an undo context if none open. */
        /*------------------------------------*/
        UndoContext undo = app.UndoContext;

        if (!undo.IsOpen)
          undo.Open(GetType().Name, false);

        /*----------------------------------------------------------*/
        /* Replace the selected block, move the caret to the indent */
        /* position on the last line, and close the Undo Context.   */
        /*----------------------------------------------------------*/
        ep1.Delete(ep2);
        ep1.Insert(block);

        select.MoveToLineAndOffset(ep1.Line, indent+1, false);

        undo.Close();
        }
      }

    /***************************************************************************
    * convertBlock */
    /**
    * Converts the input text and returns the result.
    *
    * @param  selection  Selected text block.
    * @param  indent     Indent offset.
    * @return            Converted block, null=cancel.
    ***************************************************************************/
    protected abstract string convertBlock(string selection, int indent);
    }
  }
