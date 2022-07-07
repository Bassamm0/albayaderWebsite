$(document).ready(function () {



    const jtoken = $('#utoken').val();
    $('#startDate').datetimepicker({
        format: 'DD-MM-yyyy'
    });
    $('#endDate').datetimepicker({
        format: 'DD-MM-yyyy'
    });


    const APIURL = $('#APIURI').val();

    $('.select2').select2();




    var table = $("#DrasftTbl").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false, "ordering": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print"]
    }).buttons().container().appendTo('#DrasftTbl_wrapper .col-md-6:eq(0)');

    $(document.body).on("change", "#ddBranch", function (e) {
        var selectedText = $(this).find("option:selected").text();
        val = $(this).val();
        filterColumn(selectedText, val, 1);
    });
    $(document.body).on("change", "#ddServiceType", function (e) {
        var selectedText = $(this).find("option:selected").text();
        val = $(this).val();
        filterColumn(selectedText, val, 2);
    });
    $(document.body).on("change", "#ddVistType", function (e) {
        var selectedText = $(this).find("option:selected").text();
        val = $(this).val();
        filterColumn(selectedText, val, 4);
    });

    function filterColumn(text, val, column) {
   
        if (val != '') {
            $('#DrasftTbl')
                .DataTable()
                .column(column)
                .search(
                    text,
                )
                .draw();
        } else {
            $('#DrasftTbl')
                .DataTable()
                .column(column)
                .search(
                    '',
                )
                .draw();
        }

    }

    intTable()
    function intTable() {

        var url = APIURL + 'service/completedservice'
        $.ajax({
            type: "GET",
            url: url,
            contentType: "application/json; charset=utf-8",
            data: {},
            async: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, status, xhr) {   // success callback function

              
                $("#DrasftTbl").DataTable().clear().draw();
                for (i = 0; i < data.length; i++) {
                    if (data[i].serviceTypeId == 1) {
                        var c1 = '<td><a href="PreventiveView?ServiceId="' + data[i].serviceId + '">#' + data[i].serviceId + '<td>';
                    } else {
                        var c1 = '<td><a href="correctiveView?ServiceId="' + data[i].serviceId + '">#' + data[i].serviceId + '<td>';
                    }
                    let dataload = "";
                    dataload += '<td><div class="dropdown"><td>';
                    dataload += '<div class="dropdown"><button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">Action';
                    dataload += '</button><ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">';
                    dataload += '<li class="adddCommentlnk" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-Add">Add Comment</a></li>';
                    dataload += '<li class="viewComment" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-view">View Comments</a></li>';
                    dataload += '<li>';
                    if (data[i].serviceTypeId == 1) {
                        dataload += '<a class="dropdown-item" href="PreventiveView?ServiceId="' + data[i].serviceId + '">View Details</a>';
                    } else {
                        dataload += '<a class="dropdown-item" href="correctiveView?ServiceId="' + data[i].serviceId + '">View Details</a>';
                    }
                    dataload += '</li>';

                    $("#DrasftTbl").DataTable().row.add([
                        c1,
                        data[i].branchName,
                        data[i].serviceTypeName,
                        data[i].createdDate,
                        data[i].vistTypeName,
                        data[i].technicianName,
                        data[i].completionDate,
                        data[i].remark,
                        data[i].supervisourFeedback,
                        dataload
                    ]).draw();

                }

            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {

            console.log('done')
            $("#startDate").val('');
            $("#endDate").val('')

        });
    }

    $('body').on('click', '#DateSearch', function () {
        if (!$("#FilterDate").valid()) {

            return;


        }
       
        var _startDate = moment(moment($("#startDate").val(), 'DD-MM-YYYY')).format('MM-DD-YYYY');
        var _endDate = moment(moment($("#endDate").val(), 'DD-MM-YYYY')).format('MM-DD-YYYY');

        var senddata = '{"startDate":"' + _startDate + '","endDate":"' + _endDate + '"} '
        
        var url = APIURL + 'service/completedservicedate'
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            data: senddata,
            async: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, status, xhr) {   // success callback function
                
                console.log(data)
                $("#DrasftTbl").DataTable().clear().draw();
                for (i = 0; i < data.length; i++) {
                    if (data[i].serviceTypeId == 1) {
                        var c1 = '<td><a href="PreventiveView?ServiceId="' + data[i].serviceId + '">#' + data[i].serviceId + '<td>';
                    } else {
                       var c1 = '<td><a href="correctiveView?ServiceId="' + data[i].serviceId + '">#' + data[i].serviceId + '<td>';
                    }
                    let dataload = "";
                    dataload += '<td><div class="dropdown"><td>';
                    dataload += '<div class="dropdown"><button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">Action';
                    dataload += '</button><ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">';
                    dataload += '<li class="adddCommentlnk" serviceid="@item.ServiceId"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-Add">Add Comment</a></li>';
                    dataload += '<li class="viewComment" serviceid="@item.ServiceId"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-view">View Comments</a></li>';
                    dataload += '<li>';
                    if (data[i].serviceTypeId == 1) {
                        dataload += '<a class="dropdown-item" href="PreventiveView?ServiceId="' + data[i].serviceId + '">View Details</a>';
                    } else {
                        dataload += '<a class="dropdown-item" href="correctiveView?ServiceId="' + data[i].serviceId + '">View Details</a>';
                    }
                    dataload += '</li>';

                    $("#DrasftTbl").DataTable().row.add([
                       c1,
                        data[i].branchName,
                        data[i].serviceTypeName,
                        data[i].createdDate,
                        data[i].vistTypeName,
                        data[i].technicianName,
                        data[i].completionDate,
                        data[i].remark,
                        data[i].supervisourFeedback,
                        dataload
                    ]).draw();

                }


                
            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {

            console.log('done')

        });

    })

    



    $('body').on('click', '#DateReset', function () {
        intTable()
      
    })


    getBranches();

    function getBranches() {

        $("#ddBranch").html('')
        $.ajax({
            type: "GET",
            url: APIURL +"branch/all",
            contentType: "application/json; charset=utf-8",
            /* async: false,*/
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
               
                $('#ddBranch').append('<option value="">All Branch  ...</option>')
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


})