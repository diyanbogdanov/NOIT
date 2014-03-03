using PAL_PlayAndLearn.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PAL_PlayAndLearn.ViewModels;
using System.ComponentModel;

namespace PAL_PlayAndLearn.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    /// 
    public partial class GamePage : UserControl
    {          
        public GamePage()
        {
            InitializeComponent();
            (this.DataContext as ViewModels.ViewModel).SubjectsNames = ViewModels.BindingData.GetAllSubjectsNames();
            (this.DataContext as ViewModels.ViewModel).TestNames = new ObservableCollection<Test>();
        }
    }
}
