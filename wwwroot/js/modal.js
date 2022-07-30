const renderModal = document.getElementById('rendermodal');
const modalBtn = document.getElementById('modalbtn');

$(modalBtn).click(function () {
    var url = $(renderModal).data('url');

    $.get(url, function (data) {
        $(renderModal).html(data);
        $(renderModal).show();

        $(window).click(function (event) {
            if (event.target == modalblock) {
                $(renderModal).hide();
            }
        });
    });
});

function closeModal() {
    $(renderModal).hide(); 
};