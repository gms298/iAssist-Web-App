using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using iAssist_Web;
using System.Data.SqlClient;

public partial class Account_Data_Entry : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Set_Data(object sender, EventArgs e)
    {
        GlobalVariables.caffeine_max = Convert.ToInt32(Caffeine_Max.Text);
        GlobalVariables.sugar_max = Convert.ToInt32(Sugar_Max.Text);
        GlobalVariables.bp_max = Convert.ToInt32(BP_Max.Text);
        GlobalVariables.bp_min = Convert.ToInt32(BP_Min.Text);
        GlobalVariables.heart_max = Convert.ToInt32(Heart_Rate_Max.Text);
        GlobalVariables.heart_min = Convert.ToInt32(Heart_Rate_Min.Text);

        //Store in User_Settings database
        SqlConnection con_Users = new SqlConnection(@"Data Source=DATABASENAMEHERE;Initial Catalog=iAssist_User;Persist Security Info=True;User ID=iassist_admin;Password=");
    
        SqlCommand cmd1 = new SqlCommand();
        try
        {
            con_Users.Open();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "Store_User_Settings";

            cmd1.Parameters.AddWithValue("@UserID", GlobalVariables.guserID);
            cmd1.Parameters.AddWithValue("@Caffeine", GlobalVariables.caffeine_max);
            cmd1.Parameters.AddWithValue("@Sugar", GlobalVariables.sugar_max);
            cmd1.Parameters.AddWithValue("@BPmax", GlobalVariables.bp_max);
            cmd1.Parameters.AddWithValue("@BPmin", GlobalVariables.bp_min);
            cmd1.Parameters.AddWithValue("@Hmax", GlobalVariables.heart_max);
            cmd1.Parameters.AddWithValue("@Hmin", GlobalVariables.heart_min);

            cmd1.Connection = con_Users;
            cmd1.ExecuteNonQuery();

            cmd1.Dispose();
            con_Users.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error !! ')", true);
        }
    }
}