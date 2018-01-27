using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FICHospitalMRT.Models
{
    public class MagasineInFact
    {
        public int NumberInOrder { get; set; }
        public bool Result { get; set; }

        //Fields for Patient Data
        public int PatientId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string CellPhone { get; set; }
        public string ConvertedBirthDate { get; set; }
        public string Adress { get; set; }
        public string JobPlace { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }

        //Fields for Study Data
        public int StudyId { get; set; }
        public string StudyType { get; set; }
        public string StudyCost { get; set; }
        public List<SelectListItem> AllUsedStudiesFromDb { get; set; }
        public string SelectedStudyInFact { get; set; }
        public string NewSelectedStudyInFact { get; set; }

        //Fields for Laborants
        public string SelectedLaborantInFact { get; set; }
        public int? LaborantId { get; set; }
        public string LaborantData { get; set; }
        public int LaborantId1 { get; set; }
        public int? NewLaborantId1 { get; set; }
        public string NewLaborant1 { get; set; }
        public string NewLaborant2 { get; set; }
        public int? LaborantId2 { get; set; }
        public int? NewLaborantId2 { get; set; }

        public List<SelectListItem> AllLaborantsFromDb { get; set; }

        //Fields for Visit Data
        public string ConvertDateOfVisit { get; set; }        
        public string NewConvertDateOfVisit { get; set; }
        public string ConvertedTimeOfVisit { get; set; }
        public bool IsPreorder { get; set; }
        public bool IsNeedSendEmail { get; set; }
        public bool IsHasVisited { get; set; }

        //Fields for Payment
        public bool IsCash { get; set; }
        public float FinalCost { get; set; }
        public string ConvertedFinalCost { get; set; }
        public string NoteForDiscount { get; set; }

        //Fields for TheStudyProcess
        public string DoctorData { get; set; }
        
        //Counting Money
        //public float CashSumByDate { get; set; }
        //public float TerminalSumByDate { get; set; }
    }
}