const denunciar = async eventOriginal => {
    const modalWrapper = document.getElementById('modal-denuncia');
    const dataUrl = modalWrapper.getAttribute('data-url');
    const dataId = eventOriginal.target.parentElement.getAttribute('data-id');
    const dataTipo = eventOriginal.target.parentElement.getAttribute('data-tipo');

    const response = await fetch(`${dataUrl}/${dataId}`);

    modalWrapper.innerHTML = await response.text();
    document.body.classList.add('modal-open');

    modalWrapper.querySelector(`#${dataTipo}_Id`).value = dataId;

    modalWrapper.querySelector('textarea').addEventListener('input', e => {
        e.target.setAttribute('style', 'height:' + (e.target.scrollHeight) + 'px');
        e.target.addEventListener('input', OnInput, false);
    })

    const closeModal = () => {
        modalWrapper.innerHTML = "";
        document.body.classList.remove('modal-open');
    }

    modalWrapper.querySelector('.close-btn').addEventListener('click', closeModal);
    modalWrapper.querySelector('#botao-cancelar-denuncia').addEventListener('click', closeModal);

    modalWrapper.querySelector('#form-denuncia').addEventListener('submit', async event => {
        event.preventDefault();

        const body = new FormData(event.target);
        const fetchConfig = { method: 'POST', body };

        const response = await fetch(event.target.action, fetchConfig);

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
    
        Notiflix.Notify.success('Denuncia realizada com sucesso.');
        eventOriginal.target.dispatchEvent(new Event('exclusao', { bubbles: true }))
        closeModal();
    });
}

const DROPDOWN_ATIVO = "https://icongr.am/material/menu-up.svg?size=20&color=ffffff";
const DROPDOWN_INATIVO = "https://icongr.am/material/menu-down.svg?size=20&color=ffffff";
const DROPDOWNS = document.querySelectorAll('.dropdown-edicao');
const POSTAGENS = document.querySelectorAll('.post-box');
const TEMPLATE_EDICAO = document.getElementById('template-edicao')?.cloneNode(true);

POSTAGENS.forEach(postagem => {
    postagem.addEventListener('atualizar', () => {
        const templateOriginal = document.getElementById('template-edicao');
        if (templateOriginal) {
            templateOriginal.querySelector('#template-botao-cancelar').dispatchEvent(new Event('click'));
            if (templateOriginal.id == 'template-edicao') {
                templateOriginal.remove();
            }
        };

        const valorOriginal = {
            texto: postagem.querySelector('.post-text')?.innerHTML.trim(),
            img: postagem.querySelector('.post-img')?.firstElementChild.src
        }
        
        const novoTemplate = TEMPLATE_EDICAO.cloneNode(true);
        postagem.replaceWith(novoTemplate);
        novoTemplate.classList.add('template-visivel');

        const textareaEdicao = novoTemplate.querySelector('textarea');
        textareaEdicao.style.height = document.querySelector('textarea').style.height;
        textareaEdicao.value = valorOriginal.texto;
        const linkImagem = novoTemplate.querySelector('#LinkImagem-template')
        if (valorOriginal.img) {
            const imgPreview = novoTemplate.querySelector('#template-img');
            imgPreview.src = valorOriginal.img;
            imgPreview.parentElement.style.display = 'block';
            linkImagem.value = valorOriginal.img;
        }

        const astroId =  document.getElementsByName('AstroId')[0].value;

        novoTemplate.querySelector('input[name="AstroId"]').value = astroId ? astroId : postagem.getAttribute('data-astro-id');
        novoTemplate.querySelector('#template-botao-cancelar').addEventListener('click', () => novoTemplate.replaceWith(postagem));

        const imagemInput = novoTemplate.querySelector('#imagem-template');
        const imagemForm = novoTemplate.querySelector('#upload-imagem-template');
        const form = novoTemplate.querySelector('#postform-template');
        const templateImg = novoTemplate.querySelector('#template-img');
        form.querySelector('input[name="Id"]').value = postagem.id.split('-')[1];

        imagemInput.addEventListener('input', () => imagemForm.dispatchEvent(new Event('submit')))
        imagemForm.addEventListener('submit', async event => {
            event.preventDefault();
            const resposta = await saveImg(event.target);
            templateImg.setAttribute('src', resposta.linkImagem);
            linkImagem.value = resposta.linkImagem;
            novoTemplate.querySelector('#template-img-preview').style.display = 'block';
        });
        
        form.addEventListener("submit",  event => {
            event.preventDefault();
            handleFormSubmit(event.target, event.target.action, false);
            
            postagem.querySelector('.post-text').innerHTML = textareaEdicao.value;
            const img = postagem.querySelector('.post-img')?.firstElementChild;
            if (img && templateImg.getAttribute('src')) {
                img.src = templateImg.getAttribute('src');
            }

            novoTemplate.replaceWith(postagem);
        });
    });

    postagem.addEventListener('exclusao', () => postagem.remove());
})

