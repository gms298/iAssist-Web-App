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
    DateTime dt = DateTime.Now;
    SqlConnection con_nutrition = new SqlConnection(@"Data Source=DATABASENAMEHERE;Initial Catalog=iAssist_Nutrition;Persist Security Info=True;User ID=iassist_admin;Password=");
  //  SqlConnection con_vitals = new SqlConnection(@"Data Source=DATABASENAMEHERE;Initial Catalog=iAssist_Vitals;Persist Security Info=True;User ID=iassist_admin;Password=");
    SqlConnection con_Body = new SqlConnection(@"Data Source=DATABASENAMEHERE;Initial Catalog=iAssist_Body;Persist Security Info=True;User ID=iassist_admin;Password=");
    SqlConnection con_Exercise = new SqlConnection(@"Data Source=DATABASENAMEHERE;Initial Catalog=iAssist_Exercise;Persist Security Info=True;User ID=iassist_admin;Password=");
   // SqlConnection con_Sleep = new SqlConnection(@"Data Source=DATABASENAMEHERE;Initial Catalog=iAssist_Sleep;Persist Security Info=True;User ID=iassist_admin;Password=");
    SqlConnection con_Users = new SqlConnection(@"Data Source=DATABASENAMEHERE;Initial Catalog=iAssist_User;Persist Security Info=True;User ID=iassist_admin;Password=");
    SqlCommand cmd;
    SqlDataReader dr;

    SqlConnection con_iassist = new SqlConnection(@"Data Source=DATABASENAMEHERE;Initial Catalog=iassist_db;Persist Security Info=True;User ID=iassist_admin;Password=");
    SqlCommand cmd_iassist;
    //SqlDataReader dr_iassist;
   

    protected void Page_Load(object sender, EventArgs e)
    {
       //Retrieve and store in global variables the values of user settings
        GlobalVariables.guserID = 1;
        try
        {
            con_Users.Open();
            SqlCommand cmd2 = new SqlCommand("select * from User_Settings where UserID = " + GlobalVariables.guserID + "", con_Users);
            dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                GlobalVariables.caffeine_max = Convert.ToInt32(dr[1].ToString());
                GlobalVariables.sugar_max = Convert.ToInt32(dr[2].ToString());
             //   GlobalVariables.bp_max = Convert.ToInt32(dr[3].ToString());
             //   GlobalVariables.bp_min = Convert.ToInt32(dr[4].ToString());
             //   GlobalVariables.heart_max = Convert.ToInt32(dr[5].ToString());
             //   GlobalVariables.heart_min = Convert.ToInt32(dr[6].ToString());
            }
            else
            {
                GlobalVariables.caffeine_max = 266;
                GlobalVariables.sugar_max = 133;
              //  GlobalVariables.bp_max = 129;
               // GlobalVariables.bp_min = 74;
                //GlobalVariables.heart_max = 79;
                //GlobalVariables.heart_min = 59;
            }
            dr.Dispose();
            cmd2.Dispose();

            cmd2 = new SqlCommand("select Name,Health_Percent from User_Data where UserID = " + GlobalVariables.guserID + "", con_Users);
            dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                Top_Label.Text = "Hi " + dr[0].ToString() + " .. ";
                Health_Percent_Display.Text = "Your Health Percent is " + dr[1].ToString() + "%";
            }
            dr.Dispose();
            con_Users.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 1')", true);
        }
       
    }
    
    protected void Get_Data(object sender, EventArgs e)
    {
        //Clear all data in report fields

        Report_Caffeine.Text = "";
        Report_Calcium.Text = "";
        Report_Carbohydrates.Text = "";
       // Report_Dietary_Cholestrol.Text = "";
        Report_Fibre.Text = "";
        Report_Iodine.Text = "";
        Report_Sugar.Text = "";
        Report_Total_Fat.Text = "";
        Report_Active_Calories.Text = "";
        Report_BMI.Text = "";
        Report_Body.Text = "";
       // Report_BP.Text = "";
        Report_Cycling_Distance.Text = "";
       // Report_Dietary_Cholestrol.Text = "";
        Report_Exercise.Text = "";
        Report_Fat_Percent.Text = "";
        Report_Flights_Climbed.Text = "";
      //  Report_Heart_Rate.Text = "";
        Report_Nutrition.Text = "";
        Report_Resting_Calories.Text = "";
      //  Report_Sleep.Text = "";
        Report_steps.Text = "";
        Report_Sugar.Text = "";
       // Report_Vitals.Text = "";
        Report_Walk_Run_Distance.Text = "";
        
        // For counting the VH,VL,OK

        int nutrition_vh = 0;
        int nutrition_vl = 0;
        int exercise_vh = 0;
        int exercise_vl = 0;

        int body_vh =0;
        int body_vl = 0;
        int body_ok = 0;

        // For Calculating health %

        Double health_nutrition = 0.0;
        Double health_exercise = 0.0;

        Double health_body = 0.0;
        Double calculated_health = 0.0;
        Double old_health = 0.0;
        Double new_health = 0.0;

        //Nutrition Category
        //Caffeine

        int caffeine_val;
        caffeine_val = Convert.ToInt32(Caffeine.Text);
        if (caffeine_val > GlobalVariables.caffeine_max)
        {
            //Fetch VH for caffeine
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Caffeine where Caffeine = 'VH'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Caffeine.Text = dr[0].ToString();
                    nutrition_vh++;

                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 2')", true);
            }

        }
      
        //Calcium

        int calcium_val;
        calcium_val = Convert.ToInt32(Calcium.Text);
        if (calcium_val < 100)
        {
            //Fetch VL 
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Calcium where Calcium = 'VL'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Calcium.Text = dr[0].ToString();
                    nutrition_vl++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 3')", true);
            }
        }
        else if (calcium_val > 200)
        {
            //Fetch VH 
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Calcium where Calcium = 'VH'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Calcium.Text = dr[0].ToString();
                    nutrition_vh++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 4')", true);
            }

        }

        //Carbohydrates

        int carbohydrates_val;
        carbohydrates_val = Convert.ToInt32(Carbohydrates.Text);
        if (carbohydrates_val > 333)
        {
            //Fetch VH 
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Carbohydrates where Carbohydrates = 'VH'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Carbohydrates.Text = dr[0].ToString();
                    nutrition_vh++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 5')", true);
            }

        }

    

        //Fibre

        int Fibre_val;
        Fibre_val = Convert.ToInt32(Fibre.Text);
        if (Fibre_val > 333)
        {
            //Fetch VH 
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Fiber where Fiber = 'VH'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Fibre.Text = dr[0].ToString();
                    nutrition_vh++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 8')", true);
            }

        }

        //Iodine

        int iodine_val;
        iodine_val = Convert.ToInt32(Iodine.Text);
        if (iodine_val < 3)
        {
            //Fetch VL 
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Iodine where Iodine = 'VL'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Iodine.Text = dr[0].ToString();
                    nutrition_vl++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 9')", true);
            }
        }
        else if (iodine_val > 6)
        {
            //Fetch VH 
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Iodine where Iodine = 'VH'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Iodine.Text = dr[0].ToString();
                    nutrition_vh++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 10')", true);
            }
        }

        //Sugar

        int sugar_val;
        sugar_val = Convert.ToInt32(Sugar.Text);
        if (sugar_val > GlobalVariables.sugar_max )
        {
            //Fetch VH
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Sugar where Sugar = 'VH'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Sugar.Text = dr[0].ToString();
                    nutrition_vh++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 11')", true);
            }
        }

        //Total Fat

        int total_fat_val;
        total_fat_val  = Convert.ToInt32(Total_Fat.Text);
        if (total_fat_val < 167)
        {
            //Fetch VL 
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Total_Fat where Total_Fat = 'VL'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Total_Fat.Text = dr[0].ToString();
                    nutrition_vl++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 12')", true);
            }
        }
        else if (total_fat_val > 333)
        {
            //Fetch VH 
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Total_Fat where Total_Fat = 'VH'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Total_Fat.Text = dr[0].ToString();
                    nutrition_vh++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 13')", true);
            }
        }


        // Exercise Category

        // Active Calories

        int active_cal_val;
        active_cal_val = Convert.ToInt32(Active_Calories.Text);
        if (active_cal_val < 999)
        {
            //Fetch VL 
            try
            {
                con_Exercise.Open();
                cmd = new SqlCommand("select Message from Active_Calories where Active_Calories = 'VL'", con_Exercise);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Active_Calories.Text = dr[0].ToString();
                    exercise_vl++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Exercise.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 14')", true);
            }
        }


        // Cycling Distance

        int cycling_distance_val;
        cycling_distance_val= Convert.ToInt32(Cycling_Distance.Text);
        if (cycling_distance_val < 33330)
        {
            //Fetch VL 
            try
            {
                con_Exercise.Open();
                cmd = new SqlCommand("select Message from Cycling_Distance where Cycling_Distance = 'VL'", con_Exercise);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Cycling_Distance.Text = dr[0].ToString();
                    exercise_vl++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Exercise.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 15')", true);
            }
        }


        // Flights Climbed

        int flights_climbed_val;
        flights_climbed_val = Convert.ToInt32(Flights_Climbed.Text);
        if (flights_climbed_val < 8)
        {
            //Fetch VL 
            try
            {
                con_Exercise.Open();
                cmd = new SqlCommand("select Message from Flights_Climbed where Flights_Climbed = 'VL'", con_Exercise);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Flights_Climbed.Text = dr[0].ToString();
                    exercise_vl++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Exercise.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 16')", true);
            }
        }


        // Resting Calories

        int resting_calories_val;
        resting_calories_val = Convert.ToInt32(Resting_Calories.Text);
        if (resting_calories_val > 1999)
        {
            //Fetch VH 
            try
            {
                con_Exercise.Open();
                cmd = new SqlCommand("select Message from Resting_Calories where Resting_Calories = 'VH'", con_Exercise);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Resting_Calories.Text = dr[0].ToString();
                    exercise_vh++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Exercise.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 17')", true);
            }
        }


        // Steps

        int steps_val;
        steps_val = Convert.ToInt32(Steps.Text);
        if (steps_val < 333)
        {
            //Fetch VL 
            try
            {
                con_Exercise.Open();
                cmd = new SqlCommand("select Message from Steps where Steps = 'VL'", con_Exercise);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_steps.Text = dr[0].ToString();
                    exercise_vl++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Exercise.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 18')", true);
            }
        }


        // Walk_Run_Distance

        int Walk_Run_Distance_val;
        Walk_Run_Distance_val = Convert.ToInt32(Walk_Run_Distance.Text);
        if (Walk_Run_Distance_val < 8)
        {
            //Fetch VL 
            try
            {
                con_Exercise.Open();
                cmd = new SqlCommand("select Message from Walk_Run_Distance where Walk_Run_Distance = 'VL'", con_Exercise);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Walk_Run_Distance.Text = dr[0].ToString();
                    exercise_vl++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Exercise.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 19')", true);
            }
        }


 

        // Body Category - Others

        //BMI


        int bmi_val;
        bmi_val = Convert.ToInt32(BMI.Text);

        if (bmi_val > 25)
        {
            //Fetch VH 
            try
            {
                con_Body.Open();
                cmd = new SqlCommand("select Message from BMI where BMI = 'VH'", con_Body);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_BMI.Text = dr[0].ToString();
                    body_vh++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Body.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 26')", true);
            }
        }
        else if (bmi_val < 19)
        {
            //Fetch VL
            try
            {
                con_Body.Open();
                cmd = new SqlCommand("select Message from BMI where BMI = 'VL'", con_Body);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_BMI.Text = dr[0].ToString();
                    body_vl++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Body.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 27')", true);
            }

        }
        else
        {
            //Fetch OK
            try
            {
                con_Body.Open();
                cmd = new SqlCommand("select Message from BMI where BMI = 'OK'", con_Body);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_BMI.Text = dr[0].ToString();
                    body_ok++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Body.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 28')", true);
            }

        }

        // Fat Percentage


        int fat_percent_val;
        fat_percent_val = Convert.ToInt32(Fat_Percent.Text);

        if (fat_percent_val > 66)
        {
            //Fetch VH 
            try
            {
                con_Body.Open();
                cmd = new SqlCommand("select Message from Fat_Percent where Fat_Percent = 'VH'", con_Body);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Fat_Percent.Text = dr[0].ToString();
                    body_vh++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Body.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 29')", true);
            }
        }
        else if (fat_percent_val < 33)
        {
            //Fetch VL
            try
            {
                con_Body.Open();
                cmd = new SqlCommand("select Message from Fat_Percent where Fat_Percent = 'VL'", con_Body);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Fat_Percent.Text = dr[0].ToString();
                    body_vl++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Body.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 30')", true);
            }

        }
        else
        {
            //Fetch OK
            try
            {
                con_Body.Open();
                cmd = new SqlCommand("select Message from Fat_Percent where Fat_Percent = 'OK'", con_Body);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Fat_Percent.Text = dr[0].ToString();
                    body_ok++;
                }
                dr.Dispose();
                cmd.Dispose();
                con_Body.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 31')", true);
            }

        }

        // Set overall reports

        // Nutrition Overall

        if (nutrition_vh > nutrition_vl)
        {
            
            //Fetch VH
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Overall_Nutrition where Nutrition = 'VH'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Nutrition.Text = dr[0].ToString();

                    // Store it in Std_Nutrition DB using Date

                    SqlCommand cmd1 = new SqlCommand();
                    try
                    {
                        con_Users.Open();
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.CommandText = "Store_Nutrition";

                        cmd1.Parameters.AddWithValue("@UserID", GlobalVariables.guserID);
                        cmd1.Parameters.AddWithValue("@Date_Entered", dt);
                        cmd1.Parameters.AddWithValue("@Value", "VH");


                        cmd1.Connection = con_Users;
                        cmd1.ExecuteNonQuery();

                        cmd1.Dispose();
                        con_Users.Close();
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 35')", true);
                    }
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 32')", true);
            }
        }
        else if (nutrition_vh < nutrition_vl)
        {
            //Fetch VL
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Overall_Nutrition where Nutrition = 'VL'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Nutrition.Text = dr[0].ToString();
                    // Store it in Std_Nutrition DB using Date

                    SqlCommand cmd1 = new SqlCommand();
                    try
                    {
                        con_Users.Open();
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.CommandText = "Store_Nutrition";

                        cmd1.Parameters.AddWithValue("@UserID", GlobalVariables.guserID);
                        cmd1.Parameters.AddWithValue("@Date_Entered", dt);
                        cmd1.Parameters.AddWithValue("@Value", "VL");


                        cmd1.Connection = con_Users;
                        cmd1.ExecuteNonQuery();

                        cmd1.Dispose();
                        con_Users.Close();
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 36')", true);
                    }
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 33')", true);
            }
        }
        else 
        {
            //Fetch OK
            try
            {
                con_nutrition.Open();
                cmd = new SqlCommand("select Message from Overall_Nutrition where Nutrition = 'OK'", con_nutrition);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Nutrition.Text = dr[0].ToString();
                    // Store it in Std_Nutrition DB using Date

                    SqlCommand cmd1 = new SqlCommand();
                    try
                    {
                        con_Users.Open();
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.CommandText = "Store_Nutrition";

                        cmd1.Parameters.AddWithValue("@UserID", GlobalVariables.guserID);
                        cmd1.Parameters.AddWithValue("@Date_Entered", dt);
                        cmd1.Parameters.AddWithValue("@Value", "OK");


                        cmd1.Connection = con_Users;
                        cmd1.ExecuteNonQuery();

                        cmd1.Dispose();
                        con_Users.Close();
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 37')", true);
                    }
                }
                dr.Dispose();
                cmd.Dispose();
                con_nutrition.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 34')", true);
            }
        }
        // Exercise Overall

        if (exercise_vh > exercise_vl)
        {
            //Fetch VH
            try
            {
                con_Exercise.Open();
                cmd = new SqlCommand("select Message from Overall_Exercise where Exercise = 'VH'", con_Exercise);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Exercise.Text = dr[0].ToString();
                    // Store it in Std_Exercise DB using Date

                    SqlCommand cmd1 = new SqlCommand();
                    try
                    {
                        con_Users.Open();
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.CommandText = "Store_Exercise";

                        cmd1.Parameters.AddWithValue("@UserID", GlobalVariables.guserID);
                        cmd1.Parameters.AddWithValue("@Date_Entered", dt);
                        cmd1.Parameters.AddWithValue("@Value", "VH");


                        cmd1.Connection = con_Users;
                        cmd1.ExecuteNonQuery();

                        cmd1.Dispose();
                        con_Users.Close();
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 39')", true);
                    }
                }
                dr.Dispose();
                cmd.Dispose();
                con_Exercise.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 38')", true);
            }
        }
        else if (exercise_vh < exercise_vl)
        {
            //Fetch VL
            try
            {
                con_Exercise.Open();
                cmd = new SqlCommand("select Message from Overall_Exercise where Exercise = 'VL'", con_Exercise);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Exercise.Text = dr[0].ToString();
                    // Store it in Std_Exercise DB using Date

                    SqlCommand cmd1 = new SqlCommand();
                    try
                    {
                        con_Users.Open();
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.CommandText = "Store_Exercise";

                        cmd1.Parameters.AddWithValue("@UserID", GlobalVariables.guserID);
                        cmd1.Parameters.AddWithValue("@Date_Entered", dt);
                        cmd1.Parameters.AddWithValue("@Value", "VL");


                        cmd1.Connection = con_Users;
                        cmd1.ExecuteNonQuery();

                        cmd1.Dispose();
                        con_Users.Close();
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 41')", true);
                    }
                }
                dr.Dispose();
                cmd.Dispose();
                con_Exercise.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 40')", true);
            }
        }
        else
        {
            //Fetch OK
            try
            {
                con_Exercise.Open();
                cmd = new SqlCommand("select Message from Overall_Exercise where Exercise = 'OK'", con_Exercise);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Exercise.Text = dr[0].ToString();
                    // Store it in Std_Exercise DB using Date

                    SqlCommand cmd1 = new SqlCommand();
                    try
                    {
                        con_Users.Open();
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.CommandText = "Store_Exercise";

                        cmd1.Parameters.AddWithValue("@UserID", GlobalVariables.guserID);
                        cmd1.Parameters.AddWithValue("@Date_Entered", dt);
                        cmd1.Parameters.AddWithValue("@Value", "OK");


                        cmd1.Connection = con_Users;
                        cmd1.ExecuteNonQuery();

                        cmd1.Dispose();
                        con_Users.Close();
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 43')", true);
                    }
                }
                dr.Dispose();
                cmd.Dispose();
                con_Exercise.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 42')", true);
            }
        }

   
        // Body Overall

        if ((body_vh > body_vl) && (body_vh > body_ok))
        {
            //Fetch VH
            try
            {
                con_Body.Open();
                cmd = new SqlCommand("select Message from Overall_Body where Body = 'VH'", con_Body);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Body.Text = dr[0].ToString();
                    // Store it in Std_Body DB using Date

                    SqlCommand cmd1 = new SqlCommand();
                    try
                    {
                        con_Users.Open();
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.CommandText = "Store_Body";

                        cmd1.Parameters.AddWithValue("@UserID", GlobalVariables.guserID);
                        cmd1.Parameters.AddWithValue("@Date_Entered", dt);
                        cmd1.Parameters.AddWithValue("@Value", "VH");


                        cmd1.Connection = con_Users;
                        cmd1.ExecuteNonQuery();

                        cmd1.Dispose();
                        con_Users.Close();
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 51')", true);
                    }
                }
                dr.Dispose();
                cmd.Dispose();
                con_Body.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 50')", true);
            }
        }
        else if ((body_vl > body_vh) && (body_vl > body_ok))
        {
            //Fetch VL
            try
            {
                con_Body.Open();
                cmd = new SqlCommand("select Message from Overall_Body where Body = 'VL'", con_Body);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Report_Body.Text = dr[0].ToString();
                    // Store it in Std_Body DB using Date

                    SqlCommand cmd1 = new SqlCommand();
                    try
                    {
                        con_Users.Open();
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.CommandText = "Store_Body";

                        cmd1.Parameters.AddWithValue("@UserID", GlobalVariables.guserID);
                        cmd1.Parameters.AddWithValue("@Date_Entered", dt);
                        cmd1.Parameters.AddWithValue("@Value", "VL");


                        cmd1.Connection = con_Users;
                        cmd1.ExecuteNonQuery();

                        cmd1.Dispose();
                        con_Users.Close();
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 53')", true);
                    }
                }
                dr.Dispose();
                cmd.Dispose();
                con_Body.Close();
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 52')", true);
            }
        }
        else
           
            {
                //Fetch OK
                try
                {
                    con_Body.Open();
                    cmd = new SqlCommand("select Message from Overall_Body where Body = 'OK'", con_Body);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        Report_Body.Text = dr[0].ToString();
                        // Store it in Std_Body DB using Date

                        SqlCommand cmd1 = new SqlCommand();
                        try
                        {
                            con_Users.Open();
                            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd1.CommandText = "Store_Body";

                            cmd1.Parameters.AddWithValue("@UserID", GlobalVariables.guserID);
                            cmd1.Parameters.AddWithValue("@Date_Entered", dt);
                            cmd1.Parameters.AddWithValue("@Value", "OK");


                            cmd1.Connection = con_Users;
                            cmd1.ExecuteNonQuery();

                            cmd1.Dispose();
                            con_Users.Close();
                        }
                        catch (Exception)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 55')", true);
                        }
                    }
                    dr.Dispose();
                    cmd.Dispose();
                    con_Body.Close();
                }
                catch (Exception)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 54')", true);
                }
            }

 
        // Calculate health %

        // Nutrition
        switch (nutrition_vh)
        {
            case 8: health_nutrition = 0.00;
                break;
            case 7: health_nutrition = 12.50;
                break;
            case 6: health_nutrition = 25.00;
                break;
            case 5: health_nutrition = 47.50;
                break;
            case 4: health_nutrition = 50.00;
                break;
            case 3: health_nutrition = 62.50;
                break;
            case 2: health_nutrition = 75.00;
                break;
            case 1: health_nutrition = 87.50;
                break;
            case 0: health_nutrition = 100.00;
                break;
            default: health_nutrition = 0.00;
                break;
        }
        // Exercise
        switch (exercise_vl)
        {
            case 6: health_exercise = 0.00;
                break;
            case 5: health_exercise = 16.66;
                break;
            case 4: health_exercise = 33.33;
                break;
            case 3: health_exercise = 50.00;
                break;
            case 2: health_exercise = 66.00;
                break;
            case 1: health_exercise = 83.33;
                break;
            case 0: health_exercise = 100.00;
                break;
            default: health_exercise = 0.00;
                break;
        }
        // Body
        switch (body_vh)
        {
            case 2: health_body = 0.00;
                break;
            case 1: 
                if( body_ok == 1 )
                {
                    health_body = 100.00;
                }
                else
                {
                    health_body = 50.00;
                }
                break;
            case 0: if (body_vl != 2)
                {
                    health_body = 100.00;
                }
                else
                {
                    if (body_ok == 1)
                    {
                        health_body = 50.00;
                    }
                    else
                    {
                        health_body = 0.00;
                    }
                }
                break;
            default: health_body = 0.00;
                break;
        }
        
  

        //Calculate the calculated_nutrition

        // Retrieve old_health %
        try
        {
            con_Users.Open();
            SqlCommand cmd2 = new SqlCommand("select Health_Percent from User_Data where UserID = " + GlobalVariables.guserID + "", con_Users);
            dr = cmd2.ExecuteReader();
            if (dr.Read())
            {
                old_health = Convert.ToDouble(dr[0].ToString());

                // Calculate and store new health%
                dr.Close();
                calculated_health = (health_body + health_exercise + health_nutrition ) / 3;
                new_health = (calculated_health + old_health) / 2;

                SqlCommand cmd5 = new SqlCommand();

                cmd5.CommandType = System.Data.CommandType.StoredProcedure;
                cmd5.CommandText = "Update_Health";

                cmd5.Parameters.AddWithValue("@UserID", GlobalVariables.guserID);
                cmd5.Parameters.AddWithValue("@Health", new_health);

                cmd5.Connection = con_Users;
                cmd5.ExecuteNonQuery();

                cmd5.Dispose();

                Health_Percent_Display.Text = "Your present average Health Percent is " + new_health.ToString() + " %";

            }

            dr.Close();
            cmd2.Dispose();
            con_Users.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Network Error on 62')", true);
        }

    }
    protected void Fill_Data(object sender, EventArgs e)
    {
        int uid, uid_no;
        Decimal ud;
        string s1;
       
        // Caffeine
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT Caffeine FROM [iassistdev].[HData]", con_iassist);
                
            s1 =  cmd_iassist.ExecuteScalar().ToString();
                s1 = s1.Substring(0, 4);
                ud = Convert.ToDecimal(s1);
                uid_no = GetNumberOfDigits(ud);
                s1 = s1.Substring(0, uid_no);
                uid = Convert.ToInt32(s1);
                uid = Convert.ToInt32(uid / 2.2);
               Caffeine.Text = uid.ToString();
            
          
            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful - caffeine')", true);
        }
        // Active Calories
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT ActiveCalories FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid * 4.18);
            Active_Calories.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-active calories')", true);
        }

        // Carbohydrates
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT Carbohydrates FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid / 2.2);
           Carbohydrates.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-carbohydrates')", true);
        }

        // Resting Calories
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT RestingCalories FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid * 4.18);
            Resting_Calories.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-resting calories')", true);
        }

        // Cycling Distance
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT CyclingDistance FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid / 1.09);
            Cycling_Distance.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-cycling')", true);
        }
        // Flights Climbed
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT FlightsClimbed FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid * 1.09);
            Flights_Climbed.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-flights climbed')", true);
        }
        // Steps
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT Steps FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 5);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid * 2.26);
            Steps.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-steps')", true);
        }
        // Iodine
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT Iodine FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid / 2.2);
            Iodine.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-iodine')", true);
        }
        // Calcium
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT Calcium FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid / 2.2);
           Calcium.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-calcium')", true);
        }
        // Total Fat
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT TotalFat FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid / 2.2);
            Total_Fat.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-total fat')", true);
        }

        // Sugar
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT Sugar FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid / 2.2);
            Sugar.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-sugar')", true);
        }
        // Fat Percent
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT FatPercent FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid * 2.83);
            Fat_Percent.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-fat percent')", true);
        }
        // WalkRunDistance
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT WalkRunDistance FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid / 1.09);
           Walk_Run_Distance.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-walk run')", true);
        }
        // Fiber
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT Fiber FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 4);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            uid = Convert.ToInt32(uid / 2.2);
           Fibre.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-fiber')", true);
        }
        // BMI
        try
        {
            con_iassist.Open();
            cmd_iassist = new SqlCommand("SELECT Bmi FROM [iassistdev].[HData]", con_iassist);

            s1 = cmd_iassist.ExecuteScalar().ToString();
            s1 = s1.Substring(0, 3);
            ud = Convert.ToDecimal(s1);
            uid_no = GetNumberOfDigits(ud);
            s1 = s1.Substring(0, uid_no);
            uid = Convert.ToInt32(s1);
            BMI.Text = uid.ToString();


            cmd_iassist.Dispose();
            con_iassist.Close();
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('unsuccessful-bmi')", true);
        }
           
    }
    static int GetNumberOfDigits(decimal d)
    {
        decimal abs = Math.Abs(d);

        return abs < 1 ? 0 : (int)(Math.Log10(decimal.ToDouble(abs)) + 1);
    }
}