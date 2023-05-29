using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_7
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["key"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            //check if user is an employee
            //changes the buttons if true
            if (Application["user"].ToString() == "Employee")
            {
                btn_Login.HRef = "EmployeePage.aspx";
                btn_Login.InnerText = "Search Products";

                //set button visbile if user is employee
                btnAdd.Visible = true;
            }

            //check if user is an farmer
            //changes the buttons if true
            if (Application["user"].ToString() == "Farmer")
            {
                btn_Login.HRef = "FarmerPage.aspx";
                btn_Login.InnerText = "Add Products";
            }
        }
    }
}