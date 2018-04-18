using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageServer.Util;
using MySql.Data.MySqlClient;
using System.Web.Mvc;

namespace ManageServer.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductManage
        public ActionResult productManage()
        {
            return View();
        }
        public ActionResult productPush()
        {
            return View();
        }
        public ActionResult productList()
        {
            string sql = "select * from product_info order by StateId desc";
            MySqlDataReader dr = DBHelper.ExecuteReader(sql);
            string tableStr = "<caption><h4>上下文表格布局</h4></caption><thead><tr><th>产品编号</th><th>产品图</th><th>产品名称</th><th>产品价格</th><th>产品状态</th><th>操作</th></tr></thead><tbody>";
            if (dr.HasRows)
            {                
                while (dr.Read())
                {
                    string ProductId = dr["ProductId"].ToString();
                    string Param= "S" + dr["ProductId"].ToString();
                    string StateId = dr["StateId"].ToString().Trim() == "0" ? "已下架" : "已上架";
                    string buttonhtml = dr["StateId"].ToString().Trim() == "0" ? "<button type=\"button\" class=\"btn btn-primary updown\" onclick=\"Pup('" + Param + "')\" id=\"S" + ProductId + "\">上架</button>" : "<button type=\"button\" class=\"btn btn-danger updown\" onclick=\"Pdown('" + Param + "')\" id=\"S" + ProductId + "\">下架</button>";
                    tableStr += "<tr><td>" + ProductId + "</td>" +
                        "<td><img src=" + dr["MainImgUrl"].ToString() + " class=\"img-rounded\" style=\"width:50px;height:50px;\"></td>" +
                        "<td>"+dr["ProductName"].ToString()+"</td>" +
                        "<td>" +dr["ProductPrice"].ToString()+"</td>" +
                        "<td>"+ StateId+ "</td>" +
                        "<td>"+ buttonhtml + "</td></tr>";                    
                }
            }
            tableStr += "</ tbody >";
            return Json(new { result = false, message = tableStr.Trim() }, JsonRequestBehavior.AllowGet);
        }
    }
}