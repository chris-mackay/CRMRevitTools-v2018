using System;
using System.Windows.Forms;
using System.IO;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Diagnostics;
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
            Transaction t = new Transaction(myRevitDoc, "Create Schedules");
            t.Start();

            List<ViewSchedule> schedules = new List<ViewSchedule>();

            //Create an empty view schedule of mechanical equipment category.
            ViewSchedule schedule = ViewSchedule.CreateSchedule(myRevitDoc, new ElementId(BuiltInCategory.OST_MechanicalEquipment), ElementId.InvalidElementId);
            schedule.Name = "Equipment Schedule 1";
            schedules.Add(schedule);

            #region Read through the csv and fill a list of all the parameters

            List<string> guids = new List<string>();

            guids.Add("7ea3ef06-f670-4fed-8a0e-fb3b318c4d69"); //DPS_03 _AirPD_Tertiary
            guids.Add("194b530b-d2d8-4c54-8f64-c076d771fe76"); //DPS_03 _EAMoisture_Secondary
            guids.Add("38daa70b-d93d-4d64-a26c-04c59a93d9e9"); //DPS_03_LATExhaust_Dry
            guids.Add("b9bd740f-16c9-45b8-a2ed-bab40753337a"); //DPS_03 _TotalLoad_Tertiary
            guids.Add("0efcf10f-df67-416e-8d49-51e08dadb3ec"); //DPS_03 _EWT_Primary
            guids.Add("92880710-f206-4fe9-9420-4216875a2f15"); //DPS_07_Discharge Pressure
            guids.Add("9855c811-d594-4494-9ca0-73a6f0192131"); //DPS_03_NumCoils_Tertiary
            guids.Add("4cd01b12-31e7-47a2-bd92-2722809aac28"); //DPS_03 _SteamPSIG_Primary
            guids.Add("e9cec914-3a90-48d8-af6f-633e07b86dda"); //DPS_03 _FlowGPM_Secondary
            guids.Add("9fb3ee15-3c89-4d62-9827-a9890468f89f"); //DPS_03 _NumManifolds_Primary
                       
            #endregion
                
            foreach (string guid in guids)
            {
                SchedulableField schedulableField = schedule.Definition.GetSchedulableFields().FirstOrDefault(x => IsSharedParameterSchedulableField(schedule.Document, x.ParameterId, new Guid(guid)));

                if (schedulableField != null)
                    schedule.Definition.AddField(schedulableField);
            }

            t.Commit();
            
        }

        private static bool IsSharedParameterSchedulableField(Document document, ElementId parameterId, Guid sharedParameterId)
        {
            var sharedParameterElement = document.GetElement(parameterId) as SharedParameterElement;

            return sharedParameterElement?.GuidValue == sharedParameterId;
        }
    }
}
