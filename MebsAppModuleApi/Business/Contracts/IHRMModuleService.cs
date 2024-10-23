using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IHRMModuleService
    {
        public Task<dynamic> GetHRMService(string flag, string pagevalue, string paravalue);

        public Task<dynamic> PostHRMService(PostReqDto _reqdto);
        public Task<dynamic> DocumentUpload(DocUploadReqDto _uploaddto);
    }

}
