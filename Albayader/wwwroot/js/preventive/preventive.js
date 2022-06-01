$(document).ready(function () {
   
    
    let cn = 0;
    $('body').on('click', '#AddAnotherForm', function () {
        var fielUploadValidationHolder ='<div class="fielUploadValidationHolder">'
            +' <span id="uploadError" class="errorMessage uploadError"></span>'
            +' <progress id="fileProgress" style="display: none" class="progress"></progress>'
            +' <span id="lblMessage" class="uploadSccessMessage lblMessage"></span>'
            +'  <div id="thumbHolder" class="thumbHolder"></div>'
            +' </div>'
       
        cn++;

        let clone = $("#Equipment").clone()
        clone.find('.removeForm').attr("data-card-widget", "remove")
        clone.find('#Equipment').attr("id", "Equipment" + cn)
        clone.find('#Serial').attr("id", "Serial" + cn)
        clone.find('#BeforePropFixed').attr("id", "BeforePropFixed" + cn)
        clone.find('#Elect').attr("id", "Elect" + cn)
        clone.find('#Moving').attr("id", "Moving" + cn)
        clone.find('#Bearings').attr("id", "Bearings" + cn)
        clone.find('#Bells').attr("id", "Bells" + cn)
        clone.find('#Motor').attr("id", "Motor" + cn)
        clone.find('#Heater').attr("id", "Heater" + cn)
        clone.find('#Safety').attr("id", "Safety" + cn)
        clone.find('#Control').attr("id", "Control" + cn)
        clone.find('#Compressor').attr("id", "Compressor" + cn)
        clone.find('#Tmp').attr("id", "Tmp" + cn)
        clone.find('#MaterialUsed').attr("id", "MaterialUsed" + cn)
        clone.find('#Rquiredmaterials').attr("id", "Rquiredmaterials" + cn)
        clone.find('#afterFix').attr("id", "afterFix" + cn)

        clone.find('#PicturesBeforeFix').attr("id", "PicturesBeforeFix" + cn)
        clone.find('#PicturesAfterFix').attr("id", "PicturesAfterFix" + cn)


        clone.find('[for="Elect"]').attr("for", "Elect" + cn)
        clone.find('[for="Moving"]').attr("for", "Moving" + cn)
        clone.find('[for="Bearings"]').attr("for", "Bearings" + cn)
        clone.find('[for="Bells"]').attr("for", "Bells" + cn)
        clone.find('[for="Motor"]').attr("for", "Motor" + cn)
        clone.find('[for="Heater"]').attr("for", "Heater" + cn)
        clone.find('[for="Control"]').attr("for", "Control" + cn)
        clone.find('[for="Safety"]').attr("for", "Safety" + cn)
        clone.find('[for="Compressor"]').attr("for", "Compressor" + cn)
        clone.find('[for="Tmp"]').attr("for", "Tmp" + cn)
        clone.find('#uploadBeforeHolder').html(' <input id="PicturesBeforeFix' + cn + '" name="PicturesBeforeFix" class="file fa-2x fileImage " type="file" multiple="true" style="height:50px;">' + fielUploadValidationHolder)
        clone.find('#uploadAftereHolder').html(' <input id="PicturesAfterFix' + cn + '" name="PicturesBeforeFix" class="file fa-2x fileImage " type="file" multiple="true" style="height:50px;">' + fielUploadValidationHolder)

        clone.appendTo('#mainEquHolder');

        initFileInput("PicturesBeforeFix" + cn);
        initFileInput("PicturesAfterFix" + cn);
    })

    $('body').on('click', '.removeForm', function () {
        cn--;
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

});