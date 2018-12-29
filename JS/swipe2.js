//================================================================================================
// This implementation is designed to be used in a page that have one or more swipeable object
// parameters:
// containerId - the outer container for the swipeable region. should include # or . and be unique
// contentId - the region that contain all content. should include # or . and be unique
// viewpartId - the viewable region. should inlcude . and be a css class
// pagingContainerId - the outer region for paging. should include # or . and be unique
// pageId - the page class. should include . and be a css class
// p1 = true - animate swpiing effect
// q1 = true - animate circularly
// o1 = true - animate container background
// z1 - initial page # (zero-based)
//
// Note: the difference betweenb swpipe.js and swipe2.js is the ipad swipe implementation.
//================================================================================================

// Swipeable class constructor
function Swipeable2(containerId, contentId, viewportId, pagingContainerId, pageId, pageLeftId, pageRightId, isAnimated, isCircular, isBackground, initialPage) {
    this.r = containerId;
    this.s = contentId;
    this.t = contentId + ' ' + viewportId;
    this.x = pagingContainerId + ' ' + pageId;
    this.y = pagingContainerId + ' ' + pageLeftId;
    this.z = pagingContainerId + ' ' + pageRightId;
    if (isAnimated != null) this.p = isAnimated;
    if (isCircular != null) this.q = isCircular;
    if (isBackground != null) this.o = isBackground;
    if (initialPage != null) this.vc = initialPage;
    this.sa = contentId + ' a';
    this.l += containerId.substring(1);
    this.lc += containerId.substring(1);
}

// Swipeable class properties: used as local variables
Swipeable2.prototype.a = null;
Swipeable2.prototype.b = null;
Swipeable2.prototype.c = null;
Swipeable2.prototype.d = '';
Swipeable2.prototype.e = 0;
Swipeable2.prototype.f = 0;
Swipeable2.prototype.g = 0;
Swipeable2.prototype.h = 0;
Swipeable2.prototype.i = 700;
Swipeable2.prototype.j = '';
Swipeable2.prototype.k = 'easeOutBack';
Swipeable2.prototype.l = 'inFocus-';
Swipeable2.prototype.lc = '.inFocus-';
Swipeable2.prototype.m = null;
Swipeable2.prototype.my = null;
Swipeable2.prototype.ny = null;
Swipeable2.prototype.n = null;
Swipeable2.prototype.o = true;
Swipeable2.prototype.p = true;
Swipeable2.prototype.q = true;
Swipeable2.prototype.r = '';
Swipeable2.prototype.s = '';
Swipeable2.prototype.sa = '';
Swipeable2.prototype.t = '';
Swipeable2.prototype.x = '';
Swipeable2.prototype.y = '';
Swipeable2.prototype.z = '';
Swipeable2.prototype.vc = 0;
Swipeable2.prototype.track = false;

// Swipeable methods
Swipeable2.prototype.init = function() {
    var u = this.t + ":first";
    var v = this.x + ":first";
    this.e = parseInt($(u).width(), 10),
    this.f = parseInt(this.e / 10, 10),
    this.g = $(this.t).length - 1;
    if (window.navigator.msPointerEnabled || window.navigator.msMaxTouchPoints > 0) {
        this.f = 30;
        $(this.s).css('-ms-touch-action', 'none');
        this.msswipe();
    }
    else if (window.navigator.userAgent.match(/iPad/i) != null) {
        this.f = 30;
        this.ipadswipe();
    }
    this.mouseswipe(); // this is always available for all browsers
    this.paging();
    this.pagingLeft();
    this.pagingRight();
    $(u).addClass(this.l);
    $(v).addClass("pageSelected");
    $(this.r).attr("style", "background-position:0px 0px;");
    if (this.vc > 0 && this.vc <= this.g) {
        this.panelAnimate(this.vc);
    }
}

Swipeable2.prototype.msswipe = function () {
    // for Windows 8 & IE10 with swipe harwdare
    var _self = this;
    try {
        var os = document.getElementById(_self.s.substring(1));
        os.addEventListener('MSPointerDown', function (e) {
            _self.track = window.navigator.msMaxTouchPoints == 1;
            if (_self.track) {
                _self.m = e.clientX;
                _self.n = _self.m;
                $(_self.t).addClass("grabbing");
                os.addEventListener('MSPointerMove', function (e) {
                    e.preventDefault();
                    _self.n = e.clientX;
                    if (_self.n > _self.m + _self.f)
                        _self.d = "right";
                    else if (_self.n < _self.m - _self.f)
                        _self.d = "left";
                    else if (_self.ny > _self.my + _self.f || _self.ny < _self.my - _self.f)
                        e.preventDefault();
                    return false
                }, false);
                os.addEventListener("MSPointerUp", function (e) {
                    if (_self.track) {
                        e.preventDefault();
                        _self.n = e.clientX;
                        $(document, _self.sa).unbind();
                        if (_self.p) {
                            if (Math.abs(_self.m - _self.n) > _self.f)
                                _self.intelliIncrement();
                            else
                                _self.panelAnimate($(_self.lc).index());
                        }
                    }
                    _self.track = false;
                }, false);
            }
            else
                _self.track = false;
        }, false);
    }
    catch (e1) { }
    return false;
}

