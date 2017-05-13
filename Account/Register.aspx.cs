using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using iAssist_Web;
using System.Data.SqlClient;

public partial class Account_Register : Page
{
    protected void CreateUser_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(@"Data Source=DATABASENAMEHERE;Initial Catalog=iAssist_User;Persist Security Info=True;User ID=iassist_admin;Password=");
            SqlCommand cmd = new SqlCommand();

            try
            {
                con.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "RegisterUser";

                cmd.Parameters.AddWithValue("@Name", UserName.Text);
                cmd.Parameters.AddWithValue("@Age", Age.Text);
                cmd.Parameters.AddWithValue("@EmailID", EmailID.Text);
                cmd.Parameters.AddWithValue("@Password", Password.Text);

                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registration successful! Please Login to Continue..')", true);
                cmd.Dispose();
                con.Close();
            }
        catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Login Unsuccessful due to network')", true);
            }

    }
}