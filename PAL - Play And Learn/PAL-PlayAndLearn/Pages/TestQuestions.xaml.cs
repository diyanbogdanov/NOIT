using PAL_PlayAndLearn.Behavior;
using PAL_PlayAndLearn.ViewModels;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PAL_PlayAndLearn.Pages
{
    /// <summary>
    /// Interaction logic for TestQuestions.xaml
    /// </summary>
    public partial class TestQuestions : UserControl
    {
        public static int time = 45; //Time for answer
        public static ColorAnimation animation;
        public static SolidColorBrush brush;
        public static DispatcherTimer dispatcherTimer;
        public TestQuestions()
        {
            InitializeComponent();
            (this.DataContext as ViewModels.ViewModel).TestQuestions = ViewModels.BindingData.GetAllTestQuestions(ViewModel.TestID);
            int maxPoints = 0;
            foreach (int points in (this.DataContext as ViewModels.ViewModel).TestQuestions.Select(c => c.Points))
            {
                maxPoints += points;
            }
            maximalPointsTextBlock.Content = maxPoints;
            int currentTestQuestionsNumber = (this.DataContext as ViewModels.ViewModel).TestQuestions.Count();
            AllQuestions.Content = currentTestQuestionsNumber;
            (this.DataContext as ViewModels.ViewModel).RemainQuestions = currentTestQuestionsNumber;
            InitializeDispatcherTimer();
        }

        private void Countdown(object sender, EventArgs e)
        {            
            if (time > 0)
            {
                if (time <= 10)
                {
                    time--;
                    Timer.Content = string.Format("00:0{0}:0{1}", time / 60, time % 60);
                }
                else
                {
                    time--;
                    Timer.Content = string.Format("00:0{0}:{1}", time / 60, time % 60);
                }
            }
            else
            {
                dispatcherTimer.Stop();
                WPFMessageBox.MessageBox.ShowWarning("Вие не отговорихте на въпроса в интервалът от 45(четиридесет и пет) секунди!", "Времето ви изтече.");
                Switcher.Switch(Switcher.StartUpProgramSwitcher, new MainProgram());
            }
        }

        private void GreenToRedAnimation()
        {
            brush = GreenToRed as SolidColorBrush; 
            animation = new ColorAnimation(Colors.LightGreen, Colors.Red, new Duration(TimeSpan.FromSeconds(time)));
            brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private void InitializeDispatcherTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += Countdown;
        }

        public void NextQuestion(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ViewModels.ViewModel).DataContext = this.GetDataContext();
        }

        private ViewModel GetDataContext()
        {
            var dataContext = this.DataContext;
            return dataContext as ViewModel;
        }

        private void ShowQuestions(object sender, RoutedEventArgs e)
        {
            InformationStackPanel.Visibility = Visibility.Collapsed;
            QuestionStackPanel.Visibility = Visibility.Visible;
            dispatcherTimer.Start();
            GreenToRedAnimation();
        }

        private void QuitTest(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(Switcher.StartUpProgramSwitcher, new MainProgram());
        }
    }
}
