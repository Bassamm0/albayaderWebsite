$(document).ready(function (){


    const jtoken = $('#utoken').val();

    $('#startDate').datetimepicker({
        format: 'DD/MM/yyyy'
    });
    $('#EndtDate').datetimepicker({
        format: 'DD/MM/yyyy'
    });



    const APIURL = $('#APIURI').val();

    $('.select2').select2();


    //Date range picker
    $('#reservation').daterangepicker()

    var table=$("#DrasftTbl").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print"]
    }).buttons().container().appendTo('#DrasftTbl_wrapper .col-md-6:eq(0)');

    $(document.body).on("change", "#ddBranch", function (e) {
        var selectedText = $(this).find("option:selected").text();
        val = $(this).val();
        filterColumn(selectedText,val,1);
    });
    $(document.body).on("change", "#ddServiceType", function (e) {
        var selectedText = $(this).find("option:selected").text();
        val = $(this).val();
        filterColumn(selectedText,val, 2);
    });

    function filterColumn(text,val,column) {
        console.log(val);
        if (val != '') {
            $('#DrasftTbl')
                .DataTable()
                .column(column)
                .search(
                    text,
                )
                .draw();
        } else {
            $('#DrasftTbl')
                .DataTable()
                .column(column)
                .search(
                    '',
                )
                .draw();
        }
       
    }
   




    getBranches()

    function getBranches() {

        $("#ddBranch").html('')
        $.ajax({
            type: "GET",
            url: APIURL +"branch/all",
            contentType: "application/json; charset=utf-8",
           /* async: false,*/
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                console.log(arrUpdates)
                $('#ddBranch').append('<option value="">All Branch  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].branchName);
                    val = arrUpdates[i].branchId;
                    populate(text, val, '#ddBranch');

                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#ddBranch').append('<option value="">Data Not Loaded  ...</option>')
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
                console.log('Error in Operation');
            }
        });

    }




    function populate(text, val, controlId) {
        $(controlId).append('<option value=' + val + '>' + text + '</option>');

    }



    $('#FilterDate').validate({
     

 
        rules: {
            EndtDate: { greaterThan: "#startDate" }

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