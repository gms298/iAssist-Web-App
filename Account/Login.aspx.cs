using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using iAssist_Web;
using System.Data.SqlClient;

public partial class Account_Login : Page
{
        protected void Page_Load(object sender, EventArgs e)
        {
          

        }
        SqlConnection con = new SqlConnection(@"Data Source=DATABASENAMEHERE;Initial Catalog=iassist_db;Persist Security Info=True;User ID=iassist_admin;Password=");
        SqlCommand cmd;
        SqlDataReader dr;
   
        protected void LogIn(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                cmd = new SqlCommand("select * from [iassistdev].[Accounts] where email = '" + UserName.Text + "' and username = '" + Password.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    GlobalVariables.guserID = 1;
                    Response.Redirect("~/Account/Data_Entry.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Login Unsuccessful')", true);
                }
                dr.Close();
                cmd.Dispose();
                con.Close();

            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Login Unsuccessful due to network')", true);
            }

        }
}