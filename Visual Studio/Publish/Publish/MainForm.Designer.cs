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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cbRevisions = new System.Windows.Forms.ComboBox();
            this.lblRevision = new System.Windows.Forms.Label();
            this.btnPublish = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPrintLocation = new System.Windows.Forms.TextBox();
            this.lblPrintLocation = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbRevisions
            // 
            this.cbRevisions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRevisions.FormattingEnabled = true;
            this.cbRevisions.Location = new System.Drawing.Point(12, 138);
            this.cbRevisions.Name = "cbRevisions";
            this.cbRevisions.Size = new System.Drawing.Size(415, 21);
            this.cbRevisions.TabIndex = 2;
            this.cbRevisions.TabStop = false;
            this.cbRevisions.SelectedIndexChanged += new System.EventHandler(this.cbRevisions_SelectedIndexChanged);
            // 
            // lblRevision
            // 
            this.lblRevision.AutoSize = true;
            this.lblRevision.Location = new System.Drawing.Point(12, 116);
            this.lblRevision.Name = "lblRevision";
            this.lblRevision.Size = new System.Drawing.Size(100, 13);
            this.lblRevision.TabIndex = 1;
            this.lblRevision.Text = "Revision Sequence";
            // 
            // btnPublish
            // 
            this.btnPublish.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPublish.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPublish.Location = new System.Drawing.Point(271, 268);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Size = new System.Drawing.Size(75, 23);
            this.btnPublish.TabIndex = 1;
            this.btnPublish.TabStop = false;
            this.btnPublish.Text = "Publish";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(352, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 91);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.txtPrintLocation);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbRevisions);
            this.panel1.Controls.Add(this.lblPrintLocation);
            this.panel1.Controls.Add(this.lblRevision);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 259);
            this.panel1.TabIndex = 0;
            // 
            // txtPrintLocation
            // 
            this.txtPrintLocation.Location = new System.Drawing.Point(12, 195);
            this.txtPrintLocation.Name = "txtPrintLocation";
            this.txtPrintLocation.Size = new System.Drawing.Size(415, 20);
            this.txtPrintLocation.TabIndex = 4;
            this.txtPrintLocation.TabStop = false;
            // 
            // lblPrintLocation
            // 
            this.lblPrintLocation.AutoSize = true;
            this.lblPrintLocation.Location = new System.Drawing.Point(12, 175);
            this.lblPrintLocation.Name = "lblPrintLocation";
            this.lblPrintLocation.Size = new System.Drawing.Size(72, 13);
            this.lblPrintLocation.TabIndex = 3;
            this.lblPrintLocation.Text = "Print Location";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(352, 221);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.TabStop = false;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 303);
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPrintLocation;
        private System.Windows.Forms.Label lblPrintLocation;
        private System.Windows.Forms.Button btnBrowse;
    }
}