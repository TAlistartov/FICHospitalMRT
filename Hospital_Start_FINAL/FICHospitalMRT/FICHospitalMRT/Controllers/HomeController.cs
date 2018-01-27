using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using FICHospitalMRT.Helpers;
using FICHospitalMRT.Models;
using System.Collections.Specialized;




namespace FICHospitalMRT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Bll bll=new Bll();
        Dal dal=new Dal();
        UIHelper uihelper=new UIHelper();
        Study controlStudy=new Study();
        StudyChangeData controlStudyChangeData = new StudyChangeData();
        Laborants controlLaborants = new Laborants();
        LaborantsChangeData controlLaborantsChangeData = new LaborantsChangeData();
        Patients controlpatient = new Patients();
        MagasineOfPreorder magasineofpreorder = new MagasineOfPreorder();
        MagasineOfPreorder emptyrowofpreorder = new MagasineOfPreorder();

        MagasineInFact magasineinfact = new MagasineInFact();
        MagasineInFact emptyrowinfact = new MagasineInFact();

        MChargesByDate mchargesbydate = new MChargesByDate();

        private static List<Patients> ListOfPatientFromDB;
        private static List<MagasineOfPreorder> PreorderPatientByDate;
        private static List<MagasineInFact> InFactPatientByDate=new List<MagasineInFact>();
        private static List<MChargesByDate> AllChargesPerDay = new List<MChargesByDate>();

        //private static DateTime DateOfPreorderMagasine;
        private static bool ResultOfReadingFromDB;
        
        //Helper for refreshing List of Patients
        private void HelperRefreshListOfPatient()
        {
            List<Patients> FilteringPatient = new List<Patients>();
            var dalgetallpatients = bll.BllGetAllPatient().ToList();
            ResultOfReadingFromDB = false;
            foreach (var item in dalgetallpatients)
            {
                Patients ListOfPatients = new Patients()
                {

                    PatientId = item.Pat_Id,
                    LastName = item.Pat_LastName,
                    FirstName = item.Pat_FirstName,
                    MiddleName = item.Pat_MiddleName,
                    ConvertedBirthDate = item.Pat_ConvertedBirthDate.Trim(),
                    CellPhone = item.Pat_CellPhone,
                    Adress = item.Pat_Adress,
                    JobPlace = item.Pat_JobPlace,
                    Email = item.Pat_Email,
                    Note = item.Pat_Note
                };
                FilteringPatient.Add(ListOfPatients);
            }
            ListOfPatientFromDB = FilteringPatient;
        }
        
        //Help Method for Refreshing Preorder Magasine
        private void HelperRefreshPreorderMagasine(string DateForPreordering)
        {
            List<DalMagasines> dalpreorderbydate = new List<DalMagasines>();
            dalpreorderbydate = uihelper.UIConvertGetAllPatientToPreorderMagasineByDate(DateForPreordering).ToList();
            List<MagasineOfPreorder> preorderbydate = new List<MagasineOfPreorder>();

            foreach (var item in dalpreorderbydate)
            {
                preorderbydate.Add(new MagasineOfPreorder
                {
                    PatientId = item.Pat_Id,
                    LastName = item.Pat_LastName,
                    FirstName = item.Pat_FirstName,
                    MiddleName = item.Pat_MiddleName,
                    CellPhone = item.Pat_CellPhone,
                    ConvertedBirthDate = item.Pat_ConvertedBirthDate,
                    Adress = item.Pat_Adress,
                    JobPlace = item.Pat_JobPlace,
                    Email = item.Pat_Email,
                    Note = item.Pat_Note,
                    StudyType = item.St_Type,
                    ConvertedTimeOfVisit = item.ConvertedTimeOfVisit,
                    StudyId=item.St_Id,
                    ConvertDateOfVisit=item.ConvertedFullVisitDate,
                    ConvertedStudyCost=item.ConvertedStudyCost
                });
            }
            PreorderPatientByDate = preorderbydate;
        }

        //Help method for In Fact Magasine
        private void HelperRefreshInFactMagasine(string DateOfNoteToClinick)
        {
            List<DalMagasines> dalinfactmagasine = new List<DalMagasines>();
            dalinfactmagasine=uihelper.UIConvertGetAllPatientToInFactMagasineByDate(DateOfNoteToClinick).ToList();
            List<MagasineInFact> magasineinfactbydate = new List<MagasineInFact>();
            //int numinorder = 0;

            foreach (var item in dalinfactmagasine)
            {
                //numinorder++;
                magasineinfactbydate.Add(new MagasineInFact
                {
                    // NumberInOrder= numinorder,
                    PatientId = item.Pat_Id,
                    LastName = item.Pat_LastName,
                    FirstName = item.Pat_FirstName,
                    MiddleName = item.Pat_MiddleName,
                    CellPhone = item.Pat_CellPhone,
                    ConvertedBirthDate = item.Pat_ConvertedBirthDate,
                    Adress = item.Pat_Adress,
                    JobPlace = item.Pat_JobPlace,
                    Email = item.Pat_Email,
                    Note = item.Pat_Note,

                    StudyType = item.St_Type,
                    ConvertedTimeOfVisit = item.ConvertedTimeOfVisit,
                    StudyId = item.St_Id,
                    ConvertDateOfVisit = item.ConvertedFullVisitDate,
                    IsCash = item.IsCash,
                    IsNeedSendEmail = item.IsNeedSendEmail,
                    ConvertedFinalCost = item.ConvertFinalCost,
                    NoteForDiscount = item.NoteForDiscount,
                    DoctorData = item.DoctorData,
                    LaborantId = item.LaborantId,
                    LaborantData = item.LaborantData,

                    IsHasVisited = item.IsHasVisited,
                    IsPreorder = item.IsPreorder,
                    StudyCost = item.ConvertedStudyCost,
                   
                });
            }
            InFactPatientByDate = magasineinfactbydate;
        }

        //Help method for Charges by date
        private void HelpRefreshChargesByDate(string DateOfCharges)
        {
            List<DalChargesByDate> dalchargesbydate = new List<DalChargesByDate>();
            dalchargesbydate= bll.BllGetAllChargesByDate(DateOfCharges);
            List<MChargesByDate> tempcharges = new List<MChargesByDate>();
            int count = 1;
            foreach (var item in dalchargesbydate)
            {                
                tempcharges.Add(new MChargesByDate
                {
                    NumberInOrder=count,
                    ChargeId = item.ChargeId,
                    ChargeNote=item.ChargeNote,
                    ConvertChargesByDate = item.ConvertChargesByDate,
                    ConvertedCostCharge = item.ConvertedCostCharge

                    //CostCharge=item.CostCharge,
                    //ChargesByDate=item.ChargesByDate,
                });
                count++;
            }
            AllChargesPerDay = tempcharges;
        }
    
        public ViewResult Index ()
        {
            return View();
        }

