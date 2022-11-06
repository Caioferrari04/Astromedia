const img_btn = document.getElementById('img-up');
const img_preview = document.getElementById('post-img-preview');

img_btn.addEventListener('change', () => {
    getImgData();
    console.log('post-imgcon', img_preview.innerHTML);
});

function getImgData() {
    const files = img_btn.files[0];
    if (files) {
        const fileReader = new FileReader();
        fileReader.readAsDataURL(files);
        fileReader.addEventListener("load", function() {
            img_preview.style.display = "block";
            img_preview.innerHTML = '<img src="' + this.result + '" />';
        });    
    }
}