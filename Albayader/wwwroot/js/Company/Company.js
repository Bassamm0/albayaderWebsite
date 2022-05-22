$(document).ready(function () {



    $('body').on('click', '.deleteBtn', function () {

        var companyName = $(this).attr('CompanyName')
        $('#companyToDeleteName').html(companyName)
        var companyid = $(this).attr('companyid')
        $('#deletedCompanyId').val(companyid);
        //Company?id=1&handler=DeleteCompany

       
    })
    $('body').on('click', '#DeleteCompany', function () {

        var companyid = $("#deletedCompanyId").val()       
        var url = 'Company?handler=DeleteCompany&id=' + $("#deletedCompanyId").val();

        //var formData = new FormData();
        //formData.append("companyId", $("#deletedCompanyId").val());
       
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
            
            $('.companyHolder[companyid=' + companyid + ']').animate({ 'background': 'yellow' }, 1000).fadeOut(500, function () { $(this).remove(); });
          });
          
  
     
    })


    //function deletItem() {
    //    const uri = 'Company?handler=DeleteCompany&id=' + $("#deletedCompanyId").val();

    //    fetch(uri, {
    //        method: 'POST',
    //        headers: {
    //            'Accept': 'application/json',
    //            'Content-Type': 'application/json',
    //            RequestVerificationToken:
    //             $('input:hidden[name="__RequestVerificationToken"]').val()
    //        },
    //        body: JSON.stringify($("#deletedCompanyId").val())
    //    })
    //        .then(response => response.json())
    //        .then(() => {
    //            alert('')
    //        })
    //        .catch(error => console.error('Unable to add item.', error));
    //}

})