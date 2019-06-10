"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function ShowBadgeCart() {
    try {
        $.ajax({
            type: "GET",
            url: "/Carrello/IsCarrelloEmpty",
            contentType: "application/json",
            dataType: "json",
            success: function (response) {
                console.log(response);
                if (response != null) {
                    var isVisibile = response;
                    if (isVisibile) {
                        var spanBadgeCart = $("#spanBadgeCart");
                        spanBadgeCart.show();
                    }
                }
            }
        });
    }
    catch (e) {
        console.log("Errore funzione: ShowBadgeCart");
    }
}
exports.ShowBadgeCart = ShowBadgeCart;
//# sourceMappingURL=utility.js.map