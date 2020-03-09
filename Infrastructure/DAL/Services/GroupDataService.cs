using InventoryWebApp.Models;
using System.Collections.Generic;
using System.Data;

namespace InventoryWebApp.Infrastructure.DAL.Services
{
    public class GroupDataService
    {
        public static List<GroupRecord> GetAllGroups()
        {
            using (var dal = new DataAccessLayer("ViewConnection"))
            {
                dal.SqlQuery = @"
                SELECT TOP 20 * 
                FROM [dbo].[View_CountGroup] 
                ORDER BY CRDT DESC";

                var groupRecords = new List<GroupRecord>();

                var dataTable = dal.ExecuteCommand();

                foreach (DataRow row in dataTable.Rows)
                {
                    groupRecords.Add(new GroupRecord
                    {
                        ctgp = row["CTGP"].ToString(),
                        ctgd = row["CTGD"].ToString()
                    });
                }

                return groupRecords;
            }
            //var groupRecords = new List<GroupRecord>();
            //groupRecords.Add(new GroupRecord
            //{
            //    ctgp = "9182",
            //    ctgd = ""
            //});
            //return groupRecords;
        }
    }
}