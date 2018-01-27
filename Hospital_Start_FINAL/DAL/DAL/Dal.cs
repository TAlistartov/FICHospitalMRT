 using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace DAL
{
    //Here we check - reading string value ==null. If true then return "", else return value which  we have read
    public static class DataReaderExtensions
    {
        public static string GetStringOrNull(this IDataReader reader, int numofcolumn)
        {
            return reader.IsDBNull(numofcolumn) ? null : reader.GetString(numofcolumn);
        }

        public static int? GetIntOrNull(this IDataReader reader,int numofcolumn)
        {
            return reader.IsDBNull(numofcolumn) ? (int?) null : reader.GetInt32(numofcolumn);
        }
       

        //public static string GetStringOrNull(this IDataReader reader, string columnName)
        //{
        //    return reader.GetStringOrNull(reader.GetOrdinal(columnName));
        //}
    }

    public static class DataWriteExtensions
    {
        public static System.Data.SqlTypes.SqlString WriteValueOrNull(string ValueForCheck)
        {
            var sqldatenull = System.Data.SqlTypes.SqlString.Null;
            return String.IsNullOrEmpty(ValueForCheck) ? sqldatenull : (System.Data.SqlTypes.SqlString)ValueForCheck;
        }

        public static System.Data.SqlTypes.SqlDateTime WriteValueOrNull (DateTime? ValueForCheck)
        {
            var sqldatenull = System.Data.SqlTypes.SqlDateTime.Null;
            return (ValueForCheck == DateTime.MinValue) ? sqldatenull : (System.Data.SqlTypes.SqlDateTime)ValueForCheck;
        }

    }
    public class Dal
    {
        // String for the connection to the DB
        string ConnecString = @"Data Source=PAVLOG;Initial Catalog=Hospital;Integrated Security=False;User ID=sa;Password=sa; Pooling=true;";
        //string ConnecString = @"Data Source=PVKIP05;Initial Catalog=Hospital;Integrated Security=True;";

//------------------------------------START WORK WITH MAIN TABLE OF PATIENT------------->>>>>>>>>>>>>>>>>>>>>>>>>>> 
        public IEnumerable<DalPatients> DalGetAllPatient()
        {
            List<DalPatients> AllPatients = new List<DalPatients>();
            DateTime? date;
            string query = String.Format("SELECT * FROM Patient");
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand(query, con);
               try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                            date = dr.IsDBNull(5) ?(DateTime?)null :dr.GetDateTime(5);
                           
                        DalPatients PatientFromDB = new DalPatients()
                        {
                            Pat_Id = dr.GetInt32(0),
                            Pat_LastName = dr.GetStringOrNull(1),
                            Pat_FirstName = dr.GetStringOrNull(2),
                            Pat_MiddleName = dr.GetStringOrNull(3),
                            Pat_CellPhone = dr.GetStringOrNull(4),
                            Pat_ConvertedBirthDate = (date.HasValue)? String.Format("{0:dd.MM.yyyy}", date):"",
                            Pat_Adress = dr.GetStringOrNull(6),//dr.GetString(6),
                            Pat_JobPlace = dr.GetStringOrNull(7),
                            Pat_Email = dr.GetStringOrNull(8),
                            Pat_Note = dr.GetStringOrNull(9)


                        };
                        AllPatients.Add(PatientFromDB);
                    }
                }
                else return null;
                }
                catch(Exception)
                {

                }
            }
            return AllPatients;
        }

        public bool DalUpdateSelectedPatient(DalPatients patients)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("UpdateSelectedPatient", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@patientId";
                param.Value = patients.Pat_Id;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientLastName";
                param.Value = DataWriteExtensions.WriteValueOrNull(patients.Pat_LastName);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientFirstName";
                param.Value = DataWriteExtensions.WriteValueOrNull(patients.Pat_FirstName);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientMiddleName";
                param.Value = DataWriteExtensions.WriteValueOrNull( patients.Pat_MiddleName);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientCellPhone";
                param.Value = DataWriteExtensions.WriteValueOrNull( patients.Pat_CellPhone);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

               // var sqldatenull = System.Data.SqlTypes.SqlDateTime.Null;

                param = new SqlParameter();
                param.ParameterName = "@patientBirthDate";
                param.Value = DataWriteExtensions.WriteValueOrNull(patients.Pat_BirthDateToDB);//(patients.BirthDateOfPatienttoDB== DateTime.MinValue) ? sqldatenull : (System.Data.SqlTypes.SqlDateTime)patients.BirthDateOfPatienttoDB;
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientAdress";
                param.Value = DataWriteExtensions.WriteValueOrNull(patients.Pat_Adress);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientJobPlace";
                param.Value = DataWriteExtensions.WriteValueOrNull(patients.Pat_JobPlace);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientEmail";
                param.Value = DataWriteExtensions.WriteValueOrNull(patients.Pat_Email);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientNote";
                param.Value = DataWriteExtensions.WriteValueOrNull(patients.Pat_Note);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    if (com.ExecuteNonQuery() == 1)
                        flagresult = true;
                }
                catch (Exception)
                {

                }
            }

            
                return flagresult;
        }

        public bool DalSaveNewPatient(DalPatients patient)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("AddPatientToList", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@patientLastName";
                param.Value = DataWriteExtensions.WriteValueOrNull(patient.Pat_LastName);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientFirstName";
                param.Value = DataWriteExtensions.WriteValueOrNull(patient.Pat_FirstName);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientMiddleName";
                param.Value = DataWriteExtensions.WriteValueOrNull(patient.Pat_MiddleName);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientCellPhone";
                param.Value = DataWriteExtensions.WriteValueOrNull(patient.Pat_CellPhone);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientBirthDate";
                param.Value = DataWriteExtensions.WriteValueOrNull(patient.Pat_BirthDateToDB);
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientAdress";
                param.Value = DataWriteExtensions.WriteValueOrNull(patient.Pat_Adress);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientJobPlace";
                param.Value = DataWriteExtensions.WriteValueOrNull(patient.Pat_JobPlace);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientEmail";
                param.Value = DataWriteExtensions.WriteValueOrNull(patient.Pat_Email);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@patientNote";
                param.Value = DataWriteExtensions.WriteValueOrNull(patient.Pat_Note);
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    if (com.ExecuteNonQuery() == 1)
                        flagresult = true;
                }
                catch (Exception)
                {

                }
            }
                return flagresult;
        }

        public bool DalDeleteSelectedPatient(int patientID)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("DeletePatientFromList", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@IdPatient";
                param.Value = patientID;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    flagresult = (com.ExecuteNonQuery() == 1) ? true : false;

                }
                catch (Exception)
                {

                }
            }

            return flagresult;
        }
