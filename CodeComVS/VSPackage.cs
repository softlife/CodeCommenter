using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using slc.codecom.vs.handlers;
using slc.codecom.vs.tools;

namespace slc.codecom.vs
  {
  /*************************************************************************************************
  * Code Commenting tools Visual Studio extension.
  *
  * Menu commands of this extension are defined by Handler objects. OleMenuCommand objects for
  * these handlers are created when the extension is first loaded. A submenu is added to the VS 
  * Tools menu to hold the commands in this extension.
  */
  /*-------------------*/
  /* Register package. */
  /*-------------------*/
  [PackageRegistration(UseManagedResourcesOnly = true)]
  [Guid(Symbols.guidCodeComPkg)]

  /*----------------------*/
  /* Info for About page. */
  /*----------------------*/
  [InstalledProductRegistration("#110", "#112", "#111", IconResourceID = 400)]

  /*------------------------------------------------------*/
  /* Apparently, this is needed for adding menu commands. */
  /*------------------------------------------------------*/
  [ProvideMenuResource("Menus.ctmenu", 1)]

  /*-----------------------------------------*/
  /* Provide Tools/Options for this package. */
  /*-----------------------------------------*/
  [ProvideOptionPage(typeof(OptionsPage), "Code Commenter", "General", 0, 0, true)]

  /*-------------------------------------------------------------------*/
  /* Load package when VS opens. Has to be done like this because no   */
  /* context exists to load the package whenever a file is loaded      */
  /* into any text editor. `UIContextGuids80.CodeWindow does not work. */
  /*-------------------------------------------------------------------*/
  [ProvideAutoLoad(UIContextGuids.NoSolution)]
  [ProvideAutoLoad(UIContextGuids.SolutionExists)]
  /************************************************************************************************/
  public sealed class VSPackage : Package
    {
    /*---------------------------------------------------------------*/
    /* Visual Studio instance, shell service, and extension options. */
    /*---------------------------------------------------------------*/
    public DTE         app     { get { return GetService   (typeof(DTE))         as DTE; }}
    public IVsUIShell  uiShell { get { return GetService   (typeof(SVsUIShell))  as IVsUIShell; }}
    public OptionsPage options { get { return GetDialogPage(typeof(OptionsPage)) as OptionsPage; }}

    /*-----------------------------------------*/
    /* Table of current comment style file     */
    /* extensions for files loaded in the IDE. */
    /*-----------------------------------------*/
    private Extensions mExtensions;
    public  Extensions extensions { get { return (mExtensions ?? (mExtensions = new Extensions { package = this })); }}

    /*------------------------*/
    /* Command/Hook handlers. */
    /*------------------------*/
    Handler[] mHandlers = new Handler[]
      {
      new CommentStyleHandler { keyBindings = "Text Editor::ctrl+k, ctrl+s" },
      new BlockCommentHandler { keyBindings = "Text Editor::ctrl+k, ctrl+c" }
      };

    /***********************************************************************************************
    * Initialize */
    /**
    * Initializes handlers in `mHandlers`.
    ***********************************************************************************************/
    protected override void Initialize()
      {
      Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
      base.Initialize();

      /*----------------------*/
      /* Initialize handlers. */
      /*----------------------*/
      OleMenuCommandService mcs  = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
      Guid                  guid = new Guid(Symbols.guidCodeComCmds);

      foreach(Handler handler in mHandlers)
        mcs.AddCommand(handler.createCommand(this, guid));
      }
    }
  }
