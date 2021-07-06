﻿using AM_projekt_desktop_app.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AM_projekt_desktop_app.View
{
    /// <summary>
    /// Interaction logic for ConfigureView.xaml
    /// </summary>
    public partial class ConfigureView : UserControl
    {
        public ConfigureView()
        {
            InitializeComponent();

            DataContext = new ConfigureViewModel();
        }
    }
}
