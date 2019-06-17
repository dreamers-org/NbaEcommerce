"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function attivatorePaginaCarrello() {
    window["aggiornaQuantita"] = aggiornaQuantita;
    ricalcolaTotale();
}
exports.attivatorePaginaCarrello = attivatorePaginaCarrello;
function ricalcolaTotale() {
    var costoTotale = 0;
    $(".select-prodotto").each(function () {
        var selectArt = this;
        var numeroArticoli = Number(selectArt.value);
        var costoArticolo = Number(selectArt.dataset.costo);
        costoTotale += numeroArticoli * costoArticolo;
    });
    console.log(costoTotale);
    $("#lblTotale")[0].innerHTML = costoTotale + "€";
}
function aggiornaQuantita(ddl, url) {
    ricalcolaTotale();
    if (ddl != null && ddl.value != null) {
        var valoreCombo = Number(ddl.value);
        var idProdottoTest = ddl.dataset.idprodotto;
        console.log(valoreCombo);
        console.log(idProdottoTest);
        $.ajax({
            type: "POST",
            url: "/Carrello/AggiornaQuantita",
            data: { idprodotto: idProdottoTest, quantita: valoreCombo.toString() },
            success: function (response) {
                console.log("Quantità cambiato con successo");
            }
        });
    }
}
//# sourceMappingURL=carrello.js.map