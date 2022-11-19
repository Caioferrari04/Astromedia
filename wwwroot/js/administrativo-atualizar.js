const textAreaOriginal = document.getElementById('Curiosidades');


const editor = window.pell.init({
    element: document.getElementById('CuriosidadesTextArea'),
    onChange: html => textAreaOriginal.value = html,
    defaultParagraphSeparator: 'div',
    styleWithCSS: false,
    actions: [
        'bold',
        'underline'
    ],
})

editor.content.innerHTML = textAreaOriginal.value;

const editors = [];
const divs = document.getElementsByClassName("margin-bottom");
console.log(divs[0].children[1].attributes.getNamedItem("Value").value)

for(let i = 0; i<divs.length; i++) {
    editors[i] = window.pell.init({
            element: document.getElementById('MarcosHistoricosDiv_'+ i),
            onChange: html => {
                editors[i].parentNode.getElementsByTagName("textarea")[0].value = html
            },
            defaultParagraphSeparator: 'div',
            styleWithCSS: false,
            actions: [
                'bold',
                'underline'
            ],
        })
    editors[i].content.innerHTML = divs[i].children[1].attributes.getNamedItem("Value").value;
    divs[i].children[1].value = editors[i].content.innerHTML;
}

