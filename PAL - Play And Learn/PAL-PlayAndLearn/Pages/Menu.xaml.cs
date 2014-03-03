using System;
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
using PAL_PlayAndLearn.Models;
using PAL_PlayAndLearn.ViewModels;


namespace PAL_PlayAndLearn.Pages
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
            Model model = new Model();
            SubjectsNumber.Content = model.TestsSubjects.Count();
            TestNumbers.Content = model.Tests.Count();
            QuestionsNumber.Content = model.TestQuestions.Count();
        }

        private void OpenHomeImage(object sender, MouseButtonEventArgs e)
        {
            Switcher.Switch(Switcher.MainProgramSwitcher, new Home());
        }

        private void OpenRanking(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(Switcher.MainProgramSwitcher, new Ranking());
        }

        private void OpenGame(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(Switcher.MainProgramSwitcher, new GamePage());
        }

        private void OpenInformation(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(Switcher.MainProgramSwitcher, new Information());
        }

        private void OpenHome(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(Switcher.MainProgramSwitcher, new Home());
        }        
    }

    public partial class ContextMenuClick
    {
        private void OpenProfile(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(Switcher.MainProgramSwitcher, new Profile());
        }

        private void OpenRanking(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(Switcher.MainProgramSwitcher, new Ranking());
        }

        private void SignOut(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = WPFMessageBox.MessageBox.ShowQuestion("Изкате ли да се отпишите от програмата?\nYes: ДА\nNo: Оставане в програмата");
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Switcher.Switch(Switcher.StartUpProgramSwitcher, new LogInPage());
            }            
        }
    }
}
