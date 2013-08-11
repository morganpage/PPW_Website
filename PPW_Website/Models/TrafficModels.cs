using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace PPW_Website.Models
{
    public class JsonResponse
    {
        public virtual string Response { get; set; }
        public JsonResponse(string response)
        {
            Response = response;
        }
    }


    public class PDAJob
    {
        public virtual string CD { get; set; }
        public virtual int TicketID { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string CusRef1 { get; set; }
        public virtual string Adr1 { get; set; }
        public virtual string Adr2 { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string OpenTimes { get; set; }
        public virtual string SiteRestrict { get; set; }
        public virtual string Container { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string ConDetail { get; set; }
        public virtual string Goods { get; set; }
        public virtual decimal ActWt { get; set; }
        public virtual DateTime? CDTime { get; set; }
        public virtual DateTime? CDDate { get; set; }
        public virtual string Cond { get; set; }
        public virtual string SignedFor { get; set; }
        public virtual DateTime SignedTime { get; set; }

        public PDAJob()
        {
            Cond = "";
        }
    }

    public class WEBJob
    {
        public virtual int TicketID { get; set; }
        public virtual DateTime? DelDate { get; set; }
        public virtual DateTime? ActTimeDel { get; set; }

        public virtual string POD { get; set; }
        public virtual string PODExtra { get; set; }
        public virtual string Collect { get; set; }
        public virtual string Deliver { get; set; }
        public virtual string InvDetail { get; set; }
        public virtual string MoreInfo { get; set; }
        public virtual string StatusReport { get; set; }

        public virtual Byte[] CPDA_Sig { get; set; }
        public virtual Byte[] PDA_Sig { get; set; }

        public virtual WebImage CPDA_SigWI { get; set; }
        public virtual WebImage PDA_SigWI { get; set; }

    }

}