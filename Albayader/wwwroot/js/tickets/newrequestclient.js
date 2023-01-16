$(document).ready(function () {


    const APIURL = $('#APIURI').val();
    const UploadUrl = $('#Uploadlocation').val();
    const jtoken = $('#utoken').val();


    $('#details').summernote({
        height: 250,   //set editable area's height
        codemirror: { // codemirror options
            theme: 'monokai'
        }
    });

    $('#replyTxt-error').html('')
    $('#replyTxt-error').css('display', 'none')


    $(".fileImage").fileinput({
        initialPreviewAsData: true,
        allowedFileExtensions: ['jpg', 'png', 'gif', 'jpeg', 'pdf'],
        showUpload: false,
        showCaption: false,
        maxFileSize: 4000,
        browseClass: "btn btn-primary btn-lg",
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
        showUploadedThumbs: false,
        showRemove: true,
        maxFileCount: 5,
    });


    $('.select2').select2();

    // validation 
    $('#createTicket').click(function (e) {

        $('#uploadError').text('')
        if ($("#ddSeverity").val() == '3' || $("#ddSeverity").val() == '4') {
            var ufiles = document.getElementById('Pictures').files

            if (ufiles.length == 0) {
                toastr["warning"]("As your problem severity is higher than medium, kindly provide some photos for the issue.")
                $('#uploadError').text('Please choose image to upload')
                return;
            }

        }

        if ($("#newRequestForm").valid()) {

            var validationElem = '#fielUploadValidationHolder';

            var InputElmentid = 'Pictures';

            saveDetailsOnUpload(InputElmentid, validationElem);
        }
    })


    
    function saveDetailsOnUpload(InputElmentid, validationElem) {



        $('#replyTxt-error').html('')
        $('#replyTxt-error').css('display', 'none')
        

        var url = APIURL + 'tickets/addticketclient' 

        // create new
        var subject = $("#subject").val();
        var ticketDetails = $("#details").val();
        var severityId = $("#ddSeverity").val();
        var ticketCategoryId = $("#ddCategory").val();
        var createdBy = $("#ddusers").val();

        if (ticketDetails == '') {
            $('#replyTxt-error').html('This field is required.')
            $('#replyTxt-error').css('display', 'block')
            return;
        }
    

        var senddata = '{"subject":"' + subject + '","ticketDetails":"' + ticketDetails + '","severityId":' + severityId + ',"ticketCategoryId":' + ticketCategoryId + ',"createdBy":' + createdBy +'} '
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
                uploadImages(InputElmentid, validationElem, data.ticketId);
            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                if (jqXhr.status == 401) {
                    window.location.href = 'Index';
                    alert(' Your login session expired, Please login again.');
                    return;
                }
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {

            console.log('done')
            var files = document.getElementById(InputElmentid).files
            if (files.length == 0) {
                window.location.href = 'tickets';
            }

        });

    }




    function uploadImages(InputElmentid, validationElem, ticketId) {

        var files = document.getElementById(InputElmentid).files
        var formData = new FormData();

        // Loop through files
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            formData.append("files", file);

        }
        if (files.length == 0) {
            return;
        }
        $('#createTicket').attr('disabled', true);
    

         formData.append("ticketId", ticketId);
   
         $.ajax({
             url: APIURL + 'fileupload/uploadticketimages',
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
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                if (arrUpdates.length > 0) {

                    $(validationElem).children(".progress").hide();
                    $(validationElem).children(".lblMessage").html("<b>" + files.length + "</b>  files has been uploaded successfuly.");
                    $(validationElem).children(".uploadError").html('')
                    $('.fileImage').fileinput('clear');

                    let uploadedFiles = "";
                    for (var i = 0; i < arrUpdates.length; i++) {
                        uploadedFiles += "<div>"
                        uploadedFiles += "<div class='file-preview-frame krajee-default  kv-preview-thumb'>"
                        uploadedFiles += "<div><a href='" + UploadUrl + arrUpdates[i] + "' target='_blank'><image src='" + UploadUrl + arrUpdates[i] + "' style='width:100px;height:100px' /></a></div>"
                        uploadedFiles += "<div><button type='button' class='btn btn-block btn-info deletImage' filename='" + arrUpdates[i] + "'  data-toggle='modal' data-target='#modal-delete'>Delete</button></div>"
                        uploadedFiles += "</div>"
                        uploadedFiles += "</div>"

                    }

                    $(validationElem).children(".thumbHolder").append(uploadedFiles)
                    console.log(uploadedFiles)
                    toastr["success"]("Picture uploaded successfuly.")
                } else {

                    $(validationElem).children('.uploadError').html('Some Thing went wrong, please contact the administrator')
                }
                $('#createTicket').attr('disabled', false);
                window.location.href = 'tickets';

            },
            xhr: function () {
                var fileXhr = $.ajaxSettings.xhr();
                if (fileXhr.upload) {
                    $(validationElem).children(".progress").show();
                    fileXhr.upload.addEventListener("progress", function (e) {
                        if (e.lengthComputable) {
                            $(validationElem).children("progress").attr({
                                value: e.loaded,
                                max: e.total
                            });
                        }
                    }, false);
                }
                return fileXhr;
            }
        });

    }
    $('#newRequestForm').validate({
        rules: {
            subject: {
                required: true,
                maxlength: 150,
            },

            details: {
                required: true,
                maxlength: 1500,

            },
            ddSeverity: {
                required: true,

            },
            ddCategory: {
                required: true,

            },
            ddCompanies: {
                required: true,
            },
            ddusers: {
                required:true,
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
        getCompanyUsers(companyId);
    });



    function getCompanyUsers(_companyId) {
       
        $("#ddusers").html('')
        $.ajax({
            type: "POST",
            url: APIURL + "user/getCompanyUsers",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ 'companyid': parseInt(_companyId) }),
            /* async: false,*/
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                console.log(arrUpdates)
                $('#ddusers').append('<option value="">Selectc User  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].firstName + ' ' + arrUpdates[i].lastName);
                    val = arrUpdates[i].userId;
                    populate(text, val, '#ddusers');

                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#ddusers').append('<option value="">Data Not Loaded  ...</option>')
                console.log('Error in Operation');
            }
        });

    }

    function populate(text, val, controlId) {
        $(controlId).append('<option value=' + val + '>' + text + '</option>');

    }
})