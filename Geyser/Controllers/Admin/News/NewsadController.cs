using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geyser.Models;
using PagedList;
using PagedList.Mvc;
using System.Globalization;
 using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
 using Geyser.Models;
using Microsoft.EntityFrameworkCore;

namespace Geyser.Controllers.Admin.News
{
    public class NewsadController : Controller
    {

        private GeyserContext db = new GeyserContext();
        List<SelectListItem> carlist = new List<SelectListItem>();
        List<SelectListItem> carlistProduct = new List<SelectListItem>();
       
        public ActionResult Index(int? page, string text, string idCate, string pageSizes, FormCollection collection)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(5, 0, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                #region[Load Menu]

                var pro = db.TblGroupNews.OrderByDescending(p => p.Ord).Take(1).ToList();
                var menuModel = db.TblGroupNews.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList(); carlist.Clear();
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
                #endregion
                if (Session["Thongbao"] != null && Session["Thongbao"] != "")
                {

                    ViewBag.thongbao = Session["Thongbao"].ToString();
                    Session["Thongbao"] = "";
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
                                    TblNews TblNews = db.TblNews.Find(id);
                                    int ord = int.Parse(TblNews.Ord.ToString());
                                    int idCates = int.Parse(TblNews.IdCate.ToString());
                                    var kiemtra = db.TblNews.Where(p => p.Ord > ord && p.IdCate == idCates).ToList();
                                    if (kiemtra.Count > 0)
                                    {
                                        var ListNews = db.TblNews.Where(p => p.Ord > ord && p.IdCate == idCates).ToList();
                                        for (int i = 0; i < ListNews.Count; i++)
                                        {
                                            int idp = int.Parse(ListNews[i].Id.ToString());
                                            var NewsUpdate = db.TblNews.Find(idp);
                                            NewsUpdate.Ord = NewsUpdate.Ord - 1;
                                            db.SaveChanges();
                                        }
                                    }
                                    db.TblNews.Remove(TblNews);
                                    db.SaveChanges();
                                    clsSitemap.DeteleSitemap(id.ToString(), "News");

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
                return View();
            }
            else
            {
                return Redirect("/Users/Erro");

            }
        }
       
