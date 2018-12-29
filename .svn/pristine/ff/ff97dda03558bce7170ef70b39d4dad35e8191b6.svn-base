function SetSwipeHanlders() {
    for (var i = 0; i < 10; i++) { // support up to 10 albums; album id starts from 1 to 10; no skipping
        var albumId = '#album' + i;
        var galleryId = '#gallery' + i;
        var pagingId = '#paging' + i;
        if ($(albumId).length > 0) { // this album has been defined in the page
            var swipeHandler = new Swipeable(albumId, galleryId, '.ce-photo-view', pagingId, '.photo-paging-item', '.button-left', '.button-right');
            swipeHandler.init();
            if (isIE()) adjustPagingForIE(pagingId);
        }
        else // album starts from 1 in ascending order with no skipping; so when we stop at first encounter when album is not found
            break;
    }
}

// update the album tab and sync things up
function setAlbumTab(albumsId, titlebarId, photoId, i) {
    var albums = $(albumsId + ' .album-item');
    albums.removeClass('show').addClass('hide');
    albums.eq(i).removeClass('hide').addClass('show');

    var titles = $(titlebarId + ' .album-title');
    titles.removeClass('show').addClass('hide');
    titles.eq(i).removeClass('hide').addClass('show');
    var summaries = $(titlebarId + ' .album-summary');
    summaries.removeClass('show').addClass('hide');
    summaries.eq(i).removeClass('hide').addClass('show');

    var photos = $(photoId + ' ul li');
    photos.removeClass('selected');
    photos.eq(i).addClass('selected');
}

function adjustPagingForIE(pagingId, top) {
    var pageItems = $(pagingId + ' .photo-paging-list .photo-paging-item');
    var isOdd = (pageItems.length % 2);
    var left = 490;
    var half = Math.floor(pageItems.length / 2);
    if (isOdd) {
        left = 490 - 28 * half;
    }
    else {
        left = 504 - 28 * half;
    }

    var height = parseInt($(pagingId).css('height'), 10);
    var top = Math.floor(height / 3) + 'px'; // empirical formular
    $('.photo-paging-list > div').css('position', 'absolute');
    for (var i = 0; i < pageItems.length; i++) {
        pageItems.get(i).style.left = '' + left + 'px';
        pageItems.get(i).style.top = top;
        left += 28;
    }
}

// these variables are for making sure the click actually happens within 10 pixels square range so that it won't confuse with swipe
var photoClickX = -1;
var photoClickY = -1;

function enablePhotoViewer() {
    $('li.photo-tile img').on('mousedown', function (e) {
        photoClickX = e.pageX;
        photoClickY = e.pageY;
    });
    $('li.photo-tile img').on('click', function (e) {
        if (Math.abs(photoClickX - e.pageX) <= 10 && Math.abs(photoClickY - e.pageY) <= 10) {
            $.colorbox({
                width: '900px',
                href: $(this).attr('src')
            });
        }
    });
}

$(function() {
    hiConfig = {
        sensitivity: 3, // number = sensitivity threshold (must be 1 or higher)
        interval: 1000,  // number = milliseconds for onMouseOver polling interval
        timeout: 1000,   // number = milliseconds delay before onMouseOut
        over: function () { // required
            $.colorbox({
                width: '900px',
                href: $(this).attr('src')
            });
        },
        out: function () { // required
        } 
    }
    //$('.photo-tile img').hoverIntent(hiConfig);
});