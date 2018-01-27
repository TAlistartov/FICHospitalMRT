//The row in JsGrid DataOfAllPatient is highlight
var row_is_highlighted = false;
//For method RowClick in jsGrid
var ptientdata=new Object();

$.holdReady(true);
RefreshListOfAllPatientFromDB();

$(document).ready(function () {
//------------------------------------START WORK WITH JsGrid DataOfAllPatient------------->>>>>>>>>>>>>>>>>>>>>>>>>>>
    var kind_of_action;
    //For UPDATING changed patient
    var itemToUpdate;
    
   
        
    $('#DataOfAllPatient').jsGrid({
             width: "100%",
             height: "auto",

             filtering: true,
             //inserting: true,
             editing: true,
             sorting: true,
             paging: true,
             autoload: true,
             noDataContent: "На выбранную дату данных не найдено.",
             deleteConfirm: function (item) {
                 return "Пациент " + item.LastName + " " + item.FirstName + " " + item.MiddleName + " будет удален. Вы хотите продолжить?"
             },
             pageSize: 8,

             rowClick: function (args) {
                 
                 //1st - Check: Has the table selected row???
                 if ($("#DataOfAllPatient tr").hasClass("highlight"))
                        $("#DataOfAllPatient tr").removeClass("highlight");
                 //2dh - Check: Highlight selectet row of the table
                 var $row = this.rowByItem(args.item);                 
                    if (!$row.hasClass("highlight"))
                        $row.toggleClass("highlight");
                   
                 //var $row = this.rowByItem(args.item);
                 //$row.toggleClass("highlight");
                   
                 if ($row.hasClass("highlight"))
                     {
                     row_is_highlighted = true;

                     //var ptientdata = new Object();
                     ptientdata.PatientId = args.item.PatientId;
                     ptientdata.LastName = args.item.LastName;
                     ptientdata.FirstName = args.item.FirstName;
                     ptientdata.MiddleName = args.item.MiddleName;
                     ptientdata.CellPhone = args.item.CellPhone;
                     ptientdata.ConvertedBirthDate = args.item.ConvertedBirthDate;
                     ptientdata.Adress = args.item.Adress;
                     ptientdata.JobPlace = args.item.JobPlace;
                     ptientdata.Email = args.item.Email;
                     ptientdata.Note = args.item.Note;
                      
                 }
             else
             {
                      row_is_highlighted=false;
                      patientdata = null;
             }
                 
             },

            //If we have done doubleClick on row then we want update data of patient
             rowDoubleClick: function (rowdata) {
                 kind_of_action = "UPDATE";
                 $("#ModalAddNewPatient .modal-header").append("<h4 class="+"modal-title>"+"Обновление данных пациента.</h4>");
                 //If we want to change patient Data
                 $("#ModalAddNewPatient").modal('show');

                 //Filling items from row of JsGrid
                 $("#PatientId").val(rowdata.item.PatientId);
                 $("#LastName").val(rowdata.item.LastName);
                 $("#FirstName").val(rowdata.item.FirstName);
                 $("#MiddleName").val(rowdata.item.MiddleName);
                 $("#BirthDate").val(rowdata.item.ConvertedBirthDate);
                 $("#CellPhone").val(rowdata.item.CellPhone);
                 $("#Adress").val(rowdata.item.Adress);
                 $("#JobPlace").val(rowdata.item.JobPlace);
                 $("#Email").val(rowdata.item.Email);
                 $("#Note").val(rowdata.item.Note);

                 //For jsGrid Method $("#DataOfAllPatient").jsGrid("updateItem", itemToUpdate, dataforsave);
                 itemToUpdate = rowdata.item;
                 
             },
             controller:
                 {
                     loadData: function (filter) {
                         return $.ajax({
                             type: 'GET',
                             url: '/Home/ReturnFilteringPatient',
                             //For filtering of patients
                             data: {LastName:filter.LastName, CellPhone:filter.CellPhone},
                             dataType: 'JSON',
                             complete: function () {
                                 //Mask for jsGrid. Filtering field
                                 $("#DataOfAllPatient .jsgrid-filter-row td:nth-child(5) input").attr('id', 'tel_input');
                                 $('#tel_input').mask("+38(999) 999-9999");
                             }
                         });
                         
                     },
                     updateItem: function (item) {
                         
                             return $.ajax({
                                 type: "POST",
                                 url: "/Home/UpdatePatientData",
                                 data: item,
                                 dataType: "JSON",
                                 success: function (data) {
                                     if (data.ResultOfReadingFromDB == true)
                                     {
                                         //Refresh Preorder and InFact magasines
                                         var date = $("#datetimepicker1 input").val();
                                         RefreshInfactMagasineByDateAfterChangeDate(date);
                                         RefreshPreorderMagasineByDateAfterChangeDate(date);

                                         alert("Изменения успешно внесены в БД.");                                                                                  
                                         $("#ModalAddNewPatient").modal('hide');
                                         
                                     }
                                         
                                     else
                                         alert("Не удалось внести изменения в БД.");
                                 },                                
                             });
                     },
                     insertItem: function (item) {
                         return $.ajax({
                             type: "POST",
                             url: "/Home/SaveNewPatient",
                             data: item,
                             dataType: "JSON",
                             success: function (data) {
                                 if (data.ResultOfReadingFromDB == true) {
                                     alert("Изменения успешно внесены в БД.");
                                     $("#ModalAddNewPatient").modal('hide');
                                 }

                                 else
                                     alert("Не удалось внести изменения в БД.");
                             }
                         });
                     },
                     deleteItem: function (item) {
                         return $.ajax({
                             type: "POST",
                             url: "/Home/DeleteSelectedPatient",
                             data: item,
                             dataType: "JSON",
                             success: function (data) {
                                 if (data.ResultOfReadingFromDB == true) 
                                     alert("Пациент успешно удален из БД.");
                                 else
                                     alert("Не удалось удалить пациента из БД.");
                                     }
                         });
                     }
                    
                 },

             fields: [
                 { name: "PatientId", type: "text", width: 2, align: "center" },
                 { name: "LastName", type: "text", width: 50, align: "center", title:"Фамилия" },
                 { name: "FirstName", type: "text", width: 30, align: "center", filtering: false, title: "Имя" },
                 { name: "MiddleName", type: "text", width: 40, align: "center", filtering: false, title: "Отчество" },
                 { name: "ConvertedBirthDate", type: "text", width: 40, align: "center", filtering: false, title: "Дата рождения" },
                 { name: "CellPhone", type: "text", width: 40, align: "center", sorting: false, title: "Номер телефона" },
                 { name: "Adress", type: "text", width: 50, align: "center", sorting: false },
                 { name: "JobPlace", type: "text", width: 10, align: "center", sorting: false },
                 { name: "Email", type: "text", width: 15, align: "center", sorting: false },
                 { name: "Note", type: "text", width: 70, align: "center", sorting: false },
                 {
                     type: "control",
                     //modeSwitchButton: false,
                     editButton: false,
                     width:20,
                     headerTemplate: function () {
                         return $("<button>").attr({ "type": "button", "id": "add" }).html("<b class=' visible-lg visible-md hidden-sm hidden-xs'>Создать</b>"+ 
                             "<b class=' hidden-lg hidden-md visible-sm visible-xs'>+</b>").on("click", function () {
                                                    kind_of_action = "ADD";
                                                    $("#ModalAddNewPatient .modal-header").append("<h4 class=" + "modal-title>" + "Создание нового пациента.</h4>");
                                                              //If we want to ADD a new patient to List
                                                               $("#ModalAddNewPatient").modal('show');
                                                });
                         //.text('<b>'+"Создать"+'</b>')
    }

                 }
             ]
         });
    
     
    //Hide all unused tags
    $("#DataOfAllPatient").jsGrid("fieldOption", "PatientId", "visible", false);
    $("#DataOfAllPatient").jsGrid("fieldOption", "Adress", "visible", false);
    $("#DataOfAllPatient").jsGrid("fieldOption", "JobPlace", "visible", false);
    $("#DataOfAllPatient").jsGrid("fieldOption", "Email", "visible", false);
    $("#DataOfAllPatient").jsGrid("fieldOption", "Note", "visible", false);

      //Validate Form SaveChangeDataOfPatient
        $("#SaveChangeDataOfPatient").validate({
            rules: {
                lastname: { required: true, maxlength: 30 },
                firstname: { required: true, maxlength: 30 },
                middlename: { required: true, maxlength: 30 },
                cellphone: { required: true, maxlength: 17 },
                birthdate: { ukrainianDate: true },
                adress: { maxlength: 150 },
                jobplace: {maxlength: 50},
                email: { email: true, maxlength: 50 },
                note: { maxlength: 500 }
                

            },
            messages: {
                lastname: { required: "Фамилия не указана, введите фамилию пациента.", maxlength: "Фамилия должна занимать не более 30 символов."},
                firstname: { required: "Имя не указано, введите имя пациента.", maxlength: "Имя должно занимать не более 30 символов." },
                middlename: { required: "Отчество не указано, введите отчество пациента.", maxlength: "Отчество должна занимать не более 30 символов." },
                cellphone: { required: "Телефон не указан, введите номер телефона пациента.", maxlength: "Номер телефона занимает более 17 символов." },
                adress: { maxlength: "Адрес должен занимать не более 150 символов." },
                jobplace: { maxlength: "Место работы занимает более 50 символов." },
                email: { email: "Вы ввели не корректный email,(_____@____.__).", maxlength: "Email занимает более 50 символов" },
                note: { maxlength: "Заметка должна занимать не более 500 символов." }
                //birthdate: "Введите корректную дату рождения."
            },
            submitHandler: function(form) {
             
            var data_of_patient={
                            PatientId : $ ("#PatientId").val(),
                            LastName : $("#LastName").val(),
                            FirstName : $("#FirstName").val(),
                            MiddleName: $("#MiddleName").val(),
                            ConvertedBirthDate: $("#BirthDate").val(),
                            CellPhone: $("#CellPhone").val(),
                            Adress: $("#Adress").val(),
                            JobPlace:$("#JobPlace").val(),
                            Email:$("#Email").val(),
                            Note:$("#Note").val()
                    };
       
                    AddOrUpdatePatient(data_of_patient);
                // form.submit(); // If we use form.submit(); the full page will be reloaded
              
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


    //The mask for validation CellPhone field
    $("#CellPhone").mask("+38(999) 999-9999");    

    //clear all fields and validation errors in form ModalAddNewPatient when modal window has closed
   $(document).on('hidden.bs.modal', '#ModalAddNewPatient', function () {
       $("#SaveChangeDataOfPatient").validate().resetForm();
       $("#SaveChangeDataOfPatient").find(".error").removeClass("error");
       $(".my_reset").val("");
       $("h4.modal-title").remove();
   });

   //Action selection: Update or Add
   function AddOrUpdatePatient(dataforsave) {
       if (kind_of_action == "UPDATE")
       {
           //Here we call method jsGrid controller-->updateItem
           $("#DataOfAllPatient").jsGrid("updateItem", itemToUpdate, dataforsave);
       }
       else if (kind_of_action == "ADD")
       {
           //Here we call method jsGrid controller-->insertItem
           $("#DataOfAllPatient").jsGrid("insertItem",dataforsave);
       }
   };
//------------------------------------END WORK WITH JsGrid DataOfAllPatient------------->>>>>>>>>>>>>>>>>>>>>>>>>>>


});
//The function which will be called for refreshing list of all created patients in DB
function RefreshListOfAllPatientFromDB() {
   $.ajax({
        type: "GET",
        url: "Home/RefreshListOfPatient",
        dataType: "JSON",
        success: function (data) {
            if (data==true)
                $.holdReady(false);
        }
    });

};


