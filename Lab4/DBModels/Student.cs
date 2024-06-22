using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DBModels
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public ICollection<Skip> Skips { get; set; }
    }

}
