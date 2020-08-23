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

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Text;
using System;
using System.IO;
using System.Windows.Forms;

namespace LabEquipment
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class Class1 : IExternalCommand
    {
        public UIApplication app;
        public Document doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            app = commandData.Application;
            doc = app.ActiveUIDocument.Document;

            FilteredElementCollector familySymbols;
            familySymbols = new FilteredElementCollector(doc);
            familySymbols.OfClass(typeof(Autodesk.Revit.DB.FamilySymbol));

            Transaction t = new Transaction(doc, "Create Lab Equipment");
            t.Start();

            foreach (FamilySymbol familySymbol in familySymbols)
            {
                if (familySymbol.Name == "Default")
                {
                    OpenFileDialog ofd = new OpenFileDialog();

                    string libraryPath = string.Empty;
                    libraryPath = "c:\\"; //DEFAULT PATH
                    ofd.InitialDirectory = libraryPath;
                    ofd.Filter = "CSV (Comma delimited) (.csv) Files (*.csv)|*.csv";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string file = string.Empty;
                        file = ofd.FileName;

                        TaskDialog td = new TaskDialog("Create Lab Equipment");
                        td.MainInstruction = "Create lab equipment from " + file + "?";
                        td.CommonButtons = TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No;

                        if (td.Show() == TaskDialogResult.Yes)
                        {
                            StreamReader sr = new StreamReader(file);
                    
                            string csvLine = string.Empty;
                            string typeName = string.Empty;
                            double length = 0.0;
                            double width = 0.0;
                            double height = 0.0;

                            // DPS_A_SE_HEIGHT d7e4498d-9147-4afb-b6eb-872e24eeb644
                            // DPS_A_SE_LENGTH 97fa63a6-3d94-40a6-9d30-4be24475220c
                            // DPS_A_SE_WIDTH f3d738c0-72c9-4c27-93c6-e1e0db3d7b0e

                            Guid guid_length = new Guid("97fa63a6-3d94-40a6-9d30-4be24475220c");
                            Guid guid_width = new Guid("f3d738c0-72c9-4c27-93c6-e1e0db3d7b0e");
                            Guid guid_height = new Guid("d7e4498d-9147-4afb-b6eb-872e24eeb644");
                    
                            while ((csvLine = sr.ReadLine()) != null)
                            {
                                char[] separator = new char[] { ',' };
                                string[] values = csvLine.Split(separator, StringSplitOptions.None);

                                typeName = values[0];
                                length = Convert.ToDouble(values[1]);
                                width = Convert.ToDouble(values[2]);
                                height = Convert.ToDouble(values[3]);

                                FamilySymbol fs = familySymbol.Duplicate(typeName) as FamilySymbol;
                                SetParameterByGuid(fs, guid_length, length);
                                SetParameterByGuid(fs, guid_width, width);
                                SetParameterByGuid(fs, guid_height, height);
                            }
                        }
                    }
                }
            }

            t.Commit();

            TaskDialog.Show("Create Lab Equipment", "Lab equipment created");

            return Result.Succeeded;
        }

        public void SetParameterByGuid(Element e, Guid guid, double dim)
        {
            e.get_Parameter(guid).Set(dim);
        }
    }
}
