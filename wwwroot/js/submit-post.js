const form = document.querySelector("#postform");
let profilePicture = "";
let userName = "";

const imagemInput = document.getElementById('Imagem')
const imagemForm = document.getElementById('uploadImagem')
if (form) {
	imagemInput.addEventListener('input', () => imagemForm.dispatchEvent(new Event('submit')))
	imagemForm.addEventListener('submit', async event => {
		event.preventDefault();
		const resposta = await saveImg(event.target);
		document.getElementById('img-preview').setAttribute('src', resposta.linkImagem);
		document.getElementById('LinkImagem').value = resposta.linkImagem;
		document.getElementById('post-img-preview').style = 'display: block';
	});
	
	form.addEventListener("submit",  event => {
		event.preventDefault();
		handleFormSubmit(event.target, event.target.action);
	});
}


async function handleFormSubmit(form, url) {	
	const body = new FormData(form);
	const fetchConfig = { method: 'POST', body };

	const response = await fetch(url, fetchConfig);
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

	window.location.reload();
}

async function saveImg(form) {
	console.log(form);
	const formData = new FormData(form);

	const imgUrl = await fetch('/Feed/UploadImagem', {
		method: "POST",
		body: formData
	});

	if(!imgUrl.ok) {
		const errorsMessage = ["Não foi possível salvar a imagem!"];
		handleError(errorsMessage)
		return null;
	}

	return await imgUrl.json();
}

function handleError(errorsMessage) {
	for (let i = 0; i<errorsMessage.length; i++) {
		Notiflix.Notify.failure(errorsMessage[i]);
	}
}

function createPost(data) {
	const postagemNova = document.createElement('div');
	postagemNova.classList.add('post-box', 'is-vertical-align', 'is-horizontal-align')
	postagemNova.innerHTML = /* html */`
	<div class="post-content">
		<div class="between-config">
			<div class="post-header-info is-left">

				<img class="usr-post-pic" src="${profilePicture}" /> 
				<div class="info-col">
					<p class="usr-post-name">${userName}</p>
					<p class="usr-post-date">${data.dataPostagem}</p>
				</div> 
			</div>
			<div class="report-btn is-vertical-align is-right">
				<img src="https://icongr.am/clarity/flag.svg?size=128&color=ffffff" />
			</div>
		</div>
		<p class="post-text">
			${data.texto}
		</p>
		${data.imagem != null ? `<div class="post-img"><img src="${data.imagem}"/></div>` : ''}
		<div class="between-config">
			<div class="usr-footer-comment is-vertical-align is-left">
				<img class="post-footer-icon comment-modal" src="https://icongr.am/fontawesome/comment-o.svg?size=128&color=ffffff" />
				<a class="text-light comment-modal" href="javascript:void(0)">Ver Comentários</a>
			</div>
			<div class="usr-footer-likes is-vertical-align is-right">
				<img class="post-footer-icon" src="https://icongr.am/clarity/heart.svg?size=128&color=ffffff" />
				<small class="text-light likes">0</small>
			</div>
		</div>
	</div>
	`

	const postsContainer = document.getElementById("posts");

	document.querySelectorAll(".post-box").length == 0 ? postsContainer.appendChild(postagemNova) : postsContainer.prepend(postagemNova);

	form.elements[0].value = "";
}
	
	