        public PartialViewResult PartialNews(int? page, string text, string idCate, string pageSizes)
        {
            var ListNews = db.TblNews.OrderByDescending(p => p.DateCreate).ToList();
            int pageSize = 20;
            var pageNumber = (page ?? 1);
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


            if (Request.IsAjaxRequest())
            {
                int idCatelogy;
                if (pageSizes != null)
                {
                    ViewBag.pageSizes = pageSizes;
                    pageSize = int.Parse(pageSizes.ToString());
                    ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + pageSize + "</span> / <span style='color: #333;'>" + ListNews.Count.ToString() + "</span>";
                    return PartialView("PartialNews", ListNews.ToPagedList(pageNumber, pageSize));

                }
                if (text != null && text != "")
                {
                    ListNews = db.TblNews.Where(p => p.Name.ToUpper().Contains(text.ToUpper()) && p.Active == true).OrderByDescending(p => p.DateCreate).ToList();
                    ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + ListNews.Count + "</span> ";

                    return PartialView("PartialNews", ListNews.ToPagedList(pageNumber, pageSize));
                }
                if (idCate != null && idCate != "")
                {
                    idCatelogy = int.Parse(idCate);
                    ListNews = db.TblNews.Where(p => p.IdCate == idCatelogy).OrderByDescending(p => p.DateCreate).ToList();
                    ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + ListNews.Count + "</span> ";
                    ViewBag.IdMenu = idCate;
                    return PartialView("PartialNews", ListNews.ToPagedList(pageNumber, pageSize));
                }
                if (text != null && text != "" && idCate != null && idCate != "")
                {
                    idCatelogy = int.Parse(idCate);
                    ViewBag.IdMenu = idCate;
                    ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + ListNews.Count + "</span> ";
                    ListNews = db.TblNews.Where(p => p.Name.ToUpper().Contains(text.ToUpper()) && p.IdCate == (int.Parse(idCate)) && p.Active == true).OrderByDescending(p => p.Ord).ToList();
                    return PartialView("PartialNews", ListNews);
                }
                else
                {
                    ListNews = db.TblNews.OrderByDescending(p => p.Ord).ToList();

                }

            }

            if (pageSizes != null)
            {
                ViewBag.pageSizes = pageSizes;
                pageSize = int.Parse(pageSizes.ToString());

            }
            ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + pageSize + "</span> / <span style='color: #333;'>" + ListNews.Count.ToString() + "</span>";
            

             var menuModel = db.TblGroupNews.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
            carlist.Clear();
            string strReturn = "---";
            foreach (var item in menuModel)
            {
                carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                StringClass.DropDownListNews(item.Id, carlist, strReturn);
                strReturn = "---";
            }
            if (idCate != null)
            {

                int idcates = int.Parse(idCate);
                ListNews = db.TblNews.Where(p => p.IdCate == idcates && p.Active == true).OrderByDescending(p => p.DateCreate).ToList();
                ViewBag.IdMenu = idCate;
                ViewBag.IdCate = idCate;
                ViewBag.ddlMenu = carlist;
                return PartialView(ListNews.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.ddlMenu = carlist;
            }
            return PartialView(ListNews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UpdateNews(string IdNews, string cbIsActive, string chkHome, string ordernumber, string idCate)
        {
            if (ClsCheckRole.CheckQuyen(5, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                int id = int.Parse(IdNews);
                var News = db.TblNews.Find(id);

                News.ViewHomes = bool.Parse(chkHome);
                News.Active = bool.Parse(cbIsActive);
                News.IdCate = int.Parse(idCate);
                int Ord = int.Parse(ordernumber);
                News.Ord = Ord;
                int idCates = int.Parse(idCate);
                 var Kiemtra = db.TblNews.Where(p => p.Ord == Ord && p.IdCate == idCates && p.Id != id).ToList();
                if (Kiemtra.Count > 0)
                {
                    var ListNewss = db.TblNews.Where(p => p.Ord >= Ord && p.IdCate == idCates).ToList();
                    for (int i = 0; i < ListNewss.Count; i++)
                    {
                        int idp = int.Parse(ListNewss[i].Id.ToString());
                        var NewUpdate = db.TblNews.Find(idp);
                        NewUpdate.Ord = NewUpdate.Ord + 1;
                        db.SaveChanges();
                    }
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

            if (Session["Thongbao"] != null && Session["Thongbao"] != "")
            {

                ViewBag.thongbao = Session["Thongbao"].ToString();
                Session["Thongbao"] = "";
            }
            if (ClsCheckRole.CheckQuyen(5, 1, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                var menuModel = db.TblGroupNews.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList(); 
                string strReturn = "---";
                carlist.Clear();
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListNews(item.Id, carlist, strReturn);
                    strReturn = "---";
                }
                if (id != "")
                {
                    int ids = int.Parse(id); 
                    var pro = db.TblNews.Where(p => p.IdCate ==ids).OrderByDescending(p => p.Ord).Take(1).ToList(); 
                    ViewBag.drMenu = new SelectList(carlist, "Value", "Text", id);
                    int idcate = int.Parse(id.ToString());
                    if (pro.Count > 0)
                        ViewBag.Ord = pro[0].Ord + 1;
                    else
                        ViewBag.Ord = "1";
                }
                else
                {
                    ViewBag.drMenu = carlist;
                    var pro = db.TblNews.OrderByDescending(p => p.Ord).Take(1).ToList();
                    if (pro.Count > 0)
                        ViewBag.Ord = pro[0].Ord + 1;
                }
                var Manufacture = db.TblManufactures.Where(m => m.Active == true).OrderBy(m => m.Ord).ToList();
                var lstmanu = new List<SelectListItem>();

                foreach (var item in Manufacture)
                {
                    lstmanu.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
                ViewBag.mutilManu = new SelectList(lstmanu, "Value", "Text");
                return View();
            }
            else
            {
                return Redirect("/Users/Erro");

            }
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult Create(TblNews TblNews, FormCollection Collection, string id, int[] mutilManu)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }


            string nidCate = Collection["drMenu"];
            if (nidCate != "")
            {
                TblNews.IdCate = int.Parse(nidCate);
                int idcate = int.Parse(nidCate);
                TblNews.DateCreate = DateTime.Now;
                TblNews.Tag = StringClass.NameToTag(TblNews.Name);
                TblNews.DateCreate = DateTime.Now;
                TblNews.Visit = 0;
                string IdUser = Request.Cookies["Username"].Values["UserID"];
                TblNews.IdUser = int.Parse(IdUser);
                db.TblNews.Add(TblNews);
                db.SaveChanges(); var ListNews = db.TblNews.OrderByDescending(p => p.Id).Take(1).ToList();

                int IdNews = int.Parse(ListNews[0].Id.ToString());
                string nkeyword = TblNews.Tabs;
                string[] mangKeyword = nkeyword.Split(',');
                for (int i = 0; i < mangKeyword.Length; i++)
                {
                    if (mangKeyword[i] != null && mangKeyword[i] != "")
                    {
                        TblNewsTag TblNewstag = new TblNewsTag();
                        TblNewstag.Idn = IdNews;
                        TblNewstag.Name = mangKeyword[i];
                        TblNewstag.Tag = StringClass.NameToTag(mangKeyword[i]);
                        db.TblNewsTag.Add(TblNewstag);
                        db.SaveChanges();
                    }
                }
                var listprro = db.TblNews.OrderByDescending(p => p.Id).Take(1).ToList();
                clsSitemap.CreateSitemap(TblNews.Tag + ".htm", listprro[0].Id.ToString(), "News");
                #region[Updatehistory]
                 #endregion
               
                if (mutilManu != null)
                {
                    foreach (var idMenu in mutilManu)
                    {
                        TblConnectManuToNews conntectNews = new TblConnectManuToNews();
                        conntectNews.IdManu = idMenu;
                        conntectNews.IdNews = IdNews;
                        db.TblConnectManuToNews.Add(conntectNews);
                        db.SaveChanges();
                    }
                }
                if (Collection["btnSave"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã thêm tinthành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/Newsad/index?idCate=" + nidCate + "");
                }
                if (Collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm tin  thành công, mời bạn thêm tin mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/Newsad/Create?id=" + nidCate + "");
                }
            }
            return View();




        }
        public ActionResult AutoOrd(string idCate)
        {

            int id = int.Parse(idCate);
            var ListNews = db.TblNews.Where(p => p.IdCate == id).OrderByDescending(p => p.Ord).Take(1).ToList();
            var result = string.Empty;
            if (ListNews.Count > 0)
            {
                int stt = int.Parse(ListNews[0].Ord.ToString()) + 1;
                result = stt.ToString();
            }
            else
            {
                result = "0";

            }
            return Json(new { result = result });
        }
        public string CheckNews(string text)
        {
            string chuoi = "";
            var ListNews = db.TblNews.Where(p => p.Name == text).ToList();
            if (ListNews.Count > 0)
            {
                chuoi = "Tin đã bị trùng lặp !";

            }
            Session["Check"] = ListNews.Count;
            return chuoi;
        }
        public ActionResult Edit(int? id)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(5, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                Session["id"] = id.ToString();
                Int32 ids = Int32.Parse(id.ToString());
                TblNews TblNews = db.TblNews.Find(ids);

                if (TblNews == null)
                {
                    return HttpNotFound();
                }
                var menuModel = db.TblGroupNews.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlist.Clear();
                string strReturns = "---";
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListNews(item.Id, carlist, strReturns);
                    strReturns = "---";
                }
                int idGroups = 0;
                if (TblNews.IdCate != null)
                {
                    idGroups = int.Parse(TblNews.IdCate.ToString());
                }
                ViewBag.drMenu = new SelectList(carlist, "Value", "Text", idGroups);
 

                var Manufacture = db.TblManufactures.Where(m => m.Active == true).OrderBy(m => m.Ord).ToList();
                var listIdManu = db.TblConnectManuToNews.Where(p => p.IdNews == id).Select(p => p.IdManu).ToList();
                var lstmanu = new List<SelectListItem>();

                foreach (var item in Manufacture)
                {
                    lstmanu.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
                ViewBag.mutilManu = new MultiSelectList(lstmanu, "Value", "Text", listIdManu);
                return View(TblNews);
            }
            else
            {
                return Redirect("/Users/Erro");

            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TblNews TblNews, FormCollection collection, int? id, int[] mutilManu)
        {

            if (ModelState.IsValid)
            {
                if (collection["drMenu"] != "" || collection["drMenu"] != null)
                {
                     string IdUser = Request.Cookies["Username"].Values["UserID"];
                     TblNews.IdUser = int.Parse(IdUser);
                     bool URL = (collection["URL"] == "false") ? false : true;
                    if (URL == true)
                    {
                        TblNews.Tag = StringClass.NameToTag(TblNews.Name);
                    }
                    else
                    {
                        TblNews.Tag = collection["NameURL"];
                    }
                    clsSitemap.CreateSitemap(TblNews.Tag, id.ToString(), "Newsad");
                    TblNews.IdCate = int.Parse(collection["drMenu"]);
                    TblNews.DateCreate = DateTime.Now;
                    db.Entry(TblNews).State = EntityState.Modified;
                    db.SaveChanges();

                    if (URL == true)
                    {
                        TblNews.Tag = StringClass.NameToTag(TblNews.Name);
                        clsSitemap.UpdateSitemap( StringClass.NameToTag(TblNews.Name) + ".htm", id.ToString(), "News");

                    }
                    else
                    {
                        TblNews.Tag = collection["NameURL"];
                        clsSitemap.UpdateSitemap( StringClass.NameToTag(TblNews.Name) + ".htm", id.ToString(), "News");
                    }
                    int Ord = int.Parse(TblNews.Ord.ToString());
                    int idCate = int.Parse(collection["drMenu"]);
                    var Kiemtra = db.TblNews.Where(p => p.Ord == Ord && p.IdCate == idCate && p.Id != id).ToList();
                    if (Kiemtra.Count > 0)
                    {
                        var ListNewss = db.TblNews.Where(p => p.Ord >= Ord && p.IdCate == idCate).ToList();
                        for (int i = 0; i < ListNewss.Count; i++)
                        {
                            int idp = int.Parse(ListNewss[i].Id.ToString());
                            var NewUpdate = db.TblNews.Find(idp);
                            NewUpdate.Ord = NewUpdate.Ord + 1;
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();

                    var listNewsTag = db.TblNewsTag.Where(p => p.Idn == id).ToList();
                    for (int i = 0; i < listNewsTag.Count; i++)
                    {
                        int ids = listNewsTag[i].Id;
                        TblNewsTag TblNewstags = db.TblNewsTag.Find(ids);
                        db.TblNewsTag.Remove(TblNewstags);
                        db.SaveChanges();

                    }
                    string nkeyword = collection["Tabs"]; 
                    string[] mangKeyword = nkeyword.Split(',');
                    for (int i = 0; i < mangKeyword.Length; i++)
                    {
                        if (mangKeyword[i] != null && mangKeyword[i] != "")
                        {
                            TblNewsTag TblNewstags = new TblNewsTag();
                            TblNewstags.Idn = id;
                            TblNewstags.Name = mangKeyword[i];
                            TblNewstags.Tag = StringClass.NameToTag(mangKeyword[i]);
                            db.TblNewsTag.Add(TblNewstags);
                            db.SaveChanges();
                        }
                    }
                    var listIdManu = db.TblConnectManuToNews.Where(p => p.IdNews == id).ToList();
                    for (int i = 0; i < listIdManu.Count; i++)
                    {
                        db.TblConnectManuToNews.Remove(listIdManu[i]);
                        db.SaveChanges();
                    }
                    if (mutilManu != null)
                    {
                        foreach (var idMenu in mutilManu)
                        {
                            TblConnectManuToNews connectimage = new TblConnectManuToNews();
                            connectimage.IdManu = idMenu;
                            connectimage.IdNews = id;
                            db.TblConnectManuToNews.Add(connectimage);
                            db.SaveChanges();
                        }
                    }
                }
                if (collection["btnSave"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa tin thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                    return Redirect("/Newsad/Index?idCate=" + int.Parse(collection["drMenu"]));
                }
                if (collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm tin thành công, mời bạn thêm tin mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/Newsad/Create?id=" + +int.Parse(collection["drMenu"]) + "");
                }
                #region[Updatehistory]
                 #endregion
            }
            return View(TblNews);
        }
        public ActionResult DeleteNews(int id)
        {
            if (ClsCheckRole.CheckQuyen(4, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblNews TblNews = db.TblNews.Find(id);
                clsSitemap.DeteleSitemap(id.ToString(), "News");
                var result = string.Empty;
                int ord = int.Parse(TblNews.Ord.ToString());
                int idCate = int.Parse(TblNews.IdCate.ToString());
                var kiemtra = db.TblNews.Where(p => p.Ord > ord && p.IdCate == idCate).ToList();
                if (kiemtra.Count > 0)
                {
                    var ListNews = db.TblNews.Where(p => p.Ord > ord && p.IdCate == idCate).ToList();
                    for (int i = 0; i < ListNews.Count; i++)
                    {
                        int idp = int.Parse(ListNews[i].Id.ToString());
                        var NewsUpdate = db.TblNews.Find(idp);
                        NewsUpdate.Ord = NewsUpdate.Ord - 1;
                        db.SaveChanges();
                    }
                }
                db.TblNews.Remove(TblNews);
                db.SaveChanges();
                result = "Bạn đã xóa thành công.";
                return Json(new { result = result });
            }
            else
            {
                var result = string.Empty;

                result = "Bạn không có quyền thay đổi tính năng này !.";
                return Json(new { result = result });

            }

        }
        public JsonResult ListTag(string q)
        {
            var listTag = db.TblNews.Where(p => p.Active == true).ToList();
            List<string> Mang = new List<string>();
            for (int i = 0; i < listTag.Count; i++)
            {
                string[] tag = listTag[i].Keyword.Split(',');
                for (int j = 0; j < tag.Length; j++)
                {
                    if (tag[j].ToUpper().Contains(q.ToUpper()))
                        Mang.Add(tag[j].ToString());
                }
            }
            var ListName = Mang.Take(15);
            return Json(new
            {
                data = ListName,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Command()
        {
            var listNews = db.TblNews.ToList();
            for(int i=0;i<listNews.Count;i++)
            {
                TblConnectManuToNews connect = new TblConnectManuToNews();
                connect.IdManu = 1;
                connect.IdNews = listNews[i].Id;
                db.TblConnectManuToNews.Add(connect);
                db.SaveChanges();
            }
            return View();
        }
        public ActionResult commandTag()
        {
            var listNews = db.TblNews.ToList();
            for (int i = 0; i < listNews.Count; i++)
            {
                int id = listNews[i].Id;
                try
                {
                    if (listNews[i].Tabs != null && listNews[i].Tabs != "")
                    {
                        string[] tabs = listNews[i].Tabs.Split(',');
                        for (int j = 0; j < tabs.Length; j++)
                        {
                            TblNewsTag newsTag = new TblNewsTag();
                            if (tabs[j] != null && tabs[j] != "")
                            {
                                newsTag.Name = tabs[j];
                                newsTag.Tag = StringClass.NameToTag(tabs[j]);
                                newsTag.Idn = id;
                                db.TblNewsTag.Add(newsTag);
                                db.SaveChanges();
                            }

                        }


                    }
                }
                catch (Exception ex)
                {

                }

            }
            return View();
        }

    }
}