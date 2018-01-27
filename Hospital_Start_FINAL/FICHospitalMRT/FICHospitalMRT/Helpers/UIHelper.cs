using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using FICHospitalMRT.Models;
using BLL;
using System.Web.Mvc;

namespace FICHospitalMRT.Helpers
{
    public static class SendItemsFromTo
    {
        public static DalMagasines ReassignmentOfFields(MagasineOfPreorder magasineofpreorder)
        {
            DalMagasines dalpreorder = new DalMagasines();
            dalpreorder.Pat_Id = magasineofpreorder.PatientId;
            dalpreorder.Pat_LastName = magasineofpreorder.LastName;
            dalpreorder.Pat_FirstName = magasineofpreorder.FirstName;
            dalpreorder.Pat_MiddleName = magasineofpreorder.MiddleName;
            dalpreorder.Pat_CellPhone = magasineofpreorder.CellPhone;
            dalpreorder.Pat_BirthDateToDB = Convert.ToDateTime(magasineofpreorder.ConvertedBirthDate);
            dalpreorder.Pat_Adress = magasineofpreorder.Adress;
            dalpreorder.Pat_JobPlace = magasineofpreorder.JobPlace;
            dalpreorder.Pat_Email = magasineofpreorder.Email;
            dalpreorder.Pat_Note = magasineofpreorder.Note;
            dalpreorder.VisitDate = Convert.ToDateTime(magasineofpreorder.ConvertDateOfVisit);
            dalpreorder.NewVisitDate = Convert.ToDateTime(magasineofpreorder.NewConvertDateOfVisit);
            dalpreorder.IsPreorder = magasineofpreorder.IsPreorder;
            dalpreorder.IsNeedSendEmail = magasineofpreorder.IsNeedSendEmail;
            dalpreorder.IsHasVisited = magasineofpreorder.IsHasVisited;
            dalpreorder.St_Id = magasineofpreorder.StudyId;
            dalpreorder.NewStudyId = magasineofpreorder.NewStudyId;
            dalpreorder.IsCash = magasineofpreorder.IsCash;
            return dalpreorder;
        }

        public static DalMagasines ReassignmentOfFields(MagasineInFact magasineinfact)
        {
            DalMagasines dalinfact = new DalMagasines();
            dalinfact.Pat_Id = magasineinfact.PatientId;
            dalinfact.Pat_LastName = magasineinfact.LastName;
            dalinfact.Pat_FirstName = magasineinfact.FirstName;
            dalinfact.Pat_MiddleName = magasineinfact.MiddleName;
            dalinfact.Pat_CellPhone = magasineinfact.CellPhone;
            dalinfact.Pat_BirthDateToDB = Convert.ToDateTime(magasineinfact.ConvertedBirthDate);
            dalinfact.Pat_Adress = magasineinfact.Adress;
            dalinfact.Pat_JobPlace = magasineinfact.JobPlace;
            dalinfact.Pat_Email = magasineinfact.Email;
            dalinfact.Pat_Note = magasineinfact.Note;
            dalinfact.VisitDate = Convert.ToDateTime(magasineinfact.ConvertDateOfVisit);
            //dalinfact.NewVisitDate = Convert.ToDateTime(magasineinfact.NewConvertDateOfVisit);
            dalinfact.IsPreorder = magasineinfact.IsPreorder;
            dalinfact.IsNeedSendEmail = magasineinfact.IsNeedSendEmail;
            dalinfact.IsHasVisited = magasineinfact.IsHasVisited;

            string[] SeparateIdFromCost= magasineinfact.SelectedStudyInFact.Trim().Split(new char[] { '#' });
            dalinfact.St_Id = Int32.Parse(SeparateIdFromCost[0]);

            if (magasineinfact.NewSelectedStudyInFact!=null)
            {
                string[] SeparateIdFromCostNewStudy = magasineinfact.NewSelectedStudyInFact.Trim().Split(new char[] { '#' });
                dalinfact.NewStudyId = Int32.Parse(SeparateIdFromCostNewStudy[0]);
            }
            

            //dalinfact.NewStudyId = magasineinfact.NewStudyId;
            dalinfact.IsCash = magasineinfact.IsCash;

            dalinfact.LaborantId1 = magasineinfact.LaborantId1;
            dalinfact.LaborantId2 = magasineinfact.LaborantId2;
            dalinfact.NewLaborantId1 = magasineinfact.NewLaborantId1;
            dalinfact.NewLaborantId2 = magasineinfact.NewLaborantId2;

            string notconvertfinalcost = (magasineinfact.ConvertedFinalCost.Contains(".") || magasineinfact.ConvertedFinalCost.Contains(",") ?
                                            magasineinfact.ConvertedFinalCost.Replace(".", ",").Trim() :
                                                magasineinfact.ConvertedFinalCost.Trim());

            dalinfact.FinalCost = float.Parse(notconvertfinalcost);
            dalinfact.NoteForDiscount = (magasineinfact.NoteForDiscount== null) ? "": magasineinfact.NoteForDiscount;
            dalinfact.DoctorData = (magasineinfact.DoctorData== null) ? "": magasineinfact.DoctorData;

            return dalinfact;
        }
    }
    public class UIHelper
    {
        
