const itens = document.querySelectorAll('.item img');

for (const item of itens) {
    item.addEventListener("mouseenter", openTooltip);
    item.addEventListener("mouseout", closeTooltip);
}

function openTooltip() {
    let tooltip = this.parentNode.getElementsByClassName('tooltip')[0];
    tooltip.style.display = "inline";
}

function closeTooltip() {
    let tooltip = this.parentNode.getElementsByClassName('tooltip')[0];
    tooltip.style.display = "none";
}