using InventoryWebApp.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Razor;

namespace InventoryWebApp.Infrastructure.DAL.Services
{
    public class InventoryDataService
    {
        //Populating dropdown with items with the same group id
        public static List<InventoryRecord> GetInventoryRecordsByGroupId(string groupId)
        {
            using (var dal = new DataAccessLayer())
            {
                dal.SqlQuery = @"
                        SELECT * 
                        FROM tblGroupTable 
                        WHERE CTGP = @CTGP
                        ORDER BY ITDSC";

                dal.SqlParameters.Add(new SqlParameter("@CTGP", groupId));

                var inventoryRecords = new List<InventoryRecord>();

                var dataTable = dal.ExecuteCommand();
                foreach (DataRow row in dataTable.Rows)
                {
                    inventoryRecords.Add(new InventoryRecord
                    {
                        ctgp = row["CTGP"].ToString().Trim(),
                        whid = row["WHID"].ToString(),
                        itno = row["ITNO"].ToString().Trim(),
                        itdsc = row["ITDSC"].ToString(),
                        blno = row["BLNO"].ToString().Trim(),
                        whlc = row["WHLC"].ToString().Trim(),
                        unmsr = row["UNMSR"].ToString(),
                        seentry = row["SEEntry"].ToString(),
                        bdentry = row["BdEntry"].ToString(),
                        stkentry = row["stkEntry"].ToString(),
                        umbd = row["UM_BD"].ToString(),
                        umse = row["UM_SE"].ToString(),
                        ctq1 = row["ctq1"].ToString(),
                        um2bd = row["UM2BD"].ToString(),
                        bdqnty = row["BDQnty"].ToString(),
                        bdconvert = row["BDConvert"].ToString(),
                        um2se = row["UM2SE"].ToString(),
                        seqnty = row["SEQnty"].ToString(),
                        seconvert = row["SEConvert"].ToString()
                    });
                }

              

                return inventoryRecords;
            }
        }

        //Searching item number
        public static List<InventoryRecord> GetInventoryRecordById(string inventoryId, string groupId)
        {
            using (var dal = new DataAccessLayer())
            {
                dal.SqlQuery = @"
                        SELECT * 
                        FROM tblGroupTable 
                        WHERE CTGP = @CTGP AND
                        ITNO = @ITNO";

                dal.SqlParameters.Add(new SqlParameter("@ITNO", inventoryId.ToString()));
                dal.SqlParameters.Add(new SqlParameter("@CTGP", groupId.ToString()));
                var inventoryRecords = new List<InventoryRecord>();
                var dataTable = dal.ExecuteCommand();
                foreach (DataRow row in dataTable.Rows)
                {
                    inventoryRecords.Add(new InventoryRecord
                    {
                        ctgp = row["CTGP"].ToString().Trim(),
                        whid = row["WHID"].ToString(),
                        itno = row["ITNO"].ToString().Trim(),
                        itdsc = row["ITDSC"].ToString(),
                        blno = row["BLNO"].ToString().Trim(),
                        whlc = row["WHLC"].ToString().Trim(),
                        unmsr = row["UNMSR"].ToString(),
                        seentry = row["SEEntry"].ToString(),
                        bdentry = row["BdEntry"].ToString(),
                        stkentry = row["stkEntry"].ToString(),

                        umbd = row["UM_BD"].ToString(),
                        umse = row["UM_SE"].ToString(),
                        ctq1 = row["ctq1"].ToString(),

                        um2bd = row["UM2BD"].ToString(),
                        bdqnty = row["BDQnty"].ToString(),
                        bdconvert = row["BDConvert"].ToString(),

                        um2se = row["UM2SE"].ToString(),
                        seqnty = row["SEQnty"].ToString(),
                        seconvert = row["SEConvert"].ToString()
                    });
                }

                return inventoryRecords;
            }
        }

