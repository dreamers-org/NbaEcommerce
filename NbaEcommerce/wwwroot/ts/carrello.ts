export function attivatorePaginaCarrello() {
    window["aggiornaQuantita"] = aggiornaQuantita;

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


function aggiornaQuantita(ddl: HTMLSelectElement, url:string) {

    ricalcolaTotale();

    if (ddl != null && ddl.value != null) {
        let valoreCombo: Number = Number(ddl.value);
        let idProdottoTest:string = ddl.dataset.idprodotto;
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



