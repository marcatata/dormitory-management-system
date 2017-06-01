using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dormitory_management_system
{
    class CRenter
    {
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public string RenterType { set; get; }
        public long EGN { set; get; }
        public long ContactNumber { set; get; }
        public string FamilyStatus { set; get; }
        public DateTime DayOfAccommodation { set; get; }
        public DateTime DayOfLeaving { set; get; }

        public class CStudent
        {
            public string Specialty { set; get; }
            public long FacultyNumber { set; get; }
            public string CurrCourse { set; get; }
        }
        public CStudent student = new CStudent();

        public static List<CRenter> RentersList = new List<CRenter>();
    }
}
