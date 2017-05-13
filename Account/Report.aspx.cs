using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int anInteger;
        anInteger = Convert.ToInt32(TextBox1.Text);
        anInteger = anInteger + 2;
        TextBox1.Text = anInteger.ToString();


    }
}