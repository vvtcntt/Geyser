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

namespace Geyser.Controllers.Admin
{
    public class ManufacturesadController : Controller
    {
        List<SelectListItem> carlist = new List<SelectListItem>();

        private GeyserContext db = new GeyserContext();

        // GET: Manufacturesad
        public ActionResult Index(string idCate, FormCollection collection)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(8, 0, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
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

                                    var tblManu = db.TblManufactures.Find(id);
                                    db.TblManufactures.Remove(tblManu);
                                    db.SaveChanges();
                                    var ListManu = db.TblConnectManuProduct.Where(p => p.IdManu == id).ToList();
                                    for (int i = 0; i < ListManu.Count; i++)
                                    {
                                        db.TblConnectManuProduct.Remove(ListManu[i]);
                                        db.SaveChanges();
                                    }
                                    clsSitemap.DeteleSitemap(id.ToString(), "Manufactures");

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
        public PartialViewResult partialManufactures(int? page, string text, string idCate, string pageSizes)
        {
            var ListManu = db.TblManufactures.OrderByDescending(p => p.Ord).ToList();

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
                ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + pageSize + "</span> / <span style='color: #333;'>" + ListManu.Count.ToString() + "</span>";
                return PartialView(ListManu.ToPagedList(pageNumber, pageSize));

            }
            if (idCate != "" && idCate != null)
            {
                int idmenu = int.Parse(idCate);
                List<int> mang = new List<int>();
                var ListManus = db.TblConnectManuProduct.Where(p => p.IdCate == idmenu).ToList();
                for (int i = 0; i < ListManus.Count; i++)
                {
                    mang.Add(int.Parse(ListManus[i].IdManu.ToString()));
                }
                ListManu = db.TblManufactures.Where(p => mang.Contains(p.Id)).OrderBy(m => m.Ord).ToList();
                ViewBag.Idcha = idCate;
                return PartialView(ListManu.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.Idcha = 0;
            }

            return PartialView(ListManu.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UpdateManufactures(string id, string Ord, string cbIsActive,string Priority)
        {
            if (ClsCheckRole.CheckQuyen(8, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                int ids = int.Parse(id);
                var tblManu = db.TblManufactures.Find(ids);
                tblManu.Ord = int.Parse(Ord);
                tblManu.Active = bool.Parse(cbIsActive);
                tblManu.Priority = bool.Parse(Priority);
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
        public ActionResult DeleteManufactures(int id)
        {
            if (ClsCheckRole.CheckQuyen(8, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblManufactures tblmanu = db.TblManufactures.Find(id);
                var result = string.Empty;
                db.TblManufactures.Remove(tblmanu);
                db.SaveChanges();
                var ListManu = db.TblConnectManuProduct.Where(p => p.IdManu == id).ToList();
                for (int i = 0; i < ListManu.Count; i++)
                {
                    db.TblConnectManuProduct.Remove(ListManu[i]);
                    db.SaveChanges();
                }
                clsSitemap.DeteleSitemap( id.ToString(), "Manufactures");

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
            if (ClsCheckRole.CheckQuyen(8, 1, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
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
                var pro = db.TblManufactures.OrderByDescending(p => p.Ord).Take(1).ToList();
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
        [ValidateInput(false)]

        public ActionResult Create(TblManufactures tblmanu, FormCollection collection, int[] MutilMenu)
        {
            tblmanu.Tag = StringClass.NameToTag(tblmanu.Name);
            db.TblManufactures.Add(tblmanu);
            db.SaveChanges();
            var ListManu = db.TblManufactures.OrderByDescending(p => p.Id).Take(1).ToList();
            int idManu = int.Parse(ListManu[0].Id.ToString());
            if (MutilMenu != null)
            {
                foreach (var idCate in MutilMenu)
                {
                    TblConnectManuProduct TblManufactures = new TblConnectManuProduct();
                    TblManufactures.IdCate = idCate;
                    TblManufactures.IdManu = idManu;
                    db.TblConnectManuProduct.Add(TblManufactures);
                    db.SaveChanges();

                }
            }
            var ListManufac = db.TblManufactures.OrderByDescending(p => p.Id).Take(1).ToList();
            clsSitemap.CreateSitemap("hang-san-xuat/" + ListManufac[0].Tag, ListManufac[0].Id.ToString(), "Manufactures");

             if (collection["btnSave"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã thêm thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/Manufacturesad/Index");
            }
            if (collection["btnSaveCreate"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/Manufacturesad/Create");
            }
            return Redirect("Index");


        }
        public ActionResult Edit(int id = 0)
        {


            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(8, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblManufactures tblmanu = db.TblManufactures.Find(id);

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
                var ListManu = db.TblConnectManuProduct.Where(p => p.IdManu == id).ToList();
                List<int> mang = new List<int>();
                for (int i = 0; i < ListManu.Count; i++)
                {

                    mang.Add(int.Parse(ListManu[i].IdCate.ToString()));

                }
                ViewBag.MutilMenu = new MultiSelectList(carlist, "Value", "Text", mang);
                if (tblmanu == null)
                {
                    return HttpNotFound();
                }
                return View(tblmanu);
            }
            else
            {
                return Redirect("/Users/Erro");


            }
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Edit(TblManufactures TblManufacture, int id, FormCollection collection, int[] MutilMenu)
        {
            if (ModelState.IsValid)
            {
                TblManufacture.Tag = StringClass.NameToTag(TblManufacture.Name);
                db.Entry(TblManufacture).State = EntityState.Modified;
                db.SaveChanges();
                var ListManu = db.TblConnectManuProduct.Where(p => p.IdManu == id).ToList();
                for (int i = 0; i < ListManu.Count; i++)
                {
                    db.TblConnectManuProduct.Remove(ListManu[i]);
                    db.SaveChanges();
                }
                if (MutilMenu != null)
                {
                    foreach (var idCates in MutilMenu)
                    {
                        TblConnectManuProduct TblManufactures = new TblConnectManuProduct();
                        TblManufactures.IdCate = idCates;
                        TblManufactures.IdManu = id;
                        db.TblConnectManuProduct.Add(TblManufactures);
                        db.SaveChanges();
                    }
                }
                clsSitemap.UpdateSitemap("hang-san-xuat/" + TblManufacture.Tag, id.ToString(), "Manufactures");
                #region[Updatehistory]
                 #endregion
                if (collection["btnSave"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa  thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                    return Redirect("/Manufacturesad/Index");
                }
                if (collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/Manufacturesad/Create");
                }
            }
            return View(TblManufacture);
        }
    }
}