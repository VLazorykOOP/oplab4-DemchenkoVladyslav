using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DBModels
{
    public class LessonType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }

}
