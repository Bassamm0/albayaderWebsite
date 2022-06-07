$(document).ready(function () {

    $('#Equipments').select2();
    $('#MaterialUsed').select2({
        placeholder: 'Select  Materials Used  ...'
    });
    $('#Rquiredmaterials').select2({
        placeholder: 'Select  Materials Required  ...'
    });

    
    let cn = 1;
    $('body').on('click', '#AddAnotherForm', function () {



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


        $('#Equipments').select2('destroy');
        $('#MaterialUsed').select2('destroy');
        $('#Rquiredmaterials').select2('destroy');

        let clone = $("#Equipment").clone()

       // clone.find('#Elect,#Moving,#Bearings,#Bells,#Motor,#Heater,#Safety,#Control,#Compressor,#Tmp').removeAttr("checked")
       
        clone.find('.removeForm').attr("data-card-widget", "remove")
        clone.find('.removeForm').attr("EquId", cn)

       
        clone.find('#Serial').val('')
        clone.find('#MaterialUsed').val('')
        clone.find('#Rquiredmaterials').val('')

        clone.find('#Equipment').attr("EquId",   cn)
        clone.find('#Equipment').attr("id", "Equipment" + cn)
        clone.find('#Equipments').attr("id", "Equipments" + cn)

        clone.find('#Serial').attr("id", "Serial" + cn)
        clone.find('#BeforePropFixed').attr("id", "BeforePropFixed" + cn)
        clone.find('#partsHolder').html(parts)

        clone.find('#MaterialUsed').attr("id", "MaterialUsed" + cn)
        clone.find('#Rquiredmaterials').attr("id", "Rquiredmaterials" + cn)

        clone.find('#MaterialUsed').attr("name", "MaterialUsed" + cn)
        clone.find('#Rquiredmaterials').attr("name", "Rquiredmaterials" + cn)


        clone.find('#afterFix').attr("id", "afterFix" + cn)

        clone.find('#PicturesBeforeFix').attr("id", "PicturesBeforeFix" + cn)
        clone.find('#PicturesAfterFix').attr("id", "PicturesAfterFix" + cn)



        clone.find('#uploadBeforeHolder').html(' <input id="PicturesBeforeFix' + cn + '" name="PicturesBeforeFix" class="file fa-2x fileImage " type="file" multiple="true" style="height:50px;">' + fielUploadValidationHolder)
        clone.find('#uploadAftereHolder').html(' <input id="PicturesAfterFix' + cn + '" name="PicturesBeforeFix" class="file fa-2x fileImage " type="file" multiple="true" style="height:50px;">' + fielUploadValidationHolder)



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


        $("#Equipments").select2();
        $("#MaterialUsed").select2();
        $("#Rquiredmaterials").select2();
      
       
       // $('#Equipments1').trigger('change');
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
    }
   


    // upload

    //$('body').on('click', '.fileinput-upload-button', function () {
    //    console.log($(this).next('div').children('input').attr('id'))
    //})

    $('body').on('click', '.fileinput-upload-button', function () {

        var validationElem = $(this).next('div').parent('span').parent('div').next('div');
        var InputElmentid = $(this).next('div').children('input').attr('id')
        
        var files = document.getElementById(InputElmentid).files

        var formData = new FormData();
        // Loop through files
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            formData.append("files", file);



        }
        formData.append("serviceid", "1");
        $.ajax({
            url: 'https://localhost:7174/api/fileupload/UploadServiceImages',
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
                        uploadedFiles += "<div><a href='https://localhost:7174/uploads/" + arrUpdates[i] + "' target='_blank'><image src='https://localhost:7174/uploads/" + arrUpdates[i] + "' style='width:100px;height:100px' /></a></div>"
                        uploadedFiles += "<div><button type='button' class='btn btn-block btn-info deletImage' filename='" + arrUpdates[i] +"'>Delete</button></div>"
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



    })



    // save draft
    $('#SaveDraft').click(function () {
       console.log($('#Elect1').prop('checked'))
    })

});