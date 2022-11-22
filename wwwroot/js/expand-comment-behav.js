function expandComment() {
    const expandirComentario = document.querySelector('.expand-comment-btn')

    const clickInicial = () => {
        const commentBox = document.querySelector('.comment-input-box')
        const allholder =  document.querySelector('.commentmodal-holder')
    
        const clone = commentBox.cloneNode(true)
        clone.classList.remove('hide')
    
        allholder.prepend(clone)
        new Promise(r => setTimeout(r, 200)).then(() => clone.classList.remove('comment-input-box-hidden'))
    
        expandirComentario.addEventListener('click', () => {
            clone.classList.add('comment-input-box-hidden')
            new Promise(r => setTimeout(r, 700)).then(() => clone.remove())
            expandirComentario.addEventListener('click', clickInicial, { once: true })
        })

        document.querySelector('textarea').addEventListener('input', e => {
            e.target.setAttribute('style', 'height:' + (e.target.scrollHeight) + 'px')
            e.target.addEventListener('input', OnInput, false)
        })

        const cmmform = document.querySelector('#commentform')
        cmmform.addEventListener('submit', e => {
            e.preventDefault()
            handleFormSubmit(e.target, e.target.action)
        })

        const handleFormSubmit = async (form, url) => {	
            const body = new FormData(form)
            const fetchConfig = { method: 'POST', body }
        
            const response = await fetch(url, fetchConfig)
            if (!response.ok) {
                handleError(['Houve um erro com sua requisição, tente novamente mais tarde!'])
                return
            }
        
            const json = await response.json()
        
            if (!json.success) {
                const mensagens = []
                json.errors.forEach(erro => mensagens.push(erro))
                handleError(mensagens)
                return
            }

            const novoComentario = document.createElement('div');
            novoComentario.classList.add('post-content', 'commentmodal-comment')
            
            novoComentario.innerHTML = /* HTML */
            `
            <div class="between-config">
                <div class="post-header-info is-left">

                    <img class="usr-post-pic commentmodal-usrpic" src="${json.dados.fotoUsuario}" />
                    <div class="info-col">
                        <p class="commentmodal-usrname usr-post-name">${json.dados.nomeUsuario}</p>
                        <p class="commentmodal-usrdate usr-post-date">${json.dados.dataPostagem}</p>
                    </div>
                </div>
                <img src="/icons/trash.svg" class="dropdown-acoes excluir-comentario acoes-btn" data-id="${json.dados.id}"/>
            </div>
            <p class="commentmodal-text">
                ${json.dados.texto}
            </p>
            <div class="commentmodal-bottom">
                <div class="usr-footer-likes is-vertical-align is-right">
                    <img class="post-footer-icon btn-like"  data-id="${json.dados.id}" data-url="/Feed/AdicionarLikeComentario" data-tipo="Comentario" src="/icons/heart.svg" />
                    <small class="text-light likes">0</small>
                </div>
            </div>
            `

            expandirComentario.dispatchEvent(new Event('click'))
            allholder.append(novoComentario);
            allholder.scrollTop = allholder.scrollHeight;

            novoComentario.querySelector('.btn-like').addEventListener('click', submitLike, { once: true })
            novoComentario.querySelector('.excluir-comentario').addEventListener('click', e => {
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
            })

            // window.location.reload()
        }
    }

    expandirComentario.addEventListener('click', clickInicial, { once: true })
}
