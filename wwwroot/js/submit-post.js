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
				createPost(json.data)
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
	let div1 = document.createElement("div");
	let div2 = document.createElement("div");
	let div3 = document.createElement("div");
	let div4 = document.createElement("div");
	let image1 = document.createElement("img");
	let div5 = document.createElement("div");
	let p1 = document.createElement("p");
	let p2 = document.createElement("p");
	let div6 = document.createElement("div");
	let image2 = document.createElement("img");
	let p3 = document.createElement("p");
	let div8 = document.createElement("div");
	let div9 = document.createElement("div");
	let image4 = document.createElement("img");
	let div10 = document.createElement("div");
	let image5 = document.createElement("img");

	div1.appendChild(div2);
	div2.appendChild(div3);
	div3.appendChild(div4);
	div3.appendChild(div6);
	div4.appendChild(image1);
	div4.appendChild(div5);
	div5.appendChild(p1);
	div5.appendChild(p2);
	div6.appendChild(image2);
	div2.appendChild(p3);
	div2.appendChild(div8);
	div8.appendChild(div9);
	div9.appendChild(image4);
	div8.appendChild(div10);
	div10.appendChild(image5);

	div1.setAttribute("id", "usr-all-post");
	div1.setAttribute("class", "post-box");
	div2.setAttribute("class", "post-content");
	div3.setAttribute("class", "between-config");
	div4.setAttribute("class", "post-header-info is-left");
	image1.setAttribute("src", profilePicture);
	image1.setAttribute("class", "usr-post-pic");
	div5.setAttribute("class", "info-col");
	p1.setAttribute("class", "usr-post-name");
	p2.setAttribute("class", "usr-post-date");
	p1.innerText = userName;
	p2.innerText = data.dataPostagem;
	div6.setAttribute("class", "report-btn is-vertical-align is-right");
	image2.setAttribute("src", "https://icongr.am/clarity/flag.svg?size=128&color=ffffff");
	p3.setAttribute("class", "post-text");
	p3.innerText = data.texto;
	div8.setAttribute("class", "between-config");
	div9.setAttribute("class", "usr-footer-comment is-vertical-align is-left");
	image4.setAttribute("class", "post-footer-icon");
	image4.setAttribute("src", "https://icongr.am/fontawesome/comment-o.svg?size=128&color=ffffff");
	div10.setAttribute("class", "usr-footer-likes is-vertical-align is-right");
	image5.setAttribute("class", "post-footer-icon");
	image5.setAttribute("src", "https://icongr.am/clarity/heart.svg?size=128&color=ffffff");

	if(data.imagem != null) {
		let div7 = document.createElement("div");
		let image3 = document.createElement("img");
		div2.appendChild(div7);
		div7.appendChild(image3);
		div7.setAttribute("class", "post-img");
		image3.setAttribute("src", data.imagem);
	}

	const posts = document.querySelectorAll(".post-box");
	const postsContainer = document.querySelector("#posts");

	posts.length == 0 ? postsContainer.appendChild(div1) : posts[posts.length-1].parentNode.insertBefore(div1, posts[posts.length-1].nextSibling);

	form.elements[0].value = "";
}
	
	