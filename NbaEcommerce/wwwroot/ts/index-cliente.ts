export function attivatorePaginaIndexCliente() {
    window["cambiaListaCategorie"] = cambiaListaCategorie;
    window["cambiaListaMarchi"] = cambiaListaMarchi;
}

function cambiaListaMarchi() {
    let valori:string = "";

    $(".gridCheck2").each(function () {
        let chk: HTMLInputElement = <HTMLInputElement>this;
        if (chk.checked) {
            valori += ";" + this.dataset.id;
        }
    });

    //$.ajax({
    //    type: "POST",
    //    url: "/Prodotto/GetDispositiviByMarchio",
    //    data: { marchiSelezionati: valori },
    //    success: function (data) {
    //        for (var i = 0; i < data.length; i++) {
    //            console.log(data[i].id);
    //        }
    //    },
    //    error: function () {
    //    }
    //});

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