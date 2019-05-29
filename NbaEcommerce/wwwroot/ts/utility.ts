export function ShowBadgeCart() {
    try {

        let spanBadgeCart: JQuery<HTMLElement> = $("#ShowBadgeCart");
        spanBadgeCart.show();

    } catch (e) {
        console.log("Errore funzione: ShowBadgeCart")
    }
}