
const contStar = 300;
const space = document.getElementsByClassName('main')[0];
function createSpace() {
    let stars = '';
    for (let index = 0; index < contStar; index++) {
        let star = document.createElement('span');
        star.classList.add('star');
        let top = parseInt(Math.random() * space.offsetHeight);
        let left = parseInt(Math.random() * space.offsetWidth);
        let padding = parseInt(Math.random() * 3);
        star.style.top = top + 'px';
        star.style.left = left + 'px';
        star.style.padding = padding + 'px';
        let random = parseInt(Math.random() * 10);
        if (random % 2 == 0) {
            star.classList.add('starAnimated')
            let delay = (Math.random() * 4);
            star.style.animationDelay = delay + 's';
        }
        space.appendChild(star);
    }
}

window.onload = createSpace;

function bookSeat(ID) {
    $.ajax({
        method: "POST",
        url: "/Boleto/Book",
        data: { lugar: ID },
        success: function () {
            var img = document.getElementById(ID);
            if (img.title == "DISPONIBLE") {
                img.src = 'https://img.icons8.com/material-rounded/24/00E154/bus-seat-top-view.png';
                img.title = "RESERVADO"
            } else {
                img.src = 'https://img.icons8.com/material-rounded/24/666666/bus-seat-top-view.png';
                img.title = "DISPONIBLE"
            }
        },
        error: function (error) {
            //TODO: Add some code here for error handling or notifications
        }
    });
}
