const recomendacoes = document.getElementsByClassName('recomendacao--mobile');



new Splide('.splide', {
    start  : recomendacoes.length < 3 ? recomendacoes.length : 0,
    focus  : 'center',
    type   : 'loop',
    perPage: 3,
    arrows : false,
    gap    : '1rem',
    // padding: '1.5rem',
    pagination: true,
    classes: {
        pagination: 'splide__pagination paginacao-splide',
    },
    // autoWidth : true,
}).mount();