//<<<<<<<<<<<<<<<<<<<<<<<<------------END WORK WITH MAIN TABLE OF PATIENT----------------------------------------

//------------------------------------START WORK WITH TABLE OF PREORDER MAGASINE------------->>>>>>>>>>>>>>>>>>>>>>>>>>> 
        public IEnumerable<DalMagasines> DalGetAllPatientByDateForPreorderMagasine(DateTime? VisitDate)
        {
            List<DalMagasines> AllPatientToPreorderMagasine = new List<DalMagasines>();
            DateTime? birthdate;
            DateTime? visitdate;
            string finalcost;
            string item;

            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("GetAllInformatienOfPatientForPreorderMagasine", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param = new SqlParameter();
                param.ParameterName = "@DateOnPreordering";
                param.Value = DataWriteExtensions.WriteValueOrNull(VisitDate);
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);
                try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            birthdate = dr.IsDBNull(8) ? (DateTime?)null : dr.GetDateTime(8);
                            visitdate = dr.IsDBNull(0) ? (DateTime?)null : dr.GetDateTime(0);

                            item = dr.GetValue(13).ToString().Replace(".", ",").Trim();

                            DalMagasines dalpatienttopreordermagasine = new DalMagasines()
                            {
                                //Fields for Patient Information
                                Pat_Id = dr.GetInt32(7),
                                Pat_LastName = dr.GetStringOrNull(1),
                                Pat_FirstName = dr.GetStringOrNull(2),
                                Pat_MiddleName = dr.GetStringOrNull(3),
                                Pat_CellPhone = dr.GetStringOrNull(4),
                                Pat_ConvertedBirthDate = (birthdate.HasValue) ? String.Format("{0:dd.MM.yyyy}", birthdate) : "",
                                Pat_Adress = dr.GetStringOrNull(9),//dr.GetString(6),
                                Pat_JobPlace = dr.GetStringOrNull(10),
                                Pat_Email = dr.GetStringOrNull(11),
                                Pat_Note = dr.GetStringOrNull(6),
                                St_Id = dr.GetInt32(12),

                                //Fields for Visit
                                ConvertedTimeOfVisit = (visitdate.HasValue) ? String.Format("{0:HH:mm}", visitdate) : "",
                                ConvertedFullVisitDate = (visitdate.HasValue) ? String.Format("{0:dd.MM.yyyy HH:mm}", visitdate) : "",
                                //IsPreorder=dr.GetBoolean(),
                                //IsNeedSendEmail

                                //Fields for Study
                                St_Type = dr.GetStringOrNull(5),
                                ConvertedStudyCost = item

                            };
                            AllPatientToPreorderMagasine.Add(dalpatienttopreordermagasine);
                        }
                    }
                    else return AllPatientToPreorderMagasine;
                }
                catch (Exception)
                {

                }
            }
            return AllPatientToPreorderMagasine;
        }

        public bool DalAddNewPatientToPreorderMagasine(int PatientId, int StudyId, DateTime DateAndTimeOfPreordering, bool IsPreorder,
                                                        bool IsNeedSendEmail, bool IsHasVisited, bool IsCash)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {

                SqlCommand com = new SqlCommand("AddNewPatientToPreorderMagasine", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();

                param.ParameterName = "@PatientId";
                param.Value = PatientId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@StudyId";
                param.Value = StudyId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@DateAndTimeOfPreordering";
                param.Value = DateAndTimeOfPreordering;
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@IsPreorder";
                param.Value = IsPreorder;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@IsNeedSendEmail";
                param.Value = IsNeedSendEmail;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@IsHasVisited";
                param.Value = IsHasVisited;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@IsCash";
                param.Value = IsCash;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    if (com.ExecuteNonQuery() == 4)
                        
                        flagresult = true;
                }
                catch (Exception)
                {

                }

            }
            return flagresult;
        }

        public bool DalUpdateNoteOfPreorderMagasine(int PatientId,int StudyId,int NewStudyId,DateTime DateOfVisit,DateTime NewDateOfVisit)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("UpdateDataOfPatientInPreorderMagasine", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@PatientId";
                param.Value = PatientId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@OldStudyId";
                param.Value = StudyId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@NewStudyId";
                param.Value = NewStudyId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@OldTimeOfVisit";
                param.Value = DataWriteExtensions.WriteValueOrNull(DateOfVisit);
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@NewTimeOfVisit";
                param.Value = DataWriteExtensions.WriteValueOrNull(NewDateOfVisit);
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    if (com.ExecuteNonQuery() == 2)
                        flagresult = true;
                }
                catch (Exception)
                {

                }
            }


            return flagresult;
        }
        
        //This method using for deliting note of patient from preorder and infact magasine
        public bool DalDeleteNoteFromPreorderMagasine(int StudyId,DateTime preorderdate,int PatientId,string nameofstoredprocedure, int countofdelitingrows)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {

                SqlCommand com = new SqlCommand(nameofstoredprocedure, con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();

                param.ParameterName = "@PatientId";
                param.Value = PatientId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@StudyId";
                param.Value = StudyId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@TimeOfVisit";
                param.Value = preorderdate;
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    if (com.ExecuteNonQuery() == countofdelitingrows)

                        flagresult = true;
                }
                catch (Exception)
                {

                }

            }
            return flagresult;
        }
