$(document).ready(function () {

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


    $('.select2').select2();

    $('#reportedDate').datetimepicker({
        format: 'L'
    });




});
