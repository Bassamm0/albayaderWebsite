$(document).ready(function () {



    $('.select2').select2();

    // validation 
    $('#SaveMaterialh').click(function (e) {

        if ($("#MaterialForm").valid()) {

            $('#MaterialForm').submit();
        }
    })


    $('#MaterialForm').validate({
        rules: {
            MaterialName: {
                required: true,
                maxlength: 50,
            },

            Price: {
                decimal:true,
                required: true,

            },
            Description: {

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