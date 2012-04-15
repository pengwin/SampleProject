
define([
  'jquery',
  'underscore',
  'backbone',
], function ($, _, Backbone) {

    /**
    * Model of canvas in blueprint
    */
    var CanvasModel = Backbone.Model.extend({

        defaults: {
            width: 1,
            height: 1,
            widthLimit: 1,
            heightLimit: 1,
            padding: 1,
            gridStep: 1
        },

        initialize: function () {
            /// <summary>
            /// constructor
            /// </summary>

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

            if (typeof attrs.padding != 'undefined' && isNaN(attrs.padding)) {
                return "padding is Not a Number";
            }

            if (typeof attrs.widthLimit != 'undefined' && isNaN(attrs.widthLimit)) {
                return "width limit is Not a Number";
            }

            if (typeof attrs.heightLimit != 'undefined' && isNaN(attrs.heightLimit)) {
                return "height limit is Not a Number";
            }

            if (attrs.widthLimit <= 0) {
                return "width limit should be greater than 0";
            }
            if (attrs.heightLimit <= 0) {
                return "height limit should be greater than 0";
            }
            if (attrs.width <= 0) {
                return "width should be greater than 0";
            }
            if (attrs.width > this.get('widthLimit')) {
                return "width should be less than " + this.get('widthLimit');
            }
            if (attrs.height <= 0) {
                return "height should be greater than 0";
            }
            if (attrs.height > this.get('heightLimit')) {
                return "width should be less than " + this.get('heightLimit');
            }
        }
    });
    return CanvasModel;
});