        Bll bll=new Bll();
        DalStudy dalstudy = new DalStudy();
        DalPatients dalpatient = new DalPatients();
        DalMagasines DalMagasines = new DalMagasines();

//-------------------------------------------Start Work With Study>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public IEnumerable<SelectListItem> UIGetUsedStudies()
        {
            var returnSelectListItem = new List<SelectListItem>();
            List<DalStudy> UsedStudies = bll.BllGetUsedStudies().ToList();
            var items = new List<SelectListItem>();


            foreach (var item in UsedStudies)
            {
                {
                    items.Add(new SelectListItem { Value = item.St_Id.ToString(), Text = item.St_Type });
                }
            }
            returnSelectListItem = items;
            return returnSelectListItem;
        }
        public IEnumerable<SelectListItem> UIGetUsedStudiesWithCost()
        {
            var returnSelectListItem = new List<SelectListItem>();
            List<DalStudy> UsedStudies = bll.BllGetUsedStudies().ToList();
            var items = new List<SelectListItem>();


            foreach (var item in UsedStudies)
            {
                {
                    items.Add(new SelectListItem { Value = item.St_Id.ToString() + "#" + item.St_Cost.ToString(), Text = item.St_Type });
                }
            }
            returnSelectListItem = items;
            return returnSelectListItem;
        }
        public bool UIConvertSaveNewStudy(Study newstudy)
        {

            //The 1st paremetr
            string notconvertCost =(newstudy.Cost.ToString().Contains(".")|| newstudy.Cost.ToString().Contains(","))? 
                newstudy.Cost.ToString().Replace(".", ",").Trim(): newstudy.Cost.ToString().Trim();
            dalstudy.St_Cost = float.Parse(notconvertCost);
            
            //The 2dh paremetr
            dalstudy.St_Type = newstudy.Type;

            //The 3dh parametr
            dalstudy.St_Used = newstudy.Used;

            return bll.BllSaveNewStudy(dalstudy.St_Type, dalstudy.St_Cost, dalstudy.St_Used);
        }

