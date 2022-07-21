$(document).ready(function () {


    const APIURL = $('#APIURI').val();
    const UploadUrl = $('#Uploadlocation').val();
    const role = $('#uRol').val();
    var jtoken = $('#utoken').val();
   

    if (role.toLowerCase() == 'administrator' || role.toLowerCase() == 'manager' || role.toLowerCase() == 'client manager') {
        var year = new Date().getFullYear().toString();
        initDashbaord(year)
     
    }
    $('body').on("change", "#ddyear", function () {
        pieChart.destroy();
        lineChart.destroy();
        donutChart.destroy();
        ctxbarChart.destroy();
        correctivetype.destroy();
        CorrectiveTypePerBranch.destroy();

        year = $(this).val()
        initDashbaord(year)
       
    });
 

    function initDashbaord(year) {


      
      
        $.ajax({
            type: "POST",
            url: APIURL + "dashboard/dashboarddata",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ 'year': year }),
            crossDomain: true,
           
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
              , Authorization: 'Bearer '+ jtoken,
              
            },
            success: function (data, textStatus, xhr) {
                //console.log(xhr.status);
                //console.log(data);
        
                PreventiveCorectivePie(data.preventiveCount, data.correctiveCount)
                preventiveCoreectivePerMonth(data.preventMonth, data.correctiveMonth)

                allServiceMonth(data.allServiceMonth)
                
                brancService(data.preventiveBranch, data.correctiveBranch)

                visitTypeMonth(data.lsservicePerMonthVist)
                serviceTypeBranchs(data.lsservicePerBranchVisit)

                $('#preventiveCount').html('<h3>' + data.preventiveCount+'</h3> <p>Preventive Service</p>')
                $('#correctiveCount').html('<h3>' + data.correctiveCount + '</h3> <p>Corrective Service</p>')
                $('#otherCount').html('<h3>' + data.otherCount + '</h3> <p>Other Service</p>')
                $('#branchCount').html('<h3>' + data.branchCount + '</h3> <p>Branches</p>')

            },

            error: function (xhr, textStatus, errorThrown) {

                console.log('Error in Operation');
    
                if (xhr.status == 401) {
                    window.location.href = 'Index';
                }
            },
             complete: function (xhr, textStatus) {
                console.log(xhr.status);
            }
        });
    }

   
    var pieChart;

    function PreventiveCorectivePie(Preventive, Corrective) {
       pieChart=  new Chart(document.getElementById("areaChart"), {
            type: 'pie',
            data: {
                labels: ["Preventive", "Corrective"],
                datasets: [{
                    label: "Services ",
                    backgroundColor: ["rgba(76, 203, 133, 0.5)", "rgba(138, 19, 250, 0.5)"],
                    data: [Preventive, Corrective]
                }]
            },
            options: {
                title: {
                    display: true,
                    text: 'Preventive  vs Corrective'
                }
            }
        });


    }

    


    //-------------
    //- LINE CHART -
    //--------------
    var lineChart;

    function preventiveCoreectivePerMonth(PreveData,CorrectiveData) {

        let preMonths = []
        let preData = []
        
        for (var i = 0; i < PreveData.length; i++) {
            preMonths[i] = PreveData[i].monthName
            preData[i] = PreveData[i].value

        }
        let corMonths = []
        let corData = []
        for (var i = 0; i < CorrectiveData.length; i++) {
            corMonths[i] = CorrectiveData[i].monthName
            corData[i] = CorrectiveData[i].value

        }


         lineChart=  new Chart(document.getElementById("lineChart"), {
            type: 'bar',
            data: {
                labels: preMonths.length > corMonths.length ? preMonths : corMonths,
                datasets: [
                    {
                        label: "Preventive",
                        backgroundColor: 'rgba(76, 203, 133, 0.2)',
                        borderColor: 'rgba(76, 203, 133, 1)',
                        borderWidth: 1,
                        data: preData
                    }, {
                        label: "Corrective",
                        backgroundColor: 'rgba(138, 19, 250, 0.2)',
                        borderColor: 'rgba(138, 19, 250, 1)',
                        borderWidth: 1,
                        data: corData
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Services per Month by Type'
                }
            }
        });
    }
   
    //-------------
    //- DONUT CHART -
    //-------------
    // Get context with jQuery - using jQuery's .get() method.

    var donutChart
    function allServiceMonth(ServiceData) {


        let SMonths = []
        let SData = []

        for (var i = 0; i < ServiceData.length; i++) {
            SMonths[i] = ServiceData[i].monthName
            SData[i] = ServiceData[i].value

        }

         donutChart= new Chart(document.getElementById("donutChart"), {
            type: 'bar',
            data: {
                labels: SMonths,
                datasets: [
                    {
                        label: "Services",
                        backgroundColor: 'rgba(54, 162, 235, 0.2)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 1,

                        data: SData
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Services per Month'
                }
            }
        });

    }

    


    //-------------
    //- BAR CHART -
    //-------------

    var ctxbarChart;
    let finalbranchs = [];

    let preBranch = []

    let corBranch = []
    function brancService(preventiveBranchData,correctiveBranchData) {


        if (preventiveBranchData == null && correctiveBranchData == null) {
            return;
        }

    
        if (preventiveBranchData != null) {
            for (var i = 0; i < preventiveBranchData.length; i++) {
                preBranch[i] = preventiveBranchData[i].branchName
            }
        }

        if (correctiveBranchData != null) {
            for (var i = 0; i < correctiveBranchData.length; i++) {
                corBranch[i] = correctiveBranchData[i].branchName
            }
        }

         finalbranchs = preBranch.concat(corBranch.filter((item) => preBranch.indexOf(item) < 0));
       
      
  
        let prevFinal = [];
        let preBack = []
        let preBorder = []
        isExist = false;

        for (var i = 0; i < finalbranchs.length; i++) {
            isExist = false;
            for (var j = 0; j < preventiveBranchData.length; j++) {
            
                if (finalbranchs[i] == preventiveBranchData[j].branchName) {
                  
                    prevFinal.push(preventiveBranchData[j].value);
                    isExist = true;
                }
            }
            if (!isExist) {
                prevFinal.push(0);
                isExist = false;
            }
            preBack[i] = 'rgba(76, 203, 133, 0.2)'
            preBorder[i] = 'rgba(76, 203, 133, 1)'
        }

        let corFinal = [];
     
        let corBack = []
        let corBorder = []

       
        for (var i = 0; i < finalbranchs.length; i++) {
            isExist = false;
            for (var j = 0; j < correctiveBranchData.length; j++) {
                if (finalbranchs[i] == correctiveBranchData[j].branchName) {
                   
                    corFinal.push(correctiveBranchData[j].value);
                    isExist = true;
                }
            }
            if (!isExist) {
                corFinal.push(0);
                isExist = false;
            }
            corBack[i] = 'rgba(138, 19, 250, 0.2)'
            corBorder[i] = 'rgba(138, 19, 250, 1)'
        }

       
        const data = {
            labels: finalbranchs,
            datasets: [{
                axis: 'y',
                label: 'Preventive',
                data: prevFinal,
                fill: false,
                backgroundColor: preBack,
                borderColor: preBorder,
                borderWidth: 1
            },
                {
                    axis: 'y',
                    label: 'Corrective',
                    data: corFinal,
                    fill: false,
                    backgroundColor: corBack,
                    borderColor: corBorder,
                    borderWidth: 1
                }
            ]
        };


        ctx = document.getElementById('barChart').getContext('2d');
        ctxbarChart= new Chart(ctx, {
                type: 'bar',
                data,
                options: {
                    indexAxis: 'y',
                }
         
        });
    }

   //corrective by month and type

    let Warranty = [];
    let Repairing = [];
    let Commissioning = [];
    let Installation = [];
    let AMC = [];
    let Emergency = [];
    let arrayType = ["Warranty", "Repairing", "Commissioning and Startup", "Installation", "AMC", "Emergency"];
    var correctivetype;
    function visitTypeMonth(lsservicePerMonthVist) {

         Warranty = [];
         Repairing = [];
         Commissioning = [];
         Installation = [];
         AMC = [];
         Emergency = [];


        if (lsservicePerMonthVist == null ) {
            return;
        }

        let months = ["Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec"]
       
       
        for (var i = 0; i < arrayType.length; i++) {
            switch (i) {
                case 0:
                    createTypesValues(months, lsservicePerMonthVist, Warranty, arrayType[i]);
                    break;
                case 1:
                    createTypesValues(months, lsservicePerMonthVist, Repairing, arrayType[i]);
                    break;
                case 2:
                    createTypesValues(months, lsservicePerMonthVist, Commissioning, arrayType[i]);
                    break;
                case 3:
                    createTypesValues(months, lsservicePerMonthVist, Installation, arrayType[i]);
                    break;
                case 4:
                    createTypesValues(months, lsservicePerMonthVist, AMC, arrayType[i]);
                    break;
                case 5:
                    createTypesValues(months, lsservicePerMonthVist, Emergency, arrayType[i]);
                    break;
            }
          

        }
       
         ctxa = document.getElementById('CorrectiveTypePerMonth').getContext('2d');
        correctivetype= new Chart(ctxa, {
            type: 'bar',
            data: {
                labels: ["Jan",
                    "Feb",
                    "Mar",
                    "Apr",
                    "May",
                    "Jun",
                    "Jul",
                    "Aug",
                    "Sep",
                    "Oct",
                    "Nov",
                    "Dec"],
                datasets: [
                    {
                        label: "Warranty",
                        backgroundColor: 'rgba(54, 162, 235, 0.2)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 1,
                        data: Warranty
                    }, {
                        label: "Repairing",
                        backgroundColor: 'rgba(76, 203, 133, 0.2)',
                        borderColor: 'rgba(76, 203, 133, 1)',
                        borderWidth: 1,
                        data: Repairing
                    }, {
                        label: "Commissioning and Startup",
                        backgroundColor: 'rgba(111, 66, 193, 0.2)',
                        borderColor: 'rgba((111, 66, 193, 1)',
                        borderWidth: 1,
                        data: Commissioning
                    }, {
                        label: "Installation",
                        backgroundColor: 'rgba(9, 168, 200, 0.2)',
                        borderColor: 'rgba(9, 168, 200, 1)',
                        borderWidth: 1,
                        data: Installation
                    }, {
                        label: "AMC",
                        backgroundColor: 'rgba(214, 51, 132, 0.2)',
                        borderColor: 'rgba(214, 51, 132, 1)',
                        borderWidth: 1,
                        data: AMC
                    }, {
                        label: "Emergency",
                        backgroundColor: 'rgba(200, 53, 0, 0.2)',
                        borderColor: 'rgba(200, 53, 0, 1)',
                        borderWidth: 1,
                        data: Emergency
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Population growth (millions)'
                }
            }
        });
    }

    function createTypesValues(months, lsservicePerMonthVist, arryobj,type) {
        
        var found = false;
        for (var i = 0; i < months.length; i++) {
            found = false;
            for (var j = 0; j < lsservicePerMonthVist.length; j++) {
                if (months[i] == lsservicePerMonthVist[j].monthName) {
                    if (lsservicePerMonthVist[j].vistTypeName == type) {
                        arryobj.push(lsservicePerMonthVist[j].value)
                        found = true;

                    }
                }
            }
            if (!found) {
                arryobj.push(0)
                found = false;
            }
        }
    }
    // end

    let ArrWarranty = [];
    let ArrRepairing = [];
    let ArrCommissioning = [];
    let ArrInstallation = [];
    let ArrAMC = [];
    let ArrEmergency = [];
    var CorrectiveTypePerBranch;
    // type branchs
    function serviceTypeBranchs(lsservicePerBranchVisit) {


         ArrWarranty = [];
         ArrRepairing = [];
         ArrCommissioning = [];
         ArrInstallation = [];
         ArrAMC = [];
         ArrEmergency = [];
        if (lsservicePerBranchVisit == null) {
            return;
        }
        //console.log(corBranch)

      
        for (var i = 0; i < arrayType.length; i++) {
            switch (i) {
                case 0:
                    constructBranchType( lsservicePerBranchVisit, ArrWarranty, arrayType[i]);
                    break;
                case 1:
                    constructBranchType( lsservicePerBranchVisit, ArrRepairing, arrayType[i]);
                    break;
                case 2:
                    constructBranchType( lsservicePerBranchVisit, ArrCommissioning, arrayType[i]);
                    break;
                case 3:
                    constructBranchType( lsservicePerBranchVisit, ArrInstallation, arrayType[i]);
                    break;
                case 4:
                    constructBranchType( lsservicePerBranchVisit, ArrAMC, arrayType[i]);
                    break;
                case 5:
                    constructBranchType( lsservicePerBranchVisit, ArrEmergency, arrayType[i]);
                    break;
            }


        }



      


        const data = {
            labels: corBranch,
            datasets: [{
                axis: 'y',
                label: 'Warranty',
                data: ArrWarranty,
                fill: false,
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1,
            },
            {
                axis: 'y',
                label: 'Repairing',
                data: ArrRepairing,
                fill: false,
                backgroundColor: 'rgba(76, 203, 133, 0.2)',
                borderColor: 'rgba(76, 203, 133, 1)',
                borderWidth: 1,
                }
                ,
                {
                    axis: 'y',
                    label: 'Commissioning and Startup',
                    data: ArrCommissioning,
                    fill: false,
                    backgroundColor: 'rgba(111, 66, 193, 0.2)',
                    borderColor: 'rgba((111, 66, 193, 1)',
                    borderWidth: 1,
                }
                ,
                {
                    axis: 'y',
                    label: 'Installation',
                    data: ArrInstallation,
                    fill: false,
                    backgroundColor: 'rgba(9, 168, 200, 0.2)',
                    borderColor: 'rgba(9, 168, 200, 1)',
                    borderWidth: 1,
                }
                ,
                {
                    axis: 'y',
                    label: 'AMC',
                    data: ArrAMC,
                    fill: false,
                    backgroundColor: 'rgba(214, 51, 132, 0.2)',
                    borderColor: 'rgba(214, 51, 132, 1)',
                    borderWidth: 1,
                }
                ,
                {
                    axis: 'y',
                    label: 'Emergency',
                    data: ArrEmergency,
                    fill: false,
                    backgroundColor: 'rgba(200, 53, 0, 0.2)',
                    borderColor: 'rgba(200, 53, 0, 1)',
                    borderWidth: 1,
                }
            ]
        };


         ctxb = document.getElementById('CorrectiveTypePerBranch').getContext('2d');
        CorrectiveTypePerBranch=  new Chart(ctxb, {
            type: 'bar',
            data,
            options: {
                indexAxis: 'y',
            }

        });
    }

    function constructBranchType(lsservicePerBranchVisit, arryobj, type){

        isTypeExist = false;

        for (var b = 0; b < corBranch.length; b++) {
            isTypeExist = false;
            for (var j = 0; j < lsservicePerBranchVisit.length; j++) {

                if (corBranch[b] == lsservicePerBranchVisit[j].branchName && lsservicePerBranchVisit[j].vistTypeName == type) {
                    arryobj.push(lsservicePerBranchVisit[j].value)
                    isTypeExist = true;
                }
            }
            if (!isTypeExist) {
                arryobj.push(0)
                isTypeExist = false;
            }

        }

    }

})