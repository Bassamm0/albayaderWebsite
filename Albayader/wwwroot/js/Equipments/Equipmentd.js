$(document).ready(function () {

    const APIURL = $('#APIURI').val();
    const jtoken = $('#utoken').val();
    const uploadurl = $('#UPLOADURL').val();
    var obj;

    var table = $("#DrasftTbl").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print"]
    }).buttons().container().appendTo('#DrasftTbl_wrapper .col-md-6:eq(0)');



    $('body').on('click', '.deleteequipment', function () {

        var equipmentName = $(this).attr('equipmentName')
        $('#equipmentToDeleteName').html(equipmentName)
        var equipmentid = $(this).attr('equipmentId')
        $('#deletedequipmentId').val(equipmentid);
       
        obj = $(this).parents('ul').parents('div').parents('td').parents('tr')
       
        

    })
    $('body').on('click', '#Deleteequipment', function () {

        
        var url = 'equipments?handler=Deleteequipment&id=' + $("#deletedequipmentId").val();

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

            $('#delClose').click();
            // datatable redraw
            obj.remove()
            table.clear().draw();
            toastr["success"]("equipment delete successfuly.")
        });



    })

});