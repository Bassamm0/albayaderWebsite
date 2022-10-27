﻿$(document).ready(function () {

    const arole = $('#Arole').val();


    const jtoken = $('#utoken').val();
    $('#startDate').datetimepicker({
        format: 'DD-MM-yyyy HH:mm',
        icon: { time: 'far fa-clock' }
    });
    $('#endDate').datetimepicker({
        format: 'DD-MM-yyyy HH:mm',
        icon: { time: 'far fa-clock' }
    });

    $('#newDateChange').datetimepicker({
        format: 'DD-MM-yyyy'
    });

    const APIURL = $('#APIURI').val();

    $('.select2').select2();

    var table = $("#DrasftTbl").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false, "ordering": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print"]
    }).buttons().container().appendTo('#DrasftTbl_wrapper .col-md-6:eq(0)');


    
    

    $('body').on('click', '#DateSearch', function () {
       
        if ($("#FilterDate").valid()) {
            $('#FilterDate').submit();
            $('#actionType').val('filter')
        }


    })
    $('body').on('click', '#DateReset', function () {

       // if ($('#actionType').val() == 'filter') {
            $('#actionType').val('reset')
            $('#FilterDate').submit();
      //  } 
   


    })

    $('#FilterDate').validate({

        rules: {
            endDate: { greaterThan: "#startDate" }

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