using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Geyser.Models;
using Geyser.Models;
namespace Geyser.Controllers.Admin
{
    public class AdminController : Controller
    {
        GeyserContext db = new GeyserContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult partialBanner()
        {
            ViewBag.donhang = db.TblOrder.Where(p => p.Status == false && p.Active==true).ToList().Count;
            return PartialView();
        }
    }
}