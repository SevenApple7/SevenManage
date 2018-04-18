using ManageServer.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageServer.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult OrderManage()
        {
            return View();
        }
        public ActionResult OrderList()
        {
            string sql = "select * from orderrecord order by RefreshDate desc";
            MySqlDataReader dr = DBHelper.ExecuteReader(sql);
            string tableStr = "<thead><tr><th>订单编号</th><th>订单金额</th><th>订单状态</th><th>更新时间</th><th>操作</th></tr></thead><tbody>";
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string OrderId = dr["OrderId"].ToString();
                    string Amount = dr["Amount"].ToString();
                    string RefreshDate = dr["RefreshDate"].ToString();
                    string StateId = dr["StateId"].ToString().Trim() == "2" ? "未发货" : "已发货";
                    string buttonhtml = dr["Stateid"].ToString().Trim() == "2" ? "<a href=\"#\" class=\"btn btn-default\" role=\"button\">去发货</a>" : "<a href=\"#\" class=\"btn btn-default\" role=\"button\">查看</a>";
                    tableStr += "<tr><td>" + OrderId + "</td>" +
                        "<td>" + Amount + "</td>" +
                        "<td>" + StateId + "</td>" +
                        "<td>" + RefreshDate + "</td>" +
                        "<td>" + buttonhtml + "</td></tr>";
                }
            }
            tableStr += "</ tbody >";
            return Json(new { result = false, message = tableStr.Trim() }, JsonRequestBehavior.AllowGet);
        }
    }
}