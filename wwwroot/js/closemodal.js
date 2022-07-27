function showPass() {
    const inputbox = document.getElementById('inputbox-password');
    let type = inputbox.getAttribute("type") === "password" ? "text" : "password";
    inputbox.setAttribute("type", type);
};