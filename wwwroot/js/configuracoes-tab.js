let botoes = document.getElementsByClassName("nav-button");

function mostrarAba(x) {
    console.log(x);
    for (let i = 0; i < 4; i++) {
        if (i == x) {
            botoes[i].classList = "nav-button active";
        } else {
            botoes[i].classList = "nav-button";
        }
    }
}