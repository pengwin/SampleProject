// Use this as a quick template for future modules
define([
  'jquery',
  'underscore',
  'backbone',
  'views/graphics/basegraphics'
], function ($, _, Backbone, BaseGraphics) {
    /**
    * Ellipse element
    */
    var EllipseGraphics = BaseGraphics.extend({


        _calculateEllipseParams: function () {
            this.horizontalRadius = this.width / 2;
            this.verticalRadius = this.height / 2;
            this.centerX = this.x + this.horizontalRadius;
            this.centerY = this.y + this.verticalRadius;
        },
        renderOnPaper: function (paper) {
            /// <summary>
            /// Renders rectangle on raphael paper
            /// </summary>
            /// <param name="paper">Raphael.paper</param>
            if (typeof this._el != 'undefined') {
                this._el.remove();
            }
            this._calculateEllipseParams();
            this._el = paper.ellipse(this.centerX, this.centerY, this.horizontalRadius, this.verticalRadius);
            this.update();
            this.bindMouseEvents();
        },
        update: function () {
            /// <summary>
            /// Updates rectangle
            /// </summary>

            BaseGraphics.prototype.update.call(this);
            
            this._calculateEllipseParams();
            
            this._el.attr('cx', this.centerX);
            this._el.attr('cy', this.centerY);
            this._el.attr('rx', this.horizontalRadius);
            this._el.attr('ry', this.verticalRadius);

        }

    });
    return EllipseGraphics;
});