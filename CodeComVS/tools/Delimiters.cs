/*******************************************************************************
* $Id: Delimiters.cs 5278 2012-04-04 17:44:34Z sthames $
*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace slc.codecom.vs.tools
  {
  /*****************************************************************************
  * Comment Delimiter Specifications.
  *
  * <p>$Id</p>
  *****************************************************************************/
  public enum Delimiters : int 
    { 
    /** None (Internal Use) */ NULL, 
    /** Default */             Default, 
    /** .NET Page */           ASPX, 
    /** Batch File */          BATCH, 
    /** C Program  */          C, 
    /** HTML Page */           HTML,
    /** Inno Setup File */     ISS,
    /** Pascal */              PAS,
    /** Tex/LaTeX */           TEX,
    /** Visual Basic */        VB, 
    /** XML Markup */          XML 
    }
  }
