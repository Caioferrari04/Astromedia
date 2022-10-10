const form = document.querySelector("#postform");

form.addEventListener("submit",  (event) => {
    handleFormSubmit(event);
});

async function handleFormSubmit(event) {
    event.preventDefault();
	
	let form2 = event.currentTarget;

	let data = {};
	data.Texto = form2.elements[0].value;
	data.DataPostagem = new Date(Date.now());
	data.UsuarioDTO = null;


	const file = document.getElementById('img-up').files[0];
	file == null ? data.Imagem = null : data.Imagem = await saveImg(file);

	console.log(data)
	
    let response = fetch(
		'https://localhost:7036/Feed/SavePostagem', 
		{
			method: "POST",
			body: JSON.stringify(data),
			headers: {"Content-type": "application/json; charset=UTF-8"}
		}
	)

	if(!response.ok) {
		handleError()
	}
}

async function saveImg(file) {
	let formData = new FormData();
	formData.append('image', file);

	let imgUrl = await fetch('https://api.imgur.com/3/image/', {
		method: "POST",
		headers: {
			Authorization: "Client-ID aca6d2502f5bfd8",
		},
		body: formData
	});

	if(!imgUrl.ok) {
		handleError()
		return null;
	}

	return await imgUrl.json();

	// fetch('https://api.imgur.com/3/image/', {
	// 	method: "POST",
	// 	headers: {
	// 		Authorization: "Client-ID aca6d2502f5bfd8",
	// 	},
	// 	body: formData
	// }).then(data => data.json()).then(data => {
	// 	// img.src = data.data.link
	// 	img.src = "oi"
	// 	console.log(data)
	// });
}

function handleError() {
	let textError = document.querySelector(".error-message")
	if(textError == null) {
		form.innerHTML += "<p class='error-message'>Erro ao salvar postagem</p>"
	}
}