//------------------------------------START WORK WITH MAIN TABLE OF PATIENT------------->>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpGet]
        public PartialViewResult WorkWithPatients()
        {
            var modelPatients = new Patients();
            return PartialView(modelPatients);
        }
        [HttpGet]
        public JsonResult ReturnFilteringPatient (Patients filter)
        {
            var fulllistofpatients = ListOfPatientFromDB;
            var ResultOfFiltering = fulllistofpatients.Where(c =>
            (String.IsNullOrEmpty(filter.LastName) || c.LastName.Contains(filter.LastName) || c.LastName.ToLower().Contains(filter.LastName)) &&
            (String.IsNullOrEmpty(filter.CellPhone) || c.CellPhone.Contains(filter.CellPhone))
            );
            return Json(ResultOfFiltering.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RefreshListOfPatient()
        {
            HelperRefreshListOfPatient();
           return Json(ResultOfReadingFromDB=true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdatePatientData (Patients patient)
        {
            
            patient.ResultOfReadingFromDB = uihelper.UIConvertUpdatePatientData(patient);
            HelperRefreshListOfPatient();

            return Json(patient);
        }

        [HttpPost]
        public JsonResult SaveNewPatient (Patients patient)
        {
            patient.ResultOfReadingFromDB = uihelper.UIConvertSaveNewPatient(patient);
            HelperRefreshListOfPatient();

            //Here we took Id of last added Patient to DB. It's need for jsGrid insertItem
            int IdOfLastEddedPatient = ListOfPatientFromDB.Max(c=>c.PatientId);
            patient.PatientId = IdOfLastEddedPatient;

            return Json(patient);
        }

        [HttpPost]
        public JsonResult DeleteSelectedPatient (Patients patient)
        {
            patient.ResultOfReadingFromDB = bll.BllDeleteSelectedPatient(patient.PatientId);
            return Json(patient);
        }
//<<<<<<<<<<<<<<<<<<<<<<<<------------END WORK WITH MAIN TABLE OF PATIENT----------------------------------------

//-----------------------------START WORK WITH MODAL WINDOW OF PREORDERING------------->>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpGet]
        public PartialViewResult RecordPatientToPreorderMagasine()
        {
            //List<DalStudy> UsedStudies = bll.BllGetUsedStudies().ToList();
            //var items = new List<SelectListItem>();


            //foreach (var item in UsedStudies)
            //{
            //    {
            //        items.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Type });
            //    }
            //}
            //magasineofpreorder.AllUsedStudiesFromDb = items;
            magasineofpreorder.AllUsedStudiesFromDb = uihelper.UIGetUsedStudies().ToList();
            return PartialView(magasineofpreorder);
        }

//-----------------------------END WORK WITH MODAL WINDOW OF PREORDERING------------->>>>>>>>>>>>>>>>>>>>>>>>>>>

//-----------------------------START WORK WITH MODAL WINDOW - MAGASINE IN FACT------------->>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpGet]
        public PartialViewResult M_RecordPatientToMagasineInFact()
        {
            magasineinfact.AllUsedStudiesFromDb=uihelper.UIGetUsedStudiesWithCost().ToList();
            magasineinfact.AllLaborantsFromDb = uihelper.UIGetUsedLaborants().ToList();
            return PartialView(magasineinfact);
        }

//-----------------------------END WORK WITH MODAL WINDOW - MAGASINE IN FACT------------->>>>>>>>>>>>>>>>>>>>>>>>>>>



//-----------------------------START WORK WITH TABLE IN FACT------------->>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpGet]
        public JsonResult RefreshListInFactPatientByDate(MagasineInFact magasineinfact)
        {
            HelperRefreshInFactMagasine(magasineinfact.ConvertDateOfVisit);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllInFactPatientByDate(MagasineInFact filter)
        {
            //HelperRefreshInFactMagasine(filter.ConvertDateOfVisit);
            //IOrderedEnumerable<MagasineInFact> resultoffiltering;

            if (InFactPatientByDate.Count()!=0)
            {
                var ResultOfFilteringInFactMagasin = InFactPatientByDate;
                var resultoffiltering = ResultOfFilteringInFactMagasin.Where(c =>
                (String.IsNullOrEmpty(filter.LastName) || c.LastName.Contains(filter.LastName) || c.LastName.ToLower().Contains(filter.LastName)) &&
                (String.IsNullOrEmpty(filter.StudyType) || c.StudyType.Contains(filter.StudyType) || c.StudyType.ToLower().Contains(filter.StudyType)))
                   .OrderBy(c => c.ConvertedTimeOfVisit);

                int count = 1;
                foreach (var finallist in resultoffiltering)
                {
                    finallist.NumberInOrder = count;
                    count++;
                }

               
                return Json(resultoffiltering.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
                return Json(InFactPatientByDate, JsonRequestBehavior.AllowGet);


        }
        
        [HttpPost]
        public JsonResult AddNewPatientToInFactMagasine(MagasineInFact magasineinfact)
        {
            //MagasineOfPreorder emptyrowofpreorder = new MagasineOfPreorder();
            var newinfactorder = SendItemsFromTo.ReassignmentOfFields(magasineinfact);
            magasineinfact.Result = bll.BllAddNewPatientToInFactMagasine(newinfactorder.Pat_Id, newinfactorder.St_Id, newinfactorder.VisitDate,
                                                     newinfactorder.IsPreorder, newinfactorder.IsNeedSendEmail, newinfactorder.IsHasVisited, newinfactorder.IsCash,
                                                     newinfactorder.LaborantId1,newinfactorder.LaborantId2,newinfactorder.FinalCost,
                                                     newinfactorder.NoteForDiscount,newinfactorder.DoctorData);
            if (magasineinfact.Result)
            {
                string[] words = magasineinfact.ConvertDateOfVisit.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string ShortConvertDateOfVisit = words[0];
                HelperRefreshInFactMagasine(ShortConvertDateOfVisit);
                return Json(magasineinfact);
            }
            else
            {
                return Json(emptyrowinfact);
            }

        }
        public JsonResult UpdateNoteOfInFactMagasine (MagasineInFact magasineinfact)
        {
            var updateinfactorder = SendItemsFromTo.ReassignmentOfFields(magasineinfact);
            magasineinfact.Result = bll.BllUpdateNoteOfInFactMagasine(updateinfactorder.Pat_Id, updateinfactorder.St_Id,
                                                                            updateinfactorder.NewStudyId, updateinfactorder.VisitDate,
                                                                            updateinfactorder.IsNeedSendEmail, updateinfactorder.IsCash, updateinfactorder.LaborantId1,
                                                                            updateinfactorder.NewLaborantId1,updateinfactorder.LaborantId2, updateinfactorder.NewLaborantId2,
                                                                            updateinfactorder.FinalCost, 
                                                                            updateinfactorder.NoteForDiscount, updateinfactorder.DoctorData);
            if (magasineinfact.Result)
            {
                //For refresh we need short date dd/mm/yyyy
                string[] words = magasineinfact.ConvertDateOfVisit.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string ShortConvertDateOfVisit = words[0];
                HelperRefreshInFactMagasine(ShortConvertDateOfVisit);

                //For preorder JsGrid, after updated of the note
                //magasineinfact.ConvertedTimeOfVisit = magasineofpreorder.NewConvertedTimeOfVisit;

                return Json(magasineinfact);
            }
            else
            {
                return Json(magasineinfact);
            }

        }

        [HttpPost]
        public JsonResult DeleteNoteFromInFactMagasine(MagasineInFact magasineinfact)
        {
            DateTime dateofvisitinfact = new DateTime();
            dateofvisitinfact = Convert.ToDateTime(magasineinfact.ConvertDateOfVisit);
            magasineinfact.Result = bll.BllDeleteNoteFromInFactMagasine(magasineinfact.StudyId, dateofvisitinfact, magasineinfact.PatientId,
                                        magasineinfact.IsPreorder, magasineinfact.IsHasVisited,magasineinfact.LaborantData);
            if (magasineinfact.Result)
                HelperRefreshInFactMagasine(magasineinfact.ConvertDateOfVisit);
            return Json(magasineinfact);
        }

//-----------------------------END WORK WITH TABLE IN FACT------------->>>>>>>>>>>>>>>>>>>>>>>>>>>



//-----------------------------START WORK WITH TABLE OF PREORDERING------------->>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpGet]
      public JsonResult RefreshListPreorderPatientByDate (MagasineOfPreorder magasineofpreorder)
        {
            magasineofpreorder.Result = false;
            HelperRefreshPreorderMagasine(magasineofpreorder.ConvertDateOfVisit);           
            magasineofpreorder.Result = true;
            return Json(magasineofpreorder.Result, JsonRequestBehavior.AllowGet);
        }
              
        [HttpGet]
        public JsonResult GetAllPreorderingPatientByDate(MagasineOfPreorder filter)
        {
            //if (!filter.Latch)
            //HelperRefreshPreorderMagasine(filter.ConvertDateOfVisit);
            var ResultOfFilteringInPreorderMagasin = PreorderPatientByDate;
            var resultoffiltering=ResultOfFilteringInPreorderMagasin.Where(c =>
           (String.IsNullOrEmpty(filter.LastName) || c.LastName.Contains(filter.LastName) || c.LastName.ToLower().Contains(filter.LastName)) &&
           (String.IsNullOrEmpty(filter.CellPhone) || c.CellPhone.Contains(filter.CellPhone))&&
           (String.IsNullOrEmpty(filter.StudyType)|| c.StudyType.Contains(filter.StudyType) || c.StudyType.ToLower().Contains(filter.StudyType))
           ).OrderBy(c=>c.ConvertedTimeOfVisit);
            return Json(resultoffiltering.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddNewPatientToPreorderMagasine(MagasineOfPreorder magasineofpreorder)
        {
            //MagasineOfPreorder emptyrowofpreorder = new MagasineOfPreorder();
            var newpreorder=SendItemsFromTo.ReassignmentOfFields(magasineofpreorder);
            magasineofpreorder.Result=bll.BllAddNewPatientToPreorderMagasine(newpreorder.Pat_Id,newpreorder.St_Id,newpreorder.NewVisitDate,
                                                     newpreorder.IsPreorder,newpreorder.IsNeedSendEmail,newpreorder.IsHasVisited,newpreorder.IsCash);
            if (magasineofpreorder.Result)
            {
                HelperRefreshPreorderMagasine(magasineofpreorder.ConvertDateOfVisit);
                return Json(magasineofpreorder);
            }
            else
            {
                return Json(emptyrowofpreorder);
            }
            
        }

        [HttpPost]
        public JsonResult UpdateNoteOfPreorderMagasine (MagasineOfPreorder magasineofpreorder)
        {
           
            var updatepreordernote = SendItemsFromTo.ReassignmentOfFields(magasineofpreorder);
            magasineofpreorder.Result = bll.BllUpdateNoteOfPreorderMagasine(updatepreordernote.Pat_Id, updatepreordernote.St_Id,
                                                                            updatepreordernote.NewStudyId, updatepreordernote.VisitDate,
                                                                            updatepreordernote.NewVisitDate);
            if (magasineofpreorder.Result)
            {
                //For refresh we need short date dd/mm/yyyy
                string[] words = magasineofpreorder.ConvertDateOfVisit.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string ShortConvertDateOfVisit = words[0];
                HelperRefreshPreorderMagasine(ShortConvertDateOfVisit);

                //For preorder JsGrid, after updated of the note
                magasineofpreorder.ConvertedTimeOfVisit = magasineofpreorder.NewConvertedTimeOfVisit;

                return Json(magasineofpreorder);
            }
            else
            {
                return Json(emptyrowofpreorder);
            }
        }
        [HttpPost]
        public JsonResult DeleteNoteFromPreorderMagasine(MagasineOfPreorder magasineofpreorder)
        {
            DateTime preorderdate = new DateTime();
            preorderdate = Convert.ToDateTime(magasineofpreorder.ConvertDateOfVisit);
            magasineofpreorder.Result = bll.BllDeleteNoteFromPreorderMagasine(magasineofpreorder.StudyId, preorderdate, magasineofpreorder.PatientId);
            if(magasineofpreorder.Result)
                HelperRefreshPreorderMagasine(magasineofpreorder.ConvertDateOfVisit);
            return Json(magasineofpreorder);
        }
//-----------------------------END WORK WITH TABLE OF PREORDERING------------->>>>>>>>>>>>>>>>>>>>>>>>>>>


//-------------------------------------START WORK WITH STUDIES-------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        [HttpGet]
        public PartialViewResult SaveNewStudy()
        {
            var modelStudy = new Study();
            return PartialView(modelStudy);
        }

        [HttpPost]
        public JsonResult SaveNewStudy(Study study)
        {
                study.Used = true;
                study.AddNewStudyResult = uihelper.UIConvertSaveNewStudy(study);
                return Json(study,JsonRequestBehavior.AllowGet); 
        }

      [HttpGet]
       public PartialViewResult GetAllStudies()
        {
            List<DalStudy> studies=bll.GetAllStadies().ToList();
            var items = new List<SelectListItem>();
           

            foreach (var item in studies)
            {
                {
                    items.Add(new SelectListItem { Value = item.St_Id.ToString(), Text = item.St_Type });
                }
            }
            controlStudy.AllStudiesDb = items;
            return PartialView(controlStudy);
        }

        [HttpGet]
        public PartialViewResult GetAllStudiesForChangingDB()
        {
            List<DalStudy> studies = bll.GetAllStadies().ToList();
            var items = new List<SelectListItem>();


            foreach (var item in studies)
            {
                {
                    items.Add(new SelectListItem { Value = item.St_Id.ToString()+"#"+item.St_Cost.ToString(), Text = item.St_Type });
                }
            }
            controlStudyChangeData.AllStudiesDb = items;
            return PartialView(controlStudyChangeData);
        }

        //Take all fields from DB for change colour in DropDownList some Study which have 
        //[Study].[UsedOrNot]==false
        [HttpGet]
        public JsonResult GetAllStudiesWithAllFields()
        {
            List<DalStudy> studies = new List<DalStudy>();
            studies = bll.GetAllStadies().ToList();
            var items = new List<Study>();
            foreach (var item in studies)
            {
                items.Add(new Study
                {
                    StudyId = item.St_Id,
                    Type = item.St_Type,
                    Cost = item.St_Cost.ToString(),
                    Used = item.St_Used
                });
            }
            controlStudy.AllItemsFromDB = items;
            return Json(controlStudy, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteStudy(Study study)
        {
            
                if (study.SelectedValue!=null)
                study.AddNewStudyResult=bll.BllDeleteStudy(study.SelectedValue);
            
            return Json (study,JsonRequestBehavior.AllowGet);
       
        }

        //Add or Delete selected study from list of using Studies
        [HttpGet]
        public JsonResult UseOrUnuseSelectedStudy(Study study)
        {
            study.AddNewStudyResult = bll.BllUseOrUnuseSelectedStudy(study.SelectedValue, study.Used);
            return Json(study, JsonRequestBehavior.AllowGet);
        }

        //Partial View for change cost or name of study
        [HttpGet]
        public PartialViewResult ChangeExistingStudy()
        {
            return PartialView();
        }

        //Change DATA of existing Study
        [HttpPost]
        [ValidateAjax]
        public JsonResult ChangeDataOfExistingStudy (StudyChangeData changedstudy)
        {
            changedstudy.AddNewStudyResult = uihelper.UIConvertSaveNewChangedStudy(changedstudy);
            return Json(changedstudy);
        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<--------END WORK WITRH STUDIES-------------------------------------------


//-------------------------------------START WORK WITH LABORANTS-------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //Partial View for saving a new one laborant
        [HttpGet]
        public PartialViewResult SaveNewLaborant()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAjax]
        public JsonResult SaveNewLaborant(Laborants laborant)
        {
            laborant.AddNewLaborantResult = bll.SaveNewLaborant(laborant.LastName, laborant.FirstName, laborant.Used);
            return Json(laborant, JsonRequestBehavior.AllowGet);
        }
        
        //Partial View for getting full list of laborants
        [HttpGet]
        public PartialViewResult GetAllLaborants()
        {
            //List<DalLaborant> laborants = new List<DalLaborant>();
            //laborants = bll.GetAllLaborants().ToList();
            //var items = new List<SelectListItem>();
            //foreach( var item in laborants)
            //{
            //    items.Add(new SelectListItem { Value = item.IdOfLaborant.ToString(),
            //                                   Text =item.LastNameOfLaborant+" "+ item.FirstNameOfLaborant});
            //    controlLaborants.AllLaborantsDb = items;
            //}
            controlLaborants.AllLaborantsDb = uihelper.UIGetAllLaborants().ToList();
            return PartialView(controlLaborants);
        }

        [HttpGet]
        public PartialViewResult GetAllLaborantsForChangingDB()
        {
            //List<DalLaborant> laborants = new List<DalLaborant>();
            //laborants = bll.GetAllLaborants().ToList();
            //var items = new List<SelectListItem>();
            //foreach (var item in laborants)
            //{
            //    items.Add(new SelectListItem
            //    {
            //        Value = item.IdOfLaborant.ToString(),
            //        Text = item.LastNameOfLaborant + " " + item.FirstNameOfLaborant
            //    });
            //    controlLaborantsChangeData.AllLaborantsDb = items;
            //}
            controlLaborantsChangeData.AllLaborantsDb = uihelper.UIGetAllLaborants().ToList();
            return PartialView(controlLaborantsChangeData);
        }

        //Take all fields from DB for change colour in DropDownList some Laborants which have 
        //[Laborant].[UsedOrNot]==false
        [HttpGet]        
        public JsonResult GetAllLaborantsWithAllFields()
        {
            List<DalLaborant> laborants = new List<DalLaborant>();
            laborants = bll.GetAllLaborants().ToList();
            var items = new List<Laborants>();
            foreach (var item in laborants)
            {
                items.Add(new Laborants { LaborantId = item.IdOfLaborant, LastName = item.LastNameOfLaborant,
                    FirstName = item.FirstNameOfLaborant, Used = item.UsedOrUnusedLaborant});
            }
            controlLaborants.AllItemsFromDB = items;
            return Json(controlLaborants, JsonRequestBehavior.AllowGet);
        }

        //Delete laborant from DB
        [HttpPost]
        public JsonResult DeleteLaborantFromDB (Laborants laborant)
        {
            if (laborant.SelectedValue != null)
                laborant.AddNewLaborantResult = bll.DeleteLaborantFromDB(laborant.SelectedValue);
            return Json(laborant, JsonRequestBehavior.AllowGet);
        }

        //Add or Delete selected laborant from list of using Laborants
        [HttpPost]
        public JsonResult UseOrUnuseSelectedLaborant(Laborants laborant)
        {
            laborant.AddNewLaborantResult = bll.BllUseOrUnuseSelectedLaborant(laborant.SelectedValue, laborant.Used);
            return Json(laborant, JsonRequestBehavior.AllowGet);
        }

        //Partial View for change laborants data
        [HttpGet]
        public PartialViewResult ChangeExistingLaborant()
        {
            return PartialView();
        }
        //Change data of selected laborant
        [HttpPost] 
        [ValidateAjax]
        public JsonResult ChangeDataOfExistingLaborant(LaborantsChangeData laborant)
        {
            laborant.AddNewLaborantResult = bll.ChangeDataOfSelectedLaborant(laborant.SelectedValue, laborant.LastNameChangeLaborant, laborant.FirstNameChangeLaborant, laborant.Used);
            return Json(laborant, JsonRequestBehavior.AllowGet);
        }
//-------------------------------------END WORK WITH LABORANTS-------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        //Partial View for CountingMoney on the main window
        [HttpGet]
        public PartialViewResult CountingMoneyPerDay()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult CountMoneyPerDayCashAndTerminal()
        {
            mchargesbydate.CashSumByDate = 0;
            mchargesbydate.TerminalSumByDate = 0;
            mchargesbydate.DifferentCharges = 0;

            if (InFactPatientByDate.Count() != 0)
            {
                mchargesbydate.CashSumByDate = InFactPatientByDate.Where(p => p.IsCash == true).Sum(p => float.Parse(p.ConvertedFinalCost));
                mchargesbydate.TerminalSumByDate = InFactPatientByDate.Where(p => p.IsCash == false).Sum(p => float.Parse(p.ConvertedFinalCost));
            }
            else
            {
                mchargesbydate.CashSumByDate = 0;
                mchargesbydate.TerminalSumByDate = 0;
            }
            if (AllChargesPerDay.Count()!=0)
            {
                mchargesbydate.DifferentCharges = AllChargesPerDay.Sum(p => float.Parse(p.ConvertedCostCharge));
            }
            else
            {
                mchargesbydate.DifferentCharges = 0;
            }

            return Json(mchargesbydate);
        }

        [HttpGet]
        public JsonResult RefreshListOfCharges(MChargesByDate chargesbydate)
        {
            HelpRefreshChargesByDate(chargesbydate.ConvertChargesByDate);            
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllChargesByDate()
        {           
            return Json(AllChargesPerDay, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddNewCharge(MChargesByDate chargersbydate)
        {
            chargersbydate.ResultOfAction = bll.BllAddNewCharge(chargersbydate.ChargeNote, chargersbydate.ConvertedCostCharge,
                                                                    chargersbydate.ConvertChargesByDate);
            if (chargersbydate.ResultOfAction == true)
                HelpRefreshChargesByDate(chargersbydate.ConvertChargesByDate);
            return Json(chargersbydate);
        }
        [HttpPost]
        public JsonResult UpdateSelectedCharge(MChargesByDate chargersbydate)
        {
            chargersbydate.ResultOfAction = bll.BllUpdateSelectedCharge(chargersbydate.ChargeId, 
                                                                            chargersbydate.ChargeNote,
                                                                                chargersbydate.ConvertedCostCharge);
            if (chargersbydate.ResultOfAction == true)
                HelpRefreshChargesByDate(chargersbydate.ConvertChargesByDate);
            return Json(chargersbydate);
        }
        [HttpPost]
        public JsonResult DeleteSelectedCharge(MChargesByDate chargersbydate)
        {
            chargersbydate.ResultOfAction = bll.BllDeleteSelectedCharge(chargersbydate.ChargeId);
            if (chargersbydate.ResultOfAction==true)
                HelpRefreshChargesByDate(chargersbydate.ConvertChargesByDate);

            return Json(chargersbydate);
        }



    }

}
