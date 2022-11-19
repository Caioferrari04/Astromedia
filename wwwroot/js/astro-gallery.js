const $galleryContainer = document.getElementById("gallery-container");

const lg = lightGallery($galleryContainer, {
speed: 500,
plugins: [lgThumbnail],
enableDrag: false,
enableSwipe: false
});

lg.outer.on("click", (e) => {
    const $item = lg.outer.find(".lg-current .lg-image");
    if (
        e.target.classList.contains("lg-image") ||
        $item.get().contains(e.target)
    ) {
        lg.goToNextSlide();
    }
});