$(document).ready(function () {


    const APIURL = $('#APIURI').val();
    let loadedmaterialCount = $('#materialcount').val();
  
    let removedElem;
    let alloptions;
                  
                       
    var items = [];
    for (var i = 0; i < loadedmaterialCount; i++) {
        items.push(i);
    }
    console.log(items)

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
            '<textarea class="form-control descClass" id="description' + loadedmaterialCount + '" rows="3">' +
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
            serviceid: {
                required: true,
                digits: true,
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