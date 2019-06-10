import { attivatorePaginaIndexCliente } from "./index-cliente";
import { attivatorePaginaCarrello } from "./carrello";

export interface pageModule {
    page?: string;
    function?: (destination?: JQuery, template?: any) => void
    menuItem?: string;
}

export var arrayPageModules: pageModule[] = [
    {
        page: "/Prodotto/IndexCliente",
        function: function (destination, template) { attivatorePaginaIndexCliente() }
    },
    {
        page: "/Carrello",
        function: function (destination, template) { attivatorePaginaCarrello() }
    }
];


