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

using System;
using System.Collections.Generic;
using System.IO;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Publish
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        #region CLASS_LEVEL_VARIABLES

        UIApplication myRevitUIApp = null;
        Document myRevitDoc = null;

        public IList<Element> viewSheets = null;
        public string REVIT_VERSION = "v2018";

        ViewSet set = null;
        FileSystemWatcher w = null;

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(UIApplication incomingUIApp)
        {
            InitializeComponent();

            myRevitUIApp = incomingUIApp;
            myRevitDoc = myRevitUIApp.ActiveUIDocument.Document;
            

            FilteredElementCollector sheetsCol = new FilteredElementCollector(myRevitDoc);
            viewSheets = sheetsCol.OfClass(typeof(ViewSheet)).ToElements();

            rbSequence.Checked = true;
            btnPublish.Enabled = false;

            LoadItems();
        }

        private void OnCreated(object source, FileSystemEventArgs eventArgs)
        {
            // Check the count and names of files in the directory.
            // Confirm that all the correct files are finished printing
            // If all files have printed trigger renaming of files.

            DirectoryInfo dInfo = new DirectoryInfo(@"C:\Users\cmackay\Desktop\Print");
            int fCount = Directory.GetFiles(dInfo.FullName, "*", SearchOption.TopDirectoryOnly).Length;

            // Checks to make sure the number of file printed equals the number in the sheet set.
            // If the counts match then rename all the files.
            if (fCount == set.Size)
            {
                #region RenameFiles

                string[] fileEntries = Directory.GetFiles(dInfo.FullName);

                List<string> oldFilesInDirectory = new List<string>();

                foreach (string oldFile in fileEntries)
                    oldFilesInDirectory.Add(oldFile);

                IList<Element> viewSheetSets = null;
                ViewSet viewSet = null;
                List<string> newFiles = new List<string>();

                FilteredElementCollector sheetSetsCol = new FilteredElementCollector(myRevitDoc);
                viewSheetSets = sheetSetsCol.OfClass(typeof(ViewSheetSet)).ToElements();

                //GET ALL THE SHEETS FROM THE SHEETSET SELECTED
                foreach (ViewSheetSet vs in viewSheetSets)
                    if (vs.Name == cbRevisions.SelectedItem.ToString())
                        viewSet = vs.Views;

                List<string> reOrderedFiles = new List<string>();

                foreach (ViewSheet oldSheet in viewSet)
                {
                    string sheetNumber = string.Empty;
                    string sheetName = string.Empty;

                    sheetNumber = oldSheet.SheetNumber;
                    sheetName = oldSheet.Name;

                    // SHEET NUMBER NEEDS TO BE CHECKED FOR THE FOLLOWING SPECIAL CHARACTERS BELOW

                    // THESE NEED TO BE REPLACED WITH '-'
                    // / * " .

                    // REVIT CHECKS FOR THE FOLLOWING CHARACTERS BELOW AND DON'T NEED TO BE HANDLED
                    // \ : {} [] ; < > ? ` ~

                    // REVIT & WINDOWS ALLOW THE CHARACTERS BELOW
                    // ! @ # $ % ^ & * ( ) _ + = - ' ,

                    if (sheetNumber.Contains(@"/")) sheetNumber = sheetNumber.Replace(@"/", "-");

                    if (sheetNumber.Contains("*")) sheetNumber = sheetNumber.Replace("*", "-");

                    if (sheetNumber.Contains("\"")) sheetNumber = sheetNumber.Replace("\"", "-");

                    if (sheetNumber.Contains(".")) sheetNumber = sheetNumber.Replace(".", "-");

                    string rev = string.Empty;

                    rev = oldSheet.LookupParameter("Current Revision").AsString();

                    string newFileName = string.Empty;
                    string newFile = string.Empty;

                    string projectNumber = string.Empty;
                    projectNumber = myRevitDoc.ProjectInformation.LookupParameter("Project Number").AsString();

                    newFileName = projectNumber + "-" + sheetNumber + "_" + rev + ".pdf";
                    newFile = dInfo.FullName + "\\" + newFileName;
                    newFiles.Add(newFile);

                    foreach (string file in oldFilesInDirectory)
                        if (file.Contains(sheetNumber))
                            reOrderedFiles.Add(file);
                }

                int index = 0;

                //LOOP THROUGH EACH FILE IN THE DIRECTORY AND RENAME THE FILE
                foreach (string oldFile in reOrderedFiles)
                {
                    string newF = string.Empty;
                    newF = newFiles[index];

                    if (File.Exists(newF))
                        File.Delete(newF);

                    File.Move(oldFile, newF);

                    index += 1;
                }

                w.Dispose();

                TaskDialog dialog = new TaskDialog("Publish");
                dialog.MainInstruction = "Published successfully";
                dialog.Show();

                #endregion
            }

        }

        private void Publish()
        {
            string prop = cbRevisions.SelectedItem.ToString();
            int selectedSequence = RevisionSequenceNumber(prop);
            Transaction trans = new Transaction(myRevitDoc, "Publish");

            try
            {
                #region CreateSheetSet

                set = new ViewSet();

                foreach (ViewSheet vss in viewSheets)
                {
                    IList<ElementId> revisionIds = vss.GetAllRevisionIds();

                    foreach (ElementId i in revisionIds)
                    {
                        Element elem = myRevitDoc.GetElement(i);
                        Revision r = elem as Revision;

                        int sequenceNumber = r.SequenceNumber;
                        string num = vss.GetRevisionNumberOnSheet(i);
                        string date = r.RevisionDate;

                        if (rbSequence.Checked)
                            if (selectedSequence == sequenceNumber)
                                set.Insert(vss);
                            else if (rbNumber.Checked)
                                if (num == prop)
                                    set.Insert(vss);
                                else
                                if (date == prop)
                                    set.Insert(vss);
                    }
                }

                PrintManager print = myRevitDoc.PrintManager;
                print.PrintRange = PrintRange.Select;
                ViewSheetSetting viewSheetSetting = print.ViewSheetSetting;
                viewSheetSetting.CurrentViewSheetSet.Views = set;
                    
                trans.Start();
                viewSheetSetting.SaveAs(prop);
                trans.Commit();

                #endregion
         
                string dir = @"C:\Users\cmackay\Desktop\Print";

                // Create a file listener and wait until all the files have been printed before renaming
                w = new FileSystemWatcher();
                w.Path = dir;
                w.NotifyFilter = NotifyFilters.LastAccess
                                | NotifyFilters.LastWrite
                                | NotifyFilters.FileName
                                | NotifyFilters.DirectoryName;

                w.Filter = "*.pdf";
                w.Created += OnCreated;

                w.EnableRaisingEvents = true;

                // Print the newly created sheet set
                print.SubmitPrint();
                print.Dispose();

            }
            catch (Exception ex)
            {
                TaskDialog error = new TaskDialog("Publish");
                error.MainInstruction = "Failed to publish " + prop;
                error.MainContent = ex.Message;
                trans.RollBack();
                error.Show();
            }
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            Publish();
        }

        private void cbRevisions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRevisions.SelectedIndex == -1) 
                btnPublish.Enabled = false;
            else 
                btnPublish.Enabled = true;
        }

        private int RevisionSequenceNumber(string selectedSequenceName)
        {
            int seqNum = 0;

            int from = selectedSequenceName.IndexOf("Seq. ") + "Seq. ".Length;
            int to = selectedSequenceName.IndexOf(" - ");

            string num = selectedSequenceName.Substring(from, to - from);
            num = num.Trim();

            seqNum = int.Parse(num);

            return seqNum;
        }

        private string RevisionSequenceName(Revision revision, string desc)
        {
            string seqName = string.Empty;

            seqName = "Seq. " + revision.SequenceNumber + " - " + desc;

            return seqName;
        }

        private void LoadItems()
        {
            foreach (ViewSheet vss in viewSheets)
            {
                IList<ElementId> revisionIds = vss.GetAllRevisionIds();

                foreach (ElementId i in revisionIds)
                {
                    Element elem = myRevitDoc.GetElement(i);
                    Revision r = elem as Revision;

                    if (rbSequence.Checked)
                    {
                        string sequenceName = RevisionSequenceName(r, r.Description);

                        if (!cbRevisions.Items.Contains(sequenceName))
                            cbRevisions.Items.Add(sequenceName);
                    }
                    else if (rbNumber.Checked)
                        if (!cbRevisions.Items.Contains(vss.GetRevisionNumberOnSheet(i)))
                            cbRevisions.Items.Add(vss.GetRevisionNumberOnSheet(i));
                    else
                        if (!cbRevisions.Items.Contains(r.RevisionDate))
                            cbRevisions.Items.Add(r.RevisionDate);
                }
            }
        }

        private void radioButtonCheckChanged(object sender, EventArgs e)
        {
            cbRevisions.Items.Clear();
            cbRevisions.SelectedIndex = -1;

            if (cbRevisions.SelectedIndex == -1)
                btnPublish.Enabled = false;
            else
                btnPublish.Enabled = true;

            LoadItems();
        }
    }
}
