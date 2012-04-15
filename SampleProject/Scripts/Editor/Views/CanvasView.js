// BlueprintView module
// Responsible for displaying blueprint data

define([
        'jquery',
        'underscore',
        'backbone',
        'views/graphics/canvasgraphics'
    ], function ($, _, Backbone, CanvasGraphics) {

        var CanvasView = Backbone.View.extend({

            initialize: function () {
                /// <summary>
                /// constructor
                /// </summary>
                this._renderNavbar();
                this.btnCanvasEdit = $(this.navbar);

                this._initGraphics();

                this._bindHandlers();
            },

            _initGraphics: function () {
                this.graphics = new CanvasGraphics();
                this.paper = this.graphics.el;
            },
            _renderNavbar: function () {
                this.navbar = this.make("a", { href: "#", id: "btnCanvasEdit" });
                $(this.navbar).html("Canvas");
            },

            _bindHandlers: function () {
                /// <summary>
                /// Binds handlers to buttons
                /// </summary>
                _.extend(this, Backbone.Events);

                var self = this;

                this.btnCanvasEdit.click(function () {
                    self.trigger("edit_request");
                });
            },

            update: function (attrs) {
                this.graphics.set(attrs);
            }
        });

        return CanvasView;
    });