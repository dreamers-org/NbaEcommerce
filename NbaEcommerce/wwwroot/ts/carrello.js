"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function attivatorePaginaCarrello() {
    window["ricalcolaTotale"] = ricalcolaTotale;
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
//# sourceMappingURL=carrello.js.map