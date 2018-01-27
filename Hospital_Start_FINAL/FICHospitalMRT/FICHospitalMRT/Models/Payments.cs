using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FICHospitalMRT.Models
{
    public class Payments
    {
        public int PaymentId { get; }
        public bool IsCash { get; set; }
        public int VisitId { get; }
        public int StudyProcessId { get; }
    }
}