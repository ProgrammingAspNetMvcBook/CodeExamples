$(function () {

    $('.show-bid-history').click(function (evt) {
        $('<div class="bid-history">')
            .load($(this).attr('href'))
            .insertAfter(this);

        $('<a href="#">Hide Bid History</a>')
            .click(function() {
                $(this).siblings('.bid-history').hide();
                $(this).siblings('.show-bid-history').show();
                $(this).remove();
                return false;
            })
            .insertBefore(this);

        $(this).hide();
        
        evt.stopPropagation();
        return false;
    });
});