const modalblock = document.getElementById('modalblock');

window.onclick = function(event) {
    if (event.target == modalblock) {
        modalblock.classList.add('is-hidden');
    }
}

