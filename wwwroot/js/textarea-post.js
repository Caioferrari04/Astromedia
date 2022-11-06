function OnInput() {
  this.style.height = "auto";
  this.style.height = (this.scrollHeight) + "px";
}
document.querySelectorAll("textarea").forEach(textarea => {
  textarea.addEventListener('change', (e) => {
    e.target.setAttribute("style", "height:" + (e.target.scrollHeight) + "px;");
    e.target.addEventListener("input", OnInput, false);
  })

  textarea.dispatchEvent(new Event('change'));
})