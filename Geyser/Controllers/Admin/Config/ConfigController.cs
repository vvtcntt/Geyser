using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geyser.Models;
 using Geyser.Models;
using Microsoft.EntityFrameworkCore;

namespace Geyser.Controllers.Admin.Config
{
    public class ConfigController : Controller
    {
        // GET: Config
        private GeyserContext db = new GeyserContext();
        public ActionResult Index()
        {

            if ((Request.Cookies["Username"] == null))
            {

                return RedirectToAction("LoginIndex", "Login");
            }
 
            if (ClsCheckRole.CheckQuyen(1, 0, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                if (Session["Thongbao"] != "")
                {
                    ViewBag.thongbao = Session["Thongbao"];
                    Session["Thongbao"] = "";
                }
                TblConfig TblConfig = db.TblConfig.FirstOrDefault();
               

                if (TblConfig == null)
                {
                    return HttpNotFound();
                }
                else
                    return View(TblConfig);

               
            }
            else
            {
               Session["Role"] = "<script>$(document).ready(function(){ alert('Bạn không có quyền truy cập vào tính năng này !') });</script>";

            }
            return Redirect("/Users/Erro");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(TblConfig TblConfig, int id = 1)
        {
            if (ModelState.IsValid)
            {

                TblConfig.Id = id;

                #region[Updatehistory]
                 #endregion
                db.Entry(TblConfig).State =  EntityState.Modified;
                db.SaveChanges();
                Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn cập nhật thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return RedirectToAction("Index");
            }
            return View(TblConfig);
        }
    }
}