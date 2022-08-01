const tabs = document.getElementsByClassName("button-nav");

for (const tab of tabs) {
    tab.addEventListener("click", openTab);
}

const tabsContent = document.getElementsByClassName("tab-content");

function openTab() {
    for (let i = 0; i < tabs.length; i++) {
        if(this.id === tabs[i].id) {
            tabs[i].classList.add("active");
            tabsContent[i].classList.add("tab-content-active");
        }
        else {
            tabs[i].classList.remove("active");
            tabsContent[i].classList.remove("tab-content-active");
        }
    }
}