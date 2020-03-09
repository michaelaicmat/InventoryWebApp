using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryWebApp.Infrastructure.DAL
{
    public class DataAccessLayer : IDisposable
    {
        private string ConnectionString { get; set; }
        private SqlConnection SqlConnection { get; set; }
        public string SqlQuery { get; set; }
        public List<SqlParameter> SqlParameters { get; set; }

        public DataAccessLayer(string connString = "DefaultConnection")
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[connString].ConnectionString;
            SqlConnection = new SqlConnection(ConnectionString);
            SqlParameters = new List<SqlParameter>();
        }

        public DataTable ExecuteCommand()
        {
            var sqlCommand = new SqlCommand(SqlQuery);
            sqlCommand.Connection = SqlConnection;
            SqlConnection.Open();
            if(SqlParameters.Count > 0)
            {
                sqlCommand.Parameters.AddRange(SqlParameters.ToArray());
            }
            var dataReader = sqlCommand.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(dataReader);

            return dataTable;
        }

        public void Dispose()
        {
            SqlConnection.Close();
        }
    }
}