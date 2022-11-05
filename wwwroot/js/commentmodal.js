const renderComment = document.querySelector('#rendercomment');
const commentmodalBtn = document.querySelector('.comment-modal');
const commentmodalBtnAll = document.querySelectorAll('.comment-modal');
const bodyScroll = document.getElementsByTagName('body')[0];

function closeModal() {
    bodyScroll.classList.remove('block-scroll');
    $(renderComment).hide(); 
};

Array.from(commentmodalBtnAll).forEach(commentmodalBtn => {
    $(commentmodalBtn).click(() => {
        var url = $(renderComment).data('url');
    
        bodyScroll.classList.add('block-scroll');
    
        $.get(url, data => {
            $(renderComment).html(data);
            $(renderComment).show();
    
            $(window).click(event => {
                if (event.target == modalblock) {
                    bodyScroll.classList.remove('block-scroll');
                    $(renderComment).hide();
                }
            });
        });
    });
});

