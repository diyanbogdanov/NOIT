﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PAL_PlayAndLearn.Pages
{
    /// <summary>
    /// Interaction logic for MainProgram.xaml
    /// </summary>
    public partial class MainProgram : UserControl
    {
        public MainProgram()
        {
            InitializeComponent();
            Switcher.MainProgramSwitcher = this.MainProgramContentControl;
            Switcher.Switch(Switcher.MainProgramSwitcher, new Home());
        }
    }
}
