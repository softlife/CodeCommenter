/*******************************************************************************
* $Id: BlockComment.cs 5278 2012-04-04 17:44:34Z sthames $
*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace slc.codecom.vs.tools
  {
  /*****************************************************************************
  * Builds a comment block for insertion into an editor file. The input text can
  * be in a layout format or the text can be contiguous and unformatted. The
  * text will be formatted into a block and surrounded with comment delimiters.
  * The delimiters used is dependent on the file name extension or can be
  * specified. For a C program, for instance, '/*' style delimiters are used.
  * 
  * <p>When the selected text is loaded into the object, it is cleaned of all
  * delimiters before any formatting takes place so it is not necessary to
  * remove old comment delimiters before selecting the text to be formatted.</p>
  * 
  * <p>When a block of text is to be formatted, the maximum width and number of
  * lines requested is specified in the call to the Format() method. If the
  * number of lines is not specified, the block will be optimized for line width
  * such that any dangling last lines will not be much smaller than the other
  * lines in the block. The Maximum Line Width overrides all other
  * considerations. So, for 30 character line width specified and 3 lines
  * requested with 220 characters of text selected, there will be many more than
  * 3 lines, each no more than 30 characters.</p>
  * 
  * <p><b>Examples</b></p>
  * <pre>
  *     <---- Selected Block ---->     <------ Commented Block ------>
  *                                    '*----------------------------*
  *     This is a formatted block:     '* This is a formatted block: *
  *       1) 1st bullet.               '*  1) 1st bullet.            *
  *       2) 2nd bullet.               '*  2) 2nd bullet.            *
  *                                    '*----------------------------*
  *
  *     <----------------------------- Selected Text ---------------------->
  *     This is non-formatted, contiguous text which will be formated into a 
  *     block of text according to the lines and maximum width specified.
  *
  *     <------------------ Commented Block ------------------->
  *     '*-----------------------------------------------------*
  *     '* This is non-formatted, contiguous text which will   *
  *     '* will be formated into a block of text according     *
  *     '* to the number of lines and maximum width specified. *
  *     '*-----------------------------------------------------*
  * </pre>
  * 
  * <p>$Id: BlockComment.cs 5278 2012-04-04 17:44:34Z sthames $</p>
  *****************************************************************************/
  public class BlockComment
    {
    /***************************************************************************
    * Constants
    ***************************************************************************/
    /** Max width auto-calculation base.  Used as the base max width when 
        auto-calculating the max width. */
    protected static int AUTO_WIDTH_BASE = 60;

    /** Max width auto-calculation increment.  Used to increment the base width
        when auto-calculating the max width. */
    protected static int AUTO_WIDTH_INCREMENT = 5;

    /** Delimiter Specifications.  Those entries that do not have a type
        specified are used only to remove old delimiters.  Those with types
        are used to create the new comment block. */
    protected static DelimiterSpec[] DELIMITER_SPECS =
      {
      new DelimiterSpec(Delimiters.Default, "#\\*", "  ",   "",     "#*",   '-', "*"),   
      new DelimiterSpec(                    "#",    " ",    ""),
      new DelimiterSpec(Delimiters.ASPX,    "<%--", "    ", "--%>", "<%--", '-', "--%>"), 
      new DelimiterSpec(Delimiters.BATCH,   ":\\*", "  ",   "\\*",  ":*",   '-', "*"),   
      new DelimiterSpec(                    ":",    " ",    ""),
      new DelimiterSpec(Delimiters.C,       "/\\*", "  ",   "\\*/", "/*",   '-', "*/"),  
      new DelimiterSpec(                    "//*",  "   ",  "\\*"),
      new DelimiterSpec(                    "//",   "  ",   ""),
      new DelimiterSpec(Delimiters.HTML,    "<!--", "    ", "-->",  "<!--", '-', "-->"),
      new DelimiterSpec(Delimiters.ISS,     ";\\*", "  ",   "\\*",  ";*",   '-', "*"),   
      new DelimiterSpec(Delimiters.PAS,     "{\\*", "  ",   "\\*}", "{*",   '-', "*}"),   
      new DelimiterSpec(Delimiters.TEX,     "%\\*", "  ",   "",     "%*",   '-', "*"),   
      new DelimiterSpec(                    "%",    " ",    ""),
      new DelimiterSpec(Delimiters.VB,      "'\\*", "  ",   "\\*",  "'*",   '-', "*"),   
      new DelimiterSpec(                    "'",    " ",    ""),
      new DelimiterSpec(Delimiters.XML,     "<!--", "    ", "-->",  "<!--", '=', "-->"), 
      new DelimiterSpec("","","")
      };

    /***************************************************************************
    * Properties
    ***************************************************************************/
    /** Delimiter spec.  @private */ protected Delimiters mDelimiters;
    
    /** Sets the Offset Position of Comment Block.  This amount of space is 
        inserted before each line of the comment block. */ 
    public int indent;
    
    /** Raw comment text. */
    public String text;

    /** Sets the Comment Block Delimiter Specification.  Determines the type of 
        comment delimiters to use. */
    public void setDelimiters(Delimiters t) { mDelimiters = t; }

    /***************************************************************************
    * Constructor */
    /**
    * Loads object with Selected Text and removes delimiters.
    *
    * @param  s  Selected Text
    ***************************************************************************/
    public BlockComment(string s)
      {
      text   = s;
      indent = 0;
      clean();
      }

    /***************************************************************************
    * clean */
    /**
    * Prepares the loaded comment text for blocking.  Removes borders and
    * delimiters, removes trailing whitespace, and trims leading whitespace
    * while maintaining the layout of the input block.  This will retain the
    * layout in case the resulting block is not reformatted in this object.
    ***************************************************************************/
    protected void clean()
      {
      string       rs = Regex.Replace(text, "\\r\\n", "\n");
      List<string> rb = new List<string>(rs.Split('\n'));

      /*----------------------------*/
      /* Remove top/bottom borders. */
      /*----------------------------*/
      for (int i=0; DELIMITER_SPECS[i].leftRE.Length > 0; i++)
        {
        String border = "^\\s*" + DELIMITER_SPECS[i].leftRE
                      + "\\" + DELIMITER_SPECS[i].border + "+"
                      + DELIMITER_SPECS[i].rightRE + "\\s*$";

        if (Regex.IsMatch(rb[0],          border)) rb.RemoveAt(0);
        if (Regex.IsMatch(rb[rb.Count-1], border)) rb.RemoveAt(rb.Count-1);
        }

      /*--------------------------------------------------*/
      /* Remove all comment delimiters. Replace left      */
      /* delimiter with spaces the width of the delimiter */
      /* to maintain the integrity of formatted blocks.   */
      /*--------------------------------------------------*/
      for (int i=0; DELIMITER_SPECS[i].leftRE.Length > 0; i++)
        {
        string left  = "^(\\s*)"                  + DELIMITER_SPECS[i].leftRE;
        string lrep  = "$1"                       + DELIMITER_SPECS[i].leftSP;
        string right = DELIMITER_SPECS[i].rightRE + "\\s*$";

        for (int j=0; j < rb.Count; j++)
          rb[j] = Regex.Replace(Regex.Replace(rb[j], left, lrep), right, "");
        }

      /*-----------------------------*/
      /* Remove trailing whitespace. */
      /*-----------------------------*/
      for (int i=0; i < rb.Count; i++)
        rb[i] = Regex.Replace(rb[i], "\\s*$", "");

      /*------------------------------------------------------------------*/
      /* Find the smallest width of leading whitespace of all non-blank   */
      /* lines and trim this width from the front of each non-blank line. */
      /* This maintains the layout of blocks that will not be formatted.  */
      /*------------------------------------------------------------------*/
      int w = 1000;
      for (int i=0; i < rb.Count; i++)
        if (rb[i].Length > 0)
          w = Math.Min(w, rb[i].Length - Regex.Replace(rb[i], "^\\s*", "").Length);

      if (w >= 0)
        for (int i=0; i < rb.Count; i++)
          if (rb[i].Length > 0)
            rb[i] = rb[i].Substring(w);

      /*---------------------------------*/
      /* Save the cleaned comment block. */
      /*---------------------------------*/
      text = String.Join("\r\n", rb.ToArray());
      }

    /***************************************************************************
    * comment */
    /**
    * Surrounds the comment text with a border and comment delimiters.
    *
    * @return  Commented Block
    ***************************************************************************/
    protected string comment()
      {
      int          i, w;
      string       rs = Regex.Replace(text, "\\r\\n", "\n");
      List<string> cb = new List<string>(rs.Split('\n'));

      /*-----------------------*/
      /* Find delimiter specs. */
      /*-----------------------*/
      DelimiterSpec delims = null;

      for (int d=0; DELIMITER_SPECS[d].leftRE.Length > 0; d++)
        if (DELIMITER_SPECS[d].type == mDelimiters)
          {
          delims = DELIMITER_SPECS[d];
          break;
          }

      /*--------------------------------------------------*/
      /* Find the greatest line width and make all lines  */
      /* this width with a single leading/trailing blank. */
      /*--------------------------------------------------*/
      for (i=0, w=0; i < cb.Count; i++)
        w = Math.Max(w, cb[i].Length);

      for (i=0; i < cb.Count; i++)
        cb[i] = cb[i].PadRight(w+1).PadLeft(w+2);

      /*----------------------------*/
      /* Add the top/bottom border. */
      /*----------------------------*/
      string border = "".PadRight(w+2, delims.border);

      cb.Insert(0, border);
      cb.Add   (border);

      /*----------------------------*/
      /* Add left/right delimiters. */
      /*----------------------------*/
      for (i=0; i < cb.Count; i++)
        cb[i] = delims.left + cb[i] + delims.right;

      /*----------------------------------*/
      /* Adjust block for comment offset. */
      /*----------------------------------*/
      if (indent > 0)
        for (i=0; i < cb.Count; i++)
          cb[i] = "".PadLeft(indent) + cb[i];

      /*-----------------------------*/
      /* Return the commented block. */
      /*-----------------------------*/
      return String.Join("\r\n", cb.ToArray());
      }

    /***************************************************************************
    * format */
    /**
    * Formats the comment block text.  Lets the formatter determine size/shape.
    */
    public void format() { format(0, 0); }

    /**
    * Formats the comment block text.  If <b>MaxWidth</b> is 0, the max width
    * is auto-calculated based on the default width and the total length of the
    * comment to be formatted.  If <b>ReqLines</b> is 0, it is ignored and the
    * number of lines is determined by the max width.  Max width takes
    * precedence over number of lines.
    *
    * <p>Text separated by blank lines represent paragraphs and are formatted
    * separately.</p>
    *
    * @param  MaxWidth  Maximum Line Width (not include Delimiters)
    * @param  ReqLines  Requested Number of Lines
    ***************************************************************************/
    public void format(int MaxWidth, int ReqLines)
      {
      int          width;
      string       rs = Regex.Replace(text, "\\r\\n", "\n");
      List<string> rb = new List<string>();

      /*-----------------------------------------------------*/
      /* Replace blank lines with paragraph indicator and    */
      /* remove leading/trailing blanks from each paragraph. */
      /*-----------------------------------------------------*/
      rs = Regex.Replace(rs, "^\\s*\\n", "\x01", RegexOptions.Multiline);

      List<string> graphs = new List<string>(rs.Split('\x01'));

      for (int i=0; i < graphs.Count; i++)
        graphs[i] = Regex.Replace(Regex.Replace(graphs[i], "^\\s+", ""), "\\s+$", "");         

      rs = String.Join("\x01", graphs.ToArray());

      /*-----------------------------------------*/
      /* Replace CRLF with single space.         */
      /* Reduce multiple spaces to single space. */
      /*-----------------------------------------*/
      rs = Regex.Replace(rs, "\\n",  " ");        
      rs = Regex.Replace(rs, "\\s+", " ");        

      /*--------------------------------------------------------*/
      /* Auto-calculate the max width based on the total length */
      /* of text and the default max width if not specified.    */
      /*--------------------------------------------------------*/
      if (MaxWidth <= 0)
        {
        MaxWidth = AUTO_WIDTH_BASE;
        for (int lines=1; (int)rs.Length / lines > MaxWidth; MaxWidth += AUTO_WIDTH_INCREMENT, lines++);
        }

      /*------------------------------------------*/
      /* Default to one line and let the          */
      /* formatter determine the number of lines. */
      /*------------------------------------------*/
      if (ReqLines <= 0)
        ReqLines = 1;

      /*---------------------------------------------------*/
      /* Format the comment block and increase width until */
      /* comment fits within lines and max width. Increase */
      /* lines as necessary to fit within max width.       */
      /*---------------------------------------------------*/
      for (width=0, rb.Clear(); rb.Count != ReqLines || width > MaxWidth; )
        {
        width = (int)rs.Length / ReqLines;

        /*------------------------------------*/
        /* Format rb and increase width until */
        /* data fits within lines and width.  */
        /*------------------------------------*/
        for (; rb.Count != ReqLines && width <= MaxWidth; width++)
          {
          Queue<string> lines = new Queue<string>(rs.Split('\x01'));
          rb.Clear();

          /*-------------------------------*/
          /* Format paragraphs separately. */
          /*-------------------------------*/
          while (lines.Count > 0)
            {
            string line = lines.Dequeue();
            int    p;

            /*----------------------------------------------*/
            /* Fill block lines to the current width value. */
            /*----------------------------------------------*/
            while ((int)line.Length > width)
              if ((p = (int)line.LastIndexOf(' ', width-1)) != -1)
                {
                rb.Add(line.Substring(0, p));
                line = line.Substring(p+1);
                }
              else
                {
                rb.Add(line.Substring(0, width));
                line = line.Substring(width);
                }
   
            /*--------------------------------------*/
            /* Add the remaining line and add a     */
            /* blank line for paragraph separation. */
            /*--------------------------------------*/
            rb.Add(line);

            if (lines.Count > 0)
              rb.Add("");
            }
          }

        /*-----------------------------*/
        /* If over max width, increase */
        /* no. of lines and retry.     */
        /*-----------------------------*/
        if (width > MaxWidth)
          {
          ReqLines++;
          rb.Clear();
          }
        }

      /*-----------------------*/
      /* Join lines with CRLF. */
      /*-----------------------*/
      rs = String.Join("\r\n", rb.ToArray());

      text = rs;
      }

    /***************************************************************************
    * setDelimitersFromExtension */
    /**
    * Sets the comment type based on the file name extension.
    *
    * @param  fe  File Name Extension
    ***************************************************************************/
    public void setDelimitersFromExtension(string fe)
      {
      foreach (CommentStyle.Extension e in CommentStyle.Extensions)
        if (fe == e.extension || e.extension == null)
          mDelimiters = e.delimiters;
      }

    /***************************************************************************
    * setDelimitersFromFileName */
    /**
    * Sets the comment type based on the file name extension.
    *
    * @param  fn  File Name
    ***************************************************************************/
    public void setDelimitersFromFileName(String fn)
      {
      setDelimitersFromExtension(fn.Substring(fn.LastIndexOf('.')+1));
      }

    /***************************************************************************
    * toArray */
    /**
    * Surrounds the block with comment delimiters and returns as string vector.
    *
    * @return  Commented Block
    ***************************************************************************/
    public string[] toArray()
      {
      return comment().Split('\r','\n');
      }

    /***************************************************************************
    * toString */
    /**
    * Surrounds the block with comment delimiters and returns as string.
    *
    * @return  Commented Block
    ***************************************************************************/
    public string toString()
      {
      return comment();
      }

    /***************************************************************************
    * Class: Delimiter */
    /**
    * Comment delimiter specifications.  Used to create a list of commenting
    * styles for scanning and cleaning out old delimiters as well as creating
    * the new comment block.
    ***************************************************************************/
    protected class DelimiterSpec
      {
      /*************************************************************************
      * Properties
      *************************************************************************/
      /** Comment Specification.  Identifies the type of comment delimiters to
          use when building the comment block.  When this value is
          <b>Delimiters.NULL</b>, the entry is used only to find and remove
          delimiters in the old comment block. */
      public Delimiters type;         

      /** Left Delimiter RE Pattern.  Used to find left comment delimiters. */             
      public string     leftRE;       

      /** Left Delimiter Spaces Replacement.  A string of spaces used to replace
          the left delimiter when the block is cleaned of delimiters.  This
          maintains the integrity of any formatting. */
      public string     leftSP;         

      /** Right Delimiter RE Pattern.  Used to find right comment delimiters. */            
      public string     rightRE;      

      /** Left Delimiter in Resulting Comment Block. */              
      public string     left;         

      /** Border Character.  Comment Block borders are composed of this 
          character. */            
      public char       border;       

      /** Right Delimiter in Resulting Comment Block. */             
      public string     right;        

      /*************************************************************************
      * Constructor */
      /**
      * Builds the delimiter specification.  These specs are used only for
      * removing old delimiters.
      *
      * @param  leftRE   Left Delimiter RE Pattern
      * @param  leftSP   Left Delimiter Replacement String
      * @param  rightRE  Right Delimiter RE Pattern
      */
      public DelimiterSpec(string leftRE, string leftSP, string rightRE)
      : this(Delimiters.NULL, leftRE, leftSP, rightRE, null, (char)0, null) {}

      /**
      * Builds the delimiter specification.  These specs are used for removing
      * old delimiters and also define the delimiters for creation of the new
      * comment block.
      *
      * @param  type     Specification Identifier
      * @param  leftRE   Left Delimiter RE Pattern
      * @param  leftSP   Left Delimiter Replacement String
      * @param  rightRE  Right Delimiter RE Pattern
      * @param  left     Left Delimiter in Comment Block
      * @param  border   Comment Block Border Character
      * @param  right    Right Delimiter in Comment Block
      *************************************************************************/
      public DelimiterSpec(Delimiters type, string leftRE, string leftSP, string rightRE, string left, char border, string right)
        {
        this.type    = type;
        this.leftRE  = leftRE;
        this.leftSP  = leftSP;
        this.rightRE = rightRE;
        this.left    = left;
        this.border  = border;
        this.right   = right;
        }
      }
    }
  }
