using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.MenuModel;
using MVC_Project.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Drawing;
using MVC_Project.Repository;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Protocol;
using MVC_Project.DTO;
using System.Text.Json;
using System.Net.Http;
using System.Net;

namespace MVC_Project.Controllers
{
  
    public class LoansController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DecryptionRepo _repo;
        private readonly GetDataRepo _Grepo;
        private readonly PostDataRepo _Prepo;
        private readonly string baseurl;
        private readonly string rootfolder;
        private readonly int OTPacc_id;

        private readonly IConfiguration _configuration;


        public object Session { get; private set; }
        public string MainHeadID = "1";
        public string ResponseData = "";

        public LoansController(ILogger<HomeController> logger, DecryptionRepo repo, GetDataRepo grepo, PostDataRepo Prepo, IConfiguration configuration)
        {
            _logger = logger;
            _repo = repo;
            _Grepo = grepo;
            _Prepo = Prepo;
            _configuration = configuration;
            baseurl = _configuration.GetValue<string>("BasValues:BaseUrl");    //   uatapp
            rootfolder = _configuration.GetValue<string>("BasValues:root");    //   mebsapp
            OTPacc_id = _configuration.GetValue<int>("BasValues:otp_acc_id");   // OTP acc id

        }

        public IActionResult LoginPage()
        {
            ViewData["baseurl"] = baseurl;

            return View();
        }

        public IActionResult GoldOverdraft(string datas)
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
            String flag = "";

            return View(model);
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

