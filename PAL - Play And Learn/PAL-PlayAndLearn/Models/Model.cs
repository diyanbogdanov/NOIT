using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using PAL_PlayAndLearn.Persisters;

namespace PAL_PlayAndLearn.Models
{
    class Model : ViewModels.ViewModelBase
    {
        public IEnumerable<UserRegistration> UserInformation
        {
            get
            {
                return Persister.UserRerInfo;
            }
        }
        public IEnumerable<TestsSubjectsName> TestsSubjects
        {
            get
            {
                return Persister.TestsSubjects;
            }
        }

        public IEnumerable<Test> Tests
        {
            get
            {
                return Persister.Tests;
            }
        }

        public IEnumerable<TestQuestion> TestQuestions
        {
            get
            {
                return Persister.TestQuestions;
            }
        }
    }
}
