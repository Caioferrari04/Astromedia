const rendermodal = document.getElementById('rendermodal');
const modalbtn = document.getElementById('modalbtn');

$(modalbtn).click(function() {
    var url = $(rendermodal).data('url');

    $.get(url, function(data) {
        $(rendermodal).html(data);
        $(rendermodal).show();
        
        $(window).click(function(event) {
            if (event.target == modalblock) {
                $(rendermodal).hide();
            }
        })
    })
})
