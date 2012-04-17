// BaseView module

// View public fields:
// * infobar - dom element to display on infobar
// * navbar - dom element to display on navbar
// * graphics - svg graphics to display on paper
// This elements aren't necessary.

// View events:
// * infobar_click
// * infobar_mouseover
// * infobar_mouseout
// * infobar_mousemove (sends coordinates of cursor)

// * navbar_click
// * navbar_mouseover
// * navbar_mouseout
// * navbar_mousemove (sends coordinates of cursor)

// * graphics_click
// * graphics_mouseup
// * graphics_mousedown
// * graphics_mouseover
// * graphics_mouseout
// * graphics_mousemove (sends coordinates of cursor)

// View public methods:
// renderGraphics(paper) renders graphics on raphael paper
// remove() - removes elements. and fires 'removed' event


define([
        'jquery',
        'underscore',
        'backbone'
    ], function ($, _, Backbone) {

        var BaseView = Backbone.View.extend({

            initialize: function () {
                /// <summary>
                /// constructor
                /// </summary>

                this.initNavbar();
                this.initInfobar();
                this.initGraphics();

                this._bindHandlers();
            },

            initNavbar: function () {
                /// <summary>
                /// Inits navbar element
                /// </summary>    
            },
            initInfobar: function () {
                /// <summary>
                /// Inits infobar element
                /// </summary>    
            },

            initGraphics: function () {
                /// <summary>
                /// Inits svg graphics
                /// </summary>
            },

            _bindInfobar: function () {
                /// <summary>
                /// Binds handlers to infobar
                /// </summary>

                var self = this;

                if (typeof this.infobar == 'undefined') return;
                $(this.infobar).click(function () {
                    self.trigger('infobar_click');
                });
                $(this.infobar).mouseover(function () {
                    self.trigger('infobar_mouseover');
                });
                $(this.infobar).mouseout(function () {
                    self.trigger('infobar_mouseout');
                });
                $(this.infobar).mousemove(function (event) {
                    self.trigger('infobar_mousemove', { x: event.offsetX, y: event.offsetY });
                });
            },

            _bindNavbar: function () {
                /// <summary>
                /// Binds handlers to navbar
                /// </summary>

                var self = this;

                if (typeof this.navbar == 'undefined') return;
                $(this.navbar).click(function () {
                    self.trigger('navbar_click');
                });
                $(this.navbar).mouseover(function () {
                    self.trigger('navbar_mouseover');
                });
                $(this.navbar).mouseout(function () {
                    self.trigger('navbar_mouseout');
                });
                $(this.navbar).mousemove(function (event) {
                    self.trigger('navbar_mousemove', { x: event.offsetX, y: event.offsetY });
                });
            },

            _bindGraphics: function () {
                /// <summary>
                /// Binds handlers to graphics
                /// </summary>

                var self = this;

                if (typeof this.graphics == 'undefined') return;
                this.graphics.on('click', function () {
                    self.trigger('graphics_click');
                });
                this.graphics.on('mouseup', function () {
                    self.trigger('graphics_mouseup');
                });
                this.graphics.on('mousedown', function () {
                    self.trigger('graphics_mousedown');
                });
                this.graphics.on('mouseover', function () {
                    self.trigger('graphics_mouseover');
                });
                this.graphics.on('mouseout', function () {
                    self.trigger('graphics_mouseout');
                });
                this.graphics.on('mousemove', function (sender, coord) {
                    self.trigger('graphics_mousemove', coord);
                });
            },

            _bindHandlers: function () {
                /// <summary>
                /// Binds handlers
                /// </summary>
                _.extend(this, Backbone.Events);

                this._bindInfobar();
                this._bindNavbar();
                this._bindGraphics();
            },
            renderGraphics: function (paper) {
                /// <summary>
                /// Renders graphics on paper
                /// </summary>
                /// <param name="paper"></param>
                if (typeof this.graphics == 'undefined') return;
                this.graphics.renderOnPaper(paper);
                var attrs = this.graphics._el.attr();
            },
            updateInfobar: function (attrs) {
                /// <summary>
                /// Updates infobar
                /// </summary>
                /// <param name="attrs"></param>

            },
            updateNavbar: function (attrs) {
                /// <summary>
                /// Updates navbar
                /// </summary>
                /// <param name="attrs"></param>
            },
            updateGraphics: function (attrs) {
                /// <summary>
                /// Updates graphics
                /// </summary>
                /// <param name="attrs"></param>
                if (typeof this.graphics == 'undefined') return;
                this.graphics.set(attrs);
                this.graphics.update();

            },
            update: function (attrs) {
                /// <summary>
                /// Updates view
                /// </summary>
                /// <param name="attrs"></param>
                if (typeof attrs == 'undefined') return;
                this.updateInfobar(attrs);
                this.updateNavbar(attrs);
                this.updateGraphics(attrs);
            },
            remove: function () {
                $(this.infobar).remove();
                $(this.navbar).remove();
                this.graphics.remove();
                this.trigger('removed', this);
            }
        });

        return BaseView;
    });