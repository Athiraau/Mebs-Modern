using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.MenuModel;
using MVC_Project.Repository;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC_Project.Controllers
{
    public class OthersController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly DecryptionRepo _repo;
        private readonly GetDataRepo _Grepo;
        private readonly PostDataRepo _Prepo;

        private readonly string baseurl;
        private readonly string rootfolder;

        private readonly IConfiguration _configuration;
        public string MainHeadID = "7";

        public OthersController(ILogger<HomeController> logger, DecryptionRepo repo, GetDataRepo grepo, PostDataRepo Prepo, IConfiguration configuration)
        {
            _logger = logger;
            _repo = repo;
            _Grepo = grepo;
            _Prepo = Prepo;
            _configuration = configuration;
            baseurl = _configuration.GetValue<string>("BasValues:BaseUrl");
            rootfolder = _configuration.GetValue<string>("BasValues:root");
        }
        public object Session { get; private set; }
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

            HttpContext.Session.SetString("headname", "OTHERS");

            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;
            ViewData["HeadName"] = datas;
            return View(model);
        }

        public IActionResult DEVICEUPDATION(string datas)
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

        public IActionResult GRIEVANCEREGISTER(string datas)
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

        public dynamic DocumentUpload(string datas)     //upload API Response
        {
            string[] DataArray = datas.Split('~');
            string ApiPath = "MebsAppModuleApi/api/OTHERSModuleAPI/OTHERSDocumentUpload";

            string query = DataArray[0];
            string code = DataArray[1];
            var response = _Prepo.UploadDocument(query, code, baseurl, ApiPath);
            return response.ToString();
        }


        public dynamic EmailPatternValidation(string datas)
        {
            string[] emailsplit = null;
            string RES = "";
            if (datas.Contains(".."))
            {
                RES = "0";
                return false;
            }

            emailsplit = datas.Split('@');

            if (emailsplit[0].Length < 1)
            {
                RES = "0";
                return false;
            }


            // string indata = datas;

            string email = datas; //email id
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                RES = "1";
            else
                RES = "0";     //invalid mail

            return RES;

        }

        public string getAPIData(string datas)
        {
            string[] DataArray = datas.Split('^');
            string ApiPath = "MebsAppModuleApi/api/OTHERSModuleAPI/GetDataOTHERS/";

            string flag = DataArray[0];
            string indata = DataArray[1];
            var resData = _Grepo.GetInternalPageData(indata, flag, baseurl, ApiPath);
            return resData;
        }

      
        public async Task<dynamic> postAPIData(string datas)

        {

            string ApiPath = "MebsAppModuleApi/api/OTHERSModuleAPI/PostDataOTHERS";
            string[] DataArray = datas.Split('^');

            string flag = DataArray[0];
            string indata = DataArray[1];
            var response = await _Prepo.PostInternalPageData(indata, flag, baseurl, ApiPath);

            return response.ToString();
        }


    }
}