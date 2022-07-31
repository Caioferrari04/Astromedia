const tabs = document.getElementsByClassName("button-nav");
const tabsContent = document.getElementsByClassName("tab-content");
function openTab(numberTab) {
    for (let i = 0; i < tabs.length; i++) {
        if(numberTab == i) {
            tabs[i].classList.add("active");
            tabsContent[i].classList.add("tab-content-active");
        }

        else {
            tabs[i].classList.remove("active");
            tabsContent[i].classList.remove("tab-content-active");
        }

    }
}