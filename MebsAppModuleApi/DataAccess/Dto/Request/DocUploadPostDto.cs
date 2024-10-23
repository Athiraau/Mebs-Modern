using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Request
{
    public class DocUploadPostDto
    {
       
        public string p_query { get; set; } = string.Empty;
        // [Required]
        public byte[] DocData { get; set; }
    }
}
