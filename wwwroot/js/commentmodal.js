const renderComment = document.querySelector('#rendercomment');
const commentmodalBtn = document.querySelector('.comment-modal');

$(commentmodalBtn).click(function () {
    var url = $(renderComment).data('url');

    $.get(url, function (data) {
        $(renderComment).html(data);
        $(renderComment).show();

        $(window).click(function (event) {
            if (event.target == modalblock) {
                $(renderComment).hide();
            }
        });
    });
});

function closeModal() {
    $(renderComment).hide(); 
};