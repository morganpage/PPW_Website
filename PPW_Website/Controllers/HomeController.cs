﻿using PPW_Website.Models;
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


        public ActionResult Search(string datefrom = "", string dateto = "")
        {
            return RedirectToAction("Index", "Home", new { dateFrom = datefrom,dateTo=dateto });
        }

        [Authorize]
        public ActionResult Index(string dateFrom="", string dateTo="")
        {
            if (dateFrom == "") dateFrom = DateTime.Now.ToShortDateString();
            if (dateTo == "") dateTo = DateTime.Now.ToShortDateString();
            IFormatProvider culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            DateTime dtFrom = DateTime.Parse(dateFrom, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime dtTo = DateTime.Parse(dateTo, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            ViewBag.Message = "Job Enquiries";
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            var username = User.Identity.Name;
            var user = _db.UserProfiles.SingleOrDefault(u => u.UserName == username);
            var accountname = user.AccountName;

            ViewBag.AccountName = accountname;

            if (accountname == "")
            {
                return Content("You Account Name cannot be blank!");
            }

            var model = _db.GetWEBJobs(accountname,dtFrom,dtTo);
            return View(model);
        }

        [Authorize]
        public ActionResult Report(long lngTicketID,string strCD)
        {
            if(strCD == "C")
                ViewBag.Title = "COLLECTION NOTE";
            else
                ViewBag.Title = "CONSIGNMENT NOTE";

            ViewBag.CD = strCD;
            var model = _db.GetReportJob(lngTicketID, strCD);

            var username = User.Identity.Name;
            var user = _db.UserProfiles.SingleOrDefault(u => u.UserName == username);
            var accountname = user.AccountName;
            if (model.Ac != accountname)
            {
                return Content("You Account Name does not match!");
            }

            return View("Report","_LayoutReport",model);
        }


        [Authorize]
        public ActionResult GetImage(long lngTicketID, string strCD)
        {
            WebImage model = null;
            try
            {
                Byte[] bytes = _db.GetImage(lngTicketID, strCD);
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
