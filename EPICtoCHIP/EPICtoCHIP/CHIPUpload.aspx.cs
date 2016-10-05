using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class CHIPUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CHIPUpload_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        String todayDate = DateTime.Now.ToShortDateString();
        todayDate = todayDate.Replace('/', '-');
        string Filepath = "C:/ConversionFiles/CHIPFile" + todayDate + ".csv";
        CHIPFileUpload.SaveAs(Filepath);     
        Session["CHIPFilePath"] = Filepath;

    }

    protected void backBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("EpicMatch.aspx");
    }

    protected void nextPage_Click(object sender, EventArgs e)
    {
        if (Session["CHIPFilePath"] != null) { Response.Redirect("CHIPMatch.aspx"); }
        else
        { errorLbl.Visible = true; }
    }

}