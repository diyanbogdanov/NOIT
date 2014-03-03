using System;
using System.Windows;
using System.Windows.Controls;
using PAL_PlayAndLearn.Pages;
using System.ComponentModel;
using PAL_PlayAndLearn.ViewModels;

namespace PAL_PlayAndLearn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        public StartUpWindow() // Initialize Start Up Window
        {            
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            Switcher.StartUpProgramSwitcher = this.StartUpProgramContentControl;
            Switcher.Switch(Switcher.StartUpProgramSwitcher, new LogInPage());            
        }

        protected override void OnClosing(CancelEventArgs e) // When Window Is Closing This Method Execute
        {
            e.Cancel = true;
            MessageBoxResult messageBoxResult = WPFMessageBox.MessageBox.ShowQuestion("Изкате ли да излезите от програмата?\nYes: Изход\nNo: Оставане в програмата"); //MessageBox.Show("Изкате ли да излезите от програмата?\nYes: Изход\nNo: Оставане в програмата", "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (Pages.TestQuestions.dispatcherTimer != null)
                {
                    Pages.TestQuestions.dispatcherTimer.Stop();
                }
                ViewModel.CurrentTestPoints = 0;
                Environment.Exit(0);
            }
        }
    }
}
