using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
 using Geyser.Models;

namespace Geyser.Controllers.Display.Section.Baogia
{
    public class BaogiaController : Controller
    {
        //
        // GET: /Baogia/
        private GeyserContext db = new GeyserContext();

        public PartialViewResult Homes_Baogia()
        {
            string chuoi = "";
            var tblconfig = db.TblConfig.FirstOrDefault();
            var listbaogia = db.TblGroupProduct.Where(p => p.Active == true && p.Baogia == true).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < listbaogia.Count; i++)
            {
                int idcate = int.Parse(listbaogia[i].Id.ToString());
                
                   
 
                    chuoi += "<div class=\"Tear_Baogias\">";
                    chuoi += "<a href=\"/Bao-gia/Bao-gia-" + listbaogia[i].Tag + "\" title=\"Báo giá " + listbaogia[i].Name + "\"><img src=\"" + tblconfig.Logo + "\" alt=\"Báo giá " + listbaogia[i].Name + "\" /></a>";
                    chuoi += "<a href=\"/Bao-gia/Bao-gia-" + listbaogia[i].Tag + "\" title=\"Báo giá " + listbaogia[i].Name + "\" class=\"Name\">Báo giá " + listbaogia[i].Name + "</a>";
                    chuoi += "</div>";
                 
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }

