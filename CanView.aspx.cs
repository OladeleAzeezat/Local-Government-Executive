using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Reflection.Emit;

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString);
    private void showNotificationSuccess(string msg)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "success", "alertify.notify('" + msg + "','success',7);", true);
    }
    private void showError(string msg)
    {
        errorPanel.Visible = true;
        ErrorLabel.Text = msg;
        ClientScript.RegisterStartupScript(this.GetType(), "success", "alertify.alert('" + msg + "');", true);
    }
    private void showAlert(string msg)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "success", "alertify.alert('" + msg + "');", true);
    }
    private void hideError()
    {
        errorPanel.Visible = false;
        ErrorLabel.Text = "";
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        // Check if the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            // Redirect to the login page
            Response.Redirect("~/Default.aspx");
        }

        //Session to Call LG from Dashboard
        if (!IsPostBack)
        {
            // Retrieve the username from the session variable and display it in the textbox
            if (Session["lg"] != null)
            {
                LgList.Text = Session["lg"].ToString();
                LgLabel.Text = Session["lg"].ToString();
            }
        }


        //load ward from DB       
        if (!Page.IsPostBack)
        {
            try
            {
                // Retrieve value from session
                string inputValue = Session["lg"] as string;
                // Response.Write("Input value: " + inputValue);
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select distinct WTitle,WCode from WardTable where LgCode=(select distinct LgCode from LgTable where LgTitle=@LgCode) order by WTitle asc", conn);
                    cmd.Parameters.AddWithValue("LgCode", inputValue);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem itm = new ListItem();
                        itm.Value = dr["WCode"].ToString();
                        itm.Text = dr["WTitle"].ToString().ToUpper().Trim();
                        WardList.Items.Add(itm);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }


        GvbindAll();
        
    }
   

    protected void GvbindAll()
    {
        string conn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
        using (SqlConnection scon = new SqlConnection(conn))
        {
            scon.Open();
            SqlCommand cmd = new SqlCommand("Select pu as 'Polling Unit', pos as 'Submitted By', name as 'Name',age as 'Age', num as 'Number', card as 'Voter Card' from CanTable where lg=@lg", scon);
            cmd.Parameters.AddWithValue("@lg", LgList.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
        }

    }

    protected void GvbindWard()
    {
        string conn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
        using (SqlConnection scon = new SqlConnection(conn))
        {
            scon.Open();
            SqlCommand cmd = new SqlCommand("Select ward as 'Ward', pu as 'Polling Unit', pos as 'Submitted By', name as 'Name', age as 'Age', num as 'Number', card as 'Voter Card' from CanTable where lg =@lg And ward = @ward", scon);
            cmd.Parameters.AddWithValue("@lg", LgList.Text);
            cmd.Parameters.AddWithValue("@ward", WardList.SelectedItem.ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
        }

    }
    protected void GvbindPu()
    {
        string conn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
        using (SqlConnection scon = new SqlConnection(conn))
        {
            scon.Open();
            SqlCommand cmd = new SqlCommand("Select  pu as 'Polling Unit', pos as 'Submitted By', name as 'Name', age as 'Age', num as 'Number', card as 'Voter Card' from CanTable where lg =@lg And ward = @ward And pu=@pu", scon);
            cmd.Parameters.AddWithValue("@lg", LgList.Text);
            cmd.Parameters.AddWithValue("@ward", WardList.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@pu", Request.Form[PuList.UniqueID].ToString());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
       
       
        GvbindWard();
        GvbindPu();
    }
}