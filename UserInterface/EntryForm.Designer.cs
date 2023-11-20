namespace UserInterface
{
    partial class EntryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntryForm));
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.rudioiButton6x6 = new System.Windows.Forms.RadioButton();
            this.rudioiButton8x8 = new System.Windows.Forms.RadioButton();
            this.rudioiButton10x10 = new System.Windows.Forms.RadioButton();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.Cursor = System.Windows.Forms.Cursors.Arrow;
            resources.ApplyResources(this.labelBoardSize, "labelBoardSize");
            this.labelBoardSize.Name = "labelBoardSize";
            // 
            // rudioiButton6x6
            // 
            resources.ApplyResources(this.rudioiButton6x6, "rudioiButton6x6");
            this.rudioiButton6x6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rudioiButton6x6.Name = "rudioiButton6x6";
            this.rudioiButton6x6.TabStop = true;
            this.rudioiButton6x6.UseVisualStyleBackColor = true;
            this.rudioiButton6x6.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rudioiButton8x8
            // 
            resources.ApplyResources(this.rudioiButton8x8, "rudioiButton8x8");
            this.rudioiButton8x8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rudioiButton8x8.Name = "rudioiButton8x8";
            this.rudioiButton8x8.TabStop = true;
            this.rudioiButton8x8.UseVisualStyleBackColor = true;
            this.rudioiButton8x8.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // rudioiButton10x10
            // 
            resources.ApplyResources(this.rudioiButton10x10, "rudioiButton10x10");
            this.rudioiButton10x10.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.rudioiButton10x10.Name = "rudioiButton10x10";
            this.rudioiButton10x10.TabStop = true;
            this.rudioiButton10x10.UseVisualStyleBackColor = true;
            this.rudioiButton10x10.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // labelPlayers
            // 
            resources.ApplyResources(this.labelPlayers, "labelPlayers");
            this.labelPlayers.Name = "labelPlayers";
            // 
            // labelPlayer1
            // 
            resources.ApplyResources(this.labelPlayer1, "labelPlayer1");
            this.labelPlayer1.Name = "labelPlayer1";
            // 
            // textBoxPlayer1
            // 
            resources.ApplyResources(this.textBoxPlayer1, "textBoxPlayer1");
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            // 
            // checkBoxPlayer2
            // 
            resources.ApplyResources(this.checkBoxPlayer2, "checkBoxPlayer2");
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_Checked);
            // 
            // textBoxPlayer2
            // 
            resources.ApplyResources(this.textBoxPlayer2, "textBoxPlayer2");
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            // 
            // buttonDone
            // 
            this.buttonDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonDone, "buttonDone");
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // EntryForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.rudioiButton10x10);
            this.Controls.Add(this.rudioiButton8x8);
            this.Controls.Add(this.rudioiButton6x6);
            this.Controls.Add(this.labelBoardSize);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EntryForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton rudioiButton6x6;
        private System.Windows.Forms.RadioButton rudioiButton8x8;
        private System.Windows.Forms.RadioButton rudioiButton10x10;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.Button buttonDone;
    }
}