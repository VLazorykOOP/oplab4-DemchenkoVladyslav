using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursova.DBModels
{
    public class Lesson
    {
        public int Id { get; set; }
        public int DisciplineId { get; set; }
        public int ScheduleId { get; set; }
        public int LessonTypeId { get; set; }
        public string Room { get; set; }
        public int TeacherId { get; set; }

        public Discipline Discipline { get; set; }
        public Schedule Schedule { get; set; }
        public LessonType LessonType { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<Skip> Skips { get; set; }
    }

}
