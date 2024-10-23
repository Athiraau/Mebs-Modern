using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Request
{
    public class DocUploadReqDto
    {
      
        public string p_query { get; set; } = string.Empty;
        // [Required]
        public string DocData { get; set; }
    }
}
