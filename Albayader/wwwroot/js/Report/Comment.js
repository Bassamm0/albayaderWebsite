$(document).ready(function () {


    const APIURL = $('#APIURI').val();

    $('body').on('click', '.viewComment', function () {

        ServiceId = $(this).attr('serviceid');
        getComments(ServiceId)
    })


    function getComments(ServiceId) {

        let html = '';
        const uri = APIURL + "servicecomment/all";

        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8',
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            body: JSON.stringify({ 'serviceid': parseInt(ServiceId) })
        })
            .then(response => response.json())
            .then((data) => {
                // trigger model
                console.log(data)


                for (var i = 0; i < data.length; i++) {
                    html += ' <div class="card-comment">'
                        + '  <div class="comment-text">'
                        + '<span class="username">' + data[i].fullName
                        + '<span class="text-muted float-right">' + moment(data[i].commentDate).format('DD-MM-YYYY hh:mm:ss')+'</span>'
                        + '</span>'
                        + data[i].comment
                        + ' </div>'
                        + ' </div>'
                }
               
           


                $('#commentList').html(html)
            })
            .catch(error => console.error('Unable to add item.', error));
    }

    $('body').on('click', '.adddCommentlnk', function () {
        $('#serviceComment').val('');
        ServiceId = $(this).attr('serviceid');
        $('#addComment').attr('commentserviceid', ServiceId)
        $('#servicetextId').text(ServiceId)

    })

    $('body').on('click', '#addComment', function () {
        ServiceId = $(this).attr('commentserviceid');

        $("#Comments").submit(function (e) {
            return false;
        });
        if (!$("#Comments").valid()) {

            return false;
        }
       
        addComment(ServiceId)
    })

    function addComment() {
        const uri = APIURL + "servicecomment/add";

        var comment = $('#serviceComment').val();
        let data = { 'serviceId': parseInt(ServiceId), 'comment':comment}
        fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json; charset=utf-8',
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            body: JSON.stringify(data)
        })
            .then(response => response.json())
            .then((data) => {
         
                console.log(data)
                toastr["success"]("comment added successfuly.")
                $("#closeaddCom").click();

               
            })
            .catch(error => console.error('Unable to add item.', error));

    }




    $('#Comments').validate({
        rules: {
            serviceComment: {
                required: true,
                maxlength: 250,
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
})