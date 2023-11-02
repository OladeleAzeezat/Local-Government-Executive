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
using System.Diagnostics;

public partial class _Default : System.Web.UI.Page
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

    //Clear Textbox after submission
    void clear()
    {
        this.PosList.SelectedValue = "";
        this.txtCard.Text = "";
        this.txtName.Text = "";
        this.txtNumber.Text = "";
    }

    //Calling position list from Database
    protected void Page_Load(object sender, EventArgs e)
    {

        // Check if the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            // Redirect to the login page
            Response.Redirect("~/Default.aspx");
        }

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


        //Session to Call LG from Dashboard
        if (!IsPostBack)
        {
            // Retrieve the username from the session variable and display it in the textbox
            if (Session["lg"] != null)
            {
                txtLg.Text = Session["lg"].ToString();
                LgLabel.Text = Session["lg"].ToString();
            }
        }

        // Function to call data from database
        GvbindLg();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(mainconn))
            {
                conn.Open(); // Open the database connection

                // Check if the record already exists
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM LgExcoTb WHERE lg = @lg AND pos = @pos", conn);
                cmd.Parameters.AddWithValue("@lg", txtLg.Text);
                cmd.Parameters.AddWithValue("@pos", PosList.SelectedItem.ToString());
                int existingRecordsCount = (int)cmd.ExecuteScalar();

                if (existingRecordsCount > 0)
                {
                    showAlert(PosList.SelectedItem.ToString() + " information for " + txtLg.Text + " exist");
                }
                else
                {
                    SqlCommand scmd = new SqlCommand("INSERT INTO LgExcoTb (lg, pos, name, num, card) VALUES (@lg, @pos, @name, @num, @card)", conn);
                    scmd.Parameters.AddWithValue("@lg", txtLg.Text);
                    scmd.Parameters.AddWithValue("@pos", PosList.SelectedItem.ToString());
                    scmd.Parameters.AddWithValue("@name", txtName.Text);
                    scmd.Parameters.AddWithValue("@num", txtNumber.Text);
                    scmd.Parameters.AddWithValue("@card", txtCard.Text);
                    scmd.ExecuteNonQuery();

                   
                    showAlert("Details Saved Successfully");

                    clear();
                }
            }


        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());

            // Handle the exception or display an error message
            // Label1.ForeColor = Color.Red;
            //Label1.Text = "An error occurred: " + ex.Message;
        }

    }
    protected void GvbindLg()
    {
        string conn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
        using (SqlConnection scon = new SqlConnection(conn))
        {
            scon.Open();
            SqlCommand cmd = new SqlCommand("Select lg as 'Local Govt', pos as 'Position', name as 'Name', num as 'Number', card as 'Voter Card' from LgExcoTb where lg=@lg", scon);
            cmd.Parameters.AddWithValue("@lg", txtLg.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
        }

    }
}