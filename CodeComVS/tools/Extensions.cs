using System.Collections;
using System.Diagnostics.CodeAnalysis;
using EnvDTE;

namespace slc.codecom.vs.tools
  {
  /***************************************************************************
  * Hashtable subclass for maintaining Comment Style file extension changes 
  * for open documents.
  ***************************************************************************/
  [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
  public class Extensions : Hashtable
    {
    public  VSPackage package;
    private DTE       app { get { return package.app; }}

    /*************************************************************************
    * clean */
    /**
    * Removes file extension settings for documents no longer open.
    *************************************************************************/
    protected void clean()
      {
      /*-----------------------------------------------------*/
      /* This is done with an array list because an          */
      /* exception is thrown in mscorlib if an element is    */
      /* removed from the Keys collection while scanning it. */
      /*-----------------------------------------------------*/
      foreach(string fileName in new ArrayList(this.Keys))
        {
        bool found = false;

        foreach(Window win in app.Windows)
          if (win.Document != null)
            if (win.Document.FullName == fileName)
              found = true;

        if (!found)
          Remove(fileName);
        }
      }

    /*************************************************************************
    * get */
    /**
    * Returns the file extension for the document.  The actual file extension 
    * is returned if no extension is found in the table for the document file.
    *
    * @param  doc  Document Object
    * @return      File Extension
    *************************************************************************/
    public string get(Document doc)
      {
      clean();

      foreach(DictionaryEntry de in this)
        if (de.Key.ToString() == doc.FullName)
          return de.Value.ToString();

      return getExtension(doc);
      }

    /*************************************************************************
    * getExtension */
    /**
    * Returns the file extension for the document.
    *
    * @param  doc  Document Object
    * @return      File Extension
    *************************************************************************/
    protected string getExtension(Document doc)
      {
      return doc.FullName.Substring(doc.FullName.LastIndexOf('.')+1).ToLower();
      }

    /*************************************************************************
    * set */
    /**
    * Adds a file extension entry for the document file.
    *
    * @param  doc  Document Object
    * @param  ext  File Extension, null=Remove
    *************************************************************************/
    public void set(Document doc, string ext)
      {
      clean();

      if (this.Contains(doc.FullName))
        Remove(doc.FullName);

      if (ext != null && ext.ToLower() != getExtension(doc))
        Add(doc.FullName, ext.ToLower());
      }
    }
  }
