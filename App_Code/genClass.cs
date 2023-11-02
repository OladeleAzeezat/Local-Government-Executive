using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Net;
using System.Globalization;

/// <summary>
/// Summary description for genClass
/// </summary>


public class lgList
{
    public string lgCode { get; set; }
    public string lgTitle { get; set; }

}
public class wardList
{
    public string wardCode { get; set; }
    public string wardTitle { get; set; }

}
public class puList
{
    public string puCode { get; set; }
    public string puTitle { get; set; }


}
public class agList
{
    public string agCode { get; set; }
    public string agTitle { get; set; }


}
