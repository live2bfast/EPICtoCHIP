using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EPICMatch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string urlPth = Session["EPICFilepath"].ToString();
        if (!Page.IsPostBack)
        {
            readFile(urlPth);
        }
    }

    protected void readFile(String path)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("MPI");
        dt.Columns.Add("Name");
        dt.Columns.Add("DOB");
        dt.Columns.Add("Address");
        dt.Columns.Add("City");
        dt.Columns.Add("Zip");
        for (int i = 1; i <= 56; i++)
        {

            dt.Columns.Add("ImmDate" + i.ToString());
            dt.Columns.Add("ImmType" + i.ToString());

        }
        using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (BufferedStream bs = new BufferedStream(fs))
        using (StreamReader sr = new StreamReader(bs))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                object[] currentLine = line.Split(new char[] { '|' });
                string fullName = currentLine[1].ToString();
                fullName = fullName.Trim();
                string[] nameArr = fullName.Split(new char[] { ' ' });
                string fName = nameArr[0].ToString();
                string lName="";
                if (!nameArr[nameArr.Length-1].Contains("JR")){
                     lName = nameArr[nameArr.Length - 1].ToString();}
                    else{
                     lName = nameArr[nameArr.Length - 2].ToString();
                }
                fullName = fName + " " + lName;
                for (int i = 0; i < currentLine.Count(); i++)
                {
                    string x = currentLine.ElementAt(i).ToString();
                    x = x.Trim();
                    x = x.Replace(";", "");
                    currentLine[1] = fullName;
                    //1 to 1 mappings. 1 to many will be handled further down
                    if (x.Equals("DTAP VACCINE"))
                    {
                        currentLine[i] = "Dtap";
                    }
                    if (x.Contains("INFANRIX"))
                    {
                        currentLine[i] = "Dtap";
                    }
                    else if (x.Equals("ACTHIB (HIB)"))
                    {
                        currentLine[i] = "Hib";
                    }
                    else if (x.Equals("HEPATITIS B VACCINE"))
                    {
                        currentLine[i] = "Hbv";
                    }
                    else if (x.Equals("VARICELLA VACCINE LIVE"))
                    {
                        currentLine[i] = "Varicella";
                    }
                    else if (x.Equals("DAPTACEL (DTAP)"))
                    {
                        currentLine[i] = "Dtap";
                    }
                    else if (x.Equals("ROTATEQ (ROTOVIRUS)"))
                    {
                        currentLine[i] = "Rotavirus";
                    }
                    else if (x.Equals("ROTOVIRUS VACCINE"))
                    {
                        currentLine[i] = "Rotavirus";
                    }
                    else if (x.Equals("IPOL (IPV)"))
                    {
                        currentLine[i] = "Ipv";
                    }
                    else if (x.Equals("MMR VACCINE"))
                    {
                        currentLine[i] = "Mmr";
                    }
                    else if (x.Equals("HEPATITIS A VACCINE"))
                    {
                        currentLine[i] = "Hepatitis A";
                    }
                    else if (x.Equals("RECOMBIVAX-PED HEP B"))
                    {
                        currentLine[i] = "Hbv";
                    }
                    else if (x.Equals("PNEUMOCOCCAL VACCINE"))
                    {
                        currentLine[i] = "Prevnar";
                    }
                    else if (x.Equals("INFLUENZA VACCINE WHOLE"))
                    {
                        currentLine[i] = "Flu";
                    }
                    else if (x.Equals("INFLUENZA VACCINE SPLIT"))
                    {
                        currentLine[i] = "Flu";
                    }
                    else if (x.Equals("HIB VACCINE"))
                    {
                        currentLine[i] = "Hib";
                    }
                    else if (x.Equals("MRR/VARICELLA COMBINED VACCINE"))
                    {
                        currentLine[i] = "Varicella";
                    }
                    else if (x.Equals("HEP B - ENGERIX"))
                    {
                        currentLine[i] = "Hbv";
                    }
                    else if (x.Equals("VARIVAX (VARICELLA)"))
                    {
                        currentLine[i] = "Varicella";
                    }
                    else if (x.Equals("VAQTA-PED HEP A"))
                    {
                        currentLine[i] = "Hepatitis A";
                    }
                    else if (x.Equals("IPV"))
                    {
                        currentLine[i] = "Ipv";
                    }
                    else if (x.Equals("FLUMIST (FLU NASAL)"))
                    {
                        currentLine[i] = "Flu";
                    }
                    else if (x.Contains("FLUZONE"))
                    {
                        currentLine[i] = "Flu";
                    }
                    else if (x.Contains("FLUARIX"))
                    {
                        currentLine[i] = "Flu";
                    }
                    else if (x.Contains("PREVNAR"))
                    {
                        currentLine[i] = "Prevnar";
                    }
                    else if (x.Equals("PPD"))
                    {
                        currentLine[i] = "PPD";
                    }
                    else if (x.Contains("SYNAGIS"))
                    {
                        currentLine[i] = "Synagis";
                    }
                }
                dt.Rows.Add(currentLine);
            }
            writeToDB(dt);
        }
    }

    protected void writeToDB(DataTable SourceTable)
    {
        string connectionString;
        connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\EPIC.mdf;Integrated Security=True;Connect Timeout=30";

        //Open a connection with destination database;
        using (SqlConnection connection =
               new SqlConnection(connectionString))
        {
            connection.Open();
            string truncTblQuery = "TRUNCATE TABLE EPICData; TRUNCATE TABLE EPICfile; TRUNCATE TABLE ImmHistory; ";
            SqlCommand cmd = new SqlCommand(truncTblQuery, connection);
            cmd.ExecuteNonQuery();
            //Open bulkcopy connection.
            using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connection))
            {
                //Set destination table name
                //to table previously created.
                bulkcopy.DestinationTableName = "dbo.EPICfile";

                try
                {
                    bulkcopy.WriteToServer(SourceTable);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }

            string query = "exec dbo.InsertEpicData;";
            SqlCommand cmd1 = new SqlCommand(query, connection);
            SqlCommand cmd2 = new SqlCommand(query, connection);
            cmd1.ExecuteNonQuery();
            query = "";
            for (int i = 1; i <= 56; i++)
            {
                query += "INSERT INTO ImmHistory SELECT ImmType" + i.ToString() + ", ImmDate" + i.ToString() + ", Id FROM EPICData WHERE ImmDate" + i.ToString() + " is not null;";
            }
            cmd1 = new SqlCommand(query, connection);
            cmd1.ExecuteNonQuery();
            query = "DELETE FROM ImmHistory WHERE ImmHistDate = '' OR ImmHistType IS NULL;";
            cmd1 = new SqlCommand(query, connection);
            cmd1.ExecuteNonQuery();

            connection.Close();
            processImmHistMultis();
        }
    }

    protected void processImmHistMultis()
    {
        string connectionString;
        connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\EPIC.mdf;Integrated Security=True;MultipleActiveResultSets=true;Connect Timeout=30";

        //Open a connection with destination database;
        using (SqlConnection connection =
               new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlConnection connection1 =
               new SqlConnection(connectionString))
            {
                connection1.Open();
                string query = "";
                SqlCommand cmd1 = new SqlCommand(query, connection);
                SqlCommand cmd2 = new SqlCommand(query, connection);
                query = "select * from ImmHistory where ltrim(rtrim(ImmHistType)) = 'DTAP/HEPB/IPV VACCINE'";
                cmd1 = new SqlCommand(query, connection);
                SqlDataReader reader = cmd1.ExecuteReader();
                string pID = "";
                string immHistDate = "";
                string immHistId = "";
                while (reader.Read())
                {
                    pID = reader["patientId"].ToString().Trim();
                    immHistDate = reader["immHistDate"].ToString().Trim();
                    immHistId = reader["Id"].ToString().Trim();
                    query = "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Dtap','" + immHistDate + "','" + pID + "');";
                    query += "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Hbv','" + immHistDate + "','" + pID + "');";
                    query += "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Ipv','" + immHistDate + "','" + pID + "');";
                    query += "DELETE FROM ImmHistory WHERE Id=" + immHistId + ";";
                    cmd2 = new SqlCommand(query, connection);
                    cmd2.ExecuteNonQuery();
                    connection1.Close();
                }
                reader.Close();
                query = "select * from ImmHistory where ltrim(rtrim(ImmHistType)) = 'DTAP/HIB/IPV VACCINE (PENTACEL)' OR ltrim(rtrim(ImmHistType)) = 'DTAP/HIB/IPV COMBINED VACCINE'";
                cmd1 = new SqlCommand(query, connection);
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    pID = reader["patientId"].ToString().Trim();
                    immHistDate = reader["immHistDate"].ToString().Trim();
                    immHistId = reader["Id"].ToString().Trim();
                    query = "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Dtap','" + immHistDate + "','" + pID + "');";
                    query += "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Hib','" + immHistDate + "','" + pID + "');";
                    query += "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Ipv','" + immHistDate + "','" + pID + "');";
                    query += "DELETE FROM ImmHistory WHERE Id=" + immHistId + ";";
                    cmd2 = new SqlCommand(query, connection);
                    cmd2.ExecuteNonQuery();
                    connection1.Close();
                }
                reader.Close();

                query = "select * from ImmHistory where ltrim(rtrim(ImmHistType)) = 'PROQUAD (MMR / VARIVAX)'";
                cmd1 = new SqlCommand(query, connection);
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    pID = reader["patientId"].ToString().Trim();
                    immHistDate = reader["immHistDate"].ToString().Trim();
                    immHistId = reader["Id"].ToString().Trim();
                    query = "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Mmr','" + immHistDate + "','" + pID + "');";
                    query += "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Varicella','" + immHistDate + "','" + pID + "');";                    
                    query += "DELETE FROM ImmHistory WHERE Id=" + immHistId + ";";
                    cmd2 = new SqlCommand(query, connection);
                    cmd2.ExecuteNonQuery();
                    connection1.Close();
                }
                reader.Close();

                query = "select * from ImmHistory where ltrim(rtrim(ImmHistType)) = 'DTAP/IPV (KINRIX)'";
                cmd1 = new SqlCommand(query, connection);
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    pID = reader["patientId"].ToString().Trim();
                    immHistDate = reader["immHistDate"].ToString().Trim();
                    immHistId = reader["Id"].ToString().Trim();
                    query = "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Dtap','" + immHistDate + "','" + pID + "');";
                    query += "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Ipv','" + immHistDate + "','" + pID + "');";
                    query += "DELETE FROM ImmHistory WHERE Id=" + immHistId + ";";
                    cmd2 = new SqlCommand(query, connection);
                    cmd2.ExecuteNonQuery();
                    connection1.Close();
                }
                reader.Close();


                query = "select * from ImmHistory where ltrim(rtrim(ImmHistType)) = 'HEP B/HIB COMBINED VACCINE'";
                cmd1 = new SqlCommand(query, connection);
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    pID = reader["patientId"].ToString().Trim();
                    immHistDate = reader["immHistDate"].ToString().Trim();
                    immHistId = reader["Id"].ToString().Trim();
                    query = "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Hbv','" + immHistDate + "','" + pID + "');";
                    query += "INSERT INTO ImmHistory (ImmHistType,ImmHistDate,PatientId) VALUES ('Hib','" + immHistDate + "','" + pID + "');";
                    query += "DELETE FROM ImmHistory WHERE Id=" + immHistId + ";";
                    cmd2 = new SqlCommand(query, connection);
                    cmd2.ExecuteNonQuery();
                    connection1.Close();
                }
                reader.Close();
            }

            connection.Close();
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Control ctrl = GridView1.Rows[e.RowIndex].FindControl("ImmType");
        Control ctrl2 = GridView1.Rows[e.RowIndex].FindControl("ImmType2");
        Control ctrl3 = GridView1.Rows[e.RowIndex].FindControl("ImmType3");
        if (ctrl != null)
        {
            DropDownList cb = ctrl as DropDownList;
            String immType = cb.SelectedValue.ToString();
            if (immType == "Please Select") { InputDS.UpdateParameters["ImmType"].DefaultValue = ""; }
            else
            {
                InputDS.UpdateParameters["ImmType"].DefaultValue = immType;
            }
        }
        if (ctrl2 != null)
        {
            DropDownList cb2 = ctrl2 as DropDownList;
            String immType2 = cb2.SelectedValue.ToString();
            if (immType2 == "Please Select") { InputDS.UpdateParameters["ImmType2"].DefaultValue = ""; }
            else
            {
                InputDS.UpdateParameters["ImmType2"].DefaultValue = immType2;
            }
        }
        if (ctrl3 != null)
        {
            DropDownList cb3 = ctrl3 as DropDownList;
            String immType3 = cb3.SelectedValue.ToString();
            if (immType3 == "Please Select") { InputDS.UpdateParameters["ImmType3"].DefaultValue = ""; }
            else
            {
                InputDS.UpdateParameters["ImmType3"].DefaultValue = immType3;
            }
        }
    }

    protected void nxtPage_Click(object sender, EventArgs e)
    {
        Response.Redirect("CHIPUpload.aspx");
    }


}