using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AutoCompleteTest
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string> ShowCustomer(string sLookUP)
        {
            List<string> customers = new List<string>();

            string sConnString = @"Data Source=.\sqlexpress;Initial Catalog=HouseExpertsMVC;Integrated Security=true";

            SqlConnection myConn = new SqlConnection(sConnString);
            SqlCommand objComm = new SqlCommand("SELECT LastName FROM dbo.Customers " +
                                                "WHERE LastName LIKE '%'+@LookUP+'%' ORDER BY LastName", myConn);
            myConn.Open();

            objComm.Parameters.AddWithValue("@LookUP", sLookUP);
            SqlDataReader reader = objComm.ExecuteReader();

            while (reader.Read())
            {
                customers.Add(reader["LastName"].ToString());
            }
            myConn.Close(); return customers;
        }
    }
}
