using Business.Contracts;
using Business.Helpers;
using DataAccess.Contracts;
using DataAccess.Dto.Request;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MebsAppModuleAPI.Controllers
{
    
    // [Authorize]
    [AllowAnonymous]
    [Route("api/HRMModuleAPI")]
    [EnableCors("MyPolicy")]
    [ApiController]
    
    public class HRMController : Controller
    {
       // private readonly IAuditPunchService _service;
        private readonly ILoggerService _logger;
        private readonly IServiceWrapper _service;
        private readonly HelperWrapper _helper;
        private IConfiguration _config;
        private int logflag = 0;
        
        public HRMController(IServiceWrapper service, HelperWrapper helper, ILoggerService logger, IConfiguration config)
        {
            _service = service;
            _helper = helper;
            _logger = logger;
            _config = config;
            logflag = Convert.ToInt32(_config["LogFlag"]);   //log creation can disable or enable by changing the value of LogFlag in configuration file. || 0  --> disable || 1  --> enable
            
        }


        [HttpGet("GetDataHRM/{flag}/{pagevalue}/{paravalue}", Name = "GetDataHRM")]
        public async Task<IActionResult> GetDataHRM([FromRoute] string flag, string pagevalue, string paravalue)
        {    
            var errorRes = _helper.CHelper.ValidateFlag(flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {

                if(logflag==1) _logger.LogError("Invalid/wrong request data  sent from client.");
                
            
                return BadRequest(errorRes.Result.errorMessage);
            }


            var punchdata = await _service.HRMService.GetHRMService(flag, pagevalue,paravalue);
           
            if (punchdata == null)
            {
                if (logflag == 1) _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
            else
            {
               if (logflag == 1)    _logger.LogInfo($"Returned details of data required to load filter for flag: {flag}");

                    return Ok(JsonConvert.SerializeObject(punchdata));

            }

        }


        [HttpPost("PostDataHRM", Name = "PostDataHRM")]
        public async Task<IActionResult> PostDataHRM([FromBody] PostReqDto PostReq)
        {
            //FLAG VALIDATION
            var errorRes = _helper.CHelper.ValidateFlag(PostReq.p_flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {
              if (logflag == 1) _logger.LogError("Invalid/wrong request data  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }


            var punchdata = await _service.HRMService.PostHRMService(PostReq);
            if (punchdata == null)
            {
                if (logflag == 1) _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
            else
            {
                if (logflag == 1) _logger.LogInfo($"Returned response data after saving early going req: {PostReq.p_flag}");
                return Ok(JsonConvert.SerializeObject(punchdata));

            }

        }

        [HttpPost("HRMDocumentUpload", Name = "HRMDocumentUpload")]
        public async Task<IActionResult> HRMDocumentUpload([FromBody] DocUploadReqDto uploadDto)
        {
            
            var errorRes = _helper.CHelper.ValidateFlag(uploadDto.p_query);

            if (errorRes.Result.errorMessage.Count > 0)
            {
                if (logflag == 1) _logger.LogError("Invalid/wrong request data  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }

            var Responsedata = await _service.HRMService.DocumentUpload(uploadDto);
            if (Responsedata == null)
            {
                if (logflag == 1) _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
            else
            {
                if (logflag == 1) _logger.LogInfo($"Returned response data");
                return Ok(JsonConvert.SerializeObject(Responsedata));

            }

        }

    }
}
