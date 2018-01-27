using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FICHospitalMRT.Models
{
    public class StudyProcess
    {
        public int StudyProcessId { get; }
        public int VisitId { get; }
        public int DoctorId { get; }
        public int LaborantId { get; }
        public int StudyId { get; }
    }
}