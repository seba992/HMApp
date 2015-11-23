﻿using DiamondApp.ViewModels;
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
using System.Windows.Shapes;

namespace DiamondApp.Views
{
    /// <summary>
    /// Interaction logic for RemoveUserView.xaml
    /// </summary>
    public partial class RemoveUserView : Window
    {
        public RemoveUserView()
        {
            InitializeComponent();
            RemoveUserViewModel removeUserView = new RemoveUserViewModel();
            DataContext = removeUserView;
        }
    }
}