"use strict";
var CEApp = CEApp || {};

CEApp.Cookie = function () {
    var cookieLifeSpan = 64,
        cookiePath = '/',
        cookieParamName = 'cookieName',
        cookieDomain = 'culturalexploration.org',

        cookieExist = function (options) {
            var cookieName = (options && options[cookieParamName]) ? options[cookieParamName] : undefined;
            return cookieName == undefined ? false : $.cookie(cookiename);
        },
    
        setCookie = function (options, data) {
            var cookieName = (options && options[cookieParamName]) ? options[cookieParamName] : undefined;
            if (cookieName != undefined && data != null) {
                $.cookie(cookieName, data, { expires: cookieLifeSpan, path: cookiePath });
                return true;
            }
            return false;
        },

        getCookie = function (options) {
            var cookieName = (options && options[cookieParamName]) ? options[cookieParamName] : undefined;
            if (cookieName != undefined && $.cookie(cookieName) != undefined) {
                return $.cookie(cookieName);
            }
            return null;
        },

        deleteCookie = function (options) {
            var cookieName = (options && options[cookieParamName]) ? options[cookieParamName] : undefined;
            return cookieName == undefined ? false : $.removeCookie(cookieName, { path: cookiePath });
        }

        return {
            cookieExist: cookieExist,
            setCookie: setCookie,
            getCookie: getCookie,
            deleteCookie: deleteCookie
        }
}();
