using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.DBModels
{
    public class Reason
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Skip> Skips { get; set; }
    }
}
