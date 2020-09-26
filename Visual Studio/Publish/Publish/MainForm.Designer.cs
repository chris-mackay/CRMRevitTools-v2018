//    Copyright(C) 2020  Christopher Ryan Mackay

//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with this program.If not, see<https://www.gnu.org/licenses/>.

namespace Publish
{
    partial class MainForm
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
            this.cbRevisions = new System.Windows.Forms.ComboBox();
            this.lblRevision = new System.Windows.Forms.Label();
            this.btnPublish = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rbSequence = new System.Windows.Forms.RadioButton();
            this.rbNumber = new System.Windows.Forms.RadioButton();
            this.rbDate = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbRevisions
            // 
            this.cbRevisions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRevisions.FormattingEnabled = true;
            this.cbRevisions.Location = new System.Drawing.Point(12, 209);
            this.cbRevisions.Name = "cbRevisions";
            this.cbRevisions.Size = new System.Drawing.Size(415, 21);
            this.cbRevisions.TabIndex = 3;
            this.cbRevisions.TabStop = false;
            this.cbRevisions.SelectedIndexChanged += new System.EventHandler(this.cbRevisions_SelectedIndexChanged);
            // 
            // lblRevision
            // 
            this.lblRevision.AutoSize = true;
            this.lblRevision.Location = new System.Drawing.Point(12, 187);
            this.lblRevision.Name = "lblRevision";
            this.lblRevision.Size = new System.Drawing.Size(90, 13);
            this.lblRevision.TabIndex = 2;
            this.lblRevision.Text = "Revision Property";
            // 
            // btnPublish
            // 
            this.btnPublish.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPublish.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPublish.Location = new System.Drawing.Point(271, 269);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(75, 23);
            this.btnPublish.TabIndex = 4;
            this.btnPublish.TabStop = false;
            this.btnPublish.Text = "Publish";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(352, 269);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a revision from the dropdown list below to create\r\na sheet set containing " +
    "all the sheets with the selected \r\nrevision property";
            // 
            // rbSequence
            // 
            this.rbSequence.AutoSize = true;
            this.rbSequence.Location = new System.Drawing.Point(22, 23);
            this.rbSequence.Name = "rbSequence";
            this.rbSequence.Size = new System.Drawing.Size(118, 17);
            this.rbSequence.TabIndex = 0;
            this.rbSequence.Text = "Revision Sequence";
            this.rbSequence.UseVisualStyleBackColor = true;
            this.rbSequence.CheckedChanged += new System.EventHandler(this.radioButtonCheckChanged);
            // 
            // rbNumber
            // 
            this.rbNumber.AutoSize = true;
            this.rbNumber.Location = new System.Drawing.Point(22, 46);
            this.rbNumber.Name = "rbNumber";
            this.rbNumber.Size = new System.Drawing.Size(106, 17);
            this.rbNumber.TabIndex = 1;
            this.rbNumber.Text = "Revision Number";
            this.rbNumber.UseVisualStyleBackColor = true;
            this.rbNumber.CheckedChanged += new System.EventHandler(this.radioButtonCheckChanged);
            // 
            // rbDate
            // 
            this.rbDate.AutoSize = true;
            this.rbDate.Location = new System.Drawing.Point(22, 69);
            this.rbDate.Name = "rbDate";
            this.rbDate.Size = new System.Drawing.Size(92, 17);
            this.rbDate.TabIndex = 2;
            this.rbDate.Text = "Revision Date";
            this.rbDate.UseVisualStyleBackColor = true;
            this.rbDate.CheckedChanged += new System.EventHandler(this.radioButtonCheckChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbNumber);
            this.groupBox1.Controls.Add(this.rbDate);
            this.groupBox1.Controls.Add(this.rbSequence);
            this.groupBox1.Location = new System.Drawing.Point(15, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Revision Properties";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cbRevisions);
            this.panel1.Controls.Add(this.lblRevision);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 255);
            this.panel1.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 304);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPublish);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Publish";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRevisions;
        private System.Windows.Forms.Label lblRevision;
        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbSequence;
        private System.Windows.Forms.RadioButton rbNumber;
        private System.Windows.Forms.RadioButton rbDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
    }
}