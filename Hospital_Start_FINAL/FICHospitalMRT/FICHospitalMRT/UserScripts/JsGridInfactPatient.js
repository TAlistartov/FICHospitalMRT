//window.onload = function () {
//    RefreshInfactMagasineByDateAfterChangeDate(currentday);
//};
$.holdReady(true);
RefreshInfactMagasineByDateAfterChangeDate(currentday);

$(document).ready(function () {
    var infactdayofvisit;
    var DataForUpdateInFact;
    var KindOfActionInFactMagasine;
    //The main tools of DateTimePicker
    $(function () {
        $('#VisitDateInFact').datetimepicker(
               {
                   language: 'ru',
                   daysOfWeekDisabled: [0],
                   minuteStepping: 5
               }
            );
    });

    //Different action if patient in main table doesn't selected and if it selected
    $(document).on('click', '#InFact', function () {
        if (row_is_highlighted == true) {
            KindOfActionInFactMagasine = "ADD";

            //Copying information to modal window form Object which was created in JsGridAllPatient - ptientdata
            FillingFieldsOfModalWindow(ptientdata);
            $('#RecordPatientToMagasineInFact').modal('show');

        }
        else
            alert("Сначала выберите пациента для дальнейшей записи...");
    });

    //Transfer Data from PreorderMagasine to InFactMagasine
    $(document).on('click', '#btnTransferDataFromPreorderToInFact', function () {
        if (row_is_highlighted_preordermagasine == true)
                {           
                    KindOfActionInFactMagasine = "ADD_FROM_PREORDER_MAGASINE";
                    //ptientdata.ConvertDateOfVisit = rowdata.item.ConvertDateOfVisit;                    
                    //Copying information to modal window form Object which was created in JsGridAllPatient - ptientdata
                    FillingFieldsOfModalWindow(ptientdata_from_preordermagasine);
                    ptientdata.SelectedStudyInFact = $("#ViewOfStudyInFact").val();
                    ptientdata.PatientId = ptientdata_from_preordermagasine.PatientId;
                    ptientdata.LaborantId1 ="";
                    ptientdata.LaborantId2 = "";
                    //ptientdata.LaborantId1 = $("#LaborantData").val();
                    //ptientdata.LaborantId2 = $("#LaborantData_1").val();
                    $('#RecordPatientToMagasineInFact').modal('show');
               }
    else
            alert("Сначала выберите пациента для дальнейшей записи...");
    })

    //Show the Additional informatien about patient
    $(document).on('click', '#MoreInformationAboutPatient', function () {
        $("#AdditionalInformatien").collapse('toggle');

        if ($("#AdditionalInformatien").attr('aria-expanded') == "true")
            $('#MoreInformationAboutPatient').addClass('fa-rotate-180');
        else
            $('#MoreInformationAboutPatient').removeClass('fa-rotate-180');
    });

    //clear all fields and validation errors in form RecordPatientToMagasineInFact when modal window has closed
    $(document).on('hidden.bs.modal', '#RecordPatientToMagasineInFact', function () {   
        $("#RecordPatientToFact").validate().resetForm();
        $("#RecordPatientToFact").find(".error").removeClass("error");
        //When modal window is closing we reset date field
        $(".my_reset").val("");

        $("#ViewOfStudyInFact").val("");
        
        $("#LaborantData option:selected").each(function () {
            this.selected = false;
        });
        $('#AddOneMoreLaborant').css({ "display": "none" })

        //When modal window is closing we reset select study and laborant №1 field
        $("#LaborantData_1 option:selected").each(function () {
            this.selected = false;
        });
        $('#TheSecondLaborant').css({ 'display': 'none' });
        $('#DeleteSecondLaborant').css({ 'display': 'none' });
        $('#LaborantData_1').remove();
        KindOfActionInFactMagasine = "";
        $('#IsNeedSendEmail').prop('checked', false);
        $('#IsCash').prop('checked', false);
        
        //DateTimePicker in modal window - Enable
        $("#RecordPatientToFact #Calendar").css("pointer-events","auto");
        $('#RecordPatientToFact #VisitDateInFactMagasine').removeAttr('readonly');
        //Selected Study in modal window -Enable
        $('#RecordPatientToFact #ViewOfStudyInFact').removeAttr('readonly');
        $("#RecordPatientToFact #ViewOfStudyInFact").css("pointer-events", "auto");
    });

    //When Selected Study was changed
    $(document).on('change', '#ViewOfStudyInFact', function () {
        var NotParsedSelectedStudyID = $('#ViewOfStudyInFact option:selected').val();
        var arr = NotParsedSelectedStudyID.split('#', 2);
        var SelectedStudyId = arr[0];
        var SelectedStudyCost = arr[1];
        $('#RecordPatientToFact #FinalCost').val(SelectedStudyCost);
    });

    //When the one of the Laborant was selected
    //Get value of the 1st selected laborant
    var value_select_1st_laborant;
    $(document).on('change', '#LaborantData', function () {
        var item = $('#LaborantData').val();
        if (item == "") {
            $('#AddOneMoreLaborant').css({ "display": "none" });
        }
        else {
            $('#AddOneMoreLaborant').css({ "display": "inline-block" });
            value_select_1st_laborant = $('#LaborantData').val();
        }
    });

    //If administrator click Add one more laborant    
    $(document).on('click', '#AddOneMoreLaborant', function () {
        $('#DeleteSecondLaborant').css({ 'display': 'inline-block' });
        $('#AddOneMoreLaborant').css({ "display": "none" });
        var item = $('#LaborantData_1').length;
        //Administrator can Add just one additional Laborant
        if (!item) {
            //div with Id=TheSecondLaborant
            $('#TheSecondLaborant').css({ 'display': 'block' });
            //Here we copy DropDownList LaborantData
            $('#LaborantData').clone().attr({ 'id': 'LaborantData_1', 'name': 'lab_data' }).appendTo('.row #lab_num_2');

            //Hide a Laborant in the 2dh DropDownList, which was selected in the 1st DropDownList
            $("#LaborantData_1 option[value="+ value_select_1st_laborant + "]").hide();
        }
    });

    //If we want to delete the second Laborant
    $(document).on('click', '#DeleteSecondLaborant', function () {
        $("#LaborantData_1 option:selected").each(function () {
            this.selected = false;
        });
        $('#LaborantData_1').remove();
        //$('#TheSecondLaborant').detach();
        $('#TheSecondLaborant').css({ 'display': 'none' });
        $('#DeleteSecondLaborant').css({ 'display': 'none' });
        $('#AddOneMoreLaborant').css({ "display": "inline-block" });
    });

    $('#InFactPatients').jsGrid({
        width: "100%",
        height: "auto",

        filtering: true,
        sorting: true,
        paging: true,
        pageLoading: false,
        noDataContent: "На выбранную дату данных не найдено.",
        autoload: true,
        deleteConfirm: function (item) {
            return "Запись пациента " + item.LastName + " " + item.FirstName + " " + item.MiddleName + " будет удалена. Вы хотите продолжить?"
        },
        rowDoubleClick: function (rowdata) {
            KindOfActionInFactMagasine = "UPDATE";
            DataForUpdateInFact = rowdata.item;
            ptientdata = DataForUpdateInFact;

            //Copying information to modal window             
            var arr = rowdata.item.StudyCost.split(',', 2);
            //Parse to View 60-->6 and 63-->63
            var secondPartArray = arr[1];
            //Here we check the number contains 0(zero)                
            var FinalCost = secondPartArray.replace(/0/g, "");
            if (FinalCost !== "")
                rowdata.item.StudyCost = arr[0] + ',' + FinalCost;
            else
                rowdata.item.StudyCost = arr[0];
            
            FillingFieldsOfModalWindow(rowdata.item);
            //Start values before UPDATE
            ptientdata.ConvertDateOfVisit = rowdata.item.ConvertDateOfVisit;
            ptientdata.SelectedStudyInFact = $("#ViewOfStudyInFact").val();
            ptientdata.LaborantId1 = $("#LaborantData").val();
            ptientdata.LaborantId2 = $("#LaborantData_1").val();           
                        
            $("#RecordPatientToMagasineInFact").modal('show');
        },
        pageSize: 8,
        fields: [
                  { name: "NumberInOrder", type: "text", width: 5, align: "center", filtering: false, title: "№ п/п" },
                  { name: "LastName", type: "text", width: 40, align: "center", title: "Фамилия" },
                  { name: "FirstName", type: "text", width: 30, align: "center", sorting: false, filtering: false, title: "Имя" },
                  { name: "MiddleName", type: "text", width: 35, align: "center", sorting: false, filtering: false, title: "Отчество" },
                  { name: "LaborantData", type: "text", width: 40, align: "center", filtering: false, title: "Лаборант(ы)" },
                  { name: "DoctorData", type: "text", width: 35, align: "center", filtering: false, title: "Направил" },
                  { name: "StudyType", type: "text", width: 40, align: "center", sorting: false, title: "Обследование" },
                  { name: "ConvertedFinalCost", type: "text", width: 20, align: "center", sorting: false, filtering: false, title: "Сумма" },
                  { name: "IsCash", type: "checkbox", width: 5, align: "center", sorting: false, filtering: false, title: "Кэш ?" },
                  { name: "IsNeedSendEmail", type: "checkbox", width: 5, align: "center", sorting: false, filtering: false, title: "На @ ? " },




                  { name: "ConvertedTimeOfVisit", type: "text", width: 0, align: "center", filtering: false, title: "Время" },
                  { name: "Note", type: "textarea", width: 0, align: "center", sorting: false, filtering: false, title: "Пометка" },
                  { name: "PatientId", type: "text", width: 0, align: "center" },
                  { name: "Adress", type: "text", width: 0, align: "center", sorting: false },
                  { name: "JobPlace", type: "text", width: 0, align: "center", sorting: false },
                  { name: "Email", type: "text", width: 0, align: "center", sorting: false },
                  { name: "StudyId", type: "text", width: 0, align: "center", sorting: false },
                  { name: "ConvertDateOfVisit", type: "text", width: 0, align: "center", sorting: false },
                  { name: "NoteForDiscount", type: "textarea", width: 0, align: "center", sorting: false, filtering: false, title: "Пометка о скидке" },
                  { name: "CellPhone", type: "text", width: 0, align: "center", sorting: false, title: "Номер телефона" },
                  { name: "ConvertedBirthDate", type: "text", width: 0, align: "center", filtering: false, title: "Дата рождения" },
                  { name: "IsHasVisited", type: "text", width: 0, align: "center", filtering: false },
                  { name: "IsPreorder", type: "text", width: 0, align: "center", filtering: false },
                  { name: "StudyCost", type: "text", width: 0, align: "center", filtering: false },

           {
               type: "control",
               //modeSwitchButton: false,
               editButton: false,
               width: 5
           }
        ],
        controller:
                      {
                          loadData: function (filter) {
                              return $.ajax({
                                  type: 'GET',
                                  url: '/Home/GetAllInFactPatientByDate',
                                  //For filtering of patients
                                  data: {
                                      LastName: filter.LastName, StudyType: filter.StudyType
                                                //ConvertDateOfVisit: infactdayofvisit
                                  },
                                  dataType: 'JSON',
                                  success:function()
                                  {
                                      CountMoneyPerDayCashAndTerminal();
                                  }
                              });
                          },

                          insertItem: function (item) {
                              return $.ajax({
                                  type: 'POST',
                                  url: '/Home/AddNewPatientToInFactMagasine',
                                  data: item,
                                  dataType: 'JSON',
                                  success: function (data) {
                                      if (data.Result == true) {
                                          $("#InFactPatients").jsGrid("render").done(function () {
                                              $("#RecordPatientToMagasineInFact").modal('hide');
                                              $("#DataOfAllPatient tr").removeClass("highlight");
                                              $("#PreorderPatients tr").removeClass("highlight_preorder");
                                              //CountMoneyPerDayCashAndTerminal();
                                              
                                          })
                                          alert("Пациент был добавлен в журнал 'По Факту'.");

                                      }
                                      else
                                          alert("Не удалось добавить пациента в журнал 'По Факту'.");

                                  }
                              })
                          },

                          deleteItem: function (item) {
                              return $.ajax({
                                  type: "POST",
                                  url: "/Home/DeleteNoteFromInFactMagasine",
                                  data: item,
                                  dataType: "JSON",
                                  success: function (data) {
                                      if (data.Result == true) 
                                      {
                                          //Refresh InFact List by date
                                          infactdayofvisit = $("#datetimepicker1 input").val();
                                          RefreshInfactMagasineByDateAfterChangeDate(infactdayofvisit);                                         
                                          CountMoneyPerDayCashAndTerminal();

                                          alert("Запись успешно удалена из БД.");
                                      }                                          
                                      else
                                          alert("Не удалось удалить запись из БД.");
                                  }
                              });
                          }
                      },

                        updateItem: function (item) {

                            return $.ajax({
                                type: "POST",
                                url: "/Home/UpdateNoteOfInFactMagasine",
                                data: item,
                                dataType: "JSON",
                                success: function (data) {
                                    if (data.Result == true) {
                                        $("#InFactPatients").jsGrid("render").done(function () {                                           
                                            $("#PreorderPatients tr").removeClass("highlight_preorder");
                                            $("#RecordPatientToMagasineInFact").modal('hide');
                                        })
                                        alert("Изменения успешно внесены в БД.");
                                        //CountMoneyPerDayCashAndTerminal();
                                        //$("#RecordPatientToPreorderMagasine").modal('hide');
                                    }

                                    else
                                        alert("Не удалось внести изменения в БД.");
                                },
                            });
                        },
    });

    //Hide all unused tags
    $("#InFactPatients").jsGrid("fieldOption", "PatientId", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "Adress", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "JobPlace", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "Email", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "ConvertedTimeOfVisit", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "StudyId", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "ConvertDateOfVisit", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "Note", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "NoteForDiscount", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "CellPhone", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "ConvertedBirthDate", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "IsHasVisited", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "IsPreorder", "visible", false);
    $("#InFactPatients").jsGrid("fieldOption", "StudyCost", "visible", false);

    $('#RecordPatientToFact').validate({
        rules: {
            SelectedStudyInFact: { required: true },
            amount: {
                required: true,
                //name of JS function below
                regex: /(\d{1,3},\d{1,2})|(\d{1,3}\.\d{1,2})|(^(?:\d+|\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$)/
            },
            SelectedLaborantInFact: { required: true },
            lab_data: { required: true },
            visitdateinfactmagasine: { required: true },
            doctordata: { maxlength: 30 },
            notefordiscount: { maxlength: 100 }
        },
        messages: {
            SelectedStudyInFact: { required: "Выберите обследование." },
            amount: { required: "Укажите стоимость." },
            SelectedLaborantInFact: { required: "Выберите лаборанта №1." },
            lab_data: { required: "Выберите лаборанта №2." },
            visitdateinfactmagasine: { required: "Укажите дату." },
            doctordata: { maxlength: "Слишком длинно (<30 симв.)" },
            notefordiscount: { maxlength: "Слишком длинно (<100 симв.)" }

        },
        submitHandler: function (form) {

            if (KindOfActionInFactMagasine == "ADD")
            {
                ptientdata.IsPreorder = false;
                ptientdata.SelectedStudyInFact = $("#ViewOfStudyInFact").val();
                ptientdata.LaborantId1 = $("#LaborantData").val();
                ptientdata.LaborantId2 = $("#LaborantData_1").val();
            }
            else if (KindOfActionInFactMagasine=="ADD_FROM_PREORDER_MAGASINE")
            {
                //ptientdata.PatientId=
                ptientdata.IsPreorder = true;
                //ptientdata.IsHasVisited = true;
                ptientdata.NewSelectedStudyInFact = $("#ViewOfStudyInFact").val();
                ptientdata.NewLaborantId1 = $("#LaborantData").val();
                ptientdata.NewLaborantId2 = $("#LaborantData_1").val(); 
            }
            else if (KindOfActionInFactMagasine == "UPDATE")
            {
                ptientdata.NewSelectedStudyInFact = $("#ViewOfStudyInFact").val();               
                ptientdata.NewLaborantId1 = $("#LaborantData").val();
                ptientdata.NewLaborantId2 = $("#LaborantData_1").val();                
            }
            
            ptientdata.ConvertDateOfVisit = $('#VisitDateInFactMagasine').val();
            ptientdata.ConvertedFinalCost = $("#FinalCost").val();
            ptientdata.DoctorData = $("#DoctorData").val();
            ptientdata.NoteForDiscount = $("#NoteForDiscount").val();

            if ($('#IsCash').is(":checked")) 
                ptientdata.IsCash = true;
            else
                ptientdata.IsCash = false;
            //ptientdata.IsCash = $("#IsCash").val();
            if ($('#IsNeedSendEmail').is(":checked"))
                ptientdata.IsNeedSendEmail = true;
            else
                ptientdata.IsNeedSendEmail = false;
            //ptientdata.IsNeedSendEmail = $("#IsNeedSendEmail").val();
            ptientdata.IsHasVisited = true;

            SelectionOfActionInFactMagasine(ptientdata);
        }
    });

    //Add method of Regular expression. Where regexexpression it's RegularExpression, regex - name of JS function
    $.validator.addMethod('regex', function (value, element, regexp) {
        // var regexp = new RegExp(regexexpression);
        var check = false;
        return this.optional(element) || regexp.test(value);
    },
            "Не верный фомат.");

    //When ul InFactMagasine is active
    $(document).on('click', '#InFactMagasine', function () {
      
        if ($('#InFactMagasine').attr('aria-expanded') == "true")
        {
            infactdayofvisit = $("#datetimepicker1 input").val();
            RefreshInfactMagasineByDateAfterChangeDate(infactdayofvisit);
        }        
    });

    //Catch the change of date
    $("#datetimepicker1").on("dp.change", function () {
        //Get all patients which recorder at this day
        //if ($('#InFactMagasine').attr('aria-expanded') == "true")
        //{
            infactdayofvisit = $("#datetimepicker1 input").val();
            RefreshInfactMagasineByDateAfterChangeDate(infactdayofvisit);
        //}
    });
    
    function SelectionOfActionInFactMagasine(data)
    {
        if (KindOfActionInFactMagasine == "ADD")
        {
            //Here we call method jsGrid controller-->insertItem
            $("#InFactPatients").jsGrid("insertItem", data);
        }
        else if (KindOfActionInFactMagasine == "UPDATE")
        {
            //Here we call method jsGrid controller-->updateItem
            $("#InFactPatients").jsGrid("updateItem", DataForUpdateInFact, data);
        }
        else if ( KindOfActionInFactMagasine == "ADD_FROM_PREORDER_MAGASINE")
            {
                $("#InFactPatients").jsGrid("updateItem", data);
            }
    }

    function FillingFieldsOfModalWindow (data)
    {
        $('#RecordPatientToFact #LastName').val(data.LastName);
        $('#RecordPatientToFact #FirstName').val(data.FirstName);
        $('#RecordPatientToFact #MiddleName').val(data.MiddleName);
        $('#RecordPatientToFact #CellPhone').val(data.CellPhone);
        $('#RecordPatientToFact #BirthDate').val(data.ConvertedBirthDate);
        $('#RecordPatientToFact #Adress').val(data.Adress);
        $('#RecordPatientToFact #JobPlace').val(data.JobPlace);
        $('#RecordPatientToFact #Email').val(data.Email);

        //Add atribute readonly if administrator wants to Add patient to magasine "By Fact" from main
        //table of patient without Preorder Magasine
        $('#RecordPatientToFact #LastName').attr({ 'readonly': 'readonly' });
        $('#RecordPatientToFact #FirstName').attr({ 'readonly': 'readonly' });
        $('#RecordPatientToFact #MiddleName').attr({ 'readonly': 'readonly' });
        $('#RecordPatientToFact #CellPhone').attr({ 'readonly': 'readonly' });
        $('#RecordPatientToFact #BirthDate').attr({ 'readonly': 'readonly' });
        $('#RecordPatientToFact #Adress').attr({ 'readonly': 'readonly' });
        $('#RecordPatientToFact #JobPlace').attr({ 'readonly': 'readonly' });
        $('#RecordPatientToFact #Email').attr({ 'readonly': 'readonly' });

        if (KindOfActionInFactMagasine == "UPDATE" || KindOfActionInFactMagasine == "ADD_FROM_PREORDER_MAGASINE")
        {
            //Administrator can't change the date of note
            $('#RecordPatientToFact #VisitDateInFactMagasine').attr({ 'readonly': 'readonly' });           
            //remove click calendar event 
            $("#RecordPatientToFact #Calendar").css("pointer-events", "none");
                       
            $('#VisitDateInFactMagasine').val(data.ConvertDateOfVisit);
            $('#DoctorData').val(data.DoctorData);
            if (KindOfActionInFactMagasine == "ADD_FROM_PREORDER_MAGASINE"){
                        $('#RecordPatientToFact #ViewOfStudyInFact').attr({ 'readonly': 'readonly' });
                        $("#RecordPatientToFact #ViewOfStudyInFact").css("pointer-events", "none");
                        var manualIdStudy = data.StudyId + "#" + data.ConvertedFinalCost;
            }
            else {
                var manualIdStudy = data.StudyId + "#" + data.StudyCost;
                //$('#RecordPatientToFact #ViewOfStudyInFact').removeAttr('readonly');
                //$("#RecordPatientToFact #ViewOfStudyInFact").css("pointer-events", "auto");
            }
            
            //Manual creating Id of Study            
            
            $('#ViewOfStudyInFact').val(manualIdStudy);            
            $('#FinalCost').val(data.ConvertedFinalCost);            
            $('#NoteForDiscount').val(data.NoteForDiscount);
            //Filling Laborant fields
            var laborantdata = data.LaborantData;
            if (laborantdata.trim() != "")
                $('#AddOneMoreLaborant').css({ "display": "inline-block" });
            var separator = ', ';
            var laborantdata1;
            var laborantdata2;
            var valuelaborant1;
            var valuelaborant2;
            //One or two used laborants
            if (laborantdata.indexOf(separator) == -1)
            {
                laborantdata1 = laborantdata;
                //Search value of selected laborant by text
                valuelaborant1=GetValueOfLaborantByText(laborantdata1)
                $("#LaborantData").val(valuelaborant1);                               
            }
            else
            {
                var array = laborantdata.split(',', 2);
                laborantdata1 = array[0].trim();
                laborantdata2 = array[1].trim();
                //The first selected Laborant
                valuelaborant1=GetValueOfLaborantByText(laborantdata1)
                $("#LaborantData").val(valuelaborant1);

                //The Second selected Laborant                
                $('#TheSecondLaborant').css({ 'display': 'block' });
                //Here we copy DropDownList LaborantData
                $('#LaborantData').clone().attr({ 'id': 'LaborantData_1', 'name': 'lab_data' }).appendTo('.row #lab_num_2');
                $('#DeleteSecondLaborant').css({ 'display': 'inline-block' });
                valuelaborant2 = GetValueOfLaborantByText(laborantdata2)
                $("#LaborantData_1").val(valuelaborant2);


            }
            if (data.IsCash==true)
                $('#IsCash').prop('checked', true);
            else
                $('#IsCash').prop('checked', false);
            if (data.IsNeedSendEmail==true)
                $('#IsNeedSendEmail').prop('checked', true);
            else
                $('#IsNeedSendEmail').prop('checked', false);
        }
    }

    var GetValueOfLaborantByText= function (labdata)
    {
        var labvalue;
        $("#LaborantData option").each(function () {
            if ($(this).text() == labdata) {
                labvalue = $(this).val();                
            }

        });
        return labvalue;
    }

    
    //CHANGE TEXT OF HEADER TH WHEN WIDTH IS SMALL
    $(window).resize(function () {
        if ($(window).width() < 870)
        {
            $('#InFactPatients th:contains("№ п/п")').text("№");
            $('#InFactPatients th:contains("Обследование")').text("Обслед.");
            $('#InFactPatients th:contains("Сумма")').text("Сум.");
            $('#InFactPatients th:contains("На @ ?")').text("@ ?");

            $('#PreorderMagasine:contains("Журнал записи пациентов "По предзаписи")').text('"По предзаписи"');
            $('#InFactMagasine:contains("Журнал записи пациентов "По факту")').text('"По факту"');
        }
        else if ($(window).width() >= 870) {
            $('#InFactPatients th:contains("№")').text("№ п/п");
            $('#InFactPatients th:contains("Обслед.")').text("Обследование");
            $('#InFactPatients th:contains("Сум.")').text("Сумма");
            $('#InFactPatients th:contains("@ ?")').text("На @ ?");

            $('#PreorderMagasine:contains("По предзаписи")').text('"Журнал записи пациентов "По предзаписи"');
            $('#InFactMagasine:contains("По факту")').text('"Журнал записи пациентов "По факту"');
        }
        
    });
    
            
        
    
   
});

//Refresh Load Data after change Date of preordering or in first loading of page
function RefreshInfactMagasineByDateAfterChangeDate(item) {
    $.ajax({
        type: "Get",
        url: "Home/RefreshListInFactPatientByDate",
        data: { ConvertDateOfVisit: item },
        dataType: "JSON",
        success: function () {            
            $("#InFactPatients").jsGrid("loadData");
        },
        //},
        complete: function () {
            $("#InFactPatients").jsGrid("render");

            //Change text of the header if width < 870px
            var widthofwindow = window.innerWidth;
            if (widthofwindow < 870) {
                $('#InFactPatients th:contains("№ п/п")').text("№");
                $('#InFactPatients th:contains("Обследование")').text("Обслед.");
                $('#InFactPatients th:contains("Сумма")').text("Сум.");
                $('#InFactPatients th:contains("На @ ?")').text("@ ?");
            }

            $.holdReady(false);
           
        }

    });
}