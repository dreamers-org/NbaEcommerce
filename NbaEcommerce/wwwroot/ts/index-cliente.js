"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var utility_1 = require("./utility");
function attivatorePaginaIndexCliente() {
    window["cambiaListaCategorie"] = cambiaListaCategorie;
    window["cambiaListaMarchi"] = cambiaListaMarchi;
    utility_1.ShowBadgeCart();
}
exports.attivatorePaginaIndexCliente = attivatorePaginaIndexCliente;
function cambiaListaMarchi() {
    var valori = "";
    $(".gridCheck2").each(function () {
        var chk = this;
        if (chk.checked) {
            valori += ";" + this.dataset.id;
        }
    });
    $("#listaMarchi").val(valori);
    $('#test').submit();
}
function cambiaListaCategorie() {
    var valori = "";
    $(".gridCheck").each(function () {
        var chk = this;
        if (chk.checked) {
            valori += ";" + this.dataset.id;
        }
    });
    $("#listaCategorie").val(valori);
    $('#test').submit();
}
//# sourceMappingURL=index-cliente.js.map