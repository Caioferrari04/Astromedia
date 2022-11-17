Splide.defaults = {
    // focus  : 'center',
    type   : 'slide',
    perPage: 5,
    // arrows : false,
    gap    : '5rem',
    classes: {
        arrow: 'splide__arrow flecha-splide'
    },
    breakpoints: {
        1900: {
            perPage: 4
        },
        1280: {
            perPage: 3
        },
        1024: {
            gap: '1rem'
        },
        768: {
            arrows: false
        },
        425: {
            perPage: 2
        }
    }
}

new Splide('.astros-usuario').mount();
new Splide('.astros-recomendacoes').mount();

