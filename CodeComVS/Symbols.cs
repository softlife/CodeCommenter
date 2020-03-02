namespace slc.codecom.vs
  {
  /*************************************************************************************************
  * Symbols linking VSCT Command Table definitions in `Symbols.vsct` to C# code.
  * 
  * Use Regex to convert Symbols.vsct to Symbols.cs:
  *   From                                              To
  *   \<GuidSymbol:b+name="{.@}":b+value="\{{.@}\}".+$  public const string \1 = "\2";
  *     \<IDSymbol:b+name="{.@}":b+value="{.@}".+$      public const int    \1 = \2;
  *************************************************************************************************/
  public class Symbols
    {
    public const string guidCodeComPkg     = "2d2d5875-ac6a-4a67-b21d-a21feb8d5fe0";
    public const string guidCodeComCmds    = "ECDB6D40-2F3A-432E-9DB3-C741F25606DF";
    public const int    idToolsMenuGroup   = 0x1000;
    public const int    idCodeComMenu      = 0x1020;
    public const int    idCodeComMenuGroup = 0x1021;
    public const int    idCommentStyleCmd  = 0x0101;
    public const int    idBlockCommentCmd  = 0x0102;
    
    public const string guidCodeComImgs    = "BBDDE128-899C-4450-94BE-EB35FFD6D1E5";
    public const int    bmpBlockComment    = 1;
    public const int    bmpCommentStyle    = 2;
    }
  }
