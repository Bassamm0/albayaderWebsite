$(document).ready(function () {




    // validation 
    $('#SaveUser').click(function (e) {

        if ($("#UserForm").valid()) {

            $('#UserForm').submit();
        }
    })


    $('#UserForm').validate({
        rules: {
            UseryName: {
                required: true,
                maxlength: 50,
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