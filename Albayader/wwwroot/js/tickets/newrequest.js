$(document).ready(function () {


    const APIURL = $('#APIURI').val();
    const UploadUrl = $('#Uploadlocation').val();
    const jtoken = $('#utoken').val();



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

        if ($("#newRequestForm").valid()) {

            var validationElem = 'fielUploadValidationHolder';

            var InputElmentid = 'Pictures';

            saveDetailsOnUpload(InputElmentid, validationElem);
        }
    })


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

    //$('body').on('click', '.fileinput-upload-button', function () {

    //    $("#newRequestForm").submit(function (e) {
    //        return false;

    //    });
    //    if (!$('#newRequestForm').valid()) {


    //        toastr["warning"]("Please select the required fields.")
    //        return false;
    //    }

    //    var validationElem = $(this).next('div').parent('span').parent('div').next('div');

    //    var InputElmentid = $(this).next('div').children('input').attr('id');

    //    saveDetailsOnUpload(InputElmentid, validationElem);


    //})

    function saveDetailsOnUpload(InputElmentid, validationElem) {



       
        var url = APIURL + 'tickets/add' 

        // create new
        var subject = $("#subject").val();
        var ticketDetails = $("#details").val().replace(/\n/g, '<br>');
        var severityId = $("#ddSeverity").val();
        var ticketCategoryId = $("#ddCategory").val();



        var senddata = '{"subject":"' + subject + '","ticketDetails":"' + ticketDetails + '","severityId":' + severityId + ',"ticketCategoryId":' + ticketCategoryId + '} '
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
            window.location.href = 'tickets';

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


    // delete 
    $('body').on('click', '.deletImage', function () {
        var image = $(this).attr('filename')
        $('#DeleteImageBtn').attr('fileName', image)
        $('#deletedItemName').html('')
    })

    $('body').on('click', '#DeleteImageBtn', function () {
        var image = $(this).attr('filename');
        deleteImage(image);
    })
    function deleteImage(image) {

        var url = APIURL + 'servicedetails/deleteimage';
        var data = '{"image":"' + image + '"}'



        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            data: data,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, status, xhr) {   // success callback function
                console.log('deleted')


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
            $('#Closedelete').click();
            $(".deletImage[filename='" + image + "']").parent('div').parent('div').remove();
            toastr["success"]("Image deleted successfuly.")


        });
    }



})