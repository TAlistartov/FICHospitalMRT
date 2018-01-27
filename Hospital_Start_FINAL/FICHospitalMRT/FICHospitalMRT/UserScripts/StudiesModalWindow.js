$(document).ready(function () {
    //Update colour of elements of dropdownlist Studies in "Delete" & "Change" Containers
    UpdateStyleOfElementsDropDownListInDeleteContainer();
    UpdateStyleOfElementsDropDownListInChangeContainer();
    //---------------------------------------------START WORK WITH STUDIES>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    //Delete selected study from DB
    $(document).on('click', '#btnDeleteStudies', function () {
        var selectedDropdownAllStudies = $('#dropdownAllStudies').val();
        if (confirm("Вы действительно хотите удалить обследование???")) {
            $.ajax({

                type: 'POST',
                url: '/Home/DeleteStudy',
                data: { SelectedValue: selectedDropdownAllStudies },
                dataType: 'json',
                success: function (data) {
                    //updating the list of the studies (anti cash for GET request in brouser)
                    if (data.AddNewStudyResult==true)
                        alert('Запись была успешно удалена');
                    else
                        alert('Запись не была удалена');
                },

                error: function (xhr, status, error)
                {
                    alert('Возникла критическая ошибка ' + xhr.responseText + '|\n' + status + '|\n' + error);
                },
                
                complete: function () {
                    updategetallstudies();
                }

            });

        };
    });

    //Delete selected study from list of using Studies
    $(document).on('click', '#btnDeleteStudiesFromExisting', function () {
        var selectedDropdownAllStudies = $('#dropdownAllStudies').val();
        if (confirm("Вы действительно хотите пометить выбранное обследование как НЕИСПОЛЬЗУЕМОЕ?")) {
            $.ajax({

                type: 'GET',
                url: '/Home/UseOrUnuseSelectedStudy',
                data: { SelectedValue: selectedDropdownAllStudies, Used: false },
                dataType: 'json',
                success: function (data) {
                    //updating the list of the studies (anti cash for GET request in brouser)
                    if (data.AddNewStudyResult == true)
                        alert('Запись была успешно помечена как НЕИСПОЛЬЗУЕМАЯ.');
                    else
                        alert('Запись не была помечена как НЕИСПОЛЬЗУЕМАЯ.');
                },

                error: function (xhr, status, error) {
                    alert('Возникла критическая ошибка ' + xhr.responseText + '|\n' + status + '|\n' + error);
                },

                complete: function () {
                    updategetallstudies();
                }
                   

            });

        };
    });
    //Add selected study to list of using Studies
    $(document).on('click', '#btnRecoverStudiesFromExisting', function () {
        var selectedDropdownAllStudies = $('#dropdownAllStudies').val();
        if (confirm("Вы действительно хотите пометить выбранное обследование как ИСПОЛЬЗУЕМОЕ?")) {
            $.ajax({
                type: 'GET',
                url: '/Home/UseOrUnuseSelectedStudy',
                data: { SelectedValue: selectedDropdownAllStudies, Used: true },
                dataType: 'json',
                success: function (data) {
                    //updating the list of the studies (anti cash for GET request in brouser)
                    if (data.AddNewStudyResult == true)
                        alert('Запись была успешно помечена как ИСПОЛЬЗУЕМАЯ');
                    else
                        alert('Запись не была помечена как ИСПОЛЬЗУЕМАЯ');

                },

                error: function (xhr, status, error) {
                    alert('Возникла критическая ошибка ' + xhr.responseText + '|\n' + status + '|\n' + error);
                },

                complete: function () {

                    updategetallstudies();

                }
            });

        }
    });

    //Change Data for selected laborant
    $(document).on("change","#dropdownAllStudiesForChangingDB", function () {
        var fullnamestudyIdandCost = $("#dropdownAllStudiesForChangingDB").val();
        var fullnamestudyType = $('#dropdownAllStudiesForChangingDB option:selected').text();
        var array = fullnamestudyIdandCost.split('#',2);
        var studyId = array[0];
        var studyCost = array[1];
        $("#NameofStudyChangeDB").val(fullnamestudyType);
        $("#CostofStudyChangeDB").val(studyCost);
    });
    $(document).on("click", "#ChangeDataOfSelectedStudyButton", function () {
        var selectedStudy = $("#dropdownAllStudiesForChangingDB").val();
        var studyType = $('#NameofStudyChangeDB').val();
        var studyCost = $('#CostofStudyChangeDB').val();
        if (selectedStudy !="")
        {
            if (confirm('Вы действительно хотите изменить данные выбранного обследования, название на: ' + studyType + ' и его стоимость на ' + studyCost + '?')) {
                if (studyType != "" && studyCost != "") {
                    $.ajax
                    ({
                        type: 'POST',
                        url: '/Home/ChangeDataOfExistingStudy',
                        data: { SelectedValue: selectedStudy, ChangeDataType: studyType, ChangeDataCost: studyCost, Used: true },
                        dataType: 'json',
                        success: function (data) {
                            //updating the list of the studies (anti cash for GET request in brouser)
                            if (data.AddNewStudyResult == true) {
                                alert('Запись была успешно изменена.');
                                $("#NameofStudyChangeDB").val('');
                                $("#CostofStudyChangeDB").val('');
                            }
                            else
                                alert('Запись не была изменена.');
                            updategetallstudies();


                        },

                        error: function (xhr) {
                            SerchUncorrectInputFields(xhr)
                        },

                        complete: function () {
                            //updategetallstudies();
                        }
                    });
                }
                else alert("Невозможно изменить наименование и стоимость выбранного обследования на пустые строки.");
            }
            
        }
        else alert('Сначала выберите обследование.');
        
});

    //show & hide buttons in modal window for Delete/Recover study
    //$('#getallstudieslink').css('display', 'inline');
    //$('#GetAllStudiesButtons').css('display', 'none');
    //$('#getallstudieslink').click(function () {
    //    $('#GetAllStudiesButtons').slideToggle(200);
    //});
       
    //show & hide button in modal window for Change study
    //$('#ChangeStudyLink').css('display', 'inline');
    //$('#ChangeSelectedStudyButton').css('display', 'none');
    //$('#ChangeStudyLink').click(function () {
    //    $('#ChangeSelectedStudyButton').slideToggle(200);
    //});

    //Update colour of elements of dropdownlist Studies in "Delete Container"
    $(document).on('focusin', '#dropdownAllStudies', function () {
        UpdateStyleOfElementsDropDownListInDeleteContainer();
    });
    //Update colour of elements of dropdownlist Studies in "Change Container"
    $(document).on('focusin', '#dropdownAllStudiesForChangingDB', function () {
        UpdateStyleOfElementsDropDownListInChangeContainer();
    });

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<END WORK WITH STUDIES---------------------------------------------------------

    //Validation fields of save studies
    $('#form0').validate({
        rules:
         {
             Type:
                 {
                     required: true,
                     maxlength: 50
                 },
             Cost:
                 {
                     required: true,
                     minlength: 1,
                     maxlength: 6,
                     //name of JS function below
                     regex: /(\d{1,3},\d{1,2})|(\d{1,3}\.\d{1,2})|(^(?:\d+|\d{1,3}(?:,\d{3})+)?(?:\.\d+)?$)/
                 },

         },
        messages:
            {
                Type:
                    {
                        required: "Поле не должно быть пустым.",
                        maxlength: "Название обследования не может быть более 50 символов."
                    },
                Cost:
                    {
                        required: "Поле не должно быть пустым.",
                        minlength: "Стоимость не может быть меньше, либо равно 0.",
                        maxlength: "Стоимость не может привышать 999.99.",
                        regex: 'Проверьте валидность введенной стоимости.'
                    }
            },


    });

    //update container drodownlist all laborants in delete part and change part
    function updategetallstudies() {
        $.get('/Home/GetAllStudies?_=' + new Date().getTime(), null, function (data) {
            $('#getallstudies').html(data);
        });
        $.get('/Home/GetAllStudiesForChangingDB?_=' + new Date().getTime(), null, function (data) {
            $('#getallstudies1').html(data);
        });
    }

    //Change colour of elements of dropdownlist Studies in "Delete Container" which has UsedOrNot==false
    function UpdateStyleOfElementsDropDownListInDeleteContainer() {
        $.getJSON('/Home/GetAllStudiesWithAllFields', null, function (data) {
            var GetAllFieldsOfStudies = data.AllItemsFromDB;
            var AllOptionsDropDownList = $('#dropdownAllStudies option');
            for (row in GetAllFieldsOfStudies) {
                if (GetAllFieldsOfStudies[row].Used == false) {
                    for (option in AllOptionsDropDownList) {
                        if (GetAllFieldsOfStudies[row].StudyId == AllOptionsDropDownList[option].value) {
                            AllOptionsDropDownList[option].style.backgroundColor = '#f00';
                            break;
                        }

                    }
                }
            }
        });
    };
    //Change colour of elements of dropdownlist Studies in "Change Container" which has UsedOrNot==false
    function UpdateStyleOfElementsDropDownListInChangeContainer() {
        $.getJSON('/Home/GetAllStudiesWithAllFields', null, function (data) {
            var GetAllFieldsOfStudies = data.AllItemsFromDB;
            var AllOptionsDropDownList = $('#dropdownAllStudiesForChangingDB option');
            for (row in GetAllFieldsOfStudies) {
                if (GetAllFieldsOfStudies[row].Used == false) {
                    for (option in AllOptionsDropDownList) {
                        var IdAndCost = AllOptionsDropDownList[option].value;
                        var SeparateIdFromCost = IdAndCost.split('#', 2);
                        var Id = SeparateIdFromCost[0];
                        if (GetAllFieldsOfStudies[row].StudyId == Id) {
                            AllOptionsDropDownList[option].style.backgroundColor = '#f00';
                            break;
                        }

                    }
                }
            }
        });
    };

    //Checking inputed data on the server side. C# code in Helpers/ValidateAjax
    function SerchUncorrectInputFields(ArrayOfErrors) {
        var errorsArray = eval(ArrayOfErrors.responseText);//eval - The convert from string to the object
        for (i = 0; i < errorsArray.length; ++i) {
            var keys = errorsArray[i].key;
            var error = errorsArray[i].errors;
            $('input[name="' + keys + '"]').addClass("error");
            //Here We take Id of uncorrect input
            var newIdforErrorLabel = $('input[name="' + keys + '"]').attr('Id');
            //If error and input field hasn't error label, the programm will add a label 
            if (!($("label").is('#' + newIdforErrorLabel + '-error'))) {
                //Here we add label with text of error, add Id to this label and class=ERROR for JQuery validate style. 
                $('input[name="' + keys + '"]').after('<label class=error id=' + newIdforErrorLabel + '-error>' + error);
            }


        }
    };
    //Delete error classes and error message
    $('.checking').on('propertychange input', function () {
        var clickinput = $(this).attr('id');
        $('#' + clickinput).removeClass('error');
        $('#' + clickinput + '-error').remove();
    });

    //Close all popup in Modal window Studies/Laborants
    $('#modal_1').on('hidden.bs.modal', function () {
        //Clear all values
        //Studies
        $('#txtStudyType').val('');
        $('#txtStudyCost').val('');
        $('#dropdownAllStudiesForChangingDB').val('');        
        $('#NameofStudyChangeDB').val('');
        $('#CostofStudyChangeDB').val('');

        //Laborants
        $('#LastNameOfLaborant').val('');
        $('#FirstNameOfLaborant').val('');
        $('#dropdownAllLaborantsForChangingDB').val('');
        $('#LastNameofLaborantChange').val('');
        $('#FirstNameofLaborantChange').val('');       

        $('.collapse').collapse("hide");
    });

});

