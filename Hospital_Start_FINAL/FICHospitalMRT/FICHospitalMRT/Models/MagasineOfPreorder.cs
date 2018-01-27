using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FICHospitalMRT.Models
{
    public class MagasineOfPreorder
    {
        public bool Result { get; set; }
        public bool Latch { get; set; }
        //Fields for Patient Data
        public int PatientId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string CellPhone { get; set; }
        //public DateTime BirthDate { get; set; }
        public string ConvertedBirthDate { get; set; }
        public string Adress { get; set; }
        public string JobPlace { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }

        //Fields for Study Data
        public int StudyId { get; set; }
        public int NewStudyId { get; set; }
        public string StudyType { get; set; }
        public string ConvertedStudyCost { get; set; }
        public List<SelectListItem> AllUsedStudiesFromDb { get; set; }
        public string SelectedStudy { get; set; }

        //Fields for Visit Data
        public string ConvertDateOfVisit { get; set; }
        public string NewConvertDateOfVisit { get; set; }
        public DateTime TimeOfVisit { get; set; }
        public string ConvertedTimeOfVisit { get; set; }
        public string NewConvertedTimeOfVisit { get; set; }
        public bool IsPreorder { get; set; }
        public bool IsNeedSendEmail { get; set; }
        public bool IsHasVisited { get; set; }

        //Fields for Payment

        public bool IsCash { get; set; }
    }
}