$(document).ready(function () {


    const APIURL = $('#APIURI').val();
    const UploadUrl = $('#Uploadlocation').val();
    const role = $('#uRol').val();
    var jtoken = $('#utoken').val();
    console.log(jtoken)

    if (role.toLowerCase() == 'administrator' || role.toLowerCase() == 'manager' || role.toLowerCase() == 'client manager') {
        initDashbaord()
     
    }
  

    function initDashbaord() {

      
        var year = '2022';

        $.ajax({
            type: "POST",
            url: APIURL + "dashboard/dashboarddata",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ 'year': year }),
            crossDomain: true,
           
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val(),
                Authorization: 'Bearer '+ jtoken,
              
            },
            success: function (data, textStatus, xhr) {
                console.log(data)
                PreventiveCorectivePie(data.preventiveCount, data.correctiveCount)
                preventiveCoreectivePerMonth(data.preventMonth, data.correctiveMonth)

                allServiceMonth(data.allServiceMonth)

                
                brancService(data.preventiveBranch, data.correctiveBranch)

                $('#preventiveCount').html('<h3>' + data.preventiveCount+'</h3> <p>Preventive Service</p>')
                $('#correctiveCount').html('<h3>' + data.correctiveCount + '</h3> <p>Corective Service</p>')
                $('#otherCount').html('<h3>' + data.otherCount + '</h3> <p>Other Service</p>')
                $('#branchCount').html('<h3>' + data.branchCount + '</h3> <p>Branchs</p>')

            },

            error: function (xhr, textStatus, errorThrown) {

                console.log('Error in Operation');
            }
        });
    }

   


    function PreventiveCorectivePie(Preventive, Corrective) {
        new Chart(document.getElementById("areaChart"), {
            type: 'pie',
            data: {
                labels: ["Preventive", "Corrective"],
                datasets: [{
                    label: "Services ",
                    backgroundColor: ["#3e95cd", "#8e5ea2"],
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


        new Chart(document.getElementById("lineChart"), {
            type: 'bar',
            data: {
                labels: preMonths.length > corMonths.length ? preMonths : corMonths,
                datasets: [
                    {
                        label: "Preventive",
                        backgroundColor: "#3e95cd",
                        data: preData
                    }, {
                        label: "Corrective",
                        backgroundColor: "#8e5ea2",
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


    function allServiceMonth(ServiceData) {


        let SMonths = []
        let SData = []

        for (var i = 0; i < ServiceData.length; i++) {
            SMonths[i] = ServiceData[i].monthName
            SData[i] = ServiceData[i].value

        }

        new Chart(document.getElementById("donutChart"), {
            type: 'bar',
            data: {
                labels: SMonths,
                datasets: [
                    {
                        label: "Services",
                        backgroundColor: "#17a2b8",
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

    function brancService(preventiveBranchData,correctiveBranchData) {


        if (preventiveBranchData == null && correctiveBranchData == null) {
            return;
        }
        let preBranch = []
      
        let corBranch = []
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

        let finalbranchs = preBranch.concat(corBranch.filter((item) => preBranch.indexOf(item) < 0));

  
        let prevFinal = [];
        let preBack = []
        let preBorder = []
        for (var i = 0; i < finalbranchs.length; i++) {

            for (var j = 0; j < preventiveBranchData.length; j++) {
                if (finalbranchs[i] == preventiveBranchData[j].branchName) {
                    prevFinal[i] = preventiveBranchData[j].value;
                continue;
                } else {
                   prevFinal[i] = 0;

                }
                preBack[i] = 'rgba(255, 99, 132, 0.2)'
                preBorder[i] = 'rgba(255, 99, 132, 1)'
            }
               
            
        }

        let corFinal = [];
     
        let corBack = []
        let corBorder = []
        for (var i = 0; i < finalbranchs.length; i++) {
            for (var j = 0; j < correctiveBranchData.length; j++) {
                if (finalbranchs[i] == correctiveBranchData[j].branchName) {
                    corFinal[i] = correctiveBranchData[j].value;
                    continue;
                } else {
                    corFinal[i] = 0;
                    continue;
                }
                corBack[i] = 'rgba(54, 162, 235, 0.2)'
                corBorder[i] = 'rgba(54, 162, 235, 1)'
            }
               

        }

        console.log(prevFinal)
        console.log(corFinal)


         
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


        var ctx = document.getElementById('barChart').getContext('2d');
        new Chart(ctx, {
                type: 'bar',
                data,
                options: {
                    indexAxis: 'y',
                }
         
        });
    }

   

})