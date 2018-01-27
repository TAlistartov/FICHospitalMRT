using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DalLaborant
    {
        public int IdOfLaborant { get; set; }
        public string LastNameOfLaborant { get; set; }
        public string FirstNameOfLaborant { get; set; }
        public bool UsedOrUnusedLaborant { get; set; }
    }
}
