import { ShowBadgeCart } from './utility';

export function attivatorePaginaIndexCliente() {
    window["cambiaListaCategorie"] = cambiaListaCategorie;
    window["cambiaListaMarchi"] = cambiaListaMarchi;

    ShowBadgeCart();
}

function cambiaListaMarchi() {
    let valori:string = "";

    $(".gridCheck2").each(function () {
        let chk: HTMLInputElement = <HTMLInputElement>this;
        if (chk.checked) {
            valori += ";" + this.dataset.id;
        }
    });

    $("#listaMarchi").val(valori);
    $('#test').submit();

}

function cambiaListaCategorie() {
    let valori:string = "";

    $(".gridCheck").each(function () {
        let chk: HTMLInputElement = <HTMLInputElement>this;
        if (chk.checked) {
            valori += ";" + this.dataset.id;
        }
    });

    $("#listaCategorie").val(valori);
    $('#test').submit();
}


