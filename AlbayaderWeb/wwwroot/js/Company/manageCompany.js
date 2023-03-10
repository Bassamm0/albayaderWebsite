$(document).ready(function () {


    const APIURL = $('#APIURI').val();
    const jtoken = $('#utoken').val();

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
            url: APIURL+'fileupload/upload',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,

            },
            success: function (data) {
                $("#fileProgress").hide();
                $("#lblMessage").html("<b>" + data + "</b> has been uploaded.");
                $("#uploadedfile").val(data);
                $('#uploadError').html('')
                $('#logoFile').val('')
                $('#RemoveLogobtn').hide();
                $('#UploadLogobtn').hide();
            },
            error: function (xhr, textStatus, errorThrown) {
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                    alert(' Your login session expired, Please login again.');
                    return;
                }
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

    GetCountries( );
    function GetCountries( ) {
        $("#ddCountry").html('')
        $.ajax({
            url: APIURL+"company/getcountries",
            type: 'GET',
            dataType: 'json',
            async: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,

            },
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                console.log(arrUpdates)
                 $('#ddCountry').append('<option value="">Select  Country  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].name);
                    val = arrUpdates[i].countryId;
                    populate(text, val, '#ddCountry');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#ddCountry').append('<option value="">Data Not Loaded  ...</option>')
                console.log('Error in Operation');
            }
        });
   
    }


    function populate(text, val, controlId) {
        $(controlId).append('<option value=' + val + '>' + text + '</option>');

    }

   
});



