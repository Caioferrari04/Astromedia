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

        document.querySelector('textarea').addEventListener('change', e => {
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
        
            window.location.reload()
        }
    }

    expandirComentario.addEventListener('click', clickInicial, { once: true })
}
