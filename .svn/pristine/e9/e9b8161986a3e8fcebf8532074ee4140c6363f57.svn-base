/* javascript for CE pages */

// adjust paging buttons to align to middle
function adjustNewsPaging(pagingListId, pagingViewId) {
    var li = $(pagingListId).children('li');
    var count = 0;
    if (li != null) count = $(pagingListId).children('li').length;
    var paddingLeft = 170 - count * 10; // this is empirical formula
    if (paddingLeft < 0) paddingLeft = 0;
    $(pagingViewId).css('padding-left', paddingLeft + 'px');
}
