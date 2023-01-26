﻿



$(document).ready(function () {

    const APIURL = $('#APIURI').val();

    $('#export').click(function () {
     
        exportexcel()
    })

    function exportexcel() {
      
     const jtoken = $('#utoken').val();

        var sType = "All Type";
        var visitType = "All Site Vist";
        var branch = "All Branch";
        var searchValue = "";
       
        var url = APIURL+'service/exportexcel';
       // var url = '/test?handler=Downloadfile&sType='+sType+'&visitType='+visitType+'&branch='+branch+'&searchValue=' +searchValue;

        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            // dataType: "json",
            data: {},
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
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