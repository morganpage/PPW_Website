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

    public class ReportJob
    {
        public virtual int TicketID { get; set; }
        public virtual string Ac { get; set; }
        public virtual string Name { get; set; }
        public virtual string POD { get; set; }
        public virtual string DiName { get; set; }
        public virtual string PODExtra { get; set; }

        public virtual string Collect1 { get; set; }
        public virtual string Collect2 { get; set; }
        public virtual string CPostCode { get; set; }
        public virtual DateTime? ColDate { get; set; }
        public virtual DateTime? ColTime { get; set; }
        public virtual string CRegNo{ get; set; }
        public virtual string CNickName { get; set; }
        public virtual string ColTrlr { get; set; }
        public virtual int CLoadNo { get; set; }
        public virtual Int16 CPickUp { get; set; }
        public virtual string ColSiteRestrict { get; set; }
        public virtual string ColOpenTimes { get; set; }

        public virtual string Deliver1 { get; set; }
        public virtual string Deliver2 { get; set; }
        public virtual string DPostCode { get; set; }
        public virtual DateTime? DelDate { get; set; }
        public virtual DateTime? DelTime { get; set; }
        public virtual string DRegNo { get; set; }
        public virtual string DNickName { get; set; }
        public virtual string DelTrlr { get; set; }
        public virtual int LoadNo { get; set; }
        public virtual Int16 Drop { get; set; }
        public virtual string DelSiteRestrict { get; set; }
        public virtual string DelOpenTimes { get; set; }

//            strSQL += "Units,M3,Miles,Pallets,ActWt,Goods,Remarks,ConDetail,VanDeVan,Container,CrLimit,Rate,RateName,";
  //          strSQL += "SignedFor,ActCTimeArr,DelDate_MD,PDA_ID";
        public virtual decimal Units { get; set; }
        public virtual decimal M3 { get; set; }
        public virtual Int16 Miles { get; set; }
        public virtual int Pallets { get; set; }
        public virtual decimal ActWt { get; set; }
        public virtual string Goods { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string ConDetail { get; set; }
        public virtual string VanDeVan { get; set; }
        public virtual string Container { get; set; }
        public virtual double CrLimit { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual string RateName { get; set; }
        public virtual string SignedFor { get; set; }
        public virtual DateTime? ActCTimeArr { get; set; }
        public virtual DateTime? DelDate_MD { get; set; }
        public virtual string PDA_ID { get; set; }



    }

}