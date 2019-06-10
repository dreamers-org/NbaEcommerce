export function attivatorePaginaCarrello() {
    window["ricalcolaTotale"] = ricalcolaTotale;

    ricalcolaTotale();
}

function ricalcolaTotale() {
    let costoTotale: number = 0;

    $(".select-prodotto").each(function () {
        let selectArt: HTMLSelectElement = this as HTMLSelectElement;

        let numeroArticoli: number = Number(selectArt.value);
        let costoArticolo: number = Number(selectArt.dataset.costo);

        costoTotale += numeroArticoli * costoArticolo
    });

    console.log(costoTotale);

    $("#lblTotale")[0].innerHTML = costoTotale + "€";
}



