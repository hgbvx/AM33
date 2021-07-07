using AM_projekt_desktop_app.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using AM_projekt_desktop_app.Model;
using System.Net;

namespace AM_projekt_desktop_app.View
{
    /// <summary>
    /// Interaction logic for WorkingView.xaml
    /// </summary>
    public partial class WorkingView : UserControl
    {
        private readonly WorkingViewModel wvm;
        private IoTserver iots;

        public WorkingView()
        {
            InitializeComponent();

            wvm = new WorkingViewModel();
            wvm.SetSLed(SendScreen);

            iots = new IoTserver(wvm.ipAddress);

            DataContext = wvm;
        }

        private void SendScreen()
        {
            string arg1 = "", arg2 = "", arg3 = "", arg4 = "";
            
            if (TempCheckBox.IsChecked == (bool?)true)
            {
                arg1 = "arg1=t";
            }
            if (PresCheckBox.IsChecked == (bool?)true)
            {
                if (String.IsNullOrEmpty(arg1))
                {
                    arg2 = "arg2=p";
                }
                else { arg2 = "&arg2=p"; }
                
            }
            if (InteCheckBox.IsChecked == (bool?)true)
            {
                if (String.IsNullOrEmpty(arg1) && String.IsNullOrEmpty(arg2))
                {
                    arg3 = "arg3=l";
                }
                else { arg3 = "&arg3=l"; }
            }
            if (IPCheckBox.IsChecked == (bool?)true)
            {
                if (String.IsNullOrEmpty(arg1) && String.IsNullOrEmpty(arg2) && String.IsNullOrEmpty(arg3))
                {
                    arg4 = "arg4=i";
                }
                else { arg4 = "&arg4=i"; }
            }
            //string disp_url = $"http://192.168.0.11/post_display.php/?{arg1}{arg2}{arg3}{arg4}";
            string disp_url = $"post_display.php/?{arg1}{arg2}{arg3}{arg4}";
            iots.Display_Set(disp_url);

        }

        
    }
}
