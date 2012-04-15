// Use this as a quick template for future modules
define([
  'jquery',
  'underscore',
  'backbone'
], function ($, _, Backbone) {
    /**
    * Base class for graphics elements
    */
    var BaseGraphics = Backbone.View.extend({

        _setDefaults: function () {
            /// <summary>
            /// Sets default values
            /// </summary>
            this.x = 0;
            this.y = 0;
            this.width = 1;
            this.height = 1;
            this.color = 'black';
            this.opacity = 0.5;
            this.lineThickness = 1;
            this.lineColor = 'black';
            this.lineOpacity = 1;
        },

        initialize: function () {
            /// <summary>
            /// Constructor
            /// </summary>

            
            this._setDefaults();
        },

        set: function (attr) {
            /// <summary>
            /// Sets attributes
            /// </summary>
            /// <param name="attr">
            /// x,y,width,height,color,opacity,lineThickness,lineColor,lineOpacity
            /// </param>

            if (typeof attr == 'undefined') {
                return;
            }

            if (typeof attr.x != 'undefined') {
                this.x = attr.x;
            }
            if (typeof attr.y != 'undefined') {
                this.y = attr.y;
            }

            if (typeof attr.width != 'undefined') {
                this.width = attr.width;
            }
            if (typeof attr.height != 'undefined') {
                this.height = attr.height;
            }
            if (typeof attr.color != 'undefined') {
                this.color = attr.color;
            }
            if (typeof attr.opacity != 'undefined') {
                this.opacity = attr.opacity;
            }
            if (typeof attr.lineThickness != 'undefined') {
                this.lineThickness = attr.lineThickness;
            }
            if (typeof attr.lineColor != 'undefined') {
                this.lineColor = attr.lineColor;
            }
            if (typeof attr.lineOpacity != 'undefined') {
                this.lineOpacity = attr.lineOpacity;
            }
        },

        renderOnPaper: function (paper) {
            // child class should override this

        },
        remove: function () {
            /// <summary>
            /// Removes the element
            /// </summary>
            /// <param name="paper"></param>

            this._el.remove();
        },

        update: function (paper) {
            // child class should override this

        },

        bindMouseEvents: function () {
        	/// <summary>
            /// Binds mouse events on raphael element if it exists
        	/// bind: mouseup, mousedown, mouseover, mouseout, mousemove
            /// </summary>
            
            if (typeof this._el == 'undefined') {
                return;
            }

            _.extend(this, Backbone.Events);
            
            var self = this;

            this._el.mouseup(function () {
                self.trigger("mouseup", self);
            });

            this._el.mousedown(function () {
                self.trigger("mousedown", self);
            });

            this._el.mousemove(function (event) {
                var curPos = { x: event.offsetX, y: event.offsetY };
                self.trigger("mousemove", self, curPos);
            });

            this._el.mouseover(function () {
                self.trigger("mouseover", self);
            });

            this._el.mouseout(function () {
                self.trigger("mouseout", self);
            });

        }
    });
    return BaseGraphics;
});