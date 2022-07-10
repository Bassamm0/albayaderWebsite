$(document).ready(function () {



    const jtoken = $('#utoken').val();
    $('#startDate').datetimepicker({
        format: 'DD-MM-yyyy'
    });
    $('#endDate').datetimepicker({
        format: 'DD-MM-yyyy'
    });
    var obj;

    const APIURL = $('#APIURI').val();

    $('.select2').select2();




    var table = $("#DrasftTbl").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false, "ordering": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print"]
    }).buttons().container().appendTo('#DrasftTbl_wrapper .col-md-6:eq(0)');

    $(document.body).on("change", "#ddBranch", function (e) {
        var selectedText = $(this).find("option:selected").text();
        val = $(this).val();
        filterColumn(selectedText, val, 2);
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

        var url = APIURL + 'servicequote/all'
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
                  
                    var c1 = '<td><a href="quoteManage?serviceQuoteId=' + data[i].serviceQuoteId  + '">#' + data[i].serviceQuoteId + '<td>';
                   
                    let dataload = "";
                    dataload += '<td><div class="dropdown"><td>';
                    dataload += '<div class="dropdown"><button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">Action';
                    dataload += '</button><ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">';
                    dataload += '<li class="ViewQ" serviceQuoteId="' + data[i].serviceQuoteId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-view">View</a></li>';
                    dataload += '<li class="EditQ" serviceQuoteId="' + data[i].serviceQuoteId + '"><a  class="dropdown-item " href="ManageQuote?Smode=Edit&qid=' + data[i].serviceQuoteId + '"  >Edit </a></li>';
                    dataload += '<li class="DeleteQ" serviceQuoteId="' + data[i].serviceQuoteId + '"><a  class="dropdown-item DeleteLinkQ" href="#"  data-toggle="modal" data-target="#modal-delete">Delete </a></li>';

                    dataload += '</li>';
                    $("#DrasftTbl").DataTable().row.add([
                       c1,
                        data[i].serviceId,
                        data[i].branchName,
                        data[i].companyName,
                        data[i].serviceQuoteDate,
                        dataload
                    ]).draw();

                }

            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
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

        var url = APIURL + 'servicequote/allbydate'
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

                    var c1 = '<td><a href="quoteManage?serviceQuoteId=' + data[i].serviceQuoteId + '">#' + data[i].serviceQuoteId + '<td>';

                    let dataload = "";
                    dataload += '<td><div class="dropdown"><td>';
                    dataload += '<div class="dropdown"><button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">Action';
                    dataload += '</button><ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">';
                    dataload += '<li class="ViewQ" serviceQuoteId="' + data[i].serviceQuoteId + '"><a  class="dropdown-item " href="#"  data-toggle="modal" data-target="#modal-view">View</a></li>';
                    dataload += '<li class="EditQ" serviceQuoteId="' + data[i].serviceQuoteId + '"><a  class="dropdown-item " href="ManageQuote?qid=' + data[i].serviceQuoteId + '"  >Edit </a></li>';
                    dataload += '<li class="DeleteQ" serviceQuoteId="' + data[i].serviceQuoteId + '"><a  class="dropdown-item DeleteLinkQ" href="#"  data-toggle="modal" data-target="#modal-delete">Delete </a></li>';

                    dataload += '</li>';
                    $("#DrasftTbl").DataTable().row.add([
                        c1,
                        data[i].serviceId,
                        data[i].branchName,
                        data[i].companyName,
                        data[i].serviceQuoteDate,
                        dataload
                    ]).draw();

                }



            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
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


    $('body').on('click', '.DeleteQ', function () {

        var quoteId = $(this).attr('serviceQuoteId');
        $('#deletedQoute').val(quoteId);
        $('#QuoteToDeleteName').text('#' + quoteId);
        obj = $(this).parents('ul').parents('div').parents('td').parents('tr')

    })
    $('body').on('click', '#DeleteQuote', function () {

        qouteId = $('#deletedQoute').val();
        var senddata = '{"id":' + qouteId + '} '

        var url = APIURL + 'servicequote/remove'
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
                $('#closedelete').click();
            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {

            console.log('done')
            obj.remove()
            table.clear().draw();
        });

    })


    $('body').on('click', '.ViewQ', function () {

        id = $(this).attr('serviceQuoteId');
        var senddata = '{"id":' + id + '} '

        var url = APIURL + 'servicequote/getservicequotebyid'
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
                var htmlel = ''
               htmlel += ' <div class="col-md-12">'
               htmlel += '<div class="form-group">'
                htmlel += '<div class="row" style="padding-left:20px;">'
                htmlel += '<div class="col-md-12">'
                htmlel += ' <span class="labelsView">Company: </span>' + data.companyName
                htmlel += '</div>'
                htmlel += '<div class="col-md-12">'
                htmlel += ' <span class="labelsView">Branch: </span>' + data.branchName
                htmlel += '</div>'
               htmlel += '<div class="col-md-12">'
               htmlel += ' <span class="labelsView">Quote #Ref: </span>' + data.serviceQuoteId
               htmlel += '</div>'
               htmlel += '<div class="col-md-12">'
               htmlel += ' <span class="labelsView">Service #Ref: </span>' + data.serviceId
               htmlel += '</div>'
               htmlel += ' <div class="col-md-12">'
               htmlel += '<span class="labelsView">Quote Date : </span>' + data.serviceQuoteDate
               htmlel += '</div>'
               htmlel += '<div class="row" style="padding-left:20px;">'
               htmlel += '<label class="ServiceViewHeaders">Quote Details</label>'
               
                for (var i = 0;i< data.qouteDetails.length; i++) {
                   htmlel += ' <div class="col-md-12">'
                   htmlel += ' <span class="labelsView">Material : </span>' + data.qouteDetails[i].materialName
                   htmlel += '</div>'
                   htmlel += ' <div class="col-md-12">'
                   htmlel += '<span class="labelsView">Price : </span>' + data.qouteDetails[i].quotationPrice
                   htmlel += '</div>'
                   htmlel += '<div class="col-md-12">'
                   htmlel += '<span class="labelsView">qty : </span>' + data.qouteDetails[i].qty
                   htmlel += '</div>'
                   htmlel += ' <div class="col-md-12">'
                   htmlel += '<span class="labelsView">description : </span>' + data.qouteDetails[i].description
                   htmlel += '</div>'
                }

               htmlel += ' </div>'
               htmlel += '</div>'
               htmlel += '</div>'
               htmlel += '</div>'

                $('#QuoteHolder').html(htmlel);
            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {

            console.log('done')
           
        });

    })

})