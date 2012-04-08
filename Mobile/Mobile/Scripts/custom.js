

jQuery(document).ready(function () {
    $('#show_menu').hide();
    $("#hide_menu").click(function () {
        $('#left_menu').hide();
        $('#show_menu').show();
        $("body").addClass("nobg");
        $("#content").css("marginLeft", 30);


    });

    $('#show_menu').click(function () {
        $('#left_menu').show();
        $('#show_menu').hide();
        $("body").removeClass("nobg");
        $("#content").css("marginLeft", 240);
    });

    
});
        
