using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows;
using System.Collections.ObjectModel;
using PAL_PlayAndLearn.Models;
using PAL_PlayAndLearn.Pages;
using PAL_PlayAndLearn.Behavior;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace PAL_PlayAndLearn.ViewModels
{
    class ViewModel : ViewModelBase
    {
        public static int ID { get; set; } // Current User ID

        public static int TestID { get; set; } // Current Selected Test ID

        public static int CurrentTestPoints { get; set; } // Current Test Win Points

        private IEnumerable<UserInfoSort> userSortInfo;

        public IEnumerable<UserInfoSort> SortingInformation // Collection Of All Users
        {
            get
            {
                if (this.userSortInfo == null)
                {
                    this.userSortInfo = BindingData.GetAll;
                }
                return this.userSortInfo;
            }
        }

        private UserInfo currentUserInfo;

        public UserInfo CurrentUserInformation // Used In Profile Page For Showing Current User Information
        {
            get
            {
                if (this.currentUserInfo == null)
                {
                    this.currentUserInfo = BindingData.UserInformation;
                }
                return currentUserInfo;
            }
        }

        private QuestionGameInfo currentTestInfo;

        public QuestionGameInfo CurrentTestInformation
        {
            get
            {
                if (this.currentTestInfo == null)
                {
                    this.currentTestInfo = BindingData.GetCurentTestNameSubject;
                }
                return this.currentTestInfo;
            }
        }

        private ObservableCollection<TestsSubjectsName> subjectsNames;

        public ObservableCollection<TestsSubjectsName> SubjectsNames // Collection Of All Subjects Names
        {
            get
            {
                return subjectsNames;
            }
            set
            {
                subjectsNames = value;
                OnPropertyChanged("SubjectsNames");
            }
        }

        private ObservableCollection<Test> testNames;

        public ObservableCollection<Test> TestNames // Collection Of All Tests Names
        {
            get
            {
                return this.testNames;
            }
            set
            {
                if (value != this.testNames)
                {
                    this.testNames = value;
                    OnPropertyChanged("TestNames");
                }
            }
        }

        private ObservableCollection<TestQuestion> testQuestions;

        public ObservableCollection<TestQuestion> TestQuestions // Collection Of All Choosen Test Questions
        {
            get { return this.testQuestions; }
            set
            {
                if (value != this.testQuestions)
                {
                    this.testQuestions = value;
                    OnPropertyChanged("TestQuestions");
                }
            }
        }

        private TestsSubjectsName selectedSubjectNames;

        public TestsSubjectsName SelectedSubjectNames // Used In ListView On Subject SelectedItem
        {
            get { return this.selectedSubjectNames; }
            set
            {
                if (value != this.selectedSubjectNames)
                {
                    this.selectedSubjectNames = value;
                    OnSelectedSubjectNameChanged(value);
                    OnPropertyChanged("SelectedSubjectNames");
                }
            }
        }

        private void OnSelectedSubjectNameChanged(TestsSubjectsName subjectName) // When Select Subject Show All Test For This Subject 
        {
            // Get The list of Tests with the selected subjectName ID
            // and put the result in the observable collection
            TestNames = new ObservableCollection<Test>(BindingData.GetAllTestsNames(subjectName.ID));
        }

        private Test selectedTestName;

        public Test SelectedTestName // Used In ListView On Test SelectedItem
        {
            get { return this.selectedTestName; }
            set
            {
                if (value != this.selectedTestName)
                {
                    this.selectedTestName = value;
                    OnSelectedTestNameChanged(value);
                    OnPropertyChanged("SelectedTestName");
                }
            }
        }

        private void OnSelectedTestNameChanged(Test value) // When Select Test Open It
        {
            TestID = value.ID;
            Pages.TestQuestions.time = 45;
            Switcher.Switch(Switcher.StartUpProgramSwitcher, new TestQuestions());
        }

        public void Sort() // Sort All Users
        {
            var userCollectionView = this.GetDefaultView(this.userSortInfo);
            if (userCollectionView.SortDescriptions.Count == 0)
            {
                userCollectionView.SortDescriptions.Add(new SortDescription("Points", ListSortDirection.Descending));
                userCollectionView.SortDescriptions.Add(new SortDescription("LevelInGame", ListSortDirection.Descending));
                userCollectionView.SortDescriptions.Add(new SortDescription("NumberOfTests", ListSortDirection.Descending));
                userCollectionView.SortDescriptions.Add(new SortDescription("BestScoreFromTest", ListSortDirection.Descending));
            }
            else
            {
                userCollectionView.SortDescriptions.Clear();
            }         
        }

        private ICollectionView GetDefaultView<T>(IEnumerable<T> collection) // Get Default View Of Collection
        {
            return CollectionViewSource.GetDefaultView(collection);
        }

        private ViewModel dataContext;

        public ViewModel DataContext // DataContext Used In Changing Current Collection Next Element
        {
            get
            {
                return this.dataContext;
            }
            set
            {
                this.dataContext = value;
                OnPropertyChanged("DataContext");
            }
        }

        private ICommand answerQuestion;

        public ICommand AnswerQuestion // Show Answered Questions
        {
            get
            {
                if (this.answerQuestion == null)
                {
                    this.answerQuestion = new RelayCommand(this.HandleAnswerQuestionCommand);
                }
                return this.answerQuestion;
            }
        }

        private int remainQuestions;

        public int RemainQuestions // Show Remaining Questions
        {
            get { return this.remainQuestions; }
            set
            {
                this.remainQuestions = value;
                OnPropertyChanged("RemainQuestions");
            }
        }

        private void HandleAnswerQuestionCommand(object parameter) // Handler For AnswerQuestion Command
        {
            var commandParameter = (object[])parameter;
            string answer = commandParameter[0].ToString();
            int correctAnswer = int.Parse(commandParameter[1].ToString());
            int correctAnswerID = int.Parse(commandParameter[2].ToString());
            if (correctAnswer == 1)
            {
                var currentQuestionAnswerAndPoints = TestQuestions.Where(c => c.ID == correctAnswerID).Select(t => new TestQuestion
                {
                    AnswerOne = t.AnswerOne,
                    Points = t.Points
                }).Single();
                if (answer == currentQuestionAnswerAndPoints.AnswerOne)
                {
                    CurrentTestPoints += currentQuestionAnswerAndPoints.Points;
                }
            }
            else if (correctAnswer == 2)
            {
                var currentQuestionAnswerAndPoints = TestQuestions.Where(c => c.ID == correctAnswerID).Select(t => new TestQuestion
                {
                    AnswerTwo = t.AnswerTwo,
                    Points = t.Points
                }).Single();
                if (answer == currentQuestionAnswerAndPoints.AnswerTwo)
                {
                    CurrentTestPoints += currentQuestionAnswerAndPoints.Points;
                }
            }
            else if (correctAnswer == 3)
            {
                var currentQuestionAnswerAndPoints = TestQuestions.Where(c => c.ID == correctAnswerID).Select(t => new TestQuestion
                {
                    AnswerThree = t.AnswerThree,
                    Points = t.Points
                }).Single();
                if (answer == currentQuestionAnswerAndPoints.AnswerThree)
                {
                    CurrentTestPoints += currentQuestionAnswerAndPoints.Points;
                }
            }
            else
            {
                var currentQuestionAnswerAndPoints = TestQuestions.Where(c => c.ID == correctAnswerID).Select(t => new TestQuestion
                {
                    AnswerFour = t.AnswerFour,
                    Points = t.Points
                }).Single();
                if (answer == currentQuestionAnswerAndPoints.AnswerFour)
                {
                    CurrentTestPoints += currentQuestionAnswerAndPoints.Points;
                }
            }
            Pages.TestQuestions.time = 46;
            RemainQuestions--;
            Pages.TestQuestions.animation = new ColorAnimation(Colors.LightGreen, Colors.Red, new Duration(TimeSpan.FromSeconds(Pages.TestQuestions.time)));
            Pages.TestQuestions.brush.BeginAnimation(SolidColorBrush.ColorProperty, Pages.TestQuestions.animation);
            DataContext.Next();
        }

        public void Next() // Get Next Element In ViewModel Collection
        {
            var testQuestionsCollectionView = this.GetDefaultView(this.TestQuestions);
            testQuestionsCollectionView.MoveCurrentToNext();
            if (testQuestionsCollectionView.IsCurrentAfterLast)
            {
                testQuestionsCollectionView.MoveCurrentToLast();
                CompleteTest();
            }
        }

        private static void CompleteTest()
        {
            Pages.TestQuestions.dispatcherTimer.Stop();
            int points = ViewModel.CurrentTestPoints;
            PALEntities dataContext = new PALEntities();
            UserGameInformation currentUserGameInformation = GetUserById(dataContext, ID);
            int currentUserLevel = Convert.ToInt32(currentUserGameInformation.LevelInGame);
            int currentUserPoints = Convert.ToInt32(currentUserGameInformation.Points) + points;
            bool levelUp = false;
            currentUserGameInformation.Points = currentUserPoints.ToString();
            if (Convert.ToInt32(currentUserGameInformation.BestScoreFromTests) < points)
            {
                currentUserGameInformation.BestScoreFromTests = points.ToString();
            }
            if (currentUserLevel > 2)
            {
                int levelSpecialValue = 0;
                for (int i = 0; i < currentUserLevel; i++)
                {
                    levelSpecialValue += 30 * i;
                }
                if (((currentUserPoints - 100) - levelSpecialValue) / 100 > currentUserLevel )
                {
                    levelUp = true;
                    currentUserLevel++;
                    currentUserGameInformation.LevelInGame = (currentUserLevel++).ToString();
                }
            }
            else
            {                
                if ((currentUserPoints / 100) > currentUserLevel)
                {
                    levelUp = true;
                    currentUserLevel++;
                    currentUserGameInformation.LevelInGame = (currentUserPoints / 100).ToString();
                }
            }
            currentUserGameInformation.NumberOfTests = (Convert.ToInt32(currentUserGameInformation.NumberOfTests) + 1).ToString();
            dataContext.SaveChanges();
            ViewModel.CurrentTestPoints = 0;
            string msgBoxInfo = string.Format("Честито, вие завършихте успешно избраният от вас тест.\nСпечелени точки: {0}", points);            
            Switcher.Switch(Switcher.StartUpProgramSwitcher, new MainProgram());
            WPFMessageBox.MessageBox.ShowInformation(msgBoxInfo);
            if (levelUp)
            {
                string msgBoxLeveUp = string.Format("Честито, вие вдигнахте ниво.\nСега сте {0} ниво", currentUserLevel);
                WPFMessageBox.MessageBox.ShowInformation(msgBoxLeveUp);
            }
        }

        private static UserGameInformation GetUserById(PALEntities dataContex, int id)
        {
            UserGameInformation userGameInfo = dataContex.UserGameInformations.FirstOrDefault(p => p.UserRegID == id);
            return userGameInfo;
        }
    } // Main View Model

    class BindingData // Bind Data From SQL DataBase
    {
        public static UserInfo UserInformation // Get Current User Information
        {
            get
            {
                return new Model().UserInformation.Where(c => c.ID == ViewModel.ID).Select(c => new UserInfo
                {
                    UserName = c.UserName,
                    UserEMail = c.UserEMail,
                    Password = c.UserPassword,
                    UserAvatar = c.UserPersonalInformations.Select(t => t.AvatarPicture).FirstOrDefault(),
                    UserCityName = c.UserPersonalInformations.Select(t => t.CityName).FirstOrDefault(),
                    UserClass = c.UserPersonalInformations.Select(t => t.Class).FirstOrDefault(),
                    UserSchoolName = c.UserPersonalInformations.Select(t => t.SchoolName).FirstOrDefault(),
                    UserSex = c.UserPersonalInformations.Select(t => t.Sex).FirstOrDefault(),
                    UserYears = c.UserPersonalInformations.Select(t => t.YearsOld).FirstOrDefault(),
                    LevelInGame = c.UserGameInformations.Select(t => t.LevelInGame).FirstOrDefault(),
                    Points = c.UserGameInformations.Select(t => t.Points).FirstOrDefault(),
                    NumberOfTests = c.UserGameInformations.Select(t => t.NumberOfTests).FirstOrDefault(),
                    BestScoreFromTest = c.UserGameInformations.Select(t => t.BestScoreFromTests).FirstOrDefault(),
                }).Single();
            }
        }

        public static IEnumerable<UserInfoSort> GetAll // Get All Users Information
        {
            get
            {
                return new Model().UserInformation.Select(c => new UserInfoSort
                {
                    UserName = c.UserName,
                    UserYears = c.UserPersonalInformations.Select(t => t.YearsOld).FirstOrDefault(),
                    UserAvatar = c.UserPersonalInformations.Select(t => t.AvatarPicture).FirstOrDefault(),
                    UserCityName = c.UserPersonalInformations.Select(t => t.CityName).FirstOrDefault(),
                    UserSchoolName = c.UserPersonalInformations.Select(t => t.SchoolName).FirstOrDefault(),
                    LevelInGame = c.UserGameInformations.Select(t => t.LevelInGame).FirstOrDefault(),
                    Points = c.UserGameInformations.Select(t => Convert.ToInt32(t.Points)).FirstOrDefault(),
                    BestScoreFromTest = c.UserGameInformations.Select(t => t.BestScoreFromTests).FirstOrDefault()
                });
            }
        }

        public static ObservableCollection<TestsSubjectsName> GetAllSubjectsNames() // Get All Subject Names
        {
            var enumerable = new Model().TestsSubjects.Select(c => new TestsSubjectsName
            {
                ID = c.ID,
                TestsSubjectName = c.TestsSubjectName
            });
            return new ObservableCollection<TestsSubjectsName>(enumerable);
        }

        public static ObservableCollection<Test> GetAllTestsNames(int selectedID) // Get All Test Names
        {
            var enumerable = new Model().Tests.Where(c => c.TestSBJNameID == selectedID).Select(t => new Test
            {
                ID = t.ID,
                TestName = t.TestName
            });
            return new ObservableCollection<Test>(enumerable);
        }

        public static ObservableCollection<TestQuestion> GetAllTestQuestions(int testID) // Get All Questions Of Selected Test
        {
            var enumerable = new Model().TestQuestions.Where(c => c.ITTestID == testID).Select(t => new TestQuestion
            {
                ID = t.ID,
                QuestionText = t.QuestionText,
                AnswerOne = t.AnswerOne,
                AnswerTwo = t.AnswerTwo,
                AnswerThree = t.AnswerThree,
                AnswerFour = t.AnswerFour,
                CorrectAnswer = t.CorrectAnswer,
                Points = t.Points
            });
            return new ObservableCollection<TestQuestion>(enumerable);
        }

        public static QuestionGameInfo GetCurentTestNameSubject
        {
            get
            {
                return new Model().Tests.Where(c => c.ID == ViewModel.TestID).Select(t => new QuestionGameInfo { CurrentTestName = t.TestName, CurrentSubjectName = t.TestsSubjectsName.TestsSubjectName }).Single();
            }
        }
    }

    class UserInfo // Class Used In BindingData - UserInformation
    {
        #region User Register Inforamtion

        public string UserName { get; set; }

        public string UserEMail { get; set; }

        public string Password { get; set; }

        #endregion

        #region User Personal Information

        public string UserAvatar { get; set; }

        public string UserCityName { get; set; }

        public string UserSchoolName { get; set; }

        public string UserClass { get; set; }

        public string UserSex { get; set; }

        public string UserYears { get; set; }

        #endregion

        #region User Game Information

        public string LevelInGame { get; set; }

        public string Points { get; set; }

        public string NumberOfTests { get; set; }

        public string BestScoreFromTest { get; set; }

        #endregion
    }

    class UserInfoSort // Class Used In BindingData - UserInformation
    {
        #region User Register Inforamtion

        public string UserName { get; set; }

        #endregion

        #region User Personal Information

        public string UserAvatar { get; set; }

        public string UserCityName { get; set; }

        public string UserSchoolName { get; set; }

        public string UserClass { get; set; }

        public string UserYears { get; set; }

        #endregion

        #region User Game Information

        public string LevelInGame { get; set; }

        public int Points { get; set; }

        public string BestScoreFromTest { get; set; }

        #endregion
    }

    class QuestionGameInfo
    {
        public string CurrentTestName { get; set; }
        public string CurrentSubjectName { get; set; }
    }
}
