using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Drawing;
using System.Collections;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Calling Lg from Database Lg Table
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close(); conn.Open();
                SqlCommand cmd = new SqlCommand("select * from LgTable order by LgTitle asc", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListItem itm = new ListItem();
                    itm.Value = dr[0].ToString();
                    itm.Text = dr[1].ToString().ToUpper().Trim();
                    LgList.Items.Add(itm);
                }
                dr.Close();
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

    }


    SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString);


    protected void loginbtn_Click(object sender, EventArgs e)
    {
        try
        {
            // Database login authentication 
            conn.Open();
            string checklogin = "SELECT * FROM Login WHERE Lg = @lg AND password = @password";
            SqlCommand cmd = new SqlCommand(checklogin, conn);
            cmd.Parameters.AddWithValue("@lg", LgList.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@password", passTxt.Text);

            SqlDataAdapter dr = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dr.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                // Authentication successful, create the authentication ticket
                string username = LgList.SelectedItem.ToString();
                bool rememberMe = true; // Set this based on your logic

                // Create the authentication ticket
                var ticket = new FormsAuthenticationTicket(
                   1,                  // version
                   username,           // user name
                   DateTime.Now,       // issue time
                   DateTime.Now.AddMinutes(30),    // expiration time
                   rememberMe,         // whether to persist the cookie
                   "your custom user data"         // additional user data
                );

                // Encrypt the authentication ticket
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Create the authentication cookie
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                // Set the cookie expiration
                if (rememberMe)
                {
                    authCookie.Expires = ticket.Expiration;
                }

                // Add the cookie to the response
                Response.Cookies.Add(authCookie);

                // Set the session variable
                Session["lg"] = LgList.SelectedItem.ToString();

                // Redirect to the desired page
                Response.Redirect("~/Dashboard.aspx");
            }
            else
            {
                Literal1.Text = "Invalid Login";
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }






    //protected void loginbtn_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //Database login authentication 
    //        conn.Open();
    //        string checklogin = "SELECT * FROM Login WHERE Lg = @lg AND password = @password";
    //        SqlCommand cmd = new SqlCommand(checklogin, conn);
    //        cmd.Parameters.AddWithValue("@lg", LgList.SelectedItem.ToString());
    //        cmd.Parameters.AddWithValue("@password", passTxt.Text);

    //        SqlDataAdapter dr = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        dr.Fill(dt);
    //        //session creation
    //        if (dt.Rows.Count > 0)
    //        {
    //            Session["lg"] = LgList.SelectedItem.ToString();
    //            Response.Redirect("Dashboard.aspx");
    //        }
    //        else
    //        {
    //            Literal1.Text = "Invalid Login";

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex);

    //    }

    //}
}