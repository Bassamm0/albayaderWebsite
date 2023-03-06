$(document).ready(function () {

    $('#recover').click(function (e) {

        if (!PasswordRequirement) {



            $('#pswd_info').css({
                top: $('#passwordHolder').position().top + 50,
                right: 0
            });
            $('#pswd_info').show();
            e.preventDefault();
            return;
        }

        if ($("#recoverPasswordForm").valid()) {

            $('#recoverPasswordForm').submit();

        } else {
            e.preventDefault();
            return;
        }
    })

    $('#recoverPasswordForm').validate({
        rules: {
           
            password: {
                required: true,
            },
            passwordconf: {
                required: true,
                equalTo: "#password"
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


    //********************* password strong validations

    var linkPosition = $('#passwordHolder').position();

    if ($('#passwordHolder').length) {
        $('#pswd_info').css({
            top: $('#passwordHolder').position().top
        });
        $(window).scroll(function () {
            $('#pswd_info').css({
                top: $('#passwordHolder').position().top + 80
            });
        })
    }


    var PasswordRequirement = false;

    $('#password').keyup(function () {
        PasswordRequirement = false;
        var pswd = $(this).val();

        //validate the length
        if (pswd.length < 8) {
            $('#length').removeClass('valid').addClass('invalid');
            PasswordRequirement = true;
        } else {
            $('#length').removeClass('invalid').addClass('valid');
            PasswordRequirement = false;

        }
        //validate letter
        if (pswd.match(/[A-z]/)) {
            $('#letter').removeClass('invalid').addClass('valid');
            PasswordRequirement = true;
        } else {
            $('#letter').removeClass('valid').addClass('invalid');
            PasswordRequirement = false;
        }

        //validate capital letter
        if (pswd.match(/[A-Z]/)) {
            $('#capital').removeClass('invalid').addClass('valid');
            PasswordRequirement = true;
        } else {
            $('#capital').removeClass('valid').addClass('invalid');
            PasswordRequirement = false;
        }

        //validate number
        if (pswd.match(/\d/)) {
            $('#number').removeClass('invalid').addClass('valid');
            PasswordRequirement = true;
        } else {
            $('#number').removeClass('valid').addClass('invalid');
            PasswordRequirement = false;
        }

        //validate Symbol
        if (pswd.match(/[$-/:-?{-~!"^_@#`\[\]]/)) {
            $('#Symbol').removeClass('invalid').addClass('valid');
            PasswordRequirement = true;
        } else {
            $('#Symbol').removeClass('valid').addClass('invalid');
            PasswordRequirement = false;
        }
        

    }).focus(function () {

        
        $('#pswd_info').css({
            top: $('#passwordHolder').position().top + 50,
            right: 0
        });
        $('#pswd_info').show();

    }).blur(function () {
        $('#pswd_info').hide();
    });

    //********************* password strong validations

})