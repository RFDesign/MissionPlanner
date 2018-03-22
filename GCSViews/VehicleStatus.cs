using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DirectShowLib;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using log4net;
using Microsoft.Scripting.Utils;
using MissionPlanner.Controls;
using MissionPlanner.Joystick;
using MissionPlanner.Log;
using MissionPlanner.Utilities;
using MissionPlanner.Warnings;
using OpenTK;
using WebCamService;
using ZedGraph;
using LogAnalyzer = MissionPlanner.Utilities.LogAnalyzer;
using MissionPlanner.Controls.BackstageView;

namespace MissionPlanner.GCSViews
{
    public partial class VehicleStatus : MyUserControl
    {
        public VehicleStatus()
        {
            InitializeComponent();

            var start = AddBackstageViewPage(typeof(MissionPlanner.GCSViews.VehicleStatusView.VehicleOverview), "Vehicle Overview");
            AddBackstageViewPage(typeof(MissionPlanner.GCSViews.VehicleStatusView.ElectricalStatus), "Electrical Status");
            AddBackstageViewPage(typeof(VehicleStatusView.EngineStatus), "Engine Status");
            AddBackstageViewPage(typeof(MissionPlanner.GCSViews.VehicleStatusView.ComponentLife), "Component Life");
            AddBackstageViewPage(typeof(MissionPlanner.GCSViews.VehicleStatusView.ServiceInfo), "Service Info");
            backstageView.ActivatePage(start);
        }

        private BackstageViewPage AddBackstageViewPage(Type userControl, string headerText,
            BackstageViewPage Parent = null, bool advanced = false)
        {
            try
            {
                return backstageView.AddPage(userControl, headerText, Parent, advanced);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
 