//<<<<<<<<<<<<<<<<<<<<<<<<------------END WORK WITH TABLE OF PREORDER MAGASINE----------------------------------------


//------------------------------------START WORK WITH TABLE OF IN FACT MAGASINE------------->>>>>>>>>>>>>>>>>>>>>>>>>>>
        public IEnumerable<DalMagasines> DalGetAllPatientByDateForMagasineInFact(DateTime? VisitDate)
        {
            List<DalMagasines> AllPatientInFactMagasine = new List<DalMagasines>();
            DateTime? birthdate;
            DateTime? visitdate;
            string finalcost;
            string item;

            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("GetAllInformatienForInFactMagasine", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param = new SqlParameter();
                param.ParameterName = "@DateForInFactMagasine";
                param.Value = DataWriteExtensions.WriteValueOrNull(VisitDate);
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);
                try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            birthdate = dr.IsDBNull(6) ? (DateTime?)null : dr.GetDateTime(6);
                            visitdate = dr.IsDBNull(12) ? (DateTime?)null : dr.GetDateTime(12);

                            finalcost = dr.GetValue(16).ToString();
                                if(finalcost!="")
                                    finalcost.Replace(".", ",").Trim();

                            item = dr.GetValue(22).ToString().Replace(".", ",").Trim();

                        DalMagasines dalpatienttoinfactmagasine = new DalMagasines()
                            {
                                //Fields for Patient Information
                                Pat_Id = dr.GetInt32(0),
                                Pat_LastName = dr.GetStringOrNull(1),
                                Pat_FirstName = dr.GetStringOrNull(2),
                                Pat_MiddleName = dr.GetStringOrNull(3),
                                Pat_CellPhone = dr.GetStringOrNull(4),
                                Pat_Note = dr.GetStringOrNull(5),
                                Pat_Adress = dr.GetStringOrNull(7),
                                Pat_JobPlace = dr.GetStringOrNull(8),
                                Pat_Email = dr.GetStringOrNull(9),

                                //Fields for Study
                                St_Id = dr.GetInt32(10),
                                St_Type = dr.GetStringOrNull(11),

                                //Fields for Visit                                
                                IsNeedSendEmail = dr.GetBoolean(13),

                                //Fields for Payments
                                IsCash = dr.GetBoolean(14),
                                NoteForDiscount = dr.GetStringOrNull(15),

                                //Fields for TheStudyProcess
                                DoctorData = dr.GetStringOrNull(17),

                                //Fields for Laborant
                                LaborantId = dr.GetIntOrNull(18),
                                LaborantData = dr.GetStringOrNull(19),

                                IsHasVisited = dr.GetBoolean(20),
                                IsPreorder = dr.GetBoolean(21),
                                St_Cost = float.Parse(item),


                            Pat_ConvertedBirthDate = (birthdate.HasValue) ? String.Format("{0:dd.MM.yyyy}", birthdate) : "",
                            ConvertedTimeOfVisit = (visitdate.HasValue) ? String.Format("{0:HH:mm}", visitdate) : "",
                            ConvertedFullVisitDate = (visitdate.HasValue) ? String.Format("{0:dd.MM.yyyy HH:mm}", visitdate) : "",
                            ConvertFinalCost = finalcost
                            
                        };
                            AllPatientInFactMagasine.Add(dalpatienttoinfactmagasine);
                        }
                    }
                    else return AllPatientInFactMagasine;
                }
                catch (Exception)
                {

                }
            }
            return AllPatientInFactMagasine;
        }
        public bool DalAddNewPatientToInFactMagasine(int PatientId, int StudyId, DateTime DateAndTimeOfPreordering, bool IsPreorder,
                                                            bool IsNeedSendEmail, bool IsHasVisited, bool IsCash,
                                                                int LaborantId1, int? LaborantId2, float FinalCost,
                                                                    string NoteForDiscount, string DoctorData)
        {
            bool flagresult = false;
            int countofchangedrows;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {

                SqlCommand com = new SqlCommand("AddNewPatientToInFactMagasine", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();

                param.ParameterName = "@PatientId";
                param.Value = PatientId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@StudyId";
                param.Value = StudyId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@DateAndTimeOfPreordering";
                param.Value = DateAndTimeOfPreordering;
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@IsPreorder";
                param.Value = IsPreorder;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@IsNeedSendEmail";
                param.Value = IsNeedSendEmail;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@IsHasVisited";
                param.Value = IsHasVisited;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@IsCash";
                param.Value = IsCash;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@LaborantId1";
                param.Value = LaborantId1;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@LaborantId2";
                param.Value = LaborantId2;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@FinalCost";
                param.Value = FinalCost;
                param.SqlDbType = System.Data.SqlDbType.Float;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@NoteForDiscount";
                param.Value = NoteForDiscount;
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@DoctorData";
                param.Value = DoctorData;
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                if (LaborantId2 != null)
                    countofchangedrows = 5;
                else
                    countofchangedrows = 4;

                try
                {
                    con.Open();
                    if (com.ExecuteNonQuery() == countofchangedrows)

                        flagresult = true;
                }
                catch (Exception)
                {

                }

            }
            return flagresult;
        }
        public bool DalUpdateNoteOfInFactMagasine(int PatientId, int StudyId, int NewStudyId, DateTime DateAndTimeOfPreordering, 
                                                       bool IsNeedSendEmail, bool IsCash,int LaborantId1,int? NewLaborantId1, int? LaborantId2, 
                                                            int? NewLaborantId2,float FinalCost, string NoteForDiscount, string DoctorData)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("UpdateDataOfPatientInInFactMagasine", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@PatientId";
                param.Value =PatientId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@OldStudyId";
                param.Value = StudyId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@NewStudyId";
                param.Value = NewStudyId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@OldTimeOfVisit";
                param.Value = DataWriteExtensions.WriteValueOrNull(DateAndTimeOfPreordering);
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@IsNeedSendEmail";
                param.Value = IsNeedSendEmail;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@IsCash";
                param.Value = IsCash;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@LaborantId1";
                param.Value = LaborantId1;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@NewLaborantId1";
                param.Value = NewLaborantId1;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@LaborantId2";
                param.Value = LaborantId2;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@NewLaborantId2";
                param.Value = NewLaborantId2;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@FinalCost";
                param.Value = FinalCost;
                param.SqlDbType = System.Data.SqlDbType.Float;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@NoteForDiscount";
                param.Value = NoteForDiscount;
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@DoctorData";
                param.Value = DoctorData;
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    int counter = com.ExecuteNonQuery();
                    if (counter == 4 || counter == 5)
                    flagresult = true;
                }
                catch (Exception)
                {

                }
            }


            return flagresult;
        }

        //For Deliting note from InFactMagasine, used deliting method from PreorderMagasine

