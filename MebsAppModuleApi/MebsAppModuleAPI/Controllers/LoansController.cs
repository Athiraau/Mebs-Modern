using Business.Contracts;
using Business.Helpers;
using DataAccess.Contracts;
using DataAccess.Dto.Request;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Policy;

namespace MebsAppModuleAPI.Controllers
{
    
    // [Authorize]
    [AllowAnonymous]
    [Route("api/LoansModuleAPI")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class LoansController : Controller
    {
        private readonly ILoggerService _logger;
        private readonly IServiceWrapper _service;
        private readonly HelperWrapper _helper;
        private IConfiguration _config;
        private int logflag = 0;
        
        public LoansController(IServiceWrapper service, HelperWrapper helper, ILoggerService logger, IConfiguration config)
        {
            _service = service;
            _helper = helper;
            _logger = logger;
            _config = config;
            logflag = Convert.ToInt32(_config["LogFlag"]);   //log creation can disable or enable by changing the value of LogFlag in configuration file. || 0  --> disable || 1  --> enable
        }


        [HttpGet("GetDataLoans/{flag}/{pagevalue}/{paravalue}", Name = "GetDataLoans")]
        public async Task<IActionResult> GetDataLoans([FromRoute] string flag, string pagevalue, string paravalue)
        {    
            var errorRes = _helper.CHelper.ValidateFlag(flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {

                if(logflag==1) _logger.LogError("Invalid/wrong request data  sent from client.");
                
            
                return BadRequest(errorRes.Result.errorMessage);
            }


            var punchdata = await _service.loanService.GetLoansService(flag, pagevalue, pagevalue);
           
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


        [HttpPost("PostDataLoans", Name = "PostDataLoans")]
        public async Task<IActionResult> PostDataLoans([FromBody] PostReqDto PostReq)
        {
            //FLAG VALIDATION
            var errorRes = _helper.CHelper.ValidateFlag(PostReq.p_flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {
              if (logflag == 1) _logger.LogError("Invalid/wrong request data  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }


            var punchdata = await _service.loanService.PostLoansService(PostReq);
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

        [HttpPost("LoansDocumentUpload", Name = "LoansDocumentUpload")]
        public async Task<IActionResult> LoansDocumentUpload([FromBody] DocUploadReqDto uploadDto)
        {
            
            var errorRes = _helper.CHelper.ValidateFlag(uploadDto.p_query);

            if (errorRes.Result.errorMessage.Count > 0)
            {
                if (logflag == 1) _logger.LogError("Invalid/wrong request data  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }

            var Responsedata = await _service.loanService.DocumentUpload(uploadDto);
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

        [HttpGet("PANValidation/{pan}/{empid}/{firmid}", Name = "PANValidation")]
        public async Task<IActionResult> PANValidation([FromRoute] string pan, string empid, int firmid)
        {
             HttpClient client = new HttpClient();
           
            PanReqDto _pan = new PanReqDto();
            _pan.pan = pan;
            _pan.firmid = firmid;
            _pan.empid = empid;

            var url = "https://unsecurepl.manappuram.com/aadhaarapi/api/pan";
            var response = await client.PostAsJsonAsync(url, _pan);

            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<dynamic>(resultString);
            return  Ok(result);
                
       
        }

      

    }
}
