using Business.Contracts;
using Business.Helpers;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class HRMModuleService : IHRMModuleService
    {
        private readonly HRMModuleRepo _HRM;
        private readonly DtoWrapper _dto;
        private IConfiguration _config;
        private readonly HelperWrapper _helper;
        public HRMModuleService(HRMModuleRepo HRM, DtoWrapper dto,
            IConfiguration config, HelperWrapper helper)
        {
            _HRM = HRM;
            _dto = dto;
            _config = config;
            _helper = helper;

        }


        public async Task<dynamic> GetHRMService(string flag, string pagevalue, string paravalue)
        {
            var res = await _HRM.GetHRMData(flag, pagevalue, paravalue);
            return res;
        }

        public async Task<dynamic> PostHRMService(PostReqDto _reqdto)
        {
            var res = await _HRM.PostHRMData(_reqdto);
                return res;
        }

        public async Task<dynamic> DocumentUpload(DocUploadReqDto _uploaddto)
        {

            DocUploadPostDto docu_up = new DocUploadPostDto();
            byte[] imageBytes = Convert.FromBase64String(_uploaddto.DocData);

            // int compressSize = Convert.ToInt32(_config["Image:CompressionSize"]);
             //imageBytes = _helper.CHelper.ReduceImageSize(imageBytes, compressSize);


            docu_up.DocData = imageBytes;
            docu_up.p_query = _uploaddto.p_query;


           var res = await _HRM.UploadDocument(docu_up);

        return res;
        }
    }
}