for (const dropdown of document.getElementsByClassName('dropdown-acoes')) {
    dropdown.addEventListener('click', event => {
        const isAtivo = event.target.src.includes(DROPDOWN_ATIVO);
        event.target.src = isAtivo ? DROPDOWN_INATIVO : DROPDOWN_ATIVO;
        console.log('aqui')
    
        DROPDOWNS.forEach(el => {
            el.previousElementSibling.src = el.previousElementSibling.src === DROPDOWN_ATIVO && el === event.target.nextElementSibling ? DROPDOWN_ATIVO : DROPDOWN_INATIVO;
            el.classList.toggle('dropdown-edicao-ativo', el === event.target.nextElementSibling)
        });
    
        if (isAtivo) {
            event.target.nextElementSibling.classList.remove('dropdown-edicao-ativo');
        }
    });
}

for (const item of document.getElementsByClassName('atualizar-postagem')) {
    item.addEventListener('click', (e) => e.target.dispatchEvent(new Event('atualizar', { bubbles: true })));
}

for (const item of document.getElementsByClassName('excluir-postagem')) {
    item.addEventListener('click', (e) => {
        const ok = async () => {
            const id = e.target.getAttribute('data-id');
            const body = new FormData();
            body.append('id', id);

            const fetchConfig = { method: 'POST', body };
            const response = await fetch('/Feed/DeletePostagem', fetchConfig);
    
            if (!response.ok) {
                handleError(['Houve um erro com sua requisição, tente novamente mais tarde!']);
                return;
            }
        
            const json = await response.json();
        
            if (!json.success) {
                const mensagens = [];
                json.errors.forEach(erro => mensagens.push(erro));
                handleError(mensagens);
                return;
            }
        
            Notiflix.Notify.success('Postagem excluída com sucesso.');
            e.target.dispatchEvent(new Event('exclusao', { bubbles: true }))
        }

        Notiflix.Confirm.show(
            'Excluir postagem',
            'Você tem certeza que deseja excluir essa postagem?',
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
}

for (const item of document.getElementsByClassName('historico-postagem')) {
    item.addEventListener('click', async event => {
        const modalWrapper = document.getElementById('modal-log');
        const response = await fetch(`${modalWrapper.getAttribute('data-url')}/${event.target.getAttribute('data-id')}`)
        modalWrapper.innerHTML = await response.text()
        item.parentElement.previousElementSibling.dispatchEvent(new Event('click'));
        document.body.classList.add('modal-open');

        modalWrapper.querySelector('.close-btn').addEventListener('click', () => {
            modalWrapper.innerHTML = ""
            document.body.classList.remove('modal-open');
        });
    });
}

for (const item of document.getElementsByClassName('report-btn')) {
    item.firstElementChild.addEventListener('click', denunciar)
}
