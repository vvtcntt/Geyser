using Geyser.models;
using Geyser.Models;
 using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Geyser.Controllers.Display
{
    public class productController : Controller
    {
        // GET: productCustom
        public ActionResult Index()
        {
            return View();
        }

        private GeyserContext db = new GeyserContext();

        private string nUrl = "";

        public string UrlProduct(int idCate)
        {
            var ListMenu = db.TblGroupProduct.Where(p => p.Id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = " <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"/" + ListMenu[i].Tag + "\"> <span itemprop=\"name\">" + ListMenu[i].Name + "</span></a> <meta itemprop=\"position\" content=\"\" /> </li>  " + nUrl;
                string ids = ListMenu[i].ParentId.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentId.ToString());
                    UrlProduct(id);
                }
            }
            return nUrl;
        }

        private List<string> Mangphantu = new List<string>();

        public List<string> Arrayid(int idParent)
        {
            var ListMenu = db.TblGroupProduct.Where(p => p.ParentId == idParent).ToList();

            for (int i = 0; i < ListMenu.Count; i++)
            {
                Mangphantu.Add(ListMenu[i].Id.ToString());
                int id = int.Parse(ListMenu[i].Id.ToString());
                Arrayid(id);
            }

            return Mangphantu;
        }

        public ActionResult productDetail(string tag)
        {
            TblProduct ProductDetail = db.TblProduct.First(p => p.Tag == tag);
            int id = ProductDetail.Id;
            TblGroupProduct tblgroupProduct = db.TblGroupProduct.FirstOrDefault(p => p.Id == ProductDetail.IdCate.Value);
            var tblconfig = db.TblConfig.FirstOrDefault();
            ViewBag.color = tblconfig.Color;
            int idmanu = int.Parse(db.TblConnectManuProduct.FirstOrDefault(p => p.IdCate == tblgroupProduct.Id).IdManu.ToString());
            ViewBag.manuName = db.TblManufactures.FirstOrDefault(p => p.Id == idmanu).Name;
            ViewBag.Manu = "<li><span>Thương hiệu : Geyser</span></li>";
            ViewBag.favicon = " <link href=\"" + tblconfig.Favicon + "\" rel=\"icon\" type=\"image/x-icon\" />";

            ViewBag.Title = "<title>" + ProductDetail.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + ProductDetail.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + ProductDetail.Keyword + "\" /> ";
            ViewBag.imageog = "<meta property=\"og:image\" content=\"http://maylocnuocgeyser.com.vn" + ProductDetail.ImageLinkThumb + "\"/>";
            ViewBag.titleog = "<meta property=\"og:title\" content=\"" + ProductDetail.Title + "\"/> ";
            ViewBag.site_nameog = "<meta property=\"og:site_name\" content=\"" + ProductDetail.Name + "\"/> ";
            ViewBag.urlog = "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\"/> ";
            ViewBag.descriptionog = "<meta property=\"og:description\" content=\"" + ProductDetail.Description + "\" />";
            ViewBag.nUrl = "<ol itemscope itemtype=\"http://schema.org/BreadcrumbList\">   <li itemprop=\"itemListElement\" itemscope  itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"http://maylocnuocgeyser.com.vn\">  <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li>   ›" + UrlProduct(tblgroupProduct.Id) + "</ol> ";

            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://maylocnuocgeyser.com.vn/" + ProductDetail.Tag + ".html\" />";

            StringBuilder schame = new StringBuilder();
            schame.Append("<script type=\"application/ld+json\">");
            schame.Append("{");
            schame.Append("\"@context\": \"http://schema.org\",");
            schame.Append("\"@type\": \"ProductArticle\",");
            schame.Append("\"headline\": \"" + ProductDetail.Description + "\",");
            schame.Append(" \"datePublished\": \"" + ProductDetail.DateCreate + "\",");
            schame.Append("\"image\": [");
            schame.Append(" \"" + ProductDetail.ImageLinkThumb + "\"");
            schame.Append(" ]");
            schame.Append("}");
            schame.Append("</script> ");
            ViewBag.schame = schame.ToString();
            var ListGroupCri = db.TblGroupCriteria.Where(p => p.IdCate == tblgroupProduct.Id).Select(p => p.IdCri).ToList();

            var ListCri = db.TblCriteria.Where(p => ListGroupCri.Contains(p.Id) && p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder chuoi = new StringBuilder();
            #region[Lọc thuộc tính]

            for (int i = 0; i < ListCri.Count; i++)
            {
                int idCre = int.Parse(ListCri[i].Id.ToString());
                var ListCr = (from a in db.TblConnectCriteria
                              join b in db.TblInfoCriteria on a.IdCre equals b.Id
                              where a.Idpd == id && b.IdCri == idCre && b.Active == true
                              select new
                              {
                                  b.Name,
                                  b.Url,
                                  b.Ord
                              }).OrderBy(p => p.Ord).ToList();
                if (ListCr.Count > 0)
                {
                    chuoi.Append("<tr>");
                    chuoi.Append("<td>" + ListCri[i].Name + "</td>");
                    chuoi.Append("<td>");
                    int dem = 0;
                    string num = "";
                    if (ListCr.Count > 1)
                        num = "⊹ ";
                    foreach (var item in ListCr)
                        if (item.Url != null && item.Url != "")
                        {
                            chuoi.Append("<a href=\"" + item.Url + "\" title=\"" + item.Name + "\">");
                            if (dem == 1)
                                chuoi.Append(num + item.Name);
                            else
                                chuoi.Append(num + item.Name);
                            dem = 1;
                            chuoi.Append("</a>");
                        }
                        else
                        {
                            if (dem == 1)
                                chuoi.Append(num + item.Name + "</br> ");
                            else
                                chuoi.Append(num + item.Name + "</br> ");
                            dem = 1;
                        }
                    chuoi.Append("</td>");
                    chuoi.Append(" </tr>");
                }
            }
            ViewBag.chuoi = chuoi.ToString();

            int visit = int.Parse(ProductDetail.Visit.ToString());
            if (visit > 0)
            {
                ProductDetail.Visit = ProductDetail.Visit + 1;
                db.SaveChanges();
            }
            else
            {
                ProductDetail.Visit = ProductDetail.Visit + 1;
                db.SaveChanges();
            }
            var listImages = db.TblImageProduct.Where(p => p.IdProduct == id).ToList();
            StringBuilder chuoiimages = new StringBuilder();
            if (listImages.Count > 0)
            {
                chuoiimages.Append("<li class=\"getImg" + ProductDetail.Id + "\"><a href=\"javascript:;\" onclick=\"javascript:return getImage('" + ProductDetail.ImageLinkDetail + "', 'getImg" + ProductDetail.Id + "')\" title=\"" + ProductDetail.Name + "\"><img src=\"" + ProductDetail.ImageLinkDetail + "\" alt=\"" + ProductDetail.Name + "\" /></a></li>");

                for (int i = 0; i < listImages.Count; i++)
                {
                    chuoiimages.Append("<li class=\"getImg" + listImages[i].Id + "\"><a href=\"javascript:;\" onclick=\"javascript:return getImage('" + listImages[i].Images + "', 'getImg" + listImages[i].Id + "')\" title=\"" + ProductDetail.Name + "\"><img src=\"" + listImages[i].Images + "\" alt=\"" + ProductDetail.Name + "\" /></a></li>");
                }
            }

            ViewBag.chuoiimages = chuoiimages;
            ViewBag.hotline = db.TblConfig.First().HotlineIn;
            //load root
            StringBuilder chuoitag = new StringBuilder();
            if (ProductDetail.Tabs != null)
            {
                string Chuoi = ProductDetail.Tabs;
                string[] Mang = Chuoi.Split(',');
                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {
                    string tagsp = StringClass.NameToTag(Mang[i]);
                    chuoitag.Append("<a href=\"/tag/" + tagsp + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a>");
                }
            }
            ViewBag.chuoitag = chuoitag;
            StringBuilder result = new StringBuilder();
            result.Append("<div class=\"tearListProduct\">");

            result.Append("<div class=\"contentListProductContent\">");
            var listProduct = db.TblProduct.Where(p => p.Active == true && p.IdCate == tblgroupProduct.Id && p.Tag != tag).Take(6).OrderBy(p => p.Ord).ToList();
            for (int j = 0; j < listProduct.Count; j++)
            {
                result.Append("<div class=\"tear\">");
                result.Append("<div class=\"contentTear\">");
                float price = float.Parse(listProduct[j].Price.ToString());
                float pricesale = float.Parse(listProduct[j].PriceSale.ToString());
                float phantram = 100 - ((pricesale * 100) / price);
                result.Append(" <div class=\"sale\">" + Convert.ToInt32(phantram) + "%</div>");
                if (listProduct[j].Note != null && listProduct[j].Note != "")
                    result.Append("<div class=\"noteTear\">" + listProduct[j].Note + "</div>");

                result.Append("<div class=\"img\">");
                result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\"><img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                result.Append(" </div>");
                result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a>");
                result.Append("<div class=\"boxItem\">");
                result.Append("<div class=\"boxPrice\">");
                result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                result.Append("</div>");
                result.Append("<div class=\"boxSale\">");
                result.Append("<a href=\"\" title=\"\"></a>");
                result.Append("</div>");
                result.Append("</div>");
                result.Append(" </div>");
                result.Append("</div>");
            }
            result.Append("</div>");
            result.Append("</div>");
            ViewBag.resultLienquan = result.ToString();
            #endregion

            StringBuilder resultfb = new StringBuilder();
            StringBuilder schemaResult = new StringBuilder();
            var listFeedback = db.TblFeedback.Where(p => p.Active == true && p.IdC == id && p.IdParent == null && p.Type == 1).OrderByDescending(p => p.DateCreate).ToList();
            schemaResult.Append(" <script type=\"application/ld+json\">");

            schemaResult.Append("{");

            schemaResult.Append(" \"@context\": \"https://schema.org\",");
            schemaResult.Append(" \"@type\": \"FAQPage\",");
            schemaResult.Append(" \"mainEntity\": ");
            schemaResult.Append(" [");
            int count = listFeedback.Count;
            foreach (var item in listFeedback)
            {
                count -= 1;
                resultfb.Append(" <div class=\"itemFb\">");
                resultfb.Append("<div class=\"contentItemFb\">");
                resultfb.Append("<div class=\"leftIcon\">");
                resultfb.Append("<i class=\"fas fa-user-tie\"></i>");
                resultfb.Append("</div>");
                resultfb.Append("<div class=\"rightContent\">");
                resultfb.Append("<div class=\"cm" + item.Id + "\">" + item.Content + " </div>");
                int idLikes = 0;
                if (db.TblLike.FirstOrDefault(p => p.IdC == item.Id) != null)
                {
                    idLikes = int.Parse(db.TblLike.FirstOrDefault(p => p.IdC == item.Id).Like.ToString());
                }
                resultfb.Append("<p class=\"account\"><a class=\"rp\" href=\"#txtfeedback\" title=\"" + item.Id + "\"><i class=\"fas fa-reply\"></i> Trả lời</a> <a href=\"#\" title=\"" + item.Id + "\" class=\"like" + item.Id + "\" onclick=\"javascript:return Likes(" + item.Id + "); \"><i class=\"far fa-thumbs-up\" ></i> Like (" + idLikes + ")</a> Gửi bởi <span>" + item.Name + " </span><samp>vào lúc</samp> <time>" + item.DateCreate + "</time></p>");
                resultfb.Append("</div>");
                resultfb.Append("</div>");
                int idParentfb = item.Id;

                if (item.Priority == true)
                {
                    schemaResult.Append(" {");
                    schemaResult.Append(" \"@type\": \"Question\",");
                    schemaResult.Append("\"name\": \"" + item.Content + "\",");
                    var listFeedbackChilds = db.TblFeedback.Where(p => p.Active == true && p.IdParent == idParentfb).OrderByDescending(p => p.DateCreate).Take(1).ToList();
                    if (listFeedbackChilds.Count > 0)
                    {
                        schemaResult.Append(" \"acceptedAnswer\": ");
                        schemaResult.Append("{");
                        schemaResult.Append(" \"@type\": \"Answer\",");
                        schemaResult.Append("\"text\": \"" + listFeedbackChilds[0].Content + "\"");
                        schemaResult.Append("}");
                    }
                    if (count == 0)
                        schemaResult.Append(" } ");
                    else
                    {
                        schemaResult.Append(" }, ");
                    }


                }
                var listFeedbackChild = db.TblFeedback.Where(p => p.Active == true && p.IdParent == idParentfb).OrderByDescending(p => p.DateCreate).ToList();
                if (listFeedbackChild.Count > 0)
                {
                    foreach (var itemChild in listFeedbackChild)
                    {
                        resultfb.Append("<div class=\"replay\">");
                        resultfb.Append("<div class=\"cm" + itemChild.Id + "\">" + itemChild.Content + " </div>");
                        int idLike = 0;
                        if (db.TblLike.FirstOrDefault(p => p.IdC == itemChild.Id) != null)
                        {
                            idLike = int.Parse(db.TblLike.FirstOrDefault(p => p.IdC == itemChild.Id).Like.ToString());
                        }
                        resultfb.Append("<p class=\"account\"><a class=\"rp\" href=\"#txtfeedback\" title=\"" + item.Id + "\"><i class=\"fas fa-reply\"></i> Trả lời</a> <a href=\"#\" class=\"like" + itemChild.Id + "\" onclick=\"javascript:return Likes(" + itemChild.Id + "); \" title=\"" + itemChild.Id + "\" ><i class=\"far fa-thumbs-up\"></i> Like (" + idLike + ")</a> Gửi bởi <span>" + itemChild.Name + " </span><samp>vào lúc</samp> <time>" + itemChild.DateCreate + "</time></p>");
                        resultfb.Append("</div>");
                    }

                }


                resultfb.Append("</div>");
            }
            schemaResult.Append("]");
            schemaResult.Append(" }");
            schemaResult.Append(" </script>");
            ViewBag.schema = schemaResult.ToString();
            ViewBag.resultFeedback = resultfb.ToString();

            return View(ProductDetail);
        }

        public ActionResult productList(string tag)
        {
            try
            {
                TblGroupProduct groupProduct = db.TblGroupProduct.First(p => p.Tag == tag);
                ViewBag.name = groupProduct.Name;
                ViewBag.headshord = groupProduct.Content;
                int idCate = groupProduct.Id;
                ViewBag.check = true;

                var tblconfig = db.TblConfig.FirstOrDefault();

                ViewBag.color = tblconfig.Color;
                ViewBag.favicon = " <link href=\"" + tblconfig.Favicon + "\" rel=\"icon\" type=\"image/x-icon\" />";
                ViewBag.Title = "<title>" + groupProduct.Title + "</title>";
                ViewBag.Description = "<meta name=\"description\" content=\"" + groupProduct.Description + "\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + groupProduct.Keyword + "\" /> ";
                ViewBag.imageog = "<meta property=\"og:image\" content=\"" + groupProduct.Images + "\"/>";
                ViewBag.titleog = "<meta property=\"og:title\" content=\"" + groupProduct.Title + "\"/> ";
                ViewBag.site_nameog = "<meta property=\"og:site_name\" content=\"" + groupProduct.Name + "\"/> ";
                ViewBag.urlog = "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\"/> ";
                ViewBag.descriptionog = "<meta property=\"og:description\" content=\"" + groupProduct.Description + "\" />";
                ViewBag.nUrl = "<ol itemscope itemtype=\"http://schema.org/BreadcrumbList\">   <li itemprop=\"itemListElement\" itemscope  itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"http://maylocnuocgeyser.com.vn\">  <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li>   ›" + UrlProduct(groupProduct.Id) + "</ol> ";

                ViewBag.canonical = "<link rel=\"canonical\" href=\"http://maylocnuocgeyser.com.vn/" + groupProduct.Tag + "\" />";
                StringBuilder result = new StringBuilder();

                var listGroupProduct = db.TblGroupProduct.Where(p => p.ParentId == idCate && p.Active == true).OrderBy(p => p.Ord).ToList();
                if (listGroupProduct.Count > 0)
                {
                    result.Append("<div class=\"saleProduct\">");
                    int idmanu = groupProduct.Id;
                    var listIdImages = db.TblConnectImages.Where(p => p.IdCate == idmanu).Select(p => p.IdImg).ToList();
                    var listImage = db.TblImage.Where(p => p.Active == true && p.IdCate == 4 && listIdImages.Contains(p.Id)).OrderBy(p => p.Ord).ToList();
                    for (int j = 0; j < listImage.Count; j++)
                    {
                        result.Append("<a href=\"" + listImage[j].Url + "\" title=\"" + listImage[j].Name + "\"><img src=\"" + listImage[j].Images + "\" alt=\"" + listImage[j].Name + "\" /></a>");
                    }
                    result.Append("</div>");
                    for (int i = 0; i < listGroupProduct.Count; i++)
                    {
                        int idMenu = listGroupProduct[i].Id;
                        result.Append("<div class=\"tearListProduct\">");

                        result.Append("<div class=\"filter\">");
                        result.Append("<h2 class=\"name\"><a href=\"/" + listGroupProduct[i].Tag + "\" title=\"" + listGroupProduct[i].Name + "\" style=\"color:#088ec1\">" + listGroupProduct[i].Name + " </a> </h2>");
                        result.Append("</div>");
                        result.Append("<div class=\"contentListProductContent\">");
                        var listProduct = db.TblProduct.Where(p => p.Active == true && p.IdCate == idMenu).OrderBy(p => p.Ord).Take(12).ToList();
                        for (int j = 0; j < listProduct.Count; j++)
                        {
                            result.Append("<div class=\"tear\">");
                            result.Append("<div class=\"contentTear\">");
                            float price = float.Parse(listProduct[j].Price.ToString());
                            float pricesale = float.Parse(listProduct[j].PriceSale.ToString());
                            float phantram = 100 - ((pricesale * 100) / price);
                            if (listProduct[j].New == true)
                            {
                                result.Append(" <div class=\"sale\"> Mới 2019</div>");
                            }
                            else
                            {
                                result.Append(" <div class=\"sale\">" + Convert.ToInt32(phantram) + "%</div>");
                            }
                            if (listProduct[j].Note != null && listProduct[j].Note != "")
                                result.Append("<div class=\"noteTear\">" + listProduct[j].Note + "</div>");

                            result.Append("<div class=\"img\">");
                            result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\"><img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                            result.Append(" </div>");
                            result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a>");
                            result.Append("<div class=\"boxItem\">");
                            result.Append("<div class=\"boxPrice\">");
                            result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                            result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                            result.Append("</div>");
                            result.Append("<div class=\"boxSale\">");
                            result.Append("<a href=\"\" title=\"\"></a>");
                            result.Append("</div>");
                            result.Append("</div>");
                            result.Append(" </div>");
                            result.Append("</div>");
                        }
                        var listId = db.TblConnectProductToGroup.Where(p => p.Idg == idMenu).Select(p => p.Idp).ToList();
                        var listProducts = db.TblProduct.Where(p => p.Active == true && listId.Contains(p.Id)).OrderBy(p => p.Ord).ToList();
                        for (int j = 0; j < listProducts.Count; j++)
                        {
                            result.Append("<div class=\"tear\">");
                            result.Append("<div class=\"contentTear\">");
                            float price = float.Parse(listProducts[j].Price.ToString());
                            float pricesale = float.Parse(listProducts[j].PriceSale.ToString());
                            float phantram = 100 - ((pricesale * 100) / price);
                            if (listProducts[j].New == true)
                            {
                                result.Append(" <div class=\"sale\"> Mới 2019</div>");
                            }
                            else
                            {
                                result.Append(" <div class=\"sale\">" + Convert.ToInt32(phantram) + "%</div>");
                            }
                            if (listProducts[j].Note != null && listProducts[j].Note != "")
                                result.Append("<div class=\"noteTear\">" + listProducts[j].Note + "</div>");

                            result.Append("<div class=\"img\">");
                            result.Append("<a href=\"/" + listProducts[j].Tag + ".html\" title=\"" + listProducts[j].Name + "\"><img src=\"" + listProducts[j].ImageLinkThumb + "\" alt=\"" + listProducts[j].Name + "\" /></a>");
                            result.Append(" </div>");
                            result.Append("<a href=\"/" + listProducts[j].Tag + ".html\" title=\"" + listProducts[j].Name + "\" class=\"name\">" + listProducts[j].Name + "</a>");
                            result.Append("<div class=\"boxItem\">");
                            result.Append("<div class=\"boxPrice\">");
                            result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProducts[j].PriceSale) + "đ</span>");
                            if (listProduct[j].PriceNote != null && listProduct[j].PriceNote != "")
                            {
                                result.Append("<span class=\"priceNotes\">" + listProduct[j].PriceNote + "</span>");
                            }
                            result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProducts[j].Price) + "đ</span>");
                            result.Append("</div>");
                            result.Append("<div class=\"boxSale\">");
                            result.Append("<a href=\"\" title=\"\"></a>");
                            result.Append("</div>");
                            result.Append("</div>");
                            result.Append(" </div>");
                            result.Append("</div>");
                        }

                        result.Append("</div>");
                        result.Append("</div>");
                    }
                }
                else
                {
                    result.Append("<div class=\"tearListProduct\">");
                    result.Append("<div class=\"saleProduct\">");
                    int idmanu = groupProduct.Id;
                    var listIdImages = db.TblConnectImages.Where(p => p.IdCate == idmanu).Select(p => p.IdImg).ToList();
                    var listImage = db.TblImage.Where(p => p.Active == true && p.IdCate == 4 && listIdImages.Contains(p.Id)).OrderBy(p => p.Ord).ToList();
                    for (int j = 0; j < listImage.Count; j++)
                    {
                        result.Append("<a href=\"" + listImage[j].Url + "\" title=\"" + listImage[j].Name + "\"><img src=\"" + listImage[j].Images + "\" alt=\"" + listImage[j].Name + "\" /></a>");
                    }
                    result.Append("</div>");
                    result.Append("<div class=\"filter\">");
                    result.Append("<span class=\"name\" style=\"color:#088ec1\">Danh sách sản phẩm " + groupProduct.Name + " : </span>");
                    result.Append("</div>");
                    result.Append("<div class=\"contentListProductContent\">");
                    var listProduct = db.TblProduct.Where(p => p.Active == true && p.IdCate == idmanu).OrderBy(p => p.Ord).ToList();
                    for (int j = 0; j < listProduct.Count; j++)
                    {
                        result.Append("<div class=\"tear\">");
                        result.Append("<div class=\"contentTear\">");
                        float price = float.Parse(listProduct[j].Price.ToString());
                        float pricesale = float.Parse(listProduct[j].PriceSale.ToString());
                        float phantram = 100 - ((pricesale * 100) / price);
                        if (listProduct[j].New == true)
                        {
                            result.Append(" <div class=\"sale\"> Mới 2019</div>");
                        }
                        else
                        {
                            result.Append(" <div class=\"sale\">" + Convert.ToInt32(phantram) + "%</div>");
                        }
                        if (listProduct[j].Note != null && listProduct[j].Note != "")
                            result.Append("<div class=\"noteTear\">" + listProduct[j].Note + "</div>");

                        result.Append("<div class=\"img\">");
                        result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\"><img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                        result.Append(" </div>");
                        result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a>");
                        result.Append("<div class=\"boxItem\">");
                        result.Append("<div class=\"boxPrice\">");
                        result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                        if (listProduct[j].PriceNote != null && listProduct[j].PriceNote != "")
                        {
                            result.Append("<span class=\"priceNotes\">" + listProduct[j].PriceNote + "</span>");
                        }
                        result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                        result.Append("</div>");
                        result.Append("<div class=\"boxSale\">");
                        result.Append("<a href=\"\" title=\"\"></a>");
                        result.Append("</div>");
                        result.Append("</div>");
                        result.Append(" </div>");
                        result.Append("</div>");
                    }
                    var listId = db.TblConnectProductToGroup.Where(p => p.Idg == idmanu).Select(p => p.Idp).ToList();
                    var listProducts = db.TblProduct.Where(p => p.Active == true && listId.Contains(p.Id)).OrderBy(p => p.Ord).ToList();
                    for (int j = 0; j < listProducts.Count; j++)
                    {
                        result.Append("<div class=\"tear\">");
                        result.Append("<div class=\"contentTear\">");
                        float price = float.Parse(listProducts[j].Price.ToString());
                        float pricesale = float.Parse(listProducts[j].PriceSale.ToString());
                        float phantram = 100 - ((pricesale * 100) / price);
                        if (listProducts[j].New == true)
                        {
                            result.Append(" <div class=\"sale\"> Mới 2019</div>");
                        }
                        else
                        {
                            result.Append(" <div class=\"sale\">" + Convert.ToInt32(phantram) + "%</div>");
                        }
                        if (listProducts[j].Note != null && listProducts[j].Note != "")
                            result.Append("<div class=\"noteTear\">" + listProducts[j].Note + "</div>");

                        result.Append("<div class=\"img\">");
                        result.Append("<a href=\"/" + listProducts[j].Tag + ".html\" title=\"" + listProducts[j].Name + "\"><img src=\"" + listProducts[j].ImageLinkThumb + "\" alt=\"" + listProducts[j].Name + "\" /></a>");
                        result.Append(" </div>");
                        result.Append("<a href=\"/" + listProducts[j].Tag + ".html\" title=\"" + listProducts[j].Name + "\" class=\"name\">" + listProducts[j].Name + "</a>");
                        result.Append("<div class=\"boxItem\">");
                        result.Append("<div class=\"boxPrice\">");
                        result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProducts[j].PriceSale) + "đ</span>");
                        if (listProduct[j].PriceNote != null && listProduct[j].PriceNote != "")
                        {
                            result.Append("<span class=\"priceNotes\">" + listProduct[j].PriceNote + "</span>");
                        }
                        result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProducts[j].Price) + "đ</span>");
                        result.Append("</div>");
                        result.Append("<div class=\"boxSale\">");
                        result.Append("<a href=\"\" title=\"\"></a>");
                        result.Append("</div>");
                        result.Append("</div>");
                        result.Append(" </div>");
                        result.Append("</div>");
                    }



                    result.Append("</div>");
                    result.Append("</div>");
                }
                var listIdGroup = db.TblConnectGroupProduct.Where(p => p.Idg == idCate).Select(p => p.Idc).ToList();
                if (listIdGroup.Count > 0)
                {
                    var listGroupProductChild = db.TblGroupProduct.Where(p => p.Active == true && listIdGroup.Contains(p.Id)).OrderBy(p => p.Ord).ToList();
                    for (int i = 0; i < listGroupProductChild.Count; i++)
                    {
                        int idMenu = listGroupProductChild[i].Id;
                        result.Append("<div class=\"tearListProduct\">");

                        result.Append("<div class=\"filter\">");
                        result.Append("<h2 class=\"name\"><a href=\"/" + listGroupProductChild[i].Tag + "\" title=\"\">" + listGroupProductChild[i].Name + " </a> </h2>");
                        result.Append("</div>");
                        result.Append("<div class=\"contentListProductContent\">");
                        var listProduct = db.TblProduct.Where(p => p.Active == true && p.IdCate == idMenu).OrderBy(p => p.Ord).Take(12).ToList();
                        for (int j = 0; j < listProduct.Count; j++)
                        {
                            result.Append("<div class=\"tear\">");
                            result.Append("<div class=\"contentTear\">");
                            float price = float.Parse(listProduct[j].Price.ToString());
                            float pricesale = float.Parse(listProduct[j].PriceSale.ToString());
                            float phantram = 100 - ((pricesale * 100) / price);
                            if (listProduct[j].New == true)
                            {
                                result.Append(" <div class=\"sale\"> Mới 2019</div>");
                            }
                            else
                            {
                                result.Append(" <div class=\"sale\">" + Convert.ToInt32(phantram) + "%</div>");
                            }
                            if (listProduct[j].Note != null && listProduct[j].Note != "")
                                result.Append("<div class=\"noteTear\">" + listProduct[j].Note + "</div>");

                            result.Append("<div class=\"img\">");
                            result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\"><img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                            result.Append(" </div>");
                            result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a>");
                            result.Append("<div class=\"boxItem\">");
                            result.Append("<div class=\"boxPrice\">");
                            result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                            if (listProduct[j].PriceNote != null && listProduct[j].PriceNote != "")
                            {
                                result.Append("<span class=\"priceNotes\">" + listProduct[j].PriceNote + "</span>");
                            }
                            result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                            result.Append("</div>");
                            result.Append("<div class=\"boxSale\">");
                            result.Append("<a href=\"\" title=\"\"></a>");
                            result.Append("</div>");
                            result.Append("</div>");
                            result.Append(" </div>");
                            result.Append("</div>");
                        }
                        var listId = db.TblConnectProductToGroup.Where(p => p.Idg == idMenu).Select(p => p.Idp).ToList();
                        var listProducts = db.TblProduct.Where(p => p.Active == true && listId.Contains(p.Id)).OrderBy(p => p.Ord).ToList();
                        for (int j = 0; j < listProducts.Count; j++)
                        {
                            result.Append("<div class=\"tear\">");
                            result.Append("<div class=\"contentTear\">");
                            float price = float.Parse(listProducts[j].Price.ToString());
                            float pricesale = float.Parse(listProducts[j].PriceSale.ToString());
                            float phantram = 100 - ((pricesale * 100) / price);
                            if (listProducts[j].New == true)
                            {
                                result.Append(" <div class=\"sale\"> Mới 2019</div>");
                            }
                            else
                            {
                                result.Append(" <div class=\"sale\">" + Convert.ToInt32(phantram) + "%</div>");
                            }
                            if (listProducts[j].Note != null && listProducts[j].Note != "")
                                result.Append("<div class=\"noteTear\">" + listProducts[j].Note + "</div>");

                            result.Append("<div class=\"img\">");
                            result.Append("<a href=\"/" + listProducts[j].Tag + ".html\" title=\"" + listProducts[j].Name + "\"><img src=\"" + listProducts[j].ImageLinkThumb + "\" alt=\"" + listProducts[j].Name + "\" /></a>");
                            result.Append(" </div>");
                            result.Append("<a href=\"/" + listProducts[j].Tag + ".html\" title=\"" + listProducts[j].Name + "\" class=\"name\">" + listProducts[j].Name + "</a>");
                            result.Append("<div class=\"boxItem\">");
                            result.Append("<div class=\"boxPrice\">");
                            result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProducts[j].PriceSale) + "đ</span>");
                            if (listProduct[j].PriceNote != null && listProduct[j].PriceNote != "")
                            {
                                result.Append("<span class=\"priceNotes\">" + listProduct[j].PriceNote + "</span>");
                            }
                            result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProducts[j].Price) + "đ</span>");
                            result.Append("</div>");
                            result.Append("<div class=\"boxSale\">");
                            result.Append("<a href=\"\" title=\"\"></a>");
                            result.Append("</div>");
                            result.Append("</div>");
                            result.Append(" </div>");
                            result.Append("</div>");
                        }

                        result.Append("</div>");
                        result.Append("</div>");
                    }


                }
                ViewBag.result = result.ToString();

            }
            catch (Exception ex)
            {
                return Redirect("/error");
            }
            return View();

        }

        public ActionResult productTag(string tag)
        {
            StringBuilder result = new StringBuilder();

            var listIDProduct = db.TblProductTag.Where(p => p.Tag == tag || p.Name.Contains(tag)).Select(p => p.Idp).ToList();
            var listTagProduct = db.TblProductTag.Where(p => p.Tag == tag).Take(1).ToList();
            var listProduct = db.TblProduct.Where(p => p.Active == true && (listIDProduct.Contains(p.Id) || p.Code == tag)).OrderBy(p => p.Ord).ToList();
            string name = tag;
            string title = tag;
            string description = tag;
            if (listProduct.Count > 0)
            {
                int idcate = int.Parse(listProduct[0].IdCate.ToString());
                var tblconfig = db.TblConfig.FirstOrDefault();
                ViewBag.color = tblconfig.Color;
                if (listTagProduct.Count > 0)
                {
                    name = listTagProduct[0].Name;
                    title = listTagProduct[0].Name;
                    description = listTagProduct[0].Name;
                }
                else
                {
                    name = tag;
                    title = tag;
                    description = tag;
                }

                var TblTags = db.TblTags.Where(p => p.Tag == tag && p.Active == true).Take(1).ToList();
                if (TblTags.Count > 0)
                {
                    name = TblTags[0].Name;
                    title = TblTags[0].Title;
                    description = TblTags[0].Description; ViewBag.des = TblTags[0].Content;
                }

                ViewBag.favicon = " <link href=\"" + tblconfig.Favicon + "\" rel=\"icon\" type=\"image/x-icon\" />";
                ViewBag.Title = "<title>" + title + "</title>";
                ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + title + "\" />";
                ViewBag.Description = "<meta name=\"description\" content=\"Danh sách sản phẩm " + description + "\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + name + "\" /> ";
                string meta = "";
                meta += "<meta itemprop=\"name\" content=\"" + name + "\" />";
                meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta itemprop=\"description\" content=\"" + name + "\" />";
                meta += "<meta itemprop=\"image\" content=\"\" />";
                meta += "<meta property=\"og:title\" content=\"" + title + "\" />";
                meta += "<meta property=\"og:type\" content=\"product\" />";
                meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta property=\"og:image\" content=\"\" />";
                meta += "<meta property=\"og:site_name\" content=\"http://maylocnuocgeyser.com.vn\" />";
                meta += "<meta property=\"og:description\" content=\"" + name + "\" />";
                meta += "<meta property=\"fb:admins\" content=\"\" />";
                ViewBag.Meta = meta;

                ViewBag.canonical = "<link rel=\"canonical\" href=\"http://maylocnuocgeyser.com.vn/tag/" + tag + "\" />";
                result.Append("<div class=\"tearListProduct\">");
                result.Append("<div class=\"filter\">");
                result.Append("<h1 class=\"name\">   " + name + "   </h1>");
                result.Append("</div>");
                result.Append("<div class=\"contentListProductContent\">");
                for (int j = 0; j < listProduct.Count; j++)
                {
                    result.Append("<div class=\"tear\">");
                    result.Append("<div class=\"contentTear\">");
                    float price = float.Parse(listProduct[j].Price.ToString());
                    float pricesale = float.Parse(listProduct[j].PriceSale.ToString());
                    float phantram = 100 - ((pricesale * 100) / price);
                    if (listProduct[j].New == true)
                    {
                        result.Append(" <div class=\"sale\"> Mới 2019</div>");
                    }
                    else
                    {
                        result.Append(" <div class=\"sale\">" + Convert.ToInt32(phantram) + "%</div>");
                    }
                    if (listProduct[j].Note != null && listProduct[j].Note != "")
                        result.Append("<div class=\"noteTear\">" + listProduct[j].Note + "</div>");

                    result.Append("<div class=\"img\">");
                    result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\"><img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                    result.Append(" </div>");
                    result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a>");
                    result.Append("<div class=\"boxItem\">");
                    result.Append("<div class=\"boxPrice\">");
                    result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                    if (listProduct[j].PriceNote != null && listProduct[j].PriceNote != "")
                    {
                        result.Append("<span class=\"priceNotes\">" + listProduct[j].PriceNote + "</span>");
                    }
                    result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                    result.Append("</div>");
                    result.Append("<div class=\"boxSale\">");
                    result.Append("<a href=\"\" title=\"\"></a>");
                    result.Append("</div>");
                    result.Append("</div>");
                    result.Append(" </div>");
                    result.Append("</div>");
                }
                result.Append("</div>");
                result.Append("</div>");
                ViewBag.result = result.ToString();
                ViewBag.nUrl = "<ol itemscope itemtype=\"http://schema.org/BreadcrumbList\">   <li itemprop=\"itemListElement\" itemscope  itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"http://maylocnuocgeyser.com.vn\">  <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li>   ›" + name + "</ol> ";
                return View(listProduct);
            }
            else
            {
                var TblTags = db.TblTags.Where(p => p.Tag == tag && p.Active == true).Take(1).ToList();
                if (TblTags.Count > 0)
                {
                    name = TblTags[0].Name;
                    title = TblTags[0].Title;
                    description = TblTags[0].Description;
                }
                ViewBag.Title = "<title>" + title + "</title>";
                ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + title + "\" />";
                ViewBag.Description = "<meta name=\"description\" content=\"Danh sách sản phẩm " + description + "\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tag + "\" /> ";
                ViewBag.canonical = "<link rel=\"canonical\" href=\"http://maylocnuocgeyser.com.vn/tag/" + tag + "\" />";
                result.Append("<div class=\"tearListProduct\">");
                result.Append("<div class=\"filter\">");
                result.Append("<h1 class=\"name\">   " + tag + "   </h1>");
                result.Append("</div>");
                result.Append("<div class=\"contentListProductContent\">");
                result.Append("</div>");
                result.Append("</div>");
                ViewBag.result = result.ToString();
            }
            return View(listProduct);
        }

        public ActionResult search(string tag)
        {
            if (Session["Search"] != null)
                tag = Session["Search"].ToString();
            var listProduct = db.TblProduct.Where(p => p.Name.Contains(tag) && p.Active == true).ToList();
            ViewBag.Name = tag;

            var manufactures = db.TblManufactures.FirstOrDefault(); ;
            ViewBag.manuName = manufactures.Name;
            ViewBag.color = manufactures.Color;
            listProduct = db.TblProduct.Where(p => p.Active == true && p.Name.Contains(tag)).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();

            ViewBag.favicon = " <link href=\"" + manufactures.Favicon + "\" rel=\"icon\" type=\"image/x-icon\" />";
            ViewBag.Title = "<title>" + tag + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tag + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"Danh sách sản phẩm " + tag + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tag + "\" /> ";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://maylocnuocgeyser.com.vn/search/" + tag + "\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + tag + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tag + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + tag + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://maylocnuocgeyser.com.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + tag + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            result.Append("<div class=\"tearListProduct\">");
            result.Append("<div class=\"filter\">");
            result.Append("<h1 class=\"name\">  Từ khóa bạn tìm kiếm :  " + tag + "   </h1>");
            result.Append("</div>");
            result.Append("<div class=\"contentListProductContent\">");
            for (int j = 0; j < listProduct.Count; j++)
            {
                result.Append("<div class=\"tear\">");
                result.Append("<div class=\"contentTear\">");
                float price = float.Parse(listProduct[j].Price.ToString());
                float pricesale = float.Parse(listProduct[j].PriceSale.ToString());
                float phantram = 100 - ((pricesale * 100) / price);
                if (listProduct[j].New == true)
                {
                    result.Append(" <div class=\"sale\"> Mới 2019</div>");
                }
                else
                {
                    result.Append(" <div class=\"sale\">" + Convert.ToInt32(phantram) + "%</div>");
                }
                if (listProduct[j].Note != null && listProduct[j].Note != "")
                    result.Append("<div class=\"noteTear\">" + listProduct[j].Note + "</div>");

                result.Append("<div class=\"img\">");
                result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\"><img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                result.Append(" </div>");
                result.Append("<a href=\"/" + listProduct[j].Tag + ".html\" title=\"" + listProduct[j].Name + "\" class=\"name\">" + listProduct[j].Name + "</a>");
                result.Append("<div class=\"boxItem\">");
                result.Append("<div class=\"boxPrice\">");
                result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                if (listProduct[j].PriceNote != null && listProduct[j].PriceNote != "")
                {
                    result.Append("<span class=\"priceNotes\">" + listProduct[j].PriceNote + "</span>");
                }
                result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                result.Append("</div>");
                result.Append("<div class=\"boxSale\">");
                result.Append("<a href=\"\" title=\"\"></a>");
                result.Append("</div>");
                result.Append("</div>");
                result.Append(" </div>");
                result.Append("</div>");
            }
            result.Append("</div>");
            result.Append("</div>");
            ViewBag.result = result.ToString();
            ViewBag.nUrl = "<ol itemscope itemtype=\"http://schema.org/BreadcrumbList\">   <li itemprop=\"itemListElement\" itemscope  itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"http://maylocnuocgeyser.com.vn\">  <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li>   ›Bạn đang tìm kiếm : " + tag + "</ol> ";

            return View(listProduct);
        }
         public PartialViewResult formRequest()
        {
            return PartialView();
        }
        public ActionResult likes(int id)
        {
            int like = 0;
            TblLike dbLike = db.TblLike.FirstOrDefault(p => p.IdC == id);
            if (dbLike == null)
            {
                TblLike tbllike = new TblLike();
                tbllike.IdC = id;
                tbllike.Like = 1;
                like = 1;
                db.TblLike.Add(tbllike);
                db.SaveChanges();
            }
            else
            {
                dbLike.Like += 1;
                db.SaveChanges();
                like = int.Parse(dbLike.Like.ToString());
            }

            return Json(new { like = like });
        }

        public  ActionResult command(FormCollection frmconnection)
        {
            string content = frmconnection["txtfeedback"];
            string email = frmconnection["txtEmail"];
            string name = frmconnection["txtName"];
            string idParent = frmconnection["txtId"];
            string idC = frmconnection["txtIdc"];
            string nameFb = frmconnection["txtNameFb"];
            string tag = frmconnection["txtTag"];
            TblFeedback feedback = new TblFeedback();
            if (idParent != "" && idParent != null)
            {
                feedback.Name = name;
                feedback.Email = email;
                feedback.Content = content;
                feedback.IdParent = int.Parse(idParent);
                feedback.Active = true;
                feedback.DateCreate = DateTime.Now;
                feedback.Type = 1;
                feedback.Url = "/" + tag + ".html";
                feedback.IdC = int.Parse(idC);
                db.TblFeedback.Add(feedback);
                db.SaveChanges();

            }
            else
            {
                feedback.Name = name;
                feedback.Email = email;
                feedback.Content = content;
                feedback.Active = true;
                feedback.Type = 1;
                feedback.Url = "/" + tag + ".html";

                feedback.IdC = int.Parse(idC);

                feedback.DateCreate = DateTime.Now;
                db.TblFeedback.Add(feedback);
                db.SaveChanges();
            }
            sendMail.sendmail("Một phản hổi mới trên ", nameFb, "https://maylocnuocgeyser.com.vn/" + tag + ".html"
                , name, email, "Online", "Online", email, content);

            return Redirect("/" + tag + ".html");
        }
    }
}