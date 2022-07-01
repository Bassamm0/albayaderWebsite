$(document).ready(function () {



    $('.select2').select2();

    // validation 
    $('#SaveBranch').click(function (e) {
 
        if ($("#BranchForm").valid()) {

                $('#BranchForm').submit();
        }
    })


    $('#BranchForm').validate({
        rules: {
            BranchName: {
                required: true,
                maxlength: 50,
            },

            ddEmirates: {
                required: true,
        
            },
            District: {
             
                maxlength: 150,
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