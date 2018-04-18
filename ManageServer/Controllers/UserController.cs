using ManageServer.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageServer.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserManage()
        {
            return View();
        }
        public ActionResult UserList()
        {
            string sql = "select * from wx_userinfo order by CreateDate desc";
            MySqlDataReader dr = DBHelper.ExecuteReader(sql);
            string tableStr = "<caption><h4>上下文表格布局</h4></caption><thead><tr><th>用户ID</th><th>用户头像</th><th>用户昵称</th><th>性别</th><th>区域</th><th>创建时间</th></tr></thead><tbody>";
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string openid = dr["openid"].ToString();
                    string avatarUrl = dr["avatarUrl"].ToString();
                    string nickname = dr["nickname"].ToString();
                    string gender = dr["gender"].ToString().Trim() == "1" ? "男" : "女";
                    string city = dr["city"].ToString().Trim();
                    string CreateDate = dr["CreateDate"].ToString().Trim();
                    tableStr += "<td>" + openid + "</td>" +
                        "<td><img src=" + avatarUrl + " class=\"img-rounded\" style=\"width:50px;height:50px;\"></td>" +
                        "<td>" + nickname + "</td>" +
                        "<td>" + gender + "</td>" +
                        "<td>" + city + "</td>" +
                        "<td>" + CreateDate + "</td></tr>";
                }
            }
            tableStr += "</ tbody >";
            return Json(new { result = false, message = tableStr.Trim() }, JsonRequestBehavior.AllowGet);
        }
    }
}