const recomendacoes = document.getElementsByClassName('recomendacao--mobile');



new Splide('.splide', {
    start  : recomendacoes.length < 3 ? recomendacoes.length : 0,
    focus  : 'center',
    type   : 'slide',
    perPage: 3,
    arrows : false,
    gap    : '1rem',
    padding: '1.5rem',
    pagination: false,
    // autoWidth : true,
}).mount();