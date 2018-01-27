using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FICHospitalMRT.Models
{
    public class StudyChangeData
    {
        public int StudyId { get; set; }

        [Required(ErrorMessage ="Поле не должно быть пустым")]
        [StringLength(50,MinimumLength =1,ErrorMessage ="Название обследования должно быть больше"+" 1 и меньше 50 символов")]
        public string ChangeDataType { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [RegularExpression(@"(\d{1,3},\d{1,2})|(\d{1,3}\.\d{1,2})|(^(?:\d+|\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$)", ErrorMessage ="Поле может содержать только цифры и '.' или ','.")]
        //[StringLength(7, MinimumLength = 3, ErrorMessage = "Стоимость обследования не может быть = 0"
                                                           // + " и больше чем 999.99")]
        public string ChangeDataCost { get; set; }

        public bool Used { get; set; }


        public bool? AddNewStudyResult { get; set; }
        public string AddNewStudyResultMessage { get; set; }
        public List<SelectListItem> AllStudiesDb { get; set; }
        public string SelectedValue { get; set; }
    }
}