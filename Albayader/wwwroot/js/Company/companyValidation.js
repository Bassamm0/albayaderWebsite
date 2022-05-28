

$(document).ready(function () {


    $('#SaveCompany,#UpdateCompany').click(function (e) {
        $('#uploadError').html('')
        if ($("#CompanyForm").valid()) {
            var _logoFile = $("#logoFile")[0].files[0];
            if ($("#logoFile")[0].files[0] != null) {
            
                $('#uploadError').html('Please upload or remove the selected file before save')
                e.preventDefault();
                return;
            } else {

                $('#CompanyForm').submit();
            }
           
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
                minlength: 10,
                maxlength: 15
            },
            fax: {
                digits: true,
                minlength: 10,
                maxlength: 15
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