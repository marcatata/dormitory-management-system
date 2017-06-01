using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dormitory_management_system
{
    class Bill
    {
        public DateTime start { set; get; }
        public DateTime end { set; get; }
        public decimal sum { set; get; }
        public bool payed { set; get; }
    }
}
