$(document).ready(function () {
    /* This code is executed after the DOM has been completely loaded */

    /* Changing the default easing effect - will affect the slideUp/slideDown methods: */
    
    var menu_ul = $('.menus > li > ul'),
                 menu_li = $('.menus > li > ul > li > a'),
                 menu_a = $('.menus > li > a');

    menu_ul.hide();

    menu_a.click(function (e) {
                
        //e.preventDefault();
        if (!$(this).hasClass('active')) {
            menu_a.removeClass('active');
            menu_ul.filter(':visible').slideUp('normal');
            $(this).addClass('active').next().stop(true, true).slideDown('normal');
        } else {
            $(this).removeClass('active');
            $(this).next().stop(true, true).slideUp('normal');
        }
        if (!$(this).last)
            e.preventDefault();
    });

    //menu_li.click(function (e){
    //    menu_ul.show();
    //});

});
