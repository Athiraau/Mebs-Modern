using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface ILoansModuleRepo
    {
        public Task<dynamic> GetLoansData(string flag, string pagevalue,string paravalue);
        public Task<dynamic> PostLoansData(PostReqDto PostReq);
        public Task<dynamic> UploadDocument(DocUploadPostDto docUploadDto);


    }
}
