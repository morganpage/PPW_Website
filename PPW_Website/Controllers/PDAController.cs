using PPW_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace PPW_Website.Controllers
{
    public class PDAController : Controller
    {
        dppwSQLDB _db = new dppwSQLDB();

        public JsonResult Read(string strPDA = "PDA1")
        {
            var model = _db.GetPDAJobs(strPDA);

            return Json(model, JsonRequestBehavior.AllowGet);//maybe remove allowget later for security reasons, will have to use POST instead
        }

        public JsonResult Write(HttpPostedFileBase uploaded, string jsondata, string encrypted_hash)
        {
            if (!_db.ValidatePDA(encrypted_hash))
                return Json(new JsonResponse("Wrong Password"));

            if (jsondata != null)
            {
                //Convert json to pdajob
                PDAJob pdajob = JsonConvert.DeserializeObject<PDAJob>(jsondata);
                _db.SaveJob(pdajob.TicketID, pdajob.CD, pdajob.Cond, pdajob.Container, pdajob.SignedFor, pdajob.SignedTime);//Save PDAJob Data to SQL DB
                if (uploaded != null)
                {   //Write signature away
                    byte[] bytes = new byte[uploaded.ContentLength];
                    uploaded.InputStream.Read(bytes, 0, uploaded.ContentLength);
                    _db.SaveSig(pdajob.TicketID, pdajob.CD, bytes);
                }
            }
            return Json(new JsonResponse("Success"));
        }

    }
}