Swipeable2.prototype.ipadswipe = function () {
    var _self = this;
    try {
        var os = document.getElementById(_self.s.substring(1));
        os.addEventListener('touchstart', function (e) {
            _self.track = e.touches.length == 1;
            if (_self.track) {
                _self.m = e.touches[0].pageX;
                _self.n = _self.m;
                //_self.my = e.touches[0].pageY;
                //_self.ny = _self.my;
                os.addEventListener('touchmove', function (e) {
                    e.preventDefault();                    _self.n = e.touches[0].pageX;
                    _self.ny = e.touches[0].pageY;
                    if (_self.n > _self.m + _self.f)
                        _self.d = "right";
                    else if (_self.n < _self.m - _self.f)
                        _self.d = "left";
                    //else if (_self.ny > _self.my + _self.f || _self.ny < _self.my - _self.f)
                    //    return true;
                    return false;
                }, false);
                os.addEventListener('touchend', function(e) {
                    if (_self.track) {
                        //if (Math.abs(_self.m - _self.n) <= _self.f && Math.abs(_self.my - _self.ny) > _self.f)
                        //    return true;

                        e.preventDefault();
                        $(document, _self.sa).unbind();
                        if (_self.p) {
                            if (Math.abs(_self.m - _self.n) > _self.f)
                                _self.intelliIncrement();
                            else
                                _self.panelAnimate($(_self.lc).index());
                        }
                    }
                    _self.track = false;
                }, false);
            } 
            else
                _self.track = false;
        }, false);
    }
    catch(e1) {}
    return false;
}

Swipeable2.prototype.mouseswipe = function () {
    var _self = this;
    $(_self.s).on('mousedown', function (e) {
        try {
            _self.m = e.pageX;
            _self.a = e.pageX - $(_self.s).position().left;
            _self.b = $(_self.s).position().left;
            $(_self.t).addClass("grabbing");
            $(document).on('mousemove', function (e) {
                _self.c = e.pageX - _self.a;
                $(_self.s).css({ left: _self.c });
                if (_self.c > _self.b) {
                    _self.d = "right"
                }
                else if (_self.c - _self.a < _self.b) {
                    _self.d = "left"
                }
                return false
            });
            $(document).one("mouseup", function (a) {
                _self.n = a.pageX;
                $(document, _self.sa).unbind();
                $(_self.sa).click(function (a) {
                    if (Math.abs(_self.m - _self.n) > 5) {
                        a.preventDefault();
                        return false
                    }
                });
                $(_self.t).removeClass("grabbing");
                if (_self.p) {
                    if (Math.abs(_self.m - _self.n) > _self.f) {
                        _self.intelliIncrement()
                    } else {
                        _self.panelAnimate($(_self.lc).index())
                    }
                }
            });
        }
        catch (e1) { }
        return false
    })
}

Swipeable2.prototype.paging = function (e) {
    var _self = this;
    $(_self.x).on('click', function (e) {
        _self.panelAnimate($(_self.x).index(this));
    });
}

Swipeable2.prototype.pagingLeft = function (e) {
    var _self = this;
    $(_self.y).on('click', function (e) {
        if ($(_self.lc).index() > 0) {
            _self.intelliIncrement('right');
        }
    });
}

Swipeable2.prototype.pagingRight = function (e) {
    var _self = this;
    $(_self.z).on('click', function (e) {
        if ($(_self.lc).index() < _self.g) {
            _self.intelliIncrement('left');
        }
    });
}

Swipeable2.prototype.intelliIncrement = function (dir) {
    if (dir != null) this.d = dir;
    if (this.d == "right") {
        var a = $(this.lc).index() - 1;
        if (a >= 0) {
            this.panelAnimate(a)
        } else if (this.q) {
            this.panelAnimate(this.g)
        } else {
            this.panelAnimate(0)
        }
    } else if (this.d == "left") {
        var b = $(this.lc).index() + 1;
        if (!(b > this.g)) {
            this.panelAnimate(b)
        } else if (this.q) {
            this.panelAnimate(0)
        } else {
            this.panelAnimate(this.g)
        }
    }
}

Swipeable2.prototype.panelAnimate = function (a) {
    var b = Math.abs(this.h - a) * (this.i / 10) + this.i;
    $(this.s).animate({ left: this.e * a * -1 }, b, this.k);
    if (this.o) {
        $(this.r).animate({ backgroundPosition: this.e / 10 * -1 * a }, b, this.k)
    }
    $(this.t).removeClass(this.l);
    $(this.t).eq(a).addClass(this.l);
    $(this.x).removeClass("pageSelected");
    $(this.x).eq(a).addClass("pageSelected");
    this.h = a;
}
