function login() {
    var fehlerMeldung = document.getElementById("fehler");
    fehlerMeldung.innerHTML = "";
    //Daten aus den Inputfeldern lesen
    var benutzer = {
        benutzername : document.getElementById("benutzername").value,
        passwort : document.getElementById("passwort").value
    }
    //Login abfrage machen
    var uri = "api/login";
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(benutzer)
    })
        .then(response => {
            if (response.status == "200") {
                return response.json();
            } else {
                response.text().then(data => { fehlerMeldung.innerHTML = data.replace(/\"/g, "") });
                throw new Error("HTTP status " + response.status);
            }
        })
        .then(data => {
            sessionStorage.setItem("tokenKey", data.token);
            window.location = "Geburtstagsliste.html";
        })
        .catch(error => console.error('Unable to Login.', error));
}