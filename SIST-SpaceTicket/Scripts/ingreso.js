
// ICON ANIMATION 
let logo = document.querySelectorAll('.login_logo-img');
let animationDone = 1;

function animation() {
    if (animationDone == 1) {
        animationDone = 0;
        logo[0].classList.add('freed');
        setTimeout(function () {
            logo[0].classList.remove('freed');
            animationDone = 1;
        }, 2500);
    }
}

const input = document.querySelectorAll('.input');
const combos = document.querySelectorAll('.login_location');
function focusInput() {
    let parent = this.parentNode.parentNode;
    parent.classList.add('focus');
}

function removeFocus() {
    let parent = this.parentNode.parentNode;
    if (this.value == "") {
        parent.classList.remove('focus');
    }
}

input.forEach(input => {
    input.addEventListener('focus', focusInput);
    input.addEventListener('blur', removeFocus);
});

combos.forEach(combo => {
    combo.addEventListener('click', focusInput);
});

const sr = ScrollReveal({
    origin: 'top',
    distance: '60px',
    duration: 2000,
    delay: 300
});

sr.reveal('.login_content');


// =========== Sección de registro ===========

let botones = document.querySelectorAll('.btn');
let panelViewed = 1;
let panels = document.querySelectorAll('.login_content-container');
let divBtn = document.querySelectorAll('.login_btn');
let loginTitle = document.querySelectorAll('.login_title');

function moverPanel() {
    if (panelViewed == 1) {
        panels[0].style.transform = 'translateX(-150%)';
        panels[1].style.transform = 'translateX(-150%)';
        divBtn[0].style.gridTemplateColumns = 'repeat(2, 1fr)';
        loginTitle[0].innerHTML = "Crear una cuenta <br> Localidad y Género";
        showBtn();
        panelViewed = 2;
    } else {
        panels[1].style.transform = 'translateX(150%)';
        panels[0].style.transform = 'translateX(0)';
        divBtn[0].style.gridTemplateColumns = 'repeat(1, 1fr)';
        loginTitle[0].innerHTML = "Crear una cuenta <br> Datos personales";
        notShowBtn();
        panelViewed = 1;
    }
}

let inputEmail = document.querySelectorAll('.email')[0];
let time = document.getElementById('time');
function onloadPage() {
    notShowBtn();
    if (inputEmail) {
        inputEmail.focus();
    }
    input.forEach(inp => {
        if (inp.value != "") {
            let parent = inp.parentNode.parentNode;
            parent.classList.add('focus');
        }
    });
}


function notShowBtn() {
    botones.forEach(boton => {
        boton.style.display = 'none';
        botones[0].style.display = 'initial';
    });
}

function showBtn() {
    botones.forEach(boton => {
        boton.style.display = 'initial';
        botones[0].style.display = 'none';
    });
}

if (!time) {
    window.onload = onloadPage();
}








