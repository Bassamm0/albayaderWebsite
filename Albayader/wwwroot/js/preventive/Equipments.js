$(document).ready(function () {

    $('.select2').select2();

    GetEquipments();
    function GetEquipments() {
      
        $("#Equipments").html('')
        $.ajax({
            url: "https://localhost:7174/api/data/getEquipments",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
               
                $('#Equipments').append('<option value="">Select  Equipments  ...</option>')
                 for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].name);
                     val = arrUpdates[i].equipmentId;
                      populate(text, val, '#Equipments');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#Equipments').append('<option value="">Data Not Loaded  ...</option>')
                 console.log('Error in Operation');
            }
        });

    }



    GetMaterials();
    function GetMaterials() {
        $("#MaterialUsed").html('')
        $("#Rquiredmaterials").html('')
        $.ajax({
            url: "https://localhost:7174/api/data/getmaterials",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;

                $('#MaterialUsed').append('<option value="">Select  Materials Used  ...</option>')
                $('#Rquiredmaterials').append('<option value="">Select  Materials Required  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].materialName);
                    val = arrUpdates[i].materialId;
                    populate(text, val, '#MaterialUsed');
                    populate(text, val, '#Rquiredmaterials');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#MaterialUsed').append('<option value="">Data Not Loaded  ...</option>')
                $('#Rquiredmaterials').append('<option value="">Data Not Loaded  ...</option>')
                console.log('Error in Operation');
            }
        });

    }

    function populate(text, val, controlId) {
        $(controlId).append('<option value=' + val + '>' + text + '</option>');

    }
})