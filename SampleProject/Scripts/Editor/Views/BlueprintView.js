// BlueprintView module
// Responsible for displaying blueprint data

define([
        'jquery',
        'underscore',
        'backbone',
        'text!templates/toolbar.htm',
        'text!templates/navbar.htm',
        'text!templates/infobar.htm',
        'views/canvasview',
        'views/rectangleview'
    ], function ($, _, Backbone, toolbarTemplate, navbarTemplate, infobarTemplate, CanvasView, RectangleView) {

        var BlueprintView = Backbone.View.extend({

            initialize: function () {
                /// <summary>
                /// constructor
                /// </summary>
                this._renderToolbar();
                this._renderNavbar();
                this._renderInfobar();
                this.btnSave = $("#btnSave", this.toolbar);
                this.btnLoad = $("#btnLoad", this.toolbar);
                this.btnAddRect = $("#btnAddRect", this.toolbar);
                this.btnDelRect = $("#btnDelRect", this.toolbar);
                this.btnBlueprintEdit = $('#btnBlueprintEdit', this.infobar);
                this._bindHandlers();

                this._initCanvas();

                this.rectangles = {};

                this._setStartState();
            },

            _initCanvas: function () {
                this.canvas = new CanvasView();
                $("#canvas_nav", this.navbar).append(this.canvas.navbar);
            },

            _renderToolbar: function () {
                this.toolbar = this.make("div", { class: "btn-toolbar" });
                var context = {};
                var compiledTemplate = _.template(toolbarTemplate, context);
                $(this.toolbar).html(compiledTemplate);
            },

            _renderNavbar: function () {
                this.navbar = this.make("div", { class: "sidebar-nav" });
                var context = {};
                var compiledTemplate = _.template(navbarTemplate, context);
                $(this.navbar).html(compiledTemplate);
            },
            _renderInfobar: function () {
                this.infobar = this.make("div", { class: "row" });
                var context = {};
                var compiledTemplate = _.template(infobarTemplate, context);
                $(this.infobar).html(compiledTemplate);
            },

            _bindHandlers: function () {
                /// <summary>
                /// Binds handlers to buttons
                /// </summary>
                _.extend(this, Backbone.Events);

                var self = this;
                this.btnSave.click(function () {
                    self.trigger("save_request");
                });
                this.btnLoad.click(function () {
                    self.trigger("load_request");
                });
                this.btnBlueprintEdit.click(function () {
                    self.trigger("edit_request");
                });
                this.btnAddRect.click(function () {
                    self.trigger("add_rect_request");
                });
                this.btnDelRect.click(function () {
                    self.trigger("del_rect_request");
                });
            },

            _setStartState: function () {
                /// <summary>
                ///Set initial state of view (before first update)
                /// </summary>
                this.state = 'init';
                this.btnSave.html("Create");
                this.btnLoad.attr("disabled", "disabled");
            },

            _setReadyState: function (id) {
                /// <summary>
                /// Set ready view state (then model is loaded)
                /// </summary>
                this.btnSave.html("Save");
                this.btnLoad.removeAttr("disabled");
            },

            addRectangle: function (rectData) {
                var rectView = new RectangleView();
                rectView.graphics.renderOnPaper(this.canvas.paper);
                $("#rect_nav", this.navbar).append(rectView.navbar);
                this.rectangles[rectData.cid] = rectView;
                return rectView;
            },

            update: function (attrs) {
                if (typeof attrs == 'undefined') return;
                if (typeof attrs.id != 'undefined') {
                    this._setReadyState(attrs.id);
                }
                if (typeof attrs.Name != 'undefined') {
                    $("#blueprint_name", this.infobar).html(attrs.Name);
                }
                if (typeof attrs.Description != 'undefined') {
                    $("#blueprint_desc", this.infobar).html(attrs.Description);
                }
            }
        });

        return BlueprintView;
    });