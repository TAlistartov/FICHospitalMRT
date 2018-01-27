using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FICHospitalMRT.Models
{
    public class Patients
    {
        public int PatientId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string CellPhone { get; set; }
       // public DateTime BirthDate { get; set; }
        public string ConvertedBirthDate { get; set; }
        public string Adress { get; set; }
        public string JobPlace { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }

        public bool? ResultOfReadingFromDB { get; set; }
    }
}