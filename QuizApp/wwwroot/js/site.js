// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let questionDisabled = false;
const sendResponse = (quizId, data) => {
    $.ajax({
        type: "POST",
        url: `${window.location.origin}/Quiz/${quizId}/Result`,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: () => { return null },
    });

}

const sendResult = (quiz, value) => {
    const correct = value === 'True';
    const question = parseInt($("#question_id").val(), 10);
    const attempt = parseInt($("#attempt_id").val(), 10);
    if (!questionDisabled) {
        questionDisabled = true;
        sendResponse(quiz, { correct, attempt, question});
    }
}

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