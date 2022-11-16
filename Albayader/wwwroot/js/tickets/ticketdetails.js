$(document).ready(function () {

    const arole = $('#Arole').val();

   
    const jtoken = $('#utoken').val();
    const APIURL = $('#APIURI').val();

    var ticketId = $("#ticketid").val();

    $('#replyTxt').summernote({
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
    $('#submitreply').click(function (e) {


        if ($("#replyform").valid()) {

            var validationElem = '#fielUploadValidationHolder';

            var InputElmentid = 'Pictures';

            saveDetailsOnUpload(InputElmentid, validationElem);
        }
    })



    function saveDetailsOnUpload(InputElmentid, validationElem) {
        $('#replyTxt-error').html('')
        $('#replyTxt-error').css('display', 'none')



        var url = APIURL + 'tickets/addlog'

        // create new
        var ticketId = $("#ticketid").val();
        var Message = $("#replyTxt").val();
      
        if (Message == '') {
            $('#replyTxt-error').html('This field is required.')
            $('#replyTxt-error').css('display','block')
            return;
        }

        var senddata = '{"Message":"' + Message + '","ticketId":' + ticketId + '} '
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
                if ($('#companyId').val() == 2 && $("#ddStatus").val() !='') {
                    changeStatus();
                }
               
                uploadImages(InputElmentid, validationElem, data.ticketLogId);
               
                
                $("#replyTxt").val('')
         

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
                window.location.href = 'ticketdetails?ticketId=' + ticketId;
            }

        });

    }

    function changeStatus() {
       



        var url = APIURL + 'tickets/changestatus'

        // create new
        var ticketId = $("#ticketid").val();
        var ticketStatusId = $("#ddStatus").val();
       

      

        var senddata = '{"ticketId":' + ticketId + ',"ticketStatusId":' + ticketStatusId + '} '
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
                //change status

               

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

           

        });

    }



    function uploadImages(InputElmentid, validationElem, ticketLogId) {

        var files = document.getElementById(InputElmentid).files
        var formData = new FormData();

        if (files.length == 0) {
            return;
        }
        // Loop through files
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            formData.append("files", file);

        }
        $('#submitreply').attr('disabled', true);


        formData.append("ticketLogId", ticketLogId);

        $.ajax({
            url: APIURL + 'fileupload/uploadticketLogimages',
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
                   

                    $(validationElem).children(".thumbHolder").append(uploadedFiles)
  
                    toastr["success"]("files uploaded successfuly.")
                } else {

                    $(validationElem).children('.uploadError').html('Some Thing went wrong, please contact the administrator')
                }
                $('#submitreply').attr('disabled', false);

                window.location.href = 'ticketdetails?ticketId=' + ticketId;
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
            },

             done:function () {

                  

                },

        });

    }
    $('#replyform').validate({
        rules: {
        

            replyTxt: {
                required: true,
                maxlength: 1500,

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

    $('body').on('click', '#cancel', function () {
        window.location.href = 'tickets';
    })
})  