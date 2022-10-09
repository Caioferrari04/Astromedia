function toggleComment() {
    const cm = document.querySelector(".comment-input-box");
    if (cm.style.marginTop === "-128px") {
        cm.setAttribute("style", "margin-top: -26px !important;");
        cm.style.transition = ".7s ease-in-out";
        console.log("comment-box down");
    } else {
        cm.setAttribute("style", "margin-top: -128px !important;");
        cm.style.transition = ".7s ease-in-out";
        console.log("comment-box up");
    }
}
    