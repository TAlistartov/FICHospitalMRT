using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IPatient
    {
        int Pat_Id { get; set; }
        string Pat_LastName { get; set; }
        string Pat_FirstName { get; set; }
        string Pat_MiddleName { get; set; }
        string Pat_CellPhone { get; set; }
        string Pat_ConvertedBirthDate { get; set; }
        DateTime? Pat_BirthDateToDB { get; set; }
        string Pat_Adress { get; set; }
        string Pat_JobPlace { get; set; }
        string Pat_Email { get; set; }
        string Pat_Note { get; set; }

    }
}
