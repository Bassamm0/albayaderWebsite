$(document).ready(function () {

    const arole = $('#Arole').val();


    const jtoken = $('#utoken').val();
    $('#startDate').datetimepicker({
        format: 'DD-MM-yyyy HH:mm',
        icon: { time: 'far fa-clock' }
    });
    $('#endDate').datetimepicker({
        format: 'DD-MM-yyyy HH:mm',
        icon: { time: 'far fa-clock' }
    });

   
    const APIURL = $('#APIURI').val();

    $('.select2').select2();

    var table = $("#DrasftTbl").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false, "ordering": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print"]
    }).buttons().container().appendTo('#DrasftTbl_wrapper .col-md-6:eq(0)');


    
    


    function changthestatus(statusId,ticketId) {

        statusid = $("#ddStatus").val();
        var url = 'tickets?handler=ChangeStatus&id=' + ticketId + '&statusId=' + statusId;
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
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {
         
               
               
                toastr["success"]("The ticket opened and moved to open ticket successfuly.")
                window.location.href = 'tickets';
        
          

        });


    }


    $('body').on('click', '.openTicket', function () {

        var ticketId = $(this).attr('ticketid')
   
       
        changthestatus('2',ticketId)

    })



})