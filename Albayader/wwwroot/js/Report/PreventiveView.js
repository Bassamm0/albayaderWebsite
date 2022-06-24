$(document).ready(function () {
    const APIURL = $('#APIURI').val();
    const UploadUrl = $('#Uploadlocation').val();
    const _ServiceId = $('#serviceid').val()

    for (var i = 1; i < parseInt($('#galleryCount').val()) + 1; i++) {
        var elemId = 'gallery{' + i + '}';
        touchTouch(document.getElementById(elemId).querySelectorAll('.magnifier'));


    }
    //light box


    

})