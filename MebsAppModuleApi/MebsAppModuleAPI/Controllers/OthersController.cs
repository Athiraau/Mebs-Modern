using Business.Contracts;
using Business.Helpers;
using DataAccess.Contracts;
using DataAccess.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MebsAppModuleAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/OthersModuleAPI")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class OthersController : Controller
    {
            private readonly ILoggerService _logger;
            private readonly IServiceWrapper _service;
            private readonly HelperWrapper _helper;
            private IConfiguration _config;
            private int logflag = 0;

            public OthersController(IServiceWrapper service, HelperWrapper helper, ILoggerService logger, IConfiguration config)
            {
                _service = service;
                _helper = helper;
                _logger = logger;
                _config = config;
                logflag = Convert.ToInt32(_config["LogFlag"]);   //log creation can disable or enable by changing the value of LogFlag in configuration file. || 0  --> disable || 1  --> enable

            }


            [HttpGet("GetDataOthers/{flag}/{pagevalue}/{paravalue}", Name = "GetDataOthers")]
            public async Task<IActionResult> GetDataOthers([FromRoute] string flag, string pagevalue, string paravalue)
            {
                var errorRes = _helper.CHelper.ValidateFlag(flag);
                if (errorRes.Result.errorMessage.Count > 0)
                {

                    if (logflag == 1) _logger.LogError("Invalid/wrong request data  sent from client.");


                    return BadRequest(errorRes.Result.errorMessage);
                }


                var punchdata = await _service.OthersService.GetOthersService(flag, pagevalue, paravalue);

                if (punchdata == null)
                {
                    if (logflag == 1) _logger.LogError($"Details of filter data could not be returned in db.");

                    return NotFound();

                }
                else
                {
                    if (logflag == 1) _logger.LogInfo($"Returned details of data required to load filter for flag: {flag}");

                    return Ok(JsonConvert.SerializeObject(punchdata));

                }

            }


            [HttpPost("PostDataOthers", Name = "PostDataOthers")]
            public async Task<IActionResult> PostDataOthers([FromBody] PostReqDto PostReq)
            {
                //FLAG VALIDATION
                var errorRes = _helper.CHelper.ValidateFlag(PostReq.p_flag);
                if (errorRes.Result.errorMessage.Count > 0)
                {
                    if (logflag == 1) _logger.LogError("Invalid/wrong request data  sent from client.");
                    return BadRequest(errorRes.Result.errorMessage);
                }


                var punchdata = await _service.OthersService.PostOthersService(PostReq);
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

            [HttpPost("OthersDocumentUpload", Name = "OthersDocumentUpload")]
            public async Task<IActionResult> OthersDocumentUpload([FromBody] DocUploadReqDto uploadDto)
            {

                var errorRes = _helper.CHelper.ValidateFlag(uploadDto.p_query);

                if (errorRes.Result.errorMessage.Count > 0)
                {
                    if (logflag == 1) _logger.LogError("Invalid/wrong request data  sent from client.");
                    return BadRequest(errorRes.Result.errorMessage);
                }

                var Responsedata = await _service.OthersService.DocumentUpload(uploadDto);
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
