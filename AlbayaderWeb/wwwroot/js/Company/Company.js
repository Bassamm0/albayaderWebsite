$(document).ready(function () {

    const APIURL = $('#APIURI').val();
    const jtoken = $('#utoken').val();
    const uploadurl = $('#UPLOADURL').val();

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
                if (jqXhr.status == 401) {
                    window.location.href = 'Index';
                }
              alert('Error: something went wronge please try again later');
            }
           
        }).done(function () {
         
            $('#modal-delete').modal('hide');
            
            $('.companyHolder[companyid=' + companyid + ']').animate({ 'background': 'yellow' }, 700).fadeOut(500, function () { $(this).remove(); });

            toastr["success"]("Company delete successfuly.")
          });
          
  
     
    })


    $('body').on('click', '#ViewCompany', function (){
        companyId = $(this).attr('companyId')
       
        getCompanyById(companyId);
    })

    function getCompanyById(companyId) {
        let html = '';
        const uri = APIURL+"company/getCompanyById";

        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8',
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            body: JSON.stringify({ 'id':parseInt(companyId)})
        })
            .then(response => response.json())
            .then((data) => {
               // trigger model
                console.log(data)
               
                html = ` <li class="list-group-item"><span class='ViewDetailsTit'><img class="" style='max-width: 300px;'

                        src="${uploadurl}${data.companyLogo}" /></li>`
                 + ` <li class="list-group-item"><span class='ViewDetailsTit'>Name:</span> ${data.name}</li>`
                 + ` <li class="list-group-item"><span class='ViewDetailsTit'>Country:</span> ${data.countryName}</li>`
                     + ` <li class="list-group-item"><span class='ViewDetailsTit'>Tel:</span> ${data.telephone}</li>`
                     + ` <li class="list-group-item"><span class='ViewDetailsTit'>Fax:</span> ${data.fax}</li>`
                 + ` <li class="list-group-item"><span class='ViewDetailsTit'>City:</span> ${data.city}</li>`
          
                     + ` <li class="list-group-item"><span class='ViewDetailsTit'>Street:</span> ${data.street}</li>`
                     + ` <li class="list-group-item"><span class='ViewDetailsTit'>Street No.:</span> ${data.streetNo}</li>`
                     + ` <li class="list-group-item"><span class='ViewDetailsTit'>Latitude:</span> ${data.latitude}</li>`
                     + ` <li class="list-group-item"><span class='ViewDetailsTit'>Longitude:</span> ${data.longitude}</li>`
               
                $('#CompanyDetails').html(html)
            })
            .catch(error => console.error('Unable to add item.', error));
    }

})