$(document).ready(function () {

    const APIURL = $('#APIURI').val();
    const jtoken = $('#utoken').val();
    const uploadurl = $('#UPLOADURL').val();
    var obj;

    var table = $("#DrasftTbl").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print"]
    }).buttons().container().appendTo('#DrasftTbl_wrapper .col-md-6:eq(0)');



    $('body').on('click', '.deleteEvent', function () {

        var EventName = $(this).attr('EventName')
        $('#EventToDeleteName').html(EventName)
        var EventId = $(this).attr('EventId')
        $('#deletedEventId').val(EventId);

        obj = $(this).parents('ul').parents('div').parents('td').parents('tr')



    })
    $('body').on('click', '#DeleteEvent', function () {


        var url = 'event?handler=DeleteEvent&id=' + $("#deletedEventId").val();

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
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {

            $('#delClose').click();
            // datatable redraw
            obj.remove()
            table.clear().draw();
            toastr["success"]("event delete successfuly.")

        });



    })


    $('body').on('click', '.ViewEvent', function () {
        EventId = $(this).attr('EventId')

        getEvent(EventId);
    })

    function getEvent(EventId) {
        console.log(EventId)
        let html = '';
        const uri = APIURL + "calendarevent/geteventById";

        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8',
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            body: JSON.stringify({ 'id': parseInt(EventId) })
        })
            .then(response => response.json())
            .then((data) => {
                // trigger model
            

                var techname = '', branchName = '';
                if (data.eventTypeId == 1 || data.eventTypeId == 2) {
                   techname=  ` <li class="list-group-item"><span class='ViewDetailsTit'>Technicain:</span> ${data.technicainName}</li>`
                    branchName = ` <li class="list-group-item"><span class='ViewDetailsTit'>Branch:</span> ${data.branchName}</li>`

                }
                html = ` <li class="list-group-item"><span class='ViewDetailsTit'>Title:</span> ${data.title}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Start Date:</span> ${data.eventStartDate}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>End Date:</span> ${data.eventEndDate}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Type:</span> ${data.eventType}</li>`
                    + techname
                    + branchName
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Description:</span> ${data.description}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>url:</span><a style="word-break: break-word;" target="_blank" href="${data.url}"> ${data.url}</a></li>`

                $('#EventDetails').html(html)
            })
            .catch(error => console.error('Unable to add item.', error));
    }



});