using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL_PlayAndLearn.Persisters
{
    public class Persister
    {
        public static IEnumerable<TestsSubjectsName> TestsSubjects
        {
            get
            {
                return new PALEntities().TestsSubjectsNames;
            }
        }

        public static IEnumerable<Test> Tests
        { 
            get
            {
                return new PALEntities().Tests;
            }
        }

        public static IEnumerable<TestQuestion> TestQuestions
        {
            get
            {
                return new PALEntities().TestQuestions;
            }
        }

        public static IEnumerable<UserRegistration> UserRerInfo
        {
            get
            {
                return new PALEntities().UserRegistrations.Include("UserPersonalInformations").Include("UserGameInformations");
            }
        }
    }
}
