﻿//    Copyright(C) 2018  Christopher Ryan Mackay

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
using System.Windows.Forms;
using System.IO;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Diagnostics;

namespace SheetRenamer
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        #region CLASS_LEVEL_VARIABLES

        UIApplication myRevitUIApp = null;
        Document myRevitDoc = null;

        public string projectNumber = string.Empty;
        public string drawingDirectory = string.Empty;
        List<string> oldFilesInDirectory = new List<string>();

        public IList<Element> viewSheetSets = null;

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
            
            FilteredElementCollector sheetSetsCol = new FilteredElementCollector(myRevitDoc);
            
            viewSheetSets = sheetSetsCol.OfClass(typeof(ViewSheetSet)).ToElements(); //GET ALL THE SHEETSETS IN THE PROJECT

            projectNumber = myRevitDoc.ProjectInformation.LookupParameter("Project Number").AsString();

            //LOOPS THROUGH ALL THE SHEETSETS IN THE PROJECT AND FILL COMBOBOX FOR SELECTION
            foreach (ViewSheetSet vss in viewSheetSets)
            {
                cbSheetSets.Items.Add(vss.Name);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fldrBrowser = new FolderBrowserDialog();
            fldrBrowser.Description = "Select the directory where the sheets you want to rename are located";

            //GET DIRECTORY WHERE THE DRAWINGS ARE SAVED
            if (fldrBrowser.ShowDialog() == DialogResult.OK)
            {
                drawingDirectory = fldrBrowser.SelectedPath;
                txtDrawingDirectory.Text = drawingDirectory;

                string[] fileEntries = Directory.GetFiles(drawingDirectory); //GET ALL THE FILES IN THE SELECTED DIRECTORY FOR RENAMING

                foreach (string oldFile in fileEntries)
                {
                    oldFilesInDirectory.Add(oldFile);
                }
            }            
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            TaskDialog taskDialog = new TaskDialog("Sheet Renamer");

            taskDialog.MainIcon = TaskDialogIcon.TaskDialogIconNone;
            taskDialog.MainInstruction = "Are you sure you want to rename all the sheets in the directory below?";
            taskDialog.MainContent = drawingDirectory;
            taskDialog.CommonButtons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No;

            if (taskDialog.Show() == TaskDialogResult.Yes)
            {
                List<string> newFiles = new List<string>();
                ViewSet viewSet = null;

                //GET ALL THE SHEETS FROM THE SHEETSET SELECTED
                foreach (ViewSheetSet vs in viewSheetSets)
                {
                    if (vs.Name == cbSheetSets.SelectedItem.ToString())
                    {
                        viewSet = vs.Views;
                    }
                }

                List<string> reOrderedFiles = new List<string>();

                //LOOP THROUGH ALL THE SHEETS FROM THE SHEETSET, CREATE NEW SHEET NAMES, AND FILL NEW FILE LIST
                foreach (ViewSheet oldSheet in viewSet)
                {
                    
                    string sheetNumber = string.Empty;
                    string sheetName = string.Empty;

                    sheetNumber = oldSheet.SheetNumber;
                    sheetName = oldSheet.Name;

                    string rev = string.Empty;

                    rev = oldSheet.LookupParameter("Current Revision").AsString();

                    string newFileName = string.Empty;
                    string newFile = string.Empty;

                    newFileName = projectNumber + "-" + sheetNumber + "_" + rev + ".pdf"; //DPS STANDARD FILE NAMING CONVENTION (E.G. 816075-HE-100_0.pdf)
                    newFile = drawingDirectory + "\\" + newFileName;
        
                    newFiles.Add(newFile);
                    
                    foreach (string file in oldFilesInDirectory)
                    {
                        if (file.Contains(sheetNumber))
                        {
                            reOrderedFiles.Add(file);
                        }
                    }
                    
                }

                int index = 0;

                //LOOP THROUGH EACH FILE IN THE DIRECTORY AND RENAME THE FILE
                foreach (string oldFile in reOrderedFiles)
                {
                    try
                    {

                        string newFile = string.Empty;
                        newFile = newFiles[index];

                        if (File.Exists(newFile))
                        {
                            File.Delete(newFile);
                        }

                        File.Move(oldFile, newFile);
                        
                    }
                    catch (Exception ex)
                    {
                        TaskDialog errorTaskDialog = new TaskDialog("Sheet Renamer");
                        errorTaskDialog.MainInstruction = "An error occured while renaming the files. See message below.";
                        errorTaskDialog.MainContent = "Error Message: " + ex.Message + "\nError Source: " + ex.Source;
                        errorTaskDialog.CommonButtons = TaskDialogCommonButtons.Ok;
                        errorTaskDialog.Show();
                        return;
                    }

                    index += 1;

                }
                TaskDialog completeTaskDialog = new TaskDialog("Sheet Renamer");
                completeTaskDialog.MainInstruction = "The sheets have been renamed successfully";
                completeTaskDialog.MainContent = "";
                completeTaskDialog.CommonButtons = TaskDialogCommonButtons.Ok;
                completeTaskDialog.Show();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {

            string helpFile = string.Empty;
            helpFile = @"C:\Users\" + Environment.UserName + @"\Documents\CRMRevitTools\v2018\CRMRevitTools_Help\sheet_renamer.html";

            if (File.Exists(helpFile))
            {
                Process.Start(helpFile);
            }
            else
            {
                TaskDialog taskDialog = new TaskDialog("Sheet Renamer");

                taskDialog.MainIcon = TaskDialogIcon.TaskDialogIconNone;
                taskDialog.MainInstruction = "The Help file for Sheet Renamer could not be found. It may have been moved or deleted.";
                taskDialog.Show();
            }

        }
    }
}