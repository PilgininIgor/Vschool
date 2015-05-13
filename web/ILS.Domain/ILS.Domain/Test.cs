using System.Collections.Generic;

namespace ILS.Domain
{
    public class Test : ThemeContent
    {
        public virtual ICollection<Question> Questions { get; set; }
        
        public int MinResult { get; set; }

        public bool IsComposite { get; set; }

        public virtual ICollection<TestRun> TestRuns { get; set; }

        public int AttemptsNumber { get; set; }

        public Test()
        {
            Questions = new List<Question>();
            TestRuns = new List<TestRun>();
        }
    }
}