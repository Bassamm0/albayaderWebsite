$(document).ready(function () {


    const connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7174/API/ticketHub")
        .build();

    connection.on("newTicketAdded", (ticket) => {
    
        const encodedMsg = ticket ;
        toastr["success"]("New ticket has been add  " + encodedMsg)
        console.log(ticket)
        getTickets();
    });

    connection.start().catch(err => console.error(err.toString()));

    //Send the message




    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "15000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    const jtoken = $('#utoken').val()

    const APIURL = $('#APIURI').val();


    let dataload = "";
    let dataload1 = "";
    let dataload2 = "";
    let pageNumber=1;
    let pageCount = 0;
    var intervalId;

    getTickets()

    function getTickets() {
        dataload = "";
        dataload1 = "";
        dataload2 = "";
        pageCount = 0;
        $('#ticketsHolder').html('');
        clearInterval(intervalId)

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
                let newcss = "";
       


                console.log(arrUpdates.length)
                for (var i = 0; i < arrUpdates.length; i++) {
                    newcss = statusCase(arrUpdates[i].ticketStatusId)
                    
                    assigned = "Not Assign";
                    if (arrUpdates[i].assignedUser.trim() != '') {
                        assigned = arrUpdates[i].assignedUser;
                    }
                   
                    

                    if (i <= 5) {
                        pageCount = 1;
                        if (arrUpdates[i].ticketStatusId == 5) {
                            rowblink = '<div class="row rowblink first">';
                        } else {
                            rowblink = '<div class="row first"  >';

                        }


                        dataload += rowblink;
                        dataload += '<div class="colRef">';
                        dataload += arrUpdates[i].ticketId;
                        dataload += '</div>';
                        dataload += '<div class="colSubject">';
                        dataload += arrUpdates[i].subject;
                        dataload += '</div>';
                        dataload += '<div class="colBranch">';
                        dataload += arrUpdates[i].branchName;
                        dataload += '</div>';
                        dataload += '<div class="colBranch">';
                        dataload += assigned;
                        dataload += '</div>';
                        dataload += '<div class="colStatus">';
                        dataload += '<span class="badge bview badge-' + newcss + '">' + arrUpdates[i].statusName + '</span>';
                        dataload += '</div>';
                        dataload += '</div>';
                        dataload += '<div class="dropdown-divider-light first"></div>';

                    } else if (i > 5 && i <= 11) {
                        pageCount = 2;
                        if (arrUpdates[i].ticketStatusId == 5) {
                            rowblink = '<div class="row rowblink second" >';
                        } else {
                            rowblink = '<div class="row second"  >';

                        }

                        dataload1 += rowblink;
                        dataload1 += '<div class="colRef">';
                        dataload1 += arrUpdates[i].ticketId;
                        dataload1 += '</div>';
                        dataload1 += '<div class="colSubject">';
                        dataload1 += arrUpdates[i].subject;
                        dataload1 += '</div>';
                        dataload1 += '<div class="colBranch">';
                        dataload1 += arrUpdates[i].branchName;
                        dataload1 += '</div>';
                        dataload1 += '<div class="colBranch">';
                        dataload1 += assigned;
                        dataload1 += '</div>';
                        dataload1 += '<div class="colStatus">';
                        dataload1 += '<span class="badge bview badge-' + newcss + '">' + arrUpdates[i].statusName + '</span>';
                        dataload1 += '</div>';
                        dataload1 += '</div>';
                        dataload1 += '<div class="dropdown-divider-light second"></div>';
                    } else {
                        pageCount = 3;
                        if (arrUpdates[i].ticketStatusId == 5) {
                            rowblink = '<div class="row rowblink third" >';
                        } else {
                            rowblink = '<div class="row third"  >';

                        }

                        dataload2 += rowblink;
                        dataload2 += '<div class="colRef">';
                        dataload2 += arrUpdates[i].ticketId;
                        dataload2 += '</div>';
                        dataload2 += '<div class="colSubject">';
                        dataload2 += arrUpdates[i].subject;
                        dataload2 += '</div>';
                        dataload2 += '<div class="colBranch">';
                        dataload2 += arrUpdates[i].branchName;
                        dataload2 += '</div>';
                        dataload2 += '<div class="colBranch">';
                        dataload2 += assigned;
                        dataload2 += '</div>';
                        dataload2 += '<div class="colStatus">';
                        dataload2 += '<span class="badge bview badge-' + newcss + '">' + arrUpdates[i].statusName + '</span>';
                        dataload2 += '</div>';
                        dataload2 += '</div>';
                        dataload2 += '<div class="dropdown-divider-light third"></div>';
                    }
                   
                }

                intervalId

                $('#ticketsHolder').append(dataload);
                $('#ticketsHolder').append(dataload1);
                $('#ticketsHolder').append(dataload2);
                $('body .second').hide();
                $('body .third').hide();
                pageNumber = 1;
                startInter()
               
            },
            error: function (xhr, textStatus, errorThrown) {
              
                console.log('Error in Operation');
            }
        });

    }

    function startInter() {
         intervalId = window.setInterval(function () {

             if (pageCount == 3) {
                 if (pageNumber == 1) {
                     pageNumber = 2;

                     $('body .first').fadeOut(function () {
                         $('body .second').fadeIn(function () {
                             $('body .third').fadeOut(function () {
                                 //do your stuff
                             });
                         });
                     });

                 } else if (pageNumber == 2) {
                     pageNumber = 3;
                     $('body .second').fadeOut(function () {
                         $('body .third').fadeIn(function () {
                             $('body .first').fadeOut(function () {
                                 //do your stuff
                             });
                         });
                     });

                 } else {
                     pageNumber = 1;
                     $('body .third').fadeOut(function () {
                         $('body .first').fadeIn(function () {
                             $('body .second').fadeOut(function () {
                                 //do your stuff
                             });
                         });
                     });
                 }


             } else if (pageCount == 2) {
                 if (pageNumber == 1) {
                     pageNumber = 2;

                     $('body .first').fadeOut(function () {
                         $('body .second').fadeIn(function () {

                         });
                     });

                 } else {
                     pageNumber = 1;
                     $('body .second').fadeOut(function () {
                         $('body .first').fadeIn(function () {

                         });
                     });
                 }
             }
           

        }, 5000);

    }
    
   


    function statusCase(id) {
        
        var css;
        switch (id) {
            case 1:

                css = "badge badge-purple"
                break;
            case 2:
                css = "badge badge-Lightblue"
                break;
            case 3:
                css = "badge badge-warning"

                break;
            case 4:
                css = "badge badge-dark"

                break;
            case 5:
                css = "badge badge-danger rowblink"

                break;
            case 6:
                css = "badge badge-orang"


                break;

            default:
            // code block
        }
       
        return css;
    }

});