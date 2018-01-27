var currentday;
var d = new Date();

var month = d.getMonth() + 1;
var day = d.getDate();
var output = (day < 10 ? '0' : '') + day + '/' +
    (month < 10 ? '0' : '') + month + '/' +
    d.getFullYear();
currentday = output;

$(document).ready(function () {
    
    $("#datetimepicker1 input").val(output);
   
    
    $(function () {
        //Идентификатор элемента HTML (например: #datetimepicker1), для которого необходимо инициализировать виджет "Bootstrap datetimepicker"
        $('#datetimepicker1').datetimepicker(
               {
                   language: 'ru',
                   pickTime: false,
                   daysOfWeekDisabled: [0],
                                    
               }
            );
    });


});
