
 
$(document).ready(function () {


    $('#lognBtn').click(function () {
       
        if ($("#loginForm").valid()) {
            $('#loginForm').submit();
        }
    })


    $('#loginForm').validate({
        rules: {
            LoginEmailName: {
                required: true,
                email: true,
            },
            LoginPasswordName: {
                required: true,
                minlength: 8
            },
          
        },
        messages: {
            LoginEmailName: {
                required: "Please enter a email address",
                email: "Please enter a valid email address"
            },
            LoginPasswordName: {
                required: "Please provide a password",
                minlength: "Your password must be at least 8 characters long"
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