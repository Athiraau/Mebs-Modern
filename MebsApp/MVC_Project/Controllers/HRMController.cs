using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.MenuModel;
using MVC_Project.Repository;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace MVC_Project.Controllers
{

    public class HRMController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DecryptionRepo _repo;
        private readonly GetDataRepo _Grepo;
        private readonly PostDataRepo _Prepo;
        private readonly string baseurl;
        private readonly string rootfolder;

        private readonly IConfiguration _configuration;


        public object Session { get; private set; }
        public string MainHeadID = "5";

        public HRMController(ILogger<HomeController> logger, DecryptionRepo repo, GetDataRepo grepo, PostDataRepo Prepo, IConfiguration configuration)
        {
            _logger = logger;
            _repo = repo;
            _Grepo = grepo;
            _Prepo=Prepo;

            _configuration = configuration;
            baseurl = _configuration.GetValue<string>("BasValues:BaseUrl");    //   uatapp/app
            rootfolder = _configuration.GetValue<string>("BasValues:root");    //   mebsapp
            
        }


        public IActionResult Index(string datas)
        {
            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;

            //string decryptedData = " ";
            //decryptedData = Convert.ToString(_repo.DecryptStringAES(datas));


            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");


            var FirmID = HttpContext.Session.GetString("firm_id");


            HttpContext.Session.SetString("headname", "HRM");

            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;
            ViewData["HeadName"] = datas;
            ViewData["FirmID"] = FirmID;



            return View(model);
        }

        public IActionResult ONLINECOURSEAPPLICATION(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;



            return View(model);

        }

        public IActionResult FIELDTOURAPPLICATION(string datas)
        {
            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;
            return View(model);

        }


        public IActionResult COURSEVERIFICATION(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;



            return View(model);

        }


        public IActionResult LEAVEREPORT(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;
            ViewData["EmpName"] = empname;



            return View(model);

        }


        public IActionResult INDPUNCHINGRPT(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;
            ViewData["EmpName"] = empname;



            return View(model);

        }


        public IActionResult ONLINECOURSEAPPROVAL(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;
            ViewData["EmpName"] = empname;



            return View(model);

        }


        public IActionResult MYWORKALERTTODAY(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;



            return View(model);

        }

        public IActionResult TODAYSHIFTCHANGE(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;



            return View(model);

        }

        public IActionResult SHIFTCHANGE_PRESS(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;
            ViewData["EmpName"] = empname;

            //ViewData["EmpName"] = empname;
            ////337545
            //ViewData["EmpCode"] = 337545;

            return View(model);

        }

        public IActionResult SHIFCHANGEREPORT(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;
            ViewData["EmpName"] = empname;
            ////337545
            //ViewData["EmpCode"] = 337545;


            return View(model);

        }

        public IActionResult TRAN_PROM_INCRE_RPT(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;
            ViewData["EmpName"] = empname;




            return View(model);

        }

        public string getAPIData(string datas)     //Get API Response
        {
            string ApiPath = "MebsAppModuleApi/api/HRMModuleAPI/GetDataHRM/";

            string[] DataArray = datas.Split('^');

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;


            string flag = DataArray[0];
            string indata = DataArray[1];
            var resData = _Grepo.GetInternalPageData(indata, flag, baseurl, ApiPath);
            // return resData.ToJson();
            resData = resData.Replace(@"{""RESP"":", @"");
            return resData;
        }



        //[HttpPost]
        public dynamic DocumentUpload(string datas)     //upload API Response
        {
            string[] DataArray = datas.Split('~');
            string ApiPath = "MebsAppModuleApi/api/HRMModuleAPI/HRMDocumentUpload";

            string query = DataArray[0];
            string code = DataArray[1];
            var response = _Prepo.UploadDocument(query, code, baseurl, ApiPath);
            return response.ToString();
        }


        // [HttpPost]
        // public dynamic postAPIData(string datas)
        public async Task<dynamic> postAPIData(string datas)

        {

            string ApiPath = "MebsAppModuleApi/api/HRMModuleAPI/PostDataHRM";
            string[] DataArray = datas.Split('^');

            string flag = DataArray[0];
            string indata = DataArray[1];
            var response = await _Prepo.PostInternalPageData(indata, flag, baseurl, ApiPath);
            //return response.ToString();

            return Ok(response);
        }



    }
}
