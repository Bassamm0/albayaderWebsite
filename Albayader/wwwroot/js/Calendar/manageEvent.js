$(document).ready(function () {


    const APIURL = $('#APIURL').val();
    console.log('url', APIURL)

    const jtoken = $('#utoken').val();
    

    console.log('token',jtoken)
    let fixtype = false;



    $('#startDate').datetimepicker({
        format: 'DD-MM-yyyy'
    });
    $('#endDate').datetimepicker({
        format: 'DD-MM-yyyy'
    });
    $('.select2').select2();










    $('body').on("change", "#ddType", function () {
        typeid =  $(this).val()
        console.log(typeid)
        if (typeid == 1 || typeid == 2) {

            $('#eventFixHolder').css('display', 'block')
            fixtype = true;
           
        } else {
            $('#eventFixHolder').css('display', 'none')
            fixtype = false;

        }
        addRemoveValidation(fixtype)

    });




    getAllTech()
    function getAllTech() {

        $("#ddTechnicain").html()
        $.ajax({
            type: "GET",
            url: APIURL + "user/getalltechnicain",
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
                console.log(arrUpdates)
                $('#ddTechnicain').append('<option value="">Select Client Company  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].firstName + ' '+arrUpdates[i].lastname);
                    val = arrUpdates[i].userId;
                    populate(text, val, '#ddTechnicain');

                }
            },
            error: function (xhr, textStatus, errorThrown) {
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
                $('#ddTechnicain').append('<option value="">Data Not Loaded  ...</option>')
                console.log('Error in Operation');
            }
        });

    }

    // get comp
    getCompanies()
    function getCompanies() {

        $("#ddCompanies").html()
        $.ajax({
            type: "GET",
            url: APIURL + "company/all",
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
                console.log(arrUpdates)
                $('#ddCompanies').append('<option value="">Select Client Company  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].name);
                    val = arrUpdates[i].companyID;
                    populate(text, val, '#ddCompanies');

                }
            },
            error: function (xhr, textStatus, errorThrown) {
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
                $('#ddCompanies').append('<option value="">Data Not Loaded  ...</option>')
                console.log('Error in Operation');
            }
        });

    }




    $('body').on("change", "#ddCompanies", function () {
        companyId = $('#ddCompanies').val()
        getCompanyBranch(companyId);
    });



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








    // validation 
    $('#SaveEvent').click(function (e) {

        if ($("#EventForm").valid()) {
          
           $('#EventForm').submit();
        }
    })
    function addRemoveValidation(fix){

        if (fix) {

            jQuery.validator.addClassRules("dropreq", {
                required: true,

            });
        } else {

            jQuery.validator.removeClassRules("dropreq", {
                required: true,

            });
        }

    }


    $('#EventForm').validate({
        rules: {
            ddType: {
                required: true,
                maxlength: 50,
            },
            startDate: {
                required: true,
                maxlength: 50,
            },
            title: {
                required: true,
                maxlength: 50,
            },
            description: {

                maxlength: 250,
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

})