        public static List<InventoryRecord> UpdateInventory(string itno, string bdentry, string seentry, string stkentry, string groupId, string blno, string whlc)
        {
            var selectedItem = SelectInventory(groupId, itno, whlc, blno).First();

            //variable declarations
            double seqntyF;
            double bdqntyF;
            double seentryF;
            double bdentryF;
            double stkentryF;
            double calculatedbd;
            double calculatedse;
            double seqnty;
            double bdqnty;
            double um2bdF = string.IsNullOrWhiteSpace(selectedItem.um2bd) ? 0 : System.Math.Round((double.Parse(selectedItem.um2bd)), 3);
            double um2seF = string.IsNullOrWhiteSpace(selectedItem.um2se) ? 0 : System.Math.Round((double.Parse(selectedItem.um2se)), 3);

            //if else ( variable = condition ? consequent : alternative)
            seqntyF = string.IsNullOrWhiteSpace(selectedItem.seqnty) ? 0 : double.Parse(selectedItem.seqnty);
            bdqntyF = string.IsNullOrWhiteSpace(selectedItem.bdqnty) ? 0 : double.Parse(selectedItem.bdqnty);

            //Values from user (BdEntry & SeEntry are whole numbers, StkEntry is decimal)
            bdentryF = string.IsNullOrWhiteSpace(bdentry) ? 0 : System.Math.Round((double.Parse(bdentry)), 2);
            seentryF = string.IsNullOrWhiteSpace(seentry) ? 0 : System.Math.Round((double.Parse(seentry)), 2);
            stkentryF = string.IsNullOrWhiteSpace(stkentry) ? 0 : System.Math.Round((double.Parse(stkentry)), 2);

            //Calculate BD and SE Convert, sets to 0 if BDqnty or SEqnty is null
            calculatedbd = string.IsNullOrWhiteSpace(selectedItem.bdqnty) ? 0 : System.Math.Round(((um2bdF / bdqntyF) * bdentryF), 2);
            calculatedse = string.IsNullOrWhiteSpace(selectedItem.seqnty) ? 0 : System.Math.Round(((um2seF / seqntyF) * seentryF), 2);

            var calculatedctq1 = System.Math.Round(calculatedbd + calculatedse + stkentryF, 2);

            var user = HttpContext.Current.User.Identity.GetUserName().Split('@').First();
            using (var dal = new DataAccessLayer())
            {
                dal.SqlQuery = @"
                        UPDATE tblGroupTable SET BdEntry = @BDENTRY,
                        SEEntry = @SEENTRY,
                        stkEntry = @STKENTRY,
                        BDConvert = @BDCONVERT,
                        SEConvert = @SECONVERT,
                        CTQ1 = @CTQ1,
                        UpdatedBy = @UPDATEDBY,
                        Updated = 1
                        WHERE ITNO = @ITNO 
                        AND CTGP = @CTGP";

                dal.SqlParameters.Add(new SqlParameter("@BDCONVERT", calculatedbd));
                dal.SqlParameters.Add(new SqlParameter("@SECONVERT", calculatedse));
                dal.SqlParameters.Add(new SqlParameter("@CTQ1", calculatedctq1.ToString()));
                dal.SqlParameters.Add(new SqlParameter("@UPDATEDBY", user.ToString()));

                dal.SqlParameters.Add(new SqlParameter("@ITNO", itno.ToString()));
                dal.SqlParameters.Add(new SqlParameter("@CTGP", groupId.ToString()));
                dal.SqlParameters.Add(new SqlParameter("@BDENTRY", bdentryF.ToString()));
                dal.SqlParameters.Add(new SqlParameter("@SEENTRY", seentryF.ToString()));
                dal.SqlParameters.Add(new SqlParameter("@STKENTRY", stkentryF.ToString()));

                var inventoryRecords = new List<InventoryRecord>();

                dal.ExecuteCommand();

                return inventoryRecords;
            }
        }


        public static List<InventoryRecord> SelectInventory(string countGroup, string itemNumber, string warehouseLoc, string batchLot)
        {
            using (var dal = new DataAccessLayer())
            {
                dal.SqlQuery = @"
                        SELECT * 
                        FROM tblGroupTable 
                        WHERE CTGP = @CTGP
                        AND ITNO = @ITNO
                        AND WHLC = @WHLC
                        AND BLNO = @BLNO
                        ORDER BY ITDSC";

                dal.SqlParameters.Add(new SqlParameter("@CTGP", countGroup.ToString()));
                dal.SqlParameters.Add(new SqlParameter("@ITNO", itemNumber.ToString()));
                dal.SqlParameters.Add(new SqlParameter("@WHLC", warehouseLoc.ToString()));
                dal.SqlParameters.Add(new SqlParameter("@BLNO", batchLot.ToString()));

                var inventoryRecords = new List<InventoryRecord>();
                var dataTable = dal.ExecuteCommand();
                foreach (DataRow row in dataTable.Rows)
                {
                    inventoryRecords.Add(new InventoryRecord
                    {
                        ctgp = row["CTGP"].ToString().Trim(),
                        whid = row["WHID"].ToString(),
                        itno = row["ITNO"].ToString().Trim(),
                        itdsc = row["ITDSC"].ToString(),
                        blno = row["BLNO"].ToString().Trim(),
                        whlc = row["WHLC"].ToString().Trim(),
                        unmsr = row["UNMSR"].ToString(),
                        seentry = row["SEEntry"].ToString(),
                        bdentry = row["BdEntry"].ToString(),
                        stkentry = row["stkEntry"].ToString(),

                        umbd = row["UM_BD"].ToString(),
                        umse = row["UM_SE"].ToString(),
                        ctq1 = row["ctq1"].ToString(),

                        um2bd = row["UM2BD"].ToString(),
                        bdqnty = row["BDQnty"].ToString(),
                        bdconvert = row["BDConvert"].ToString(),

                        um2se = row["UM2SE"].ToString(),
                        seqnty = row["SEQnty"].ToString(),
                        seconvert = row["SEConvert"].ToString()
                    });
                }

                return inventoryRecords;
            }
        }

    }
}