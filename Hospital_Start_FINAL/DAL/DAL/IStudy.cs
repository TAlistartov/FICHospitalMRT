using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IStudy
    {
        int St_Id { get; set; }
        float St_Cost { get; set; }
        string St_Type { get; set; }

    }
}
