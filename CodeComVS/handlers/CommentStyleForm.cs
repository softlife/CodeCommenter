using System.Collections;
using System.Windows.Forms;
using slc.codecom.vs.tools;

namespace slc.codecom.vs.handlers
  {
  /*****************************************************************************
  * Includes dropdown of possible file extension selections. Returns Ok on 
  * Enter and Ignore for ESC.
  *****************************************************************************/
  public class CommentStyleForm : Form
    {
    private Label label1;
    private Button btnOk;
    public  ComboBox cbxStyle;

    /***************************************************************************
    * Constructor
    ***************************************************************************/
    public CommentStyleForm()
      {
      InitializeComponent();

      cbxStyle.DataSource    = new ArrayList(CommentStyle.Extensions);
      cbxStyle.DisplayMember = "extension";
      cbxStyle.ValueMember   = "extension";
      }

    /***************************************************************************
    * form_KeyDown */
    /**
    * Escape closes the dialog and returns Ignore command.
    ***************************************************************************/
    private void form_KeyDown(object sender, KeyEventArgs e)
      {
      if (e.KeyCode == Keys.Escape)
        {
        e.Handled = true;
        DialogResult = DialogResult.Ignore;
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
    this.label1 = new System.Windows.Forms.Label();
    this.cbxStyle = new System.Windows.Forms.ComboBox();
    this.btnOk = new System.Windows.Forms.Button();
    this.SuspendLayout();
    // 
    // label1
    // 
    this.label1.AutoSize = true;
    this.label1.Location = new System.Drawing.Point(12, 15);
    this.label1.Name = "label1";
    this.label1.Size = new System.Drawing.Size(80, 13);
    this.label1.TabIndex = 0;
    this.label1.Text = "Comment Style:";
    // 
    // cbxStyle
    // 
    this.cbxStyle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
    this.cbxStyle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
    this.cbxStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
    this.cbxStyle.FormattingEnabled = true;
    this.cbxStyle.Location = new System.Drawing.Point(98, 12);
    this.cbxStyle.Name = "cbxStyle";
    this.cbxStyle.Size = new System.Drawing.Size(60, 21);
    this.cbxStyle.TabIndex = 1;
    this.cbxStyle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
    // 
    // btnOk
    // 
    this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
    this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
    this.btnOk.Location = new System.Drawing.Point(50, 45);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new System.Drawing.Size(75, 23);
    this.btnOk.TabIndex = 2;
    this.btnOk.Text = "Ok";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
    // 
    // CommentStyleForm
    // 
    this.AcceptButton = this.btnOk;
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.ClientSize = new System.Drawing.Size(174, 80);
    this.Controls.Add(this.btnOk);
    this.Controls.Add(this.cbxStyle);
    this.Controls.Add(this.label1);
    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = "CommentStyleForm";
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
    this.Text = "Select Style";
    this.ResumeLayout(false);
    this.PerformLayout();

      }

    #endregion
    }
  }