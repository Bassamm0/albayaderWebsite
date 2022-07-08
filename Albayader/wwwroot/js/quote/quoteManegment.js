$(document).ready(function () {


    const APIURL = $('#APIURI').val();
    const jtoken = $('#utoken').val();
    let loadedmaterialCount = $('#materialcount').val();
  
    let removedElem;
    let alloptions;
                  
                       
    var items = [];
    for (var i = 0; i < loadedmaterialCount; i++) {
        items.push(i);
    }
    console.log(items)



    getCompanies()
    function getCompanies() {

        $("#ddCompanies").html()
        $.ajax({
            type: "GET",
            url: APIURL + "company/all",
            contentType: "application/json; charset=utf-8",
            data: {},
            async: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                console.log(arrUpdates)
                $('#ddCompanies').append('<option value="">Select Client Company  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].name);
                    val = arrUpdates[i].companyID;
                    populate(text, val, '#ddCompanies');

                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#ddCompanies').append('<option value="">Data Not Loaded  ...</option>')
                console.log('Error in Operation');
            }
        });

    }

    $('body').on("change", "#ddCompanies", function () {
        companyId = $('#ddCompanies').val()
        getCompanyBranch(companyId);
    });
    function getCompanyBranch(_companyId) {

        $("#ddBranch").html('')
        $.ajax({
            type: "POST",
            url: APIURL + "branch/companybranchs",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ 'companyid': parseInt(_companyId) }),
            async: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                console.log(arrUpdates)
                $('#ddBranch').append('<option value="">Select  Branch  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].branchName);
                    val = arrUpdates[i].branchId;
                    populate(text, val, '#ddBranch');

                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#ddBranch').append('<option value="">Data Not Loaded  ...</option>')
                console.log('Error in Operation');
            }
        });

    }

    $('body').on("change", "#ddBranch", function () {
        branchId = $('#ddBranch').val()
        getBranchServices(branchId);
    });

    function getBranchServices(branchId) {

        $("#ddService").html('')
        $.ajax({
            type: "POST",
            url: APIURL + "service/completedservicebyBranch",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ 'barnchId': parseInt(branchId) }),
            async: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,
            },
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;
                console.log(arrUpdates)
                $('#ddService').append('<option value="">Select  Service  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].serviceId);
                    val = arrUpdates[i].serviceId;
                    populate(text, val, '#ddService');

                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('#ddService').append('<option value="">Data Not Loaded  ...</option>')
                console.log('Error in Operation');
            }
        });

    }



    $('body').on('click', '.removeitem', function () {

        var qoutedetialsid = $(this).attr('qoutedetialsid');
        removedElem = $('div[qoutedetialsid="' + qoutedetialsid + '"]');

        var elemno = $(this).attr('elemno');

        console.log('value', elemno)
        console.log("before", items)

        var myIndex = items.indexOf(parseInt(elemno));
        console.log("index", myIndex)
        if (myIndex !== -1) {
            items.splice(myIndex, 1);
            $('#itemsids').val(items)
        }

        if (items.length == 0) {
            $('#AddItemMessage').text('Get Started by adding items')
        }
        console.log("after", items)

        deleteMaterialquote()
    })

    function deleteMaterialquote() {

        // after ajax
        removedElem.remove();
         console.log(items.length)
     }

    $('body').on('click', '#addItem', function () {
        addMateriaquotel()
    })

    function addMateriaquotel() {

        loadedmaterialCount = parseInt(loadedmaterialCount) + 1
       
        console.log("before", items)
        items.push(loadedmaterialCount);
        $('#itemsids').val(items)
        createElement(loadedmaterialCount)
        console.log("after", items)
        if (items.length > 0) {
            $('#AddItemMessage').text('')
            $('#errorMessage').text('')

            
        }

    }

    function createElement(loadedmaterialCount) {
        var MaterQuote = '' +
            '<div class="row" id="item' + loadedmaterialCount + '" qoutedetialsid= "' + loadedmaterialCount + '" >' +
            ' <fieldset class="form-group border p-3">' +
            ' <div class="row">' +
            '<div class="col-md-3">' +
            '<div class="form-group">' +
            ' <label>- Material</label>' +
            '<select name="Material' + loadedmaterialCount + '" id="Material' + loadedmaterialCount + '" class="form-control select2 materialLoad" style="width: 100%;" required >' +
            ' </select>' +
            '  </div>' +
            ' </div>' +
            ' <div class="col-md-3">' +
            ' <div class="form-group">' +
            '<label for="lat">Price</label>' +
            ' <input type="text" class="form-control priceClass" name="price' + loadedmaterialCount + '" id="Price' + loadedmaterialCount + '" placeholder="Enter Price" required value="" />' +
            ' </div>' +
            ' </div>' +
            '<div class="col-md-3">' +
            '<div class="form-group">' +
            ' <label for="lat">Qty</label>' +
            '<input type="text" class="form-control qtyClass" name="Qty' + loadedmaterialCount + '" id="Qty' + loadedmaterialCount + '" placeholder="Enter Qty" value="" required />' +
            '</div>' +
            '</div>' +
            ' <div class="col-md-3">' +
            ' <div style="padding: 20px;float:right">' +
            '<button type="button" class="btn btn-default removeitem" elemno="' + loadedmaterialCount + '" qoutedetialsid="' + loadedmaterialCount + '" style="background-color: rgb(250, 245, 245);"><i class="fa-solid fa-xmark"></i></button>' +
            ' </div>' +
            ' </div>' +
            '</div>' +
            ' <div class="col-md-12">' +
            ' <div class="form-group">' +
            '<label for="exampleInputEmail1">Description</label>' +
            '<textarea class="form-control descClass" name="description' + loadedmaterialCount + '"id="description' + loadedmaterialCount + '" rows="3">' +
            ' </textarea>' +
            '</div>' +
            ' </div>' +
            '</fieldset>' +
            ' </div >';
            
        $('#allElementHolder').append(MaterQuote)
        $('#Material' + loadedmaterialCount).html('<option value="">Select  Materials   ...</option>'+alloptions)
        $('#Material' + loadedmaterialCount).select2();
    }
  
    $('.select2').select2();
    GetMaterials();
    function GetMaterials() {
        $(".materialLoad").html('')
        $.ajax({
            url: APIURL + "data/getmaterials",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (data, textStatus, xhr) {
                var arrUpdates = (typeof data) == 'string' ? eval('(' + data + ')') : data;

                $('.materialLoad').append('<option value="">Select  Materials   ...</option>')
                alloptions = '';
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim(arrUpdates[i].materialName);
                    val = arrUpdates[i].materialId;
                    populate(text, val, '.materialLoad');
                 
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                $('.materialLoad').append('<option value="">Data Not Loaded  ...</option>')
                
                console.log('Error in Operation');
            }
        });

    }
 
    function populate(text, val, controlId) {
        $(controlId).append('<option value=' + val + '>' + text + '</option>');
        alloptions += '<option value=' + val + '>' + text + '</option>'
    }

    $('#Savequote').click(function (e) {

        if (items.length == 0) {
            $('#errorMessage').text("Please add at least one Item");
            return;
        }

        if ($("#quoteForm").valid()) {

            $('#quoteForm').submit();
        }
    })


    

    // validation 
    
    jQuery.validator.addClassRules("priceClass", {
        required: true,
        decimal: true,
    });
    jQuery.validator.addClassRules("qtyClass", {
        required: true,
        digits: true,
    });
    jQuery.validator.addClassRules("descClass", {
        maxlength: 250,
    });


    $('#quoteForm').validate({
        rules: {
            ddService: {
                required: true,
               
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