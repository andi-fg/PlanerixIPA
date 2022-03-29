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
//Daten zum start absenden um direkt eine Liste zu generieren
dataAbsenden();
//Daten absenden und Liste erhalten
function dataAbsenden() {
    document.getElementById("fehler").innerHTML = "";
    var uri = "api/jubiläum?"
    var abteilungOptions = document.getElementById("abteilung").options;
    for (var i = 0, iLen = abteilungOptions.length; i < iLen; i++) {
        opt = abteilungOptions[i];
        if (opt.selected) {
            uri += "&abteilungen=" + opt.value;
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
                document.getElementById("tabelleJubiläum").innerHTML = "";
                document.getElementById("link").innerHTML = "Link: ";
                response.text().then(data => { document.getElementById("fehler").innerHTML = data.replace(/\"/g, "") });
                throw new Error("HTTP status " + response.status);
            }
        })
        .then(data => {
            //Tabelle erstellen
            machTabelle(data);
        })
}
function machTabelle(mitarbeiter) {
    var tabelle = document.getElementById("tabelleJubiläum");
    tabelle.innerHTML = "";
    mitarbeiter.forEach(mit => {
        var tr = document.createElement("tr");

        var tdName = document.createElement("td");
        tdName.innerHTML = mit.name;
        tr.appendChild(tdName);

        var tdVorname = document.createElement("td");
        tdVorname.innerHTML = mit.vorname;
        tr.appendChild(tdVorname);

        var tdDienst = document.createElement("td");
        tdDienst.innerHTML = mit.dienstjahre;
        tr.appendChild(tdDienst);

        var tdNaechst = document.createElement("td");
        tdNaechst.innerHTML = mit.nächstes;
        tr.appendChild(tdNaechst);

        var tdEintritt = document.createElement("td");
        tdEintritt.innerHTML = mit.eintritt;
        tr.appendChild(tdEintritt);

        tabelle.appendChild(tr);
    })
}