
$(document).ready(function () {

    const APIURL = $('#APIURI').val();
    const UploadUrl = $('#Uploadlocation').val();
    var jtoken = $('#utoken').val();
    var eventObj = []
    getevents()

    function getevents() {


   
        $.ajax({
            type: "GET",
            url: APIURL + "calendarevent/display",
            contentType: "application/json; charset=utf-8",
            data: {},
          
            crossDomain: true,

            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
                , Authorization: 'Bearer ' + jtoken,

            },
            success: function (data, textStatus, xhr) {
                
                 
               console.log(data);
                createEventObj(data)

            },

            error: function (xhr, textStatus, errorThrown) {

                console.log('Error in Operation');

                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
            },
            complete: function (xhr, textStatus) {
                console.log(xhr.status);
            }
        });
    }


    function createEventObj(data) {
       
        for (var i = 0; i < data.length; i++) {

           
            var startinitdate = moment(data[i].eventStartDate, 'DD-MM-yyyy HH:mm').format('yyyy-MM-DD HH:mm');



            console.log('id:' + data[i].eventId + 'Date:' + startinitdate)


            var strTime = ("0" + new Date(data[i].eventStartDate).getHours()).slice(-2) + ':' + ("0" + new Date(data[i].eventStartDate).getMinutes()).slice(-2);
            var endTime = ("0" + new Date(data[i].eventEndDate).getHours()).slice(-2) + ':' + ("0" + new Date(data[i].eventEndDate).getMinutes()).slice(-2);
         

           
            var newEvent = new Object();
           
           
            newEvent.id = data[i].eventId;
            newEvent.title = data[i].title;
            
            newEvent.start = moment(data[i].eventStartDate, 'DD-MM-yyyy HH:mm').format('yyyy-MM-DD HH:mm');
            newEvent.end = moment(data[i].eventEndDate, 'DD-MM-yyyy HH:mm').format('yyyy-MM-DD HH:mm');
           // console.log(newEvent.start);

            //if (data[i].eventTypeId != 1 || data[i].eventTypeId != 2) {
            //    if (data[i].url != null && data[i].url != '') {
            //        newEvent.url = data[i].url
            //    }
            //}
          
            newEvent.allDay = true;
            if (data[i].statusId != 5) {
                if (data[i].eventTypeId == 1) {
                    newEvent.backgroundColor = '#17a2b8';
                    newEvent.borderColor = '#0aa4bd';
                } else if (data[i].eventTypeId == 2) {
                    newEvent.backgroundColor = '#c489fc';
                    newEvent.borderColor = '#934fd3';

                } else if (data[i].eventTypeId == 3) {
                    newEvent.backgroundColor = '#dc3545';
                    newEvent.borderColor = '#b11a65';

                } else if (data[i].eventTypeId == 4) {
                    newEvent.backgroundColor = '#fd7e14';
                    newEvent.borderColor = '#d76303';
                } else {
                    newEvent.backgroundColor = '#00695c';
                    newEvent.borderColor = '#00695c';
                }

            } else {
                newEvent.backgroundColor = '#007E33';
                newEvent.borderColor = '#007E33';
            }
           

            eventObj.push(newEvent)

        }

       
        ini_events($('#external-events div.external-event'));
        var calendar = new Calendar(calendarEl, {
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'
            },
            themeSystem: 'bootstrap',
            //Random default events


            events: eventObj,
            editable: false,
            droppable: false, // this allows things to be dropped onto the calendar !!!
            eventClick: function (info) {
               
               // if (info.event.url == 'null' || info.event.url == '') {

                    $('#modal-view').modal('show');
                    getEvent(info.event.id);
                    return;
              //  }
                
              
            }
        });

        calendar.render();
    }

    /* initialize the external events
    -----------------------------------------------------------------*/
    function ini_events(ele) {
        ele.each(function () {

            // create an Event Object (https://fullcalendar.io/docs/event-object)
            // it doesn't need to have a start or end
            var eventObject = {
                title: $.trim($(this).text()) // use the element's text as the event title
            }

            // store the Event Object in the DOM element so we can get to it later
            $(this).data('eventObject', eventObject)

          

        })
    }

  

    /* initialize the calendar
     -----------------------------------------------------------------*/
    //Date for the calendar events (dummy data)
    var date = new Date()
    var d = date.getDate(),
        m = date.getMonth(),
        y = date.getFullYear()

    var Calendar = FullCalendar.Calendar;

    var calendarEl = document.getElementById('calendar');

    // initialize the external events
    // -----------------------------------------------------------------

   

       // var date = moment('07-02-2022','mm-d-yyyy').format('D-mm-yyyy')
        //console.log(date)

   
    // $('#calendar').fullCalendar()

    /* ADDING EVENTS */
    var currColor = '#3c8dbc' //Red by default
    // Color chooser button
    $('#color-chooser > li > a').click(function (e) {
        e.preventDefault()
        // Save color
        currColor = $(this).css('color')
        // Add color effect to button
        $('#add-new-event').css({
            'background-color': currColor,
            'border-color': currColor
        })
    })


   

    function getEvent(EventId) {
 
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
                if (data.technicainName == null) {
                    data.technicainName = "";
                }
                if (data.eventTypeId == 1 || data.eventTypeId == 2) {
                    techname = ` <li class="list-group-item"><span class='ViewDetailsTit'>Technician:</span> ${data.technicainName}</li>`
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

                $('#EventDetails').html(html);

            })
            .catch(error => console.error('Unable to add item.', error));
    }



})
