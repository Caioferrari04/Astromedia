const renderComment = document.querySelector('#rendercomment');
const commentmodalBtn = document.querySelector('.comment-modal');

$(commentmodalBtn).click(() => {
    var url = $(renderComment).data('url');

    $.get(url, data => {
        $(renderComment).html(data);
        $(renderComment).show();

        $(window).click(event => {
            if (event.target == modalblock) {
                $(renderComment).hide();
            }
        });
    });
});

function closeModal() {
    $(renderComment).hide(); 
};