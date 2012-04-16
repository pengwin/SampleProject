// CanvasView module
// Responsible for displaying canvas data

define([
        'jquery',
        'underscore',
        'backbone',
        'views/baseview',
        'views/graphics/canvasgraphics'
    ], function ($, _, Backbone, BaseView, CanvasGraphics) {

        var CanvasView = BaseView.extend({

            initialize: function () {
                BaseView.prototype.initialize.call(this);
                this._renderCoordbar();
            },

            initNavbar: function () {
                /// <summary>
                /// Inits navbar element
                /// </summary>   

                this.navbar = this.make("a", { id: "btnCanvasEdit" });
                $(this.navbar).html("Canvas");
            },

            initGraphics: function () {
                /// <summary>
                /// Inits svg graphics
                /// </summary>
                this.graphics = new CanvasGraphics();
            },

            _renderCoordbar: function () {
                this.coordbar = this.make("p", { id: "lblCoords" });
                $(this.coordbar).html("Cursor pos. x: 0 y: 0");
                $(this.graphics.el).append(this.coordbar);
                this.lblCoords = $(this.coordbar);
            },

            _bindGraphics: function () {
                /// <summary>
                /// Binds handlers to buttons
                /// </summary>

                BaseView.prototype._bindGraphics.call(this);
                var self = this;

                this.graphics.on('mousemove', function (sender, coord) {
                    self.lblCoords.html("Cursor pos. x: " + coord.x + " y: " + coord.y);
                });

            }
        });

        return CanvasView;
    });