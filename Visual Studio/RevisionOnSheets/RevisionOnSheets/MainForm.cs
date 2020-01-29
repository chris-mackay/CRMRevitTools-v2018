using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevisionOnSheets
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        #region CLASS_LEVEL_VARIABLES

        UIApplication myRevitUIApp = null;
        Document myRevitDoc = null;

        public IList<Element> viewSheets = null;
        public IList<Element> revisions = null;
        public string REVIT_VERSION = "v2018";

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

            LoadRevisions(cbRevisions);

            cbRevisions.SelectedIndex = 0;
            int seq = cbRevisions.SelectedIndex + 1;

            LoadSheets(dgvSheets);
            SetCheckboxes(dgvSheets, seq);
        }

        private bool RevisionIsOnSheet(ViewSheet viewSheet, int sequence)
        {
            IList<ElementId> revisionIds = viewSheet.GetAllRevisionIds();
            bool flag = false;

            foreach (ElementId i in revisionIds)
            {
                Element elem = myRevitDoc.GetElement(i);
                Revision r = elem as Revision;

                if (r.SequenceNumber == sequence) flag = true; else flag = false;
                if (flag) break;
            }

            return flag;
        }

        private void RemoveRevisionOnSheet(ViewSheet viewSheet, Revision revisionToRemove)
        {
            IList<ElementId> revisionIds = null;
            revisionIds = viewSheet.GetAllRevisionIds();
            revisionIds.Remove(revisionToRemove.Id);

            viewSheet.SetAdditionalRevisionIds(revisionIds);
        }

        private void AddRevisionOnSheet(ViewSheet viewSheet, Revision revisionToAdd)
        {
            IList<ElementId> revisionIds = null;
            revisionIds = viewSheet.GetAllRevisionIds();
            revisionIds.Add(revisionToAdd.Id);

            viewSheet.SetAdditionalRevisionIds(revisionIds);
        }

        private void SetCheckboxes(DataGridView dataGridView, int sequence)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (ViewSheet viewSheet in viewSheets)
                {
                    if (row.Cells["SheetNumber"].Value.ToString() == viewSheet.SheetNumber)
                    {
                        if (RevisionIsOnSheet(viewSheet, sequence))
                            row.Cells["Set"].Value = true;
                        else
                            row.Cells["Set"].Value = false;
                    }
                }
            }
        }

        private void LoadSheets(DataGridView dataGridView)
        {
            DrawingControl.SetDoubleBuffered(dataGridView);
            DrawingControl.SuspendDrawing(dataGridView);

            foreach (ViewSheet viewSheet in viewSheets)
            {
                string number = viewSheet.SheetNumber;
                string name = viewSheet.Name;
                dataGridView.Rows.Add(number, name, false);
            }

            DrawingControl.ResumeDrawing(dataGridView);
        }

        private void LoadRevisions(System.Windows.Forms.ComboBox comboBox)
        {
            FilteredElementCollector revCol = new FilteredElementCollector(myRevitDoc);
            revisions = revCol.OfClass(typeof(Revision)).ToElements();

            foreach (Revision revision in revisions)
            {
                string seq = revision.SequenceNumber.ToString();
                string desc = revision.Description;
                string item = "Seq. " + seq + " - " + desc;

                if (!comboBox.Items.Contains(item))
                {
                    comboBox.Items.Add(item);
                }
            }
        }

        private void cbRevisions_SelectedIndexChanged(object sender, EventArgs e)
        {
            int seq = cbRevisions.SelectedIndex + 1;
            SetCheckboxes(dgvSheets, seq);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Transaction trans = new Transaction(myRevitDoc, "Revision On Sheets");
                trans.Start();

                foreach (DataGridViewRow row in dgvSheets.Rows)
                {
                    foreach (ViewSheet viewSheet in viewSheets)
                    {
                        string sheetNumber = row.Cells["SheetNumber"].Value.ToString();
                        bool set = bool.Parse(row.Cells["Set"].Value.ToString());

                        if (viewSheet.SheetNumber == sheetNumber && set == true)
                        {
                            int seq = cbRevisions.SelectedIndex + 1;

                            foreach (Revision revision in revisions)
                            {
                                if (revision.SequenceNumber == seq)
                                {
                                    AddRevisionOnSheet(viewSheet, revision);
                                }
                            }
                        }
                        else if (viewSheet.SheetNumber == sheetNumber && set == false)
                        {
                            int seq = cbRevisions.SelectedIndex + 1;

                            foreach (Revision revision in revisions)
                            {
                                if (revision.SequenceNumber == seq)
                                {
                                    RemoveRevisionOnSheet(viewSheet, revision);
                                }
                            }
                        }
                    }
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                TaskDialog td = new TaskDialog("Error");
                td.MainInstruction = "Failed to set revision";
                td.MainContent = ex.Message;
                td.Show();
                return;
            }
        }

        public static class DrawingControl
        {
            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr _hWnd, Int32 _wMsg, bool _wParam, Int32 _lParam);

            private const int WM_SETREDRAW = 11;

            public static void SetDoubleBuffered(System.Windows.Forms.Control _ctrl)
            {
                if (!SystemInformation.TerminalServerSession)
                {
                    typeof(System.Windows.Forms.Control).InvokeMember("DoubleBuffered", (System.Reflection.BindingFlags.SetProperty
                                    | (System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)), null, _ctrl, new object[] {
                            true});
                }
            }

            public static void SetDoubleBuffered_ListControls(List<System.Windows.Forms.Control> _ctrlList)
            {
                if (!SystemInformation.TerminalServerSession)
                {
                    foreach (System.Windows.Forms.Control ctrl in _ctrlList)
                    {
                        typeof(System.Windows.Forms.Control).InvokeMember("DoubleBuffered", (System.Reflection.BindingFlags.SetProperty
                                        | (System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)), null, ctrl, new object[] {
                                true});
                    }
                }
            }

            public static void SuspendDrawing(System.Windows.Forms.Control _ctrl)
            {
                SendMessage(_ctrl.Handle, WM_SETREDRAW, false, 0);
            }

            public static void SuspendDrawing_ListControls(List<System.Windows.Forms.Control> _ctrlList)
            {
                foreach (System.Windows.Forms.Control ctrl in _ctrlList)
                {
                    SendMessage(ctrl.Handle, WM_SETREDRAW, false, 0);
                }
            }

            public static void ResumeDrawing(System.Windows.Forms.Control _ctrl)
            {
                SendMessage(_ctrl.Handle, WM_SETREDRAW, true, 0);
                _ctrl.Refresh();
            }

            public static void ResumeDrawing_ListControls(List<System.Windows.Forms.Control> _ctrlList)
            {
                foreach (System.Windows.Forms.Control ctrl in _ctrlList)
                {
                    SendMessage(ctrl.Handle, WM_SETREDRAW, true, 0);
                    ctrl.Refresh();
                }
            }
        }
    }
}