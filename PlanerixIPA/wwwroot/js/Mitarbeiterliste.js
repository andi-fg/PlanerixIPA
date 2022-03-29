var token = "Bearer " + sessionStorage.getItem("tokenKey");
document.getElementById("token").innerHTML = "Token: " + token;
//Logout
function logout() {
    sessionStorage.clear();
    window.location = "login.html"
}
initSelect("abteilung");
initSelect("funktion");
initSelect("programm");
//Selects erstellen
function initSelect(id) {
    var link = "api/"+id
    fetch(link, {
        headers: {
            'Authorization': token
        }
    })
        .then(response => response.json())
        .then(data => {
            var sel = document.getElementById(id);
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
}
function dataAbsenden() {
    document.getElementById("fehler").innerHTML = "";
    var datum = document.getElementById("datum").value;
    var uri = "api/mitarbeiterliste?datum=" + datum;
    //Name
    var name = document.getElementById("name").value;
    if (name.length > 0) {
        uri += "&name=" + name;
    }
    //Vorname
    var vorname = document.getElementById("vorname").value;
    if (vorname.length > 0) {
        uri += "&vorname=" + vorname;
    }
    //Abteilung
    var abteilungOptions = document.getElementById("abteilung").options;
    for (var i = 0, iLen = abteilungOptions.length; i < iLen; i++) {
        opt = abteilungOptions[i];
        if (opt.selected) {
            uri += "&abteilungen=" + opt.value;
        }
    }
    //Funktion
    var funktionOptions = document.getElementById("funktion").options;
    for (var i = 0, iLen = funktionOptions.length; i < iLen; i++) {
        opt = funktionOptions[i];
        if (opt.selected) {
            uri += "&funktionen=" + opt.value;
        }
    }
    //Programm
    var programmOptions = document.getElementById("programm").options;
    for (var i = 0, iLen = programmOptions.length; i < iLen; i++) {
        opt = programmOptions[i];
        if (opt.selected) {
            uri += "&programme=" + opt.value;
        }
    }
    fetch(uri, {
        headers: {
            'Authorization': token
        }
    })
        .then(response => {
            if (response.status == "200") {
                document.getElementById("link").innerHTML = "Link: https://localhost:44338/" + uri;
                return response.json();
            } else if (response.status == "401") {
                document.getElementById("fehler").innerHTML = "Unauthorized";
                throw new Error("HTTP status " + response.status);
            } else {
                document.getElementById("output").innerHTML = "";
                document.getElementById("tabelleMitarbeiter").innerHTML = "";
                document.getElementById("link").innerHTML = "Link: ";
                response.text().then(data => { document.getElementById("fehler").innerHTML = data.replace(/\"/g, "") });
                throw new Error("HTTP status " + response.status);
            }
        })
        .then(data => {
            //Tabelle erstellen
            machTabelle(data);
            //Diagramm erstellen
            initStatistik(data);
        })
}
function machTabelle(mitarbeiter) {
    var tabelle = document.getElementById("tabelleMitarbeiter");
    tabelle.innerHTML = "";
    mitarbeiter.forEach(mit => {
        var tr = document.createElement("tr");

        var tdName = document.createElement("td");
        tdName.innerHTML = mit.name;
        tr.appendChild(tdName);

        var tdVorname = document.createElement("td");
        tdVorname.innerHTML = mit.vorname;
        tr.appendChild(tdVorname);

        var tdMail = document.createElement("td");
        tdMail.innerHTML = mit.email;
        tr.appendChild(tdMail);

        var tdAbteilung = document.createElement("td");
        var abteilung = ""
        mit.abteilungen.forEach(abt => {
            abteilung += abt + ","
        })
        tdAbteilung.innerHTML = abteilung.substring(0, abteilung.length - 1)
        tr.appendChild(tdAbteilung);

        var tdFunktionen = document.createElement("td");
        var funktion = ""
        mit.funktionen.forEach(funk => {
            funktion += funk + ","
        })
        tdFunktionen.innerHTML = funktion.substring(0, funktion.length - 1)
        tr.appendChild(tdFunktionen);

        var tdProgramme = document.createElement("td");
        var programm = ""
        mit.programme.forEach(prog => {
            programm += prog + ","
        })
        tdProgramme.innerHTML = programm.substring(0, programm.length - 1)
        tr.appendChild(tdProgramme);

        var tdEintritt = document.createElement("td");
        tdEintritt.innerHTML = mit.eintritt;
        tr.appendChild(tdEintritt);

        var tdAustritt = document.createElement("td");
        tdAustritt.innerHTML = mit.austritt;
        tr.appendChild(tdAustritt);

        tabelle.appendChild(tr);
    })
}
//Statistiken erstellen
function initStatistik(mitarbeiter) {
    document.getElementById("output").innerHTML = "";
    //Abteilungen
    var auswahlAbteilung = [];
    var sel = document.getElementById("abteilung");
    var length = sel.options.length;
    for (i = length - 1; i >= 0; i--) {
        auswahlAbteilung.push(sel.options[i].value);
    }
    var anzahlAbteilung = [];
    var labelAbteilung = [];
    auswahlAbteilung.forEach(abt => {
        var anzahl = 0;
        mitarbeiter.forEach(mit => {
            mit.abteilungen.forEach(abtMit => {
                if (abt == abtMit) {
                    anzahl++;
                }
            })
        })
        if (anzahl > 0) {
            anzahlAbteilung.push(anzahl);
            labelAbteilung.push(abt);
        }
    })
    statistik(labelAbteilung, anzahlAbteilung, "Abteilung");
    //Funktion
    var auswahlFunktion = [];
    var sel = document.getElementById("funktion");
    var length = sel.options.length;
    for (i = length - 1; i >= 0; i--) {
        auswahlFunktion.push(sel.options[i].value);
    }
    var anzahlFunktion = [];
    var labelFunktion = [];
    auswahlFunktion.forEach(funk => {
        var anzahl = 0;
        mitarbeiter.forEach(mit => {
            mit.funktionen.forEach(funkMit => {
                if (funk == funkMit) {
                    anzahl++;
                }
            })
        })
        if (anzahl > 0) {
            anzahlFunktion.push(anzahl);
            labelFunktion.push(funk);
        }
    })
    statistik(labelFunktion, anzahlFunktion, "Funktionen");
    //Programm
    var auswahlProgramm = [];
    var sel = document.getElementById("programm");
    var length = sel.options.length;
    for (i = length - 1; i >= 0; i--) {
        auswahlProgramm.push(sel.options[i].value);
    }
    var anzahlProgramm = [];
    var labelProgramm = [];
    auswahlProgramm.forEach(prog => {
        var anzahl = 0;
        mitarbeiter.forEach(mit => {
            mit.programme.forEach(progMit => {
                if (prog == progMit) {
                    anzahl++;
                }
            })
        })
        if (anzahl > 0) {
            anzahlProgramm.push(anzahl);
            labelProgramm.push(prog);
        }
    })
    statistik(labelProgramm, anzahlProgramm, "Programme");
}
//Chart erstellen 
function statistik(label, data, titel) {
    var output = document.getElementById("output");
    var divCanv = document.createElement("div");
    var canvas = document.createElement("canvas");
    divCanv.classList.add("col-3");
    divCanv.classList.add("offset-1");
    divCanv.style.height = "300px";
    divCanv.style.width = "300px";
    divCanv.style.alignContent = "Center";
    var myChart = new Chart(canvas, {
        type: 'pie',
        data: {
            labels: label,
            datasets: [{
                label: '# of Votes',
                data: data,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                    'rgba(240,248,255,0.2)',
                    'rgba(255,193,193,0.2)',
                    'rgba(32,178,170,0.2)',
                    'rgba(255,20,147,0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)',
                    'rgba(240,248,255,0.2)',
                    'rgba(255,193,193,0.2)',
                    'rgba(32,178,170,0.2)',
                    'rgba(255,20,147,0.2)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            maintainAspectRatio: false,
            plugins: {
                title: {
                    display: true,
                    text: titel,
                    font: {
                        size: 14
                    }
                }
            }
        }
    });
    divCanv.appendChild(canvas);
    output.appendChild(divCanv);
}