const commentmodalBtn = document.querySelector('.comment-modal');
const commentmodalBtnAll = document.querySelectorAll('.comment-modal');
const bodyScroll = document.getElementsByTagName('body')[0];

function closeModal() {
    bodyScroll.classList.remove('block-scroll');
    $(renderComment).hide(); 
};

Array.from(commentmodalBtnAll).forEach(commentmodalBtn => {
    $(commentmodalBtn).click(async e => {

        const renderComment = document.getElementById('rendercomment');
        const response = await fetch(`${renderComment.getAttribute('data-url')}/${e.target.getAttribute('data-id')}`)
        renderComment.innerHTML = await response.text()
        
        if (!renderComment.querySelector('.post-img.commentmodal-img')) {
            renderComment.querySelector('.commentmodal-content').classList.add('modo-flexbox')
        }

        bodyScroll.classList.add('block-scroll');
    
        $(window).click(event => {
            if (event.target == renderComment.children[0]) {
                bodyScroll.classList.remove('block-scroll');
                renderComment.innerHTML = "";
            }
        });

        const likesBtns = renderComment.querySelectorAll('.btn-like')
        const dislikeBtns = renderComment.querySelectorAll('.btn-dislike')

        likesBtns.forEach(el => el.addEventListener('click', submitLike, { once: true }))
        dislikeBtns.forEach(el => el.addEventListener('click', submitDislike, { once: true }))

        renderComment.querySelector('.close-comment-btn').addEventListener('click', e => {
            renderComment.innerHTML = ''
            bodyScroll.classList.remove('block-scroll');
        })

        expandComment();
    });
});

