$(document).ready(function () {
//----------------------------------------START WORK WITH LABORANTS>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    //Call function of updating DropDownList style, when site has loaded
        UpdateStyleOfElementsDropDownListInDeleteContainer();
        UpdateStyleOfElementsDropDownListInChangeContainer();

    //Save new laborant to DB
    $(document).on('click', '#SaveNewLaborantButton', function () {
        var LaborantLastName = $('#LastNameOfLaborant').val();
        var LaborantFirstName = $('#FirstNameOfLaborant').val();
        if (LaborantLastName != "" && LaborantFirstName != "")
            {
        if(confirm("Вы действительно хотите сохранить лаборанта: "+LaborantLastName+" "+LaborantFirstName+"?"))
        {
            $.ajax({

                type: 'POST',
                url: '/Home/SaveNewLaborant',
                data: { LastName: LaborantLastName, FirstName: LaborantFirstName, Used: true},
                dataType: 'json',
                success: function (data) {
                    //updating the list of the studies (anti cash for GET request in brouser)
                    if (data.AddNewLaborantResult == true)
                    {
                        $('#LastNameOfLaborant').val('');
                        $('#FirstNameOfLaborant').val('');
                        alert('Запись была успешно сохранена.');
                    }
                        
                    else
                        alert('Запись не была сохранена!');

                    
                },

                //Checking on the server side. Helpers/ValidateAjax
                error: function (xhr) {
                    SerchUncorrectInputFields(xhr)
                },

                complete: function ()
                {
                    updategetalllaborants();
                }

            });
        }
        }
        else alert("Невозможно сохранить лаборанта без имени и фамилии.")
    });

    //Delete selected Laborant from DB
    $(document).on('click', '#btnDeleteLaborant', function () {
        var selecteddropdownAllLaborants = $('#dropdownAllLaborants').val();

        if (confirm("Вы действительно хотите удалить лаборанта???"))
        {
            $.ajax({
                type: 'POST',
                url: '/Home/DeleteLaborantFromDB',
                data: { SelectedValue: selecteddropdownAllLaborants },
                dataType: 'json',
                success: function (data) {
                    //updating the list of the studies (anti cash for GET request in brouser)
                    if (data.AddNewLaborantResult == true)
                        alert('Запись была успешно удалена');
                    else
                        alert('Запись не была удалена');

                    
                },

                error: function (xhr, status, error) {
                    alert('Возникла критическая ошибка ' + xhr.responseText + '|\n' + status + '|\n' + error);
                },

                complete: function ()
                {
                    updategetalllaborants();
                }
               

            });

        }

    });

    //Delete selected laborant from list of using Laborants
    $(document).on('click', '#btnDeleteLaborantFromExisting', function () {
        var selecteddropdownAllLaborants=$('#dropdownAllLaborants').val();
        if(confirm("Вы действительно хотите пометить выбранного лаборанта как НЕИСПОЛЬЗУЕМОГО?"))
        {
            $.ajax({
                type: 'POST',
                url: '/Home/UseOrUnuseSelectedLaborant',
                data: { SelectedValue: selecteddropdownAllLaborants, Used: false },
                dataType: 'json',
                success: function (data) {
                    //updating the list of the studies (anti cash for GET request in brouser)
                    if (data.AddNewLaborantResult == true)
                        alert('Запись была успешно помечена как НЕИСПОЛЬЗУЕМАЯ.');
                    else
                        alert('Запись не была помечена как НЕИСПОЛЬЗУЕМАЯ.');
                   
                },

                error: function (xhr, status, error) {
                    alert('Возникла критическая ошибка ' + xhr.responseText + '|\n' + status + '|\n' + error);
                },

                complete: function () {
                    updategetalllaborants();                   
                }
            });
               
        }
    });

    //Add selected laborant to list of using Laborants
    $(document).on('click', '#btnRecoverLaborantFromExisting', function () {
        var selecteddropdownAllLaborants = $('#dropdownAllLaborants').val();
        if (confirm("Вы действительно хотите пометить выбранного лаборанта как ИСПОЛЬЗУЕМОГО?")) {
            $.ajax({
                type: 'POST',
                url: '/Home/UseOrUnuseSelectedLaborant',
                data: { SelectedValue: selecteddropdownAllLaborants, Used: true},
                dataType: 'json',
                success: function (data) {
                    //updating the list of the studies (anti cash for GET request in brouser)
                    if (data.AddNewLaborantResult == true)
                        alert('Запись была успешно помечена как ИСПОЛЬЗУЕМАЯ');
                    else
                        alert('Запись не была помечена как ИСПОЛЬЗУЕМАЯ');
                                        
                },

                error: function (xhr, status, error) {
                    alert('Возникла критическая ошибка ' + xhr.responseText + '|\n' + status + '|\n' + error);
                },

                complete: function () {
                    
                    updategetalllaborants();

                }
            });

        }
    });

    //Change Data for selected laborant
    $(document).on('change', '#dropdownAllLaborantsForChangingDB', function () {
        var fullname = $("#dropdownAllLaborantsForChangingDB option:selected").text();
        var arr = fullname.split(' ', 2);
        var LastName = arr[0];
        var FirstName = arr[1];
        $('#LastNameofLaborantChange').val(LastName);
        $('#FirstNameofLaborantChange').val(FirstName);
    });
    $(document).on('click', '#ChangeSelectedLaborantInDBButton', function () {
        var selecteddropdownAllLaborants1 = $('#dropdownAllLaborantsForChangingDB').val();
        var NewLastNameOfLaborant = $('#LastNameofLaborantChange').val();
        var NewFirstNameOfLaborant = $('#FirstNameofLaborantChange').val();
        if (selecteddropdownAllLaborants1 != "")
        {
            if (NewLastNameOfLaborant != "" && NewFirstNameOfLaborant != "")
                {
        if (confirm("Вы действительно хотите изменить данные выбранного лаборанта на: "+NewLastNameOfLaborant+ " "+NewFirstNameOfLaborant+"?"))
        {
            $.ajax({
                type: 'POST',
                url: '/Home/ChangeDataOfExistingLaborant',
                data: {SelectedValue: selecteddropdownAllLaborants1, LastNameChangeLaborant: NewLastNameOfLaborant,
                    FirstNameChangeLaborant: NewFirstNameOfLaborant, Used: true
                },
                dataType: 'json',
                success: function (data) {
                    //updating the list of the studies (anti cash for GET request in brouser)
                    if (data.AddNewLaborantResult == true)
                        {
                            alert('Запись была успешно изменена.');
                            $("#LastNameofLaborantChange").val('');
                            $("#FirstNameofLaborantChange").val('');
                        }
                    else
                        alert('Запись не была изменена.');

                                       
                },

                error: function (xhr) {
                    SerchUncorrectInputFields(xhr)
                },

                complete: function ()
                {                    
                    updategetalllaborants();
                }
            })
        }
            }
            else alert("Невозможно изменить имя и фамилию выбранного лаборанта на пустые строки.")
        }
        else alert('Сначала выберите лаборанта.')

    });

    //Update colour of elements of dropdownlist Laborants in "Delete Container"
    $(document).on('focusin', '#dropdownAllLaborants', function () {
        UpdateStyleOfElementsDropDownListInDeleteContainer();
    });
    //Update colour of elements of dropdownlist Laborants in "Change Container"
    $(document).on('focusin', '#dropdownAllLaborantsForChangingDB', function () {
        UpdateStyleOfElementsDropDownListInChangeContainer();
    });

//-------------------------------------VALIDATION ON THE SERVER SIDE>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    //Checking inputed data on the server side. C# code in Helpers/ValidateAjax
    function SerchUncorrectInputFields(ArrayOfErrors) {
        var errorsArray = eval(ArrayOfErrors.responseText);//eval - The convert from string to the object
        for (i = 0; i < errorsArray.length;++i)
        {
            var keys = errorsArray[i].key;
            var error = errorsArray[i].errors;
            $('input[name="' + keys + '"]').addClass("error");
            //Here We take Id of uncorrect input
            var newIdforErrorLabel = $('input[name="' + keys + '"]').attr('Id');
            //If error and input field hasn't error label, the programm will add a label 
            if (!($("label").is('#' + newIdforErrorLabel + '-error')))
            {
                //Here we add label with text of error, add Id to this label and class=ERROR for JQuery validate style. 
                $('input[name="' + keys + '"]').after('<label class=error id=' + newIdforErrorLabel + '-error>' + error);
            }
                        
                        
        }
    };                    
    //Delete error classes and error message
    $('.checking').on('propertychange input', function () {
        var clickinput = $(this).attr('id');       
        $('#' + clickinput).removeClass('error');
        $('#'+clickinput + '-error').remove();
    });
   
      
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<END OF SCRIPTS OF VALIDATION ON THE SERVER SIDE--------------------------------


    //update container drodownlist all laborants in delete part and change part
    function updategetalllaborants()
    {
        $.get('/Home/GetAllLaborants?_=' + new Date().getTime(), null, function (data) {
            $('#GetAllLaborants').html(data);
        });
        $.get('/Home/GetAllLaborantsForChangingDB?_=' + new Date().getTime(), null, function (data) {
            $('#GetAllLaborants1').html(data);
        });
    }

    //Change colour of elements of dropdownlist Laborants in "Delete Container" which has UsedOrNot==false
    function UpdateStyleOfElementsDropDownListInDeleteContainer() {
      $.getJSON('/Home/GetAllLaborantsWithAllFields', null, function (data) {
            var GetAllFieldsOfLaborants = data.AllItemsFromDB;
            var AllOptionsDropDownList = $('#dropdownAllLaborants option');
            for (row in GetAllFieldsOfLaborants) {
                if (GetAllFieldsOfLaborants[row].Used == false) {
                    for (option in AllOptionsDropDownList) {
                        if (GetAllFieldsOfLaborants[row].LaborantId == AllOptionsDropDownList[option].value) {
                            AllOptionsDropDownList[option].style.backgroundColor = '#f00';
                            break;
                        }

                    }
                }
            }
      });
    };
    

    //Change colour of elements of dropdownlist Laborants in "Change Container" which has UsedOrNot==false
    function UpdateStyleOfElementsDropDownListInChangeContainer() {
        $.getJSON('/Home/GetAllLaborantsWithAllFields', null, function (data) {
            var GetAllFieldsOfLaborants = data.AllItemsFromDB;
            var AllOptionsDropDownList = $('#dropdownAllLaborantsForChangingDB option');
            for (row in GetAllFieldsOfLaborants) {
                if (GetAllFieldsOfLaborants[row].Used == false) {
                    for (option in AllOptionsDropDownList) {
                        if (GetAllFieldsOfLaborants[row].LaborantId == AllOptionsDropDownList[option].value) {
                            AllOptionsDropDownList[option].style.backgroundColor = '#f00';
                            break;
                        }

                    }
                }
            }
        });
    };

   
    //show & hide buttons in modal window for Delete/Recover Laborant
    //$('#getalllaborantslink').css('display', 'inline');
    //$('#GetAllLaborantsButtons').css('display', 'none');
    //$('#getalllaborantslink').click(function () {
    //    $('#GetAllLaborantsButtons').slideToggle(200);
    //});

    //show & hide button in modal window for Change Laborant
    //$('#ChangeLaborantLink').css('display', 'inline');
    //$('#ChangeSelectedLaborantButton').css('display', 'none');
    //$('#ChangeLaborantLink').click(function () {
    //    $('#ChangeSelectedLaborantButton').slideToggle(200);
    //});

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<END WORK WITH STUDIES---------------------------------------------------------

});