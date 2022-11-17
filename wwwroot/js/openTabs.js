const tabs = document.getElementsByClassName("tab");
const tabsContent = document.getElementsByClassName("tab-content");

for (const tab of tabs) {
    tab.addEventListener("click", (e) => {
        for (const tab of tabsContent) {
            const tabNav = document.getElementById(tab.getAttribute('data-id'));
            tabNav.classList.toggle('active', tabNav == e.target)
            tab.classList.toggle('tab-content-active', tab.getAttribute('data-id') == e.target.id);
        }
    });
}