//<<<<<<<<<<<<<<<<<<<<<<<<------------END WORK WITH TABLE OF IN FACT MAGASINE----------------------------------------


//-------------------------------------START WORK WITH STUDIES-------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ------------------------------------Get all Studies-----------------------------------------------
        public IEnumerable <DalStudy> GetAllStudies()
               {
                  List <DalStudy>studies = new List<DalStudy>();
                string item;
                  
                  string query = String.Format("SELECT * FROM Study"); 

                  using (SqlConnection con = new SqlConnection(ConnecString))
                    {
                        SqlCommand com=new SqlCommand(query,con);

                        try
                        {
                            con.Open();
                            SqlDataReader dr = com.ExecuteReader();

                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    item=dr.GetValue(2).ToString().Replace(".",",").Trim();
                                    
                                    DalStudy dalStudy = new DalStudy()
                                    {
                                        St_Id =dr.GetInt32(0),
                                        St_Type = dr.GetString(1),
                                        St_Cost = float.Parse(item),
                                        St_Used = dr.GetBoolean(3)
                                    };
                                    
                                    studies.Add(dalStudy);
                                }
                       // return studies;

                    }
                            else return null;
                        }
                        catch (Exception)
                        {
                            
                        }
                    }

            return studies;
        }

        public IEnumerable<DalStudy> DalGetUsedStudies()
        {
            List<DalStudy> UsedStudies = new List<DalStudy>();
            string item;

            string query = String.Format("SELECT Id,Type,Cost FROM Study WHERE UsedOrNot=(1)");

            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = dr.GetValue(2).ToString().Replace(".", ",").Trim();

                            DalStudy dalStudy = new DalStudy()
                            {
                                St_Id = dr.GetInt32(0),
                                St_Type = dr.GetString(1),
                                St_Cost = float.Parse(item),
                                //Used = dr.GetBoolean(3)
                            };

                            UsedStudies.Add(dalStudy);
                        }
                        // return studies;

                    }
                    else return null;
                }
                catch (Exception)
                {

                }
            }

            return UsedStudies;
        }


        //----------------------------------Create a new Study----------------------------------------------
        public bool SaveNewStudy(string type, float cost, bool used)
               {
                    bool flagresult = false;
                    using (SqlConnection con = new SqlConnection(ConnecString))
                    {

                        SqlCommand com = new SqlCommand("AddNewStudy", con);
                        com.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@Type";
                        param.Value = type;
                        param.SqlDbType = System.Data.SqlDbType.NVarChar;
                        param.Direction = System.Data.ParameterDirection.Input;
                        com.Parameters.Add(param);

                        param = new SqlParameter();
                        param.ParameterName = "@Cost";
                        param.Value = cost;
                        param.SqlDbType = System.Data.SqlDbType.Decimal;
                        param.Direction = System.Data.ParameterDirection.Input;
                        com.Parameters.Add(param);

                        param = new SqlParameter();
                        param.ParameterName = "@UsedOrNot";
                        param.Value = used;
                        param.SqlDbType = System.Data.SqlDbType.Bit;
                        param.Direction = System.Data.ParameterDirection.Input;
                        com.Parameters.Add(param);

                try
                        {
                            con.Open();
                            if (com.ExecuteNonQuery() == 1)
                                flagresult = true;
                        }
                        catch (Exception)
                        {

                        }

                     }

                            return flagresult;
                 }

        //-------------------------------------Delete a Study from the DB-------------------------------------
            public bool DeleteStudy(int studyID)
                    {
                        bool flagresult = false;
                        using (SqlConnection con = new SqlConnection(ConnecString))
                        {
                            SqlCommand com = new SqlCommand("DeleteStudyFromList", con);
                            com.CommandType = System.Data.CommandType.StoredProcedure;

                            SqlParameter param = new SqlParameter();
                            param.ParameterName = "@IdStudy";
                            param.Value = studyID;
                            param.SqlDbType = System.Data.SqlDbType.Int;
                            param.Direction = System.Data.ParameterDirection.Input;
                            com.Parameters.Add(param);

                            try
                            {
                                con.Open();
                                flagresult = (com.ExecuteNonQuery() == 1)? true : false;

                            }
                            catch (Exception)
                            {

                            }
                        }

                        return flagresult;
                    }

        //---------------------Add or Delete selected study from list of using Studies------------------------
        public bool DalUseOrUnuseSelectedStudy(int studyId, bool UseOrUnuse)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("UsingOrUnusingStudy", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@studyId";
                param.Value = studyId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@studyUsedOrUnused";
                param.Value = UseOrUnuse;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    flagresult = (com.ExecuteNonQuery() == 1) ? true : false;
                }
                catch (Exception)
                {

                }
            }
            return flagresult;
        }

        public bool DalChangeDataOfSelectedStudy (int studyId, string studyType, float studyCost, bool studyUsed)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("UpdateSelectedStudy", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@studyId";
                param.Value = studyId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@studyType";
                param.Value = studyType;
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@studyCost";
                param.Value = studyCost;
                param.SqlDbType = System.Data.SqlDbType.Real;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@studyUsedOrNot";
                param.Value = studyUsed;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    flagresult = (com.ExecuteNonQuery() == 1) ? true : false;
                }
                catch (Exception)
                {

                }
            }
            return flagresult;
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<--------END WORK WITRH STUDIES-------------------------------------------


