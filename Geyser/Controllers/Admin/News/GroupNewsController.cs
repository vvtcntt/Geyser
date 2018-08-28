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

namespace Geyser.Controllers.Admin.News
{
    public class GroupNewsController : Controller
    {
        List<SelectListItem> carlist = new List<SelectListItem>();
        //public void DropDownListNews(int cateid)
        //{
        //    GeyserContext db = new GeyserContext();
        //    var cars = db.TblGroupNews.Where(p => p.ParentId == cateid).ToList();
        //    foreach (var item in cars)
        //    {
        //        carlist.Add(new SelectListItem { Text = StringClass.ShowNameLevel(int.Parse(item.Level.ToString())) + " " + item.Name, Value = item.Id.ToString() });
        //        DropDownListNews(item.Id);
        //    }
        //}
        // GET: GroupNews
        private GeyserContext db = new GeyserContext();
        public ActionResult Index(string idCate, FormCollection collection)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(5, 0, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                var menuModel = db.TblGroupNews.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlist.Clear();
                string strReturn = "---";
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() }); 
                    StringClass.DropDownListNews(item.Id, carlist, strReturn);
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
                                if (ClsCheckRole.CheckQuyen(5, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
                                {
                                    int id = Convert.ToInt32(key.Remove(0, 4));
                                    TblGroupNews TblGroupNews = db.TblGroupNews.Find(id);
                                    db.TblGroupNews.Remove(TblGroupNews);
                                    db.SaveChanges();
                                    var listnews = db.TblNews.Where(p => p.IdCate == id).ToList();
                                    for(int i=0;i<listnews.Count;i++)
                                    {
                                        db.TblNews.Remove(listnews[i]);
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
        public PartialViewResult PartialGroupNews(int? page, string text, string idCate, string pageSizes)
        {
            var ListNews = db.TblGroupNews.Where(p => p.ParentId==null && p.Active==true).OrderBy(p => p.Ord).ToList();

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
                ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + pageSize + "</span> / <span style='color: #333;'>" + ListNews.Count.ToString() + "</span>";
                return PartialView(ListNews.ToPagedList(pageNumber, pageSize));

            }
            if (idCate != "" && idCate != null)
            {
                int idmenu = int.Parse(idCate);
                var menucha = db.TblGroupNews.Find(idmenu);
              
                ListNews = db.TblGroupNews.Where(p =>p.ParentId==idmenu).OrderBy(p => p.Ord).ToList();
                ViewBag.Idcha = idCate;
                return PartialView(ListNews.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.Idcha = 0;
            }

            return PartialView(ListNews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UpdateGroupNews(string id, string Active, string order, string idCate, string Priority)
        {
            if (ClsCheckRole.CheckQuyen(5, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                int ids = int.Parse(id);
                var GroupNews = db.TblGroupNews.Find(ids);
                int idcate1 = GroupNews.Id;
                GroupNews.Active = bool.Parse(Active);
                GroupNews.Ord = int.Parse(order);
                 if(idCate=="" || idCate==null)
                {
                    GroupNews.ParentId = int.Parse(idCate);
                    int idCates = int.Parse(idCate);
                    if (idcate1 != idCates)
                    {
                        var listord = db.TblGroupNews.Where(p => p.ParentId == idCates).OrderByDescending(p => p.Id).Take(1).ToList();
                        GroupNews.Ord = int.Parse(listord[0].Ord.ToString()) + 1;

                    }
                }
                else
                {
                    GroupNews.ParentId = null;
 
                } 
                  
                db.SaveChanges();
                 var result = string.Empty;
                result = "Thành công";
                return Json(new { result = result });
            }
            else
            {
                var result = string.Empty;
                result = "Bạn không có quyền truy cập tính năng này";
                return Json(new { result = result });
            }
        }
        public ActionResult Create(string id)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(5, 1, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                 var pro = db.TblGroupNews.OrderByDescending(p => p.Ord).Take(1).ToList();
                 var menuModel = db.TblGroupNews.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                 carlist.Clear();
                 string strReturn = "---";
                 foreach (var item in menuModel)
                 {
                     carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                     StringClass.DropDownListNews(item.Id, carlist, strReturn);
                     strReturn = "---";
                 }
                 ViewBag.drMenu = new SelectList(carlist, "Value", "Text", id);
                ViewBag.Ord = pro[0].Ord + 1;
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TblGroupNews TblGroupNews, FormCollection collection)
        {

            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            string drMenu = collection["drMenu"];
            string nLevel;

            if (drMenu == "")
            {
                TblGroupNews.ParentId = null;
             }
            else
            {

                var dbLeve = db.TblGroupNews.Find(int.Parse(drMenu));
                TblGroupNews.ParentId = dbLeve.Id;
             }

            TblGroupNews.DateCreate = DateTime.Now;
            string IdUser = Request.Cookies["Username"].Values["UserID"];
            TblGroupNews.IdUser = int.Parse(IdUser);
            TblGroupNews.Tag = StringClass.NameToTag(TblGroupNews.Name);
            db.TblGroupNews.Add(TblGroupNews);
            db.SaveChanges();
 
            var Groups = db.TblGroupNews.Where(p => p.Active == true).OrderByDescending(p => p.Id).Take(1).ToList();
            string id = Groups[0].Id.ToString();

            clsSitemap.CreateSitemap(StringClass.NameToTag(TblGroupNews.Name), id, "GroupNews");
            if (collection["btnSave"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã thêm danh mục thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                return Redirect("/GroupNews/Index?idCate=" + drMenu);
            }
            if (collection["btnSaveCreate"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm danh mục thành công, mời bạn thêm danh mục mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/GroupNews/Create?id=" + drMenu + "");
            }
            return Redirect("/GroupNews/Index?idCate=" + drMenu);


        }
        public ActionResult AutoOrd(string idCate)
        {
            var result = string.Empty;

            if (idCate != "")
            {
                int id = int.Parse(idCate);
 
                var GroupNews = db.TblGroupNews.Where(p => p.ParentId==id).OrderByDescending(p => p.Ord).Take(1).ToList();

                if (GroupNews.Count > 0)
                {
                    int stt = int.Parse(GroupNews[0].Ord.ToString()) + 1;
                    result = stt.ToString();
                }
                else
                {
                    result = "0";

                }
            }
            else
            {
                var GroupNews = db.TblGroupNews.Where(p => p.ParentId == null).OrderByDescending(p => p.Ord).Take(1).ToList();

                int stt = int.Parse(GroupNews[0].Ord.ToString()) + 1;
                result = stt.ToString();
            }


            return Json(new { result = result });
        }
        public ActionResult Edit(int id)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(5, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblGroupNews TblGroupNews = db.TblGroupNews.First(p => p.Id == id);
                if (TblGroupNews == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id = id;
                var menuName = db.TblGroupNews.ToList();
                var pro = db.TblGroupNews.OrderByDescending(p => p.Ord).Take(1).ToList();
                var menuModel = db.TblGroupNews.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlist.Clear();
                string strReturn = "---";
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListNews(item.Id, carlist, strReturn);
                    strReturn = "---";
                }
                ViewBag.drMenu = new SelectList(carlist, "Value", "Text", id);

                return View(TblGroupNews);
            }
            else
            {
                return Redirect("/Users/Erro");

            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TblGroupNews TblGroupNews, FormCollection collection, int id)
        {
            
                string drMenu = collection["drMenu"];
                string nLevel = "";

                if (drMenu == "")
                {
                    TblGroupNews.ParentId = null;
                     var counts = db.TblGroupNews.Where(p => p.ParentId == null).OrderByDescending(p => p.Ord).Take(1).ToList();
                 }
                else
                {
                     if (drMenu != id.ToString())
                    {
                        var dbLeve = db.TblGroupNews.Find(int.Parse(drMenu));
                        TblGroupNews.ParentId = dbLeve.Id;
                     }
                     
                }
                string IdUser = Request.Cookies["Username"].Values["UserID"];
                TblGroupNews.IdUser = int.Parse(IdUser);

                bool URL = (collection["URL"] == "false") ? false : true;
                if (URL == true)
                {
                    TblGroupNews.Tag = StringClass.NameToTag(TblGroupNews.Name);
                }
                else
                {
                    TblGroupNews.Tag = collection["NameURL"];
                }
                 clsSitemap.CreateSitemap(TblGroupNews.Tag, id.ToString(), "GroupNews");

                TblGroupNews.DateCreate = DateTime.Now;
                db.Entry(TblGroupNews).State = EntityState.Modified;
                db.SaveChanges();
                #region[Updatehistory]
                 #endregion
                if (collection["btnSave"] != null)
                {

                    if (drMenu == "")
                    {
                        Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa danh mục thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                        return Redirect("/GroupNews/Index?id=" + drMenu + "");
                    }
                    else
                    {
                        Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa danh mục thành công  !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                        var checkgroup = db.TblGroupNews.Where(p => p.Id==int.Parse(drMenu)).ToList();
                        if (checkgroup.Count > 0)
                            return Redirect("/GroupNews/Index?idCate=" + checkgroup[0].Id);

                    }
                }
                if (collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã sửa danh mục thành công, mời bạn thêm danh mục  mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/GroupNews/Create?id=" + drMenu + "");
                }
            
            return Redirect("/GroupNews/");
        }
        public ActionResult DeleteGroupNews(int id)
        {
            if (ClsCheckRole.CheckQuyen(5, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblGroupNews TblGroupNews = db.TblGroupNews.Find(id);
                clsSitemap.DeteleSitemap(id.ToString(), "GroupNews");
                var result = string.Empty;
                db.TblGroupNews.Remove(TblGroupNews);
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
    }
}