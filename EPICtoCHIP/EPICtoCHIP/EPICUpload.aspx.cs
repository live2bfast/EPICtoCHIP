using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EPICUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

 

    protected void EPICUpload_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        String todayDate = DateTime.Now.ToShortDateString();
        todayDate = todayDate.Replace('/', '-');
        string Filepath = "C:/ConversionFiles/EPICFile" + todayDate + ".csv";
        EPICFileUpload.SaveAs(Filepath);
        Session["EPICFilepath"] = Filepath;
        
    }

 

    protected void nxtPage_Click(object sender, EventArgs e)
    {
        if (Session["EPICFilePath"] != null) { Response.Redirect("EPICMatch.aspx"); }
        else
        { errorLbl.Visible = true; }
        
    }

}