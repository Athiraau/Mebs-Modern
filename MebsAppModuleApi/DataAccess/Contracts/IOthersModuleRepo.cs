using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IOthersModuleRepo
    {
        
        public Task<dynamic> GetOthersData(string flag, string pagevalue, string paravalue);
        public Task<dynamic> PostOthersData(PostReqDto PostReq);
        public Task<dynamic> UploadDocument(DocUploadPostDto docUploadDto);


    }
}
