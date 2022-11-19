const button = document.getElementById('button');
const div = document.getElementsByClassName('justificar')[0];

const divs2 = document.getElementsByClassName("margin-bottom");

let i = divs2.length;


button.addEventListener('click', () => {
    createTextArea()
})

function createTextArea() {
    let element = document.createElement("div");
    element.classList.add("margin-bottom")
    element.innerHTML = `
        <div id="MarcosHistoricosDiv_${i}"></div>
        <textarea id="MarcosHistoricos_${i}" name="MarcosHistoricos" style="display: none;"></textarea>
    `
    div.appendChild(element)
    element.lastChild.value = "";


    const editor = window.pell.init({
        element: element.getElementsByTagName("div")[0],
        onChange: html => {
            editor.parentNode.getElementsByTagName("textarea")[0].value = html
        },
        defaultParagraphSeparator: 'div',
        styleWithCSS: false,
        actions: [
            'bold',
            'underline'
        ],
    })
}
