function toggleDropdown(dropdown) {
    dropdown.classList.toggle('dropdown-active');

    const backdrop = document.getElementById('backdrop');
    backdrop.classList.toggle('backdrop');
    backdrop.classList.toggle('is-fixed');
    backdrop.classList.toggle('is-full-screen');
}


window.addEventListener('DOMContentLoaded', () => {
    const toggles = document.getElementsByClassName('dropdown-toggle');
    if (toggles != null) {
        const dropdown = document.getElementById('logged-in');
        dropdown.classList.remove('dropdown-load')
        for (const iterator of toggles) {
            iterator.addEventListener('click', () => toggleDropdown(dropdown));
        }
    }
    
    if (document.body.scrollHeight <= window.innerHeight) {
        document.querySelector('.footer').classList.add('footer-fixo')
    }
})
