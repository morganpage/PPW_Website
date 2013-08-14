using PPW_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace PPW_Website.Controllers
{
    public class HomeController : Controller
    {
        dppwSQLDB _db = new dppwSQLDB();

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Job Enquiries";
            var username = User.Identity.Name;
            var user = _db.UserProfiles.SingleOrDefault(u => u.UserName == username);
            var accountname = user.AccountName;

            ViewBag.AccountName = accountname;

            var model = _db.GetWEBJobs("PDA1");
            return View(model);
        }

        public ActionResult GetImage(long lngTicketID, string strCD)
        {
            Byte[] bytes = _db.GetImage(lngTicketID, strCD);
            WebImage model = null;
            try
            {
                model = new WebImage(bytes);
            }
            catch 
            {
            }

            return View(model);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
