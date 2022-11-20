function toggleDropdown(dropdown) {
    dropdown.classList.toggle('dropdown-active');

    const backdrop = document.getElementById('backdrop');
    backdrop.classList.toggle('backdrop');
    backdrop.classList.toggle('is-fixed');
    backdrop.classList.toggle('is-full-screen');
}

function positionFooter() {
    document.querySelector('.footer').classList.toggle('footer-fixo', document.body.scrollHeight <= window.innerHeight)
}


window.addEventListener('DOMContentLoaded', () => {
    const toggles = document.getElementsByClassName('dropdown-toggle');
    if (toggles != null) {
        const dropdown = document.getElementById('logged-in');
        if (dropdown) {
            dropdown.classList.remove('dropdown-load')
            for (const iterator of toggles) {
                iterator.addEventListener('click', () => toggleDropdown(dropdown));
            }
        }
    }

    const resizeObserver = new ResizeObserver(positionFooter)

    resizeObserver.observe(document.body);
    positionFooter();
})
