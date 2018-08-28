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

namespace Geyser.Controllers.Admin.Product
{
    public class GroupProductController : Controller
    {
        // GET: GroupProduct
        private GeyserContext db = new GeyserContext();
         List<SelectListItem> carlist = new List<SelectListItem>();
        List<SelectListItem> carlistGroup     = new List<SelectListItem>();


        public ActionResult Index(string idCate, FormCollection collection)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(4, 0, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                var menuModel = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlist.Clear();
                string strReturn ="---";
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });


                    StringClass.DropDownListFor(item.Id, carlist, strReturn);
                    strReturn = "---";

                 }
                //ViewBag.drMenu = carlist; 
                ViewBag.drMenu = new SelectList(carlist, "Value", "Text", 8);
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
                                if (ClsCheckRole.CheckQuyen(4, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
                                {
                                    int id = Convert.ToInt32(key.Remove(0, 4));
                                    TblGroupProduct TblProduct = db.TblGroupProduct.Find(id);
                                    db.TblGroupProduct.Remove(TblProduct);
                                    db.SaveChanges();
                                    var listProduct = db.TblProduct.Where(p => p.IdCate == id).ToList();
                                    for(int i=0;i<listProduct.Count;i++)
                                    {
                                        db.TblProduct.Remove(listProduct[i]);
                                        db.SaveChanges();
                                    }
                                    clsSitemap.DeteleSitemap(id.ToString(), "GroupProduct");

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
        void UpdateOrder(int idCate,int Ord1, int Ord2)
        {
            if(Ord1!=Ord2)
            {
                var Kiemtra = db.TblGroupProduct.Where(p => p.Active == true && p.ParentId == idCate && p.Ord == Ord2).ToList();
                if(Kiemtra.Count>0)
                {
                    var listGroup = db.TblGroupProduct.Where(p => p.Active == true && p.ParentId == idCate && p.Ord >= Ord2).ToList();

                    if (Ord2 < Ord1)
                    {
                        listGroup = db.TblGroupProduct.Where(p => p.Active == true && p.ParentId == idCate && p.Ord >= Ord2 && p.Ord < Ord1).ToList();

                        for (int i = 0; i < listGroup.Count; i++)
                        {
                            int idMenu = listGroup[i].Id;
                            TblGroupProduct tblgroup = db.TblGroupProduct.Find(idMenu);
                            tblgroup.Ord = tblgroup.Ord + 1;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        listGroup = db.TblGroupProduct.Where(p => p.Active == true && p.ParentId == idCate && p.Ord >Ord1 && p.Ord <= Ord2).ToList();

                        for (int i = 0; i < listGroup.Count; i++)
                        {
                            int idMenu = listGroup[i].Id;
                            TblGroupProduct tblgroup = db.TblGroupProduct.Find(idMenu);
                            tblgroup.Ord = tblgroup.Ord - 1;
                            db.SaveChanges();
                        }
                    }
                }
                
            }
         }
        public PartialViewResult PartialGroupProduct(int? page, string text, string idCate, string pageSizes)
        {
            var listProduct = db.TblGroupProduct.Where(p => p.ParentId == null).OrderBy(p => p.Ord).ToList();
            
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
                ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + pageSize + "</span> / <span style='color: #333;'>" + listProduct.Count.ToString() + "</span>";
                return PartialView(listProduct.ToPagedList(pageNumber, pageSize));

            }
            if (text != null && text != "")
            {
                listProduct = db.TblGroupProduct.Where(p => p.Name.ToUpper().Contains(text.ToUpper()) && p.Active == true).OrderByDescending(p => p.DateCreate).ToList();
                ViewBag.Text = text; 
                return PartialView(listProduct.ToPagedList(pageNumber, pageSize));
                
            }
            if (Request.IsAjaxRequest())
            {
                if (text != null && text != "")
                { 
                        listProduct = db.TblGroupProduct.Where(p => p.Name.ToUpper().Contains(text.ToUpper()) && p.Active == true).OrderByDescending(p => p.DateCreate).ToList();
                        ViewBag.Text = text;
 
                    return PartialView(listProduct.ToPagedList(pageNumber, pageSize));
                }
                 
                if (idCate != "" && idCate != null)
                {
                    int idmenu = int.Parse(idCate);
                    var menucha = db.TblGroupProduct.Find(idmenu);
                     listProduct = db.TblGroupProduct.Where(p => p.ParentId == idmenu).OrderBy(p => p.Ord).ToList();
                    ViewBag.Idcha = idCate;
                    return PartialView(listProduct.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    ViewBag.Idcha = 0;
                }
            }
            if (idCate != "" && idCate != null)
            {
                int idmenu = int.Parse(idCate);
                var menucha = db.TblGroupProduct.Find(idmenu);
                 listProduct = db.TblGroupProduct.Where(p => p.ParentId == idmenu).OrderBy(p => p.Ord).ToList();
                ViewBag.Idcha = idCate;
                return PartialView(listProduct.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.Idcha = 0;
            }
            return PartialView(listProduct.ToPagedList(pageNumber, pageSize));
        }
        public void UpdateLevel(int idCate,int level)
        {
            var listord = db.TblGroupProduct.Where(p => p.ParentId == idCate).ToList();
            for(int i=0;i<listord.Count;i++)
            {
                int id = listord[i].Id;
                var GroupProduct = db.TblGroupProduct.Find(id);
                 db.SaveChanges();
                 int idca=GroupProduct.Id;
             }
        }
        public ActionResult UpdateGroupProduct(string id, string Active, string order, string idCate, string Priority)
        {
            if (ClsCheckRole.CheckQuyen(4, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                int ids = int.Parse(id);
                var GroupProduct = db.TblGroupProduct.Find(ids);
                int idcate1=int.Parse(GroupProduct.Id.ToString());
                int Ord1=int.Parse(GroupProduct.Ord.ToString());
                int Ord2 = int.Parse(order);
                GroupProduct.Active = bool.Parse(Active);
                GroupProduct.Ord = int.Parse(order);

                if (idCate != "")
                {
                    UpdateOrder(int.Parse(idCate), Ord1, Ord2);

                    int idCates=int.Parse(idCate);
                    if(idcate1!=idCates)
                    {
                        var grouppd=db.TblGroupProduct.Find(idCates);
                        var listord = db.TblGroupProduct.Where(p => p.ParentId == idCates).OrderByDescending(p => p.Id).Take(1).ToList();
                        
                        if(listord.Count>0)
                        {
                            string idParent = GroupProduct.ParentId.ToString();
                            if(idParent!=null || idParent!="")
                            {
                                int idPa = int.Parse(idParent);
                                if(idPa!=idCates)
                                {
                                    GroupProduct.Ord = listord[0].Ord + 1;
 
                                } 
                            }
                            else
                            {
                                listord = db.TblGroupProduct.Where(p => p.ParentId == null).OrderByDescending(p => p.Id).Take(1).ToList();
                                GroupProduct.Ord = listord[0].Ord + 1;
                            } 
                        }
                        else
                        {
                            GroupProduct.Ord = 1;
                        }
                        GroupProduct.ParentId = int.Parse(idCate);
                     }
                    else
                    {
                        var listord = db.TblGroupProduct.Where(p => p.ParentId == idCates).OrderByDescending(p => p.Id).Take(1).ToList();

                        if (listord.Count > 0)
                        {
                            string idParent = GroupProduct.ParentId.ToString();
                            if (idParent == null || idParent == "")
                            {
                                int idPa = int.Parse(idParent);
                                if (idPa == idCates)
                                {
                                    GroupProduct.Ord = int.Parse(order);

                                }
                                else
                                {
                                    GroupProduct.Ord = listord[0].Ord + 1;
                                }

                            }


                        }
                    }
                    
                }
                else
                {
 
                     
                   
                    GroupProduct.ParentId = null;


                }
                GroupProduct.Priority = bool.Parse(Priority);
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
            carlist.Clear();
            if (ClsCheckRole.CheckQuyen(4, 1, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                var menuModel = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                string strReturn = "---";
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListFor(item.Id, carlist, strReturn);
                    strReturn = "---";
                }
                ViewBag.drMenu = new SelectList(carlist, "Value", "Text", id);
                
                var pro = db.TblGroupProduct.OrderByDescending(p => p.Ord).Take(1).ToList();
                if(id=="")
                {
                    pro = db.TblGroupProduct.Where(p=>p.ParentId==null).OrderByDescending(p => p.Ord).Take(1).ToList();
                }
                else
                {
                    int ids = int.Parse(id);
                     pro = db.TblGroupProduct.Where(p =>p.ParentId==ids).OrderByDescending(p => p.Ord).ToList();
                }
                if(pro.Count>0)
                {
                    ViewBag.Ord = pro[0].Ord + 1;

                }
                
                else
                {
                    ViewBag.Ord = "1";
                }
                if (Session["Thongbao"] != null && Session["Thongbao"] != "")
                {

                    ViewBag.thongbao = Session["Thongbao"].ToString();
                    Session["Thongbao"] = "";
                }
                var MenuGroup = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlistGroup.Clear();
                string strReturnGrup = "---";
                foreach (var item in MenuGroup)
                {
                    carlistGroup.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListFor(item.Id, carlistGroup, strReturnGrup);
                    strReturnGrup = "---";
                }
                ViewBag.MutilMenuGroup = new MultiSelectList(carlistGroup, "Value", "Text");
                return View();
            }
            else
            {
                return Redirect("/Users/Erro");

            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TblGroupProduct TblGroupProduct, int[] MutilMenuGroup, FormCollection collection)
        {
             
                if ((Request.Cookies["Username"] == null))
                {
                    return RedirectToAction("LoginIndex", "Login");
                }
                string drMenu = collection["drMenu"];
 
                if (drMenu == "")
                {
                    TblGroupProduct.ParentId = null;
                 }
                else
                {
                    var dbLeve = db.TblGroupProduct.Find(int.Parse(drMenu));
                    TblGroupProduct.ParentId = dbLeve.Id;
                 }

                 TblGroupProduct.DateCreate = DateTime.Now;
                string IdUser = Request.Cookies["Username"].Values["UserID"];
                TblGroupProduct.IdUser = int.Parse(IdUser); 
                TblGroupProduct.Tag = StringClass.NameToTag(TblGroupProduct.Name);
                db.TblGroupProduct.Add(TblGroupProduct);
                db.SaveChanges();
           
            var Groups = db.TblGroupProduct.Where(p => p.Active == true).OrderByDescending(p => p.Id).Take(1).ToList();
                string id = Groups[0].Id.ToString(); 
                clsSitemap.CreateSitemap(StringClass.NameToTag(TblGroupProduct.Name), id, "GroupProduct");
            if (MutilMenuGroup != null)
            {
                foreach (var item in MutilMenuGroup)
                {
                    TblConnectGroupProduct conntect = new TblConnectGroupProduct();
                    conntect.Idg = int.Parse(id);
                    conntect.Idc = item;
                    db.TblConnectGroupProduct.Add(conntect);
                    db.SaveChanges();
                }
            }
            if (collection["btnSave"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã thêm danh mục thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                    return Redirect("/GroupProduct/Index?idCate=" + drMenu);
                }
                if (collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm danh mục thành công, mời bạn thêm danh mục sản phẩm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/GroupProduct/Create?id=" + drMenu + "");
                }
               return   Redirect("/GroupProduct/Index?idCate=" + drMenu);
          

         }
        public ActionResult AutoOrd(string idCate)
        {             var result = string.Empty;

            if(idCate!="")
            {
            int id = int.Parse(idCate);
              
            var ListProduct = db.TblGroupProduct.Where(p => p.ParentId==id ).OrderByDescending(p => p.Ord).Take(1).ToList();
          
            if (ListProduct.Count > 0)
            {
                int stt = int.Parse(ListProduct[0].Ord.ToString()) + 1;
                result = stt.ToString();
            }
            else
            {
                result = "0";

            }
            }
            else
            {
                var ListProduct = db.TblGroupProduct.Where(p => p.ParentId==null).OrderByDescending(p => p.Ord).Take(1).ToList();

                int stt = int.Parse(ListProduct[0].Ord.ToString()) + 1;
                result = stt.ToString();
            }
            
             
            return Json(new { result = result });
        }
        public ActionResult Edit(int id)
        {
            carlist.Clear();
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(4, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblGroupProduct TblGroupProduct = db.TblGroupProduct.First(p => p.Id == id);
                if (TblGroupProduct == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id = id;
                var menuName = db.TblGroupProduct.ToList();
                var pro = db.TblGroupProduct.OrderByDescending(p => p.Ord).Take(1).ToList();
                var menuModel = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                string strReturn = "---";
                carlist.Clear();
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListFor(item.Id, carlist, strReturn);
                    strReturn = "---";
                }
                 int idParent=0;
                string kiemtra = TblGroupProduct.ParentId.ToString();
                if (kiemtra!=null && kiemtra!="")
                {
                    idParent = int.Parse(TblGroupProduct.ParentId.ToString());
                     ViewBag.drMenu = new SelectList(carlist, "Value", "Text", idParent);
                } 
                  else 
                      ViewBag.drMenu = carlist;


                var MenuGroup = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlistGroup.Clear();
                var listIdGroup = db.TblConnectGroupProduct.Where(p => p.Idg == id).Select(p => p.Idg).ToList();
                List<int> mang = new List<int>();
for(int i=0;i<listIdGroup.Count;i++)
                {
                    mang.Add(listIdGroup[i].Value);
                }
                string strReturnGrup = "---";
                foreach (var item in MenuGroup)
                {
                    carlistGroup.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListFor(item.Id, carlistGroup, strReturnGrup);
                    strReturnGrup = "---";
                }
                ViewBag.MutilMenuGroup = new MultiSelectList(carlistGroup, "Value", "Text", mang);

                return View(TblGroupProduct);
            }
            else
            {
                return Redirect("/Users/Erro");

            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TblGroupProduct TblGroupProduct, FormCollection collection, int id, int[] MutilMenuGroup)
        {
            if (ModelState.IsValid)
            {
                ////id = int.Parse(collection["idProduct"]);
                ////TblGroupProduct.Id = id;
                ////TblGroupProduct = db.TblGroupProduct.Find(id); 
                string drMenu = collection["drMenu"];
                string levelhiden=collection["Level"];

                if (drMenu == "")
                {
                    TblGroupProduct.ParentId = null;
                     }
                else
                {
                    if (drMenu != id.ToString())
                    {
                        var dbLeve = db.TblGroupProduct.Find(int.Parse(drMenu));
                        TblGroupProduct.ParentId = dbLeve.Id;
                        
                    }
                }
                string IdUser = Request.Cookies["Username"].Values["UserID"];
                TblGroupProduct.IdUser = int.Parse(IdUser);

                bool URL = (collection["URL"] == "false") ? false : true;
                if (URL == true)
                {
                    TblGroupProduct.Tag = StringClass.NameToTag(TblGroupProduct.Name);
                }
                else
                {
                    TblGroupProduct.Tag = collection["NameURL"];
                }
                clsSitemap.UpdateSitemap(TblGroupProduct.Tag , id.ToString(), "GroupProduct");

                TblGroupProduct.DateCreate = DateTime.Now;
                db.Entry(TblGroupProduct).State = EntityState.Modified;
                 db.SaveChanges();
                var listId = db.TblConnectGroupProduct.Where(p => p.Idg == id).ToList();
                for (int i = 0; i< listId.Count;i++)
                {
                    db.TblConnectGroupProduct.Remove(listId[i]);
                    db.SaveChanges();
                }
                if (MutilMenuGroup != null)
                {
                    foreach (var item in MutilMenuGroup)
                    {
                        TblConnectGroupProduct conntect = new TblConnectGroupProduct();
                        conntect.Idg = id;
                        conntect.Idc = item;
                        db.TblConnectGroupProduct.Add(conntect);
                        db.SaveChanges();
                    }
                }
                #region[Updatehistory]
                #endregion
                if (collection["btnSave"] != null)
                {
              
                    if (drMenu=="")
                    {
                        Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa danh mục thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                        return Redirect("/GroupProduct/Index?id=" + drMenu + "");
                    } 
                    else
                    {
                        Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa danh mục thành công  !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
 
                        return Redirect("/GroupProduct/Index?idCate=" + drMenu);
                        
                    }
                }
                if (collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã sửa danh mục thành công, mời bạn thêm danh mục sản phẩm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/GroupProduct/Create?id=" + drMenu + "");
                }
            }
            return Redirect("/GroupProduct/");
        }
        public ActionResult DeleteGroupProduct(int id)
        {
            if (ClsCheckRole.CheckQuyen(4, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblGroupProduct TblProduct = db.TblGroupProduct.Find(id);
                clsSitemap.DeteleSitemap(id.ToString(), "GroupProduct");
                var result = string.Empty;
                db.TblGroupProduct.Remove(TblProduct);
                db.SaveChanges();
                var listProduct = db.TblProduct.Where(p => p.IdCate == id).ToList();
                for (int i = 0; i < listProduct.Count; i++)
                {
                    db.TblProduct.Remove(listProduct[i]);
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
    }
}