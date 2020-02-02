using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geyser.Models;
using PagedList;
using PagedList.Mvc;
using System.Globalization;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
 using System.Runtime.InteropServices;

namespace Geyser.Controllers.Admin.Product
{
    public class ProductadController : Controller
    {
        List<SelectListItem> carlist = new List<SelectListItem>();
        
                    List<SelectListItem> carlistGroup = new List<SelectListItem>();

        private GeyserContext db = new GeyserContext();
        // GET: Productad
        public ActionResult Index(int? page, string text, string idCate, string pageSizes, FormCollection collection)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(4, 0, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                #region[Load Menu]

                var pro = db.TblGroupProduct.OrderByDescending(p => p.Ord).Take(1).ToList();
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
                                if (ClsCheckRole.CheckQuyen(4, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
                                {
                                    int id = Convert.ToInt32(key.Remove(0, 4));
                                    TblProduct TblProduct = db.TblProduct.Find(id);
                                    int ord = int.Parse(TblProduct.Ord.ToString());
                                    int idCates = int.Parse(TblProduct.IdCate.ToString());
                                    var kiemtra = db.TblProduct.Where(p => p.Ord > ord && p.IdCate == idCates).ToList();
                                    if (kiemtra.Count > 0)
                                    {
                                        var listproduct = db.TblProduct.Where(p => p.Ord > ord && p.IdCate == idCates).ToList();
                                        for (int i = 0; i < listproduct.Count; i++)
                                        {
                                            int idp = int.Parse(listproduct[i].Id.ToString());
                                            var ProductUpdate = db.TblProduct.Find(idp);
                                            ProductUpdate.Ord = ProductUpdate.Ord - 1;
                                            db.SaveChanges();
                                        }
                                    }
                                    db.TblProduct.Remove(TblProduct);
                                    db.SaveChanges();
                                    clsSitemap.DeteleSitemap(id.ToString(), "Product");

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

        public PartialViewResult PartialProductData(int? page, string text, string idCate, string pageSizes)
        {
            var listProduct = db.TblProduct.OrderByDescending(p => p.DateCreate).ToList();
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
                    ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + pageSize + "</span> / <span style='color: #333;'>" + listProduct.Count.ToString() + "</span>";
                    return PartialView("PartialProductData", listProduct.ToPagedList(pageNumber, pageSize));

                }
                if (text != null && text != "")
                {
                    listProduct = db.TblProduct.Where(p => p.Name.ToUpper().Contains(text.ToUpper())).OrderByDescending(p => p.DateCreate).ToList();
                    ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + listProduct.Count + "</span> ";

                    return PartialView("PartialProductData", listProduct.ToPagedList(pageNumber, pageSize));
                }
                if (idCate != null && idCate != "")
                {
                    ViewBag.IdCate = idCate;
                    idCatelogy = int.Parse(idCate);
                    listProduct = db.TblProduct.Where(p => p.IdCate == idCatelogy).OrderByDescending(p => p.DateCreate).ToList();
                    ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + listProduct.Count + "</span> ";
                    ViewBag.IdMenu = idCate;
                    return PartialView("PartialProductData", listProduct.ToPagedList(pageNumber, pageSize));
                }
                 
                else
                {
                    listProduct = db.TblProduct.OrderByDescending(p => p.Ord).ToList();

                }

            }



            if (pageSizes != null)
            {
                ViewBag.pageSizes = pageSizes;
                pageSize = int.Parse(pageSizes.ToString());

            }
            ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + pageSize + "</span> / <span style='color: #333;'>" + listProduct.Count.ToString() + "</span>";

            var menuModel = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
            carlist.Clear();
            string strReturn = "---";
            foreach (var item in menuModel)
            {
                carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                StringClass.DropDownListFor(item.Id, carlist, strReturn);
                strReturn = "---";
            }
            if (text != null && text != "")
            {
                listProduct = db.TblProduct.Where(p => p.Name.ToUpper().Contains(text.ToUpper()) ).OrderByDescending(p => p.DateCreate).ToList();
                ViewBag.chuoicout = "<span style='color: #A52A2A;'>" + listProduct.Count + "</span> ";
                ViewBag.Text = text;
                return PartialView("PartialProductData", listProduct.ToPagedList(pageNumber, pageSize));
            }
            if (idCate != null && idCate != "")
            {

                int idcates = int.Parse(idCate);
                listProduct = db.TblProduct.Where(p => p.IdCate == idcates ).OrderByDescending(p => p.DateCreate).ToList();
                ViewBag.IdMenu = idCate;
                ViewBag.IdCate = idCate;
                ViewBag.ddlMenu = carlist;
                return PartialView(listProduct.ToPagedList(pageNumber, pageSize));


            }
            else
            {
                ViewBag.ddlMenu = carlist;
                listProduct = db.TblProduct.OrderByDescending(p => p.DateCreate).ToList();
                return PartialView(listProduct.ToPagedList(pageNumber, pageSize));
            }

            return PartialView(listProduct.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UpdateInfoProduct(string code,string productid,string chkPri, string price, string saleprice, string cbIsActive, string chkHome, string chkSale, string ordernumber, string idCate, string Status)
        {
            if (ClsCheckRole.CheckQuyen(4, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                int id = int.Parse(productid);
                var Product = db.TblProduct.Find(id);
                Product.Status = bool.Parse(Status);
                Product.Price = int.Parse(price);
                Product.PriceSale = int.Parse(saleprice);
                Product.Code = code;
                Product.ViewHomes = bool.Parse(chkHome);
                Product.Active = bool.Parse(cbIsActive);
                Product.ProductSale = bool.Parse(chkSale);
                Product.IdCate = int.Parse(idCate);
                int idcates = int.Parse(idCate); 
                int Ord = int.Parse(ordernumber);
                var Kiemtra = db.TblProduct.Where(p => p.Ord == Ord && p.IdCate == idcates && p.Id!=id).ToList();
                if (Kiemtra.Count > 0)
                {
                    var listProduct = db.TblProduct.Where(p => p.Ord >= Ord && p.IdCate == idcates).ToList();   
                    for(int i=0;i<listProduct.Count;i++)
                    {
                        int idp = int.Parse(listProduct[i].Id.ToString());
                        var productUpdate = db.TblProduct.Find(idp);
                        productUpdate.Ord = productUpdate.Ord + 1;
                        db.SaveChanges();
                    }
                }
                Product.Ord = Ord;
                db.SaveChanges();
                //db.Entry(Product).State = System.Data.EntityState.Modified;
                var result = string.Empty;
                result = "Thành công";
                if(chkPri=="true")
                {
                    var getHtmlWeb = new HtmlWeb();
                    int idcate = int.Parse(idCate);
                    var listconnectweb = db.TblConnectWebs.Where(p => p.IdCate == idcate).ToList();
                    List<int> Mang = new List<int>();
                    for (int i = 0; i < listconnectweb.Count; i++)
                    {
                        Mang.Add(int.Parse(listconnectweb[i].IdWeb.ToString()));
                    }
                    var listweb = db.TblWeb.Where(p => Mang.Contains(p.Id)).ToList();
                    for (int i = 0; i < listweb.Count; i++)
                    {

                        getHtmlWeb.Load("" + listweb[i].Url + "/Productad/UpdateProduct?Code=" + code + "&Price=" + price + "&PriceSale=" + saleprice + "&Active=" + cbIsActive + "&Status=" + Status + "");

                    }

                }
                return Json(new { result = result });
            }
            else
            {
                var result = string.Empty;
                result = "Bạn không có quyền truy cập tính năng này";
                return Json(new { result = result });
            }
        }
        public ActionResult EditPrice(string chkPri)
        {
            var result = string.Empty;
            if(chkPri=="true")
            { Session["Price"] = "1";
            result = "Bạn đã chọn thay đổi giá tại trang con";

            }
            else
            { Session["Price"] = "0";
            result = "Bạn đã tắt thay đổi giá ở trang con";
            }
            
           
            return Json(new { result = result });
        }
        public ActionResult UpdateTime(string id)
        {
            var result = string.Empty;
            if (ClsCheckRole.CheckQuyen(4, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                int idp = int.Parse(id);
                var Product = db.TblProduct.Find(idp);
                Product.DateCreate = DateTime.Now;
                db.SaveChanges();
                result = "Làm mới thành công";
            }
            else
                result = "Bạn không có quyền thay đổi chức năng này !";
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
            if (ClsCheckRole.CheckQuyen(4, 1, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
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

                if (id != "")
                {
                    int ids = int.Parse(id);

                    var pro = db.TblProduct.Where(p => p.IdCate == ids).OrderByDescending(p => p.Ord).Take(1).ToList();

                    ViewBag.drMenu = new SelectList(carlist, "Value", "Text", id);
                    int idcate = int.Parse(id.ToString());
                    pro = db.TblProduct.Where(p => p.IdCate == idcate).OrderByDescending(p => p.Ord).Take(1).ToList();
                    if (pro.Count > 0)
                        ViewBag.Ord = pro[0].Ord + 1;
                    else
                        ViewBag.Ord = "1";
                    int idCates = int.Parse(id);
                    var Listconnectcre = db.TblGroupCriteria.Where(p => p.IdCate == idCates).ToList();
                    List<int> Mang = new List<int>();
                    for (int i = 0; i < Listconnectcre.Count; i++)
                    {
                        Mang.Add(int.Parse(Listconnectcre[i].IdCri.ToString()));
                    }

                    var listCre = db.TblCriteria.Where(p => Mang.Contains(p.Id)).ToList();
                    string chuoi = "";
                    for (int i = 0; i < listCre.Count; i++)
                    {

                        chuoi += "<tr>";
                        chuoi += "<td class=\"key\">" + listCre[i].Name + "";
                        chuoi += "</td>";
                        chuoi += "<td>";
                        int idcre = int.Parse(listCre[i].Id.ToString());
                        var listInfo = db.TblInfoCriteria.Where(p => p.IdCri == idcre && p.Active == true).OrderBy(p => p.Ord).ToList();
                        for (int j = 0; j < listInfo.Count; j++)
                        {

                            chuoi += " <div class=\"boxcheck\">";
                            chuoi += "<label><input type=\"checkbox\" name=\"chkCre_" + listInfo[j].Id + "\" id=\"chkCre_" + listInfo[j].Id + "\" />" + listInfo[j].Name + "</label>";
                            chuoi += "</div>";
                        }


                        chuoi += "</td>";
                        chuoi += "</tr>";
                    }
                    ViewBag.chuoi = chuoi;
                }
                else
                {
                    ViewBag.drMenu = carlist;
                    var pro = db.TblProduct.OrderByDescending(p => p.Ord).Take(1).ToList();
                    if (pro.Count > 0)
                        ViewBag.Ord = pro[0].Ord + 1;
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

        public ActionResult Create(TblProduct TblProduct, int[] MutilMenuGroup, FormCollection Collection, string id, List<HttpPostedFileBase> uploadFile)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }

           
            string nidCate = Collection["drMenu"];
            if (nidCate != "")
            {
                TblProduct.IdCate = int.Parse(nidCate);
                int idcate = int.Parse(nidCate);
                TblProduct.DateCreate = DateTime.Now;
                TblProduct.Tag = StringClass.NameToTag(TblProduct.Name);
                TblProduct.DateCreate = DateTime.Now;
                TblProduct.Visit = 0;
                string capacity = Collection["Capacity"];     
                string[] listarray = TblProduct.ImageLinkDetail.Split('/');
                string ImageLinkDetail = Collection["ImageLinkDetail"];
                string imagethum = listarray[listarray.Length - 1];
                TblProduct.ImageLinkThumb = "/Images/_thumbs/Images/" + imagethum;
                db.TblProduct.Add(TblProduct);
                db.SaveChanges();
                var listprro = db.TblProduct.OrderByDescending(p => p.Id).Take(1).ToList();
                clsSitemap.CreateSitemap(TblProduct.Tag+".html", listprro[0].Id.ToString(), "Product");
                #region[Updatehistory]
                 #endregion
                var listproduct = db.TblProduct.OrderByDescending(p => p.Id).Take(1).ToList();
                int idp = 0;
                if (listproduct.Count > 0)
                    idp = int.Parse(listproduct[0].Id.ToString());
                TempData["Msg"] = "";
                string abc = "";
                string def = "";
                if (uploadFile != null)
                {
                    foreach (var item in uploadFile)
                    {
                        if (item != null)
                        {
                            string filename = item.FileName;
                            string path = System.IO.Path.Combine(Server.MapPath("~/Images/ImagesList"), System.IO.Path.GetFileName(item.FileName));
                            item.SaveAs(path);
                            abc = string.Format("Upload {0} file thành công", uploadFile.Count);
                            def += item.FileName + "; ";
                            TblImageProduct imgp = new TblImageProduct();
                            imgp.IdProduct = idp;
                            imgp.Images = "/Images/ImagesList/" + System.IO.Path.GetFileName(item.FileName);
                            db.TblImageProduct.Add(imgp);
                            db.SaveChanges();
                        }

                    }
                    TempData["Msg"] = abc + "</br>" + def;
                }

             
                //Thêm thuộc tính
                foreach (string key in Request.Form.Keys)
                {
                    var checkbox = "";
                    if (key.StartsWith("chkCre_"))
                    {
                        checkbox = Request.Form["" + key];
                        if (checkbox != "false")
                        {
                            int IdCri = Convert.ToInt32(key.Remove(0, 7));
                            TblConnectCriteria tblconnectcre = new TblConnectCriteria();
                            tblconnectcre.IdCre = IdCri;
                            tblconnectcre.Idpd = idp;
                            db.TblConnectCriteria.Add(tblconnectcre);
                            db.SaveChanges();

                        }
                    }
                }
                //thêm mới tabs
                string nkeyword = TblProduct.Tabs ;
                string[] mangKeyword = nkeyword.Split(',');
                for (int i = 0; i < mangKeyword.Length; i++)
                {
                    if (mangKeyword[i] != null && mangKeyword[i] != "")
                    {
                        TblProductTag TblProductTag = new TblProductTag();
                        TblProductTag.Idp = idp;
                        TblProductTag.Name = mangKeyword[i];
                        TblProductTag.Tag = StringClass.NameToTag(mangKeyword[i]);
                        db.TblProductTag.Add(TblProductTag);
                        db.SaveChanges();
                    }
                }
                if (MutilMenuGroup != null)
                {
                    foreach (var item in MutilMenuGroup)
                    {
                        TblConnectProductToGroup conntect = new TblConnectProductToGroup();
                        conntect.Idp = int.Parse(id);
                        conntect.Idg = item;
                        db.TblConnectProductToGroup.Add(conntect);
                        db.SaveChanges();
                    }
                }
                if (Collection["btnSave"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã thêm sản phẩm thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/Productad/index?idCate=" + nidCate + "");
                }
                if (Collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm sản phẩm thành công, mời bạn thêm sản phẩm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/Productad/Create?id=" + nidCate + "");
                }
               
            }
            return View();
        }
        public PartialViewResult ListImages(int id)
        {
            var listImages = db.TblImageProduct.Where(p => p.IdProduct == id).ToList();
            string chuoi = "";
            for (int i = 0; i < listImages.Count; i++)
            {
                chuoi += " <div class=\"Tear_Images\">";
                chuoi += " <img src=\"" + listImages[i].Images + "\" alt=\"\"/>";
                chuoi += " <input type=\"checkbox\" name=\"chek-" + listImages[i].Id + "\" id=\"chek-" + listImages[i].Id + "\" /> Xóa";
                chuoi += "</div>";

            }
            ViewBag.chuoi = chuoi;
            return PartialView();

        }
        public ActionResult AutoOrd(string idCate)
        {

            int id = int.Parse(idCate);
            var ListProduct = db.TblProduct.Where(p => p.IdCate == id).OrderByDescending(p => p.Ord).Take(1).ToList();
            var result = string.Empty;
            if (ListProduct.Count > 0)
            {
                int stt = int.Parse(ListProduct[0].Ord.ToString()) + 1;
                result = stt.ToString();
            }
            else
            {
                result = "0";

            }
            return Json(new { result = result });
        }
        public ActionResult AutoCriteria(string idCate)
        {
            var result = string.Empty;

            int idCates = int.Parse(idCate);
            var Listconnectcre = db.TblGroupCriteria.Where(p => p.IdCate == idCates).ToList();
            List<int> Mang = new List<int>();
            for (int i = 0; i < Listconnectcre.Count; i++)
            {
                Mang.Add(int.Parse(Listconnectcre[i].IdCri.ToString()));
            }

            var listCre = db.TblCriteria.Where(p => Mang.Contains(p.Id)).ToList();
            string chuoi = "";
            for (int i = 0; i < listCre.Count; i++)
            {

                chuoi += "<tr>";
                chuoi += "<td class=\"key\">" + listCre[i].Name + "";
                chuoi += "</td>";
                chuoi += "<td>";
                int idcre = int.Parse(listCre[i].Id.ToString());
                var listInfo = db.TblInfoCriteria.Where(p => p.IdCri == idcre && p.Active == true).OrderBy(p => p.Ord).ToList();
                for (int j = 0; j < listInfo.Count; j++)
                {

                    chuoi += " <div class=\"boxcheck\">";
                    chuoi += "<input type=\"checkbox\" name=\"chkCre_" + listInfo[j].Id + "\" id=\"chkCre_" + listInfo[j].Id + "\" />" + listInfo[j].Name + "";
                    chuoi += "</div>";
                }


                chuoi += "</td>";
                chuoi += "</tr>";
            }
             result = chuoi;
            return Json(new { result = result });
        }
        public ActionResult AutoCriteriaEdit(string idCate,string id)
        {
            var result = string.Empty;

            int idCates = int.Parse(idCate);
            var Listconnectcre = db.TblGroupCriteria.Where(p => p.IdCate == idCates).ToList();
            List<int> Mang = new List<int>();
            for (int i = 0; i < Listconnectcre.Count; i++)
            {
                Mang.Add(int.Parse(Listconnectcre[i].IdCri.ToString()));
            }
            int idp = int.Parse(id);
            var listCre = db.TblCriteria.Where(p => Mang.Contains(p.Id)).ToList();
            string chuoi = "";
            for (int i = 0; i < listCre.Count; i++)
            {

                chuoi += "<tr>";
                chuoi += "<td class=\"key\">" + listCre[i].Name + "";
                chuoi += "</td>";
                chuoi += "<td>";
                int idcre = int.Parse(listCre[i].Id.ToString());
                var listInfo = db.TblInfoCriteria.Where(p => p.IdCri == idcre && p.Active == true).OrderBy(p => p.Ord).ToList();
                for (int j = 0; j < listInfo.Count; j++)
                {

                    int idchk = int.Parse(listInfo[j].Id.ToString());

                    var listCheck = db.TblConnectCriteria.Where(p => p.IdCre == idchk && p.Idpd == idp).ToList();
                    chuoi += " <div class=\"boxcheck\">";
                    if (listCheck.Count > 0)
                    {
                        chuoi += "<input type=\"checkbox\" name=\"chkCre_" + listInfo[j].Id + "\" checked=\"checked\" id=\"chkCre_" + listInfo[j].Id + "\" />" + listInfo[j].Name + "";
                    }
                    else
                    {
                        chuoi += "<input type=\"checkbox\" name=\"chkCre_" + listInfo[j].Id + "\"   id=\"chkCre_" + listInfo[j].Id + "\" />" + listInfo[j].Name + "";
                    }
                    chuoi += "</div>";
                }


                chuoi += "</td>";
                chuoi += "</tr>";
            }
            result = chuoi;
            return Json(new { result = result });
        } 
        public string CheckProduct(string text)
        {
            string chuoi = "";
            var listProduct = db.TblProduct.Where(p => p.Name == text).ToList();
            if (listProduct.Count > 0)
            {
                chuoi = "Sản phẩm bị trùng lặp !";

            }
            Session["Check"] = listProduct.Count;
            return chuoi;
        }
        public ActionResult Edit(int? id)
        {
            if ((Request.Cookies["Username"] == null))
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            if (ClsCheckRole.CheckQuyen(4, 2, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                Session["id"] = id.ToString();
                Int32 ids = Int32.Parse(id.ToString());
                TblProduct TblProduct = db.TblProduct.Find(ids);
                if (TblProduct == null)
                {
                    return HttpNotFound();
                }
                var menuModel = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlist.Clear();
                string strReturn = "---";
                foreach (var item in menuModel)
                {
                    carlist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    StringClass.DropDownListFor(item.Id, carlist, strReturn);
                    strReturn = "---";
                }
                int idGroups = 0;
                if (TblProduct.IdCate != null)
                {
                    idGroups = int.Parse(TblProduct.IdCate.ToString());
                }

                ViewBag.drMenu = new SelectList(carlist, "Value", "Text", idGroups);
                int idCates = int.Parse(TblProduct.IdCate.ToString());
                var Listconnectcre = db.TblGroupCriteria.Where(p => p.IdCate == idCates).ToList();
                List<int> Mang = new List<int>();
                for (int i = 0; i < Listconnectcre.Count; i++)
                {
                    Mang.Add(int.Parse(Listconnectcre[i].IdCri.ToString()));
                }

                var listCre = db.TblCriteria.Where(p => Mang.Contains(p.Id)).ToList();
                string chuoi = "";
                for (int i = 0; i < listCre.Count; i++)
                {

                    chuoi += "<tr>";
                    chuoi += "<td class=\"key\">" + listCre[i].Name + "";
                    chuoi += "</td>";
                    chuoi += "<td>";
                    int idcre = int.Parse(listCre[i].Id.ToString());
                    var listInfo = db.TblInfoCriteria.Where(p => p.IdCri == idcre && p.Active == true).OrderBy(p => p.Ord).ToList();
                    for (int j = 0; j < listInfo.Count; j++)
                    {
                        int idchk=int.Parse(listInfo[j].Id.ToString());
                        var listCheck = db.TblConnectCriteria.Where(p => p.IdCre == idchk && p.Idpd == ids).ToList();
                        chuoi += " <div class=\"boxcheck\">";
                        if(listCheck.Count>0)
                        {
                            chuoi += "<label><input type=\"checkbox\" name=\"chkCre_" + listInfo[j].Id + "\" checked=\"checked\" id=\"chkCre_" + listInfo[j].Id + "\" />" + listInfo[j].Name + "</label>";
                        }
                        else
                        {
                            chuoi += "<label><input type=\"checkbox\" name=\"chkCre_" + listInfo[j].Id + "\"   id=\"chkCre_" + listInfo[j].Id + "\" />" + listInfo[j].Name + "</label>";
                        }
                        chuoi += "</div>";
                    }


                    chuoi += "</td>";
                    chuoi += "</tr>";
                }
                ViewBag.chuoi = chuoi;
                var MenuGroup = db.TblGroupProduct.Where(m => m.ParentId == null).OrderBy(m => m.Id).ToList();
                carlistGroup.Clear();
                var listIdGroup = db.TblConnectProductToGroup.Where(p => p.Idp == id).Select(p => p.Idg).ToList();
                List<int> mang = new List<int>();
                for (int i = 0; i < listIdGroup.Count; i++)
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
                return View(TblProduct);
            }
            else
            {
                return Redirect("/Users/Erro"); 

            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TblProduct TblProduct, int[] MutilMenuGroup, FormCollection collection, int? id, List<HttpPostedFileBase> uploadFile)
        {

            if (ModelState.IsValid)
            {
                if (collection["drMenu"] != "" || collection["drMenu"] != null)
                {
                    if (id == null)
                    {
                        id = int.Parse(collection["idProduct"]);
                        TblProduct = db.TblProduct.Find(id);
                    }
                    else
                    {
                        TblProduct = db.TblProduct.Find(id);
                    }
                    ViewBag.Id = id;
                    int idCate = int.Parse(collection["drMenu"]);
                    TblProduct.IdCate = idCate;
                    TblProduct.DateCreate = DateTime.Now;
                    string tag = TblProduct.Tag;
                    string Name = collection["Name"];
                    string Code = collection["Code"];
                    string Size = collection["Size"];
                    string Description = collection["Description"];
                    string Info = collection["Info"];

                    string Content = collection["Content"];
                    string Parameter = collection["Parameter"];
                    string ImageLinkDetail = collection["ImageLinkDetail"];
                    string[] listarray = ImageLinkDetail.Split('/');
                    string imagethum = listarray[listarray.Length - 1];
                    TblProduct.ImageLinkThumb = "/Images/_thumbs/Images/" + imagethum;
                    string ImageSale = collection["ImageSale"];
                    if (collection["Price"] != null)
                    {
                        float Price = float.Parse(collection["Price"]);
                        TblProduct.Price = Price;

                    }
                    if (collection["PriceSale"] != null)
                    {
                        float PriceSale = float.Parse(collection["PriceSale"]);
                        TblProduct.PriceSale = PriceSale;
                    }
                    string Warranty = collection["Warranty"];
                    string Address = collection["Address"];
                    string Sale = collection["Sale"];
                    int Ord=0;
                    if (collection["Ord"] != null)
                    {
                         Ord= int.Parse(collection["Ord"]);
                        TblProduct.Ord = Ord;
                    }
                    bool URL = (collection["URL"] == "false") ? false : true;// 
                    bool ProductSale = (collection["ProductSale"] == "false") ? false : true;//
                    bool Vat = (collection["Vat"] == "false") ? false : true;//
                    bool Status = (collection["Status"] == "false") ? false : true;//
                    bool Active = (collection["Active"] == "false") ? false : true;
                    bool New = (collection["New"] == "false") ? false : true;//
                    bool ViewHomes = (collection["ViewHomes"] == "false") ? false : true;//
                    bool Priority = (collection["Priority"] == "false") ? false : true;//
                    string Title = collection["Title"];
                    string Video = collection["Video"];
                    string note = collection["Note"];
                    string Keyword = collection["Keyword"];
                    if (TblProduct.Visit != null)
                        TblProduct.Visit = TblProduct.Visit;
                    TblProduct.PriceNote = collection["PriceNote"];
                    TblProduct.NoteInfo = collection["NoteInfo"];
                    TblProduct.Tabs = collection["Tabs"];
                    TblProduct.Time = collection["Time"];
                    TblProduct.Id = int.Parse(TblProduct.Id.ToString());
                    TblProduct.Name = Name;
                    TblProduct.Code = Code;
                    TblProduct.Note = note;
                    TblProduct.ImageSale = ImageSale;
                    TblProduct.Description = Description;
                    TblProduct.ProductSale = ProductSale;
                    TblProduct.Content = Content;
                    TblProduct.Info = Info;
                    TblProduct.Size = Size;
                    TblProduct.Parameter = Parameter;
                    TblProduct.ImageLinkDetail = ImageLinkDetail;
                    TblProduct.Vat = Vat;
                    TblProduct.Warranty = Warranty;
                    TblProduct.Sale = Sale;
                    TblProduct.Active = Active;
                    TblProduct.New = New;
                    TblProduct.Priority = Priority;
                    TblProduct.Status = Status;
                    TblProduct.DateCreate = DateTime.Now;
                    TblProduct.ViewHomes = ViewHomes;
                    TblProduct.Video = Video;
                    TblProduct.Title = Title;
                    TblProduct.Keyword = Keyword;
                    string urls = db.TblGroupProduct.Find(idCate).Tag;
                    if (URL == true)
                    {
                        TblProduct.Tag = StringClass.NameToTag(TblProduct.Name);
                        var GroupProduct = db.TblGroupProduct.Find(idCate);
                        clsSitemap.UpdateSitemap( StringClass.NameToTag(TblProduct.Name) + ".html", id.ToString(), "Product");

                    }
                    else
                    {
                        TblProduct.Tag = collection["NameURL"];
                        var GroupProduct = db.TblGroupProduct.Find(idCate);
                        clsSitemap.UpdateSitemap( StringClass.NameToTag(TblProduct.Name) + ".html", id.ToString(), "Product");
                    }
                    if (Session["Price"] == "1")
                    {
                        var getHtmlWeb = new HtmlWeb();
                         var listconnectweb = db.TblConnectWebs.Where(p => p.IdCate == idCate).ToList();
                        List<int> Mang = new List<int>();
                        for (int i = 0; i < listconnectweb.Count; i++)
                        {
                            Mang.Add(int.Parse(listconnectweb[i].IdWeb.ToString()));
                        }
                        var listweb = db.TblWeb.Where(p => Mang.Contains(p.Id)).ToList();
                        for (int i = 0; i < listweb.Count; i++)
                        {
                            getHtmlWeb.Load("" + listweb[i].Url + "/Productad/UpdateProduct?Code=" + TblProduct.Code + "&Price=" + TblProduct.Price + "&PriceSale=" + TblProduct.PriceSale + "&Active=" + TblProduct.Active + "&Status=" + TblProduct.Status + "");

 
                        }
                    }
                    var Kiemtra = db.TblProduct.Where(p => p.Ord == Ord && p.IdCate == idCate && p.Id != id).ToList();
                    if (Kiemtra.Count > 0)
                    {
                        var listProduct = db.TblProduct.Where(p => p.Ord >= Ord && p.IdCate == idCate).ToList();
                        for (int i = 0; i < listProduct.Count; i++)
                        {
                            int idp = int.Parse(listProduct[i].Id.ToString());
                            var productUpdate = db.TblProduct.Find(idp);
                            productUpdate.Ord = productUpdate.Ord + 1;
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();
                    //thêm tabs
                    var listProductTag = db.TblProductTag.Where(p => p.Idp == id).ToList();
                    for (int i = 0; i < listProductTag.Count; i++)
                    {
                        int ids = listProductTag[i].Id;
                        TblProductTag TblProductTag = db.TblProductTag.Find(ids);
                        db.TblProductTag.Remove(TblProductTag);
                        db.SaveChanges();

                    }
                    string nkeyword = collection["Tabs"]; ;
                    string[] mangKeyword = nkeyword.Split(',');
                    for (int i = 0; i < mangKeyword.Length; i++)
                    {
                        if (mangKeyword[i] != null && mangKeyword[i] != "")
                        {
                            TblProductTag TblProductTag = new TblProductTag();
                            TblProductTag.Idp = id;
                            TblProductTag.Name = mangKeyword[i];
                            TblProductTag.Tag = StringClass.NameToTag(mangKeyword[i]);
                            db.TblProductTag.Add(TblProductTag);
                            db.SaveChanges();
                        }
                    }
                    foreach (string key in Request.Form.Cast<string>().Where(key => key.StartsWith("chek-")))
                    {
                        var checkbox = "";
                        checkbox = Request.Form["" + key];
                        if (checkbox != "false")
                        {
                            Int32 idchk = Convert.ToInt32(key.Remove(0, 5));
                            TblImageProduct image = db.TblImageProduct.Find(idchk);
                            db.TblImageProduct.Remove(image);
                            db.SaveChanges();
                        }
                    }
                    TempData["Msg"] = "";
                 
                    string abc = "";
                    string def = "";
                    if (uploadFile != null)
                    {
                        foreach (var item in uploadFile)
                        {
                            if (item != null)
                            {
                                string filename = item.FileName;
                                string path = System.IO.Path.Combine(Server.MapPath("~/Images/ImagesList"), System.IO.Path.GetFileName(item.FileName));
                                item.SaveAs(path);
                                abc = string.Format("Upload {0} file thành công", uploadFile.Count);
                                def += item.FileName + "; ";
                                TblImageProduct imgp = new TblImageProduct();
                                imgp.IdProduct = id;
                                imgp.Images = "/Images/ImagesList/" + System.IO.Path.GetFileName(item.FileName);
                                db.TblImageProduct.Add(imgp);
                                db.SaveChanges();
                            }

                        }
                        TempData["Msg"] = abc + "</br>" + def;
                    }
                    //Thêm các thuộc tính
                    var listconnectCre = db.TblConnectCriteria.Where(p => p.Idpd == id).ToList();
                    for(int i=0;i<listconnectCre.Count;i++)
                    {
                        db.TblConnectCriteria.Remove(listconnectCre[i]);
                        db.SaveChanges();
                    }

                    foreach (string key in Request.Form.Keys)
                    {
                        var checkbox = "";
                        if (key.StartsWith("chkCre_"))
                        {
                            checkbox = Request.Form["" + key];
                            if (checkbox != "false")
                            {
                                int IdCri = Convert.ToInt32(key.Remove(0, 7));
                                TblConnectCriteria tblconnectcre = new TblConnectCriteria();
                                tblconnectcre.IdCre = IdCri;
                                tblconnectcre.Idpd = id;
                                db.TblConnectCriteria.Add(tblconnectcre);
                                db.SaveChanges();

                            }
                        }
                    }
                    var listId = db.TblConnectProductToGroup.Where(p => p.Idp == id).ToList();
                    for (int i = 0; i < listId.Count; i++)
                    {
                        db.TblConnectProductToGroup.Remove(listId[i]);
                        db.SaveChanges();
                    }
                    if (MutilMenuGroup != null)
                    {
                        foreach (var item in MutilMenuGroup)
                        {
                            TblConnectProductToGroup conntect = new TblConnectProductToGroup();
                            conntect.Idp = id;
                            conntect.Idg = item;
                            db.TblConnectProductToGroup.Add(conntect);
                            db.SaveChanges();
                        }
                    }
                }
                if (collection["btnSave"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info alert1\">Bạn đã sửa  sản phẩm thành công !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";

                    return Redirect("/Productad/Index?idCate=" + int.Parse(collection["drMenu"]));
                }
                if (collection["btnSaveCreate"] != null)
                {
                    Session["Thongbao"] = "<div  class=\"alert alert-info\">Bạn đã thêm   thành công, mời bạn thêm  sản phẩm mới !<button class=\"close\" data-dismiss=\"alert\">×</button></div>";
                    return Redirect("/Productad/Create?id=" + +int.Parse(collection["drMenu"]) + "");
                }
                #region[Updatehistory]
                 #endregion
            }
            return View(TblProduct);
        }
        public ActionResult DeleteProduct(int id)
        {
            if (ClsCheckRole.CheckQuyen(4, 3, int.Parse(Request.Cookies["Username"].Values["UserID"])) == true)
            {
                TblProduct TblProduct = db.TblProduct.Find(id);
                clsSitemap.DeteleSitemap(id.ToString(), "Product");
                var result = string.Empty;
                int ord = int.Parse(TblProduct.Ord.ToString());
                int idCate = int.Parse(TblProduct.IdCate.ToString());
                var kiemtra = db.TblProduct.Where(p => p.Ord > ord && p.IdCate == idCate).ToList();
                if(kiemtra.Count>0)
                {
                    var listproduct = db.TblProduct.Where(p => p.Ord > ord && p.IdCate == idCate).ToList();
                    for(int i=0;i<listproduct.Count;i++)
                    {
                        int idp = int.Parse(listproduct[i].Id.ToString());
                        var ProductUpdate = db.TblProduct.Find(idp);
                        ProductUpdate.Ord = ProductUpdate.Ord - 1;
                        db.SaveChanges();
                    }
                }

                db.TblProduct.Remove(TblProduct);
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
            var listTag = db.TblProduct.Where(p => p.Active == true).ToList();
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
        public ActionResult UpdateProduct(string Code, string Price, string PriceSale, string Active)
        {
            int nPrice = int.Parse(Price);
            int nPriceSale = int.Parse(PriceSale);
            bool nActive = bool.Parse(Active);
            var TblProduct = db.TblProduct.First(p => p.Code == Code);
            TblProduct.Price = nPrice;
            TblProduct.PriceSale = nPriceSale;
            TblProduct.Active = nActive;
            db.SaveChanges();

            var result = string.Empty;
            return Json(new { result = result });
        }
        public ActionResult commandTag()
        {
            var listProduct = db.TblProduct.ToList();
            for(int i=0;i<listProduct.Count;i++)
            {
                int id = listProduct[i].Id;
                try
                {
                    if (listProduct[i].Tabs != null && listProduct[i].Tabs != "")
                    {
                        string[] tabs = listProduct[i].Tabs.Split(',');
                        for (int j = 0; j < tabs.Length; j++)
                        {
                            TblProductTag productTag = new TblProductTag();
                            if (tabs[j] != null && tabs[j] != "")
                            {
                                productTag.Name = tabs[j];
                                productTag.Tag = StringClass.NameToTag(tabs[j]);
                                productTag.Idp = id;
                                db.TblProductTag.Add(productTag);
                                db.SaveChanges();
                            }

                        }


                    }
                }
                catch(Exception ex)
                {

                }

            }
            return View();
        }
    }
}