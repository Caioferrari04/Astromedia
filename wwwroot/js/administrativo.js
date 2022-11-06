// Initialize pell on an HTMLElement
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
