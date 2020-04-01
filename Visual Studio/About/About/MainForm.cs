using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Win32;
using System.Diagnostics;

namespace About
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private string version = "v1.0.9";

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "Version " + version;
            lblCurrentChanglog.Text = "See what changed in " + version;
            lblChangelog.Text = "Check for a newer version";
            lblSource.Text = "Source code";
        }

        private void lblCurrentChangelog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string site = "https://github.com/chris-mackay/CRMRevitTools-v2018/releases/tag/" + version;
                Process.Start(site);
            }
            catch (Exception ex)
            {
                TaskDialog td = new TaskDialog("About");
                td.MainInstruction = "Could not open the webpage";
                td.MainContent = ex.Message;
                td.MainIcon = TaskDialogIcon.TaskDialogIconError;
            }
        }

        private void lblChangelog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string site = "https://github.com/chris-mackay/CRMRevitTools-v2018/releases";
                Process.Start(site);
            }
            catch (Exception ex)
            {
                TaskDialog td = new TaskDialog("About");
                td.MainInstruction = "Could not open the webpage";
                td.MainContent = ex.Message;
                td.MainIcon = TaskDialogIcon.TaskDialogIconError;
            }
        }

        private void lblSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string site = "https://github.com/chris-mackay/CRMRevitTools-v2018";
                Process.Start(site);
            }
            catch (Exception ex)
            {
                TaskDialog td = new TaskDialog("About");
                td.MainInstruction = "Could not open the webpage";
                td.MainContent = ex.Message;
                td.MainIcon = TaskDialogIcon.TaskDialogIconError;
            }
        }
    }
}
