// Use this as a quick template for future modules
define([
  'jquery',
  'underscore',
  'backbone',
  'views/graphics/basegraphics'
], function ($, _, Backbone, BaseGraphics) {
    /**
    * Rectangle element
    */
    var RectangleGraphics = BaseGraphics.extend({

        renderOnPaper: function (paper) {
            /// <summary>
            /// Renders rectangle on raphael paper
            /// </summary>
            /// <param name="paper">Raphael.paper</param>
            if (typeof this._el != 'undefined') {
                this._el.remove();
            }
            this._el = paper.rect(this.x, this.y, this.width, this.height);
            this.update();
            this.bindMouseEvents();
        },
        update: function () {
            /// <summary>
            /// Updates rectangle
            /// </summary>

            BaseGraphics.prototype.update.call(this);

            this._el.attr('x', this.x);
            this._el.attr('y', this.y);
            this._el.attr('width', this.width);
            this._el.attr('height', this.height);

        }

    });
    return RectangleGraphics;
});