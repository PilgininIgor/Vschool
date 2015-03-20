namespace ILS.Domain
{
    using System.Collections.Generic;

	public class User : EntityBase
	{
		public string Name { get; set; }
		public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }

        public int Coins { get; set; }
	    public int Rating { get; set; }

	    public bool FacultyStands_Seen { get; set; }
        public bool FacultyStands_Finish { get; set; }
        public bool HistoryStand_Seen { get; set; }
        public bool HistoryStand_Finish { get; set; }
        public bool ScienceStand_Seen { get; set; }
        public bool ScienceStand_Finish { get; set; }
        public bool StaffStand_Seen { get; set; }
	    public bool StaffStand_Finish { get; set; }
        public bool LogotypeJump { get; set; }
        public bool TableJump { get; set; }
        public bool TerminalJump { get; set; }
        public bool LadderJump_First { get; set; }
        public bool LadderJump_All { get; set; }
        public bool LetThereBeLight { get; set; }
        public bool PlantJump_First { get; set; }
        public bool PlantJump_Second { get; set; }
        public bool BarrelRoll { get; set; }
        public bool FirstVisitLecture { get; set; }
        public bool FirstVisitTest { get; set; }
        public int Teleportations { get; set; }
        public int ParagraphsSeen { get; set; }
        public int TestsFinished { get; set; }
        
        public virtual ICollection<Role> Roles {get; set;}
        public virtual ICollection<CourseRun> CoursesRuns { get; set; }

        public User()
        {
            Roles = new List<Role>();
            CoursesRuns = new List<CourseRun>();
        }
	}
}