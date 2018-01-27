using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalChargesByDate
    {
        public int ChargeId { get; set; }
        public string ChargeNote { get; set; }
        public float CostCharge { get; set; }
        public DateTime? ChargesByDate { get; set; }
        public string ConvertChargesByDate { get; set; }
        public string ConvertedCostCharge { get; set; }
    }
}
