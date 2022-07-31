function toggleDropdown(dropdown) {
    dropdown.classList.toggle('dropdown-active');
}

const profile = document.getElementById('profile');
if (profile != null) {
    const dropdown = document.getElementById('logged-in');
    console.log('chegou aqui');
    profile.addEventListener('click', () => toggleDropdown(dropdown));
}
