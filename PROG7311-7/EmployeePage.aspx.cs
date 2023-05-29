using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_7
{
    public partial class EmployeePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Application["user"].ToString();

            if(type != "Employee")
            {
                Response.Redirect("Default.aspx", true);
            }
        }
    }
}