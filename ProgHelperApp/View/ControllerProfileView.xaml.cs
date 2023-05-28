﻿using ProgHelperApp.Model;
using ProgHelperApp.ViewModel;
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

namespace ProgHelperApp.View
{
    /// <summary>
    /// Логика взаимодействия для ControllerProfileView.xaml
    /// </summary>
    public partial class ControllerProfileView : Page
    {
        public ControllerProfileView(Frame MainFrame, Employee employee)
        {
            InitializeComponent();
            DataContext = new ControllerProfileVM(MainFrame, employee);
        }
    }
}
