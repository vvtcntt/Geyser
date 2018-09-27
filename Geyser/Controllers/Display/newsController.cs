using Geyser.Models;
using Geyser.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Geyser.Controllers.Display
{
    public class newsController : Controller
    {
        // GET: news
        private GeyserContext db = new GeyserContext();
        // GET: newsCustom
        public ActionResult Index()
        {
            return View();
        }
        private string nUrl = "";

        public string Urlnews(int idCate)
        {
            var ListMenu = db.TblGroupNews.Where(p => p.Id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = " <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"/2/" + ListMenu[i].Tag + "\"> <span itemprop=\"name\">" + ListMenu[i].Name + "</span></a> <meta itemprop=\"position\" content=\"\" /> </li>  " + nUrl;
                string ids = ListMenu[i].ParentId.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentId.ToString());
                    Urlnews(id);
                }
            }
            return nUrl;
        }

        public ActionResult newsDetail(string tag)
        {
            TblNews TblNews = db.TblNews.First(p => p.Tag == tag);
            int id = TblNews.Id;

            TblGroupNews groupNews = db.TblGroupNews.First(p => p.Id == TblNews.IdCate.Value);
            ViewBag.menuName = groupNews.Name;
            ViewBag.tagMenuName = groupNews.Tag;
            var tblconfig = db.TblConfig.FirstOrDefault();
            ViewBag.color = tblconfig.Color;
            if (TblNews.Style == true)
            {
                ViewBag.style = "width:100% !important; margin:0px";
                ViewBag.style1 = "display:none";
            }
            ViewBag.Title = "<title>" + TblNews.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + TblNews.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + TblNews.Keyword + "\" /> ";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + TblNews.Title + "\" />";
            if (TblNews.Meta != null && TblNews.Meta != "")
            {
                int phut = DateTime.Now.Minute * 2;
                ViewBag.refresh = "<meta http-equiv=\"refresh\" content=\"" + phut + "; url=" + TblNews.Meta + "\">";
            }
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + TblNews.Description + "\" />";
            string meta = "";
            
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://maylocnuocgeyser.com.vn/tin-tuc/" + StringClass.NameToTag(tag) + "\" />";

            meta += "<meta itemprop=\"name\" content=\"" + TblNews.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + TblNews.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://maylocnuocgeyser.com.vn" + TblNews.Images + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + TblNews.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://maylocnuocgeyser.com.vn" + TblNews.Images + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://maylocnuocgeyser.com.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + TblNews.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            StringBuilder schame = new StringBuilder();
            schame.Append("<script type=\"application/ld+json\">");
            schame.Append("{");
            schame.Append("\"@context\": \"http://schema.org\",");
            schame.Append("\"@type\": \"NewsArticle\",");
            schame.Append("\"headline\": \"" + TblNews.Description + "\",");
            schame.Append(" \"datePublished\": \"" + TblNews.DateCreate + "\",");
            schame.Append("\"image\": [");
            schame.Append(" \"" + TblNews.Images + "\"");
            schame.Append(" ]");
            schame.Append("}");
            schame.Append("</script> ");
            ViewBag.schame = schame.ToString();
            int IdUser = int.Parse(TblNews.IdUser.ToString());
            ViewBag.user = db.TblUser.First(p => p.Id == IdUser).UserName;
            string tab = TblNews.Tabs;
            string Tabsnews = "";
            if (tab != null)
            {

                string[] mang = tab.Split(',');
                for (int i = 0; i < mang.Length; i++)
                {

                    Tabsnews += " <a href=\"/newstag/" + StringClass.NameToTag(mang[i]) + "\" title=\"" + mang[i] + "\">" + mang[i] + "</a>";



                }
                ViewBag.tags = Tabsnews;

            }
            StringBuilder result = new StringBuilder();

            var listnews = db.TblNews.Where(p => p.Active == true && p.Id != id).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            if (listnews.Count > 0)
            {
                for (int j = 0; j < listnews.Count; j++)
                {
                    result.Append(" <li><a href=\"/" + listnews[j].Tag + ".htm\" title=\"" + listnews[j].Name + "\"><i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i>  " + listnews[j].Name + "</a><span>");
                    result.Append("  <i class=\"fa fa-clock-o\" aria-hidden=\"true\"></i>  Viết ngày : " + listnews[j].DateCreate + "    </span><li>");

                }
            }
            ViewBag.result = result.ToString();
            ViewBag.favicon = " <link href=\"" + tblconfig.Favicon + "\" rel=\"icon\" type=\"image/x-icon\" />";
            ViewBag.nUrl = "<ol itemscope itemtype=\"http://schema.org/BreadcrumbList\">   <li itemprop=\"itemListElement\" itemscope  itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"http://maylocnuocgeyser.com.vn\">  <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li>   ›" + Urlnews(TblNews.IdCate.Value) + "</ol>";
            int visit = int.Parse(TblNews.Visit.ToString());
            if (visit > 0)
            {
                TblNews.Visit = TblNews.Visit + 1;
                db.SaveChanges();
            }
            else
            {
                TblNews.Visit = TblNews.Visit + 1;
                db.SaveChanges();
            }
            return View(TblNews);
        }
        public ActionResult listNews(string tag, int? page)
        {
            TblGroupNews groupnews = db.TblGroupNews.First(p => p.Tag == tag);
            ViewBag.Title = "<title>" + groupnews.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + groupnews.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + groupnews.Keyword + "\" /> ";

            int idcate = int.Parse(groupnews.Id.ToString());
            ViewBag.Name = groupnews.Name;
            var Listnews = db.TblNews.Where(p => p.IdCate == idcate && p.Active == true).OrderByDescending(p => p.DateCreate).ToList();
            const int pageSize = 20;
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
            ViewBag.name = groupnews.Name;

            ViewBag.nUrl = "<ol itemscope itemtype=\"http://schema.org/BreadcrumbList\">   <li itemprop=\"itemListElement\" itemscope  itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"http://maylocnuocgeyser.com.vn\">  <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li>   ›" + Urlnews(idcate) + "</ol>";
            ViewBag.favicon = " <link href=\"" + db.TblConfig.First().Favicon + "\" rel=\"icon\" type=\"image/x-icon\" />";


            var tblconfig = db.TblConfig.FirstOrDefault();
            ViewBag.color = tblconfig.Color;

            Listnews = db.TblNews.Where(p => p.IdCate == idcate && p.Active == true).OrderByDescending(p => p.DateCreate).ToList();



            return View(Listnews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult tagNews(string tag, int? page)
        {
            var listIdNews = db.TblNewsTag.Where(p => p.Tag == tag || p.Name.Contains(tag)).Select(p => p.Idn).ToList();
            var listNews = db.TblNews.Where(p => p.Active == true && listIdNews.Contains(p.Id)).OrderBy(p => p.Ord).ToList();

            var tblconfig = db.TblManufactures.FirstOrDefault();
            ViewBag.color = tblconfig.Color;


            const int pageSize = 20;
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
            string name = tag;
            ViewBag.name = name;
            ViewBag.Title = "<title>" + name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + name + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + name + "\" /> ";
            ViewBag.favicon = " <link href=\"" + db.TblConfig.First().Favicon + "\" rel=\"icon\" type=\"image/x-icon\" />";

            StringBuilder schame = new StringBuilder();
            schame.Append("<script type=\"application/ld+json\">");
            schame.Append("{");
            schame.Append("\"@context\": \"http://schema.org\",");
            schame.Append("\"@type\": \"NewsArticle\",");
            schame.Append("\"headline\": \"" + name + "\",");
            schame.Append(" \"datePublished\": \"\",");
            schame.Append("\"image\": [");
            schame.Append(" \"\"");
            schame.Append(" ]");
            schame.Append("}");
            schame.Append("</script> ");
            ViewBag.schame = schame.ToString();
            return View(listNews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult partialMenuRightNews()
        {

            return PartialView();
        }
        public ActionResult partialListNewsRightNews(string tag)
        {
            var TblNews = db.TblNews.FirstOrDefault(p => p.Tag == tag);
            int idCate = int.Parse(TblNews.IdCate.ToString());
            int id = TblNews.Id;

            var tblconfig = db.TblConfig.FirstOrDefault();
            ViewBag.color = tblconfig.Color;

            var listNewsLienQuan = db.TblNews.Where(p => p.Active == true &&p.IdCate==idCate).OrderByDescending(p => p.Visit).Take(5).ToList();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < listNewsLienQuan.Count; i++)
            {
                result.Append("<li><a href=\"/" + listNewsLienQuan[i].Tag + ".htm\" title=\"" + listNewsLienQuan[i].Name + "\"><i class=\"fa fa-caret-right\" aria-hidden=\"true\"></i> " + listNewsLienQuan[i].Name + "</a></li>");
            }
            ViewBag.result = result.ToString();


            ///Mới đăng
            var ListNewsNew = db.TblNews.Where(p => p.Active == true).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            StringBuilder resultNews = new StringBuilder();
            for (int i = 0; i < ListNewsNew.Count; i++)
            {
                resultNews.Append("<li><a href=\"/" + ListNewsNew[i].Tag + ".htm\" title=\"" + ListNewsNew[i].Name + "\"><i class=\"fa fa-angle-right\" aria-hidden=\"true\"></i> " + ListNewsNew[i].Name + "</a></li>");
            }
            ViewBag.resultNews = resultNews.ToString();
            return PartialView(tblconfig);
        }
    }
}