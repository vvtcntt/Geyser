using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
 using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Geyser.Models;
using Geyser.Models;
using Microsoft.EntityFrameworkCore;

namespace Geyser.Controllers.Admin.Images
{
    public class ImagesadController : Controller
    {
        private List<SelectListItem> carlist = new List<SelectListItem>();

        // GET: Imagesad
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
                var menuModel = db.TblGroupImage.Where(m => m.Active == true).OrderBy(m => m.Id).ToList();
                carlist.Clear();
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
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
                                if (ClsCheckRole.CheckQuyen(9, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
                                {
                                    int id = Convert.ToInt32(key.Remove(0, 4));

                                    var TblImage = db.TblImage.Find(id);
                                    db.TblImage.Remove(TblImage);
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

        public PartialViewResult partialImages(int? page, string text, string idCate, string pageSizes)
        {
            var ListImages = db.TblImage.OrderByDescending(p => p.Ord).ToList();

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
                ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + pageSize + "</span> / <span style='color: #333;'>" + ListImages.Count.ToString() + "</span>";
                return PartialView(ListImages.ToPagedList(pageNumber, pageSize));
            }
            if (idCate != "" && idCate != null)
            {
                int idmenu = int.Parse(idCate);

                ListImages = db.TblImage.Where(p => p.IdCate == idmenu).OrderByDescending(m => m.Ord).ToList();
                ViewBag.Idcha = idCate;
                ViewBag.IdCate = idCate;
                return PartialView(ListImages.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.Idcha = 0;
            }

            return PartialView(ListImages.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UpdateImages(string id, string Ord, string ddlMenu, string Active)
        {
            if (ClsCheckRole.CheckQuyen(9, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                int ids = int.Parse(id);
                int idcate = int.Parse(ddlMenu);
                var TblImage = db.TblImage.Find(ids);
                TblImage.Ord = int.Parse(Ord);
                TblImage.IdCate = idcate;
                TblImage.Active = bool.Parse(Active);
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

        public ActionResult DeleteImages(int id)
        {
            if (ClsCheckRole.CheckQuyen(9, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblImage TblImage = db.TblImage.Find(id);
                var result = string.Empty;
                db.TblImage.Remove(TblImage);
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
            var ListOrd = db.TblImage.Where(p => p.IdCate == id).OrderByDescending(p => p.Ord).Take(1).ToList();
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
                ViewBag.MutilMenu = new SelectList(carlist, "Value", "Text");
                var menuModels = db.TblGroupImage.Where(m => m.Active == true).OrderBy(m => m.Ord).ToList();
                var lstMenus = new List<SelectListItem>();
                lstMenus.Clear();
                foreach (var menu in menuModels)
                {
                    lstMenus.Add(new SelectListItem { Text = menu.Name, Value = menu.Id.ToString() });
                }
                if (id != null && id != "")
                    ViewBag.drMenu = new SelectList(lstMenus, "Value", "Text", id);
                else
                    ViewBag.drMenu = lstMenus;
                var pro = db.TblImage.OrderByDescending(p => p.Ord).Take(1).ToList();
                if (id != null && id != "")
                {
                    int idcates = int.Parse(id);
                    pro = db.TblImage.Where(p => p.IdCate == idcates).OrderByDescending(p => p.Ord).Take(1).ToList();
                }
                if (pro.Count > 0)
                    ViewBag.Ord = pro[0].Ord + 1;
                else
                    ViewBag.Ord = "1";
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
        public ActionResult Create(TblImage TblImage, FormCollection collection, int[] MutilMenu, int[] mutilManu)
        {
            int idCate = int.Parse(collection["drMenu"]);
            TblImage.IdCate = idCate;
            db.TblImage.Add(TblImage);
            db.SaveChanges();
            var ListManu = db.TblImage.OrderByDescending(p => p.Id).Take(1).ToList();
            int idimg = int.Parse(ListManu[0].Id.ToString());
            if (MutilMenu != null)
            {
                foreach (var idMenu in MutilMenu)
                {
                    TblConnectImages TblConnectImages = new TblConnectImages();
                    TblConnectImages.IdCate = idMenu;
                    TblConnectImages.IdImg = idimg;
                    db.TblConnectImages.Add(TblConnectImages);
                    db.SaveChanges();
                }
            }
            if (mutilManu != null)
            {
                foreach (var idMenu in mutilManu)
                {
                    TblConnectManuToImages connectimage = new TblConnectManuToImages();
                    connectimage.IdManu = idMenu;
                    connectimage.IdImage = idimg;
                    db.TblConnectManuToImages.Add(connectimage);
                    db.SaveChanges();
                }
            }
             if (collection["btnSave"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã thêm thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/Imagesad/Index?idCate=" + idCate + "");
            }
            if (collection["btnSaveCreate"] != null)
            {
                Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                return Redirect("/Imagesad/Create?id=" + idCate + "");
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
                TblImage TblImage = db.TblImage.Find(id);
                var menuModel = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlist.Clear();
                string strReturn = "---";
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListFor(item.Id, carlist, strReturn);
                    strReturn = "---";
                }
                var ListManu = db.TblConnectImages.Where(p => p.IdImg == id).ToList();
                List<int> mang = new List<int>();
                for (int i = 0; i < ListManu.Count; i++)
                {
                    mang.Add(int.Parse(ListManu[i].IdCate.ToString()));
                }
                ViewBag.MutilMenu = new MultiSelectList(carlist, "Value", "Text", mang);
                var Manufacture = db.TblManufactures.Where(m => m.Active == true).OrderBy(m => m.Ord).ToList();
                var listIdManu = db.TblConnectManuToImages.Where(p => p.IdImage == id).Select(p => p.IdManu).ToList();
                var lstmanu = new List<SelectListItem>();

                foreach (var item in Manufacture)
                {
                    lstmanu.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
                ViewBag.mutilManu = new MultiSelectList(lstmanu, "Value", "Text", listIdManu);
                int idCate = int.Parse(TblImage.IdCate.ToString());

                var menuModels = db.TblGroupImage.Where(m => m.Active == true).OrderBy(m => m.Ord).ToList();
                var lstMenus = new List<SelectListItem>();
                lstMenus.Clear();
                foreach (var menu in menuModels)
                {
                    lstMenus.Add(new SelectListItem { Text = menu.Name, Value = menu.Id.ToString() });
                }
                ViewBag.drMenu = new SelectList(lstMenus, "Value", "Text", idCate);
                if (TblImage == null)
                {
                    return HttpNotFound();
                }
               
                return View(TblImage);
            }
            else
            {
                return Redirect("/Users/Erro");
            }
        }

        [HttpPost]
        public ActionResult Edit(TblImage TblImage, int id, FormCollection collection, int[] MutilMenu, int[] mutilManu)
        {
            if (ModelState.IsValid)
            {
                TblImage.IdCate = int.Parse(collection["drMenu"]);
                int idcate = int.Parse(collection["drMenu"]);
                db.Entry(TblImage).State = EntityState.Modified;
                db.SaveChanges();
                var ListImages = db.TblConnectImages.Where(p => p.IdImg == id).ToList();
                for (int i = 0; i < ListImages.Count; i++)
                {
                    db.TblConnectImages.Remove(ListImages[i]);
                    db.SaveChanges();
                }
                if (MutilMenu != null)
                {
                    foreach (var idCates in MutilMenu)
                    {
                        TblConnectImages tbllistimages = new TblConnectImages();
                        tbllistimages.IdCate = idCates;
                        tbllistimages.IdImg = id;
                        db.TblConnectImages.Add(tbllistimages);
                        db.SaveChanges();
                    }
                }
                var listIdManu = db.TblConnectManuToImages.Where(p => p.IdImage == id).ToList();
                for (int i = 0; i < listIdManu.Count; i++)
                {
                    db.TblConnectManuToImages.Remove(listIdManu[i]);
                    db.SaveChanges();
                }
                if (mutilManu != null)
                {
                    foreach (var idMenu in mutilManu)
                    {
                        TblConnectManuToImages connectimage = new TblConnectManuToImages();
                        connectimage.IdManu = idMenu;
                        connectimage.IdImage = id;
                        db.TblConnectManuToImages.Add(connectimage);
                        db.SaveChanges();
                    }
                }
                #region[Updatehistory]
                 #endregion
                if (collection["btnSave"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa  thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                    return Redirect("/Imagesad/Index?idCate=" + idcate + "");
                }
                if (collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm thành công, mời bạn thêm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/Imagesad/Create?id=" + idcate + "");
                }
            }
            return View(TblImage);
        }
    }
}