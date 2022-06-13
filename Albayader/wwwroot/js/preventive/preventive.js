$(document).ready(function () {


    const APIURL = $('#APIURI').val();
    const UploadUrl = $('#Uploadlocation').val();
    const _ServiceId = $('#serviceid').val()

    $('#Equipments1').select2();
    $('#MaterialUsed1').select2({
        placeholder: 'Select  Materials Used  ...'
    });
    $('#Rquiredmaterials1').select2({
        placeholder: 'Select  Materials Required  ...'
    });



    
    let cn = 1;
    $('body').on('click', '#AddAnotherForm', function () {




        validator.resetForm();




        var fielUploadValidationHolder ='<div class="fielUploadValidationHolder">'
            +' <span id="uploadError" class="errorMessage uploadError"></span>'
            +' <progress id="fileProgress" style="display: none" class="progress"></progress>'
            +' <span id="lblMessage" class="uploadSccessMessage lblMessage"></span>'
            +'  <div id="thumbHolder" class="thumbHolder"></div>'
            + ' </div>'



        cn++;

        console.log('add', cn)

        var parts = ''
            + ' <legend class="w-auto px-2">Parts</legend>'
            + '<div class="row"> '
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Elect'+cn+'" name="Elect'+cn+'" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Elect' + cn +'">Elect Parts</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Moving'+cn+'" name="Moving'+cn+'" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Moving' + cn +'">Moving Parts</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Bearings'+cn+'" name="Bearings'+cn+'" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Bearings'+cn+'">Bearings</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Bells'+cn+'" name="Bells'+cn+'" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Bells'+cn+'">Bells</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Motor'+cn+'" name="Motor'+cn+'" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Motor'+cn+'">Motor</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Heater'+cn+'" name="Heater'+cn+'" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Heater'+cn+'">Heater</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Safety'+cn+'" name="Safety'+cn+'" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Safety' + cn +'">Safety Switch</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Control'+cn+'" name="Control'+cn+'" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Control' + cn +'">Control Board</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Compressor'+cn+'" name="Compressor'+cn+'" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Compressor'+cn+'">Compressor</label> </div>'
            + '<div class="col"> <input type="checkbox" data-toggle="toggle" data-width="75" data-height="50" id="Tmp'+cn+'" name="Tmp'+cn+'" data-on="Yes" data-off="No" > '
            + '<label class="partslable" for="Tmp'+cn+'">Tmp. Control</label> </div>'
            + '</div>'


        $('#Equipments1').select2('destroy');
        $('#MaterialUsed1').select2('destroy');
        $('#Rquiredmaterials1').select2('destroy');

        let clone = $("#Equipment1").clone()

       // clone.find('#Elect,#Moving,#Bearings,#Bells,#Motor,#Heater,#Safety,#Control,#Compressor,#Tmp').removeAttr("checked")
       
        clone.find('.removeForm').attr("data-card-widget", "remove")
        clone.find('.removeForm').attr("EquId", cn)

        clone.find('#Equipments1').attr("aria-describedby", '#Equipments'+cn+'-error')
        
        clone.find('#Serial1').val('')
        clone.find('#MaterialUsed1').val('')
        clone.find('#Rquiredmaterials1').val('')

        clone.find('#Equipment1').attr("EquId",   cn)
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
        initFileInput("PicturesBeforeFix" + cn);
        initFileInput("PicturesAfterFix" + cn);
   

        $("#Equipments1").select2();
        $("#MaterialUsed1").select2();
        $("#Rquiredmaterials1").select2();
      
       
       // $('#Equipments1').trigger('change');
        $('#Equipments' + cn).attr("required");
   
        $('#Equipments' + cn).select2();
        $('#MaterialUsed' + cn).select2({
            placeholder:'Select  Materials Used  ...'
        });
        $('#Rquiredmaterials' + cn).select2({
            placeholder: 'Select  Materials Required  ...'
        });

        $('#MaterialUsed' + cn).val(null).trigger('change');
        $('#Rquiredmaterials' + cn).val(null).trigger('change');
    })

    $('body').on('click', '.removeForm', function () {

        var eqNum = $(this).attr('EquId')
        if (eqNum == "1") {
            return;
        }
        // remove all form
        $(this).parent('div').parent('div').parent('div').remove()
     
        cn--;
        console.log('remove',cn)
    });


        $(".fileImage").fileinput({
            initialPreviewAsData: true,
            allowedFileExtensions: ['jpg', 'png', 'gif', 'pmb', 'esp', 'tif'],
            showUpload: true,
            showCaption: false,
            maxFileSize: 4000,
            browseClass: "btn btn-primary btn-lg",
            previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
            showUploadedThumbs: false,
            showRemove: true,
     
        });
    
    
    function initFileInput(element) {
        
        $('#'+element).fileinput('refresh',
            {
                initialPreviewAsData: true,
                allowedFileExtensions: ['jpg', 'png', 'gif', 'pmb', 'esp', 'tif'],
                showUpload: true,
                showCaption: false,
                maxFileSize: 4000,
                browseClass: "btn btn-primary btn-lg",
                previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
                showUploadedThumbs: false,
                showRemove: true,

            });
        $('#' + element).attr("EquId", cn)



      
    }
   


    $('body').on('click', '.fileinput-upload-button', function () {

        $("#ServiceForm").submit(function (e) {
            return false;
        });

        var EquId = $(this).next('div').children('input').attr('EquId')
        
        if ($('#Equipments' + EquId).val() == '') {
            alert('Please select Equipment before uploading any image')
            return;

        }
        // validate the service details


        var validationElem = $(this).next('div').parent('span').parent('div').next('div');
        var InputElmentid = $(this).next('div').children('input').attr('id');
        saveDetails(InputElmentid, validationElem, EquId)

       
    })


    function saveDetails(InputElmentid, validationElem, EquId) {

        var obj = $('body').find('#ServiceDetailsid' + EquId);
        if ($(obj).val() == '') {
            console.log('not saved yet')
            // create new


        } else {
            // update 
            ServiceDetailsId = $(obj).val()

        }


        uploadImages(InputElmentid, validationElem, EquId)
    }


    function uploadImages(InputElmentid, validationElem, EquId) {

        console.log('equipment num:', EquId)
      
        var files = document.getElementById(InputElmentid).files

        var formData = new FormData();
        // Loop through files
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            formData.append("files", file);

        }
        formData.append("serviceid", _ServiceId);
        $.ajax({
            url: APIURL + 'fileupload/UploadServiceImages',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                if (arrUpdates.length > 0) {


                    console.log(arrUpdates);
                    $(validationElem).children(".progress").hide();
                    $(validationElem).children(".lblMessage").html("<b>" + files.length + "</b>  files has been uploaded successfuly.");
                    $(validationElem).children(".uploadError").html('')
                    $('.fileImage').fileinput('clear');

                    let uploadedFiles = "";
                    for (var i = 0; i < arrUpdates.length; i++) {
                        uploadedFiles += "<div>"
                        uploadedFiles += "<div class='file-preview-frame krajee-default  kv-preview-thumb'>"
                        uploadedFiles += "<div><a href='" + UploadUrl + arrUpdates[i] + "' target='_blank'><image src='" + UploadUrl + arrUpdates[i] + "' style='width:100px;height:100px' /></a></div>"
                        uploadedFiles += "<div><button type='button' class='btn btn-block btn-info deletImage' filename='" + arrUpdates[i] + "'>Delete</button></div>"
                        uploadedFiles += "</div>"
                        uploadedFiles += "</div>"

                    }

                    $(validationElem).children(".thumbHolder").append(uploadedFiles)
                    console.log(uploadedFiles)
                } else {

                    $(validationElem).children('.uploadError').html('Some Thing went wrong, please contact the administrator')
                }


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

    
    // save draft
    $('#SaveDraft').click(function () {
       
        for (i = 1; i <= cn; i++){
            var obj = $('body').find('#ServiceDetailsid' + i);
            if ($(obj).val() == '') {
                console.log('not saved')
            } else {
                console.log($(obj).val())
            }
            console.log(i);
        
            console.log('is Elect checked'+i,$('#Elect' + i).prop('checked'))
    }
        
        if ($("#ServiceForm").valid()) {


        }
       
    })



    // change Equip
    $('body').on('change', '.ddEquipment', function () {
        //if ($(this).val() != '') {
        //    $('#PicturesBeforeFix').fileinput('enable');
        //    $('#PicturesAfterFix').fileinput('enable');
        //} else {
        //    $('#PicturesBeforeFix').fileinput('disable');
        //    $('#PicturesAfterFix').fileinput('disable');
        //}
      
    })


    

    $('#ServiceForm').validate({
   
        
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


});