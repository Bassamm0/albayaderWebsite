$(document).ready(function (){






    const APIURL = $('#APIURI').val();

    $('.select2').select2();


    //Date range picker
    $('#reservation').daterangepicker()

    $("#example1").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print"]
    }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');


})