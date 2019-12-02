using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Park.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
        public void Daoru(HttpPostedFileBase File)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("/File/")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("/File/"));
            }
            File.SaveAs(Server.MapPath("/File/" + File.FileName));
            PostFile(File.ToString());
        }
        public void PostFile(string file)
        {
            DataTable dt = PublicToolsLib.HelpExcel.NpoiExcelHelper.ImportExcel(file);
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            pairs.Add("", "");
            pairs.Add("", "");
            pairs.Add("", "");
            PublicToolsLib.HelpExcel.NpoiExcelHelper.ExportExcel(dt,pairs);
            HttpCookie cookie = new HttpCookie("Dao");
            cookie.Value = JsonConvert.SerializeObject(dt);
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies["Dao"].Values;
            
        }

    }
}