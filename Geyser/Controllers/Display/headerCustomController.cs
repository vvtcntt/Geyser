using System.Linq;
using System.Text;
using System.Web.Mvc;
 using Geyser.Models;

namespace Geyser.Controllers.DisplayCustom
{
    public class headerCustomController : Controller
    {
        // GET: header
        public ActionResult Index()
        {
            return View();
        }

        private GeyserContext db = new GeyserContext();
        public PartialViewResult partialHeader()
        {
           
                var listGroupProduct = db.TblGroupProduct.Where(p => p.Active == true   && p.Parent==true).OrderBy(p => p.Ord).ToList();

                StringBuilder result = new StringBuilder();
                StringBuilder resultMobile = new StringBuilder();
                for(int i=0;i<listGroupProduct.Count;i++)
                {
                    resultMobile.Append(" <li>");
                    resultMobile.Append("<a href=\"/" + listGroupProduct[i].Tag + "\" title=\"" + listGroupProduct[i].Name + "\">" + listGroupProduct[i].Name + "</a>");
                   
                 


                    result.Append("<li class=\"li1\">");
                    result.Append("<a href=\"/"+listGroupProduct[i].Tag+"\" title=\""+listGroupProduct[i].Name+"\">");
                    result.Append("<img src=\"" + listGroupProduct[i].ICon + "\" alt=\"" + listGroupProduct[i].Name + "\" /> <span>" + listGroupProduct[i].Name + "</span>  ");
                    result.Append("</a>");
                    int idCate = listGroupProduct[i].Id;
                    var listGroupChild = db.TblGroupProduct.Where(p => p.Active == true  && p.ParentId == idCate).OrderBy(p => p.Ord).ToList();
                    if(listGroupChild.Count>0)
                    {
                        resultMobile.Append("<ul>");
                    
                        result.Append("<div class=\"menuChild\">");
                        result.Append("<ul class=\"ul2\">");
                        for(int j=0;j<listGroupChild.Count;j++)
                        {
                            result.Append("<li class=\"li2\">");
                            result.Append("<a href=\"/"+listGroupChild[j].Tag+ "\" title=\"" + listGroupChild[j].Name + "\">");
                            result.Append("<img src=\"" + listGroupChild[j].Images + "\" title=\"" + listGroupChild[j].Name + "\" />");
                            result.Append("</a>");
                            result.Append("<a href=\"/" + listGroupChild[j].Tag + "\" class=\"nameMenu\" title=\"" + listGroupChild[j].Name + "\">" + listGroupChild[j].Name + "</a>");
                            result.Append("</li>");
                            resultMobile.Append("<li><a href=\"/" + listGroupChild[j].Tag + "\" title=\"" + listGroupChild[j].Name + "\">" + listGroupChild[j].Name + "</a></li> ");

                        }
                        result.Append("</ul>");
                        resultMobile.Append("</ul>");
                        result.Append("</div>");
                    }
          


                    result.Append("</li>");
                    resultMobile.Append("</li>");
                }
                ViewBag.result = result.ToString();
                ViewBag.resultMobile = resultMobile.ToString();
                
          

            return PartialView(db.TblConfig.FirstOrDefault());
        }
        public ActionResult command(FormCollection collection)
        {
            Session["Search"] = collection["txtSearch"];

            return Redirect("/tim-kiem");
        }
    }
}