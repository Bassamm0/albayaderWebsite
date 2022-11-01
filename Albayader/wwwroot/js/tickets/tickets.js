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


    
    

    $('body').on('click', '#DateSearch', function () {
       
        if ($("#FilterDate").valid()) {
            $('#actionType').val('filter')
            $('#FilterDate').submit();        
        }


    })
    $('body').on('click', '#DateReset', function () {

       
        $('#startDate').val('');
        $('#endDate').val('');

        $("#FilterDate").validate().cancelSubmit = true;
        $('#actionType').val('reset')
            $('#FilterDate').submit();
  
   


    })

    $('#FilterDate').validate({

        rules: {
            endDate: { greaterThan: "#startDate" }

        },

        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
    //changeStatus
    var obj;
    var objbage
    $('body').on('click', '.changeStatus', function () {
        
        var ticketId = $(this).attr('ticketid')
        $('.statusTicketId').html(ticketId)
     
        $('#changeStatusServiceId').val(ticketId);
        obj = $(this).parents('div').parents('div').parents('td').parents('tr');
        objbage = $(this).parents('div').parents('div').parents('td').prev('td').children('span');

    })

    

    $('body').on('click', '#doChangeStatus', function () {

        changthestatus($("#ddStatus").val());


    })

    function changthestatus(statusId) {

        statusid = $("#ddStatus").val();
        var url = 'tickets?handler=ChangeStatus&id=' + $("#changeStatusServiceId").val() + '&statusId=' + statusId;
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
            if (statusId != '7') {
                $('#changestatusclose').click();
                newcss = statusCase(statusid)
                objbage.attr('class', newcss)
                objbage.html($("#ddStatus option:selected").text())
                 
                toastr["success"]("Status changed successfuly.")
            } else {
                obj.remove()
                // $("#DrasftTbl").DataTable().clear().draw();
                table.clear().draw();
                toastr["success"]("The ticket closed and moved to closed ticket successfuly.")
            }
          

        });


    }

   var objAssgin;
    $('body').on('click', '.assginUser', function () {

        var ticketId = $(this).attr('ticketid')
        $('#assginTicketId').html(ticketId)

        $('#assginServiceId').val(ticketId);
        obj = $(this).parents('div').parents('div').parents('td').parents('tr');
        objAssgin = $(this).parents('div').parents('div').parents('td').prev('td').prev('td');

    })

    $('body').on('click', '#doAssignUser', function () {

        if (!$("#assignForm").valid()) {
            return false;
        }
        statusid = $("#ddStatus").val();
        var url = 'tickets?handler=Assign&id=' + $("#assginServiceId").val() + '&userId=' + $("#ddAssgin").val();
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

            $('#assignClose').click();

            objAssgin.html($("#ddAssgin option:selected").text())
            toastr["success"]("ticket assigned  successfuly.")

        });

    });

    $('body').on('click', '.closeTicket', function () {

        var ticketId = $(this).attr('ticketid')
        $("#changeStatusServiceId").val(ticketId)

        obj = $(this).parents('div').parents('div').parents('td').parents('tr');
       
        changthestatus('7')

    })




    $('#changeStatus').validate({
        rules: {
            ddStatus: {
                required: true,
            },


        },

        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });

    $('#assignForm').validate({
        rules: {
            ddAssgin: {
                required: true,
            },


        },

        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });

    $('#creatServiceForm').validate({
        rules: {
            ddBranch: {
                required: true,
            },


        },

        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });

    function statusCase(id) {
        var css;
        switch (id) {
            case '1':

                css = "badge badge-purple"
                break;
            case '2':
                css = "badge badge-Lightblue"
                break;
            case '3':
                css = "badge badge-warning"

                break;
            case '4':
                css = "badge badge-dark"

                break;
            case '5':
                css = "badge badge-danger"

                break;
            case '6':
                css = "badge badge-orang"


                break;

            default:
            // code block
        }
        return css;
    }

    $('body').on('click', '.createService', function () {

        var ticketId = $(this).attr('ticketid')
        var companyid = $(this).attr('companyid')
        $('#assginTicketId').html(ticketId)
        $('#createServiceId').val(ticketId)
       

        getCompanyBranch(companyid)

    })


    function getCompanyBranch(_companyId) {

        $("#ddBranch").html('')
        $.ajax({
            type: "POST",
            url: APIURL + "branch/companybranchs",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ 'companyid': parseInt(_companyId) }),
            async: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                console.log(arrUpdates)
                $('#ddBranch').append('<option value="">Select  Branch  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].branchName);
                    val = arrUpdates[i].branchId;
                    populate(text, val, '#ddBranch');

                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#ddBranch').append('<option value="">Data Not Loaded  ...</option>')
                console.log('Error in Operation');
            }
        });

    }




    function populate(text, val, controlId) {
        $(controlId).append('<option value=' + val + '>' + text + '</option>');

    }

    $('body').on('click', '#doAssignUser', function () {

        if (!$("#creatServiceForm").valid()) {
            return false;
        }
        branchId = $("#ddBranch").val();
        var url = 'tickets?handler=CreateService&id=' + $("#createServiceId").val() + '&branchId=' + branchId;
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

            $('#createserviceClose').click();

            
            toastr["success"]("Service created  successfuly.")

        });

    });




})