var token = "Bearer " + sessionStorage.getItem("tokenKey");
document.getElementById("token").innerHTML = "Token: " + token;
//Logout
function logout() {
    sessionStorage.clear();
    window.location = "login.html";
}
//AbteilungSelect erstellen
fetch("api/abteilung", {
    headers: {
        'Authorization': token
    }
})
    .then(response => response.json())
    .then(data => {
        var sel = document.getElementById("abteilung")
        var length = sel.options.length;
        for (i = length - 1; i >= 0; i--) {
            sel.options[i] = null;
        }
        data.forEach(item => {
            var opt = document.createElement("option");
            opt.value = item.bezeichnung;
            opt.text = item.bezeichnung;
            sel.add(opt, null);
        })
    })
function dataAbsenden() {
    document.getElementById("fehler").innerHTML = "";
    var von = document.getElementById("von").value;
    var bis = document.getElementById("bis").value;
    var options = document.getElementById("abteilung").options;
    var uri = "api/einaustrittmonat?von=" + von + "&bis=" + bis;
    for (var i = 0, iLen = options.length; i < iLen; i++) {
        opt = options[i];
        if (opt.selected) {
            uri += "&abteilungen=" + opt.value;
        }
    }
    document.getElementById("link").innerHTML = "Link: https://localhost:44338/" + uri;
    fetch(uri, {
        headers: {
            'Authorization': token
        }
    })
        .then(response => {
            if (response.status == "200") {
                return response.json();
            } else if (response.status == "401") {
                document.getElementById("fehler").innerHTML = "Unauthorized";
                throw new Error("HTTP status " + response.status);
            } else {
                if (myChart != null) {
                    myChart.destroy();
                }
                document.getElementById("link").innerHTML = "Link: ";
                response.text().then(data => { document.getElementById("fehler").innerHTML = data.replace(/\"/g, "") });

            }
        })
        .then(data => {
            machStatistik(data)
        })
}
var myChart = null;
function machStatistik(einaustritte) {
    var eintritte = [];
    var austritte = [];
    var label = [];
    einaustritte.forEach(ea => {
        eintritte.push(ea.eintritte);
        austritte.push(ea.austritte);
        label.push(ea.monat);
    })
    var canvas = document.getElementById('einaustrittDiagramm');
    canvas.style.height = "300px";
    if (myChart != null) {
        myChart.destroy();
    }
    myChart = new Chart(canvas, {
        type: 'bar',
        data: {
            labels: label,
            datasets:[
            {
                label: "Eintritte",
                data: eintritte,
                backgroundColor: 'rgba(255, 99, 132, 1)'
           },{
                label: "Austritte",
                data: austritte,
                backgroundColor: 'rgba(54, 162, 235, 1)'
           }
        ]
        },
        options: {
            maintainAspectRatio: false,
            plugins: {
                title: {
                    display: true,
                    text: "Ein-Austrittemonat",
                    font: {
                        size: 14
                    }
                }
            }
        }
    })
}