$(document).ready(function () {

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });
    $("body").on("click", "#UploadLogobtn", function () {
    
        var formData = new FormData();
     
        formData.append("files", $("#logoFile")[0].files[0]);
        $.ajax({
            url: 'https://localhost:7174/api/fileupload/upload',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                $("#fileProgress").hide();
                $("#lblMessage").html("<b>" + data + "</b> has been uploaded.");
                $("#uploadedfile").val(data);
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


   // $("body").on("click", "#SaveCompany", function () {

    //    var CLanguageId = $("#ddLanguage").val();
    //    var txtCountry = $("#ddCountry").val();
    //    var ddlIndustryName = $("#ddIndustry").val();
    //    var CompanyCategoryId = $("#ddCompanyCategory").val();
    //    var Name = $("#txtCompanyName").val();
    //    var txtCity = $("#txtCity").val();
    //    var txtStreet = $("#txtStreet").val();
    //    var txtStreetNo = $("#txtStreetNo").val();
    //    var txtTelephone = $("#txtTelephone").val();
    //    var txtFax = $("#txtFax").val();
    //    var txtLatitude = $("#txtLatitude").val();
    //    var txtLongitude = $("#txtLongitude").val();
    //    var txtAltitude = $("#txtAltitude").val();
    //    var txtDescription = $("#txtDescription").val();
    //    var txtAdminEmail = $("#txtAdminEmail").val();
    //    var sMode = $('#hdCompanySmode').val();
    //    if (sMode == "U") {
    //        var companyid = $("#HdCompanyId").val();
    //    }

    //    $.ajax({
    //        type: "POST",
    //        url: "WCFCompanies.svc/InsertCompanies",
    //        data: '{"COID":"' + txtCountry + '","CID":"' + companyid + '","Name":"' + Name + '","INDID":"' + ddlIndustryName + '","Description":"' + txtDescription + '","City":"' + txtCity + '","Street":"' + txtStreet + '","StreetNo":"' + txtStreetNo + '","Telephone":"' + txtTelephone + '","Fax":"' + txtFax + '","Latitude":"' + txtLatitude + '","Longitude":"' + txtLongitude + '","Altitude":"' + txtAltitude + '","CompanyLogo":"' + filecompanylogo + '","AdminEmail":"' + txtAdminEmail + '","CompanyCategoryId":"' + CompanyCategoryId + '","LanguageId":"' + CLanguageId + '","sMode":"' + sMode + '"}',
    //        contentType: "application/json", // content type sent to server
    //        success: ServiceSucceeded,
    //        error: showerror
    //    });

    //    function ServiceSucceeded(msg) {

    //        if (msg.d == 'true') {
    //            if (sMode == "U") {
    //                var theMessage = 'Company  ' + Name + ' updated  Successfully   ';
    //                showSuccessMessage(theMessage);

    //            } else {
    //                var theMessage = 'Company  ' + Name + ' created  Successfully   ';
    //                showSuccessMessage(theMessage);
    //            }
    //            GetAllCompanies();
    //            $('#managerCloseBtn').click();
    //            resetForm('CompanyForm');


    //            $('#fileCompanyLogoHolder').html('');
    //            $('#fileCompanyLogoHolder').html(' <input id="fileCompanyLogo" name="fileCompanyLogo" class="file fa-2x fileImage" type="file" multiple="true">')
    //            $('#fileCompanyLogo').fileinput('refresh',
    //                {
    //                    allowedFileExtensions: ['jpg', 'png', 'gif', 'pdf', 'pmb', 'esp'],
    //                    showUpload: false,
    //                    showCaption: true,
    //                    maxFileSize: 4000,
    //                    browseClass: "btn btn-primary btn-lg",
    //                    previewFileIcon: "<i class='glyphicon glyphicon-king'></i>"
    //                });

    //        } else if (msg.d == 'false') {
    //            var theEMessage = 'Sorry this action  Couldn\'t be completed please contact <a href="mailto:support@smartcamme.com">our support</a>  ';
    //            showErrorMessage(theEMessage);
    //        } else {

    //            showErrorMessage(msg.d);
    //        }

    //    }

    //    function showerror(msg) {


    //    }

    //})

});