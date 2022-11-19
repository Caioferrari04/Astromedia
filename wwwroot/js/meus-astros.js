Splide.defaults = {
    // focus  : 'center',
    type   : 'slide',
    perPage: 5,
    // arrows : false,
    padding: '6rem',
    gap    : '3rem',
    lazyLoad: 'nearby',
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
            gap: '1rem',
            padding: '0'
        },
        768: {
            arrows: false,
        },
        425: {
            perPage: 2
        }
    }
}

if (document.querySelector('.astros-usuario')) new Splide('.astros-usuario').mount();
new Splide('.astros-recomendacoes').mount();
new Splide('.astros-famosos').mount();

