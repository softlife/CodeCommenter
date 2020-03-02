using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using slc.codecom.vs.tools;
using Microsoft.VisualStudio.Shell.Interop;

namespace slc.codecom.vs.handlers
  {
  /*****************************************************************************
  * Plugin Command/Hook Handler base class.
  *****************************************************************************/
  public abstract class Handler
    {
    /***************************************************************************
    * Properties
    ***************************************************************************/
    /* Package instance. */      public VSPackage   package;
    /* VS app interface. */      public DTE         app        { get { return package.app; }}
    /* Package optins. */        public OptionsPage options    { get { return package.options; }}
    /* File Extensions table. */ public Extensions  extensions { get { return package.extensions; }}

    /* Menu command ID from VSCT file. */
    protected abstract int    commandID   { get; }

    /* Keyboard shortcut bindings. Ignored if not set. */
    public    virtual  string keyBindings { get; set; }

    /***************************************************************************
    * createCommand */
   /**
    * Builds and returns the command object for the handler.  Called from 
    * `Package.Initialize`. Requires `commandID` accessor set appropriately.
    *                                                                                                          
    * @param  package  Extension instance.
    * @param  guid     Extension guid.
    * @return          Menu command object.
    ***************************************************************************/
    public OleMenuCommand createCommand(VSPackage package, Guid guid)
      {
      Trace.WriteLine("Entering createCommand() of: {0}", this.GetType().Name);

      this.package           = package;
      CommandID      id      = new CommandID(guid, commandID);
      OleMenuCommand cmd     = new OleMenuCommand(exec, id );
      cmd.BeforeQueryStatus += queryStatus;

      if (keyBindings != null)
        setKeyBindings(guid, commandID, keyBindings);

      return cmd;
      }

    /***************************************************************************
    * exec */
    /**
    * Called from Exec to execute the command.
    *
    * @param  sender  Command object.
    ***************************************************************************/
    public abstract void exec(object sender, EventArgs e);

    /***************************************************************************
    * queryStatus */
    /**
    * Called by QueryStatus to update command availability. Sets `Enabled` to
    * value returned from `show()`.
    *
    * @param  sender  Command object.
    ***************************************************************************/
    protected void queryStatus(object sender, EventArgs e)
      {
      Trace.WriteLine("Entering queryStatus() of: {0}", this.GetType().Name);
      ((OleMenuCommand)sender).Enabled = show();
      }

    /***************************************************************************
    * setKeyBindings */
    /**
    * Sets the key bindings for the command.  Displays any exception if binding
    * fails.
    *
    * @param  cmd   Command
    * @param  keys  Key Bindings
    ***************************************************************************/
    protected void setKeyBindings(Guid guid, int commandID, string keys)
      {
      foreach(EnvDTE.Command cmd in app.Commands)
        if (new Guid(cmd.Guid) == guid && cmd.ID == commandID)
          {
          try { cmd.Bindings = keys; }
          catch(Exception ex)
            {
            VsShellUtilities.ShowMessageBox(package, String.Format("Could not set key bindings for {0}\nkey bindings: {1}\nnexception: {2}",
              cmd.Name, keys, ex.Message), "Warning", OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }
          }
      }

    /***************************************************************************
    * show */
    /**
    * Called by queryStatus to set `Enabled` property. This implementation
    * returns true.
    *
    * @return  True=enable command, False=disable command.
    ***************************************************************************/
    protected virtual bool show()
      {
      return false;
      }
    }
  }
