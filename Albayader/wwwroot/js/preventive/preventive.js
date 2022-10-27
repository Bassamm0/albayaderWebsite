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

    $('#ddStatusAfter').select2();
    $('#Equipments1').select2();
    $('#MaterialUsed1').select2({
        placeholder: 'Select  Materials Used  ...'
    });
    $('#Rquiredmaterials1').select2({
        placeholder: 'Select  Materials Required  ...'
    });

    let cn = 1;
    $('body').on('click', '#AddAnotherForm', function () {

        addAnotherFormSer();
    })

    // load draft
    getServiceDetails(_ServiceId)
    function getServiceDetails(serivceId) {
    
        var Sdata = '{"id":' + serivceId + '}'
        $.ajax({
            type: "POST",
            url: APIURL + "service/getservicebyid",
            contentType: "application/json; charset=utf-8",
            data: Sdata,
           // async: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, status, xhr) {   // success callback function
                constract(data)
            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                if (jqXhr.status == 401) {
                    window.location.href = 'Index';
                }
                console.log('Error: something went wronge please try again later');
            }

        }).done(function () {

            console.log('done')

        });

    }

    function constract(ServiceObject) {
        
        console.log(ServiceObject)
        for (var i = 0; i < ServiceObject.serviceDetails.length; i++) {
            if (i > 0) {
                addAnotherFormSer()
            }
            $("#Equipments"+(i+1)).val(ServiceObject.serviceDetails[i].equipmentId).trigger('change');
            $("#Serial" + (i + 1)).val(ServiceObject.serviceDetails[i].serialNo);
            $("#Elect" + (i + 1)).prop("checked", ServiceObject.serviceDetails[i].elect).change();
            $("#Moving" + (i + 1)).prop("checked", ServiceObject.serviceDetails[i].moving).change();
            $("#Bearings" + (i + 1)).prop("checked", ServiceObject.serviceDetails[i].bearings).change();
            $("#Bells" + (i + 1)).prop("checked", ServiceObject.serviceDetails[i].bells).change();
            $("#Motor" + (i + 1)).prop("checked", ServiceObject.serviceDetails[i].motor).change();
            $("#Heater" + (i + 1)).prop("checked", ServiceObject.serviceDetails[i].heater).change();
            $("#Safety" + (i + 1)).prop("checked", ServiceObject.serviceDetails[i].safetySwitch).change();
            $("#Control" + (i + 1)).prop("checked", ServiceObject.serviceDetails[i].controlBoard).change();
            $("#Compressor" + (i + 1)).prop("checked", ServiceObject.serviceDetails[i].compressor).change();
            $("#Tmp" + (i + 1)).prop("checked", ServiceObject.serviceDetails[i].tmpControl).change();

             IntuploadedFilesBefore = "";
             IntuploadedFilesAfter = "";
            for (var p = 0; p < ServiceObject.serviceDetails[i].servicePictures.length; p++) {

               
                if (ServiceObject.serviceDetails[i].servicePictures[p].pictureTypeId == 1) {
                    IntuploadedFilesBefore += "<div>"
                    IntuploadedFilesBefore += "<div class='file-preview-frame krajee-default  kv-preview-thumb'>"
                    IntuploadedFilesBefore += "<div><a href='" + UploadUrl + ServiceObject.serviceDetails[i].servicePictures[p].fileName + "' target='_blank'><image src='" + UploadUrl + ServiceObject.serviceDetails[i].servicePictures[p].fileName + "' style='width:100px;height:100px' /></a></div>"
                    IntuploadedFilesBefore += "<div><button type='button' class='btn btn-block btn-info deletImage' filename='" + ServiceObject.serviceDetails[i].servicePictures[p].fileName + "'  data-toggle='modal' data-target='#modal-delete'>Delete</button></div>"
                    IntuploadedFilesBefore += "</div>"
                    IntuploadedFilesBefore += "</div>"
                }
             
                if (ServiceObject.serviceDetails[i].servicePictures[p].pictureTypeId == 2) {
                    IntuploadedFilesAfter += "<div>"
                    IntuploadedFilesAfter += "<div class='file-preview-frame krajee-default  kv-preview-thumb'>"
                    IntuploadedFilesAfter += "<div><a href='" + UploadUrl + ServiceObject.serviceDetails[i].servicePictures[p].fileName + "' target='_blank'><image src='" + UploadUrl + ServiceObject.serviceDetails[i].servicePictures[p].fileName + "' style='width:100px;height:100px' /></a></div>"
                    IntuploadedFilesAfter += "<div><button type='button' class='btn btn-block btn-info deletImage' filename='" + ServiceObject.serviceDetails[i].servicePictures[p].fileName + "'  data-toggle='modal' data-target='#modal-delete'>Delete</button></div>"
                    IntuploadedFilesAfter += "</div>"
                    IntuploadedFilesAfter += "</div>"
                }

                //append
            }
            $("#PicturesBeforeFix" + (i + 1)).parent('div').parent('span').parent('div').next('div').children('div').append(IntuploadedFilesBefore)
            $("#PicturesAfterFix" + (i + 1)).parent('div').parent('span').parent('div').next('div').children('div').append(IntuploadedFilesAfter)


            //$('#mySelect2').val(['1', '2']);
  
            var arrayused=[]
            for (var u = 0; u < ServiceObject.serviceDetails[i].materialsUsed.length; u++) {
                arrayused[u] = ServiceObject.serviceDetails[i].materialsUsed[u].materialId
            }
            var arrayReq = []
            for (var r = 0; r < ServiceObject.serviceDetails[i].requiredMaterials.length; r++) {
                arrayReq[r] = ServiceObject.serviceDetails[i].requiredMaterials[r].materialId
            }
            console.log(i,arrayused)
            $('#MaterialUsed' + (i + 1)).val(arrayused);
            $('#Rquiredmaterials' + (i + 1)).val(arrayReq);
            $('#MaterialUsed' + (i + 1)).trigger('change');
            $('#Rquiredmaterials' + (i + 1)).trigger('change');
            $('#ServiceDetailsid' + (i + 1)).val(ServiceObject.serviceDetails[i].serviceDetailId)

        }
 
        $("#ddStatusAfter").val(ServiceObject.statusAfterId)
        $("#ddStatusAfter").trigger('change');
        $("#serviceRemark").val((ServiceObject.remark).replaceAll('<br>', '\n'));
        //ServiceDetailsid1
        toastr["success"]("Service loaded successfuly.")
    }

    // functions

    function addAnotherFormSer() {
        validator.resetForm();

        var fielUploadValidationHolder = '<div class="fielUploadValidationHolder">'
            + ' <span id="uploadError" class="errorMessage uploadError"></span>'
            + ' <progress id="fileProgress" style="display: none" class="progress"></progress>'
            + ' <span id="lblMessage" class="uploadSccessMessage lblMessage"></span>'
            + '  <div id="thumbHolder" class="thumbHolder"></div>'
            + ' </div>'

        cn++;

        console.log('add', cn)

        var parts = ''
            + ' <legend class="w-auto px-2">Parts</legend>'
            + '<div class="row"> '
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Elect' + cn + '" name="Elect' + cn + '" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Elect' + cn + '">Elect Parts</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Moving' + cn + '" name="Moving' + cn + '" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Moving' + cn + '">Moving Parts</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Bearings' + cn + '" name="Bearings' + cn + '" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Bearings' + cn + '">Bearings</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Bells' + cn + '" name="Bells' + cn + '" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Bells' + cn + '">Belts</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Motor' + cn + '" name="Motor' + cn + '" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Motor' + cn + '">Motor</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Heater' + cn + '" name="Heater' + cn + '" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Heater' + cn + '">Heater</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Safety' + cn + '" name="Safety' + cn + '" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Safety' + cn + '">Safety Switch</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Control' + cn + '" name="Control' + cn + '" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Control' + cn + '">Control Board</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Compressor' + cn + '" name="Compressor' + cn + '" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Compressor' + cn + '">Compressor</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Tmp' + cn + '" name="Tmp' + cn + '" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Tmp' + cn + '">Tmp. Control</label> </div>'
            + '</div>'


        $('#Equipments1').select2('destroy');
        $('#MaterialUsed1').select2('destroy');
        $('#Rquiredmaterials1').select2('destroy');

        let clone = $("#Equipment1").clone()

        // clone.find('#Elect,#Moving,#Bearings,#Bells,#Motor,#Heater,#Safety,#Control,#Compressor,#Tmp').removeAttr("checked")

       // clone.find('.removeForm').attr("data-card-widget", "remove")
        clone.find('.removeForm').attr("EquId", cn)

        clone.find('#Equipments1').attr("aria-describedby", '#Equipments' + cn + '-error')

        clone.find('#Serial1').val('')
        clone.find('#MaterialUsed1').val('')
        clone.find('#Rquiredmaterials1').val('')

        clone.find('#Equipment1').attr("EquId", cn)
        clone.find('#Equipment1').attr("id", "Equipment" + cn)

        clone.find('#Equipments1').attr("name", "Equipments" + cn)
        clone.find('#Equipments1').attr("id", "Equipments" + cn)


        clone.find('#Serial1').attr("id", "Serial" + cn)

        clone.find('#partsHolder1').html(parts)

        clone.find('#MaterialUsed1').attr("name", "MaterialUsed" + cn)
        clone.find('#Rquiredmaterials1').attr("name", "Rquiredmaterials" + cn)

        clone.find('#MaterialUsed1').attr("id", "MaterialUsed" + cn)
        clone.find('#Rquiredmaterials1').attr("id", "Rquiredmaterials" + cn)



        clone.find('#PicturesBeforeFix1').attr("id", "PicturesBeforeFix" + cn)
        clone.find('#PicturesAfterFix1').attr("id", "PicturesAfterFix" + cn)

        clone.find('#uploadBeforeHolder').html(' <input id="PicturesBeforeFix' + cn + '" name="PicturesBeforeFix" class="file fa-2x fileImage " type="file" multiple="true" style="height:50px;">' + fielUploadValidationHolder)
        clone.find('#uploadAftereHolder').html(' <input id="PicturesAfterFix' + cn + '" name="PicturesBeforeFix" class="file fa-2x fileImage " type="file" multiple="true" style="height:50px;">' + fielUploadValidationHolder)

        clone.find('#ServiceDetailsid1').attr("value", "")
        clone.find('#ServiceDetailsid1').attr("id", "ServiceDetailsid" + cn)
        clone.find('#ServiceDetailsid1').attr("name", "ServiceDetailsid" + cn)


        // remove select
        //clone.find('#Equipments'+cn).next('.select2-container').remove();
        //clone.find('#MaterialUsed'+cn).next('.select2-container').remove();
        //clone.find('#Rquiredmaterials'+cn).next('.select2-container').remove();

        clone.appendTo('#mainEquHolder');


        // initailaize the switch
        $('#Elect' + cn).bootstrapToggle();
        $('#Moving' + cn).bootstrapToggle();
        $('#Bearings' + cn).bootstrapToggle();
        $('#Bells' + cn).bootstrapToggle();
        $('#Motor' + cn).bootstrapToggle();
        $('#Heater' + cn).bootstrapToggle();
        $('#Control' + cn).bootstrapToggle();
        $('#Safety' + cn).bootstrapToggle();
        $('#Compressor' + cn).bootstrapToggle();
        $('#Tmp' + cn).bootstrapToggle();


        // initailaize the uploader
        initFileInput("PicturesBeforeFix" + cn, 1);
        initFileInput("PicturesAfterFix" + cn, 2);


        $("#Equipments1").select2();
        $("#MaterialUsed1").select2();
        $("#Rquiredmaterials1").select2();


        // $('#Equipments1').trigger('change');
        $('#Equipments' + cn).attr("required");

        $('#Equipments' + cn).select2();
        $('#MaterialUsed' + cn).select2({
            placeholder: 'Select  Materials Used  ...'
        });
        $('#Rquiredmaterials' + cn).select2({
            placeholder: 'Select  Materials Required  ...'
        });

        $('#MaterialUsed' + cn).val(null).trigger('change');
        $('#Rquiredmaterials' + cn).val(null).trigger('change');
    }
    function initFileInput(element,type) {
        
        $('#'+element).fileinput('refresh',
            {
                initialPreviewAsData: true,
                allowedFileExtensions: ['jpg', 'png', 'gif', 'jpeg' ],
                showUpload: true,
                showCaption: false,
                maxFileSize: 4000,
                browseClass: "btn btn-primary btn-lg",
                previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
                showUploadedThumbs: false,
                showRemove: true,
                maxFileCount: 5,

            });
        $('#' + element).attr("EquId", cn)
        $('#' + element).attr("pictureType", type)
     



      
    }
   



    $('body').on('click', '.fileinput-upload-button', function () {

        $("#ServiceForm").submit(function (e) {
            return false;
        });

        var EquId = $(this).next('div').children('input').attr('EquId')
        var pictureType = $(this).next('div').children('input').attr('picturetype')
       

        if ($('#Equipments' + EquId).val() == '') {
            alert('Please select Equipment before uploading any image')
            return;

        }
        // validate the service details


        var validationElem = $(this).next('div').parent('span').parent('div').next('div');
        var InputElmentid = $(this).next('div').children('input').attr('id');
        saveDetailsOnUpload(InputElmentid, validationElem, EquId, pictureType)

       
    })
    
    function saveDetailsOnUpload(InputElmentid, validationElem, EquId, pictureType) {
       
        var obj = $('body').find('#ServiceDetailsid' + EquId);
         var Operation=""
        if ($(obj).val() == '') {
            Operation = "add"
            ServiceDetailsId = 0;

        } else {
            // update 
            ServiceDetailsId = $(obj).val();
            console.log('id', ServiceDetailsId)
            Operation = "update"

        }

        var url = APIURL + 'servicedetails/'+Operation
       
            // create new
            var ServiceId = _ServiceId;
            var equipmentId = $("#Equipments" + EquId).val();
            var elect = $('#Elect' + EquId).prop('checked');
            var moving = $('#Moving' + EquId).prop('checked');
            var bearings = $('#Bearings' + EquId).prop('checked');
            var bells = $('#Bells' + EquId).prop('checked');
            var motor = $('#Motor' + EquId).prop('checked');
            var heater = $('#Heater' + EquId).prop('checked');
            var Safety = $('#Safety' + EquId).prop('checked');
            var controlBoard = $('#Control' + EquId).prop('checked');
            var compressor = $('#Compressor' + EquId).prop('checked');
            var tmpControl = $('#Tmp' + EquId).prop('checked');
            var serialNo = $("#Serial" + EquId).val();

        var senddata = '{"serviceDetailId":' + ServiceDetailsId + ',"ServiceId":' + ServiceId + ',"equipmentId":' + equipmentId + ',"elect":' + elect + ',"moving":' + moving + ',"bearings":' + bearings + ',"bells":' + bells + ',"motor":' + motor + ',"heater":' + heater + ',"safetySwitch":' + Safety + ',"controlBoard":' + controlBoard + ',"compressor":' + compressor + ',"tmpControl":' + tmpControl + ',"serialNo":"' + serialNo + '"}'
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
                    if (Operation == "add") {
                        $(obj).val(data.serviceDetailId);
                        addMaterials(EquId, data.serviceDetailId);
                        if (document.getElementById(InputElmentid).files.length > 0) {
                            uploadImages(InputElmentid, validationElem, EquId, data.serviceDetailId, pictureType);

                        }
                    } else {
                        serviceDetailId= $(obj).val()
                        addMaterials(EquId, serviceDetailId);
                        if (document.getElementById(InputElmentid).files.length > 0) {
                            uploadImages(InputElmentid, validationElem, EquId, serviceDetailId, pictureType);
                        }
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

                console.log('done')

            });
      
    }


    function uploadImages(InputElmentid, validationElem, EquId, ServiceDetailsId, pictureTypeId) {
     
        var files = document.getElementById(InputElmentid).files
        var formData = new FormData();

        // Loop through files
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            formData.append("files", file);

        }
        formData.append("serviceDetailsId", ServiceDetailsId);
        formData.append("pictureTypeId", pictureTypeId);


        // disable save
        $('#SaveDraft').attr('disabled', true);
        $('#SaveAndContinue').attr('disabled', true);

        $.ajax({
            url: APIURL + 'fileupload/UploadServiceImages',
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




    function addMaterials(EquId, ServiceDetailsId) {

        var url = APIURL + 'servicedetails/addMaterial';

        var selectedRequiredmaterials = $('#Rquiredmaterials' + EquId).select2('data');
        var Requiredmaterials = [];
        if (selectedRequiredmaterials.length > 0) {
            for (var i = 0; i < selectedRequiredmaterials.length; i++) {
                Requiredmaterials[i] =parseInt(selectedRequiredmaterials[i].id);
            }
        }

        var selectedMaterialUsed = $('#MaterialUsed' + EquId).select2('data')
        var MaterialUsed = []
        if (selectedMaterialUsed.length > 0) {
            for (var i = 0; i < selectedMaterialUsed.length; i++) {
                MaterialUsed[i] = parseInt(selectedMaterialUsed[i].id);
            }
        }

        if (Requiredmaterials.length == 0 && MaterialUsed.length == 0) {
          
            return
        }       
        var sendReqData = '{"serviceDetailsId":' + ServiceDetailsId + ',"requiredmaterials":' + JSON.stringify(Requiredmaterials) + ',"materialUsed":' + JSON.stringify(MaterialUsed) + '}'

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
                }
                console.log('Error: something went wronge please try again later');
            }
        }).done(function () {
            console.log('done')
        });

    }

    
    




    $('#ServiceForm').validate({
        rules: {
            ddStatusAfter: {
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


    var validator = $("#ServiceForm").validate();

    

    $.validator.addClassRules("ddEquipment", {
        required: true,
    });




    // delete 
    $('body').on('click', '.deletImage', function () {
        var image = $(this).attr('filename')
        $('#DeleteImageBtn').attr('fileName',image)
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
                }
                console.log('Error: something went wronge please try again later');
            }

        }).done(function () {
            $('#Closedelete').click();
            $(".deletImage[filename='" + image + "']").parent('div').parent('div').remove();
            toastr["success"]("Image deleted successfuly.")
         

        });
    }
    var deletedform;

    $('body').on('click', '.removeForm', function (e) {
        deletedform = $(this);
        var EquId = $(this).attr('EquId')
        
        if (EquId == "1") {
            return;
        }
        // remove all form
        $('#deletedItemName').html('full form details')

        var ServiceDetailsId = $('body').find('#ServiceDetailsid' + EquId).val();
        
        if (ServiceDetailsId == '') {
            deletedform.parent('div').parent('div').parent('div').remove();
            cn--;
            return;
        }
        $('#deletedServiceDetailsId').val(ServiceDetailsId)
        $('#modal-details').modal('show')
        //deleteServiceDetails($(this),ServiceDetailsId)
       
    });

    $('body').on('click', '#deleteServiceDetailsbtn', function () {

        deleteServiceDetails();
       //cn--;
        console.log('remove', cn)

    })
    
    function deleteServiceDetails() {


        ServiceDetailsId = $('#deletedServiceDetailsId').val()

        var url = APIURL + 'servicedetails/deleteservicedetails';
        var data = '{"serviceDetailsId":' + ServiceDetailsId + '}'

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
                toastr["success"]("Equipment deleted successfuly.")

            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {
            $('#modal-details').modal('hide');
            deletedform.parent('div').parent('div').parent('div').remove()
            cn--;

        });


    }



    // save draft
    $('#SaveDraft').click(function () {

        var validation = true;
        if ($("#ServiceForm").valid()) {

            for (i = 1; i <= cn; i++) {
                if ($("#PicturesAfterFix" + i)[0].files[0] != null || $("#PicturesBeforeFix" + i)[0].files[0] != null) {

                    alert('Please upload or remove the selected picture before save')
                    validation = false;
                    e.preventDefault();
                    return;
                } 
            }

            if (validation) {
                for (i = 1; i <= cn; i++) {
                    SaveService(i)
                }
                updateStatus(3)
                toastr["success"]("Service saved successfuly as Draft.")
            }

        }

    })



    function SaveService( EquId) {

        var obj = $('body').find('#ServiceDetailsid' + EquId);
        var Operation = ""
        if ($(obj).val() == '') {
            Operation = "add"
            ServiceDetailsId = 0;

        } else {
            // update 
            ServiceDetailsId = $(obj).val();
            console.log('id', ServiceDetailsId)
            Operation = "update"

        }

        var url = APIURL + 'servicedetails/' + Operation
    
        // create new
        var ServiceId = _ServiceId;
        var equipmentId = $("#Equipments" + EquId).val();
        var elect = $('#Elect' + EquId).prop('checked');
        var moving = $('#Moving' + EquId).prop('checked');
        var bearings = $('#Bearings' + EquId).prop('checked');
        var bells = $('#Bells' + EquId).prop('checked');
        var motor = $('#Motor' + EquId).prop('checked');
        var heater = $('#Heater' + EquId).prop('checked');
        var Safety = $('#Safety' + EquId).prop('checked');
        var controlBoard = $('#Control' + EquId).prop('checked');
        var compressor = $('#Compressor' + EquId).prop('checked');
        var tmpControl = $('#Tmp' + EquId).prop('checked');
        var serialNo = $("#Serial" + EquId).val();

        var senddata = '{"serviceDetailId":' + ServiceDetailsId + ',"ServiceId":' + ServiceId + ',"equipmentId":' + equipmentId + ',"elect":' + elect + ',"moving":' + moving + ',"bearings":' + bearings + ',"bells":' + bells + ',"motor":' + motor + ',"heater":' + heater + ',"safetySwitch":' + Safety + ',"controlBoard":' + controlBoard + ',"compressor":' + compressor + ',"tmpControl":' + tmpControl + ',"serialNo":"' + serialNo + '"}'
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
                if (Operation == "add") {
                    $(obj).val(data.serviceDetailId);
                    addMaterials(EquId, data.serviceDetailId);

                   
                   
                } else {
                    serviceDetailId = $(obj).val()
                    addMaterials(EquId, serviceDetailId);
                    
                   
                }

            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback
                if (jqXhr.status == 401) {
                    window.location.href = 'Index';
                }
                console.log('Error: something went wronge please try again later');
            }

        }).done(function () {

            console.log('done')

        });

    }




    function updateStatus(statusId) {

        console.log(_ServiceId)
        var remark = $('#serviceRemark').val().replace(/\n/g, '<br>');
        var url = APIURL + 'service/updatestatus'
        var statusAfterId = $('#ddStatusAfter').val()
        var siteVistTypeId = 1;

        var senddata = '{"serviceId":' + _ServiceId + ',"statusId":' + statusId + ',"remark":"' + remark + '","statusAfterId":' + statusAfterId + ',"siteVistTypeId":' + siteVistTypeId + '}'
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
                   
                    window.location.href = "PreventiveSignature?ServiceId=" + _ServiceId;
                }
            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                if (jqXhr.status == 401) {
                    window.location.href = 'Index';
                }
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {

            console.log('done')

        });
    }




    $('#SaveAndContinue').click(function () {

        var validation = true;
        if ($("#ServiceForm").valid()) {

            for (i = 1; i <= cn; i++) {
                if ($("#PicturesAfterFix" + i)[0].files[0] != null || $("#PicturesBeforeFix" + i)[0].files[0] != null) {

                    alert('Please upload or remove the selected picture before save')
                    validation = false;
                    e.preventDefault();
                    return;
                }
            }

            if (validation) {
                for (i = 1; i <= cn; i++) {
                    SaveService(i)
                }
                updateStatus(4)
            }

        }

    })

    
});