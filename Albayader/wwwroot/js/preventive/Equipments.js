$(document).ready(function () {

    const APIURL = $('#APIURI').val();

    GetEquipments();
    function GetEquipments() {
      
        $("#Equipments1").html('')
        $.ajax({
            url: APIURL+"data/getEquipments",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
               
                $('#Equipments1').append('<option value="">Select  Equipments  ...</option>')
                 for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].name);
                     val = arrUpdates[i].equipmentId;
                      populate(text, val, '#Equipments1');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#Equipments1').append('<option value="">Data Not Loaded  ...</option>')
                 console.log('Error in Operation');
            }
        });

    }



    GetMaterials();
    function GetMaterials() {
        $("#MaterialUsed1").html('')
        $("#Rquiredmaterials1").html('')
        $.ajax({
            url: APIURL +"data/getmaterials",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;

                //$('#MaterialUsed').append('<option value="">Select  Materials Used  ...</option>')
                //$('#Rquiredmaterials').append('<option value="">Select  Materials Required  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].materialName);
                    val = arrUpdates[i].materialId;
                    populate(text, val, '#MaterialUsed1');
                    populate(text, val, '#Rquiredmaterials1');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#MaterialUsed1').append('<option value="">Data Not Loaded  ...</option>')
                $('#Rquiredmaterials1').append('<option value="">Data Not Loaded  ...</option>')
                console.log('Error in Operation');
            }
        });

    }

    function populate(text, val, controlId) {
        $(controlId).append('<option value=' + val + '>' + text + '</option>');

    }
})