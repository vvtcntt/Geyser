using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
 using Geyser.Models;

namespace Geyser.Controllers.DisplayCustom
{
    public class DefaultController : Controller
    {
        private GeyserContext db = new GeyserContext();

        // GET: manufactures
        public ActionResult Index(string tag)
        {
             var tblconfig = db.TblConfig.FirstOrDefault();
              ViewBag.Title = "<title>" + tblconfig.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblconfig.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblconfig.Name + "\" /> ";
            Session["idManu"] = tblconfig.Id;
            ViewBag.h1 = "<h1 class=\"h1\">"+tblconfig.Title+"</h1>";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Geyser.vn/\" />";
            ViewBag.favicon = " <link href=\"" + tblconfig.Favicon + "\" rel=\"icon\" type=\"image/x-icon\" />";
            return View();
        }
        public PartialViewResult partialdefault()
        {
             
            return PartialView(db.TblConfig.First());
        }
        public PartialViewResult partialHeadHome()
        { 
             var listImage = db.TblImage.Where(p => p.IdCate == 1  && p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < listImage.Count; i++)
            {
                result.Append("<a href=\"" + listImage[i].Url + "\" title=\"" + listImage[i].Name + "\"><img src=\"" + listImage[i].Images + "\" data-thumb=\"" + listImage[i].Images + "\" alt=\"" + listImage[i].Name + "\" /></a>");
            }
            ViewBag.result = result.ToString();
            var listImageAdw = db.TblImage.Where(p => p.IdCate == 2 && p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder resultAdw = new StringBuilder();
            for (int i = 0; i < listImageAdw.Count; i++)
            {
                resultAdw.Append("<a href=\"" + listImageAdw[i].Url + "\" title=\"" + listImageAdw[i].Name + "\"><img src=\"" + listImageAdw[i].Images + "\" data-thumb=\"" + listImageAdw[i].Images + "\" alt=\"" + listImageAdw[i].Name + "\" /></a>");
            }
            ViewBag.resultAdw = resultAdw.ToString();
            return PartialView();
        }

        public PartialViewResult productSaleHomes()
        {
           
                 var listProduct = db.TblProduct.Where(p => p.Active == true && p.ProductSale == true  ).OrderByDescending(p => p.DateCreate).Take(12).ToList();
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < listProduct.Count; i++)
                {
                    result.Append(" <div class=\"item\">");
                    float price = float.Parse(listProduct[i].Price.ToString());
                    float pricesale = float.Parse(listProduct[i].PriceSale.ToString());
                    float phantram = 100 - ((pricesale * 100) / price);
                if (listProduct[i].New == true)
                {
                    result.Append(" <div class=\"sale\"> Mới 2018</div>");
                }
                else
                {
                    result.Append(" <div class=\"sale\">" + Convert.ToInt32(phantram) + "%</div>");
                }
                if (listProduct[i].Note != null && listProduct[i].Note != "")
                    result.Append("<div class=\"noteTear\">" + listProduct[i].Note + "</div>");

                result.Append("<div class=\"img\">");
                    result.Append("<a href=\"/" + listProduct[i].Tag + ".html\" title=\"" + listProduct[i].Name + "\"><img src=\"" + listProduct[i].ImageLinkThumb + "\" alt=\"" + listProduct[i].Name + "\" /></a>");
                    result.Append("</div>");
                    result.Append("<a class=\"name\" href=\"/" + listProduct[i].Tag + ".html\" title=\"" + listProduct[i].Name + "\">" + listProduct[i].Name + "</a>");
                    result.Append("<div class=\"boxItem\">");
                    result.Append("<div class=\"boxPrice\">");
                    result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[i].PriceSale) + "đ</span>");
                    result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[i].Price) + "đ</span>");
                    result.Append("</div>");
                    result.Append("<div class=\"boxSale\">");
                    result.Append("<a href=\"\" title=\"\"></a>");
                    result.Append("</div>");
                    result.Append("</div>");
                    result.Append("</div>");
                }
                ViewBag.result = result.ToString();
                return PartialView(db.TblConfig.FirstOrDefault());
             
         }

        public PartialViewResult productHomes()
        {
             
                var manufactures = db.TblManufactures.FirstOrDefault();
                 var groupProduct = db.TblGroupProduct.Where(p => p.Active == true && p.Priority == true  ).OrderBy(p => p.Ord).ToList();
                StringBuilder result = new StringBuilder();
                StringBuilder resultIcon = new StringBuilder();
                for (int i = 0; i < groupProduct.Count; i++)
                {
                    resultIcon.Append("<li class=\"current\"><a href=\"#neo-" + i + "\" class=\"neo\"><img src=\"" + groupProduct[i].ICon + "\" alt=\"Tên tầng " + i + "\" title=\"Tên tầng " + i + "\"></a></li>");
                    result.Append("<div class=\"products\">");
                    result.Append("<div id=\"neo-" + i + "\" class=\"cneo\"></div>");
                    result.Append("<div class=\"Floor\" style=\"background:" + manufactures.Color + "\">");
                    result.Append("<div class=\"Content_Floor\">");
                    result.Append("<div class=\"LeftFloor\" style=\"background:" + manufactures.Color + "\">");
                    result.Append("<div class=\"Leftfloor1\"><span>"+ manufactures.Name + " </span></div>");
                    result.Append("<div class=\"circle\" style=\"color:" + manufactures.Color + "\">"+(i+1)+"</div>");
                    result.Append("</div>");
                    result.Append("<div class=\"Center_Floor\">");
                    result.Append("<h2><a href=\"/" + groupProduct[i].Tag + "\" title=\"" + groupProduct[i].Name + "\">" + groupProduct[i].Name + "</a></h2>");
                    result.Append("</div>");
                    result.Append("<div class=\"Menufloor\">");
                    int idCate = groupProduct[i].Id;
                    var listIdChild = db.TblGroupProduct.Where(p => p.ParentId == idCate && p.Active == true).Select(p => p.Id).ToList();
                    var groupProductChild = db.TblGroupProduct.Where(p => p.Active == true && listIdChild.Contains(p.Id)).OrderBy(p => p.Ord).ToList();
                    for (int j = 0; j < groupProductChild.Count; j++)
                    {
                        result.Append("<a href=\"/" + groupProductChild[j].Tag + "\" title=\"" + groupProductChild[j].Name + "\">" + groupProductChild[j].Name + "</a>");
                    }

                    result.Append(" </div>");
                    result.Append("<div class=\"RightFloor\">");
                    result.Append("<div class=\"stairs\">");
                    if(i<groupProduct.Count-1)
                    result.Append("<a href=\"#neo-" + (i + 1) + "\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>");
                    result.Append("<i class=\"Elevator\"></i>");
                    if(i>0)
                    result.Append("<a href=\"#neo-" + (i - 1) + "\" title=\"Lên tầng\"><i class=\"up\"></i></a>");
                    result.Append("</div>");
                    result.Append("</div>");
                    result.Append("</div>");
                    result.Append("</div>");
                    result.Append("<div class=\"contentProducts\">");

                    result.Append("<div class=\"adwProducts\">");
                    var listIdChildImage = db.TblConnectImages.Where(p => p.IdCate == idCate).Select(p => p.IdImg).ToList();
                    var listImages = db.TblImage.Where(p => p.Active == true && p.IdCate == 3 && listIdChildImage.Contains(p.Id)).OrderBy(p => p.Ord).ToList();
                    for (int j = 0; j < listImages.Count; j++)
                    {
                        result.Append("<a href=\"" + listImages[j].Url + "\" title=\"" + listImages[j].Name + "\"><img src=\"" + listImages[j].Images + "\" alt =\"" + listImages[j].Name + "\" /></a>");
                    }

                    result.Append("</div>");

                    result.Append("<div class=\"listProducts\">");
                    listIdChild.Add(idCate);

                    var listProduct = db.TblProduct.Where(p => p.Active == true && p.ViewHomes == true && listIdChild.Contains(p.IdCate.Value)).OrderBy(p =>p.IdCate).ToList();
                    for (int j = 0; j < listProduct.Count; j++)
                    {
                        result.Append("<div class=\"tear\">");
                        result.Append("<div class=\"contentTear\">");
                        float price = float.Parse(listProduct[j].Price.ToString());
                        float pricesale = float.Parse(listProduct[j].PriceSale.ToString());
                        float phantram = 100 - ((pricesale * 100) / price);
                    if (listProduct[j].New == true)
                    {
                        result.Append(" <div class=\"sale\"> Mới 2018</div>");
                    }
                    else
                    {
                        result.Append(" <div class=\"sale\">" + Convert.ToInt32(phantram) + "%</div>");
                    }
                    if (listProduct[j].Note!= null && listProduct[j].Note != "")
                        result.Append("<div class=\"noteTear\">" + listProduct[j].Note+"</div>");
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
                    result.Append(" </div>");

                    result.Append("  </div>");

                    result.Append("  </div>");
                }
                ViewBag.result = result.ToString();
                ViewBag.resultIcon = resultIcon.ToString();
            
            return PartialView();
        }

        public PartialViewResult newsVideoHomes()
        {
            var tblconfig = db.TblConfig.FirstOrDefault();
                 var manufactures = db.TblManufactures.FirstOrDefault();
                ViewBag.name = manufactures.Name;
                ViewBag.code = manufactures.Video;
                ViewBag.color = tblconfig.Color;
                 var listNews = db.TblNews.Where(p => p.Active == true ).OrderByDescending(p => p.DateCreate).Take(3).ToList();
                StringBuilder result = new StringBuilder();
                for(int i=0;i<listNews.Count;i++)
                {
                    result.Append("<div class=\"tearNews\">");
                    result.Append("<a href=\"/"+listNews[i].Tag+ ".htm\" title=\"" + listNews[i].Name + "\"><img src=\"" + listNews[i].Images + "\" alt=\"\" /></a>");
                    result.Append("<a class=\"name\" href=\"/" + listNews[i].Tag + ".htm\" title=\"" + listNews[i].Name + "\">" + listNews[i].Name + "</a>");
                    result.Append("<span> " + listNews[i].Description + "</span>");
                    result.Append(" <span class=\"times\">Ngày cập nhật : " + listNews[i].DateCreate + "</span>");
                    result.Append("</div>");
                }
                ViewBag.result = result;
            
            return PartialView();
        }
    }
}