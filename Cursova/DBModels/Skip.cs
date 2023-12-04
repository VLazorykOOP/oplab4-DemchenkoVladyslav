using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursova.DBModels
{
    public class Skip
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public DateTime Date { get; set; }
        public int StudentId { get; set; }
        public int ReasonId { get; set; }
        public Lesson Lesson { get; set; }
        public Student Student { get; set; }
        public Reason Reason { get; set; }
    }

}
