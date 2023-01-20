



$(document).ready(function () {
    const jtoken = $('#utoken').val();
    const APIURL = $('#APIURI').val();

    $('#export').click(function () {

        exportexcel()
    })

    function exportexcel() {

        let html = '';
        const uri = APIURL + "service/exportexcel";

        //fetch(uri, {
        //    method: 'POST',
        //    headers: {
        //        'Accept': 'application/json',
        //        'Content-Type': 'application/json; charset=utf-8',
        //        RequestVerificationToken:
        //            $('input:hidden[name="__RequestVerificationToken"]').val(),
        //        Authorization: 'Bearer ' + jtoken,
        //    },
        //    body:{}
        //})
        //    .then(response => response.json())
        //    .then((data) => {
        //        // trigger model
        //        console.log(data)

        //    })
        //    .catch(error => console.error('Unable to add item.', error));



        $.ajax({
            type: "POST",
            url: uri, //call your controller and action
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
        }).done(function (data) {
            alert(data)
            var response = JSON.parse(data);
           
            window.location = '/?fileGuid=' + response.FileGuid
                + '&filename=' + response.FileName;

        });

 
    }
})