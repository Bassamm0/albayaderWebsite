

$(document).ready(function () {


    $('#SaveCompany').click(function () {

        if ($("#CompanyForm").valid()) {
           // $('#loginForm').submit();
        }
    })


    $('#CompanyForm').validate({
        rules: {
            CompanyName: {
                required: true,
                maxlength: 50,
            },
           
            street: {
                maxlength: 50,
            },
            streetno: {
                maxlength: 50,
            },
            tel: {
                digits:true,
                minlength: 11,
                maxlength: 11
            },
            fax: {
                digits: true,
                minlength: 11,
                maxlength: 11
            },
            street: {
                maxlength: 50,
            },

        },
        //messages: {
        //    LoginEmailName: {
        //        required: "Please enter a email address",
        //        email: "Please enter a valid email address"
        //    },
        //    LoginPasswordName: {
        //        required: "Please provide a password",
        //        minlength: "Your password must be at least 8 characters long"
        //    },

        //},
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