using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL;



namespace BLL
{
    public static class Convertation
    {
        public static float ConvStrToFloat(string str)
        {
            //Convert Cost from string to float
            string strcostofcharge = str.Trim().Replace(".", ",");
            float flcostofstring = float.Parse(strcostofcharge);

            return flcostofstring;
        }
    }


    public class Bll
    {
        Dal dal=new Dal();
//------------------------------------START WORK WITH MAIN TABLE OF PATIENT------------->>>>>>>>>>>>>>>>>>>>>>>>>>> 
        public IEnumerable <DalPatients> BllGetAllPatient()
        {
            List<DalPatients> AllPatients = new List<DalPatients>();
            AllPatients = dal.DalGetAllPatient().ToList();
            return AllPatients;
        }

        public bool BllConvertUpdatePatientData(DalPatients patient)
        {
            return dal.DalUpdateSelectedPatient(patient);
        }

        public bool BllConvertSaveNewPatient (DalPatients patient)
        {
            return dal.DalSaveNewPatient(patient);
        }

        public bool BllDeleteSelectedPatient (int patientID)
        {
            return dal.DalDeleteSelectedPatient(patientID);
        }
//<<<<<<<<<<<<<<<<<<<<<<<<------------END WORK WITH MAIN TABLE OF PATIENT----------------------------------------


//--------------------------------------START WORK WITH PREORDER MAGASINE>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public IEnumerable<DalMagasines> BllGetAllPatientByDateForPreorderMagasine(DateTime visitdate)
        {
            List<DalMagasines> preorderbydate = new List<DalMagasines>();
            preorderbydate = dal.DalGetAllPatientByDateForPreorderMagasine(visitdate).ToList();
            return preorderbydate;
        }

