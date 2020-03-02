/*******************************************************************************
* $Id: CommentStyle.cs 5278 2012-04-04 17:44:34Z sthames $
*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace slc.codecom.vs.tools
  {
  /*****************************************************************************
  * Comment Styles.
  * 
  * <p>$Id: CommentStyle.cs 5278 2012-04-04 17:44:34Z sthames $</p>
  *****************************************************************************/
  public class CommentStyle
    {
    /** File Extension / Comment Delimiters translation table.  Default is first
        entry. */
    public static Extension[] Extensions = new Extension[]
      {
      new Extension(null,   Delimiters.Default),
      new Extension("ashx", Delimiters.ASPX),
      new Extension("asmx", Delimiters.ASPX),
      new Extension("aspx", Delimiters.ASPX),
      new Extension("bat",  Delimiters.BATCH),
      new Extension("as",   Delimiters.C),
      new Extension("c",    Delimiters.C),
      new Extension("cpp",  Delimiters.C),
      new Extension("cs",   Delimiters.C),
      new Extension("css",  Delimiters.C),
      new Extension("java", Delimiters.C),
      new Extension("js",   Delimiters.C),
      new Extension("sql",  Delimiters.C),
      new Extension("html", Delimiters.HTML),
      new Extension("htm",  Delimiters.HTML),
      new Extension("iss",  Delimiters.ISS),
      new Extension("pas",  Delimiters.PAS),
      new Extension("tex",  Delimiters.TEX),
      new Extension("vb",   Delimiters.VB),
      new Extension("vbs",  Delimiters.VB),
      new Extension("bas",  Delimiters.VB),
      new Extension("asp",  Delimiters.VB),
      new Extension("xml",  Delimiters.XML),
      new Extension("mxml", Delimiters.XML)
      };

    /***************************************************************************
    * Class: Extension */
    /**
    * File Extension / Comment Delimiter specification.
    ***************************************************************************/
    public class Extension
      {
      /*************************************************************************
      * Properties
      *************************************************************************/
      /** File Extension.         @private */ protected string     mExtension;
      /** Comment Delimiter Spec. @private */ protected Delimiters mDelimiters;

      /** File Extension.  Required for use as a DataSource. */
      public string     extension  { get { return mExtension; }}
 
      /** Comment Delimiter Spec.  Required for use as a DataSource. */
      public Delimiters delimiters { get { return mDelimiters; }}

      /*************************************************************************
      * Constructor
      * 
      * @param  extension   File Extension
      * @param  delimiters  Comment Delimiter Specification
      *************************************************************************/
      public Extension(string extension, Delimiters delimiters)
        {
        mExtension  = extension;
        mDelimiters = delimiters;
        }
      }
    }
  }