//Add method of Regular expression. Where regexexpression it's RegularExpression, regex - name of JS function
$.validator.addMethod('regex', function (value, element, regexp) {
    // var regexp = new RegExp(regexexpression);
    var check = false;
    return this.optional(element) || regexp.test(value);
},
        "Поле стоимости не может содержать символы кроме цифр и разделительных знаков");

//results of saving a new one study - SUCCESS or ERROR
function Onsuccess(data) {
    var nameOfsavingStudy = $('#txtStudyType').val();

    if (data.AddNewStudyResult == true) {
        alert('Запись ' + nameOfsavingStudy + ' была успешно добавлена');
        //Clear input field
        $('#txtStudyType').val('');
        $('#txtStudyCost').val('');
    }
    else {
        alert('Запись ' + nameOfsavingStudy + ' не была добавлена');
        //Clear input field
        $('#txtStudyType').val('');
        $('#txtStudyCost').val('');
    }
        
}

function OnFailure(xhr, status, error) {
    alert('Возникла критическая ошибка ' + xhr.responseText + '|\n' + status + '|\n' + error);
}

function OnComplete() {
    //updating the list of the studies when the study was added
    $.get('/Home/GetAllStudies?_=' + new Date().getTime(), null, function (data) {
        $('#getallstudies').html(data);
    });
    $.get('/Home/GetAllStudiesForChangingDB?_=' + new Date().getTime(), null, function (data) {
        $('#getallstudies1').html(data);
    });
}

