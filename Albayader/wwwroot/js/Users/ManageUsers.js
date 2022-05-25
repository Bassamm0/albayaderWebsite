﻿$(document).ready(function () {


    

    // validation 
    $('#SaveUser').click(function (e) {

        if ($("#UserForm").valid()) {

            $('#UserForm').submit();
        }
    })


    $('#UserForm').validate({
        rules: {
            UseryName: {
                required: true,
                maxlength: 50,
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


    // upload

    $(".custom-file-input").on("change", function () {

        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
        if (fileName != '') {
            $('#RemoveLogobtn').show();
            $('#UploadLogobtn').show();
        } else {
            $('#RemoveLogobtn').hide();
            $('#UploadLogobtn').hide();
        }

    });
    $("body").on("click", "#UploadLogobtn", function () {
        if ($("#logoFile")[0].files[0] == null) {
            $('#uploadError').html('Please select a file to upload.')
            return;

        }

        var formData = new FormData();

        formData.append("files", $("#logoFile")[0].files[0]);
        $.ajax({
            url: 'https://localhost:7174/api/fileupload/upload',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                $("#fileProgress").hide();
                $("#lblMessage").html("<b>" + data + "</b> has been uploaded.");
                $("#uploadedfile").val(data);
                $('#uploadError').html('')
                $('#logoFile').val('')
                $('#RemoveLogobtn').hide();
                $('#UploadLogobtn').hide();
            },
            xhr: function () {
                var fileXhr = $.ajaxSettings.xhr();
                if (fileXhr.upload) {
                    $("progress").show();
                    fileXhr.upload.addEventListener("progress", function (e) {
                        if (e.lengthComputable) {
                            $("#fileProgress").attr({
                                value: e.loaded,
                                max: e.total
                            });
                        }
                    }, false);
                }
                return fileXhr;
            }
        });
    });
    $("body").on("click", "#RemoveLogobtn", function () {
        $('#logoFile').next('label').html('Select a file');
        $('#logoFile').val('')
        $('#RemoveLogobtn').hide();
        $('#UploadLogobtn').hide();
    })


    GetCountries();
    function GetCountries() {
        $("#ddCountry").html('')
        $("#ddNationality").html('')
        $.ajax({
            url: "https://localhost:7174/api/company/getcountries",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                console.log(arrUpdates)
                $('#ddCountry').append('<option value="">Select  Country  ...</option>')
             //   $('#ddNationality').append('<option value="">Select  Country  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].name);
                    val = arrUpdates[i].countryId;
                   populate(text, val, '#ddNationality');
                    populate(text, val, '#ddCountry');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#ddCountry').append('<option value="">Data Not Loaded  ...</option>')
                $('#ddNationality').append('<option value="">Data Not Loaded  ...</option>')
                console.log('Error in Operation');
            }
        });

    }
    GetPositions();
    function GetPositions() {
        $("#ddPosition").html('')
        $.ajax({
            url: "https://localhost:7174/api/user/getpostions",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                console.log(arrUpdates)
                $('#ddPosition').append('<option value="">Select  position  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].name);
                    val = arrUpdates[i].countryId;
                    populate(text, val, '#ddPosition');
                 }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#ddPosition').append('<option value="">Data Not Loaded  ...</option>')
                 console.log('Error in Operation');
            }
        });

    }

    function populate(text, val, controlId) {
        $(controlId).append('<option value=' + val + '>' + text + '</option>');

    }

})