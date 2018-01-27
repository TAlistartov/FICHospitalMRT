using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FICHospitalMRT.Models
{
    public class MChargesByDate
    {
        public int ChargeId { get; set; }
        public string ChargeNote { get; set; }
        public float CostCharge { get; set; }
        public string ConvertedCostCharge { get; set; }
        public DateTime? ChargesByDate { get; set; }
        public string ConvertChargesByDate { get; set; }
        public int NumberInOrder { get; set; }
        public bool ResultOfAction { get; set; }
        public float CashSumByDate { get; set; }
        public float TerminalSumByDate { get; set; }
        public float DifferentCharges { get; set; }
    }
}