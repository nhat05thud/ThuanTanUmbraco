var cart = {
    properties: {
        carts: [],
        cartsId: [],
        cartWrapHeader: "#wrap-into-cart",
        cartItemHeader: ".items-in-cart .item",
        parents: ".item__grid--2",
        image: ".item-image",
        title: ".item-title",
        price: ".current-price",
        quantity: ".item-quantity"
    },
    init: function() {
        cart.checkItemVisible();
    },
    isInArray: function (value, array) {
        return array.indexOf(value) > -1;
    },
    getItemProperties: function (e) {
        var item = {
            id: $(e).parents(cart.properties.parents).find(cart.properties.title).data("id"),
            image: $(e).parents(cart.properties.parents).find(cart.properties.image).attr("src"),
            title: $(e).parents(cart.properties.parents).find(cart.properties.title).text(),
            link: $(e).parents(cart.properties.parents).find(cart.properties.title).attr("href"),
            price: $(e).parents(cart.properties.parents).find(cart.properties.price).children("span").text().split(".").join(""),
            quantity: 1
        }
        return item;
    },
    addToCart: function(e) {
        var item = cart.getItemProperties(e);
        if (!cart.isInArray(item.id, cart.properties.cartsId)) {
            cart.properties.cartsId.push(item.id);
            cart.properties.carts.push(item);
        } else {
            for (var i = 0; i < cart.properties.carts.length; i++) {
                if (cart.properties.carts[i].id === item.id) {
                    cart.properties.carts[i].quantity += 1;
                }
            }
        }
        cart.genarateCart(cart.properties.carts);
    },
    genarateCart: function (list) {
        $.ajax({
            type: "POST",
            data: { model: JSON.stringify(list) },
            url: "/umbraco/surface/cart/rendercartitem",
            success: function (res) {
                $(cart.properties.cartWrapHeader).html(res);
                var quantity = 0;
                for (var i in list) {
                    quantity += parseInt(list[i].quantity);
                }
                $(".cart-form .item-visible > span").text(quantity);
            }
        });
    },
    checkItemVisible: function() {
        $(cart.properties.cartItemHeader).each(function (i, e) {
            var item = {
                id: $(e).find(cart.properties.title).data("id"),
                image: $(e).find(cart.properties.image).attr("src"),
                title: $(e).find(cart.properties.title).text(),
                link: $(e).find(cart.properties.title).attr("href"),
                price: $(e).find(".price").children("span").first().text().split(".").join(""),
                quantity: $(e).find(".price").find(cart.properties.quantity).text()
            }
            cart.properties.cartsId.push(item.id);
            cart.properties.carts.push(item);
        });
    }
}
cart.init();