function getEffectivePagePath() {
    var url = window.location.pathname.toLowerCase();
    if (url.indexOf('cearticle.aspx') >= 0 || url.indexOf('cetab.aspx') >= 0 || url.indexOf('cepage.aspx') >= 0 || url.indexOf('ceblank.aspx') >= 0 || url.indexOf('cealbum.aspx') >= 0 || url.indexOf('cejourney.aspx') >= 0) {
        var path = getQueryString('path');
        if (path != null) {
            if (path.indexOf('/') < 0) path = '/' + path;
            return path.toLowerCase();
        }
    }
    return url;
}

function getQueryString(name) {
    return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null;
}

function showTopMenu() {
    var path = getEffectivePagePath();
    var id = 1;
    if (path.indexOf('/tours') >= 0)
        id = 2;
    else if (path.indexOf('/talent') >= 0)
        id = 3;
    else if (path.indexOf('/resources') >= 0)
        id = 4;
    else if (path.indexOf('/support') >= 0)
        id = 5;
    else if (path.indexOf('/admin') >= 0)
        id = 6;

    $('#ce-top-nav-' + id + ' div').addClass('ce-top-nav-selected');
    // show the submenu for 3 seconds so that tablet can see the submenu
    showSubNav(id, 1);
    closeSubNav(0);

}

function removeCookies() {
    $.removeCookie('CE_ADMIN', { path: '/'});
    $.removeCookie('CE_USER', { path: '/' });
    $.removeCookie('CE_PUBLIC', { path: '/' });
    $.removeCookie('CE_ROLE', { path: '/' });
}

function adjustSignIn() {
    var name = 'CE_ADMIN';
    if ($.cookie(name) == name + '=ce-admin') { // cookie returns with name=value form
        $('#ce-sign-in').html('<span><a href="javascript:signout();">Logout</a></span>');
    }
    else {
        $('#ce-sign-in').html('<span><a href="' + baseUrl + '/public/celogin.aspx">Login</a></span >');
    }
}

function signout() {
    removeCookies();
    alert('You have successfully signed out.');
    if (window.location.href.toLowerCase().indexOf("/admin/") >= 0)
        window.location.replace(baseUrl + '/public/home.aspx');
    else
        window.location.replace(window.location.href);
}

function getTextWidth(text, font) {
    var currentObj = $('<span>').hide().appendTo(document.body);
    $(currentObj).html(text).css('font', font);
    var width = currentObj.outerWidth();
    currentObj.remove();
    return width;
}

function getInternetExplorerVersion() // return -1 if other than IE
{
    var rv = -1; // Return value assumes failure.
    if (navigator.appName == 'Microsoft Internet Explorer') {
        var ua = navigator.userAgent;
        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
    }
    return rv;
}

function isIE() {
    return getInternetExplorerVersion() > -1;
}

function ieVersion() {
    return getInternetExplorerVersion();
}

function isValidEmail(email) {
    var pattern = new RegExp(/^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/);
    return pattern.test(email);
}

function isValidPhone(phone) { // match xxxxxxxxxx or xxx-xxx-xxxx
    var pattern = new RegExp(/^\d{3}-?\d{3}-?\d{4}$/);
    return pattern.test(phone);
}

function isValidDate(d) { // match mm/dd/yy or mm/dd/yyyy
    var pattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = d.match(pattern);

    if (dtArray == null) return false;

    //Checks for mm/dd/yyyy format.
    var dtMonth = dtArray[1];
    var dtDay = dtArray[3];
    var dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }

    return true;
}

function getCurrentDateTime() {
    var now = new Date();
    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var day = now.getDate();
    var hour = now.getHours();
    var minute = now.getMinutes();
    var second = now.getSeconds();
    if (month.toString().length == 1) {
        var month = '0' + month;
    }
    if (day.toString().length == 1) {
        var day = '0' + day;
    }
    if (hour.toString().length == 1) {
        var hour = '0' + hour;
    }
    if (minute.toString().length == 1) {
        var minute = '0' + minute;
    }
    if (second.toString().length == 1) {
        var second = '0' + second;
    }
    var dateTime = year + '/' + month + '/' + day + ' ' + hour + ':' + minute + ':' + second;
    return dateTime;
}

function isValidName(name) {
    var pattern = new RegExp(/^[A-Za-z0-9_]*[A-Za-z0-9][A-Za-z0-9_]*$/); // match any character + _
    return pattern.test(name);
}

function goHome() {
    window.location.replace(baseUrl + '/public/home.aspx');
    return true;
}

function gotoNextPage(page) {
    $('#applicationPage1').hide();
    $('#applicationPage2').hide();
    $('#applicationPage3').hide();
    $('#applicationPage4').hide();
    $('#applicationPage' + page).show();
    return false;
}

function modalDialog(id) {
    $.colorbox({
        scrolling: false,
        inline: true,
        escKey: false,
        overlayClose: false,
        width: "600px",
        height: "220px",
        href: id,
        transition: "none",
        opacity: 0.3,
        className: "ce-dialog",
        onLoad: function () { $('#cboxClose').remove(); }
    });
}

function closeDialog() {
    $.colorbox.close();
    window.location.href = baseUrl + '/public/home.aspx';
    return true;
}

function exitDialog() {
    $.colorbox.close();
    return true;
}

function redirectDialog(url) {
    $.colorbox.close();
    window.location.href = url;
    return true;
}

function toAbsoluteUrl(url) {
    return baseUrl + url;
}