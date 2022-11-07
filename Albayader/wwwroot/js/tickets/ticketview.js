$(document).ready(function () {


    const jtoken = $('#utoken').val()

    const APIURL = $('#APIURI').val();
    getTickets()

    function getTickets() {

   
        $.ajax({
            type: "GET",
            url: APIURL + "tickets/openforview",
            contentType: "application/json; charset=utf-8",
            data: {},
            async: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                console.log(arrUpdates)
               
            },
            error: function (xhr, textStatus, errorThrown) {
              
                console.log('Error in Operation');
            }
        });

    }

});