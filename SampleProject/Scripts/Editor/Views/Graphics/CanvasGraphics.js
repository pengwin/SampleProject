
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

            this.padding = 5;
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

            this._rectangles = [];
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
            this.update();
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
            this.update();
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

        _updateRectangles: function () {
            for (var i = 0; i < this._rectangles.length; i++) {
                var rectangle = this._rectangles[i];
                rectangle.set({
                    opacity: this.style.assetsOpacity,
                    lineThickness: this.style.assetsLineThickness,
                    lineOpacity: this.style.assetsLineOpacity,
                    lineColor: this.style.assetsLineColor
                });
                rectangle.update();
            }
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
            this._updateRectangles();
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

        bindMouseEvents: function () {
            /// <summary>
            /// Sets up mouse events handlers on container
            /// </summary>

            _.extend(this, Backbone.Events);
            var self = this;

            // bind handlers on border item using raphael
            this._border.on('mouseup', function () {
                self.trigger("svg:mouseup", self);
            });

            this._border.on('mousedown', function () {
                self.trigger("svg:mousedown", self);
            });

            // bind handlers on div element using jquery
            $(this.el).mousedown(function () {
                self.trigger("dom:mousedown", self);
            });

            $(this.el).mouseup(function () {
                self.trigger("dom:mouseup", self);
            });

            $(this.el).mousemove(function (event) {
                var offset = $(self.el).offset();
                var x = event.pageX - offset.left - self.padding;
                var y = event.pageY - offset.top - self.padding;
                self.trigger("dom:mousemove", self, { x: x, y: y });
            });
        },

        drawRectangle: function () {
            /// <summary>
            /// Creates rectangle on paper
            /// </summary>

            var rectangle = new RectangleGraphics();
            rectangle.renderOnPaper(this._paper);

            this._rectangles.push(rectangle);

            return rectangle;
        },

        removeRectangle: function (rectangle) {
            /// <summary>
            /// Removes the rectangle from paper.
            /// </summary>

            rectangle.remove();
            this._rectangles.pop(rectangle);
        }
    });
    return CanvasGraphics;
});