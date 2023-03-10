$(document).ready(function () {

    const APIURL = $('#APIURI').val();

    const jtoken = $('#utoken').val();
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


    $('.select2').select2();
    // get comp
    getCompanies()
    function getCompanies() {

        $("#ddCompanies").html()
        $.ajax({
            type: "GET",
            url: APIURL +"company/all",
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


    var companyId = getUrlParameter('companyId');
    var serviceType = getUrlParameter('serviceType');
    $('#serviceType').val(serviceType)
   
    if (companyId > 0) {
        getCompanyBranch(companyId);
        $("#ddCompanies").val(companyId).trigger('change');
        $("#ddCompanies").select2('enable', false);
    } 



    $('body').on("change", "#ddCompanies", function () {
        companyId = $('#ddCompanies').val()
        getCompanyBranch(companyId);
    });
    


    function getCompanyBranch(_companyId) {

        $("#ddBranch").html('')
        $.ajax({
            type: "POST",
            url: APIURL +"branch/companybranchs",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ 'companyid':parseInt(_companyId) }),
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


    $('body').on('click', '#StartPreventiveService', function () {

      var  branchId= $('#ddBranch').val();
        if ($("#preStartForm").valid()) {

           // window.location.replace("Preventive?BranchId=" + branchId);
            $('#preStartForm').submit();
        }


    })


    // validation

    $('#preStartForm').validate({
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
})