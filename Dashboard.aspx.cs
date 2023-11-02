using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        // Check if the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            // Redirect to the login page
            Response.Redirect("~/Default.aspx");
        }


        if (!IsPostBack)
        {
            // Retrieve the username from the session variable and display it in the textbox
            if (Session["lg"] != null)
            {
                LgLabel.Text = Session["lg"].ToString();
            }
        }
    }

    protected void btnLgList_Click(object sender, EventArgs e)
    {
        Response.Redirect("LgSub.aspx");
    }

    protected void btnLgView_Click(object sender, EventArgs e)
    {
        Response.Redirect("LgView.aspx");
    }

    protected void btnWaList_Click(object sender, EventArgs e)
    {
        Response.Redirect("WardSub.aspx");
    }

    protected void btnWaView_Click(object sender, EventArgs e)
    {
        Response.Redirect("WardView.aspx");
    }

    protected void btnPuList_Click(object sender, EventArgs e)
    {
        Response.Redirect("PuSub.aspx");
    }

    protected void btnPuView_Click(object sender, EventArgs e)
    {
        Response.Redirect("PuView.aspx");
    }

    
    protected void btnCanList_Click(object sender, EventArgs e)
    {
        Response.Redirect("CanSub.aspx");
    }

    protected void btnCanView_Click(object sender, EventArgs e)
    {
        Response.Redirect("CanView.aspx");
    }
}