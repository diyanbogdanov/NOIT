//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PAL_PlayAndLearn
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserGameInformation
    {
        public int ID { get; set; }
        public string LevelInGame { get; set; }
        public string Points { get; set; }
        public string NumberOfTests { get; set; }
        public string BestScoreFromTests { get; set; }
        public string ScoreForMount { get; set; }
        public int UserRegID { get; set; }
    
        public virtual UserRegistration UserRegistration { get; set; }
    }
}