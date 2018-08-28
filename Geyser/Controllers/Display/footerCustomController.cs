using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Geyser.Models;
using Geyser.Models;
namespace Geyser.Controllers.DisplayCustom
{
    public class footerCustomController : Controller
    {
        private GeyserContext db = new GeyserContext();
        // GET: footerCustom
        public ActionResult Index()
        {
            return View();
        }
         public PartialViewResult partialFooter()
        {
            var manufactures = db.TblManufactures.FirstOrDefault();
                var tblconfig = db.TblConfig.FirstOrDefault();
                var listAddress = db.TblAddress.Where(p => p.Active == true  ).OrderBy(p => p.Ord).ToList();
                StringBuilder result = new StringBuilder();
                for(int i=0;i<listAddress.Count;i++)
                {
                    result.Append("<div class=\"tearLoca\">");
                    result.Append("<div class=\"contentTearLoca\">");
                    string images = listAddress[i].Images;
                    if (images==null)
                        images = manufactures.ImagesShowroom;
                    result.Append("<div class=\"leftcontentTearLoca\"><img src=\""+ images + "\" alt=\"" + listAddress[i].Name + "\" /></div>");
                    result.Append("<div class=\"rightcontentTearLoca\">");
                    result.Append("<p class=\"p1\"><span style=\"color:"+manufactures.Color+"\">" + manufactures.Name + "</span> " + listAddress[i].Name + "</p>");
                    result.Append("<p class=\"p2\"> " + listAddress[i].Address + "</p><a href=\"#\" title=\"Địa chỉ chỉ dẫn\"> <i class=\"fa fa-map-marker\" aria-hidden=\"true\"></i> Bản đồ đường đi</a>");
                    result.Append("</div>");
                    result.Append("</div>");
                    result.Append("</div>");
                }
                ViewBag.result = result.ToString();
                 StringBuilder chuoinew = new StringBuilder();
                var listnew = db.TblNews.Where(p => p.Active == true && p.ViewHomes == true && p.IdCate == 2  ).OrderByDescending(p => p.DateCreate).Take(5).ToList();
                for (int i = 0; i < listnew.Count; i++)
                {

                    chuoinew.Append(" <li><a href=\"/" + listnew[i].Tag + ".htm\" title=\"" + listnew[i].Name + "\">" + listnew[i].Name + "</a> </li>");

                }
                ViewBag.chuoinew = chuoinew.ToString();
                StringBuilder resultProduct = new StringBuilder();
                 var listProduct = db.TblProduct.Where(p => p.Active == true && p.ViewHomes == true  ).OrderByDescending(p => p.DateCreate).Take(5).ToList();
                for(int i=0;i<listProduct.Count;i++)
                {
                    resultProduct.Append("<li class=\"li2\"><a href=\"/"+listProduct[i].Tag+ ".html \" title=\"" + listProduct[i].Name + "\">› " + listProduct[i].Name + " </a></li>");
                }
                ViewBag.resultProduct = resultProduct.ToString() ;
                var listBaogia = db.TblGroupProduct.Where(p => p.Active == true && p.Baogia == true  ).OrderBy(p => p.Ord).Take(10).ToList();
                StringBuilder resultBaogia = new StringBuilder();
            StringBuilder resultMenu = new StringBuilder();
                for (int i = 0; i < listBaogia.Count; i++)
                {
                if(i<5)
                    resultBaogia.Append("<li class=\"li2\"><a href=\"/bao-gia/bao-gia-" + listBaogia[i].Tag + " \" title=\"Báo giá " + listBaogia[i].Name + "\">› Báo giá " + listBaogia[i].Name + " </a></li>");

                resultMenu.Append("<li><a href=\"/" + listBaogia[i].Tag + "\" title=\"" + listBaogia [i].Name+ "\">" + listBaogia[i].Name + " </a> › </li>");
            }
            ViewBag.resultMenu = resultMenu.ToString();
                ViewBag.resultBaogia = resultBaogia.ToString();
                var TblConfig = db.TblConfig.FirstOrDefault();

                StringBuilder Chuoiimg = new StringBuilder();
                if (Request.Browser.IsMobileDevice)
                {
                    Chuoiimg.Append("<div id=\"adwfooter\"><div class=\"support\">");
                    Chuoiimg.Append("<div class=\"leftSupport\">");
                    Chuoiimg.Append("<p><i class=\"fa fa-comments-o\" aria-hidden=\"true\"></i> Hỗ trợ khách hàng</p>");
                    Chuoiimg.Append("<a href=\"tel:" + manufactures.Hotline1 + "\"> " + manufactures.Hotline1 + "</a>");
                    Chuoiimg.Append("<a href=\"tel:" + manufactures.Hotline2 + "\">" + manufactures.Hotline2 + "</a>");
                    Chuoiimg.Append("</div>");
                    Chuoiimg.Append("<div class=\"rightSupport\">");
                    Chuoiimg.Append("<p><i class=\"fa fa-clock-o\" aria-hidden=\"true\"></i> Thời gian làm việc</p>");
                    Chuoiimg.Append("<span class=\"sp1\"> 7H đến 22H</span>");
                    Chuoiimg.Append("<span class=\"sp2\"> Làm cả thứ 7 & Chủ nhật</span>");
                    Chuoiimg.Append("</div>");
                    Chuoiimg.Append("</div></div>");

                }
                ViewBag.results = Chuoiimg.ToString();
                ViewBag.codeChat = TblConfig.CodeChat;

            var listImage = db.TblImage.Where(p => p.IdCate == 10).ToList();
            StringBuilder resultImages = new StringBuilder();
            for(int i=0;i<listImage.Count;i++)
            {
                resultImages.Append("<img src=\""+listImage[i].Images+"\" alt=\""+listImage[i].Name+"\" />");
            }
            ViewBag.resultImages = resultImages.ToString();

            return PartialView(db.TblConfig.FirstOrDefault());
        }
    }
}