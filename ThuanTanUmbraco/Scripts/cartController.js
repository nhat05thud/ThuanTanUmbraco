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
    init: function () {
        cart.checkSessionCart();
        setTimeout(function () { cart.checkItemVisible() }, 500);
    },
    isInArray: function (value, array) {
        return array.indexOf(value) > -1;
    },
    getItemProperties: function (e) {
        var item = {
            id: $(e).parents(cart.properties.parents).find(cart.properties.title).data("id"),
            image: $(e).parents(cart.properties.parents).find(cart.properties.image).attr("src"),
            name: $(e).parents(cart.properties.parents).find(cart.properties.title).text(),
            url: $(e).parents(cart.properties.parents).find(cart.properties.title).attr("href"),
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
                    cart.properties.carts[i].quantity = parseInt(cart.properties.carts[i].quantity) + 1;
                }
            }
        }
        cart.genarateCart(cart.properties.carts);
        cart.addSessionCart(cart.properties.carts);
    },
    addSessionCart: function (list) {
        $.ajax({
            type: "POST",
            data: { model: JSON.stringify(list) },
            url: "/umbraco/surface/cart/addtocart",
            success: function (res) {}
        });
    },
    deleteCartItem: function (item, id) {
        $.ajax({
            type: "POST",
            data: { id: parseInt(id) },
            url: "/umbraco/surface/cart/deletecartitem",
            success: function(res) {
                if ($(cart.properties.cartItemHeader).length > 1) {
                    $(item).parent().remove();
                    var total = parseInt($(cart.properties.cartWrapHeader).children(".total").find("i").text().split(".").join("")) - parseInt(res.price);
                    $(cart.properties.cartWrapHeader).children(".total").find("i").text(cart.formatNumberThousand(total));
                } else {
                    $(cart.properties.cartWrapHeader).html("<div class='empty-cart'><p>Chưa có sản phẩm nào trong giỏ hàng</p></div>");
                }
                cart.updateQuantity();
            }
        });
    },
    deleteAllCart: function () {
        $.ajax({
            type: "POST",
            url: "/umbraco/surface/cart/deleteallcart",
            success: function (data) {
                $("#wrapper-cart").html(data);
                cart.checkSessionCart();
            }
        });
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
    checkSessionCart: function () {
        $.ajax({
            type: "POST",
            url: "/umbraco/surface/cart/initcart",
            success: function (res) {
                $(cart.properties.cartWrapHeader).html(res);
                cart.updateQuantity();
            }
        });
    },
    checkItemVisible: function() {
        $(cart.properties.cartItemHeader).each(function (i, e) {
            var item = {
                id: $(e).find(cart.properties.title).data("id"),
                image: $(e).find(cart.properties.image).attr("src"),
                name: $(e).find(cart.properties.title).text(),
                url: $(e).find(cart.properties.title).attr("href"),
                price: $(e).find(".price").children("span").first().text().split(".").join(""),
                quantity: $(e).find(".price").find(cart.properties.quantity).text()
            }
            cart.properties.cartsId.push(item.id);
            cart.properties.carts.push(item);
        });
    },
    updateQuantity: function () {
        var quantity = 0;
        $(cart.properties.cartItemHeader).each(function (i, e) {
            quantity += parseInt($(e).find(".item-quantity").text());
        });
        $(".cart-form .item-visible > span").text(quantity);
    },
    updateTotalPrice: function (e, quantity) {
        $(".loading_div").css("display", "block");
        $.ajax({
            type: "POST",
            data: { id: parseInt($(e).data("id")), quantity: parseInt(quantity) },
            url: "/umbraco/surface/cart/updatetotalprice",
            success: function (res) {
                $(".cart-footer ul li .currency > span").text(cart.formatNumberThousand(res.total));
                cart.checkSessionCart();
                $(".loading_div").css("display", "none");
            }
        });
    },
    formatNumberThousand: function(num) {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");
    }
}
cart.init();