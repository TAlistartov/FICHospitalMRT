using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace FICHospitalMRT.Models
{
    public class Study
    {
        
        public int StudyId { get; set; }

        //[Required(ErrorMessage ="Поле не должно быть пустым")]
        //[StringLength(50,MinimumLength =1,ErrorMessage ="Название обследования должно быть больше" 
        //                                                    +" 1 и меньше 50 символов")]
        public string Type { get; set; }

        //[Required(ErrorMessage = "Поле не должно быть пустым")]
        //[RegularExpression(@"[0-9]+,\d{1,2}|[0-9]+\.\d{1,2}|[0-9]+", ErrorMessage ="Поле может содержать только цифры")]
        //[StringLength(7, MinimumLength = 3, ErrorMessage = "Стоимость обследования не может быть = 0"
        //                                                    + " и больше чем 999.99")]
        public string Cost { get; set; }


        public bool? AddNewStudyResult { get; set; }
        public string AddNewStudyResultMessage { get; set; }
        public List <SelectListItem> AllStudiesDb { get; set; }
        public string SelectedValue { get; set; }
        public bool Used { get; set; }
        public List<Study> AllItemsFromDB { get; set; }


    }
}