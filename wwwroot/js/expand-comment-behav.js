let expcmbtn = document.querySelector(".expand-comment-btn");

expcmbtn.addEventListener("click", e => {
    console.log("click");
    const cm = document.querySelector(".comment-input-box");
    const allholder = e.target.closest(".commentmodal-post");

    console.log(allholder);
    
    let clone = cm.cloneNode(true);
    clone.removeAttribute("id");

    if(!(allholder.nextSibling.className == "comment-input-box")) {
        allholder.after(clone);
        aux = allholder.nextSibling;
        setTimeout(verifyMargin, 100);
        tgg = false;
    } else {
        aux = allholder.nextSibling;
        verifyMargin();
    }

    // Arrumar comportamento do resize dinâmico
    for (let i = 0; i < tx.length; i++) {
        tx[i].setAttribute("style", "height: 54px;");
        tx[i].addEventListener("input", OnInput, false);
    }

    function OnInput() {
        this.style.height = "auto";
        this.style.height = (this.scrollHeight) + "px";
    }

    function verifyMargin() {
        if (aux.style.marginTop === "-122px") {
            console.log("122");
            aux.style.marginTop = "-26px";
        } else if (aux.style.marginTop === "-26px" && tgg) {
            console.log("tgg");
            aux.style.marginTop = "-26px";
        } else if (aux.style.marginTop === "-26px" && !tgg) {
            console.log("!!tgg");
            aux.style.marginTop = "-122px";
        }
    }
});