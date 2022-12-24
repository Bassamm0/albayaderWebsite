$(document).ready(function () {

    const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;

    const arole = $('#Arole').val();
    var datefilterval = false;
    var getUrlParameter = function getUrlParameter(sParam) {
        var sPageURL = window.location.search.substring(1),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
            }
        }
        return false;
    };


    var barnchId = getUrlParameter('br');




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



    $('body').on('click', '#DateSearch', function () {
        if (!$("#FilterDate").valid()) {
            return;
        }
        datefilterval = true
        loadDataTable(datefilterval);
    })


    $('body').on('click', '#DateReset', function () {
        $("#startDate").val('');
        $("#endDate").val('')
        datefilterval = false;
        loadDataTable(false);
    })

    loadDataTable(datefilterval);

    function loadDataTable(datefelter) {
        $("#DrasftTbl").DataTable().clear().destroy()
        var _startDate = moment(moment($("#startDate").val(), 'DD-MM-YYYY')).format('MM-DD-YYYY');
        var _endDate = moment(moment($("#endDate").val(), 'DD-MM-YYYY')).format('MM-DD-YYYY');

        if (datefelter) {
            var fullUrl = APIURL + 'service/servicereportdatefilter'

        } else {
            var fullUrl = APIURL + 'service/servicereport'
        }

        var table = $("#DrasftTbl").DataTable({
            ajax: {
                url: fullUrl,
                type: "POST",
                data: function (d) {
                    d.sType = $('#ddServiceType').find("option:selected").text();
                    d.visitType = $('#ddVistType').find("option:selected").text();
                    d.branch = $('#ddBranch').find("option:selected").text();
                    d.startDate = _startDate;
                    d.endDate = _endDate;
                },
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val(),
                    Authorization: 'Bearer ' + jtoken,
                },
            },
            processing: true,
            serverSide: true,
            filter: true,
            responsive: true,
            lengthChange: false,
            autoWidth: false,
            ordering: false,
            buttons: ["copy", "csv", "excel", "pdf", "print"],
            columns: [
                { data: "serviceId", name: "serviceId" },
                { data: "branchName", name: "branchName" },
                { data: "serviceTypeName", name: "serviceTypeName" },
                { data: "vistTypeName", name: "vistTypeName" },
                { data: "completionDate", name: "completionDate" },
                { data: "remark", name: "remark" },
                {
                    data: null,
                    // render: function (data, row) { return '<div >'+data.serviceId+'</div>'; }
                    render: function (data, row) {
                        let viewData = '';
                        let editData = '';
                        if (data.serviceTypeId == 1) {
                            viewData = '<a target="_blank" class="dropdown-item" href="PreventiveView?ServiceId=' + data.serviceId + '">View Details</a>'
                        } else {
                            viewData = '<a target="_blank" class="dropdown-item" href="correctiveView?ServiceId=' + data.serviceId + '">View Details</a>'
                        }
                        if (arole == "Administrator") {
                            editData = '<li class="changeDatecss" serviceid="' + data.serviceId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-ChangeDate">Change Date</a></li>'
                            if (data.serviceTypeId == 1) {
                                editData += '<li class="editDatecss" serviceid="' + data.serviceId + '"><a target="_blank"  class="dropdown-item " href="editpreventive?ServiceId=' + data.serviceId + '" >Edit</a></li>'
                            } else {
                                editData += '<li class="editDatecss" serviceid="' + data.serviceId + '"><a target="_blank"  class="dropdown-item " href="editcorrective?ServiceId=' + data.serviceId + '" >Edit</a></li>'

                            }

                        }

                        return '<td><div class="dropdown"><td>'
                            + '<div class="dropdown"><button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">Action'
                            + '</button><ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">'
                            + '<li class="adddCommentlnk" serviceid="' + data.serviceId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-Add">Add Comment</a></li>'
                            + '<li class="viewComment" serviceid="' + data.serviceId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-view">View Comments</a></li>'
                            + '<li>'
                            + viewData
                            + '</li>'
                            + editData
                            + '<li class="deleteBtn" serviceid="' + data.serviceId + '"><a  class="dropdown-item text-danger" href="#"  data-toggle="modal" data-target="#modal-delete">Delete</a></li>'
                    }
                },
            ],

        }).buttons().container().appendTo('#DrasftTbl_wrapper .col-md-6:eq(0)');

    }



    $(document.body).on("change", "#ddBranch", function (e) {
        var selectedText = $(this).find("option:selected").text();
        val = $(this).val();
        filterColumn(selectedText, val, 1);
    });
    $(document.body).on("change", "#ddServiceType", function (e) {
        var selectedText = $(this).find("option:selected").text();
        val = $(this).val();
        // $("input[type='search']").val(selectedText);
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
                    text
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

                $('#ddBranch').append('<option value="">All Branch</option>')
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
            loadDataTable(datefilterval);
            $("#newDateChange").val('')
            toastr["success"]("Date changed successfuly.")
        });
    }


    var obj;
    $('body').on('click', '.deleteBtn', function () {

        var ServiceId = $(this).attr('ServiceId')
        $('#ServiceToDeleteName').html('#' + ServiceId)

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

            loadDataTable(datefilterval)
            toastr["success"]("Service delete successfuly.")
        });



    })


})