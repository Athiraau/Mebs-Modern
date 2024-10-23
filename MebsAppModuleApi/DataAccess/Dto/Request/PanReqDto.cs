using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Request
{
    public class PanReqDto
    {
        public string pan { get; set; }=string.Empty;

        public int firmid { get; set; } = 0;

        public string empid { get; set; } = string.Empty;
    }
      


}