            HttpContext.Session.SetString("headname", "LOANS");

            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;
            ViewData["HeadName"] = datas;
            return View(model);
        }

        public string RemoveSpecialCharacters(string str)
        {
            //-._~+/
            //  return Regex.Replace(str, "[^a-zA-Z0-9_~+/-]+", "", RegexOptions.Compiled);

            return Regex.Replace(str, "[^a-zA-Z0-9_~+/-{}]+", "", RegexOptions.Compiled);

        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // --- Dashboard
        public IActionResult Dashboard(string session)
        {
            string decryptedData = " ";

            if (string.IsNullOrEmpty(session))
            {
                session = HttpContext.Session.GetString("SessionVal");
            }


            String[] resession = session.ToString().Split("&");
            session = resession[0];
            string result1 = _repo.FromHexToBase64(session);
            var strHeader = _repo.DecryptStringAES(result1);


            String[] arrStr;
            String[] arrStrCode;

            arrStr = strHeader.ToString().Split("|");
            arrStrCode = arrStr[2].ToString().Split("!");

            ViewData["baseurl"] = baseurl;
            HttpContext.Session.SetString("EmpName", arrStr[3]);
            HttpContext.Session.SetString("ecode", arrStr[2]);
            HttpContext.Session.SetString("BrName", arrStr[1]);
            HttpContext.Session.SetString("UserId", arrStrCode[0]);
            HttpContext.Session.SetString("SessionVal", session);
            HttpContext.Session.SetString("BrID", arrStr[0]);
            // HttpContext.Session.SetString("headname", "OTHERS");



            return View();
        }



        public IActionResult SMAReportHome(string datas)
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


        public IActionResult SMAREGION(string zoneid1)
        {
            string[] da = zoneid1.Split('~');

            ViewData["zoneid"] = da[1].ToString();
            ViewData["SelectedDate"] = da[0].ToString();

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;


            //string baseurl = _configuration.GetConnectionString("Baseurl").ToString();




            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");


            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;


            return View(model);
        }

        public IActionResult SMAAREA(string indata)
        {

            string[] da = indata.Split('~');

            ViewData["Regionid"] = da[1];
            ViewData["SelectedDate"] = da[0].ToString();
            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;

            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");


            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;


            return View(model);
        }

        public IActionResult SMABRANCH(string data)
        {
            string[] da = data.Split('~');
            ViewData["Areaid"] = da[1];
            ViewData["SelectedDate"] = da[0].ToString();


            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;

            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");


            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;


            return View(model);
        }

        public IActionResult SMAPLEDGE(string data)
        {
            string[] da = data.Split('~');
            ViewData["Branchid"] = da[1];
            ViewData["SelectedDate"] = da[0].ToString();

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;

            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");


            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);

            ViewData["EmpCode"] = UserId;

            return View(model);
        }

        public IActionResult SMAERRORBALANCE(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            // string decryptedData = " ";
            //  decryptedData = Convert.ToString(_repo.DecryptStringAES(datas));
            //  decryptedData = "18906";

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

        public IActionResult SMAFINREPORT(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            // string decryptedData = " ";
            //  decryptedData = Convert.ToString(_repo.DecryptStringAES(datas));
            //  decryptedData = "18906";

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

        public IActionResult SMAPRODUCT(string datas)
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

        public IActionResult SMA2REPORT(string datas)
        {

            string flag = datas;
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

        public IActionResult BAREPORT(string datas)
        {

            string flag = datas;
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

        public IActionResult BACREATION(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            // string decryptedData = " ";
            //  decryptedData = Convert.ToString(_repo.DecryptStringAES(datas));
            //  decryptedData = "18906";

            ViewData["user"] = HttpContext.Session.GetString("ecode");
            var empcode = HttpContext.Session.GetString("ecode");
            var empname = HttpContext.Session.GetString("EmpName");
            var branchname = HttpContext.Session.GetString("BrName");
            var UserId = HttpContext.Session.GetString("UserId");
            var brID = HttpContext.Session.GetString("BrID");

            ViewData["BrID"] = brID;
            MenuListModel model = new MenuListModel();
            model = (MenuListModel)_Grepo.GetMainMenuData(UserId, baseurl, MainHeadID);


            // ViewData["PANStatus"] = Global.Verify_msg;
            ViewData["EmpCode"] = UserId;

            //-------------------------------


            return View(model);

        }

        public IActionResult PAIDBACREATION(string datas)
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
        public IActionResult PLEDGEWISEREPORT(string datas)
        {

            string flag = datas;
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
        public IActionResult AUCTIONEERPUNCHCNTRL(string datas)
        {

            string flag = datas;
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
        public IActionResult DDREVERSAL(string datas)
        {

            string flag = datas;
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
        public IActionResult DDstatuschecking(string datas)
        {

            ViewData["baseurl"] = baseurl;
            ViewData["root"] = rootfolder;
            ViewData["HeadName"] = datas;

            // string decryptedData = " ";
            //  decryptedData = Convert.ToString(_repo.DecryptStringAES(datas));
            //  decryptedData = "18906";

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
        public IActionResult newp(string datas)
        {

            string flag = datas;
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
        public IActionResult Surplus_Reverse_PayidWise(string datas)
        {

            string flag = datas;
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
        public IActionResult surplusbranch(string datas)
        {

            string flag = datas;
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

        public IActionResult BIDDERR(string datas)
        {

            string flag = datas;
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
        public IActionResult surplus(string datas)
        {

            string flag = datas;
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


        public IActionResult ddpending_report(string datas)
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

        public IActionResult AUCREPORT(string datas)
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

        public IActionResult AUCTIONEERRENEWALAPPROVAL(string datas)
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

        public IActionResult AUCTIONEERRENEWALREQUEST(string datas)
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

        public IActionResult AUCTIONNEFTRPT(string datas)
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

        public IActionResult DDRETURNRPT(string datas)
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

        public IActionResult DN10REPORT(string datas)
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

        public IActionResult PLEDGEEXCEPTIONREQ(string datas)
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

        public IActionResult w(string datas)
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





        public IActionResult Deathcustomerrefundreport(string datas)
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

        public IActionResult paidreport(string datas)
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



        public IActionResult SMS(string datas)
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

        public IActionResult UNPAIDREPORT(string datas)
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

        public IActionResult UNSUCCESSFULBRANCHREPORTPHASE1(string datas)
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

        public IActionResult UNSUCCESSFULBRANCHREPORTPHASE2(string datas)
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

        public IActionResult ddreturnupdation(string datas)
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
        public IActionResult realization(string datas)
        {

            string flag = datas;
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

        // --------------------------------------------------------------------------------------------------------------------------------------//
        public string getAPIData(string datas)
        {
            string[] DataArray = datas.Split('^');
            string ApiPath = "MebsAppModuleApi/api/LoansModuleAPI/GetDataLoans/";


            string flag = DataArray[0];
            string indata = DataArray[1];
            var resData = _Grepo.GetInternalPageData(indata, flag, baseurl, ApiPath);
            return resData;
        }


        public dynamic DocumentUpload(string datas)     //upload API Response
        {
            string[] DataArray = datas.Split('^');
            string ApiPath = "MebsAppModuleApi/api/LoansModuleAPI/LoansDocumentUpload";

            string query = DataArray[0];
            string code = DataArray[1];
            var response = _Prepo.UploadDocument(query, code, baseurl, ApiPath);
            return response.ToString();
        }


        [HttpPost]
        public dynamic postAPIData(string datas)
        {

            string ApiPath = "MebsAppModuleApi/api/LoansModuleAPI/PostDataLoans";
            string[] DataArray = datas.Split('^');

            string flag = DataArray[0];
            string indata = DataArray[1];
            var response = _Prepo.PostInternalPageData(indata, flag, baseurl, ApiPath);
            return response.ToString();
        }



        public dynamic OTPHelper(string datas)
        {
            string[] DataArray = datas.Split('^');

            string HelperFlag = DataArray[0];

            var data = "";
            if (HelperFlag == "sendotp")
            {
                string ApiPath = "BussinessAssoApi/api/BA/GET_SendBA_OTP/";
                string flag = DataArray[1];
                string mobno = DataArray[2];   //mobile no
                int AccId = OTPacc_id;
                string SMSContent = "Dear Customer, One Time Password(OTP) for authenticating your mobilenumber is @@@@@@@.DO Not SHARE WITH ANY ONE. Manappuram";
                string indata = mobno + "/" + AccId + "/" + SMSContent;
                data = _Grepo.OTPHelperData(flag, indata, baseurl, ApiPath);
            }

            if (HelperFlag == "verifyotp")
            {
                string ApiPath = "BussinessAssoApi/api/BA/GET_VerifyBA_OTP/";
                string flag = DataArray[1];
                string indata = DataArray[2];   //otp
                data = _Grepo.OTPHelperData(flag, indata, baseurl, ApiPath);
            }

            return (data);

        }

        public dynamic PANValidation(string datas)     //upload API Response
        {
            string ApiPath = "MebsAppModuleApi/api/LOANSModuleAPI/PANValidation/";

            string[] DataArray = datas.Split('^');

            string pan_no = DataArray[0];
            string emp = DataArray[1];
            string firmid = DataArray[2];

            string indata = datas;
            var resData = _Grepo.PANHelper(pan_no, emp, firmid, baseurl, ApiPath);
            return resData;
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

            if (emailsplit[0].Length < 3)
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
    }

}
