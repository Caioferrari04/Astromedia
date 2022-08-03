function toggleDropdown(dropdown) {
    dropdown.classList.toggle('dropdown-active');
}

const profiles = document.getElementsByClassName('logo-user');
if (profiles != null) {
    const dropdown = document.getElementById('logged-in');
    console.log(profiles);
    for (const iterator of profiles) {
        iterator.addEventListener('click', () => toggleDropdown(dropdown));
    }
}
