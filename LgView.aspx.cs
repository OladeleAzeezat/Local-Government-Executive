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
using System.Drawing;

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
                txtLg.Text = Session["lg"].ToString();
                LgLabel.Text = Session["lg"].ToString();
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


    //protected void Button1_Click(object sender, EventArgs e)
    //{
       
    //    GvbindAll();
    //    //GvbindWard();
    //    //GvbindPu();
    //}

    //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    string conn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
    //    using (SqlConnection scon = new SqlConnection(conn))
    //    {
    //        scon.Open();
    //        SqlCommand cmd = new SqlCommand("delete from LgExcoTb  where lg=@lg and card =@card", scon);
    //        cmd.Parameters.AddWithValue("pos", GridView1.DataKeys[e.RowIndex].Value.ToString());
    //        cmd.Parameters.AddWithValue("@lg", txtLg.Text);
    //        int t = cmd.ExecuteNonQuery();
    //        if (t > 0)
    //        {
    //            showAlert(GridView1.DataKeys[e.RowIndex].Value.ToString() + " Result has been deleted");
    //            GridView1.EditIndex = -1;
    //            GvbindAll();
    //        }
    //    }

    //}
}