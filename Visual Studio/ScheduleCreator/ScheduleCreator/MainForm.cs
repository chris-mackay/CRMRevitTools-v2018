using System;
using System.IO;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleCreator
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        #region CLASS_LEVEL_VARIABLES

        UIApplication myRevitUIApp = null;
        Document myRevitDoc = null;

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

        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            CreateSchedule();
        }

        private void CreateSchedule()
        {
            
            DirectoryInfo d = new DirectoryInfo(@"C:\Programming\Test");
            FileInfo[] files = d.GetFiles("*.txt");
            int cnt = 1;

            foreach (FileInfo file in files)
            {
                Transaction t = new Transaction(myRevitDoc, "Create Schedule");
                t.Start();

                string scheduleName = "Equipment Schedule " + cnt;
                BuiltInCategory category = BuiltInCategory.OST_MechanicalEquipment;

                List<ViewSchedule> schedules = new List<ViewSchedule>();
                List<string> guids = new List<string>();

                ViewSchedule schedule = ViewSchedule.CreateSchedule(myRevitDoc, new ElementId(category), ElementId.InvalidElementId);
                schedule.Name = scheduleName;
                schedules.Add(schedule);

                string filePath = file.FullName;
                string[] readText = File.ReadAllLines(filePath);
                
                foreach (string s in readText) guids.Add(s);

                foreach (string guid in guids)
                {
                    SchedulableField schedulableField = schedule.Definition.GetSchedulableFields().FirstOrDefault(x => IsSharedParameterSchedulableField(schedule.Document, x.ParameterId, new Guid(guid)));
                    if (schedulableField != null) schedule.Definition.AddField(schedulableField);
                }

                t.Commit();
                cnt++;
            }
        }

        private static bool IsSharedParameterSchedulableField(Document document, ElementId parameterId, Guid sharedParameterId)
        {
            var sharedParameterElement = document.GetElement(parameterId) as SharedParameterElement;

            return sharedParameterElement?.GuidValue == sharedParameterId;
        }
    }
}
