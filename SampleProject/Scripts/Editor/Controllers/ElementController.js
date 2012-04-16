// RectangleController module
// controller set the link between rectangle model and rectangle view

define([
        'jquery',
        'underscore',
        'backbone',
        'actions/resizeaction',
        'actions/deleteaction',
        'actions/moveaction',
        'controllers/basecontroller'
    ], function ($, _, Backbone, ResizeAction, DeleteAction, MoveAction, BaseController) {

        var ElementController = BaseController.extend({
            formCaption: "BlueprintElement",
            modelExcludedFields: ['type'],

            initialize: function (attrs) {
                BaseController.prototype.initialize.call(this, attrs);

                if (typeof attrs.clickState == 'undefined') {
                    throw "clickState attribute is undefined";
                }
                this.clickState = attrs.clickState;
            },

            initActions: function () {
                BaseController.prototype.initActions.call(this);
                this.resizeAction = new ResizeAction(this.model);
                this.deleteAction = new DeleteAction(this.model);
                this.moveAction = new MoveAction(this.model);
            },
            bindHandlers: function () {

                BaseController.prototype.bindHandlers.call(this);

                var self = this;

                this.view.on('navbar_click', function () {
                    self.editAction.execute();
                });

                this.view.on('graphics_click', function () {
                    if (self.clickState.edit) {
                        self.editAction.execute();
                    }
                    if (self.clickState.del) {
                        self.deleteAction.execute();
                    }

                });

                this.view.on('graphics_mouseover', function () {
                    self.view.graphics.opacity = self.view.graphics.opacity / 1.5;
                    self.view.graphics.update();
                });

                this.view.on('graphics_mouseout', function () {
                    self.view.graphics.opacity = self.view.graphics.opacity * 1.5;
                    self.view.graphics.update();
                });

                this.view.on('graphics_mousedown', function () {
                    if (self.clickState.move) {
                        self.moveAction.execute();
                    }
                });

                this.view.on('graphics_mouseup', function () {
                    if (self.clickState.move) {
                        self.moveAction.execute();
                    }
                });

                this.view.on('graphics_mousemove', function (cursorPos) {
                    self.moveAction.setPosition(cursorPos);
                });
            }
        });

        return ElementController;
    });