using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FICHospitalMRT.Models
{
    public class Visit
    {
        public int VisitId { get; }
        public DateTime DateOfVisit { get; set; }
        public bool IsPreorder { get; set; }
        public int PatientId { get; }
        public bool IsNeedSendEmail { get; set; }
        public bool IsHasVisited { get; set; }
    }
}