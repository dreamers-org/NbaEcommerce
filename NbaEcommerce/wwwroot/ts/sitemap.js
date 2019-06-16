"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var index_cliente_1 = require("./index-cliente");
var carrello_1 = require("./carrello");
exports.arrayPageModules = [
    {
        page: "/Prodotto/IndexCliente",
        function: function (destination, template) { index_cliente_1.attivatorePaginaIndexCliente(); }
    },
    {
        page: "/Carrello",
        function: function (destination, template) { carrello_1.attivatorePaginaCarrello(); }
    }
];
//# sourceMappingURL=sitemap.js.map