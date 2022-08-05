function toggleDropdown(dropdown) {
    dropdown.classList.toggle('dropdown-active');

    const backdrop = document.getElementById('backdrop');
    backdrop.classList.toggle('backdrop');
    backdrop.classList.toggle('is-fixed');
    backdrop.classList.toggle('is-full-screen');
}

const toggles = document.getElementsByClassName('dropdown-toggle');
if (toggles != null) {
    const dropdown = document.getElementById('logged-in');
    console.log(toggles);
    for (const iterator of toggles) {
        iterator.addEventListener('click', () => toggleDropdown(dropdown));
    }
}
