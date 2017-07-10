using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Product
{
    public class MeasureDTO:IDTO
    {
        public int id { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public string description { get; set; }
    }
}
