using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geyser.Models;
using PagedList;
using PagedList.Mvc;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Xml;
 using Geyser.Models;
using Microsoft.EntityFrameworkCore;

namespace Geyser.Controllers.Admin.WebSites
{
    public class WebController : Controller
    {
        private GeyserContext db = new GeyserContext();
        List<SelectListItem> carlist = new List<SelectListItem>();

        // GET: Web
        public ActionResult Index(string idCate, FormCollection collection)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(7, 0, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                var ListCriteria = db.TblCriteria.OrderBy(m => m.Ord).ToList();
                var menuModel = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlist.Clear();
                string strReturn = "---";
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListFor(item.Id, carlist, strReturn);
                    strReturn = "---";
                }
                if (idCate != "")
                {

                    ViewBag.drMenu = new SelectList(carlist, "Value", "Text", idCate);
                    ViewBag.IdCate = idCate;
                    ViewBag.IdMenu = idCate;
                }
                else
                {
                    ViewBag.drMenu = carlist;
                }
                if (collection["btnDelete"] != null)
                {
                    foreach (string key in Request.Form.Keys)
                    {
                        var checkbox = "";
                        if (key.StartsWith("chk_"))
                        {
                            checkbox = Request.Form["" + key];
                            if (checkbox != "false")
                            {
                                if (ClsCheckRole.CheckQuyen(7, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
                                {
                                    int id = Convert.ToInt32(key.Remove(0, 4));

                                     var TblWeb = db.TblWeb.Find(id);
                                     db.TblWeb.Remove(TblWeb);
                                    db.SaveChanges();
                                    var ListWeb = db.TblConnectWebs.Where(p => p.IdWeb == id).ToList();
                                    for (int i = 0; i < ListWeb.Count; i++)
                                    {
                                        db.TblConnectWebs.Remove(ListWeb[i]);
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    return Redirect("/Users/Erro");

                                }
                            }
                        }
                    }
                    //dsd
                }
                if (Session["Thongbao"] != null && Session["Thongbao"] != "")
                {

                    ViewBag.thongbao = Session["Thongbao"].ToString();
                    Session["Thongbao"] = "";
                }
                return View();
            }
            else
            {
                return Redirect("/Users/Erro");

            }
        }
        public PartialViewResult partialWeb(int? page, string text, string idCate, string pageSizes)
        {
            var Listwebs = db.TblWeb.OrderByDescending(p => p.Ord).ToList();

            int pageSize = 20;
            var pageNumber = (page ?? 1);
            // Thiết lập phân trang
            var ship = new PagedListRenderOptions
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToIndividualPages = true,
                DisplayPageCountAndCurrentLocation = false,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                EllipsesFormat = "&#8230;",
                LinkToFirstPageFormat = "Trang đầu",
                LinkToPreviousPageFormat = "«",
                LinkToIndividualPageFormat = "{0}",
                LinkToNextPageFormat = "»",
                LinkToLastPageFormat = "Trang cuối",
                PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                FunctionToDisplayEachPageNumber = null,
                ClassToApplyToFirstListItemInPager = null,
                ClassToApplyToLastListItemInPager = null,
                ContainerDivClasses = new[] { "pagination-container" },
                UlElementClasses = new[] { "pagination" },
                LiElementClasses = Enumerable.Empty<string>()
            };
            ViewBag.ship = ship;
            if (pageSizes != null)
            {
                ViewBag.pageSizes = pageSizes;
                pageSize = int.Parse(pageSizes.ToString());
                ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + pageSize + "</span> / <span style='color: #333;'>" + Listwebs.Count.ToString() + "</span>";
                return PartialView(Listwebs.ToPagedList(pageNumber, pageSize));

            }
            if (idCate != "" && idCate != null)
            {
                int idmenu = int.Parse(idCate);
                List<int> mang = new List<int>();
                var ListWeb = db.TblConnectWebs.Where(p => p.IdCate == idmenu).ToList();
                for (int i = 0; i < ListWeb.Count; i++)
                {
                    mang.Add(int.Parse(ListWeb[i].IdWeb.ToString()));
                }
                Listwebs = db.TblWeb.Where(p => mang.Contains(p.Id)).OrderBy(m => m.Ord).ToList();
                ViewBag.Idcha = idCate;
                return PartialView(Listwebs.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.Idcha = 0;
            }

            return PartialView(Listwebs.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UpdateWeb(string id, string Ord)
        {
            if (ClsCheckRole.CheckQuyen(7, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                int ids = int.Parse(id);
                var TblWeb = db.TblWeb.Find(ids);
                TblWeb.Ord = int.Parse(Ord);
                db.SaveChanges();
                var result = string.Empty;
                result = "Thành công";
                return Json(new { result = result });
            }
            else
            {
                var result = string.Empty;
                result = "Bạn không có quyền thay đổi tính năng này";
                return Json(new { result = result });
            }

        }
        public ActionResult DeleteWeb(int id)
        {
            if (ClsCheckRole.CheckQuyen(7, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblWeb TblWeb = db.TblWeb.Find(id);
                var result = string.Empty;
                db.TblWeb.Remove(TblWeb);
                db.SaveChanges();
                var Listwebs = db.TblConnectWebs.Where(p => p.IdWeb == id).ToList();
                for (int i = 0; i < Listwebs.Count; i++)
                {
                    db.TblConnectWebs.Remove(Listwebs[i]);
                    db.SaveChanges();
                }
                result = "Bạn đã xóa thành công.";
                return Json(new { result = result });
            }
            else
            {
                var result = string.Empty;
                result = "Bạn không có quyền thay đổi tính năng này";
                return Json(new { result = result });
            }

        }
        public ActionResult Create(string id)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (Session["Thongbao"] != null && Session["Thongbao"] != "")
            {

                ViewBag.thongbao = Session["Thongbao"].ToString();
                Session["Thongbao"] = "";
            }
            if (ClsCheckRole.CheckQuyen(7, 1, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                 var menuModel = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlist.Clear();
                string strReturn = "---";
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListFor(item.Id, carlist, strReturn);
                    strReturn = "---";
                }

                ViewBag.MutilMenu = new SelectList(carlist, "Value", "Text", id);
                var pro = db.TblWeb.OrderByDescending(p => p.Ord).Take(1).ToList();
                if (pro.Count > 0)
                    ViewBag.Ord = pro[0].Ord + 1;
                else
                    ViewBag.Ord = "1";
                return View();
            }
            else
            {
                return Redirect("/Users/Erro");

            }
        }

        [HttpPost]
        public ActionResult Create(TblWeb TblWeb, FormCollection collection, int[] MutilMenu)
        {

            db.TblWeb.Add(TblWeb);
            db.SaveChanges();
            var Listwebs = db.TblWeb.OrderByDescending(p => p.Id).Take(1).ToList();
            int IdWeb = int.Parse(Listwebs[0].Id.ToString());
            if (MutilMenu != null)
            {
                foreach (var idCate in MutilMenu)
                {
                    TblConnectWebs TblConnectWebs = new TblConnectWebs();
                    TblConnectWebs.IdCate = idCate;
                    TblConnectWebs.IdWeb = IdWeb;
                    db.TblConnectWebs.Add(TblConnectWebs);
                    db.SaveChanges();

                }
            }
             if (collection["btnSave"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã thêm thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/Web/Index");
            }
            if (collection["btnSaveCreate"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/Web/Create");
            }
            return Redirect("Index");


        }
        public ActionResult Edit(int id = 0)
        {


            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(7, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblWeb TblWeb = db.TblWeb.Find(id);

                 var menuModel = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlist.Clear();
                string strReturn = "---";
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListFor(item.Id, carlist, strReturn);
                    strReturn = "---";
                }
                var Listweb = db.TblConnectWebs.Where(p => p.IdWeb == id).ToList();
                List<int> mang = new List<int>();
                for (int i = 0; i < Listweb.Count; i++)
                {

                    mang.Add(int.Parse(Listweb[i].IdCate.ToString()));

                }
                ViewBag.MutilMenu = new MultiSelectList(carlist, "Value", "Text", mang);
                if (TblWeb == null)
                {
                    return HttpNotFound();
                }
                return View(TblWeb);
            }
            else
            {
                return Redirect("/Users/Erro");


            }
        } 
        [HttpPost]
        public ActionResult Edit(TblWeb TblWeb, int id, FormCollection collection, int[] MutilMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(TblWeb).State = EntityState.Modified;
                db.SaveChanges();
                var listwebs = db.TblConnectWebs.Where(p => p.IdWeb == id).ToList();
                for (int i = 0; i < listwebs.Count; i++)
                {
                    db.TblConnectWebs.Remove(listwebs[i]);
                    db.SaveChanges();
                }
                if (MutilMenu != null)
                {
                    foreach (var idCates in MutilMenu)
                    {
                        TblConnectWebs TblConnectWeb = new TblConnectWebs();
                        TblConnectWeb.IdCate = idCates;
                        TblConnectWeb.IdWeb = id;
                        db.TblConnectWebs.Add(TblConnectWeb);
                        db.SaveChanges();
                    }
                }
                #region[Updatehistory]
                 #endregion
                if (collection["btnSave"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa  thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                    return Redirect("/Web/Index");
                }
                if (collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/Web/Create");
                }
            }
            return View(TblWeb);
        }
    }
}