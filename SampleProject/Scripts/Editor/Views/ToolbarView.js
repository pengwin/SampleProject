//Toolbar view
// fires requests events from tool buttons:
// * save_request
// fetches state from click toggled buttons
// provides public field with state of click action button group

define([
        'jquery',
        'underscore',
        'backbone',
        'text!templates/toolbar.htm'
    ], function ($, _, Backbone, toolbarTemplate) {

        var ToolbarView = Backbone.View.extend({

            initialize: function () {
                /// <summary>
                /// Constructor.
                /// </summary>

                this.renderToolbar();

                this.bindHandlers();

                this.clickState = {
                    drawRect: false,
                    drawEllipse: false,
                    edit: false,
                    move: false,
                    del: false
                };
            },

            _resetClickState: function () {
                this.clickState.drawRect = false;
                this.clickState.drawEllipse = false;
                this.clickState.edit = false;
                this.clickState.move = false;
                this.clickState.del = false;
            },

            bindHandlers: function () {
                /// <summary>
                /// Binds handlers to el buttons
                /// </summary>

                var self = this;
                _.extend(this, Backbone.Events);

                $("#btnSave", this.el).click(function () {
                    self.trigger('save_request');
                });
                $("#btnLoad", this.el).click(function () {
                    self.trigger('load_request');
                });
                $("#btnAddRect", this.el).click(function () {
                    self.trigger('add_rect_request');
                });
                $("#btnAddEllipse", this.el).click(function () {
                    self.trigger('add_ellipse_request');
                });
                $("#btnDelLast", this.el).click(function () {
                    self.trigger('del_last_request');
                });

                $("#btnDrawRect", this.el).click(function () {
                    self._resetClickState();
                    self.clickState.drawRect = true;
                });
                $("#btnDrawEllipse", this.el).click(function () {
                    self._resetClickState();
                    self.clickState.drawEllipse = true;
                });
                $("#btnEdit", this.el).click(function () {
                    self._resetClickState();
                    self.clickState.edit = true;
                });
                $("#btnMove", this.el).click(function () {
                    self._resetClickState();
                    self.clickState.move = true;
                });
                $("#btnDelete", this.el).click(function () {
                    self._resetClickState();
                    self.clickState.del = true;
                });
            },

            renderToolbar: function () {
                this.el = this.make("div", { class: "btn-toolbar" });
                var context = {};
                var compiledTemplate = _.template(toolbarTemplate, context);
                $(this.el).html(compiledTemplate);
            },

            setStartState: function () {
                /// <summary>
                ///Set initial state of view (before first update)
                /// </summary>
                this.state = 'init';
                $("#btnSave", this.el).html("Create");
                $("#btnLoad", this.el).attr("disabled", "disabled");
            },

            setReadyState: function () {
                /// <summary>
                /// Set ready view state (then model is loaded)
                /// </summary>
                $("#btnSave", this.el).html("Save");
                $("#btnLoad", this.el).removeAttr("disabled");
            }
        });

        return ToolbarView;
    });


