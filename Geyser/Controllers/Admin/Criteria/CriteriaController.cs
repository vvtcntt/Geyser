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

namespace Geyser.Controllers.Admin.Criteria
{
    public class CriteriaController : Controller
    {
        private GeyserContext db = new GeyserContext();

        // GET: Criteria
        List<SelectListItem> carlist = new List<SelectListItem>();

        public ActionResult Index(string idCate, FormCollection collection)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(6, 0, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                var ListCriteria = db.TblCriteria.OrderBy(m => m.Ord).ToList();
                var listpage = new List<SelectListItem>();
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
                                if (ClsCheckRole.CheckQuyen(6, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
                                {
                                    int id = Convert.ToInt32(key.Remove(0, 4));
                                    TblCriteria TblCriteria = db.TblCriteria.Find(id);
                                    db.TblCriteria.Remove(TblCriteria);
                                    db.SaveChanges();
                                    var listGroupcri = db.TblGroupCriteria.Where(p => p.IdCri == id).ToList();
                                    for (int i = 0; i < listGroupcri.Count; i++)
                                    {
                                        db.TblGroupCriteria.Remove(listGroupcri[i]);
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
        public PartialViewResult partialCriteria(int? page, string text, string idCate, string pageSizes)
        {
            var ListCriteria = db.TblCriteria.OrderByDescending(p => p.Ord).ToList();

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
                ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + pageSize + "</span> / <span style='color: #333;'>" + ListCriteria.Count.ToString() + "</span>";
                return PartialView(ListCriteria.ToPagedList(pageNumber, pageSize));

            }
            if (idCate != "" && idCate != null)
            {
                int idmenu = int.Parse(idCate);
                List<int> mang = new List<int>();
                var listCri = db.TblGroupCriteria.Where(p => p.IdCate == idmenu).ToList();
                for (int i = 0; i < listCri.Count; i++)
                {
                    mang.Add(int.Parse(listCri[i].IdCri.ToString()));
                }
                ListCriteria = db.TblCriteria.Where(p => mang.Contains(p.Id)).OrderByDescending(m => m.Ord).ToList();
                ViewBag.Idcha = idCate;
                return PartialView(ListCriteria.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.Idcha = 0;
            }

            return PartialView(ListCriteria.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UpdateCriteria(string id, string Ord, string cbIsActive, string Priority, string Style)
        {
            if (ClsCheckRole.CheckQuyen(6, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                int ids = int.Parse(id);
                var TblCriteria = db.TblCriteria.Find(ids);
                TblCriteria.Ord = int.Parse(Ord);
                TblCriteria.Active = bool.Parse(cbIsActive);
                TblCriteria.Priority = bool.Parse(Priority);
                TblCriteria.Style = bool.Parse(Style);
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
        public ActionResult DeleteCriteria(int id)
        {
            if (ClsCheckRole.CheckQuyen(6, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblCriteria TblCriteria = db.TblCriteria.Find(id);
                var result = string.Empty;
                db.TblCriteria.Remove(TblCriteria);
                db.SaveChanges();
                var listGroupcri = db.TblGroupCriteria.Where(p => p.IdCri == id).ToList();
                for (int i = 0; i < listGroupcri.Count; i++)
                {
                    db.TblGroupCriteria.Remove(listGroupcri[i]);
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
            if (ClsCheckRole.CheckQuyen(6, 1, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
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
                var pro = db.TblCriteria.OrderByDescending(p => p.Ord).Take(1).ToList();
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
        public ActionResult Create(TblCriteria TblCriteria, FormCollection collection, int[] MutilMenu)
        {

            db.TblCriteria.Add(TblCriteria);
            db.SaveChanges();
            var listcri = db.TblCriteria.OrderByDescending(p => p.Id).Take(1).ToList();
            int IdCri = int.Parse(listcri[0].Id.ToString());
            if (MutilMenu != null)
            {
                foreach (var idCate in MutilMenu)
                {
                    TblGroupCriteria tblgroupcrieria = new TblGroupCriteria();
                    tblgroupcrieria.IdCate = idCate;
                    tblgroupcrieria.IdCri = IdCri;
                    db.TblGroupCriteria.Add(tblgroupcrieria);
                    db.SaveChanges();

                }
            }
            if (collection["btnSave"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã thêm thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/Criteria/Index");
            }
            if (collection["btnSaveCreate"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm tiêu trí  mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/Criteria/Create");
            }
            return Redirect("Index");


        }
        public ActionResult Edit(int id = 0)
        {


            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(6, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblCriteria TblCriteria = db.TblCriteria.Find(id);

                //int idCate = int.Parse(TblCriteria.IdCate.ToString());
                var menuModel = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlist.Clear();
                string strReturn = "---";
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListFor(item.Id, carlist, strReturn);
                    strReturn = "---";
                }

                var listcri = db.TblGroupCriteria.Where(p => p.IdCri == id).ToList();
                List<int> mang = new List<int>();
                for (int i = 0; i < listcri.Count; i++)
                {

                    mang.Add(int.Parse(listcri[i].IdCate.ToString()));

                }
                ViewBag.MutilMenu = new MultiSelectList(carlist, "Value", "Text", mang);
                if (TblCriteria == null)
                {
                    return HttpNotFound();
                }
                return View(TblCriteria);
            }
            else
            {
                return Redirect("/Users/Erro");


            }
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(TblCriteria TblCriteria, int id, FormCollection collection, int[] MutilMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(TblCriteria).State = EntityState.Modified;
                db.SaveChanges();
                var listcri = db.TblGroupCriteria.Where(p => p.IdCri == id).ToList();
                for (int i = 0; i < listcri.Count; i++)
                {
                    db.TblGroupCriteria.Remove(listcri[i]);
                    db.SaveChanges();
                }
                if (MutilMenu != null)
                {
                    foreach (var idCates in MutilMenu)
                    {
                        TblGroupCriteria tblgroupcrieria = new TblGroupCriteria();
                        tblgroupcrieria.IdCate = idCates;
                        tblgroupcrieria.IdCri = id;
                        db.TblGroupCriteria.Add(tblgroupcrieria);
                        db.SaveChanges();
                    }

                    if (collection["btnSave"] != null)
                    {
                        Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa  thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                        return Redirect("/Criteria/Index");
                    }
                    if (collection["btnSaveCreate"] != null)
                    {
                        Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                        return Redirect("/Criteria/Create");
                    }
                }
            }
            return View(TblCriteria);

        }
    }
}