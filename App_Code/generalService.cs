using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
/// <summary>
/// Summary description for generalService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class generalService : System.Web.Services.WebService
{

    public generalService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    

    [WebMethod]
    public List<wardList> GetWard(string lg)
    {
        List<wardList> lst = new List<wardList>();
        try
        {
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString);
            if (conn.State == ConnectionState.Open) conn.Close(); conn.Open();
            SqlCommand cmd = new SqlCommand("select distinct WTitle,Wcode from WardTable where LgCode=@LgCode order by WTitle asc", conn);
            cmd.Parameters.AddWithValue("Lgcode", lg);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                wardList s = new wardList();
                s.wardTitle = dr[0].ToString().Trim().ToUpper();
                s.wardCode = dr[1].ToString().Trim().ToUpper();
                lst.Add(s);
            }
            dr.Close();
            if (conn.State == ConnectionState.Open) conn.Close();

        }
        catch { }
        return lst;

    }

    [WebMethod]
    //public List<puList> GetPu(string ward)
    //{
    //    List<puList> lst = new List<puList>();
    //    try
    //    {
    //        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString);
    //        if (conn.State == ConnectionState.Open) conn.Close(); conn.Open();
    //        SqlCommand cmd = new SqlCommand("select distinct PuTitle,PuCode from PuTable where WCode=(select distinct WCode from WardTable where WTitle=@WCode) order by PuTitle asc", conn);
    //        cmd.Parameters.AddWithValue("WCode", ward);
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        while (dr.Read())
    //        {
    //            puList s = new puList();
    //            s.puTitle = dr[0].ToString().Trim().ToUpper();
    //            s.puCode = dr[1].ToString().Trim().ToUpper();
    //            lst.Add(s);
    //        }
    //        dr.Close();
    //        if (conn.State == ConnectionState.Open) conn.Close();

    //    }
    //    catch { }
    //    return lst;

    //}

    //[WebMethod]
    public List<puList> GetPu(string ward)
    {
        List<puList> lst = new List<puList>();
        try
        {
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString);
            if (conn.State == ConnectionState.Open) conn.Close(); conn.Open();
            SqlCommand cmd = new SqlCommand("select distinct PuTitle,Pucode from PuTable where WCode=@WCode order by PuTitle asc", conn);
            cmd.Parameters.AddWithValue("WCode", ward);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                puList s = new puList();
                s.puTitle = dr[0].ToString().Trim().ToUpper();
                s.puCode = dr[1].ToString().Trim().ToUpper();
                lst.Add(s);
            }
            dr.Close();
            if (conn.State == ConnectionState.Open) conn.Close();

        }
        catch { }
        return lst;

    }



}
