



$(document).ready(function () {

    $('#startDate').datetimepicker({
        format: 'DD-MM-yyyy'
    });
    $('#endDate').datetimepicker({
        format: 'DD-MM-yyyy'
    });
    $('#export').click(function () {



        exportexcel()
    })

    function exportexcel() {


        var sType = $('#ddServiceType').find("option:selected").text();
        var visitType = $('#ddVistType').find("option:selected").text();
        var branch = $('#ddBranch').find("option:selected").text();
        var searchValue = $("input[type='search']").val();

        var _startDate ='';
        var _endDate = '';
        if ($("#startDate").val() !== '' && $("#endDate").val() !== '') {
             _startDate = moment(moment($("#startDate").val(), 'DD-MM-YYYY')).format('MM-DD-YYYY');
             _endDate = moment(moment($("#endDate").val(), 'DD-MM-YYYY')).format('MM-DD-YYYY');
        } 
        


        var url = 'Reports?handler=Downloadfile&sType=' + sType + '&visitType=' + visitType + '&branch=' + branch + '&searchValue=' + searchValue + '&startDate=' + _startDate + '&endDate=' + _endDate;

        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            // dataType: "json",
            data: {},
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (data, status, xhr) {   // success callback function
                var blob = new Blob([data], { type: "application/octetstream" });

                //Check the Browser type and download the File.
                var isIE = false || !!document.documentMode;
                if (isIE) {
                    window.navigator.msSaveBlob(blob, "report.xls");
                } else {
                    var url = window.URL || window.webkitURL;
                    link = url.createObjectURL(blob);
                    var a = $("<a />");
                    a.attr("download", "report.xls");
                    a.attr("href", link);
                    $("body").append(a);
                    a[0].click();
                    $("body").remove(a);
                }


            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                if (jqXhr.status == 401) {
                    window.location.href = 'Index';
                    alert(' Your login session expired, Please login again.');
                    return;
                } else {
                    alert('Error: something went wronge please try again later');

                }
            }

        }).done(function () {

            console.log('done')

        });
    }

})