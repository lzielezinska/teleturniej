const pinContainer = $('#pin-container');
let code = '';
let codeLength = 6;
const startBtn = '⏎Start';
const deleteBtn = 'Usuń';
let Keyboard = window.SimpleKeyboard.default;

let keyboard = new Keyboard({
    onKeyPress: button => onKeyPress(button),
    layout: {
        default: ["1 2 3", "4 5 6", "7 8 9", "{//} 0 {//}", `${deleteBtn} ${startBtn}`],
    },
    theme: "hg-theme-default hg-layout-numeric numeric-theme"
});

const onKeyPress = (button) => {
    if (button === deleteBtn) {
        const str = code;
        code = str.substring(0, str.length - 1);
    } else if (button === startBtn) {
        startQuiz();
    } else {
        if (code.length < codeLength) {
            code += button;
        }
    }
    renderPin();
};

const renderPin = () => {
    let pinElements = '';
    for (let i = 0; i < codeLength; i++) {
        const number = code.charAt(i);
        pinElements += `
            <div class="col-2">
                <div class="p-2 text-center pin-code-text">
                    <div class="row h-100">
                        <div class="col-sm-12 my-auto">
                            ${number}
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
    pinContainer.html(pinElements);
};

const startQuiz = () => {
    const data = {
        code
    };
    $.ajax({
        type: "POST",
        url: `${window.location.origin}/Quiz/Start`,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: 'text/plain; charset=utf-8',
        complete: (response) => {
            if (response.responseText != '0') {
                window.location = `${window.location.origin}/Quiz/${response.responseText}/Question/0`;
            } else {
                Swal.fire({
                    title: 'Błędny PIN!',
                    text: 'Spróbuj ponownie',
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
            }
        },
    });
};

$(document).ready(() => {
    renderPin();
});
