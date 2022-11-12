const expcmbtn = document.querySelector(".expand-comment-btn");

expcmbtn.addEventListener("click", e => {
    console.log("click");
    const cm = document.querySelector(".comment-input-box");
    const allholder = document.querySelector(".commentmodal-holder");

    console.log(allholder);
    
    let clone = cm.cloneNode(true);
    clone.removeAttribute("id");

    // if(!(allholder.nextSibling.className == "comment-input-box")) {
    //     allholder.after(clone);
    //     aux = allholder.nextSibling;
    //     setTimeout(verifyMargin, 100);
    //     tgg = false;
    // } else {
    //     aux = allholder.nextSibling;
    //     verifyMargin();
    // }

    // document.querySelectorAll("textarea").forEach(textarea => {
    //     textarea.addEventListener('change', (e) => {
    //       e.target.setAttribute("style", "height:" + (e.target.scrollHeight) + "px;");
    //       e.target.addEventListener("input", OnInput, false);
    //     })
      
    //     textarea.dispatchEvent(new Event('change'));
    // })

    // function verifyMargin() {
    //     if (aux.style.marginTop === "-119px") {
    //         console.log("122");
    //         aux.style.marginTop = "-26px";
    //     } else if (aux.style.marginTop === "-26px" && tgg) {
    //         console.log("tgg");
    //         aux.style.marginTop = "-26px";
    //     } else if (aux.style.marginTop === "-26px" && !tgg) {
    //         console.log("!!tgg");
    //         aux.style.marginTop = "-119px";
    //     }
    // }
    
});