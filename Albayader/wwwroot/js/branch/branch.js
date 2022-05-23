$(document).ready(function () {



    $('body').on('click', '.deleteBtn', function () {

        var branchName = $(this).attr('branchName')
        $('#branchToDeleteName').html(branchName)
        var branchid = $(this).attr('branchid')
        $('#deletedbranchId').val(branchid);
        //branch?id=1&handler=Deletebranch


    })
    $('body').on('click', '#Deletebranch', function () {
      
        var branchid = $("#deletedbranchId").val()
        var url = 'branchs?handler=Deletebranch&id=' + $("#deletedbranchId").val();

        //var formData = new FormData();
        //formData.append("branchId", $("#deletedbranchId").val());

        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8", 
            // dataType: "json",
            data: {},
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (data, status, xhr) {   // success callback function

            },
            error: function (jqXhr, textStatus, errorMessage) { // error callback 
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {

            $('#modal-delete').modal('hide');

            $('.branchHolder[branchid=' + branchid + ']').animate({ 'background': 'yellow' }, 1000).fadeOut(500, function () { $(this).remove(); });
        });



    })



})