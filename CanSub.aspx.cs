using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Utilities.Encoders;
using System.Activities.Expressions;
using System.Configuration;
using System.Xml.Linq;
using System.Drawing;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.ServiceModel.Channels;

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

    void clear()
    {
        this.PosList.SelectedValue = "";
        this.txtCard.Text = "";
        this.txtName.Text = "";
        this.txtAge.Text = "";
        this.txtNumber.Text = "";

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            // Redirect to the login page
            Response.Redirect("~/Default.aspx");
        }

        //load ward from DB       
        if (!Page.IsPostBack)
        {
            try
            {
                // Retrieve value from session
                string inputValue = Session["lg"] as string;
                Response.Write("Input value: " + inputValue);
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

        //Session to Call LG from Dashboard
        if (!IsPostBack)
        {
            // Retrieve the username from the session variable and display it in the textbox
            if (Session["lg"] != null)
            {
                LgLabel.Text = Session["lg"].ToString();
                txtLg.Text = Session["lg"].ToString();
            }
        }

        // Load Positions from Table
        if (!Page.IsPostBack)
        {

            try
            {
                SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString);
                if (conn.State == ConnectionState.Open) conn.Close(); conn.Open();
                SqlCommand cmd = new SqlCommand("select Position from PositionTable order by Position", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListItem itm = new ListItem();
                    itm.Text = dr[0].ToString().ToUpper().Trim();
                    PosList.Items.Add(itm);
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

    //Insert Details into Database
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(mainconn))
            {
                conn.Open(); // Open the database connection

                // Check if the record already exists
                SqlCommand selectCmd = new SqlCommand("SELECT COUNT(*) FROM CanTable WHERE card=@card", conn);
                selectCmd.Parameters.AddWithValue("@card", txtCard.Text);
                int existingRecordsCount = (int)selectCmd.ExecuteScalar();

                if (existingRecordsCount > 0)
                {
                    showAlert("Details exist already");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO CanTable (lg, ward, pu, pos, name,age, num, card) VALUES (@lg, @ward, @pu, @pos, @name, @age, @num, @card)", conn);
                    cmd.Parameters.AddWithValue("@lg", txtLg.Text);
                    cmd.Parameters.AddWithValue("@ward", WardList.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@pos", PosList.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@pu", Request.Form[PuList.UniqueID].ToString());
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@age", txtAge.Text);
                    cmd.Parameters.AddWithValue("@num", txtNumber.Text);
                    cmd.Parameters.AddWithValue("@card", txtCard.Text);
                    cmd.ExecuteNonQuery();

                    showAlert("Details Saved Successfully");

                    clear();
                }
            }


        }
        catch (Exception ex)
        {

            Debug.WriteLine(ex.ToString());

            // Handle the exception or display an error message
            Label1.ForeColor = Color.Red;
            Label1.Text = "An error occurred: " + ex.Message;
        }
    }
}
