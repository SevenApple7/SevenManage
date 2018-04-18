using ManageServer.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Navlist()
        {
            string sql = "select * from Indexnav order by navid ASC";
            MySqlDataReader dr = DBHelper.ExecuteReader(sql);
            string tableStr = "";
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string Name = dr["Name"].ToString();
                    string Link = dr["Link"].ToString();
                    string PictureUrl = dr["PictureUrl"].ToString().Trim();
                    tableStr += "<div class=\"col-sm-10 col-md-3\">" +
                            "<a href=\""+ Link + "\" class=\"thumbnail\">" +
                                "<img src=\""+ PictureUrl + "\">" +
                                "<div class=\"caption\" style=\"text-align:center\">" +
                                    "<h3>"+ Name + "</h3>" +
                                "</div>" +
                            "</a>" +
                        "</div>";
                }
            }
            return Json(new { result = false, message = tableStr.Trim() }, JsonRequestBehavior.AllowGet);
        }
    }
}