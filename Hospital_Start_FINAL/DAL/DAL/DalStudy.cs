using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalStudy : IStudy
    {
        private int _Id;
        public int St_Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        //------------------------
        private float _Cost;
        public float St_Cost
        {
            get { return _Cost; }
            set { _Cost = value; }
        }
        //------------------------
        private string _Type;
        public string St_Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        //-------------------------
        public bool St_Used { get; set; }
        public int St_SelectedStudy { get; set; }
        
    }
}
