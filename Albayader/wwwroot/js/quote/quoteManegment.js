$(document).ready(function () {


    const APIURL = $('#APIURI').val();
    const jtoken = $('#utoken').val();
    const uploadurl = $('#UPLOADURL').val();
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
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
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
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
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
                $('#ddService').append('<option value="0">Select  Service  ...</option>')
                for (var i = 0; i < arrUpdates.length; i++) {
                    text = $.trim('# Ref: '+ arrUpdates[i].serviceId) + ' - Completed On: ' + $.trim(arrUpdates[i].completionDate);
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
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
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
        console.log($('#uploadedfile').val())
        if ($('#uploadedfile').val() == '') {
            $('#uploadError').html('file PDF Quotation is required please upload the PDF quotation before save!')
            e.preventDefault();
            return;
        }
        if (items.length == 0) {
            $('#errorMessage').text("Please add at least one Item");
            return;
        }
        if ($("#quoteForm").valid()) {

            if ($("#logoFile")[0].files[0] != null) {

                $('#uploadError').html('Please upload or remove the selected file before save')
                e.preventDefault();
                return;
            } else {
                $('#quoteForm').submit();
            }
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
            ddBranch: {
                required: true,
               
            },
            ddCompanies: {
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




    $(".custom-file-input").on("change", function () {

        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
        if (fileName.length > 120) {
            alert("file name can't be more than 100 character please select another file");
            return;
        }

        if (fileName != '') {
            $('#RemoveLogobtn').show();
            $('#UploadLogobtn').show();
        } else {
            $('#RemoveLogobtn').hide();
            $('#UploadLogobtn').hide();
        }

    });
    $("body").on("click", "#UploadLogobtn", function () {
        if ($("#logoFile")[0].files[0] == null) {
            $('#uploadError').html('Please select a file to upload.')
            return;

        }

        var formData = new FormData();

        formData.append("files", $("#logoFile")[0].files[0]);
        $.ajax({
            url: APIURL + 'fileupload/upload',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer ' + jtoken,

            },
            success: function (data) {
                $("#fileProgress").hide();
                $("#lblMessage").html("<b>" + data + "</b> has been uploaded.");
                $("#uploadedfile").val(data);
                $('#uploadError').html('')
                $('#logoFile').val('')
                $('#RemoveLogobtn').hide();
                $('#UploadLogobtn').hide();
            },
            xhr: function () {
                var fileXhr = $.ajaxSettings.xhr();
                if (fileXhr.upload) {
                    $("progress").show();
                    fileXhr.upload.addEventListener("progress", function (e) {
                        if (e.lengthComputable) {
                            $("#fileProgress").attr({
                                value: e.loaded,
                                max: e.total
                            });
                        }
                    }, false);
                }
                return fileXhr;
            }
        });
    });



    $("body").on("click", "#RemoveLogobtn", function () {
        $('#logoFile').next('label').html('Select a file');
        $('#logoFile').val('')
        $('#RemoveLogobtn').hide();
        $('#UploadLogobtn').hide();
    })

})