        public bool UIConvertSaveNewChangedStudy(StudyChangeData changedstudy)
        {
            //Here we took selected Study (Id#Cost) and separate on Id and Cost
            string[] SeparateIdFromCost = changedstudy.SelectedValue.Trim().Split(new char[] {'#'});
            changedstudy.SelectedValue = SeparateIdFromCost[0];
            //string Cost= SeparateIdFromCost[1];
           // changedstudy.Cost = SeparateIdFromCost[1];

            //The 1st parameter
            //Convertation of Cost form string into float.
            string notconvertcost = (changedstudy.ChangeDataCost.Contains(".") || changedstudy.ChangeDataCost.Contains(",")) ?
                changedstudy.ChangeDataCost.Replace(".", ",").Trim() : changedstudy.ChangeDataCost.Trim();
            dalstudy.St_Cost = float.Parse(notconvertcost);

            //The second parameter
            dalstudy.St_Type = changedstudy.ChangeDataType;

            //The 3dh parametr
            dalstudy.St_Used = changedstudy.Used;

            //The 4th parameter
            dalstudy.St_SelectedStudy = Int32.Parse(changedstudy.SelectedValue);

            return bll.BllChangeDataOfSelectedStudy(dalstudy.St_SelectedStudy,dalstudy.St_Type, dalstudy.St_Cost, dalstudy.St_Used);
        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<End Work With Study--------------------------------------------

//-------------------------------------------Start Work With Laborants>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public IEnumerable<SelectListItem> UIGetAllLaborants ()
        {
            var returnSelectListItem = new List<SelectListItem>();
            List<DalLaborant> laborants = new List<DalLaborant>();
            laborants = bll.GetAllLaborants().ToList();
            var items = new List<SelectListItem>();
            foreach (var item in laborants)
            {
                items.Add(new SelectListItem
                {
                    Value = item.IdOfLaborant.ToString(),
                    Text = item.LastNameOfLaborant + " " + item.FirstNameOfLaborant
                });
                returnSelectListItem = items;
            }
            return returnSelectListItem;
        }
        public IEnumerable<SelectListItem> UIGetUsedLaborants()
        {
            var returnSelectListItem = new List<SelectListItem>();
            List<DalLaborant> laborants = new List<DalLaborant>();
            laborants = bll.GetUsedLaborants().ToList();
            var items = new List<SelectListItem>();
            foreach (var item in laborants)
            {
                items.Add(new SelectListItem
                {
                    Value = item.IdOfLaborant.ToString(),
                    Text = item.LastNameOfLaborant + " " + item.FirstNameOfLaborant
                });
                returnSelectListItem = items;
            }
            return returnSelectListItem;
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<End Work With Laborants--------------------------------------------

//-------------------------------------------Start Work With Patient>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public bool UIConvertUpdatePatientData (Patients patient)
        {
            dalpatient.Pat_Id = patient.PatientId;
            dalpatient.Pat_LastName= patient.LastName;
            dalpatient.Pat_FirstName= patient.FirstName;
            dalpatient.Pat_MiddleName= patient.MiddleName;
            //Converting BirthDate from string to DateTime
            dalpatient.Pat_BirthDateToDB = Convert.ToDateTime(patient.ConvertedBirthDate);
            dalpatient.Pat_CellPhone= patient.CellPhone;
            dalpatient.Pat_Adress= patient.Adress;
            dalpatient.Pat_JobPlace= patient.JobPlace;
            dalpatient.Pat_Email= patient.Email;
            dalpatient.Pat_Note= patient.Note;

            return bll.BllConvertUpdatePatientData(dalpatient);

        }

        public bool UIConvertSaveNewPatient(Patients patient)
        {
            dalpatient.Pat_LastName = patient.LastName;
            dalpatient.Pat_FirstName = patient.FirstName;
            dalpatient.Pat_MiddleName = patient.MiddleName;
            //Converting BirthDate from string to DateTime
            dalpatient.Pat_BirthDateToDB = Convert.ToDateTime(patient.ConvertedBirthDate);
            dalpatient.Pat_CellPhone = patient.CellPhone;
            dalpatient.Pat_Adress = patient.Adress;
            dalpatient.Pat_JobPlace = patient.JobPlace;
            dalpatient.Pat_Email = patient.Email;
            dalpatient.Pat_Note = patient.Note;

            return bll.BllConvertSaveNewPatient(dalpatient);
        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<End Work With Patient------------------------------------------


//-------------------------------------Start Work With Preorder Magasine>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public IEnumerable<DalMagasines> UIConvertGetAllPatientToPreorderMagasineByDate (string DateOfVisit)
        {
            DalMagasines.VisitDate = Convert.ToDateTime(DateOfVisit);
            return bll.BllGetAllPatientByDateForPreorderMagasine(DalMagasines.VisitDate);
        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<End Work With Preorder Magasine--------------------------------------

//-------------------------------------Start Work With In Fact Magasine>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public IEnumerable<DalMagasines> UIConvertGetAllPatientToInFactMagasineByDate (string DateOfVisit)
        {
            DalMagasines.VisitDate = Convert.ToDateTime(DateOfVisit);
            return bll.BllGetAllPatientByDateInFactMagasine(DalMagasines.VisitDate);
        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<End Work With In Fact Magasine--------------------------------------
    }
}