        public bool BllAddNewPatientToPreorderMagasine(int PatientId,int StudyId,DateTime DateAndTimeOfPreordering,bool IsPreorder,
                                                        bool IsNeedSendEmail,bool IsHasVisited, bool IsCash)
        {
            return dal.DalAddNewPatientToPreorderMagasine(PatientId, StudyId, DateAndTimeOfPreordering, IsPreorder,
                                                        IsNeedSendEmail, IsHasVisited, IsCash);
        }
        public bool BllUpdateNoteOfPreorderMagasine(int PatientId, int StudyId,int NewStudyId, 
                                                    DateTime VisitDate,DateTime NewVisitDate)
        {
            return dal.DalUpdateNoteOfPreorderMagasine(PatientId, StudyId, NewStudyId, VisitDate, NewVisitDate);
        }
        public bool BllDeleteNoteFromPreorderMagasine(int StudyId,DateTime preorderdate,int PatientId)
        {
            int countofdelitingrows = 4;
            string nameofstoredprocedure = "DeletePatientFromPreorderMagasine";
            return dal.DalDeleteNoteFromPreorderMagasine(StudyId, preorderdate, PatientId, nameofstoredprocedure, countofdelitingrows);
        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<END WORK WITH PREORDER MAGASINE----------------------------------------

//--------------------------------------START WORK WITH IN FACT MAGASINE>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public IEnumerable<DalMagasines> BllGetAllPatientByDateInFactMagasine (DateTime visitdate)
        {
            List<DalMagasines> infactpatientbydate = new List<DalMagasines>();
            infactpatientbydate=dal.DalGetAllPatientByDateForMagasineInFact(visitdate).ToList();
            
            var gr1 = (from a in infactpatientbydate
                       group a by new { a.Pat_Id, a.St_Id } into GroupListPatient
                       select new DalMagasines {
                                    identificatorofgroups = GroupListPatient.Key.Pat_Id,
                                    //groupStudyId=GroupListPatient.Key.StudyId,
                                    groupLastName=GroupListPatient.Select(g=>g.Pat_LastName),
                                    groupFirstName=GroupListPatient.Select(g=>g.Pat_FirstName),
                                    groupMiddleNama=GroupListPatient.Select(g=>g.Pat_MiddleName),
                                    groupCellPhone=GroupListPatient.Select(g=>g.Pat_CellPhone),
                                    groupNote=GroupListPatient.Select(g=>g.Pat_Note),
                                    groupAdress=GroupListPatient.Select(g=>g.Pat_Adress),
                                    groupJobPlace=GroupListPatient.Select(g=>g.Pat_JobPlace),
                                    groupEmail=GroupListPatient.Select(g=>g.Pat_Email),
                                    groupConvertedBirthDate=GroupListPatient.Select(g=>g.Pat_ConvertedBirthDate),
                                    groupStudyId=GroupListPatient.Select(g=>g.St_Id),
                                    groupStudyType=GroupListPatient.Select(g=>g.St_Type),
                                    groupIsNeedSendEmail=GroupListPatient.Select(g=>g.IsNeedSendEmail),
                                    groupIsCash=GroupListPatient.Select(g=>g.IsCash),
                                    groupIsHasVisited=GroupListPatient.Select(g=>g.IsHasVisited),
                                    groupIsPreorder=GroupListPatient.Select(g=>g.IsPreorder),
                                    groupNoteForDiscount=GroupListPatient.Select(g=>g.NoteForDiscount),
                                    groupDoctorData=GroupListPatient.Select(g=>g.DoctorData),
                                    groupLaborantId=GroupListPatient.Select(g=>g.LaborantId),
                                    groupConvertedTimeOfVisit=GroupListPatient.Select(g=>g.ConvertedTimeOfVisit),
                                    groupConvertedFullVisitDate=GroupListPatient.Select(g=>g.ConvertedFullVisitDate),
                                    groupConvertFinalCost=GroupListPatient.Select(g=>g.ConvertFinalCost),
                                    groupLaborantdata = GroupListPatient.Select(g => g.LaborantData),
                                    groupStudyCost=GroupListPatient.Select(g=>g.St_Cost.ToString())});

            var gr2 = (from grouplistpatient in gr1 select new DalMagasines{
                                          Pat_Id =grouplistpatient.identificatorofgroups,
                                          Pat_LastName=grouplistpatient.groupLastName.FirstOrDefault(),
                                          Pat_FirstName=grouplistpatient.groupFirstName.FirstOrDefault(),
                                          Pat_MiddleName=grouplistpatient.groupMiddleNama.FirstOrDefault(),
                                          Pat_CellPhone=grouplistpatient.groupCellPhone.FirstOrDefault(),
                                          Pat_Note=grouplistpatient.groupNote.FirstOrDefault(),
                                          Pat_Adress=grouplistpatient.groupAdress.FirstOrDefault(),
                                          Pat_JobPlace=grouplistpatient.groupJobPlace.FirstOrDefault(),
                                          Pat_Email=grouplistpatient.groupEmail.FirstOrDefault(),
                                          Pat_ConvertedBirthDate=grouplistpatient.groupConvertedBirthDate.FirstOrDefault(),
                                          St_Id=grouplistpatient.groupStudyId.FirstOrDefault(),
                                          St_Type=grouplistpatient.groupStudyType.FirstOrDefault(),
                                          IsNeedSendEmail=grouplistpatient.groupIsNeedSendEmail.FirstOrDefault(),
                                          IsCash=grouplistpatient.groupIsCash.FirstOrDefault(),
                                          IsHasVisited=grouplistpatient.groupIsHasVisited.FirstOrDefault(),
                                          IsPreorder=grouplistpatient.groupIsPreorder.FirstOrDefault(),
                                          NoteForDiscount=grouplistpatient.groupNoteForDiscount.FirstOrDefault(),
                                          DoctorData=grouplistpatient.groupDoctorData.FirstOrDefault(),
                                          LaborantId=grouplistpatient.groupLaborantId.FirstOrDefault(),
                                          ConvertedTimeOfVisit=grouplistpatient.groupConvertedTimeOfVisit.FirstOrDefault(),
                                          ConvertedFullVisitDate=grouplistpatient.groupConvertedFullVisitDate.FirstOrDefault(),
                                          ConvertFinalCost=grouplistpatient.groupConvertFinalCost.FirstOrDefault(),
                                          ConvertedStudyCost = grouplistpatient.groupStudyCost.FirstOrDefault(),                                          
                                          LaborantData = string.Join(", ", grouplistpatient.groupLaborantdata)
            });
            return gr2.ToList();
        }
        public bool BllAddNewPatientToInFactMagasine(int PatientId, int StudyId, DateTime DateAndTimeOfPreordering, bool IsPreorder,
                                                            bool IsNeedSendEmail, bool IsHasVisited, bool IsCash,
                                                                int LaborantId1, int? LaborantId2, float FinalCost,
                                                                    string NoteForDiscount, string DoctorData)
        {
            return dal.DalAddNewPatientToInFactMagasine(PatientId, StudyId, DateAndTimeOfPreordering, IsPreorder,
                                                           IsNeedSendEmail, IsHasVisited, IsCash, LaborantId1,LaborantId2, FinalCost,
                                                               NoteForDiscount,DoctorData);
        }
        public bool BllUpdateNoteOfInFactMagasine(int PatientId, int StudyId, int NewStudyId, DateTime DateAndTimeOfPreordering,
                                                      bool IsNeedSendEmail, bool IsCash, int LaborantId1,int? NewLaborantId1,
                                                      int? LaborantId2, int? NewLaborantId2,float FinalCost, string NoteForDiscount, 
                                                      string DoctorData)
        {
            //int rowscount = (NewLaborantId2 == null) ? 4 : 5;
            return dal.DalUpdateNoteOfInFactMagasine(PatientId, StudyId, NewStudyId, DateAndTimeOfPreordering,
                                                       IsNeedSendEmail, IsCash, LaborantId1,NewLaborantId1,LaborantId2, NewLaborantId2,
                                                            FinalCost, NoteForDiscount, DoctorData);           
        }
        public bool BllDeleteNoteFromInFactMagasine(int StudyId, DateTime preorderdate, int PatientId, bool IsPreorder,
                                                            bool IsHasVisited,string LaborantData)
        {
            string nameofstoredprocedure;
            //If patient was studied by 2 Patients then program should delete 5 rows else 4 rows
            int countofdelitingrows=(LaborantData.Trim().Contains(", "))?5:4;
            
                if (IsPreorder == false && IsHasVisited == true)
                    {
                        nameofstoredprocedure = "DeletePatientFromPreorderMagasine";
                        return dal.DalDeleteNoteFromPreorderMagasine(StudyId, preorderdate, PatientId, nameofstoredprocedure, countofdelitingrows);
                    }
                   
                else if (IsPreorder == true && IsHasVisited == true)
                    {
                        nameofstoredprocedure = "DeleteFromInFactMagasinePartialDeliting";
                        return dal.DalDeleteNoteFromPreorderMagasine(StudyId, preorderdate, PatientId, nameofstoredprocedure, countofdelitingrows);
                    }
                
            else return false;
                
        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<END WORK WITH IN FACT MAGASINE----------------------------------------


//---------------------------------START WORK WITH STUDY TABLE>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        public IEnumerable <DalStudy> GetAllStadies()
        {
            List<DalStudy> studies= new List<DalStudy>();
            studies= dal.GetAllStudies().ToList();
            return studies;
        }

        public IEnumerable <DalStudy> BllGetUsedStudies()
        {
            List<DalStudy> UsedStudies = new List<DalStudy>();
            UsedStudies = dal.DalGetUsedStudies().ToList();
            return UsedStudies;
        }

        public bool BllSaveNewStudy (string type, float cost, bool used)
        {
           return dal.SaveNewStudy(type, cost, used); 
        }

        public bool BllDeleteStudy (string IdStudy)
        {
            int IdStudyConvert;
            IdStudyConvert = int.Parse(IdStudy);
            return dal.DeleteStudy(IdStudyConvert);
        }

        //Add or Delete selected study from list of using Studies
        public bool BllUseOrUnuseSelectedStudy(string studyId, bool UseOrUnuse)
        {
            int convertedStudyId = Int32.Parse(studyId);
            return dal.DalUseOrUnuseSelectedStudy(convertedStudyId, UseOrUnuse);
        }

        public bool BllChangeDataOfSelectedStudy(int studyId, string studyType,float studyCost, bool studyUsedOrUnused)
        {
            return dal.DalChangeDataOfSelectedStudy(studyId, studyType, studyCost, studyUsedOrUnused);
        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<END WORK WITH STUDY TABLE--------------------------------------------------

//---------------------------------START WORK WITH LABORANTS TABLE>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        public IEnumerable<DalLaborant> GetAllLaborants()
        {
            List<DalLaborant> laborants = new List<DalLaborant>();
            laborants = dal.GetAllLaborants().ToList();
            return laborants;
        }

        public IEnumerable<DalLaborant> GetUsedLaborants()
        {
            List<DalLaborant> laborants = new List<DalLaborant>();
            laborants = dal.DalGetUsedLaborants().ToList();
            return laborants;
        }

        //Save new laborant to DB
        public bool SaveNewLaborant(string firstNameOfLaborant,string lastNameOfLaborant, bool usedLaborant)
        {
            bool convertedUsedLaborant = usedLaborant;
            return dal.SaveNewLaborant(firstNameOfLaborant, lastNameOfLaborant, convertedUsedLaborant);
        }

        //Delete laborant from DB
        public bool DeleteLaborantFromDB(string laborantId)
        {
            int IdLaborantConverted = int.Parse(laborantId);
            return dal.DeleteLaborant(IdLaborantConverted);
        }
        
        //Change data of selected laborant
        public bool ChangeDataOfSelectedLaborant (string laborantId, string laborantLastName,
                                                          string laborantFirstName,bool laborantUsed)
        {
            int convertedlaborantId = Int32.Parse(laborantId);
            bool convertedlaborantused = laborantUsed;
            return dal.ChangeDataOfSelectedLaborant(convertedlaborantId, laborantLastName, laborantFirstName, convertedlaborantused);
        }

        //Add or Delete selected laborant from list of using Laborants
        public bool BllUseOrUnuseSelectedLaborant(string laborantId,bool UseOrUnuse)
        {
            int convertedlaborantId = Int32.Parse(laborantId);
            //bool convertedUseOrUnuse = UseOrUnuse;
            return dal.DalUseOrUnuseSelectedLaborant(convertedlaborantId, UseOrUnuse);
        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<END WORK WITH LABORANT TABLE--------------------------------------------------

        public List<DalChargesByDate> BllGetAllChargesByDate(string dateofcharges)
        {
            DateTime shortdate = Convert.ToDateTime(dateofcharges);
            return dal.DalGetAllChargesByDate(shortdate);
        }

        public bool BllAddNewCharge(string chargenote,string convertedcostcharge,string convertchargesbydate)
        {
            //Convert Cost from string to float
            float flcostofstring = Convertation.ConvStrToFloat(convertedcostcharge);
            
            //Convert Charges from string to DateTime
            DateTime chargesbydate = Convert.ToDateTime(convertchargesbydate.Trim());

            return dal.DalAddNewCharge(chargenote, flcostofstring, chargesbydate);
        }
        public bool BllUpdateSelectedCharge(int ChargeId,string ChargeNote,string ConvertedCostCharge)
        {
            //Convert Cost from string to float
            float flcostofstring= Convertation.ConvStrToFloat(ConvertedCostCharge);

            return dal.DalUpdateSelectedCharge(ChargeId, ChargeNote, flcostofstring);
        }
        public bool BllDeleteSelectedCharge(int ChargeId)
        {
            return dal.DalDeleteSelectedCharge(ChargeId);
        }
    }

}
