function showPass() {
    const toggleImage = document.getElementById('show-password');
    const inputbox = document.getElementById('inputbox-password');

    let type = inputbox.getAttribute("type") === "password" ? "text" : "password";
    inputbox.setAttribute("type", type);

    if (type === "password") {
        toggleImage.src = "https://icongr.am/clarity/eye.svg?size=128&color=d2d6dd";
    } else {
        toggleImage.src = "https://icongr.am/clarity/eye-hide.svg?size=128&color=d2d6dd";
    }
};