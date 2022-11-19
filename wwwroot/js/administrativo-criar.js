// Initialize pell on an HTMLElement
const textAreaOriginal = document.getElementById('Curiosidades');
const textAreaMarcosHistoricos = document.getElementById('MarcosHistoricos_0');
textAreaMarcosHistoricos.value = "";

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

const editor2 = window.pell.init({
    element: document.getElementById('MarcosHistoricosDiv_0'),
    onChange: html => textAreaMarcosHistoricos.value = html,
    defaultParagraphSeparator: 'div',
    styleWithCSS: false,
    actions: [
        'bold',
        'underline'
    ],
})
