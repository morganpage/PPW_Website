using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace PPW_Website.Models
{
    public class dppwSQLDB : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }

        //public DbSet<PDAJob> PDAJobs { get; set; }
        public bool ValidatePDA(string encrypted_hash)
        {
            try
            {
                string secretKey = "Twas brillig, and the slithy toves. Did gyre and gimble in the wabe";
                IEnumerable<String> strPasswords = this.Database.SqlQuery<string>("SELECT WSvPassword FROM TrControl");
                string strPassword = strPasswords.First<String>();
                string inputString = strPassword + secretKey;
                if (encrypted_hash == Utility.Md5Sum(inputString))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                ErrorHandler(e);
                return false;
            }
        }
        public bool IsValidPasscode(string strAc,string Passcode)
        {
            try
            {
                IEnumerable<String> strPasscodes = this.Database.SqlQuery<string>("SELECT Web_Passcode FROM Customer WHERE Ac = '" + strAc + "'");
                string strPasscode = strPasscodes.First<String>();
                if (strPasscode == Passcode)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                ErrorHandler(e);
                return false;
            }
        }
        public IEnumerable<PDAJob> GetPDAJobs(string strPDA)//Returns list of jobs suitable for pickup by PDA and updates pdastatus
        {
            try
            {
                IEnumerable<PDAJob> model = this.Database.SqlQuery<PDAJob>("EXEC usp_WSv_GetPDAJobs @PDA_ID", new SqlParameter("PDA_ID", strPDA)).ToList<PDAJob>(); //ToList Forces immediate
                foreach (var pdajob in model) //Now set pdastatus for all these jobs to 3-Seen
                {
                    UpdatePDAStatus(pdajob.TicketID, pdajob.CD, 3);
                }
                return model;
            }
            catch (Exception e)
            {
                ErrorHandler(e);
                return null;
            }
        }

        public bool UpdatePDAStatus(long lngTicketID, string strCD, int intPDAStatus)
        {
            try
            {
                var param = new SqlParameter[] { 
                    new SqlParameter("TicketID",lngTicketID),    
                    new SqlParameter("CD",strCD),
                    new SqlParameter("PDAStatus",intPDAStatus)
                };
                this.Database.ExecuteSqlCommand("EXEC usp_Wsv_UpdatePDAStatus @TicketID,@CD,@PDAStatus", param);
                return true;
            }
            catch (Exception e)
            {
                ErrorHandler(e);
                return false;
            }
        }

        public bool SaveSig(long lngTicketID, string strCD, Byte[] bytes)
        {
            try
            {
                var param = new SqlParameter[] { 
                    new SqlParameter("TicketID",lngTicketID),    
                    new SqlParameter("CD",strCD),
                    new SqlParameter("Sig",bytes)
                };
                this.Database.ExecuteSqlCommand("EXEC usp_WSv_UpdateSig @TicketID,@CD,@Sig", param);
                UpdatePDAStatus(lngTicketID, strCD, 5); //Update pdastatus to 5-Signature
                return true;
            }
            catch (Exception e)
            {
                ErrorHandler(e);
                return false;
            }
        }

        public bool SaveJob(long lngTicketID, string strCD, string strCond, string strContainer, string strSignedFor, DateTime dtSignedTime)
        {
            try
            {
                var param = new SqlParameter[] { 
                    new SqlParameter("TicketID",lngTicketID),    
                    new SqlParameter("CD",strCD),
                    new SqlParameter("Cond",strCond),
                    new SqlParameter("Container",strContainer),
                    new SqlParameter("SignedFor",strSignedFor),
                    new SqlParameter("SignedTime",dtSignedTime)
                };
                this.Database.ExecuteSqlCommand("EXEC usp_WSv_UpdatePDAJob @TicketID,@CD,@Cond,@Container,@SignedFor,@SignedTime", param);
                UpdatePDAStatus(lngTicketID, strCD, 4); //Update pdastatus to 4-JobDetails Received
                return true;
            }
            catch (Exception e)
            {
                ErrorHandler(e);
                return false;
            }
        }

        public IEnumerable<WEBJob> GetWEBJobs(string strAc, DateTime dtFrom, DateTime dtTo)
        {
            var param = new SqlParameter[] { 
                    new SqlParameter("Ac",strAc),    
                    new SqlParameter("DateFrom",dtFrom),
                    new SqlParameter("DateTo",dtTo)
                };
            IEnumerable<WEBJob> model = this.Database.SqlQuery<WEBJob>("EXEC usp_WSv_GetWEBJobs @Ac,@DateFrom,@DateTo", param).ToList<WEBJob>(); //ToList Forces immediate
            return model;
        }
        public IEnumerable<WEBJob> GetWEBJobsOLD(string strAc,DateTime dtFrom,DateTime dtTo)
        {
            if (strAc == "") return null;

            string strSQL = "SELECT TrafficSheet.TicketID,DelDate,ActTimeDel ,POD,PODExtra, ";
            strSQL += "Collect1 + CHAR(10) +  CAST(Collect2 AS NVARCHAR(MAX)) + CHAR(10) + CPostCode AS 'Collect',";
            strSQL += "Deliver1 + CHAR(10) +  CAST(Deliver2 AS NVARCHAR(MAX)) + CHAR(10) + DPostCode AS 'Deliver',";
            strSQL += "InvDetail,Goods + CHAR(10) +  CAST(ConDetail AS NVARCHAR(MAX)) As 'MoreInfo',";
            strSQL += "TrafficSheet.StatusReport, MultiDrop.CPDA_Sig,MultiDrop.PDA_Sig ";
            strSQL += "FROM  TrafficSheet INNER JOIN MultiDrop ON TrafficSheet.TRID = MultiDrop.TRID ";
            //strSQL += "WHERE CPDA_ID = 'PDA1'";
            strSQL += "WHERE TrafficSheet.Ac ='" + strAc + "'";

            IEnumerable<WEBJob> model = this.Database.SqlQuery<WEBJob>(strSQL).ToList<WEBJob>();
            //foreach (var m in model)
            //{
            //    try
            //    {
            //        m.CPDA_SigWI = new WebImage(m.CPDA_Sig);
            //    }
            //    catch
            //    {

            //    }
            //    try
            //    {
            //        m.PDA_SigWI = new WebImage(m.PDA_Sig);
            //    }
            //    catch
            //    {

            //    }

            //}
            return model;
        }


        //public IEnumerable<Byte[]> GetImages()
        //{
        //    //WebImage image = new WebImage(
        //    IEnumerable<Byte[]> bytes;
        //    bytes = this.Database.SqlQuery<Byte[]>("SELECT PDA_Sig FROM TrafficSheet WHERE CPDA_ID = 'PDA1'");
        //    return bytes;

        //    //return null;
        //}


        public Byte[] GetImage(long lngTicketID, string strCD)
        {
            IEnumerable<Byte[]> bytes;
            string strSQL = "";
            if(strCD == "C")
                strSQL = "SELECT MultiDrop.CPDA_Sig ";
            else
                strSQL = "SELECT MultiDrop.PDA_Sig ";

            strSQL += "FROM  TrafficSheet INNER JOIN MultiDrop ON TrafficSheet.TRID = MultiDrop.TRID ";
            strSQL += "WHERE TrafficSheet.TicketID = " + lngTicketID;
            bytes = this.Database.SqlQuery<Byte[]>(strSQL);
            return bytes.First();

        }


        public Byte[] GetImage()
        {
            //WebImage image = new WebImage(
            IEnumerable<Byte[]> bytes;
            //String strSQL = "SELECT CPDA_Sig FROM TrafficSheet INNER JOIN MultiDrop ON TrafficSheet.TRID = MultiDrop.TRID WHERE CPDA_ID = 'PDA1'";

            String strSQL = "SELECT MultiDrop.CPDA_Sig FROM  TrafficSheet INNER JOIN MultiDrop ON TrafficSheet.TRID = MultiDrop.TRID WHERE  (MultiDrop.CPDA_Sig IS NOT NULL)";



            bytes = this.Database.SqlQuery<Byte[]>(strSQL);
            return bytes.First();
        }

        //public void WriteImage(Byte[] bytes)
        //{
        //    //Dim paramAvatar As SqlParameter = myCommand.Parameters.Add("@Avatar", SqlDbType.Image)
        //    var sql = @"Update TrafficSheet SET PDA_Sig = @PDA_Sig WHERE CPDA_ID = 'PDA1'";
        //    var param = new SqlParameter("PDA_Sig",SqlDbType.Image);
        //    param.Value = bytes;
        //    this.Database.ExecuteSqlCommand(sql,param);
        //}

        public void ErrorHandler(Exception e)
        {
            throw e;
        }
    }
}