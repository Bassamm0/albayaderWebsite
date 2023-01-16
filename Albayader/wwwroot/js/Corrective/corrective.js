$(document).ready(function () {

  


    const APIURL = $('#APIURI').val();
    const UploadUrl = $('#Uploadlocation').val();
    const _ServiceId = $('#serviceid').val()
    const jtoken = $('#utoken').val();



    $(".fileImage").fileinput({
        initialPreviewAsData: true,
        allowedFileExtensions: ['jpg', 'png', 'gif', 'jpeg','pdf'],
        showUpload: true,
        showCaption: false,
        maxFileSize: 4000,
        browseClass: "btn btn-primary btn-lg",
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
        showUploadedThumbs: false,
        showRemove: true,
        maxFileCount: 5,
    });


    $('.select2').select2();
    $('#MaterialUsed1').select2({
        placeholder: 'Select  Materials Used  ...'
    });
    $('#reportedDate').datetimepicker({
        format: 'DD-MM-yyyy'
      
    });

    //upload 

    $('body').on('click', '.fileinput-upload-button', function () {
    
        $("#correctiveForm").submit(function (e) {
            return false;

        });
        if (!$('#correctiveForm').valid()) {


            toastr["warning"]("Please select the required fields.")
            return false;
        }

        var pictureType = $(this).next('div').children('input').attr('picturetype')
        var validationElem = $(this).next('div').parent('span').parent('div').next('div');
        var InputElmentid = $(this).next('div').children('input').attr('id');
        saveDetailsOnUpload(InputElmentid, validationElem, pictureType,true);


    })



    function saveDetailsOnUpload(InputElmentid, validationElem, pictureType,WithUpload) {
       
       
        
        var obj = $('body').find('#ServiceDetailsid1');
        var Operation = ""
        if ($(obj).val() == '') {
            Operation = "addcorrective"
            ServiceDetailsId = 0;

        } else {
            // update 
            ServiceDetailsId = $(obj).val();
            console.log('id', ServiceDetailsId)
            Operation = "updatecorrective"

        }

        var url = APIURL + 'servicedetails/' + Operation

        // create new
        var serviceId = _ServiceId;
        var equipmentId = $("#Equipments1" ).val();
        var problemReported = $("#problemReported").val().replace(/\n/g, '<br>');
        var AMCTypeId = $("#ddAMCType").val();
        var conditionId = $("#ddCondition").val();
        var equipmentTypeId = $("#ddType").val();
        var reportedDate = moment($("#reportedDate").data('date'), 'D-mm-yyyy').format('yyyy-mm-D');
       
        var reportedBy = $("#reportedBy" ).val();
        var model = $("#Model" ).val();
        var serviceRendered = $("#Rendered").val().replace(/\n/g, '<br>');
        var serialNo = $("#Serial").val();
        
        var senddata = '{"serviceId":' + serviceId + ',"correctiveServiceDetailsId":' + ServiceDetailsId + ',"equipmentId":' + equipmentId + ',"problemReported":"' + problemReported + '","conditionId":' + conditionId + ' ,"equipmentTypeId": ' + equipmentTypeId + ', "reportedDate": "' + reportedDate + '", "reportedBy": "' + reportedBy + '", "model": "' + model + '", "serviceRendered": "' + serviceRendered + '", "serialNo": "' + serialNo + '", "AMCTypeId": "' + AMCTypeId + '"} '
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
                if (Operation == "addcorrective") {
                    $(obj).val(data.correctiveServiceDetailsId);
                    addMaterials(data.correctiveServiceDetailsId);
                    if (WithUpload) {
                        uploadImages(InputElmentid, validationElem, data.correctiveServiceDetailsId, pictureType);

                    }
                } else {
                    correctiveServiceDetailsId = $(obj).val()
                    addMaterials(correctiveServiceDetailsId);
                    if (WithUpload) {
                        uploadImages(InputElmentid, validationElem, correctiveServiceDetailsId, pictureType);
                    }
                }

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

        });

    }


    function uploadImages(InputElmentid, validationElem, ServiceDetailsId, pictureTypeId) {

        var files = document.getElementById(InputElmentid).files
        var formData = new FormData();

        // Loop through files
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            formData.append("files", file);

        }
        $('#SaveDraft').attr('disabled', true);
        $('#SaveAndContinue').attr('disabled', true);

        formData.append("correctiveServiceDetailsId", ServiceDetailsId);
        formData.append("pictureTypeId", pictureTypeId);
        console.log(formData)
        console.log('type', pictureTypeId)
        $.ajax({
            url: APIURL + 'fileupload/uploadserviceimagescorrective',
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
                $('#SaveDraft').attr('disabled', false);
                $('#SaveAndContinue').attr('disabled', false);

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




    function addMaterials(ServiceDetailsId) {

        var url = APIURL + 'servicedetails/addCorrectiveMaterial';

      
        var selectedMaterialUsed = $('#MaterialUsed1').select2('data')
        var MaterialUsed = []
        if (selectedMaterialUsed.length > 0) {
            for (var i = 0; i < selectedMaterialUsed.length; i++) {
                MaterialUsed[i] = parseInt(selectedMaterialUsed[i].id);
            }
        }

       
        var sendReqData = '{"serviceDetailsId":' + ServiceDetailsId + ',"materialUsed":' + JSON.stringify(MaterialUsed) + '}'

        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            data: sendReqData,
            async: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, status, xhr) {   // success callback function

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
        });

    }



    //



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


    $("#ddStatusAfter").on('change', function () {
        if (!$("#ddStatusAfter").val()) {

            $('#stsAfter').text('This field is required.')

        } else {
            $('#stsAfter').text('')


        }

    })

    //correctiveFormP
    $('#SaveDraft').click(function (e) {
        
        if (!$("#ddStatusAfter").val()) {
           
            $('#stsAfter').text('This field is required.')
            return;
           
        } else {
            $('#stsAfter').text('')
           

        }
       
        if ($('#correctiveForm').valid()) {
            if ($("#PicturesAfterFix1")[0].files[0] != null || $("#PicturesBeforeFix1")[0].files[0] != null) {

               // alert('Please upload or remove the selected picture before save')
                toastr["warning"]("Please upload or remove the selected picture before save.")
               
                e.preventDefault();
                return;
            }
        }
        if (!$('#correctiveForm').valid()) {


            toastr["warning"]("Please select the required fields.")
            return false;
        }
        saveDetailsOnUpload('','','',false)
        updateStatus(3)
       

    })

    $('#SaveAndContinue').click(function (e) {

        if (!$("#ddStatusAfter").val()) {

            $('#stsAfter').text('This field is required.')
            return;

        } else {
            $('#stsAfter').text('')


        }

        if ($('#correctiveForm').valid()) {
            if ($("#PicturesAfterFix1")[0].files[0] != null || $("#PicturesBeforeFix1")[0].files[0] != null) {

                // alert('Please upload or remove the selected picture before save')
                toastr["warning"]("Please upload or remove the selected picture before save.")

                e.preventDefault();
                return;
            }
        }
        if (!$('#correctiveForm').valid()) {


            toastr["warning"]("Please select the required fields.")
            return false;
        }
        saveDetailsOnUpload('', '', '', false)
        updateStatus(4)


    })




    function updateStatus(statusId) {

       
        var remark = $('#Remarks').val().replace(/\n/g, '<br>')
        var recomendation = $('#serviceRecommendation').val().replace(/\n/g, '<br>');
        var rootOfCause = $('#rootofcuase').val().replace(/\n/g, '<br>');

        var url = APIURL + 'service/updatestatus'
        var statusAfterId = $('#ddStatusAfter').val()
        var siteVistTypeId = $('#ddVistType').val()
        var serviceType = 2;

        var senddata = '{"serviceId":' + _ServiceId + ',"serviceType":' + serviceType + ',"statusId":' + statusId + ',"remark":"' + remark + '","recommendation":"' + recomendation + '","rootOfCause":"' + rootOfCause + '","statusAfterId":' + statusAfterId + ',"siteVistTypeId":' + siteVistTypeId + '}'
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

                if (statusId == 4) {

                   window.location.href = "CorrectiveSignature?ServiceId=" + _ServiceId;
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
            toastr["success"]("Service saved successfuly as Draft.")
            console.log('done')

        });
    }



    $('#correctiveForm').validate({
        rules: {
            ddVistType: {
                required: true,
      
            },
            ddCondition: {
                required: true,
            },
            ddType: {
                required: true,
            },
            //ddStatusAfter: {
            //    required:true,
            //},
            reportedBy: {
                required: true,
                maxlength: 50,
            },
            reportedDate: {
                required: true,
            },
            Equipments1: {
                required: true,
            },
            AMCTypeId: {
                required: true,
            },
            Model: {
                maxlength: 50,
            },
            Serial: {
                maxlength: 50,
            },
            tel: {
                digits: true,
                minlength: 10,
                maxlength: 15
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



});
