using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public  class DalMagasines : IStudy,IPatient
    {
        //Fields for Patient Information
        private int pat_id;
        public int Pat_Id
        {
            get { return pat_id; }
            set { pat_id = value; }
        }

        private string pat_lastName;
        public string Pat_LastName
        {
            get { return pat_lastName; }
            set { pat_lastName = value; }
        }

        private string pat_firstName;
        public string Pat_FirstName
        {
            get { return pat_firstName; }
            set { pat_firstName = value; }
        }

        private string pat_middleName;
        public string Pat_MiddleName
        {
            get { return pat_middleName; }
            set { pat_middleName = value; }
        }

        private string pat_cellPhone;
        public string Pat_CellPhone
        {
            get { return pat_cellPhone; }
            set { pat_cellPhone = value; }
        }

        private string pat_convertedBirthDate;
        public string Pat_ConvertedBirthDate
        {
            get { return pat_convertedBirthDate; }
            set { pat_convertedBirthDate = value; }
        }

        private DateTime? pat_birthDateToDB;
        public DateTime? Pat_BirthDateToDB
        {
            get { return pat_birthDateToDB; }
            set { pat_birthDateToDB = value; }
        }

        private string pat_adress;
        public string Pat_Adress
        {
            get { return pat_adress; }
            set { pat_adress = value; }
        }

        private string pat_jobPlace;
        public string Pat_JobPlace
        {
            get { return pat_jobPlace; }
            set { pat_jobPlace = value; }
        }

        private string pat_email;
        public string Pat_Email
        {
            get { return pat_email; }
            set { pat_email = value; }
        }

        private string pat_note;
        public string Pat_Note
        {
            get { return pat_note; }
            set { pat_note = value; }
        }

        ////////////////////////////////////////
        //public int PatientId { get; set; }
        //public string LastName { get; set; }
        //public string FirstName { get; set; }
        //public string MiddleName { get; set; }
        //public string CellPhone { get; set; }
        //public string ConvertedBirthDate { get; set; }
        //public DateTime? BirthDateOfPatienttoDB { get; set; }
        //public string Adress { get; set; }
        //public string JobPlace { get; set; }
        //public string Email { get; set; }
        //public string Note { get; set; }
        //////////////////////////////////////////////

        //Fields for Visit
        public DateTime VisitDate { get; set; }
        public DateTime NewVisitDate { get; set; }
        public string ConvertedFullVisitDate { get; set; }
        public string ConvertedTimeOfVisit { get; set; }
        public bool IsPreorder { get; set; }
        public bool IsNeedSendEmail { get; set; }
        public bool IsHasVisited { get; set; }

        //Fields for Study
        private int st_id;
        public int St_Id
        {
            get { return st_id; }
            set { st_id = value; }
        }
        //------------------------
        private float st_cost;
        public float St_Cost
        {
            get { return st_cost; }
            set { st_cost = value; }
        }
        //------------------------
        private string st_type;
        public string St_Type
        {
            get { return st_type; }
            set { st_type = value; }
        }
        //-------------------------
        public string ConvertedStudyCost { get; set; }
        public int NewStudyId { get; set; }
        
        //Fields for Payment
        public bool IsCash { get; set; }
        public float FinalCost { get; set; }
        public string ConvertFinalCost { get; set; }
        public string NoteForDiscount { get; set; }

        //Fields for TheStudyProcess
        public string DoctorData { get; set; }
        
        //Field for Laborant
        public int? LaborantId { get; set; }
        public int LaborantId1 { get; set; }
        public int? LaborantId2 { get; set; }
        public int? NewLaborantId1 { get; set; }
        public int? NewLaborantId2 { get; set; }
        public string LaborantData { get; set; }       

        //Fields for BLL Linq in method BllGetAllPatientByDateInFactMagasine
        public int identificatorofgroups { get; set; }
        public IEnumerable<string> groupLaborantdata { get; set; }
        public IEnumerable<string> groupLastName { get; set; }
        public IEnumerable<string> groupFirstName { get; set; }
        public IEnumerable<string> groupMiddleNama { get; set; }
        public IEnumerable<string> groupCellPhone { get; set; }
        public IEnumerable<string> groupNote { get; set; }
        public IEnumerable<string> groupAdress { get; set; }
        public IEnumerable<string> groupJobPlace { get; set; }
        public IEnumerable<string> groupEmail { get; set; }
        public IEnumerable<string> groupConvertedBirthDate { get; set; }
        public IEnumerable<int> groupStudyId { get; set; }
        public IEnumerable<string> groupStudyType { get; set; }
        public IEnumerable<bool> groupIsNeedSendEmail { get; set; }
        public IEnumerable<bool> groupIsCash { get; set; }
        public IEnumerable<bool> groupIsHasVisited { get; set; }
        public IEnumerable<bool> groupIsPreorder { get; set; }        
        public IEnumerable<string> groupNoteForDiscount { get; set; }
        public IEnumerable<string> groupDoctorData { get; set; }
        public IEnumerable<int?> groupLaborantId { get; set; }
        public IEnumerable<string> groupConvertedTimeOfVisit { get; set; }
        public IEnumerable<string> groupConvertedFullVisitDate { get; set; }
        public IEnumerable<string> groupConvertFinalCost { get; set; }
        public IEnumerable<string> groupStudyCost { get; set; }
    }
}
