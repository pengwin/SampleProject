// RectnagleView module
// Responsible for displaying rectangle data

define([
        'jquery',
        'underscore',
        'backbone',
        'views/graphics/rectanglegraphics'
    ], function ($, _, Backbone, RectangleGraphics) {

        var RectangleView = Backbone.View.extend({

            initialize: function () {
                /// <summary>
                /// constructor
                /// </summary>
                this._renderNavbar();
                this.btnEdit = $(this.navbar);

                this._initGraphics();

                this._bindHandlers();
            },

            _initGraphics: function () {
                this.graphics = new RectangleGraphics();
            },
            _renderNavbar: function () {
                this.navbar = this.make("a", { href: "#", id: "btnEdit" });
                $(this.navbar).html("Rectangle");
            },

            _bindHandlers: function () {
                /// <summary>
                /// Binds handlers to buttons
                /// </summary>
                _.extend(this, Backbone.Events);

                var self = this;

                this.btnEdit.click(function () {
                    self.trigger("edit_request");
                });
            },

            update: function (attrs) {
                this.graphics.set(attrs);
                this.graphics.update();
            },

            remove: function () {
                this.navbar.remove();
            }
        });

        return RectangleView;
    });