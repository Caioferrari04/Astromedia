function openTooltip(event) {
    const img = event.target;
    let tooltip = img.parentNode.getElementsByClassName('tooltip')[0];
    tooltip.style.display = "inline";
}

function closeTooltip(event) {
    const img = event.target;
    let tooltip = img.parentNode.getElementsByClassName('tooltip')[0];
    tooltip.style.display = "none";
}