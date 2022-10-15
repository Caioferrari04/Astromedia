const cmbtn = document.querySelector(".comment-btn");
const cmbtns = document.querySelectorAll(".comment-btn");
let tgg = true;
let aux;

Array.from(cmbtns).forEach(cmbtn => {
    cmbtn.addEventListener("click", e => {
        const cm = document.querySelector(".comment-input-box");
        const allholder = e.target.closest(".post-box");
        
        let clone = cm.cloneNode(true);
        // clone.removeAttribute("id"); dar um jeito de esconder a div

        if(!(allholder.nextSibling.className == "comment-input-box")) {
            allholder.after(clone);
            aux = allholder.nextSibling;
            setTimeout(verifyMargin, 100);
            tgg = false;
        } else {
            aux = allholder.nextSibling;
            verifyMargin();
        }

        function verifyMargin() {
            if (aux.style.marginTop === "-124px") {
                console.log("124");
                aux.style.marginTop = "-26px";
            } else if (aux.style.marginTop === "-26px" && tgg) {
                console.log("tgg");
                aux.style.marginTop = "-26px";
            } else if (aux.style.marginTop === "-26px" && !tgg) {
                console.log("!!tgg");
                aux.style.marginTop = "-124px";
            }
        }
    });
});

