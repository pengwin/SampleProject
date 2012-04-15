
define([
  'jquery',
  'underscore',
  'backbone',
  'views/graphics/basegraphics'
], function ($, _, Backbone, BaseGraphics) {
    /**
    *  Grid graphics with square cells
    */
    var GridGraphics = BaseGraphics.extend({

        _setDefaults: function () {
            /// <summary>
            /// Sets default values
            /// </summary>
            BaseGraphics.prototype._setDefaults.call(this);

            this.step = 1;
        },

        set: function (attr) {
            /// <summary>
            /// Sets attributes
            /// </summary>
            /// <param name="attr">
            /// x,y,width,height,color,opacity,lineThickness,lineColor,lineOpacity
            /// </param>

            BaseGraphics.prototype.set.call(this, attr);
            if (typeof attr.step != 'undefined') {
                this.step = attr.step;
            }
        },

        _calculatePath: function () {
            /// <summary>
            /// Creates string with svg path pseudo code
            /// </summary>
            /// <returns type=""></returns>

            var str = "";
            // adds vertical lines
            for (var i = this.x + this.step; i < this.width; i += this.step) {
                str += "M" + i + " " + this.x + "L" + i + " " + this.height;
            }
            // adds horizontal lines
            for (var j = this.y + this.step; j < this.height; j += this.step) {
                str += "M" + this.y + " " + j + "L" + this.width + " " + j;
            }
            return str;
        },

        renderOnPaper: function (paper) {
            /// <summary>
            /// Renders grid on raphael paper
            /// </summary>
            /// <param name="paper"></param>

            if (typeof this._el != 'undefined') {
                this._el.remove();
            }
            var str = this._calculatePath();
            this._el = paper.path(str);
            this.update();
        },

        update: function () {
            /// <summary>
            /// Updates grid
            /// </summary>

            var str = this._calculatePath();
            this._el.attr('path', str);
            this._el.attr('stroke', this.lineColor);
            this._el.attr('stroke-opacity', this.lineOpacity);
            this._el.attr('stroke-width', this.lineThickness);
            $(this._el.node).attr('id', 'grid');
        }
    });
    return GridGraphics;
});