$(document).ready(function () {
    const APIURL = $('#APIURI').val();
    const UploadUrl = $('#Uploadlocation').val();
    const _ServiceId = $('#serviceid').val()
    const jtoken = $('#utoken').val();

    for (var i = 1; i < parseInt($('#galleryCount').val())+1; i++) {
        var elemId = 'gallery{' + i + '}';
        touchTouch(document.getElementById(elemId).querySelectorAll('.magnifier'));
       

    }
    //light box


    $('#SaveAndComplete').attr('disabled', true);
    $('.js-signature').jqSignature({width: 400,height:250});
    $('#CleanSignature').click(function () {

        $('.js-signature').jqSignature('clearCanvas');
        $('#SaveAndComplete').attr('disabled', true);

    })

    $('#SaveAndComplete').click(function () {
        if ($("#ClientSignatureForm").valid()) {
            var signatureB64 = $('.js-signature').jqSignature('getDataURL');
           
            completeService(signatureB64)
            
        }
    })
    $('.js-signature').on('jq.signature.changed', function () {
        $('#SaveAndComplete').attr('disabled', false);
    });


    function completeService(signatureB64) {


        var clientName = $('#ClientName').val();
        var clientFeedback = $('#clientFeedback').val();

        var url = APIURL + 'service/clientsignature';
        var data = '{"serviceId":' + _ServiceId + ',"supervisourSignature":"' + signatureB64 + '","supervisourName":"' + clientName + '","supervisourFeedback":"' + clientFeedback + '"}'


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
               

              
            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                if (jqXhr.status == 401) {
                    window.location.href = 'Index';
                }
                alert('Error: something went wronge please try again later');

            }

        }).done(function () {
            //$('#modal-delete').modal('hide');
            //$(".deletImage[filename='" + image + "']").parent('div').parent('div').remove();
            toastr["success"]("Service signed by client successfuly.")
   
            window.location.href = " serviceComplet?ServiceId=" + _ServiceId;
        });

    }


    $('#ClientSignatureForm').validate({
        rules: {
            ClientName: {
                required: true,
                maxlength: 50,
                minlength: 3,
            },
            clientFeedback: {
               
                maxlength: 250,
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


})