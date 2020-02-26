(function ($) {
    $(function () {
        myfunload();
        cartOpen(".header-right__form--cart .item .item-visible");
        $(".product-colors .item").click(function () {
            $(".product-colors .item").removeClass("active");
            $(this).addClass("active");
        });
    });
})(jQuery);
$(document).ready(function () {
    $(".header-right__form--cart .items-in-cart").mCustomScrollbar({
        autoHideScrollbar: true,
        theme: "dark-thick",
        scrollbarPosition: "outside"
    });
    $(".product-colors .item").click(function () {
        var itemData = {
            id: $(this).data("id"),
            color: $(this).data("color")
        };
        $("body").append("<div class='loading_div'></div>");
        $(".loading_div").show();
        $.ajax({
            type: "POST",
            data: itemData,
            url: "/admin/surface/product/renderproductitem",
            success: function (data) {
                $(".wrap-product-detail .left").html(data);
                moreImage();
                $(".loading_div").remove();
            }
        });
    });
});
$(document).click(function (e) {
    var container = $("#header");
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        $("#menu li.li-dropdown").removeClass("a--active");
        $("#menu li.li-dropdown").find(".nav-main__sub").removeClass("nav--active");
        $(".header-right__form--cart .wrap-hidden").removeClass("active");
        $("#mobile-overlay").removeClass("menu-is--active");
    }
}); 
function cartOpen(el) {
    $(el).click(function () {
        $(".header-right__form--cart .wrap-hidden").not($(this).next()).removeClass("active");
        $(this).next().toggleClass("active");
        $("#mobile-overlay").addClass("menu-is--active");
        if (!$(this).next().hasClass("active") && !$(".li-dropdown").hasClass("a--active")) {
            $("#mobile-overlay").removeClass("menu-is--active");
        }
        if ($("#menu li.li-dropdown").hasClass("a--active")) {
            $("#menu li.li-dropdown").removeClass("a--active");
            $("#menu li.li-dropdown").find(".nav-main__sub").removeClass("nav--active");
        }
    });
}
function myfunload() {
    $(".panel-a").mobilepanel();
    $("#menu > li").not(".home").clone().appendTo($("#menuMobiles"));
    $(".menuTop > ul > li").clone().appendTo($("#menuMobiles"));
    $("#menuMobiles input").remove();
    $("#menuMobiles > li > a").append('<span class="fa fa-chevron-circle-right iconar"></span>');
    $("#menuMobiles li li a").append('<span class="fa fa-angle-right iconl"></span>');
    $("#menu > li:last-child").addClass("last");
    $("#menu > li:first-child").addClass("fisrt");

    $("#menu > li").find("ul").addClass("menu-level");

    $("#menu li.li-dropdown > a").click(function (e) {
        e.preventDefault();
        $(this).parent().toggleClass("a--active");
        $(this).parent().find(".nav-main__sub").toggleClass("nav--active");
        $("#mobile-overlay").addClass("menu-is--active");
        if (!$(this).parent().hasClass("a--active") && !$(".header-right__form--cart .wrap-hidden").hasClass("active")) {
            $("#mobile-overlay").removeClass("menu-is--active");
        }
        $(".header-right__form--cart .wrap-hidden").removeClass("active")
    });

    $(".main-banner").owlCarousel({
        items: 1,
        lazyLoad: true,
        loop: true,
        nav: false,
        dots: false,
        autoplay: true,
        autoplayTimeout: 7000,
        autoplayHoverPause: true
    });

    $(".product-carousel").owlCarousel({
        margin: 30,
        lazyLoad: true,
        loop: false,
        nav: true,
        dots: false,
        autoplay: false,
        autoplayTimeout: 7000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 2
            },
            480: {
                items: 3
            },
            860: {
                items: 4
            },
            1200: {
                items: 4
            }
        }
    });
    $('.more-image').owlCarousel({
        margin: 10,
        lazyLoad: true,
        loop: true,
        nav: false,
        dots: false,
        autoplay: false,
        autoplayTimeout: 7000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 3
            },
            480: {
                items: 3
            },
            600: {
                items: 4
            },
            1000: {
                items: 4
            },
            1200: {
                items: 5
            }
        }
    });
}
$(window).scroll(function () {
    if ($(this).scrollTop() > 100) {
        $('.scroll-to-top').fadeIn();
    } else {
        $('.scroll-to-top').fadeOut();
    }
});

$('.scroll-to-top').on('click', function (e) {
    e.preventDefault();
    $('html, body').animate({ scrollTop: 0 }, 800);
    return false;
});