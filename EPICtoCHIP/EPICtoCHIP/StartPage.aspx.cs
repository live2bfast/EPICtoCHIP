using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class StartPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         string connectionString;
connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\EPIC.mdf;Integrated Security=True;Connect Timeout=30";

//Open a connection with destination database;
using (SqlConnection connection =
       new SqlConnection(connectionString))
{
    connection.Open();
    string truncTblQuery = "TRUNCATE TABLE EPICData; TRUNCATE TABLE EPICfile; TRUNCATE TABLE ImmHistory; TRUNCATE TABLE Matches; TRUNCATE TABLE CHIPfile; TRUNCATE TABLE CHIPData";
    SqlCommand cmd = new SqlCommand(truncTblQuery, connection);
    cmd.ExecuteNonQuery();
    connection.Close();
}
    }

    protected void nxtPage_Click(object sender, EventArgs e)
    {
        Response.Redirect("EPICUpload.aspx");
    }
}