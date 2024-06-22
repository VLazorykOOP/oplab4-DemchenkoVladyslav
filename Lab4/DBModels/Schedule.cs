using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DBModels
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public int Order { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }

}
