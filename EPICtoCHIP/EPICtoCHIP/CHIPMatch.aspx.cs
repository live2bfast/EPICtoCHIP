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

public partial class CHIPMatch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string FilePath = Session["CHIPFilePath"].ToString();
        if (!Page.IsPostBack)
        {
            upfile(FilePath);
        }
    }

    protected void upfile(string fPath)
    {

        InputDS.InsertParameters["FilePath"].DefaultValue = fPath;
        InputDS.Insert();
        matchPatients();
    }

    protected void matchPatients()
    {
        string connectionString;
        connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\EPIC.mdf;Integrated Security=True;Connect Timeout=30";

        //Open a connection with destination database;
        using (SqlConnection connection =
               new SqlConnection(connectionString))
        {
            connection.Open();
            string truncTblQuery = "TRUNCATE TABLE Matches; ";
            SqlCommand cmd = new SqlCommand(truncTblQuery, connection);
            cmd.ExecuteNonQuery();
            string runMatches = "Exec dbo.PatientMatches; Exec dbo.PatientNameMatch;";
            cmd = new SqlCommand(runMatches, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        //InputDS.DataBind();
        //GridView1.DataBind();
    }
    protected void genFile_Click(object sender, EventArgs e)
    {

        EPICDataSet1.PatientNoMatchDataTable matchedDT = new EPICDataSet1.PatientNoMatchDataTable();
        EPICDataSet1.SelectImmHistoryDtapDataTable dtapDT = new EPICDataSet1.SelectImmHistoryDtapDataTable();
        EPICDataSet1TableAdapters.SelectImmHistoryDtapTableAdapter dtapTA = new EPICDataSet1TableAdapters.SelectImmHistoryDtapTableAdapter();
        EPICDataSet1.SelectImmHistoryIpvDataTable IpvDT = new EPICDataSet1.SelectImmHistoryIpvDataTable();
        EPICDataSet1TableAdapters.SelectImmHistoryIpvTableAdapter IpvTA = new EPICDataSet1TableAdapters.SelectImmHistoryIpvTableAdapter();
        EPICDataSet1.SelectImmHistoryHibDataTable HibDT = new EPICDataSet1.SelectImmHistoryHibDataTable();
        EPICDataSet1TableAdapters.SelectImmHistoryHibTableAdapter HibTA = new EPICDataSet1TableAdapters.SelectImmHistoryHibTableAdapter();
        EPICDataSet1.SelectImmHistoryMmrDataTable MmrDT = new EPICDataSet1.SelectImmHistoryMmrDataTable();
        EPICDataSet1TableAdapters.SelectImmHistoryMmrTableAdapter MmrTA = new EPICDataSet1TableAdapters.SelectImmHistoryMmrTableAdapter();
        EPICDataSet1.SelectImmHistoryHbvDataTable HbvDT = new EPICDataSet1.SelectImmHistoryHbvDataTable();
        EPICDataSet1TableAdapters.SelectImmHistoryHbvTableAdapter HbvTA = new EPICDataSet1TableAdapters.SelectImmHistoryHbvTableAdapter();
        EPICDataSet1.SelectImmHistoryVarDataTable VarDT = new EPICDataSet1.SelectImmHistoryVarDataTable();
        EPICDataSet1TableAdapters.SelectImmHistoryVarTableAdapter VarTA = new EPICDataSet1TableAdapters.SelectImmHistoryVarTableAdapter();
        EPICDataSet1.SelectImmHistoryPrevDataTable PrevDT = new EPICDataSet1.SelectImmHistoryPrevDataTable();
        EPICDataSet1TableAdapters.SelectImmHistoryPrevTableAdapter PrevTA = new EPICDataSet1TableAdapters.SelectImmHistoryPrevTableAdapter();
        EPICDataSet1.SelectImmHistoryRotDataTable RotDT = new EPICDataSet1.SelectImmHistoryRotDataTable();
        EPICDataSet1TableAdapters.SelectImmHistoryRotTableAdapter RotTA = new EPICDataSet1TableAdapters.SelectImmHistoryRotTableAdapter();
        EPICDataSet1.SelectImmHistoryHepADataTable HepADT = new EPICDataSet1.SelectImmHistoryHepADataTable();
        EPICDataSet1TableAdapters.SelectImmHistoryHepATableAdapter HepATA = new EPICDataSet1TableAdapters.SelectImmHistoryHepATableAdapter();
        EPICDataSet1.SelectImmHistoryFluDataTable FluDT = new EPICDataSet1.SelectImmHistoryFluDataTable();
        EPICDataSet1TableAdapters.SelectImmHistoryFluTableAdapter FluTA = new EPICDataSet1TableAdapters.SelectImmHistoryFluTableAdapter();
        EPICDataSet1.SelectImmHistoryPPDDataTable PPDDT = new EPICDataSet1.SelectImmHistoryPPDDataTable();
        EPICDataSet1TableAdapters.SelectImmHistoryPPDTableAdapter PPDTA = new EPICDataSet1TableAdapters.SelectImmHistoryPPDTableAdapter();
        EPICDataSet1.SelectImmHistorySynagisDataTable SynagisDT = new EPICDataSet1.SelectImmHistorySynagisDataTable();
        EPICDataSet1TableAdapters.SelectImmHistorySynagisTableAdapter SynagisTA = new EPICDataSet1TableAdapters.SelectImmHistorySynagisTableAdapter();

        //before your loop
        var csv = new StringBuilder();
        string header = "#Identifier,Date,Assessment Name,Batch Upload Action,#First Name,#Last Name,#Date of Birth,Dtap 1,Actual Date Dtap1,Dtap2,Actual Date Dtap2,Dtap3,Actual Date Dtap3,Dtap4,Actual Date Dtap4,Dtap5,Actual Date Dtap5,Ipv1,Actual Date Ipv1,Ipv2,Actual Date Ipv2,Ipv3,Actual Date Ipv3,Ipv4,Actual Date Ipv4,Hib1,Actual Date Hib1,Hib2,Actual Date Hib2,Hib3,Actual Date Hib3,Hib4,Actual Date Hib4,Mmr1,Actual Date Mmr1,Mmr2,Actual Date Mmr2,Hbv1,Actual Date Hbv1,Hbv2,Actual Date Hbv2,Hbv3,Actual Date Hbv3,Var1,Actual Date Var1,Var2,Actual Date Var2,Prev1,Actual Date Prev1,Prev2,Actual Date Prev2,Prev3,Actual Date Prev3,Prev4,Actual Date Prev4,Rot1,Actual Date Rot1,Rot2,Actual Date Rot2,Rot3,Actual Date Rot3,HepA1,Actual Date Hep A1,HepA2,Actual Date Hep A2,Flu Date 1,Flu Date 2,Flu Date 3,Flu Date 4,Flu Date 5,Flu Date 6,PPD Date 1,PPD Date 2,PPD Date 3,PPD Date 4,PPD Date 5,PPD Date 6,Synagis Date 1,Synagis Date 2,Synagis Date 3,Synagis Date 4,Synagis Date 5,Synagis Date 6";
        var newline = "";
        csv.Append(header);
        csv.Append(Environment.NewLine);
        
        EPICDataSet1TableAdapters.PatientNoMatchTableAdapter matchTA = new EPICDataSet1TableAdapters.PatientNoMatchTableAdapter();
        matchTA.FillBy(matchedDT);
        string batchDate = DateTime.Today.ToShortDateString();
        string assmtName = "Immunization Assessment";


        foreach (DataRow dr in matchedDT.Rows)
        {
            
            string patientIDStr = dr.ItemArray.ElementAt(0).ToString();
            int patientID = int.Parse(patientIDStr);
            dtapTA.Fill(dtapDT, patientID);
            IpvTA.Fill(IpvDT, patientID);
            HibTA.Fill(HibDT, patientID);
            MmrTA.Fill(MmrDT, patientID);
            HbvTA.Fill(HbvDT, patientID);
            VarTA.Fill(VarDT, patientID);
            PrevTA.Fill(PrevDT, patientID);
            RotTA.Fill(RotDT, patientID);
            HepATA.Fill(HepADT, patientID);
            FluTA.Fill(FluDT, patientID);
            PPDTA.Fill(PPDDT, patientID);
            SynagisTA.Fill(SynagisDT, patientID);
            string responseId = dr.ItemArray.ElementAt(6).ToString();
            string assmtDate = dr.ItemArray.ElementAt(10).ToString();
            string upAction = "INSERT";
            if (!string.IsNullOrEmpty(responseId))
            {
                upAction = "UPDATE";
            }
            if (string.IsNullOrEmpty(assmtDate))
            {
                assmtDate = DateTime.Today.ToShortDateString();
            }
            //string name = dr.ItemArray.ElementAt(3).ToString().Trim();
            //string[] nameList = name.Split(new char[] { ' ' });
            string fName = dr.ItemArray.ElementAt(8).ToString().Trim();
            string lName = dr.ItemArray.ElementAt(9).ToString().Trim();
            string dob = dr.ItemArray.ElementAt(1).ToString();
            string mpi = dr.ItemArray.ElementAt(2).ToString();
            //if (nameList.Count() == 3)
            //{
            //    fName = nameList[0].ToString();
            //    lName = nameList[2].ToString();
            //}
            //else if (nameList.Count() == 2)
            //{
            //    fName = nameList[0].ToString();
            //    lName = nameList[1].ToString();
            //}
            //else if (nameList.Count() == 5)
            //{
            //    if (!nameList[3].Contains("JR"))
            //    {
            //        fName = nameList[0].ToString();
            //        lName = nameList[3].ToString();
            //    }
            //}
            //else if (nameList.Count() == 4)
            //{
            //    if (nameList[3].Contains("JR"))
            //    {
            //        fName = nameList[0].ToString();
            //        lName = nameList[2].ToString();
            //    }
            //}
            string[] dtapDates = new string[5];
            string[] dtapStr = new string[5];
            for (int i = 0; i < 5; i++)
            {
                while (i < dtapDT.Rows.Count & i < 5)
                {
                    dtapDates[i] = dtapDT.Rows[i]["ImmHistDate"].ToString();
                    dtapStr[i] = "Dtap";
                    i++;
                }
            }

            string[] IpvDates = new string[4];
            string[] IpvStr = new string[4];
            for (int i = 0; i < 4; i++)
            {
                while (i < IpvDT.Rows.Count & i < 4)
                {
                    IpvDates[i] = IpvDT.Rows[i]["ImmHistDate"].ToString();
                    IpvStr[i] = "Ipv";
                    i++;
                }
            }

            string[] HibDates = new string[4];
            string[] HibStr = new string[4];
            for (int i = 0; i < 4; i++)
            {
                while (i < HibDT.Rows.Count & i < 4)
                {
                    HibDates[i] = HibDT.Rows[i]["ImmHistDate"].ToString();
                    HibStr[i] = "Hib";
                    i++;
                }
            }

            string[] MmrDates = new string[2];
            string[] MmrStr = new string[2];
            for (int i = 0; i < 2; i++)
            {
                while (i < MmrDT.Rows.Count & i < 2)
                {
                    MmrDates[i] = MmrDT.Rows[i]["ImmHistDate"].ToString();
                    MmrStr[i] = "Mmr";
                    i++;
                }
            }

            string[] HbvDates = new string[3];
            string[] HbvStr = new string[3];
            for (int i = 0; i < 3; i++)
            {
                while (i < HbvDT.Rows.Count & i < 3)
                {
                    HbvDates[i] = HbvDT.Rows[i]["ImmHistDate"].ToString();
                    HbvStr[i] = "Hbv";
                    i++;
                }
            }

            string[] VarDates = new string[2];
            string[] VarStr = new string[2];
            for (int i = 0; i < 2; i++)
            {
                while (i < VarDT.Rows.Count & i < 2)
                {
                    VarDates[i] = VarDT.Rows[i]["ImmHistDate"].ToString();
                    VarStr[i] = "Varicella";
                    i++;
                }
            }

            string[] PrevDates = new string[4];
            string[] PrevStr = new string[4];
            for (int i = 0; i < 4; i++)
            {
                while (i < PrevDT.Rows.Count & i < 4)
                {
                    PrevDates[i] = PrevDT.Rows[i]["ImmHistDate"].ToString();
                    PrevStr[i] = "Prevnar";
                    i++;
                }
            }

            string[] RotDates = new string[3];
            string[] RotStr = new string[3];
            for (int i = 0; i < 3; i++)
            {
                while (i < RotDT.Rows.Count & i < 3)
                {
                    RotDates[i] = RotDT.Rows[i]["ImmHistDate"].ToString();
                    RotStr[i] = "Rotavirus";
                    i++;
                }
            }

            string[] HepADates = new string[2];
            string[] HepAStr = new string[2];
            for (int i = 0; i < 2; i++)
            {
                while (i < HepADT.Rows.Count & i < 2)
                {
                    HepADates[i] = HepADT.Rows[i]["ImmHistDate"].ToString();
                    HepAStr[i] = "Hepatitis A";
                    i++;
                }
            }

            string[] FluDates = new string[6];
            string[] FluStr = new string[6];
            for (int i = 0; i < 6; i++)
            {
                while (i < FluDT.Rows.Count & i < 6)
                {
                    FluDates[i] = FluDT.Rows[i]["ImmHistDate"].ToString();
                    FluStr[i] = "Flu";
                    i++;
                }
            }

            string[] PPDDates = new string[6];
            string[] PPDStr = new string[6];
            for (int i = 0; i < 6; i++)
            {
                while (i < PPDDT.Rows.Count & i < 6)
                {
                    PPDDates[i] = PPDDT.Rows[i]["ImmHistDate"].ToString();
                    PPDStr[i] = "PPD";
                    i++;
                }
            }

            string[] SynagisDates = new string[6];
            string[] SynagisStr = new string[6];
            for (int i = 0; i < 6; i++)
            {
                while (i < SynagisDT.Rows.Count & i < 6)
                {
                    SynagisDates[i] = SynagisDT.Rows[i]["ImmHistDate"].ToString();
                    SynagisStr[i] = "Synagis";
                    i++;
                }
            }
            string fmtStr = "";
            string fmtStr1 = "";
            for (int i = 0; i < 44; i++)
            {
                fmtStr += "{" + i.ToString() + "},";
            }
            fmtStr += "{44}";
            for (int i = 0; i < 38; i++)
            {
                fmtStr1 += "{" + i.ToString() + "},";
            }
            fmtStr1 += "{38}";
            string newline1 = "";
            newline = string.Format(fmtStr, responseId, assmtDate, assmtName, upAction, fName, lName, dob, dtapStr[0], dtapDates[0], dtapStr[1], dtapDates[1], dtapStr[2], dtapDates[2], dtapStr[3], dtapDates[3], dtapStr[4], dtapDates[4], IpvStr[0], IpvDates[0], IpvStr[1], IpvDates[1], IpvStr[2], IpvDates[2], IpvStr[3], IpvDates[3], HibStr[0], HibDates[0], HibStr[1], HibDates[1], HibStr[2], HibDates[2], HibStr[3], HibDates[3], MmrStr[0], MmrDates[0], MmrStr[1], MmrDates[1], HbvStr[0], HbvDates[0], HbvStr[1], HbvDates[1], HbvStr[2], HbvDates[2], VarStr[0], VarDates[0]);
            newline1 = string.Format(fmtStr1, VarStr[1], VarDates[1], PrevStr[0], PrevDates[0], PrevStr[1], PrevDates[1], PrevStr[2], PrevDates[2], PrevStr[3], PrevDates[3], RotStr[0], RotDates[0], RotStr[1], RotDates[1], RotStr[2], RotDates[2], HepAStr[0], HepADates[0], HepAStr[1], HepADates[1], FluDates[0],  FluDates[1],  FluDates[2],  FluDates[3],  FluDates[4],  FluDates[5], PPDDates[0],  PPDDates[1],  PPDDates[2],  PPDDates[3],  PPDDates[4],  PPDDates[5], SynagisDates[0],  SynagisDates[1],  SynagisDates[2],  SynagisDates[3],  SynagisDates[4],  SynagisDates[5], Environment.NewLine);
            csv.Append(newline +  "," + newline1);
        }



        //after your loop
        File.WriteAllText("C:\\ConversionFiles\\Output.csv", csv.ToString());
        Response.Redirect("EndPage.aspx");

    }
    protected void backBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("EpicUpload.aspx");
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Control ctrl = GridView1.Rows[e.RowIndex].FindControl("chipCB");
        if (ctrl != null)
        {
            DropDownList cb = ctrl as DropDownList;
            String ChipId = cb.SelectedValue.ToString();

            String EpicId = GridView1.DataKeys[e.RowIndex].Value.ToString();
            InputDS.UpdateParameters["ChipId"].DefaultValue = ChipId;
            InputDS.UpdateParameters["EpicId"].DefaultValue = EpicId;
        }
    }


    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        InputDS.DataBind();
        GridView1.DataBind();
    }
}
   