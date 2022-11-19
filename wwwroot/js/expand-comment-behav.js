var expcmbtn = document.querySelector(".expand-comment-btn");
let tt = 0;

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
        if (window.screen.width > 600 && aux.style.marginTop === "-115px") {
            aux.style.marginTop = "-5px";
            aux.scrollIntoView({behavior: "smooth", block: "start", inline: "nearest"});
        } else if (window.screen.width > 600 && aux.style.marginTop === "-5px" && tgg) {
            aux.style.marginTop = "-5px";
        } else if (window.screen.width > 600 && aux.style.marginTop === "-5px" && !tgg) {
            aux.style.marginTop = "-115px";
        } 
        
        else if (window.screen.width <= 600 && aux.style.marginTop === "-115px") {
            aux.style.marginTop = "-5px";
            aux.scrollIntoView({behavior: "smooth", block: "start", inline: "nearest"});
        } else if (window.screen.width <= 600 && aux.style.marginTop === "-5px" && !tgg) {
            aux.style.marginTop = "-5px";
        }
    }

    const cmmform = document.querySelector("#commentform");
    if(tt == 0) {
        cmmform.addEventListener("submit", e => {
            e.preventDefault();
            handleFormSubmit(e.target, e.target.action);
        });
    }

    async function handleFormSubmit(form, url) {	
        const body = new FormData(form);
        const fetchConfig = { method: 'POST', body };
    
        const response = await fetch(url, fetchConfig);
        if (!response.ok) {
            handleError(['Houve um erro com sua requisição, tente novamente mais tarde!']);
            return;
        }
    
        const json = await response.json();
    
        if (!json.success) {
            const mensagens = [];
            json.errors.forEach(erro => mensagens.push(erro));
            handleError(mensagens);
            return;
        }
    
        window.location.reload();
    }
    
    tt = 1;
});