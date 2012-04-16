// Canvas graphics module
// Canvas draws paper and svg graphics on it
// provides public accessor for raphael paper

define([
  'jquery',
  'underscore',
  'backbone',
  'raphael',
  'views/graphics/gridgraphics',
  'views/graphics/rectanglegraphics'
], function ($, _, Backbone, raphael, GridGraphics, RectangleGraphics) {

    // Canvas Graphics class
    var CanvasGraphics = Backbone.View.extend({

        _setDefaults: function () {
            /// <summary>
            /// Set default values
            /// </summary>

            this.width = 1;
            this.height = 1;

            this.gridStep = 1;

            this.style = {

                color: 'white',

                borderThickness: 4,
                borderColor: 'black',
                borderOpacity: 1,

                gridThickness: 0.5,
                gridOpacity: 0.5,
                gridColor: 'black',


                assetsLineThickness: 4,
                assetsColor: 'black',
                assetsOpacity: 1,
                assetsLineColor: 'black',
                assetsLineOpacity: 1
            };

        },

        initialize: function () {
            /// <summary>
            /// Constructor
            /// </summary>

            this._setDefaults();
            this._grid = new GridGraphics();
            this._border = new RectangleGraphics();

            this.render();
        },

        set: function (attr) {
            /// <summary>
            /// Set widget parameters
            /// </summary>
            /// <param name="attr">
            /// parameters:
            /// width, height, color, padding, gridStep
            /// </param>

            if (typeof attr == 'undefined') {
                return;
            }
            var keys = Object.keys(attr);

            for (var i = 0; i < keys.length; i++) {
                var key = keys[i];
                if (typeof this[key] != 'undefined') {
                    this[key] = attr[key];
                }
            }
        },

        setStyle: function (attr) {
            /// <summary>
            /// Set widget parameters
            /// </summary>
            /// <param name="attr">
            /// parameters:
            /// width, height, color, padding, gridStep
            /// </param>

            if (typeof attr == 'undefined') {
                return;
            }
            var keys = Object.keys(attr);

            for (var i = 0; i < keys.length; i++) {
                var key = keys[i];
                if (typeof this.style[key] != 'undefined') {
                    this.style[key] = attr[key];
                }
            }
        },

        _updateGrid: function () {
            this._grid.set({
                x: 0,
                y: 0,
                width: this.width,
                height: this.height,
                step: this.gridStep,
                lineThickness: this.style.gridThickness,
                lineOpacity: this.style.gridOpacity,
                lineColor: this.style.gridColor
            });
            this._grid.update();
        },

        _updateBorder: function () {
            this._border.set({
                x: 0,
                y: 0,
                width: this.width,
                height: this.height,
                opacity: 0.0,
                lineThickness: this.style.borderThickness,
                lineOpacity: this.style.borderOpacity,
                lineColor: this.style.borderColor
            });
            this._border.update();
        },

        update: function () {
            /// <summary>
            /// Updates widget
            /// </summary>

            $(this.el).width(this.width);
            $(this.el).height(this.height);
            $(this.el).css('background-color', this.style.color);
            $(this.el).css('padding', this.padding);
            this._paper.setSize(this.width, this.height);
            this._updateGrid();
            this._updateBorder();
        },

        render: function () {
            /// <summary>
            /// Creates dom element and Raphael paper
            /// </summary>
            /// <param name="container">DOM element for Raphael paper</param>

            this.el = this.make("div", { id: "blueprint" });
            this._paper = Raphael(this.el, this.width, this.height);
            this._grid.renderOnPaper(this._paper);
            this._border.renderOnPaper(this._paper);
            this.update();
            this.bindMouseEvents();
            return this.el;
        },
        paper: function () {
            /// <summary>
            /// Gets private paper
            /// </summary>
            return this._paper;
        },
        bindMouseEvents: function () {
            /// <summary>
            /// Sets up mouse events handlers on container
            /// </summary>

            _.extend(this, Backbone.Events);
            var self = this;

            // bind handlers on div element using jquery

            $(this.el).click(function () {
                self.trigger("click", self);
            });

            $(this.el).mouseover(function () {
                self.trigger("mouseover", self);
            });

            $(this.el).mouseout(function () {
                self.trigger("mouseout", self);
            });

            $(this.el).mousemove(function (event) {
                self.trigger("mousemove", self, { x: event.offsetX, y: event.offsetY });
            });
        },
        remove: function () {
        	/// <summary>
        	/// Removes canvas graphics
        	/// </summary>
            this._paper.remove();
            $(this.el).remove();
        }
    });
    return CanvasGraphics;
});