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
    public class InfoCriteriaController : Controller
    {
        private GeyserContext db = new GeyserContext();
        List<SelectListItem> carlists = new List<SelectListItem>();
        // GET: InfoCriteria
        public ActionResult Index(string id, FormCollection collection)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(6, 0, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                var ListCriteria = db.TblInfoCriteria.OrderBy(m => m.Ord).ToList();
                var menuModel = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlists.Clear();
                string strReturn = "---";
                foreach (var item in menuModel)
                {
                    carlists.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListFor(item.Id, carlists, strReturn);
                    strReturn = "---";
                }
                if (id != "")
                {

                    ViewBag.drMenu = carlists;
                    ViewBag.IdCate = id;
                    ViewBag.IdMenu = id;
                }
                else
                {
                    ViewBag.drMenu = carlists;
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
                                    int ids = Convert.ToInt32(key.Remove(0, 4));
                                    TblInfoCriteria TblCriteria = db.TblInfoCriteria.Find(ids);
                                    db.TblInfoCriteria.Remove(TblCriteria);
                                    db.SaveChanges();
                                     
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
        public PartialViewResult partialCriteria(int? page, string text, string id, string idCate, string pageSizes)
        {
            var ListCriteria = db.TblInfoCriteria.OrderBy(p => p.Ord).ToList();

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
            if (id != "" && id != null)
            {
                int idmenu = int.Parse(id);

                ListCriteria = db.TblInfoCriteria.Where(p => p.IdCri == idmenu).OrderBy(m => m.Ord).ToList();
                if(idCate!=null && idCate!="")
                {
                    List<string> Mang = new List<string>();
                    int idCates=int.Parse(idCate);
                    var ListConnect = db.TblGroupCriteria.Where(p => p.IdCate == idCates).ToList();
                    for(int i=0;i<ListConnect.Count;i++)
                    {
                        Mang.Add(ListConnect[i].IdCri.ToString());
                    }
                    ListCriteria = db.TblInfoCriteria.Where(p => Mang.Contains(p.IdCri.ToString())).OrderBy(m => m.Ord).ToList();
                }
                ViewBag.Idcha = id;
                return PartialView(ListCriteria.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.Idcha = 0;
            }

            return PartialView(ListCriteria.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UpdateCriteria(string id, string Ord, string cbIsActive)
        {
            if (ClsCheckRole.CheckQuyen(6, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                int ids = int.Parse(id);
                var TblCriteria = db.TblInfoCriteria.Find(ids);
                TblCriteria.Active = bool.Parse(cbIsActive);
                TblCriteria.Ord = int.Parse(Ord);
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
                TblInfoCriteria TblCriteria = db.TblInfoCriteria.Find(id);
                var result = string.Empty;
                db.TblInfoCriteria.Remove(TblCriteria);
                db.SaveChanges();
                 
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
        public ActionResult AutoOrd(string idCate)
        {

            int id = int.Parse(idCate);
            var ListOrd = db.TblInfoCriteria.Where(p => p.IdCri == id).OrderByDescending(p => p.Ord).Take(1).ToList();
            var result = string.Empty;
            if (ListOrd.Count > 0)
            {
                int stt = int.Parse(ListOrd[0].Ord.ToString()) + 1;
                result = stt.ToString();
            }
            else
            {
                result = "0";

            }
            return Json(new { result = result });
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
                 var menuModel = db.TblCriteria.OrderBy(m => m.Id).ToList();
                List<SelectListItem> carlist = new List<SelectListItem>(); 
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
                if (id != null && id != "")
                    ViewBag.drMenu = new SelectList(carlist, "Value", "Text", id);
                else
                    ViewBag.drMenu = carlist;
                var pro = db.TblInfoCriteria.OrderByDescending(p => p.Ord).Take(1).ToList();
                if (id != null && id != "")
                {
                    int idcre = int.Parse(id);
                      pro = db.TblInfoCriteria.Where(p => p.IdCri == idcre).OrderByDescending(p => p.Ord).Take(1).ToList();
                }
                
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
        public ActionResult Create(TblInfoCriteria TblCriteria, FormCollection collection)
        {
            int idcate = int.Parse(collection["drMenu"]);
            TblCriteria.IdCri = idcate;
            db.TblInfoCriteria.Add(TblCriteria);
            db.SaveChanges();
            
             
             if (collection["btnSave"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã thêm thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/InfoCriteria/Index?id="+idcate+"");
            }
            if (collection["btnSaveCreate"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm tiêu trí  mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/InfoCriteria/Create?id="+idcate+"");
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
                TblInfoCriteria TblCriteria = db.TblInfoCriteria.Find(id);

                var menuModel = db.TblCriteria.OrderBy(m => m.Id).ToList();
                List<SelectListItem> carlist = new List<SelectListItem>();
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }


                ViewBag.drMenu = new SelectList(carlist, "Value", "Text", TblCriteria.IdCri);
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
        public ActionResult Edit(TblInfoCriteria TblCriteria, int id, FormCollection collection )
        {
            if (ModelState.IsValid)
            {
                int idcate = int.Parse(collection["drMenu"]);
                TblCriteria.IdCri = idcate;
                db.Entry(TblCriteria).State = EntityState.Modified;
                db.SaveChanges();
                
                 
                if (collection["btnSave"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa  thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                    return Redirect("/InfoCriteria/Index?id="+idcate+"");
                }
                if (collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/InfoCriteria/Create?id=" + idcate + "");
                }
            }
            return View(TblCriteria);
        }

    }
}