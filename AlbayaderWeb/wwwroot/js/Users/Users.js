$(document).ready(function () {

    const APIURL = $('#APIURI').val();

    const jtoken = $('#utoken').val();

    $('body').on('click', '.deleteBtn', function () {

        var UserName = $(this).attr('UserName')
        $('#UserToDeleteName').html(UserName)
        var Userid = $(this).attr('Userid')
        $('#deletedUserId').val(Userid);
        //User?id=1&handler=DeleteUser


    })
    $('body').on('click', '#DeleteUser', function () {

        var Userid = $("#deletedUserId").val()
        var url = 'Users?handler=DeleteUser&id=' + $("#deletedUserId").val();

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
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
                alert('Error: something went wronge please try again later');
            }

        }).done(function () {

            $('#modal-delete').modal('hide');
         
            $('.userHolder[Userid=' + Userid + ']').animate({ 'background': 'yellow' }, 700).fadeOut(500, function () { $(this).remove(); });
            toastr["success"]("User delete successfuly.")
        });



    })




    $('body').on('click', '#ViewUser', function () {
        UserId = $(this).attr('UserId')

        getUserById(UserId);
    })

    function getUserById(UserId) {

        let html = '';
        const uri = APIURL+"User/getUserById";

        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8',
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            body: JSON.stringify({ 'id': parseInt(UserId) })
        })
            .then(response => response.json())
            .then((data) => {
                // trigger model
 
                var dateObject = moment(data.birthday, 'yyyy-mm-D').format('yyyy-mm-DD');
               

                html = ` <li class="list-group-item"><span class='ViewDetailsTit'><img class="logoView" src="uploads/${data.pictureFileName}" /></li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>First Name:</span> ${data.firstName}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Last Name:</span> ${data.lastname}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Email:</span> ${data.email}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Mobile:</span> ${data.mobile}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Tel:</span> ${data.telephone}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Company:</span> ${data.companyName}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Branch:</span> ${data.branchName}</li>`

                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Country:</span> ${data.residentContry}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Nationality:</span> ${data.nationalityName}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>City:</span> ${data.city}</li>`
                    + ` <li class="list-group-item"><span class='ViewDetailsTit'>Birthday:</span> ${dateObject}</li>`

                $('#UserDetails').html(html)
            })
            .catch(error => console.error('Unable to add item.', error));
    }

})