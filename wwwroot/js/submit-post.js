const form = document.querySelector("#postform");

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
					messages.push(json.errors[i].errorMessage)
				}
				console.log(messages)
				handleError(messages)
			}
			else {
				console.log("Sucesso")
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