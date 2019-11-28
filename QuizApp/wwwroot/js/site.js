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

