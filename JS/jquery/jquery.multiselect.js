(function ($, undefined) {

    var multiselectID = 0;

    $.widget("ech.multiselect", {

        // default options
        options: {
            defaultButtonMessage: "Select Item...",
            multiple: true,
            filter: true,
            sorting: true
        },

        _create: function () {
            var self = this;
            self.source = this.element;
            self.numSelected = 0;
            self.defaultButtonMessage = self.options.defaultButtonMessage;

            uId = "multiselect-" + multiselectID;

            // -----------
            // initilize
            // -----------

            // hide original
            self.source.hide();

            // add html
            self.source.before("<div id='" + uId + "'></div>");
            self.wrapper = $("#" + uId);

            self.wrapper.append("<div id='button' class='mutliselect-button'><span class='text'>" + self.defaultButtonMessage + "</span><span class='arrow'><span class='img  ui-icon ui-icon-triangle-1-s'></span></span></div>");
            self.wrapper.append("<div id='menu' class='mutliselect-menu'><div id='filterArea' class='multiselect-filterArea'>Find: <input id='filter' class='multiselect-filter' type='text'/></div><ul id='unselected' class='multiselect-unselected'></ul></div>");
            self.wrapper.append("<ul id='selected' class='mutliselect-selected'></ul>");

            // bind dom objects to variables
            self.menu = $("#" + uId + " #menu");
            self.selected = $("#" + uId + " #selected");
            self.filter = $("#" + uId + " #filter");
            self.unselected = $("#" + uId + " #unselected");
            self.button = $("#" + uId + " #button");
            self.buttonText = $("#" + uId + " #button .text")

            // set width
            self.button.width(self.source.width());
            self.menu.width(self.source.width());
            self.menu.hide();
            self.wrapper.width(self.source.width() + 2);
            self.filter.width(self.source.width() - 55);

            // set values
            this.refresh();

            // ----------
            // bindings
            // ----------

            // show menu
            self.button.on('click', function () {
                self.menu.toggle();

                // fire open event if opening
                if (self.menu.is(":visible")) {
                    self.source.trigger("open");
                } else {
                    self.source.trigger("close");
                }

                // set focus to filter if enabled
                if (self.options.filter == true) {
                    self.filter.focus();
                }

                return false;
            });

            // close when clicking anywhere on the page
            $(document).on("click", function () {
                if (self.menu.is(":visible")) {
                    self.menu.hide();
                }
            });
            self.menu.on("click", function (e) {
                e.stopPropagation();
                return false;
            });

            // select binding
            if (self.options.multiple == false) {
                self._bindSingleSelect();
                self.menu.css("position", "absolute");	// single select is absolute
            } else {
                self._bindMultipleSelect();
            }

            // case insensative filter 
            self.filter.on('keyup', function () {
                var text = $(this).val();

                $.each(self.unselected.find("li.mutliselect-item"), function (index, item) {
                    var value = $(item).text().toUpperCase();
                    if ((value).match("^" + text.toUpperCase())) {
                        $(item).show();
                    } else {
                        $(item).hide();
                    }
                });
            });

            // options
            if (self.options.filter == false) {
                $("#" + uId + " #filterArea").hide();
            }

            multiselectID++;
        },

        refresh: function () {
            var self = this;
            // set options from source
            self.numSelected = 0;
            self.unselected.html("");
            self.selected.html("");
            self.buttonText.html(self.defaultButtonMessage);

            if (self.options.multiple == true) {
                $.each(self.source.find("option"), function (index, item) {
                    if (item.selected) {
                        self.numSelected++;
                        self.selected.append("<li class='mutliselect-item' tag='" + item.value + "'>" + item.text + "</li>");
                    } else {
                        self.unselected.append("<li class='mutliselect-item' tag='" + item.value + "'>" + item.text + "</li>");
                    }

                    if (self.numSelected == 0) {
                        self.buttonText.html(self.defaultButtonMessage);
                    } else {
                        self.buttonText.html(self.numSelected + " Selected");
                    }
                });
                this._sort(self.selected);
            } else {
                $.each(self.source.find("option"), function (index, item) {
                    if (item.selected) {
                        self.buttonText.html(item.text);
                        self.currentSelected = $(item);
                    }
                    self.unselected.append("<li class='mutliselect-item' tag='" + item.value + "'>" + item.text + "</li>");
                });
            }
            this._sort(self.unselected);
        },

        _bindSingleSelect: function () {
            var self = this;

            // select an item			
            self.unselected.on("click", "li.mutliselect-item", function () {
                if (self.currentSelected != null) {
                    self.currentSelected.prop("selected", false);
                }

                self.buttonText.html($(this).text());
                var tag = $(this).attr("tag");
                self.currentSelected = self.source.find("[value='" + tag + "']");
                self.currentSelected.prop("selected", true);

                self.menu.hide();

                // creates hookable trigger
                self.source.trigger("change");
            });
        },

        _bindMultipleSelect: function () {
            var self = this;

            // select an item
            self.unselected.on("click", "li.mutliselect-item", function () {
                // change button text
                self.numSelected++;
                self.buttonText.html(self.numSelected + " Selected");

                // add item to selected
                self.selected.append(this);


                // select item in select dom
                var tag = $(this).attr("tag");
                self.source.find("[value='" + tag + "']").prop("selected", true);

                // creates hookable trigger
                self.source.trigger("change");
            });

            // unselect an item
            self.selected.on("click", "li.mutliselect-item", function () {
                // change button text
                self.numSelected--;
                if (self.numSelected == 0) {
                    self.buttonText.html(self.defaultButtonMessage);
                } else {
                    self.buttonText.html(self.numSelected + " Selected");
                }

                // add item to unselected
                self.unselected.append(this);

                // de-select item in select dom
                var tag = $(this).attr("tag");
                self.source.find("[value='" + tag + "']").prop("selected", false);

                // puts item back in its proper order
                self._placeBackToUnselected(this);

                // creates hookable trigger
                self.source.trigger("change");
            });
        },

        _sort: function (ul) {
            var self = this;

            if (self.options.sorting == true) {
                var listitems = ul.children('li.mutliselect-item').get();
                listitems.sort(function (a, b) {
                    return $(a).text().toUpperCase().localeCompare($(b).text().toUpperCase());
                })

                $.each(listitems, function (idx, itm) { ul.append(itm); });
            }
        },

        _placeBackToUnselected: function (li) {
            var self = this;
            var tag = $(li).attr('tag');

            // iterate through and find first place this belongs above
            $.each(self.unselected.find("li"), function (index, item) {
                if (tag.toUpperCase().localeCompare($(item).text().toUpperCase()) == -1) {
                    $(item).before(li);
                    return false;
                }
            });
        },

        loading: function () {
            var self = this;
            self.unselected.html("<li class='multiselect-loading'>loading...</li>");
        },

        destroy: function () {
            var self = this;
            self.source.show();
            self.wrapper.remove();
        }
    });

})(jQuery);