        public ActionResult Index(string tag)
        {
            TblConfig tblcongif = db.TblConfig.First();
              var groupproduct = db.TblGroupProduct.FirstOrDefault(p => p.Tag == tag);
            int idmenu = int.Parse(groupproduct.Id.ToString());
          
                Session["idManu"] = "";
                 ViewBag.name = groupproduct.Name;
                ViewBag.color = tblcongif.Color;
                ViewBag.email = tblcongif.Email;
                ViewBag.website = "maylocnuocgeyser.com.vn";
                 ViewBag.favicon = " <link href=\"" + tblcongif.Favicon + "\" rel=\"icon\" type=\"image/x-icon\" />";
                ViewBag.imagemanu = tblcongif.Logo;

                string moth = "";
                int moths = int.Parse(DateTime.Now.Month.ToString());
                if (moths <= 3)
                {
                    moth = "tháng 1-2-3 ";
                }
                
                else if (moths > 3 && moths <= 6)
                {
                    moth = "tháng 4-5-6 ";
                }
                else if (moths > 6 && moths <= 9)
                {
                    moth = "tháng 7-8-9 ";
                }
                else if (moths >= 9 && moths <= 12)
                {
                    moth = "tháng 10-11-12 ";
                }

                ViewBag.Title = "<title>Bảng báo giá " + groupproduct.Name + " " + moth + "năm " + DateTime.Now.Year + " Rẻ Nhất Việt Nam</title>";
                ViewBag.Description = "<meta name=\"description\" content=\"Bảng báo giá " + groupproduct.Name + " chính hãng Giá rẻ nhất Việt Nam, Cam kết Geyser Hàng Chất Lượng .\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"Bảng Báo giá sản phẩm " + groupproduct.Name + "\" /> ";
                string meta = "";
            
                ViewBag.canonical = "<link rel=\"canonical\" href=\"http://maylocnuocgeyser.com.vn/bao-gia/" + groupproduct.Tag + "\" />";
                meta += "<meta itemprop=\"name\" content=\"Bảng báo giá " + groupproduct.Name + " " + moth + "năm " + DateTime.Now.Year + " rẻ nhất HN\" />";
                meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta itemprop=\"description\" content=\"Bảng báo giá " + groupproduct.Name + " chính hãng của Geyser Chính Hãng Giá rẻ nhất Việt Nam\" />";
                meta += "<meta itemprop=\"image\" content=\"http://maylocnuocgeyser.com.vn" + groupproduct.Images + "\" />";
                meta += "<meta property=\"og:title\" content=\"Bảng báo giá " + groupproduct.Name + " " + moth + "năm " + DateTime.Now.Year + " rẻ nhất HN\" />";
                meta += "<meta property=\"og:type\" content=\"product\" />";
                meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta property=\"og:image\" content=\"http://maylocnuocgeyser.com.vn" + groupproduct.Images + "\" />";
                meta += "<meta property=\"og:site_name\" content=\"http://maylocnuocgeyser.com.vn\" />";
                meta += "<meta property=\"og:description\" content=\"" + groupproduct.Description + "\" />";
                meta += "<meta property=\"fb:admins\" content=\"\" />";
                ViewBag.Meta = meta;
                ViewBag.nUrl = "<ol itemscope itemtype=\"http://schema.org/BreadcrumbList\">   <li itemprop=\"itemListElement\" itemscope  itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"http://maylocnuocgeyser.com.vn\">  <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li>   › Báo giá " + groupproduct.Name + "</ol> ";

                StringBuilder schame = new StringBuilder();
                schame.Append("<script type=\"application/ld+json\">");
                schame.Append("{");
                schame.Append("\"@context\": \"http://schema.org\",");
                schame.Append("\"@type\": \"NewsArticle\",");
                schame.Append("\"headline\": \"" + groupproduct.Name + "\",");
                schame.Append(" \"datePublished\": \"" + groupproduct.DateCreate + "\",");
                schame.Append("\"image\": [");
                schame.Append(" \"" +tblcongif.Logo.Remove(0, 1) + "\"");
                schame.Append(" ]");
                schame.Append("}");
                schame.Append("</script> ");
                ViewBag.schame = schame.ToString();

                var listId = db.TblGroupProduct.Where(p => p.ParentId == idmenu).Select(p => p.Id).ToList();
                listId.Add(idmenu);
                string chuoi = "";
                var listproduct = db.TblProduct.Where(p => p.Active == true && listId.Contains(p.IdCate.Value)).OrderBy(p => p.Ord).ToList();
                for (int i = 0; i < listproduct.Count; i++)
                {
                    chuoi += "<tr>";
                    chuoi += "<td class=\"Ords\">" + (i + 1) + "</td>";
                    chuoi += "<td class=\"Names\">";
                    string note = "";
                    if (listproduct[i].Sale != null && listproduct[i].Sale != "")
                        note = "<span>" + listproduct[i].Access + "</span>";

                    chuoi += "<a href=\"/" + listproduct[i].Tag + ".html\" title=\"" + listproduct[i].Name + "\">" + listproduct[i].Name + " " + note + " </a>";
                    chuoi += "<span class=\"n2\">Chức năng : " + listproduct[i].NoteInfo + "</span>";
                    chuoi += "<span class=\"n3\">Trực tiếp từ Geyser Chính Hãng </span>";
                    chuoi += " </td>";
                    chuoi += "<td class=\"Codes\"> " + listproduct[i].Code + " </td>";
                    chuoi += "<td class=\"Wans\"><a href=\"/" + listproduct[i].Tag + ".html\" title=\"" + listproduct[i].Name + "\"><img src=\"" + listproduct[i].ImageLinkThumb + "\" alt=\"" + listproduct[i].Name + "\" title=\"" + listproduct[i].Name + "\"/></a>" + listproduct[i].Time + "</td>";
                    chuoi += "<td class=\"Prices\">" + string.Format("{0:#,#}", listproduct[i].PriceSale) + "đ  <span class=\"n4\">Lắp đặt Free tại Hà Nội, Chuyển hàng toàn quốc</span></td>";
                    //chuoi += "<td class=\"Qualitys\">01</td>";
                    //chuoi += "<td class=\"SumPrices\">" + string.Format("{0:#,#}", listproduct[i].PriceSale) + "đ</td>";
                    chuoi += "<td class=\"Images\"><a href=\"/" + listproduct[i].Tag + ".html\" title=\"" + listproduct[i].Name + "\"><img src=\"" + listproduct[i].ImageLinkThumb + "\" alt=\"" + listproduct[i].Name + "\" title=\"" + listproduct[i].Name + "\"/></a></td>";
                    chuoi += "</tr>";
                }
                ViewBag.chuoi = chuoi;
        
            return View(tblcongif);
        }
    }
}