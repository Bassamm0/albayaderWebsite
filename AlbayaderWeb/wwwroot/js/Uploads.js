$(document).ready(function () {


    const APIURL = $('#APIURI').val();
    const UPLOADURL = $('#UPLOADURL').val();

     

    $('body').on('click', '.fileinput-upload-button', function () {
        var files = document.getElementById("txtPictureFileName").files
        var formData = new FormData();
        // Loop through files
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            formData.append("files", file);
          
            
            
        }
        formData.append("serviceid", "1");
        $.ajax({
            url: APIURL+'fileupload/UploadServiceImages',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                if (arrUpdates.length > 0) {


                    console.log(arrUpdates);
                    $("#fileProgress").hide();
                    $("#lblMessage").html("<b>" + files.length + "</b>  files has been uploaded successfuly.");
                    $('#uploadError').html('')
                    $('#txtPictureFileName').fileinput('clear');

                    let uploadedFiles = "";
                    for (var i = 0; i < arrUpdates.length; i++) {
                        uploadedFiles += "<div>"
                        uploadedFiles +="<div class='file-preview-frame krajee-default  kv-preview-thumb'>"
                        uploadedFiles += "<div><a href='" + UPLOADURL + arrUpdates[i] + "' target='_blank'><image src='" + UPLOADURL arrUpdates[i] + "' style='width:100px;height:100px' /></a></div>"
                        uploadedFiles += "<div><button type='button' class='btn btn-block btn-info deletImage' filename='"+arrUpdates[i]+"'>Delete</button></div>"
                        uploadedFiles += "</div>"
                       
                        uploadedFiles += "</div>"

                    }

                    $("#thumbHolder").html(uploadedFiles)
                    console.log(uploadedFiles)
                } else {

                    $('#uploadError').html('Some Thing went wrong, please contact the administrator')
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


      
    })
    // upload image 



  




})