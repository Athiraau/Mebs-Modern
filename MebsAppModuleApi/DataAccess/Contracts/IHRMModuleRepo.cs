using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface  IHRMModuleRepo
    {
        public Task<dynamic> GetHRMData(string flag, string pagevalue, string paravalue);
        public Task<dynamic> PostHRMData(PostReqDto PostReq);
        public Task<dynamic> UploadDocument(DocUploadPostDto docUploadDto);


    }
}
