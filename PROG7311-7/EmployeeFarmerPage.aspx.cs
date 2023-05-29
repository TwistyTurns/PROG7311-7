using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace PROG7311_7
{
    public partial class EmployeeFarmerPage : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Application["user"].ToString()) != "Employee")
            {
                Response.Redirect("Default.aspx");
            }
        }

        private void DBsave(string farmName, string pwd)
        {
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["FarmersMarketDBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(con);
            SqlCommand cmd;

            using (conn)
            {
                var sqlCmd = "INSERT INTO TBLFarmer (FarmerName)" +
                     "VALUES (@name)";

                using (cmd = new SqlCommand(sqlCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@name", farmName);
                    
                    cmd.ExecuteNonQuery();
                }

                var sqlCmd2 = "Select Id from TBL_Farmer where FarmerName=@name";
                using (cmd = new SqlCommand(sqlCmd2, conn))
                {
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 10);
                    cmd.Parameters["@name"].Value = farmName;
                }
                
                int farmID = Convert.ToInt32(cmd.ExecuteScalar());

                string hashmethod = "SHA1";

                string password = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, hashmethod);

                var sqlCmd3 = "INSERT INTO TBL_FarmerPassword (FarmerID, hashedPassword)" +
                    "VALUES (@FarmerID, @pwd)";

                using (cmd = new SqlCommand(sqlCmd3, conn))
                {
                    cmd.Parameters.AddWithValue("@pwd", pwd);
                    cmd.Parameters.AddWithValue("@id", farmID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void btnSubmitProd_ServerClick(object sender, EventArgs e)
        {
            //Application.Remove("Farmer");
            //Application.RemoveAll();

            //verify data
            //convert to ints


            if ((txtPassword.Value != null) && (txtFarmerName.Value != null))
            {
                string pass = txtPassword.Value;
                string name = txtFarmerName.Value;
                    
                DBsave(name, pass);
            }
            else
            {

            }
        }
    }
}