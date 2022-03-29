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