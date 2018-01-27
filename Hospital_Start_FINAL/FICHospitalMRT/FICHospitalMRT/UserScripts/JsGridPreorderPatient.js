$.holdReady(true);
//At first time after Loading page, don't call HelperRefreshPreorderMagasine in GetAllPreorderingPatientByDate method
var latch_of_first_refresh = true;
RefreshPreorderMagasineByDate(currentday);
//RefreshInfactMagasineByDateAfterChangeDate(currentday);
var row_is_highlighted_preordermagasine;
var ptientdata_from_preordermagasine=new Object();
$(document).ready(function () {
    var KindOfAction;
    var DataForUpdate;

       //clear all fields and validation errors in form RecordPatientToPreorderMagasine when modal window has closed
    $(document).on('hidden.bs.modal', '#RecordPatientToPreorderMagasine', function () {
        $("#RecordPatientToPreorder").validate().resetForm();
        $("#RecordPatientToPreorder").find(".error").removeClass("error");
        //When modal window is closing we reset date field
        $(".my_reset").val("");
        //When modal window is closing we reset select study field
        $("#ViewOfStudy option:selected").each(function () {
            this.selected = false;
        });
        KindOfAction = "";
        $("#info").remove();
    });

    //The main tools of DateTimePicker
    $(function () {
        $('#DateAndTimeOfPreorder').datetimepicker(
               {
                   language: 'ru',
                   daysOfWeekDisabled: [0],
                   minuteStepping:5
               }
            );
    });

    //Different action if patient in main table doesn't selected and if it selected
    $(document).on("click", "#Preorder", function ()
    {
        if (row_is_highlighted == true)
        {
            KindOfAction = "ADD";
            $("#RecordPatientToPreorderMagasine").modal('show');
            ptientdata;
        }
        else
            alert("Сначала выберите пациента для дальнейшей записи...");
    });

    ////If user clicked on Save button in the modal window id="RecordPatientToPreorderMagasine"
    ////We add a new information: day, time of visit and study type. patientdata we took from JsGridAllPatient
    //$(document).on("click", "#btn_recordtopreordermagasine", function () {
                   
    //    $('#PreorderPatients').jsGrid('insertItem', ptientdata);
    //});

    $('#RecordPatientToPreorderMagasine').on('hidden.bs.modal', function () {
        $("#PreorderPatients").jsGrid("sort", { field: "ConvertedTimeOfVisit", order: "asc" })
        .done(function () {
            //$("#PreorderPatients").jsGrid("render");
            $("#PreorderPatients").jsGrid("reset");           
        });
    })

    //----------------------------------START WORK WITH JsGrid PreorderPatients------------->>>>>>>>>>>>>>>>>>>>>>>>>>>
    $('#PreorderPatients').jsGrid({
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
            KindOfAction = "UPDATE";
            $("#RecordPatientToPreorderMagasine").modal('show');
            //Send necessary data to partial View RecordPatientToPreorderMagasine
            $("#DateOfPreorder").val(rowdata.item.ConvertDateOfVisit);
            ptientdata.ConvertDateOfVisit = $("#DateOfPreorder").val();

            $("#ViewOfStudy").val(rowdata.item.StudyId);
            ptientdata.StudyId = $("#ViewOfStudy").val();

            $("#RecordPatientToPreorder").prepend("<div class='row' id='info'>"+
                                                        "<div class='form-group '>" +
                               "<label for='Information' class='col-md-3 col-lg-3 col-sm-3 col-xs-3 control-label text-right'>Информация:</label>" +
                                        "<div class=' input-group col-lg-4 col-md-4 col-sm-4 col-xs-4 col-offset'>" +
                                            "<input type='text' class='form-control my_reset' id='Information' readonly='readonly'>" +
                                        "</div>" +
                                   "</div>"+
                        "</div>");

            $("#Information").val(rowdata.item.ConvertedTimeOfVisit+' -> '+ rowdata.item.LastName +' '+ rowdata.item.FirstName);

            //Save all informatien about selected note for UPDATion
            DataForUpdate = rowdata.item;
        },
        rowClick: function (args) {               
            
            //var $row = this.rowByItem(args.item);
            //$row.toggleClass("highlight_preorder");

            //1st - Check: Has the table selected row???
            if ($("#PreorderPatients tr").hasClass("highlight_preorder"))
                $("#PreorderPatients tr").removeClass("highlight_preorder");
            //2dh - Check: Highlight selectet row of the table
            var $row = this.rowByItem(args.item);
            if (!$row.hasClass("highlight_preorder"))
                $row.toggleClass("highlight_preorder");
                   
            //Copy all data from selected row
            if ($row.hasClass("highlight_preorder"))
            {
                row_is_highlighted_preordermagasine = true;

                ptientdata_from_preordermagasine.PatientId = args.item.PatientId;
                ptientdata_from_preordermagasine.LastName = args.item.LastName;
                ptientdata_from_preordermagasine.FirstName = args.item.FirstName;
                ptientdata_from_preordermagasine.MiddleName = args.item.MiddleName;
                ptientdata_from_preordermagasine.CellPhone = args.item.CellPhone;
                ptientdata_from_preordermagasine.ConvertedBirthDate = args.item.ConvertedBirthDate;
                ptientdata_from_preordermagasine.Adress = args.item.Adress;
                ptientdata_from_preordermagasine.JobPlace = args.item.JobPlace;
                ptientdata_from_preordermagasine.Email = args.item.Email;
                ptientdata_from_preordermagasine.Note = args.item.Note;
                ptientdata_from_preordermagasine.StudyId = args.item.StudyId;
                ptientdata_from_preordermagasine.ConvertedTimeOfVisit = args.item.ConvertedTimeOfVisit;
                ptientdata_from_preordermagasine.ConvertDateOfVisit = args.item.ConvertDateOfVisit;
                ptientdata_from_preordermagasine.LaborantData = "";
                var arr = args.item.ConvertedStudyCost.split(',', 2);
                ptientdata_from_preordermagasine.StudyCost = arr[0];

                //Parse to View 60-->6 and 63-->63
                var secondPartArray = arr[1];
                //Here we check the number contains 0(zero)                
                    var FinalCost = secondPartArray.replace(/0/g, "");                
                    if (FinalCost !== "")
                        ptientdata_from_preordermagasine.ConvertedFinalCost = arr[0] + ',' + FinalCost; //args.item.ConvertedStudyCost;
                    else
                        ptientdata_from_preordermagasine.ConvertedFinalCost = arr[0];
            }
            else
            {
                row_is_highlighted_preordermagasine=false;
                //ptientdata_from_preordermagasine = null;
            }
        },
        pageSize: 8,
        fields: [
                  { name: "ConvertedTimeOfVisit", type: "text", width: 30, align: "center", filtering: false, title: "Время"},
                  { name: "LastName", type: "text", width: 40, align: "center", title: "Фамилия" },
                  { name: "FirstName", type: "text", width: 30, align: "center", sorting: false, filtering: false, title: "Имя" },
                  { name: "MiddleName", type: "text", width: 30, align: "center", sorting: false, filtering: false, title: "Отчество" },
                  { name: "CellPhone", type: "text", width: 40, align: "center", sorting: false, title: "Номер телефона" },
                  { name: "StudyType", type: "text", width: 50, align: "center", sorting: false, title: "Обследование" },
                  { name: "Note", type: "textarea", width: 50, align: "center", sorting: false, filtering:false,title: "Пометка" },

                  { name: "PatientId", type: "text", witdth: 0, align: "center" },
                  { name: "ConvertedBirthDate", type: "text", width: 0, align: "center", filtering: false, title: "Дата рождения" },
                  { name: "Adress", type: "text", width: 0, align: "center", sorting: false },
                  { name: "JobPlace", type: "text", width: 0, align: "center", sorting: false },
                  { name: "Email", type: "text", width: 0, align: "center", sorting: false },
                  { name: "StudyId", type: "text", width: 0, align: "center", sorting: false },
                  { name: "ConvertDateOfVisit", type: "text", width: 0, align: "center", sorting: false },
                  { name: "ConvertedStudyCost", type: "text", width: 0, align: "center", sorting: false },
           {
               type: "control",
               //modeSwitchButton: false,
               editButton: false,
               width: 20
           }
        ],

            controller:
                    {
                         loadData: function (filter) {
                                return $.ajax({
                                    type: 'GET',
                                    url: '/Home/GetAllPreorderingPatientByDate',
                                    //For filtering of patients
                                    data: {
                                        LastName: filter.LastName, CellPhone: filter.CellPhone,
                                        StudyType: filter.StudyType, ConvertDateOfVisit: currentday, Latch: latch_of_first_refresh
                                    },
                                    dataType: 'JSON',
                                    //success: function(){
                                    //    CountMoneyPerDayCashAndTerminal();
                                    //},
                                    complete: function () {
                                        latch_of_first_refresh = false;
                                        //Mask for jsGrid. Filtering field
                                        $("#PreorderPatients .jsgrid-filter-row td:nth-child(5) input").attr('id', 'tel_input1');
                                        $('#tel_input1').mask("+38(999) 999-9999");

                                    }
                                });
                         },
                         insertItem: function (item) {
                             return $.ajax({
                                 type: 'POST',
                                 url: '/Home/AddNewPatientToPreorderMagasine',
                                 data: item,
                                 dataType: 'JSON',
                                 success: function (data) {
                                     if (data.Result == true)
                                     {
                                         $("#PreorderPatients").jsGrid("render").done(function () {
                                             $("#RecordPatientToPreorderMagasine").modal('hide');
                                             $("#DataOfAllPatient tr").removeClass("highlight");
                                         })
                                        alert("Пациент был добавлен в журнал 'Предзаписи'.");
                                         
                                     }
                                     else
                                         alert("Не удалось добавить пациента в журнал 'Предзаписи'.");
                                     
                                 }
                             })
                         },
                         updateItem: function (item) {

                             return $.ajax({
                                 type: "POST",
                                 url: "/Home/UpdateNoteOfPreorderMagasine",
                                 data: item,
                                 dataType: "JSON",
                                 success: function (data) {
                                     if (data.Result == true) {
                                         $("#PreorderPatients").jsGrid("render").done(function () {
                                             $("#RecordPatientToPreorderMagasine").modal('hide');
                                         })
                                         alert("Изменения успешно внесены в БД.");
                                         //$("#RecordPatientToPreorderMagasine").modal('hide');
                                     }

                                     else
                                         alert("Не удалось внести изменения в БД.");
                                 },
                             });
                         },
                         deleteItem: function (item) {
                             return $.ajax({
                                 type: "POST",
                                 url: "/Home/DeleteNoteFromPreorderMagasine",
                                 data: item,
                                 dataType: "JSON",
                                 success: function (data) {
                                     if (data.Result == true)
                                         alert("Запись успешно удалена из БД.");
                                     else
                                         alert("Не удалось удалить запись из БД.");
                                 }
                             });
                         }
                    }
    });

    //Hide all unused tags
    $("#PreorderPatients").jsGrid("fieldOption", "PatientId", "visible", false);
    $("#PreorderPatients").jsGrid("fieldOption", "Adress", "visible", false);
    $("#PreorderPatients").jsGrid("fieldOption", "JobPlace", "visible", false);
    $("#PreorderPatients").jsGrid("fieldOption", "Email", "visible", false);
    $("#PreorderPatients").jsGrid("fieldOption", "ConvertedBirthDate", "visible", false);
    $("#PreorderPatients").jsGrid("fieldOption", "StudyId", "visible", false);
    $("#PreorderPatients").jsGrid("fieldOption", "ConvertDateOfVisit", "visible", false);
    $("#PreorderPatients").jsGrid("fieldOption", "ConvertedStudyCost", "visible", false);

    $("#RecordPatientToPreorder").validate({
        rules: {
            VisitDateandTime: { required: true},
            SelectedStudy: { required: true }
        },

        messages: {
            VisitDateandTime: { required: "Укажите дату визита." },
            SelectedStudy: { required: "Укажите обследование." }
        },

        submitHandler: function (form) {
             
            //var arr;
            //All necessary information for creating new note in "Preorder Magasine"
            if (KindOfAction == "UPDATE")
            {
                ptientdata.NewConvertDateOfVisit = $("#DateOfPreorder").val();
                var array = ptientdata.NewConvertDateOfVisit.split(" ", 2);
                ptientdata.NewConvertedTimeOfVisit = array[1];

                ptientdata.NewStudyId = $("#ViewOfStudy").val();
            }
            else if (KindOfAction == "ADD")
            {
                //1) Visit Table:
                ptientdata.NewConvertDateOfVisit = $("#DateOfPreorder").val();
                var array = ptientdata.NewConvertDateOfVisit.split(" ", 2);
                ptientdata.ConvertedTimeOfVisit = array[1];
                ptientdata.ConvertDateOfVisit = $("#date_for_selection_patients_notes").val();
                //3) TheStudyProcess Table
                ptientdata.StudyId = $("#ViewOfStudy").val();
                
            }

            //1) Visit Table:           
            ptientdata.IsPreorder = true;
            ptientdata.IsNeedSendEmail = false;
            ptientdata.IsHasVisited = false;
            //2) Payment Table:
            ptientdata.IsCash = false;

            //3) TheStudyProcess Table
            ptientdata.StudyType = $("#ViewOfStudy option:selected").text();
           
            Selection_Of_Action(ptientdata);
    }
    });

    //Own validation method
    $.validator.addMethod(
       "ukrainianDate",
       function (value, element) {
           var check = false;
           var re = /^\d{1,2}\.\d{1,2}\.\d{4}$/;
           if (re.test(value)) {
               var adata = value.split('.');
               var dd = parseInt(adata[0], 10);
               var mm = parseInt(adata[1], 10);
               var yyyy = parseInt(adata[2], 10);
               var xdata = new Date(yyyy, mm - 1, dd);
               if ((xdata.getFullYear() === yyyy) && (xdata.getMonth() === mm - 1) && (xdata.getDate() === dd)) {
                   check = true;
               }
               else {
                   check = false;
               }
           } else {
               check = false;
           }
           return this.optional(element) || check;
       },
       "Правильный формат даты ДД.ММ.ГГГГ"
   );

//------------------------------------END WORK WITH JsGrid PreorderPatients------------->>>>>>>>>>>>>>>>>>>>>>>>>>>

    //Catch the change of date
    $("#datetimepicker1").on("dp.change", function () {
        //Get all patient whow recorder at this day
        var dayofvisit = $("#datetimepicker1 input").val();
        RefreshPreorderMagasineByDateAfterChangeDate(dayofvisit);
        RefreshListOfCharges(dayofvisit);
    });
   
    //Action Selection - Insert or Update
    function Selection_Of_Action(data) {
        if(KindOfAction=="UPDATE")
            //Here we call method jsGrid controller-->updateItem
            $("#PreorderPatients").jsGrid("updateItem", DataForUpdate, data);
        else if (KindOfAction=="ADD")
            //Here we call method jsGrid controller-->insertItem
            $("#PreorderPatients").jsGrid("insertItem", data);
    };
    
    //Change size of <th>
    $(window).resize(function () {
        if ($(document).width() <= 735) {
            $('#PreorderPatients th:contains("Обследование")').text('Обслед.');
        }
        else if ($(document).width() > 735) {
            $('#PreorderPatients th:contains("Обслед.")').text('Обследование');
        }
    });
    
   
   
    
});
//Refresh Load Data after change Date of preordering
function RefreshPreorderMagasineByDateAfterChangeDate(item) {
    $.ajax({
        type: "Get",
        url: "Home/RefreshListPreorderPatientByDate",
        data: { ConvertDateOfVisit: item },
        dataType: "JSON",
        success: function (data) {
            if (data == true) {
                $("#PreorderPatients").jsGrid("loadData");
            }
        },
        complete: function () {
            $("#PreorderPatients").jsGrid("render");           
        }

    });
}
//Refresh Load Data after finishing load page
function RefreshPreorderMagasineByDate(item) {
    $.ajax({
        type: "GET",
        url: "Home/RefreshListPreorderPatientByDate",
        data: { ConvertDateOfVisit: item },
        dataType: "JSON",
        success: function (data) {
             if (data == true) {
                $.holdReady(false);
            }

        }
    });
};