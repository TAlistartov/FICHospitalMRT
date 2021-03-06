﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;



namespace FICHospitalMRT.Models
{
    public class Laborants
    {
        public int LaborantId { get; set; }


        [Required (ErrorMessage = "Поле не должно быть пустым.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        public string LastName { get; set; }
        
        [Required (ErrorMessage = "Поле не должно быть пустым.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        public string FirstName { get; set; }

        public bool? AddNewLaborantResult { get; set; }
        public string SelectedValue { get; set; }
        public bool Used { get; set; }
        public List <SelectListItem> AllLaborantsDb { get; set; }
        public List <Laborants> AllItemsFromDB { get; set; }
    }
}