using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Drawing;
using System.Security.Cryptography;
using iTextSharp.text;

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

    }

    //Code to Load the data from the LG Database
    protected void GvbindLg()
    {
        //Connection String
        string conn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
        using (SqlConnection scon = new SqlConnection(conn))
        {
            scon.Open();
            SqlCommand cmd = new SqlCommand("Select * from LgExcoTb", scon);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
            dr.Close();
        }


    }

    //Code to Load the data from the Ward Database
    protected void GvbindWard()
    {
        //Connection String
        string conn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
        using (SqlConnection scon = new SqlConnection(conn))
        {
            scon.Open();
            SqlCommand cmd = new SqlCommand("Select * from WardExcoTb", scon);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
        }

    }
    //Code to Load the data from the Polling Unit Database
    protected void GvbindPu()
    {
        //Connection String
        string conn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
        using (SqlConnection scon = new SqlConnection(conn))
        {
            scon.Open();
            SqlCommand cmd = new SqlCommand("Select * from PuExcoTb", scon);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
        }

    }
    //Code to Load the Data from the Can Database
    protected void GvbindCan()
    {
        //Connection String
        string conn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
        using (SqlConnection scon = new SqlConnection(conn))
        {
            //Connection Open
            scon.Open();
            //Sql Query
            SqlCommand cmd = new SqlCommand("Select * from CanTable", scon);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
        }

    }

    protected void lgbtn_Click(object sender, EventArgs e)
    {
        GvbindLg();
    }
    protected void wdbtn_Click(object sender, EventArgs e)
    {
        GvbindWard();
    }
    protected void pubtn_Click(object sender, EventArgs e)
    {
        GvbindPu();
    }
    protected void cnbtn_Click(object sender, EventArgs e)
    {
        GvbindCan();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Connection String
        string conn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;

        using (SqlConnection scon = new SqlConnection(conn))
        {
            scon.Open();
            SqlTransaction transaction = scon.BeginTransaction(); // Start a transaction

            try
            {
                SqlCommand cmd;

                // Delete from LgExcoTb
                cmd = new SqlCommand("DELETE FROM LgExcoTb WHERE card = @card", scon, transaction);
                cmd.Parameters.AddWithValue("@card", GridView1.DataKeys[e.RowIndex].Value.ToString());
                int t = cmd.ExecuteNonQuery();

                // Delete from WardExcoTb
                cmd = new SqlCommand("DELETE FROM WardExcoTb WHERE card = @card", scon, transaction);
                cmd.Parameters.AddWithValue("@card", GridView1.DataKeys[e.RowIndex].Value.ToString());
                t += cmd.ExecuteNonQuery();
                

                // Delete from PuExcoTb
                cmd = new SqlCommand("DELETE FROM PuExcoTb WHERE card = @card", scon, transaction);
                cmd.Parameters.AddWithValue("@card", GridView1.DataKeys[e.RowIndex].Value.ToString());
                t += cmd.ExecuteNonQuery();
                

                // Delete from CanTable
                cmd = new SqlCommand("DELETE FROM CanTable WHERE card = @card", scon, transaction);
                cmd.Parameters.AddWithValue("@card", GridView1.DataKeys[e.RowIndex].Value.ToString());
                t += cmd.ExecuteNonQuery();
               

                if (t > 0)
                {
                    transaction.Commit(); // Commit the transaction if all DELETE statements succeed
                    showAlert(GridView1.DataKeys[e.RowIndex].Value.ToString() + " details have been deleted");
                    GridView1.EditIndex = -1;
                    //Response.Redirect("~/Admin.aspx");

                }
                else
                {
                    transaction.Rollback(); // Rollback the transaction if any DELETE statement fails
                    showAlert("Error deleting details.");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // Rollback the transaction if an exception occurs
                showAlert("Error: " + ex.Message);
            }
            finally
            {
                scon.Close();
            }
        }
    }


}