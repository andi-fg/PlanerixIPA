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
//Abfrage erstellen
function dataAbsenden() {
    document.getElementById("fehler").innerHTML = "";
    var von = document.getElementById("von").value;
    var bis = document.getElementById("bis").value;
    var options = document.getElementById("abteilung").options;
    var uri = "api/einaustrittliste?von=" + von + "&bis=" + bis;
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
                document.getElementById("tabelleEinAustritte").innerHTML = "";
                document.getElementById("link").innerHTML = "Link: ";
                response.text().then(data => { document.getElementById("fehler").innerHTML = data.replace(/\"/g, "") });
                throw new Error("HTTP status " + response.status);
            }
        })
        .then(data => {
            //Tabelle erstellen
            machTabelle(data);
            //Diagramm erstellen
            statistik(data);
        })
        
}
function machTabelle(mitarbeiter) {
    var tabelle = document.getElementById("tabelleEinAustritte");
    tabelle.innerHTML = "";
    mitarbeiter.forEach(mit => {
        var tr = document.createElement("tr");

        var tdName = document.createElement("td");
        tdName.innerHTML = mit.name;
        tr.appendChild(tdName);

        var tdVorname = document.createElement("td");
        tdVorname.innerHTML = mit.vorname;
        tr.appendChild(tdVorname);

        var tdEintritt = document.createElement("td");
        tdEintritt.innerHTML = mit.eintritt;
        tr.appendChild(tdEintritt);
        //Zeile färben wenn zukünftiger Eintritt
        var eintrittSplit = mit.eintritt.split(".");
        var heute = new Date();
        var eintrittDate = new Date(eintrittSplit[2] + "-" + eintrittSplit[1] + "-" + eintrittSplit[0])
        if (eintrittDate > heute) {
            tr.style.backgroundColor = "yellow";
        }

        var tdAustritt = document.createElement("td");
        tdAustritt.innerHTML = mit.austritt;
        if (mit.austritt.length > 1) {
            //Zelle färben wenn zukünftiger Austritt
            var austrittSplit = mit.austritt.split(".");
            var austrittDate = new Date(austrittSplit[2] + "-" + austrittSplit[1] + "-" + austrittSplit[0])
            if (austrittDate > heute) {
                tr.style.backgroundColor = "yellow";
            }
        }
        tr.appendChild(tdAustritt);

        var tdAbteilung = document.createElement("td");
        var abteilung = ""
        mit.abteilungen.forEach(abt => {
            abteilung += abt + ","
        })
        tdAbteilung.innerHTML = abteilung.substring(0, abteilung.length - 1)
        tr.appendChild(tdAbteilung)

        tabelle.appendChild(tr);
    })
}
//chart erstellen
var myChart = null;
function statistik(mitarbeiter) {
    var von = new Date(document.getElementById("von").value);
    var bis = new Date(document.getElementById("bis").value);
    var label = ["Eintritt", "Austritt"];
    var eintritt = 0;
    var austritt = 0;
    //Anzahl Ein- und Austritte zählen.
    mitarbeiter.forEach(mit => {
        if (mit.eintritt != null) {
            var eintrittSplit = mit.eintritt.split(".");
            var eintrittDate = new Date(eintrittSplit[2] + "-" + eintrittSplit[1] + "-" + eintrittSplit[0])
            if (eintrittDate.getTime() >= von.getTime() && eintrittDate.getTime() <= bis.getTime()) {
                eintritt++;
            }
        }
        if (mit.austritt != null) {
            var austrittSplit = mit.austritt.split(".");
            var austrittDate = new Date(austrittSplit[2] + "-" + austrittSplit[1] + "-" + austrittSplit[0])
            if (austrittDate.getTime() >= von.getTime() && austrittDate.getTime() <= bis.getTime()) {
                austritt++;
            }
        }
    })
    var einAus = [eintritt, austritt];
    var canvas = document.getElementById('myChart');
    if (myChart != null) {
        myChart.destroy();
    }
    //chart darstellen
    myChart = new Chart(canvas, {
        type: 'pie',
        data: {
            labels: label,
            datasets: [{
                label: '# of Votes',
                data: einAus,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            maintainAspectRatio: false,
            plugins: {
                title: {
                    display: true,
                    text: "Anzahl Ein- und Austritte",
                    font: {
                        size: 14
                    }
                }
            }
        }
    });
}