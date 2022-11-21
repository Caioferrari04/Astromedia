const commentmodalBtn = document.querySelector('.comment-modal');
const commentmodalBtnAll = document.querySelectorAll('.comment-modal');
const bodyScroll = document.getElementsByTagName('body')[0];

function closeModal() {
    bodyScroll.classList.remove('block-scroll');
    $(renderComment).hide(); 
};

Array.from(commentmodalBtnAll).forEach(commentmodalBtn => {
    $(commentmodalBtn).click(async e => {

        const renderComment = document.getElementById('rendercomment');
        const response = await fetch(`${renderComment.getAttribute('data-url')}/${e.target.getAttribute('data-id')}`)
        renderComment.innerHTML = await response.text()
        
        if (!renderComment.querySelector('.post-img.commentmodal-img')) {
            renderComment.querySelector('.commentmodal-content').classList.add('modo-flexbox')
        }

        bodyScroll.classList.add('block-scroll');
    
        $(window).click(event => {
            if (event.target == renderComment.children[0]) {
                bodyScroll.classList.remove('block-scroll');
                renderComment.innerHTML = "";
            }
        });

        const likesBtns = renderComment.querySelectorAll('.btn-like')
        const dislikeBtns = renderComment.querySelectorAll('.btn-dislike')

        likesBtns.forEach(el => el.addEventListener('click', submitLike, { once: true }))
        dislikeBtns.forEach(el => el.addEventListener('click', submitDislike, { once: true }))

        renderComment.querySelector('.close-comment-btn').addEventListener('click', e => {
            renderComment.innerHTML = ''
            bodyScroll.classList.remove('block-scroll');
        })

        for (const item of renderComment.querySelectorAll('.report-btn')) {
            item.firstElementChild.addEventListener('click', denunciar)
        }

        for (const item of renderComment.querySelectorAll('.commentmodal-comment')) {
            item.addEventListener('exclusao', () => item.remove());
        }

        renderComment.querySelectorAll('.excluir-comentario').forEach(el => el.addEventListener('click', e => {
            const ok = async () => {
                const id = e.target.getAttribute('data-id');
                const body = new FormData();
                body.append('id', id);
    
                const fetchConfig = { method: 'POST', body };
                const response = await fetch('/Feed/DeleteComment', fetchConfig);
        
                if (!response.ok) {
                    handleError(['Houve um erro com sua requisição, tente novamente mais tarde!']);
                    return;
                }
            
                const json = await response.json();
            
                if (!json.sucesso) {
                    const mensagens = [];
                    json.mensagem.forEach(erro => mensagens.push(erro));
                    handleError(mensagens);
                    return;
                }
            
                Notiflix.Notify.success('Comentário excluído com sucesso.');
                e.target.dispatchEvent(new Event('exclusao', { bubbles: true }))
            }
    
            Notiflix.Confirm.show(
                'Excluir comentário',
                'Você tem certeza que deseja excluir esse comentário?',
                'Sim',
                'Não',
                ok,
                null,
                {
                    backgroundColor: '#9600DB',
                    titleColor: '#d2d6dd',
                    messageColor: '#d2d6dd',
                    okButtonColor: '#d2d6dd',
                    okButtonBackground: '#d43939;',
                    cancelButtonColor: '#d2d6dd',
                    cancelButtonBackground: '#7300A8',
                }
            )
        }));

        expandComment();
    });
});

