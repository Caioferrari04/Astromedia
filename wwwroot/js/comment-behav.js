const cmbtn = document.querySelector(".comment-btn");
const cmbtns = document.querySelectorAll(".comment-btn");
let tgg = true;
let aux;

Array.from(cmbtns).forEach(cmbtn => {
    cmbtn.addEventListener("click", e => {
        const cm = document.querySelector(".comment-input-box");
        const allholder = e.target.closest(".post-box");

        let clone = cm.cloneNode(true);

        if(!(allholder.nextSibling.className == "comment-input-box")) {
            allholder.after(clone);
            aux = allholder.nextSibling;
            verifyMargin();
            tgg = false;
        } else {
            aux = allholder.nextSibling;
            verifyMargin();
        }

        console.log(aux);
        console.log(clone);
        console.log(tgg);

        function verifyMargin() {
            if (aux.style.marginTop === "-124px") {
                console.log("124");
                aux.setAttribute("style", "margin-top: -26px !important; transition: .7s cubic-bezier(0.72, 0, 0.11, 0.99);");
            } else if (aux.style.marginTop === "-26px" && tgg) {
                console.log("tgg");
                aux.setAttribute("style", "margin-top: -26px !important; transition: .7s cubic-bezier(0.72, 0, 0.11, 0.99);");
            } else if (aux.style.marginTop === "-26px" && !tgg) {
                console.log("!!tgg");
                aux.setAttribute("style", "margin-top: -124px !important; transition: .7s cubic-bezier(0.72, 0, 0.11, 0.99);");
            }
        }
    });
});

