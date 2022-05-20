$(document).ready(function () {



    $('body').on('click', '.deleteBtn', function () {

        var companyName = $(this).attr('CompanyName')
        $('#companyToDeleteName').html(companyName)

    })

})