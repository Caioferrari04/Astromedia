const form = document.querySelector("#postform");

form.addEventListener("submit",  (event) => {
    handleFormSubmit(event);
});

function handleFormSubmit(event) {
    event.preventDefault();
	
	let form2 = event.currentTarget;

	let image = document.querySelector("#post-img-preview").getElementsByTagName("img")[0];

	let data = {};
	data.Texto = form2.elements[0].value;
	data.DataPostagem = new Date(Date.now());
	data.Imagem = image == null ? null : image.src;
	data.UsuarioDTO = null;

	console.log(data)
	
	// var texto = form2.elements[0].value;
    // const formData = new FormData(event.currentTarget);

    fetch(
		'https://localhost:7036/Feed/SavePostagem', 
		{
			method: "POST",
			body: JSON.stringify(data),
			headers: {"Content-type": "application/json; charset=UTF-8"}
		}
	)
}

//  function postFormDataAsJson(texto) {
	
// 	// test.Texto = texto;
//     // const plainFormData = Object.fromEntries(formData.entries());
    
// 	// const formDataJsonString = JSON.stringify(texto);

// 	// const fetchOptions = {
// 	// 	method: "POST",
// 	// 	headers: {
// 	// 		"Content-Type": "application/json",
// 	// 		Accept: "application/json",
// 	// 	},
// 	// 	body: test,
// 	// };

// 	var _data = {};
// 	_data.Texto = "alex";
// 	_data.DataPostagem = new Date(Date.now());
// 	_data.UsuarioDTO = null;

	

// }