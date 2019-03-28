//import { attivatorePaginaCreate } from "./ordinecliente-create";
//import { attivatorePaginaCreateArticolo } from "./articolo-create";
//import { attivatorePaginaIndexPackingList } from "./packinglist-index";
//import { attivatorePaginaOrdineClienteRiepilogo } from "./ordinecliente-riepilogo"

export interface pageModule {
    page?: string;
    function?: (destination?: JQuery, template?: any) => void
    menuItem?: string;
}

export var arrayPageModules: pageModule[] = [
    {
        page: "/Prodotto/IndexCliente",
        //function: function (destination, template) { attivatorePaginaCreate() },
        menuItem: "itemCatalogoCliente"
    }
    //,
    //{
    //    page: "/OrdineCliente/Riepilogo",
    //    function: function (destination, template) { attivatorePaginaOrdineClienteRiepilogo() },
    //    menuItem: "navbarDropdown"
    //},
    //{
    //    page: "/Articolo/Create",
    //    function: function (destination, template) { attivatorePaginaCreateArticolo() },
    //    menuItem: "navbarDropdown"
    //},
    //{
    //    page: "/PackingList/Index",
    //    function: function (destination, template) { attivatorePaginaIndexPackingList() },
    //    menuItem: "navbarDropdown"
    //}

];


