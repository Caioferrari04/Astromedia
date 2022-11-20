const enviar = async (url, postagemId) => {
    const form = new FormData()
    form.append('postagemId', postagemId)

    const response = await fetch(url, {
        method: 'POST',
        body: form
    })

    if (!response.ok) {
        handleError(['Houve um erro com sua requisição, tente novamente mais tarde!'])
        return false;
    }

    const json = await response.json()

    if (!json.sucesso) {
        const mensagens = []
        json.mensagem.forEach(erro => mensagens.push(erro))
        handleError(mensagens)
        return false;
    }
    
    return true;
}

const submitDislike = async (e) => {
    if (await enviar('/Feed/RemoverLikePostagem', e.target.getAttribute('data-id'))) {
        e.target.src = "/icons/heart.svg"
        e.target.nextElementSibling.textContent = parseInt(e.target.nextElementSibling.textContent) - 1
        e.target.addEventListener('click', submitLike, { once: true })
    }
}

const submitLike = async (e) => {
    if (await enviar('/Feed/AdicionarLikePostagem', e.target.getAttribute('data-id'))) {
        e.target.src = "/icons/heart-fill.svg"
        e.target.nextElementSibling.textContent = parseInt(e.target.nextElementSibling.textContent) + 1
        e.target.addEventListener('click', submitDislike, { once: true })
    }

}

const likesBtns = document.getElementsByClassName('btn-like')
const dislikeBtns = document.getElementsByClassName('btn-dislike')


for (const likeBtn of likesBtns) {
    likeBtn.addEventListener('click', submitLike, { once: true })
}

for (const dislikeBtn of dislikeBtns) {
    dislikeBtn.addEventListener('click', submitDislike, { once: true })
}
