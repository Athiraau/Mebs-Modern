using Microsoft.AspNetCore.Mvc;
using MVC_Project.DTO;
using MVC_Project.Models.MenuModel;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC_Project.Repository
{
    public class GetDataRepo
    {
        private readonly empDto _dto;

        public GetDataRepo(empDto dto)
        {
            _dto = dto;
        }


        public dynamic GetMainMenuData(string EMPCODE, string baseurl, string MainHeadID)
        {

            MenuListModel menu_ViewModel = new MenuListModel();
            List<MenuNameModel> _mN = new List<MenuNameModel>();
            List<SubMenuNameModel> _sN = new List<SubMenuNameModel>();
            List<ChildMenuNameModel> _cN = new List<ChildMenuNameModel>();
            menu_ViewModel.EmpCode = EMPCODE;


            using (var client = new HttpClient())
            {

                string link = baseurl + "MenuApi/api/MenuApi/GetMenuData/GETMAINMENU_MVC1/";
                client.BaseAddress = new Uri(link + EMPCODE + "~" + MainHeadID + "/1");

                HttpResponseMessage result = client.GetAsync(client.BaseAddress).Result;

                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    data = data.Replace(@"{""MenuResDto"":", @"");
                    data = data.Replace(@"{""menuResDto"":", @"");
                    data = data.Replace(@"}]}", @"}]");
                    data = data.Replace("\"\"", "\"");

                    _mN = JsonConvert.DeserializeObject<List<MenuNameModel>>(data);
                }
                menu_ViewModel.M_NAME = _mN;
                //-----------------------------------------------------------------------------------------------------------------


                using (var sclient = new HttpClient())
                {

                    string slink = baseurl + "MenuApi/api/MenuApi/GetMenuData/GETSUBMENU_MVC1/";
                    sclient.BaseAddress = new Uri(slink + EMPCODE + "/1");

                    HttpResponseMessage sresult = sclient.GetAsync(sclient.BaseAddress).Result;

                    if (sresult.IsSuccessStatusCode)
                    {
                        string sdata = sresult.Content.ReadAsStringAsync().Result;
                        sdata = sdata.Replace(@"{""MenuResDto"":", @"");
                        sdata = sdata.Replace(@"{""menuResDto"":", @"");
                        sdata = sdata.Replace(@"}]}", @"}]");
                        sdata = sdata.Replace("\"\"", "\"");

                        _sN = JsonConvert.DeserializeObject<List<SubMenuNameModel>>(sdata);
                    }
                    menu_ViewModel.S_NAME = _sN;
                    //-----------------------------------------------------------------------------------------------------------------

                    using (var cclient = new HttpClient())
                    {

                        string clink = baseurl + "MenuApi/api/MenuApi/GetMenuData/GETCHILDMENU_MVC1/";
                        cclient.BaseAddress = new Uri(clink + EMPCODE + "/1");

                        HttpResponseMessage cresult = cclient.GetAsync(cclient.BaseAddress).Result;

                        if (cresult.IsSuccessStatusCode)
                        {
                            string cdata = cresult.Content.ReadAsStringAsync().Result;
                            cdata = cdata.Replace(@"{""MenuResDto"":", @"");
                            cdata = cdata.Replace(@"{""menuResDto"":", @"");
                            cdata = cdata.Replace(@"}]}", @"}]");
                            cdata = cdata.Replace("\"\"", "\"");

                            _cN = JsonConvert.DeserializeObject<List<ChildMenuNameModel>>(cdata);
                        }
                        menu_ViewModel.C_NAME = _cN;




                        //-----------------------------------------------------------------------------------------------------------------

                        //  ModelState.Clear();
                        return menu_ViewModel;
                    }
                }
            }
        }

        public string RemoveSpecialCharacters(string str)
                {
                    //-._~+/
                    //  return Regex.Replace(str, "[^a-zA-Z0-9_~+/-]+", "", RegexOptions.Compiled);

                    return Regex.Replace(str, "[^a-zA-Z0-9_~+/-{}]+", "", RegexOptions.Compiled);

                }

                public dynamic GetMainHeadData(string baseurl, string MainHeadID)
                {

                    var responseData = " ";

                    using (var client = new HttpClient())
                    {


                        string link = baseurl + "MenuApi/api/MenuApi/GetMenuData/GET_MAINHEADNAME/";
                        client.BaseAddress = new Uri(link + MainHeadID + "/1");

                        HttpResponseMessage result = client.GetAsync(client.BaseAddress).Result;

                        if (result.IsSuccessStatusCode)
                        {
                            string data = result.Content.ReadAsStringAsync().Result;
                            data = data.Replace(@"{""MenuResDto"":", @"");
                            data = data.Replace(@"}]}", @"}]");
                            data = data.Replace("\"\"", "\"");

                            responseData = JsonConvert.DeserializeObject<dynamic>(data);
                        }



                        //  ModelState.Clear();
                        return responseData;
                    }
                }

        public dynamic GetInternalPageData(string indata, string flag, string baseurl, string ApiPath)
        {          
            using (var client = new HttpClient())
            {
                string data = "";

                string link = baseurl +ApiPath;

                client.BaseAddress = new Uri(link + flag + "/" + indata + "/1");

                HttpResponseMessage result = client.GetAsync(client.BaseAddress).Result;

                if (result.IsSuccessStatusCode)
                {
                    data = result.Content.ReadAsStringAsync().Result;
                    data = data.Replace(@"{""MenuResDto"":", @"");
                    //  data = data.Replace(@"}]}", @"}]");
                    data = data.Replace("\"\"", "");
                    data = data.Replace("\'", "");
                  
                }



                //  ModelState.Clear();
                return data;
            }
        }

        public dynamic OTPHelperData(string flag, string indata, string baseurl, string ApiPath)
        {
            //string[] DataArray = indata.Split('^');

            using (var client = new HttpClient())
            {
                string data = "";

                string link = baseurl + ApiPath;

                client.BaseAddress = new Uri(link + flag + "/" + indata);

                HttpResponseMessage result = client.GetAsync(client.BaseAddress).Result;

                if (result.IsSuccessStatusCode)
                {
                    data = result.Content.ReadAsStringAsync().Result;
                  
                    data = data.Replace("\"\"", "");
                    data = data.Replace("\'", "");

                }

                return data;
            }
        }

      

        public dynamic PANHelper(string pan_no, string emp,string fid, string baseurl, string ApiPath)
        {
            
            using (var client = new HttpClient())
            {
                string data = "";

                string link = baseurl + ApiPath;

                client.BaseAddress = new Uri(link + pan_no + "/" + emp + "/" + fid);

                HttpResponseMessage result = client.GetAsync(client.BaseAddress).Result;

                if (result.IsSuccessStatusCode)
                {
                    data = result.Content.ReadAsStringAsync().Result;
                    data = data.Replace(@"{""MenuResDto"":", @"");
                    //  data = data.Replace(@"}]}", @"}]");
                    data = data.Replace("\"\"", "");
                    data = data.Replace("\'", "");

                }

                return data;
            }
        }






    }
}

