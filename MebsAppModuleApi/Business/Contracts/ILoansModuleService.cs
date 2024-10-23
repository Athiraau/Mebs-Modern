using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface ILoansModuleService
    {
        public Task<dynamic> GetLoansService(string flag, string pagevalue, string paravalue);

        public Task<dynamic> PostLoansService(PostReqDto _reqdto);
        public Task<dynamic> DocumentUpload(DocUploadReqDto _uploaddto);
    }

}
