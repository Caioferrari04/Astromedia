function showPassword() {
    const toggleImage = document.getElementById('show-password1');
    const inputbox = document.getElementById('inputbox-pass1');

    let type = inputbox.getAttribute("type") === "password" ? "text" : "password";
    inputbox.setAttribute("type", type);
    
    if (type === "password") {
        toggleImage.src = "https://icongr.am/clarity/eye.svg?size=128&color=d2d6dd";
    } else {
        toggleImage.src = "https://icongr.am/clarity/eye-hide.svg?size=128&color=d2d6dd";
    }
};

function showConfirmation() {
    const toggleImage = document.getElementById('show-password2');
    const inputbox = document.getElementById('inputbox-pass2');

    let type = inputbox.getAttribute("type") === "password" ? "text" : "password";
    inputbox.setAttribute("type", type);
    
    if (type === "password") {
        toggleImage.src = "https://icongr.am/clarity/eye.svg?size=128&color=d2d6dd";
    } else {
        toggleImage.src = "https://icongr.am/clarity/eye-hide.svg?size=128&color=d2d6dd";
    }
};