//-------------------------------------START WORK WITH LABORANTS-------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // ------------------------------------Get all Laborants-----------------------------------------------

        public IEnumerable <DalLaborant> GetAllLaborants()
        {
            List<DalLaborant> laborants = new List<DalLaborant> ();
            string query = String.Format("SELECT * FROM Laborant");
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DalLaborant dallaborant = new DalLaborant()
                            {
                                IdOfLaborant = dr.GetInt32(0),
                                LastNameOfLaborant = dr.GetString(1),
                                FirstNameOfLaborant = dr.GetString(2),
                                UsedOrUnusedLaborant=dr.GetBoolean(3)
                            };
                            laborants.Add(dallaborant);
                        }
                    }
                    else return null;
                }
                catch (Exception)
                {

                }
            }
                return (laborants);
        }
        public IEnumerable<DalLaborant> DalGetUsedLaborants()
        {
            List<DalLaborant> laborants = new List<DalLaborant>();
            string query = String.Format("SELECT Id,LastName,FirstName FROM Laborant WHERE UsedOrNot=(1)");
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            DalLaborant dallaborant = new DalLaborant()
                            {
                                IdOfLaborant = dr.GetInt32(0),
                                LastNameOfLaborant = dr.GetString(1),
                                FirstNameOfLaborant = dr.GetString(2),
                                //UsedOrUnusedLaborant = dr.GetBoolean(3)
                            };
                            laborants.Add(dallaborant);
                        }
                    }
                    else return null;
                }
                catch (Exception)
                {

                }
            }
            return (laborants);
        }

        public bool SaveNewLaborant (string lastNameOfLaborant, string firstNameOfLaborant, bool convertedUsedLaborant)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("AddLaborantsToList", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();

                param.ParameterName = "@Last_Name";
                param.Value = lastNameOfLaborant;
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@First_Name";
                param.Value = firstNameOfLaborant;
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@laborantUsed";
                param.Value = convertedUsedLaborant;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    if (com.ExecuteNonQuery() == 1)
                        flagresult = true;
                }
                catch (Exception)
                {

                }

            }
                return flagresult;
        }

        public bool DeleteLaborant(int laborantId)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("DeleteLaborantsFromList",con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@IdLaborant";
                param.Value = laborantId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    flagresult = (com.ExecuteNonQuery() == 1) ? true : false;
                }
                catch(Exception)
                {

                }
            }
                return flagresult;
        }

        public bool ChangeDataOfSelectedLaborant (int laborantId, string laborantLastName,
                                                          string laborantFirstName, bool laborantUsed)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("UpdateSelectedLaborant", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@laborantId";
                param.Value = laborantId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@laborantLastName";
                param.Value = laborantLastName;
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@laborantFirstName";
                param.Value = laborantFirstName;
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@laborantUsed";
                param.Value = laborantUsed;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    flagresult = (com.ExecuteNonQuery() == 1) ? true : false;
                }
                catch(Exception)
                {

                }

            }
            return flagresult;
        }

        //Add or Delete selected laborant from list of using Laborants
        public bool DalUseOrUnuseSelectedLaborant(int laborantId,bool UseOrUnuse)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("UsingOrUnusingLaborant", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@laborantId";
                param.Value = laborantId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@laborantUsedOrUnused";
                param.Value = UseOrUnuse;
                param.SqlDbType = System.Data.SqlDbType.Bit;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    flagresult = (com.ExecuteNonQuery() == 1) ? true : false;
                }
                catch (Exception)
                {

                }
            }
                return flagresult;
        }
        //-------------------------------------END WORK WITH LABORANTS-------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


        //Counting money per day
        public List<DalChargesByDate> DalGetAllChargesByDate(DateTime chargesbydate)
        {
            List<DalChargesByDate> dalchargesbydate = new List<DalChargesByDate>();
            string item;
            DateTime? datecharge;

            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("GetAllChargesByDate", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param = new SqlParameter();
                param.ParameterName = "@DateCharge";
                param.Value = DataWriteExtensions.WriteValueOrNull(chargesbydate);
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);
                try
                {
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            datecharge = dr.IsDBNull(3) ? (DateTime?)null : dr.GetDateTime(3);
                            item = dr.GetValue(2).ToString().Replace(".", ",").Trim();

                            DalChargesByDate temp = new DalChargesByDate()
                            {
                                ChargeId=dr.GetInt32(0),
                                ChargeNote=dr.GetStringOrNull(1),
                                ConvertedCostCharge = item,
                                ConvertChargesByDate = datecharge.ToString()
                            };
                            dalchargesbydate.Add(temp);
                        }
                    }
                    else return dalchargesbydate;
                }
                catch (Exception)
                {

                }
            }

            return (dalchargesbydate);
        }
        public bool DalAddNewCharge(string chargenote, float costcharge, DateTime chargesbydate)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("AddNewCharge", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();

                param.ParameterName = "@ChargeNote";
                param.Value = chargenote;
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@CostCharge";
                param.Value = costcharge;
                param.SqlDbType = System.Data.SqlDbType.Float;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@DateCharge";
                param.Value = chargesbydate;
                param.SqlDbType = System.Data.SqlDbType.DateTime;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    if (com.ExecuteNonQuery() == 1)
                        flagresult = true;
                }
                catch (Exception)
                {

                }

            }
            return flagresult;
        }
        public bool DalUpdateSelectedCharge(int ChargeId, string ChargeNote, float CostCharge)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("UpdateSelectedCharge", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@SelectedChargeId";
                param.Value = ChargeId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@ChargeNote";
                param.Value = ChargeNote;
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@CostCharge";
                param.Value = CostCharge;
                param.SqlDbType = System.Data.SqlDbType.Float;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    if (com.ExecuteNonQuery() == 1)
                        flagresult = true;
                }
                catch (Exception)
                {

                }
            }


            return flagresult;
        }
        public bool DalDeleteSelectedCharge(int ChargeId)
        {
            bool flagresult = false;
            using (SqlConnection con = new SqlConnection(ConnecString))
            {
                SqlCommand com = new SqlCommand("DeleteSelectedCharge", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@SelectedChargeId";
                param.Value = ChargeId;
                param.SqlDbType = System.Data.SqlDbType.Int;
                param.Direction = System.Data.ParameterDirection.Input;
                com.Parameters.Add(param);

                try
                {
                    con.Open();
                    flagresult = (com.ExecuteNonQuery() == 1) ? true : false;
                }
                catch (Exception)
                {

                }
            }
            return flagresult;
        }
    }
   
}


