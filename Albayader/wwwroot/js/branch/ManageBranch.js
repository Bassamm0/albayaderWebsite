$(document).ready(function () {




    // validation 
    $('#SaveBranch').click(function (e) {
 
        if ($("#BranchForm").valid()) {

                $('#BranchForm').submit();
        }
    })


    $('#BranchForm').validate({
        rules: {
            BranchyName: {
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