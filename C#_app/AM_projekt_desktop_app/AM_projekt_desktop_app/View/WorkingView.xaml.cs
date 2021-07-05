using AM_projekt_desktop_app.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;

namespace AM_projekt_desktop_app.View
{
    /// <summary>
    /// Interaction logic for WorkingView.xaml
    /// </summary>
    public partial class WorkingView : UserControl
    {
        private readonly WorkingViewModel wvm;

        public bool TempCheck;

        public WorkingView()
        {
            InitializeComponent();

            wvm = new WorkingViewModel();
            wvm.SetSLed(SendScreen);

            DataContext = wvm;
        }

        private void SendScreen()
        {
            if (TempCheckBox.IsChecked == (bool?)true)
            {
               
            }
            if (PresCheckBox.IsChecked == (bool?)true)
            {

            }
            if (InteCheckBox.IsChecked == (bool?)true)
            {

            }
            if (IPCheckBox.IsChecked == (bool?)true)
            {

            }
        }

        
    }
}
