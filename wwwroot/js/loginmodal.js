const modalbtn = document.getElementById('modalbtn');
const modalblock = document.getElementById('modalblock');

modalbtn.addEventListener('click', () => {
    modalblock.classList.remove('hidden');
})

window.onclick = function(event) {
    if (event.target == modalblock) {
        modalblock.classList.add('hidden');
    }
  }