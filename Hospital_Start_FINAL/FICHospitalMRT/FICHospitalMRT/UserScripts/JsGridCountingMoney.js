window.onload = function () {
    RefreshListOfCharges(currentday);
};

$(document).ready(function () {
    $()
    $('#ListOfCharges').jsGrid({
        width: "55%",
        height: "auto",
        //onItemUpdating: function (args) {
        //    previousItem = args.previousItem;
        //},
        filtering: false,
        sorting: false,
        inserting: true,
        editing: true,
        paging: true,
        pageLoading: false,
        autoload: true,
        invalidMessage: "Введенные данные некорректны!",
        noDataContent: "На выбранную дату данных не найдено.",
        deleteConfirm: function (item) {
            return "Запись '" + item.ChargeNote + "' будет удалена. Вы хотите продолжить?"
        },
        pageSize: 4,
        fields: [
                  { name: "ChargeId", type: "text", width: 2, align: "center", sorting: false, filtering: false, editing: true, inserting: false, title: "Id" },
                  { name: "ConvertChargesByDate", type: "text", width: 2, align: "center", sorting: false, filtering: false, editing: true, inserting: false, title: "Дата" },
                  { name: "NumberInOrder", type: "text", width: 2, align: "center", sorting: false, editing: false, filtering: false, inserting: false, title: "№ п/п" },
                  { name: "ChargeNote", type: "text", width: 55, align: "center", sorting: false, filtering: false,editing: true,
                      validate: { validator: "rangeLength", message: "Запись не может быть пустой и быть длиннее 100 символов.", param: [1, 100] },
                      title: "Оплата услуги или товара"
                  },
                  {
                      name: "ConvertedCostCharge", type: "text", width: 25, align: "center", sorting: false, filtering: false, editing: true,
                      validate: {validator: "pattern", param: "[0-9]+([\.,][0-9]+)?"//regex for float number
                      , message: "Измените введенную стоимость." + "\r\n" + "(Cтоимость не может содержать буквы и спецсимволы, кроме '.' и ',')"
                      },title: "Стоимость"
                  },
           {
               type: "control",
               modeSwitchButton: true,
               editButton: true,
               width: 3
           }
        ],
        controller:
                          {
                              loadData: function (filter) {
                                  return $.ajax({
                                      type: 'GET',
                                      url: '/Home/GetAllChargesByDate',
                                      dataType: 'JSON',
                                      success: function () {
                                      }
                                  });
                              },

                              insertItem: function (item) {
                                  return $.ajax({
                                      type: 'POST',
                                      url: '/Home/AddNewCharge',
                                      data: {
                                              ChargeNote: item.ChargeNote, ConvertedCostCharge: item.ConvertedCostCharge,
                                              ConvertChargesByDate: $('#date_for_selection_patients_notes').val()
                                      },
                                      //item.ConvertChargesByDate
                                      dataType: 'JSON',
                                      success: function (data) {
                                          if (data.ResultOfAction == true) {
                                              CountMoneyPerDayCashAndTerminal();
                                              //$("#ListOfCharges").jsGrid("render");
                                              alert("Была добавлена новая запись дополнительных расходов.");
                                          }
                                          else
                                              alert("Не удалось добавить новую запись дополнительных расходов.");

                                      },
                                      complete: function () {
                                          $("#ListOfCharges").jsGrid("render");
                                      }
                                  })
                              },

                              deleteItem: function (item) {
                                  return $.ajax({
                                      type: "POST",
                                      url: "/Home/DeleteSelectedCharge",
                                      data: item,
                                      dataType: "JSON",
                                      success: function (data) {
                                          if (data.ResultOfAction == true) {
                                              CountMoneyPerDayCashAndTerminal();
                                              //RefreshListOfCharges($('#date_for_selection_patients_notes').val());
                                              //$("#ListOfCharges").jsGrid("render");
                                              alert("Указанная запись была удалена.");
                                          }
                                          else
                                              alert("Не удалось удалить указанную запись.");
                                      },
                                      complete: function () {
                                          $("#ListOfCharges").jsGrid("render");
                                      }
                                  });
                              },

                              updateItem: function (updatingItem) {
                                  return $.ajax({
                                      type: "POST",
                                      url: "/Home/UpdateSelectedCharge",
                                      //data: { ChargeId: item.ChargeId, ChargeNote: item.ChargeNote, ConvertedCostCharge: item.ConvertedCostCharge },
                                      data: updatingItem,
                                      dataType: "JSON",
                                      success: function (data) {
                                          if (data.ResultOfAction == true) {
                                              CountMoneyPerDayCashAndTerminal();
                                              alert("Изменения успешно внесены в БД.");
                                          }
                                          else
                                              alert("Не удалось внести изменения в БД.");
                                      },
                                      complete: function () {
                                          $("#ListOfCharges").jsGrid("render");
                                      }
                                  });
                              },
                          },

                              

    });
    //Hide all unused tags
    $("#ListOfCharges").jsGrid("fieldOption", "ChargeId", "visible", false);
    $("#ListOfCharges").jsGrid("fieldOption", "ConvertChargesByDate", "visible", false);

    //CHANGE TEXT wHEN SIZE IS TOO SMALL 
    $(window).resize(function () {
        if ($(window).width() <= 986) {
            $('#generalsum').text("Сумма:");
            $('#terminalsum').text("Терминал:");
            $('#additionalexpenses').text("Расходы:");
            $('#cashsum').text("Остаток нал.:");           
        }
        else if ($(window).width() > 986) {
            $('#generalsum').text("Общая сумма за день:");
            $('#terminalsum').text("Сумма прошедшая через терминал:");
            $('#additionalexpenses').text("Дополнительные расходы:");
            $('#cashsum').text("Остаток по наличке:");
        }

    });
});

    //Refresh List of Charges
    function RefreshListOfCharges(item) {
        $.ajax({
            type: "Get",
            url: "Home/RefreshListOfCharges",
            data: { ConvertChargesByDate: item },
            dataType: "JSON",
            success: function () {
                $("#ListOfCharges").jsGrid("loadData");
            },            
            complete: function () {
                $("#ListOfCharges").jsGrid("render");
                //Change text when size is small
                var widthofwindow = window.innerWidth;
                if (widthofwindow < 986) {
                    $('#generalsum').text("Сумма:");
                    $('#terminalsum').text("Терминал:");
                    $('#additionalexpenses').text("Расходы:");
                    $('#cashsum').text("Остаток нал.:");
                }
              
            }

        });
    }

    function CountMoneyPerDayCashAndTerminal() {
        $.ajax({
            type: 'POST',
            url: '/Home/CountMoneyPerDayCashAndTerminal',
            dataType: 'JSON',
            success: function (data) {
                $('#AdditionalExpenses').val(data.DifferentCharges);
                var CashInFact = data.CashSumByDate - data.DifferentCharges;
                $('#CashSum').val(CashInFact);
                $('#TerminalSum').val(data.TerminalSumByDate);                
                var totalamount = data.CashSumByDate + data.TerminalSumByDate;
                $('#GeneralSum').val(totalamount);
            }
        })
    };