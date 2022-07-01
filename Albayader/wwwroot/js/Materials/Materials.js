$(document).ready(function () {

    const APIURL = $('#APIURI').val();
    const jtoken = $('#utoken').val();
    const uploadurl = $('#UPLOADURL').val();
    var obj;

    var table = $("#DrasftTbl").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print"]
    }).buttons().container().appendTo('#DrasftTbl_wrapper .col-md-6:eq(0)');



    $('body').on('click', '.deleteMaterial', function () {

        var materialName = $(this).attr('materialName')
        $('#MaterialToDeleteName').html(materialName)
        var materialid = $(this).attr('materialId')
        $('#deletedMaterialId').val(materialid);
       
        obj = $(this).parents('ul').parents('div').parents('td').parents('tr')
       
        

    })
    $('body').on('click', '#DeleteMaterial', function () {

        
        var url = 'materials?handler=DeleteMaterial&id=' + $("#deletedMaterialId").val();

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

            $('#delClose').click();
            // datatable redraw
            obj.remove()
            table.clear().draw();
            toastr["success"]("Company delete successfuly.")
        });



    })


    $('body').on('click', '.ViewMaterial', function () {
        materialId = $(this).attr('materialId')

        getmaterial(materialId);
    })

    function getmaterial(materialId) {
        console.log(materialId)
        let html = '';
        const uri = APIURL + "material/getmaterial";

        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8',
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            body: JSON.stringify({ 'id': parseInt(materialId) })
        })
            .then(response => response.json())
            .then((data) => {
                // trigger model
                console.log(data)

                html = ` <li class="list-group-item"><span class='ViewDetailsTit'>Name:</span> ${data.materialName}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Price:</span> ${data.price}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Description:</span> ${data.description}</li>`
                  
                $('#materialDetails').html(html)
            })
            .catch(error => console.error('Unable to add item.', error));
    }



});