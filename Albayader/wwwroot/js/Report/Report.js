$(document).ready(function () {

    const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;

    const arole = $('#Arole').val();

    function toTimeZone(DateTime) {
        var format = 'DD-MM-YYYY hh:mm:ss a';
        return moment.utc(DateTime, format).tz(timezone).format(format);
    }

    const jtoken = $('#utoken').val();
    $('#startDate').datetimepicker({
        format: 'DD-MM-yyyy'
    });
    $('#endDate').datetimepicker({
        format: 'DD-MM-yyyy'
    });

    $('#newDateChange').datetimepicker({
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
        filterColumn(selectedText, val, 3);
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

                console.log(data)
                


                $("#DrasftTbl").DataTable().clear().draw();
                for (i = 0; i < data.length; i++) {

                    var serials = '';
                    if (data[i].serviceTypeId == 1) {

                        serials = '';
                        if (data[i].serviceDetails != null && data[i].serviceDetails.length > 0) {
                            for (var j = 0; j < data[i].serviceDetails.length; j++) {
                                if (data[i].serviceDetails[j].serialNo != '') {
                                    if (j != data[i].serviceDetails.length - 1) {
                                        serials += data[i].serviceDetails[j].serialNo + ','
                                    } else {
                                        serials += data[i].serviceDetails[j].serialNo;
                                    }
                                }
                               
                            }
                        }




                    } else if (data[i].serviceTypeId == 2) {
                        if (data[i].correctiveServiceDetails != null && data[i].correctiveServiceDetails.length > 0) {

                            serials = data[i].correctiveServiceDetails[0].serialNo;
                        }
                    }


                    if (data[i].serviceTypeId == 1) {
                        var c1 = '<td><a href="PreventiveView?ServiceId=' + data[i].serviceId + '">#' + data[i].serviceId + '<td>';
                    } else {
                        var c1 = '<td><a href="correctiveView?ServiceId=' + data[i].serviceId + '">#' + data[i].serviceId + '<td>';
                    }
                    let dataload = "";
                    dataload += '<td><div class="dropdown"><td>';
                    dataload += '<div class="dropdown"><button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">Action';
                    dataload += '</button><ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">';
                    dataload += '<li class="adddCommentlnk" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-Add">Add Comment</a></li>';
                    dataload += '<li class="viewComment" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-view">View Comments</a></li>';
                    dataload += '<li>';
                    if (data[i].serviceTypeId == 1) {
                        dataload += '<a class="dropdown-item" href="PreventiveView?ServiceId=' + data[i].serviceId + '">View Details</a>';
                    } else {
                        dataload += '<a class="dropdown-item" href="correctiveView?ServiceId=' + data[i].serviceId + '">View Details</a>';
                    }
                    dataload += '</li>';
                    if (arole == "Administrator") {
                        dataload += '<li class="changeDatecss" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-ChangeDate">Change Date</a></li>';

                        if (data[i].serviceTypeId == 1) {
                            dataload += '<li class="editDatecss" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="editpreventive?ServiceId=' + data[i].serviceId + '" >Edit</a></li>';
                        } else {
                            dataload += '<li class="editDatecss" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="editcorrective?ServiceId=' + data[i].serviceId + '" >Edit</a></li>';

                        }

                    }
                    dataload += '<li class="deleteBtn" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item text-danger" href="#"  data-toggle="modal" data-target="#modal-delete">Delete</a></li>';

                    var remark = '<span class="Remak">' + data[i].remark + '</span>'
                    $("#DrasftTbl").DataTable().row.add([
                        c1,
                        data[i].branchName,
                        data[i].serviceTypeName,
                     
                        data[i].vistTypeName,
                       // serials,
                        toTimeZone(moment(moment(data[i].completionDate, 'MM/DD/YYYY hh:mm:ss')).format('DD-MM-YYYY hh:mm:ss a')),
                        remark,
                   
                        dataload
                        

                    ]).draw();

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

               
                console.log(data);
                $("#DrasftTbl").DataTable().clear().draw();
                for (i = 0; i < data.length; i++) {

                    var serials = '';
                    if (data[i].serviceTypeId == 1) {

                        serials = '';
                        if (data[i].serviceDetails != null && data[i].serviceDetails.length > 0) {
                            for (var j = 0; j < data[i].serviceDetails.length; j++) {
                                if (data[i].serviceDetails[j].serialNo != '') {
                                    if (j != data[i].serviceDetails.length - 1) {
                                        serials += data[i].serviceDetails[j].serialNo + ','
                                    } else {
                                        serials += data[i].serviceDetails[j].serialNo;
                                    }
                                }

                            }
                        }
                      



                    } else if (data[i].serviceTypeId == 2) {
                        if (data[i].correctiveServiceDetails != null && data[i].correctiveServiceDetails.length > 0) {
                            serials = data[i].correctiveServiceDetails[0].serialNo;
                        }
                    }


                    if (data[i].serviceTypeId == 1) {
                        var c1 = '<td><a href="PreventiveView?ServiceId=' + data[i].serviceId + '">#' + data[i].serviceId + '<td>';
                    } else {
                        var c1 = '<td><a href="correctiveView?ServiceId=' + data[i].serviceId + '">#' + data[i].serviceId + '<td>';
                    }
                    let dataload = "";
                    dataload += '<td><div class="dropdown"><td>';
                    dataload += '<div class="dropdown"><button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">Action';
                    dataload += '</button><ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">';
                    dataload += '<li class="adddCommentlnk" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-Add">Add Comment</a></li>';
                    dataload += '<li class="viewComment" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-view">View Comments</a></li>';
                    dataload += '<li>';
                    if (data[i].serviceTypeId == 1) {
                        dataload += '<a class="dropdown-item" href="PreventiveView?ServiceId=' + data[i].serviceId + '">View Details</a>';
                    } else {
                        dataload += '<a class="dropdown-item" href="correctiveView?ServiceId=' + data[i].serviceId + '">View Details</a>';
                    }
                    dataload += '</li>';

                    if (arole == "Administrator") {
                        dataload += '<li class="changeDatecss" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-ChangeDate">Change Date</a></li>';

                        if (data[i].serviceTypeId == 1) {
                            dataload += '<li class="editDatecss" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="editpreventive?ServiceId=' + data[i].serviceId + '" >Edit</a></li>';
                        } else {
                            dataload += '<li class="editDatecss" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item " href="editcorrective?ServiceId=' + data[i].serviceId + '" >Edit</a></li>';

                        }

                    }
                    dataload += '<li class="deleteBtn" serviceid="' + data[i].serviceId + '"><a  class="dropdown-item text-danger" href="#"  data-toggle="modal" data-target="#modal-delete">Delete</a></li>';

                    var remark = '<span class="Remak">' + data[i].remark + '</span>'
                    $("#DrasftTbl").DataTable().row.add([
                        c1,
                        data[i].branchName,
                        data[i].serviceTypeName,

                        data[i].vistTypeName,
                       // serials,
                        toTimeZone(moment(moment(data[i].completionDate, 'MM/DD/YYYY hh:mm:ss')).format('DD-MM-YYYY hh:mm:ss a')),
                        remark,

                        dataload


                    ]).draw();

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

    })





    $('body').on('click', '#DateReset', function () {
        intTable()

    })


    getBranches();

    function getBranches() {

        $("#ddBranch").html('')
        $.ajax({
            type: "GET",
            url: APIURL + "branch/all",
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


    $('body').on('click', '.changeDatecss', function () {
        
        changeServiceId = $(this).attr('serviceid');

        $('#serviceDateId').val(changeServiceId)
    });


    $('body').on('click', '#chnageDate', function () {
       
        changeDate()

    });
    function changeDate() {
        var serviceId = $('#serviceDateId').val();
        var newDate = moment(moment($("#newDateChange").val(), 'DD-MM-YYYY')).format('MM-DD-YYYY')

        if (newDate == '') {
            alert('Plese select Date');
            return;
        }
        var url = 'Reports?handler=changeDate&serviceId=' + serviceId + '&newDate=' + newDate;

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
                $('#closedate').click();
            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {

            $('#closedate').click();
            intTable();
            $("#newDateChange").val('')
            toastr["success"]("Date changed successfuly.")
        });
    }


    var obj;
    $('body').on('click', '.deleteBtn', function () {

        var ServiceId = $(this).attr('ServiceId')
        $('#ServiceToDeleteName').html('#'+ServiceId)
      
        $('#deletedServiceId').val(ServiceId);
        //User?id=1&handler=DeleteUser
        obj = $(this).parents('ul').parents('div').parents('td').parents('tr')


    })
    $('body').on('click', '#DeleteService', function () {

        var ServiceId = $("#deletedServiceId").val()
        var url = 'reports?handler=DeleteService&id=' + $("#deletedServiceId").val();

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

            $('#closeDelete').click();

            obj.remove()
            table.clear().draw();
            toastr["success"]("User delete successfuly.")
        });



    })

   
})