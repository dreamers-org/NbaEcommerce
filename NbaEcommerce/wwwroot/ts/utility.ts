export function ShowBadgeCart() {
    try {

        $.ajax({
            type: "GET",
            url: "/Carrello/IsCarrelloEmpty",
            contentType: "application/json",
            dataType: "json",
            success: function (response) {
                console.log(response);

                if (response != null) {
                    let isVisibile: boolean = response as boolean;
                    if (isVisibile) {
                        let spanBadgeCart: JQuery<HTMLElement> = $("#spanBadgeCart");
                        spanBadgeCart.show();
                    }
                }
            }
        });

    } catch (e) {
        console.log("Errore funzione: ShowBadgeCart")
    }
}