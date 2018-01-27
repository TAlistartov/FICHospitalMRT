using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalPatients: IPatient
    {
        private int _Id;
        public int Pat_Id
        {
            get {return _Id;}
            set { _Id = value; }
        }

        private string _LastName;
        public string Pat_LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private string _FirstName;
        public string Pat_FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _MiddleName;
        public string Pat_MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        private string _CellPhone;
        public string Pat_CellPhone
        {
            get { return _CellPhone; }
            set { _CellPhone = value; }
        }

        private string _ConvertedBirthDate;
        public string Pat_ConvertedBirthDate
        {
            get { return _ConvertedBirthDate; }
            set { _ConvertedBirthDate = value; }
        }

        private DateTime? _BirthDateToDB;
        public  DateTime? Pat_BirthDateToDB
        {
            get { return _BirthDateToDB;}
            set { _BirthDateToDB = value; }
        }

        private string _Adress;
        public string Pat_Adress
        {
            get { return _Adress; }
            set { _Adress = value; }
        }

        private string _JobPlace;
        public string Pat_JobPlace
        {
            get { return _JobPlace; }
            set { _JobPlace = value; }
        }

        private string _Email;
        public string Pat_Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Note;
        public string Pat_Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        /////////////////////////////////////////////////////////////////////////////
        //public int IdOfPatient { get; set; }
        //public string LastNameOfPatient { get; set; }
        //public string FirstNameOfPatient { get; set; }
        //public string MiddleNameOfPatient { get; set; }
        //public string BirthDateOfPatient { get; set; }

        //public DateTime? BirthDateOfPatienttoDB { get; set; }
        //public string CellPhoneOfPatient { get; set; }
        //public string AdressOfPatient { get; set; }

        //public string JobPlaceOfPatient { get; set; }

        //public string EmailOfPatient { get; set; }

        //public string NoteOfPatient { get; set; }

    }
}
