using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_7
{
    public partial class FarmerPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Application["user"].ToString()) != "Farmer")
            {
                Response.Redirect("Default.aspx");
            }
        }

        private void DBsave(string prodName, string prodType, int quant, int id, int unit)
        {
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["FarmersMarketDBConnectionString"].ConnectionString;

            SqlConnection conn = new SqlConnection(con);
            SqlCommand cmd;

            using (conn)
            {
                var sqlCmd = "INSERT INTO TBl_Product (ProductName, ProductType, FarmerId, Quantity, UnitPrice)" +
                     "VALUES (@prodName, @prodType, @id, @quant, @unit)";

                using(cmd = new SqlCommand(sqlCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@prodName", prodName);
                    cmd.Parameters.AddWithValue("@prodType", prodType);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@quant", quant);
                    cmd.Parameters.AddWithValue("@unit", unit);
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
            

            if ((txtProdName.Value != null) && (txtProdPrice.Value != null) && (txtProdQuant.Value != null))
            {
                int price;
                int quant;

                if ( (int.TryParse(txtProdPrice.Value, out price)) && (int.TryParse(txtProdQuant.Value, out quant) ) )
                {
                    string name = txtProdName.Value;
                    string type = selectProd.Value;
                    int id = (Int32) Application["FarmerID"];

                    DBsave(name, type, price, id, quant);
                }
                else
                {

                }
            }
            else
            {

            }
            

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.btnSubmitProd.ServerClick += new System.EventHandler(this.btnSubmitProd_ServerClick);

        }
    }
}