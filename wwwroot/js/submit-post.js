const form = document.querySelector("#postform");
let profilePicture = "";
let userName = "";

form.addEventListener("submit",  (event) => {
	event.preventDefault();
    handleFormSubmit(event);
});

function handleFormSubmit(event) {	
	let form2 = event.currentTarget;

	let data = {};
	data.Texto = form2.elements[0].value;
	data.UsuarioId = null;
	data.AstroId = form2.elements[3].value;
	userName = form2.elements[4].value;
	profilePicture = form2.elements[5].value;

	const file = document.getElementById('img-up').files[0];
	(file == null) ? data.Imagem = null : data.Imagem = saveImg(file);

	if(data.Texto.split(" ").join("") != "" || data.Imagem != null) {
		(data.Texto.split(" ").join("") == "") ? data.Texto = null : data.Texto = data.Texto;
		fetch(
			'/Feed/SavePostagem', 
			{
				method: "POST",
				body: JSON.stringify(data),
				headers: {"Content-type": "application/json; charset=UTF-8"}
			}
		).then(response => {
			
			return response.json();
		}).then(json => {
			if(!json.success) {
				let messages = []
				for (let i = 0; i<json.errors.length; i++) {
					messages.push(json.errors[i])
				}
				
				handleError(messages)
			}
			else {
				// createPost(json.data)
				window.location.reload();
			}
		});
	}
}

function saveImg(file) {
	let formData = new FormData();
	formData.append('image', file);

	let imgUrl = fetch('https://api.imgur.com/3/image', {
		method: "POST",
		headers: {
			Authorization: "Client-ID aca6d2502f5bfd8",
		},
		body: formData
	});

	if(!imgUrl.ok) {
		let errorsMessage = ["Não foi possível salvar a postagem"];
		handleError(errorsMessage)
		return null;
	}

	return imgUrl.json().link;
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
	
	