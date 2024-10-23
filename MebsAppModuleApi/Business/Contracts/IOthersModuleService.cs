using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IOthersModuleService
    {
        public Task<dynamic> GetOthersService(string flag, string pagevalue, string paravalue);

        public Task<dynamic> PostOthersService(PostReqDto _reqdto);
        public Task<dynamic> DocumentUpload(DocUploadReqDto _uploaddto);
    }

}
