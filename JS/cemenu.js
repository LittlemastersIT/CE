var topNavId = '#ce-top-nav';
var subNavId = '#ce-sub-nav';
var subNavPrefix = '#ce-sub-nav-';
var topNavPrefix = '#ce-top-nav-';
var submenuId = -1;
var subMenuState = false;

function hideSubNav(id, hide) {
    if (!hide) return;
    if (submenuId > 0) {
        $(subNavId).removeClass('show');
        $(subNavId).addClass('hide');
        $(subNavPrefix + submenuId).removeClass('show');
        $(subNavPrefix + submenuId).addClass('hide');
        $(topNavPrefix + submenuId + ' div').removeClass('hover');
        $(topNavPrefix + submenuId + ' div').removeClass('highlight');
    }
    submenuId = -1;
}
function showSubNav(id, show) {
    if (submenuId != id && submenuId > 0) {
        $(subNavId).removeClass('show');
        $(subNavId).addClass('hide');
        $(subNavPrefix + submenuId).removeClass('show');
        $(subNavPrefix + submenuId).addClass('hide');
        $(topNavPrefix + submenuId + ' div').removeClass('hover');
        $(topNavPrefix + submenuId + ' div').removeClass('highlight');
    }
    if (!show) {
        submenuId = -1;
        return;
    }
    submenuId = id;
    $(subNavId).removeClass('hide');
    $(subNavId).addClass('show');
    $(subNavPrefix + id).removeClass('hide');
    $(subNavPrefix + id).addClass('show');
    $(topNavPrefix + id + ' div').addClass('hover');
    $(topNavPrefix + id + ' div').addClass('highlight');
    if (show) subMenuState = true;

    var left = getSubmenuLeftOffset(topNavPrefix + id, subNavPrefix + id);
    $(subNavPrefix + id + ' ul').css('margin-left', left);
}
function closeSubNav(immediate) {
    if (immediate) {
        hideSubNav(-1, true);
    }
    else { // wait for 3 seconds before close the submenu
        subMenuState = false;
        setTimeout(function () {
            if (subMenuState == false) hideSubNav(-1, true);
        }, 3000);
    }
}
function displaySubNav() {
    if (submenuId > 1) {
        subMenuState = true;
        showSubNav(submenuId, true);
    }
}
function linkMenu(id) {
    if ($(topNavPrefix + id + ' div').attr("nodeurl") != null) {
        var currentUrl = $(topNavPrefix + id + ' div').attr("nodeurl");
        window.location.href = currentUrl;
    }
}

// get the starting left position based on the submenu text length
function getSubmenuLeftOffset(menucs, submenucs) {
    var submenuText = '';
    var submenuCount = 0;
    $(submenucs + ' ul li a').each(function () {
        submenuCount++;
        submenuText += $(this).html() + '|';
    });

    var font = $(submenucs + ' ul li a').css('font-family');
    var font = 'normal 14px ' + font;
    var length = getTextWidth(submenuText, font) + 18 * submenuCount; // 18px is the sapcing between top menu used by css

    var left = $(menucs).position().left; // relative to menu parent
    var width = $(menucs).outerWidth();   // include all padding
    var menuMiddle = (left + 130) + width / 2; // 130 is the top menu offset from left

    if ((menuMiddle - (length / 2)) <= 15) { // left edge overflow
        left = 15;
    }
    else if ((menuMiddle + (length / 2)) > 985) { // right edge overflow
        left = 985 - length;
    }
    else {
        left = menuMiddle - (length / 2);
    }

    return left + 'px';
}
