using System;
using System.Windows.Forms;

namespace slc.codecom.vs.handlers
  {
  /*****************************************************************************
  * Block Comment input form. Takes input of comment text and options. Ctrl-
  * Enter returns Ok.
  *****************************************************************************/
  public class BlockCommentForm : Form
    {
    protected Label         lblLines;
    protected Button        btnOk;
    protected Button        btnCancel;
    protected Label         label1;
    public    TextBox       txtComment;
    public    CheckBox      chkFormat;
    public    NumericUpDown numLines;

    public BlockCommentForm()
      {
      InitializeComponent();
      }

    /***************************************************************************
    * chkFormat_CheckedChanged */
    /**
    * Enables number of lines input if checked.
    ***************************************************************************/
    private void chkFormat_CheckedChanged(object sender, EventArgs e)
      {
      lblLines.Enabled = chkFormat.Checked;
      numLines.Enabled = chkFormat.Checked;
      }

    /***************************************************************************
    * txtComment_KeyDown */
    /**
    * Triggers Ok button on Ctrl-Enter.
    ***************************************************************************/
    private void txtComment_KeyDown(object sender, KeyEventArgs e)
      {
      if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
        {
        e.Handled = true;
        DialogResult = btnOk.DialogResult;
        Close();
        }
      }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
      {
      this.txtComment = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.chkFormat = new System.Windows.Forms.CheckBox();
      this.numLines = new System.Windows.Forms.NumericUpDown();
      this.lblLines = new System.Windows.Forms.Label();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.numLines)).BeginInit();
      this.SuspendLayout();
      // 
      // txtComment
      // 
      this.txtComment.AcceptsReturn = true;
      this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtComment.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtComment.Location = new System.Drawing.Point(15, 26);
      this.txtComment.Multiline = true;
      this.txtComment.Name = "txtComment";
      this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtComment.Size = new System.Drawing.Size(486, 87);
      this.txtComment.TabIndex = 0;
      this.txtComment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtComment_KeyDown);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(75, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Comment Text";
      // 
      // chkFormat
      // 
      this.chkFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.chkFormat.AutoSize = true;
      this.chkFormat.Checked = true;
      this.chkFormat.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkFormat.Location = new System.Drawing.Point(15, 122);
      this.chkFormat.Name = "chkFormat";
      this.chkFormat.Size = new System.Drawing.Size(64, 17);
      this.chkFormat.TabIndex = 2;
      this.chkFormat.Text = "Format?";
      this.chkFormat.UseVisualStyleBackColor = true;
      this.chkFormat.CheckedChanged += new System.EventHandler(this.chkFormat_CheckedChanged);
      // 
      // numLines
      // 
      this.numLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.numLines.Location = new System.Drawing.Point(119, 121);
      this.numLines.Name = "numLines";
      this.numLines.Size = new System.Drawing.Size(55, 20);
      this.numLines.TabIndex = 3;
      this.numLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // lblLines
      // 
      this.lblLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblLines.AutoSize = true;
      this.lblLines.Location = new System.Drawing.Point(81, 123);
      this.lblLines.Name = "lblLines";
      this.lblLines.Size = new System.Drawing.Size(32, 13);
      this.lblLines.TabIndex = 4;
      this.lblLines.Text = "Lines";
      this.lblLines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(335, 151);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(81, 23);
      this.btnOk.TabIndex = 7;
      this.btnOk.Text = "Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(422, 151);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(81, 23);
      this.btnCancel.TabIndex = 8;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // frmBlockComment
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(517, 184);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.lblLines);
      this.Controls.Add(this.numLines);
      this.Controls.Add(this.chkFormat);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtComment);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmBlockComment";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Paragraph Comment";
      ((System.ComponentModel.ISupportInitialize)(this.numLines)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

      }

    #endregion
    }
  }