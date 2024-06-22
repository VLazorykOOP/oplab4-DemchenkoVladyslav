using Lab4.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DisplayModels
{
    public class DisplayLesson
    {
        public int Id { get; set; }
        public string Room { get; set; }
        public string Discipline { get; set; }
        public string Day { get; set; }
        public string Order { get; set; }
        public string LessonType { get; set; }
        public string Teacher { get; set; }
    }

}
