using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace EPICtoCHIP.App_Code
{
    public class Datalayer
    {

        public static EPICDataSet1.SelectEPICInputDataTable GetInput()
        {
            try
            {

                EPICDataSet1.SelectEPICInputDataTable aDataSet = new EPICDataSet1.SelectEPICInputDataTable();
                EPICDataSet1TableAdapters.SelectEPICInputTableAdapter aTableAdapter = new EPICDataSet1TableAdapters.SelectEPICInputTableAdapter();
                aTableAdapter.Fill(aDataSet);
                return aDataSet;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("100:  An error occured while attempting to retrieve the customer comments." + ex);
            }
        }

        public static EPICDataSet1.SelectCHIPDataDataTable GetChipInput()
        {
            try
            {

                EPICDataSet1.SelectCHIPDataDataTable aDataSet = new EPICDataSet1.SelectCHIPDataDataTable();
                EPICDataSet1TableAdapters.SelectCHIPDataTableAdapter aTableAdapter = new EPICDataSet1TableAdapters.SelectCHIPDataTableAdapter();
                aTableAdapter.Fill(aDataSet);
                return aDataSet;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("100:  An error occured while attempting to retrieve the customer comments." + ex);
            }
        }

        public static EPICDataSet1.SelectImmTypeDataTable GetImmType()
        {
            try
            {

                EPICDataSet1.SelectImmTypeDataTable aDataSet = new EPICDataSet1.SelectImmTypeDataTable();
                EPICDataSet1TableAdapters.SelectImmTypeTableAdapter aTableAdapter = new EPICDataSet1TableAdapters.SelectImmTypeTableAdapter();
                aTableAdapter.Fill(aDataSet);
                return aDataSet;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("100:  An error occured while attempting to retrieve the customer comments." + ex);
            }
        }

        public static EPICDataSet1.SelectImmHistoryNoMatchDataTable GetNoImmMatches()
        {
            try
            {

                EPICDataSet1.SelectImmHistoryNoMatchDataTable aDataSet = new EPICDataSet1.SelectImmHistoryNoMatchDataTable();
                EPICDataSet1TableAdapters.SelectImmHistoryNoMatchTableAdapter aTableAdapter = new EPICDataSet1TableAdapters.SelectImmHistoryNoMatchTableAdapter();
                aTableAdapter.Fill(aDataSet);
                return aDataSet;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("100:  An error occured while attempting to retrieve the customer comments." + ex);
            }
        }

        public static EPICDataSet1.PatientNoMatchDataTable GetNoPatientMatches()
        {
            try
            {

                EPICDataSet1.PatientNoMatchDataTable aDataSet = new EPICDataSet1.PatientNoMatchDataTable();
                EPICDataSet1TableAdapters.PatientNoMatchTableAdapter aTableAdapter = new EPICDataSet1TableAdapters.PatientNoMatchTableAdapter();
                aTableAdapter.Fill(aDataSet);
                return aDataSet;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("100:  An error occured while attempting to retrieve the customer comments." + ex);
            }
        }

        public static EPICDataSet1.SelectMatchedRecordsDataTable GetMatchedData()
        {
            try
            {

                EPICDataSet1.SelectMatchedRecordsDataTable aDataSet = new EPICDataSet1.SelectMatchedRecordsDataTable();
                EPICDataSet1TableAdapters.SelectMatchedRecordsTableAdapter aTableAdapter = new EPICDataSet1TableAdapters.SelectMatchedRecordsTableAdapter();
                aTableAdapter.Fill(aDataSet);
                return aDataSet;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("100:  An error occured while attempting to retrieve the customer comments." + ex);
            }
        }

        public static void InsertEpicData(String FilePath)
        {
            try
            {
                EPICDataSet1TableAdapters.QueriesTableAdapter aTableAdapter = new EPICDataSet1TableAdapters.QueriesTableAdapter();
                aTableAdapter.InsertEPICData();
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("100:  An error occured while attempting to retrieve the customer comments." + ex);

            }
        }

        public static void InsertChipData(String FilePath)
        {
            try
            {
                EPICDataSet1TableAdapters.QueriesTableAdapter aTableAdapter = new EPICDataSet1TableAdapters.QueriesTableAdapter();
                aTableAdapter.InsertCHIPData(FilePath);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("100:  An error occured while attempting to retrieve the customer comments." + ex);

            }
        }

        public static void InsertPatientMatch(int EpicId, int ChipId, int Id)
        {
            try
            {
                EPICDataSet1TableAdapters.SelectMatchesTableAdapter aTableAdapter = new EPICDataSet1TableAdapters.SelectMatchesTableAdapter();
                aTableAdapter.Insert(EpicId, ChipId);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("100:  An error occured while attempting to retrieve the customer comments." + ex);

            }
        }

        public static void UpdateImmHistType(int Id, String ImmType, String ImmType2, String ImmType3)
        {
            try
            {
                EPICDataSet1TableAdapters.SelectImmHistoryTableAdapter aTableAdapter = new EPICDataSet1TableAdapters.SelectImmHistoryTableAdapter();
                aTableAdapter.UpdateImmHistType(Id, ImmType, ImmType2, ImmType3);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("100:  An error occured while attempting to retrieve the customer comments." + ex);

            }
        }

    }
}