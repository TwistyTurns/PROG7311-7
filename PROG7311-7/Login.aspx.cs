using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Media;
using System.Configuration;
using System.Data;

namespace PROG7311_7
{
    public partial class Login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private bool ValidateUser(string name, string pwd)
        {
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["FarmersMarketDBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(con);
            SqlCommand cmd;
            string lookupPassword = null;
            int lookupID= 0;

            if((null==name)||(0 ==name.Length)||(name.Length > 10))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of name failed");
                return false;
            }

            if ((null == pwd) || (0 == pwd.Length) || (pwd.Length > 10))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of password failed");
                return false;
            }

            if ((SelectUserType.Value == "Default") || (SelectUserType.SelectedIndex == 0))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Option not selected");
                return false;
            }

            try
            {
                //easiest way i found to search for the password for comparison would be to 
                //allow user to state if theyre a farmer or employee
                //this would limit the tables to search through
                
                string type = SelectUserType.Value;

                using (conn)
                {
                    conn.Open();

                    switch (type)
                    {
                        default:
                            //cmd was an unnasigned local variable 
                            cmd = new SqlCommand("");
                            Response.Redirect("Login.aspx", true);
                            break;

                        case "Employee":
                            //get ID using name
                            //after reviewing this i realize that there would be an issue if multiple users had the same name
                            //thus it would be better if each users name were unique - meaning that they were forced to be unique upon registering
                            //will probably fix for the final submission
                            cmd = new SqlCommand("Select Id from TBL_Employee where EmployeeName=@name", conn);
                            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 10);
                            cmd.Parameters["@name"].Value = name;

                            lookupID = Convert.ToInt32(cmd.ExecuteScalar());

                            //get password using ID
                            cmd = new SqlCommand("Select hashedPassword from TBL_EmployeePassword where EmployeeID=@lookupID", conn);
                            cmd.Parameters.Add("@lookupID", System.Data.SqlDbType.Int);
                            cmd.Parameters["@lookupID"].Value = lookupID;

                            //ensure that the global variable is empty
                            //store what type of user is accessing the site
                            //used for redirecting unauthorized users
                            Application.RemoveAll();
                            Application["user"] = "Employee";
                            break;

                        case "Farmer":
                            //get ID using name
                            cmd = new SqlCommand("Select Id from TBLFarmer where FarmerName=@name", conn);
                            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 10);
                            cmd.Parameters["@name"].Value = name;

                            lookupID = Convert.ToInt32(cmd.ExecuteScalar());

                            Application.RemoveAll();
                            Application["FarmerID"] = lookupID;

                            //get password using ID
                            cmd = new SqlCommand("Select hashedPassword from TBL_FarmerPassword where FarmerID=@lookupID", conn);
                            cmd.Parameters.Add("@lookupID", System.Data.SqlDbType.Int);
                            cmd.Parameters["@lookupID"].Value = lookupID;

                            Application["user"] = "Farmer";
                            break;
                    }

                    lookupPassword = (string) cmd.ExecuteScalar();

                    cmd.Dispose();
                    
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
            }

            if(null == lookupPassword)
            {
                return false;
            }

            return (0 == string.Compare(lookupPassword, pwd, false));
        }

        private void cmdLogin_ServerClick(object sender, System.EventArgs e)
        {
            if(ValidateUser(txtUserName.Value, txtUserPass.Value))
            {
                //string name = Application["user"].ToString();
                FormsAuthentication.RedirectFromLoginPage(txtUserName.Value, chkPersistCookie.Checked);
                
            }
            else
            {
                Response.Redirect("Login.aspx", true);
            }
        }

        
        protected void Page_Init(object sender, EventArgs e)
        {
            this.cmdLogin.ServerClick += new System.EventHandler(this.cmdLogin_ServerClick);
        }
    }
}