// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.card.answer-card').click(function () {
    $(".card.answer-card").each(function () {
        var myValue = $(this).attr('value');
        if (myValue == "True") $(this).addClass("correct-answer");
        else $(this).addClass("wrong-answer");
    });
});

const openNav = () => {
    const sidenav = document.getElementById('mySidenav').style;
    sidenav.width = '250px';
    sidenav.paddingLeft = '20px';
    sidenav.paddingRight = '20px';
    document.getElementById('main').style.marginLeft = '250px';
};

const closeNav = () => {
    const sidenav = document.getElementById('mySidenav').style;
    sidenav.width = '0';
    sidenav.paddingLeft = '0';
    sidenav.paddingRight = '0';
    document.getElementById('main').style.marginLeft = '0';
};