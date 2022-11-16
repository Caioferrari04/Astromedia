var expcmbtn = document.querySelector(".expand-comment-btn");

$(document).on("click",".expand-comment-btn", () => {
    console.log("click");
    const cm = document.querySelector(".comment-input-box");
    const allholder = document.querySelector(".commentmodal-holder");

    console.log(allholder.firstChild);
    
    let clone = cm.cloneNode(true);
    clone.removeAttribute("id");

    if(!(allholder.firstChild.className == "comment-input-box")) {
        allholder.prepend(clone);
        aux = allholder.firstChild;
        setTimeout(verifyMargin, 100);
        tgg = false;
    } else {
        aux = allholder.firstChild;
        verifyMargin();
    }

    document.querySelectorAll("textarea").forEach(textarea => {
        textarea.addEventListener('change', (e) => {
            e.target.setAttribute("style", "height:" + (e.target.scrollHeight) + "px;");
            e.target.addEventListener("input", OnInput, false);
        })
      
        textarea.dispatchEvent(new Event('change'));
    })

    function verifyMargin() {
        if (aux.style.marginTop === "-119px") {
            console.log("122");
            aux.style.marginTop = "-5px";
            aux.scrollIntoView({behavior: "smooth", block: "start", inline: "nearest"});
        } else if (aux.style.marginTop === "-5px" && tgg) {
            console.log("tgg");
            aux.style.marginTop = "-5px";
        } else if (aux.style.marginTop === "-5px" && !tgg) {
            console.log("!!tgg");
            aux.style.marginTop = "-119px";
        }
    }
    
});