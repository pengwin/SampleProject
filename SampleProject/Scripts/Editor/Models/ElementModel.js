
define([
  'jquery',
  'underscore',
  'backbone',
], function ($, _, Backbone) {

    /**
    * Model of blueprint element
    */
    var ElementModel = Backbone.Model.extend({

        defaults: {
            type: 'undefined',
            x: 0,
            y: 0,
            width: 200,
            height: 200
        },

        initialize: function () {
            /// <summary>
            /// constructor
            /// </summary>

            this.xLeftLimit = 0;
            this.xRightLimit = 100;
            this.yTopLimit = 0;
            this.yBottomLimit = 100;
        },

        validate: function (attrs) {
            /// <summary>
            /// Validates model
            /// </summary>
            /// <param name="attrs">changed attributes</param>
            /// <returns type="">error if validation failed</returns>

            if (typeof attrs.width != 'undefined' && isNaN(attrs.width)) {
                return "width is Not a Number";
            }

            if (typeof attrs.height != 'undefined' && isNaN(attrs.height)) {
                return "height is Not a Number";
            }

            if (typeof attrs.x != 'undefined' && isNaN(attrs.x)) {
                return "x is Not a Number";
            }

            if (typeof attrs.y != 'undefined' && isNaN(attrs.y)) {
                return "x is Not a Number";
            }

            // check horizontal parameters

            if (attrs.x < this.xLeftLimit) {
                return "left border out of bound ";
            }

            if ((this.attributes.x + attrs.width) > this.xRightLimit) {
                return "right border out of bound ";
            }

            if ((attrs.x + this.attributes.width) > this.xRightLimit) {
                return "right border out of bound ";
            }

            if ((attrs.x + attrs.width) > this.xRightLimit) {
                return "right border out of bound ";
            }

            if (attrs.width <= 0) {
                return "width should be greater than 0";
            }

            // check vertical parameters

            if (attrs.y < this.yTopLimit) {
                return "top border out of bound ";
            }

            if ((this.attributes.y + attrs.height) > this.yBottomLimit) {
                return "bottom border out of bound ";
            }

            if ((attrs.y + this.attributes.height) > this.yBottomLimit) {
                return "bottom border out of bound ";
            }

            if ((attrs.y + attrs.height) > this.yBottomLimit) {
                return "bottom border out of bound ";
            }

            if (attrs.height <= 0) {
                return "height should be greater than 0";
            }

        }
    });
    return ElementModel;
});