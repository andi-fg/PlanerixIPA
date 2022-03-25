var token = "Bearer " + sessionStorage.getItem("tokenKey");
document.getElementById("token").innerHTML = "Token: " + token;
//Logout
function logout() {
    sessionStorage.clear();
    window.location = "login.html"
}
//AbteilungSelect
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
                throw new Error("HTTP status " + response.status);
            } else {
                document.getElementById("tabelleEinAustritte").innerHTML = "";
                document.getElementById("link").innerHTML = "Link: ";
                response.text().then(data => { document.getElementById("fehler").innerHTML = data.replace(/\"/g, "") });
            }
        })
        .then(data => {
            machTabelle(data)
        })
        .catch(error => document.getElementById("fehler").innerHTML = "Unauthorized");
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
       

        var tdAustritt = document.createElement("td");
        if (mit.austritt != null) {
            tdAustritt.innerHTML = mit.austritt;
        } else {
            tdAustritt.innerHTML = "-";
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